using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Net;
using ConnectUO.Framework.Windows;

namespace ConnectUO.Framework
{
	public static class Utility
	{
		#region Serialization
		/// <summary>
		/// Serializes an object to a file in XML format.
		/// </summary>
		/// <typeparam name="T">The type of object to serialize</typeparam>
		/// <param name="path">The path the object will be serialized to.</param>
		/// <param name="item">The object to serialize</param>
		public static void Serialize<T>( string path, T item )
		{
			if ( File.Exists( path ) )
			{
				File.Delete( path );
			}
			//Open a file stream to serialize the object to.
			using ( FileStream stream = new FileStream( path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read ) )
			{
				//Create the serializer
				XmlSerializer serializer = new XmlSerializer( typeof( T ) );
				//Serialize the object to the stream.
				serializer.Serialize( stream, item );

				//Cleanup
				stream.Close();
			}
		}

		public static void Serialize<T>( T item, out string xml )
		{
			//Open a file stream to serialize the object to.
			using ( MemoryStream stream = new MemoryStream() )
			{
				//Create the serializer
				XmlSerializer serializer = new XmlSerializer( typeof( T ) );
				//Serialize the object to the stream.
				serializer.Serialize( stream, item );

				//Cleanup
				stream.Close();

				xml = Encoding.ASCII.GetString( stream.GetBuffer() );
			}

		}
		/// <summary>
		/// Serializes an object to a file in XML format.
		/// </summary>
		/// <param name="path">The path the object will be serialized to.</param>
		/// <param name="item">The object to serialize</param>
		/// <param name="tyoe">The object type</param>
		public static void Serialize( string path, object item, Type type )
		{
			//Open a file stream to serialize the object to.
			using ( FileStream stream = new FileStream( path, FileMode.OpenOrCreate ) )
			{
				//Create the serializer
				XmlSerializer serializer = new XmlSerializer( type );
				//Serialize the object to the stream.
				serializer.Serialize( stream, item );

				//Cleanup
				stream.Close();
			}
		}

		public static void Serialize( object item, Type type, out string xml )
		{
			//Open a file stream to serialize the object to.
			using ( MemoryStream stream = new MemoryStream() )
			{
				//Create the serializer
				XmlSerializer serializer = new XmlSerializer( type );
				//Serialize the object to the stream.
				serializer.Serialize( stream, item );

				//Cleanup
				stream.Close();

				xml = Encoding.ASCII.GetString( stream.GetBuffer() );
			}

		}

		/// <summary>
		/// Deserializes an XML file or xml string to an object
		/// </summary>
		/// <typeparam name="T">The type of object to deserialize</typeparam>
		/// <param name="path">The path the object will be deserialized from.</param>
		/// <returns>The deserialized object</returns>
		public static T Deserialize<T>( string xmlOrFilePath )
		{

			string xml = xmlOrFilePath;

			if ( File.Exists( xmlOrFilePath ) )
			{
				//Open a file stream to serialize the object to.
				StreamReader reader = new StreamReader( xmlOrFilePath );
				xml = reader.ReadToEnd();

				reader.Close();
				reader.Dispose();
			}

			return ConvertXmlToType<T>( xml );
		}

		/// <summary>
		/// Deserializes an XML file or xml string to an object
		/// </summary>
		/// <param name="path">The path the object will be deserialized from.</param>
		/// <param name="tyoe">The object type</param>
		/// <returns>The deserialized object</returns>
		public static object Deserialize( string xmlOrFilePath, Type type )
		{

			string xml = xmlOrFilePath;

			if ( File.Exists( xmlOrFilePath ) )
			{
				//Open a file stream to serialize the object to.
				StreamReader reader = new StreamReader( xmlOrFilePath );
				xml = reader.ReadToEnd();

				reader.Close();
				reader.Dispose();
			}

			return ConvertXmlToType( xml, type );
		}

		/// <summary>
		/// Converts an item into a DataSet
		/// </summary>
		/// <typeparam name="T">The type of object to serialize</typeparam>
		/// <param name="item">The object to serialize</param>
		/// <returns>The DataSet the object was serialized to.</returns>
		public static DataSet ConvertToDataSet<T>( T item )
		{
			//Create a memory stream to store 
			//the serialized object
			MemoryStream ms = new MemoryStream();
			//Create the serializer
			XmlSerializer serializer = new XmlSerializer( typeof( T ) );

			//Serialize the object to the stream.
			serializer.Serialize( ms, item );

			//This is important, we need to 
			//Seek back to position 0 so that
			//the DataSet will start at 0 when 
			//reading the XML, without this the 
			//DataSet will start at ms.Length 
			//and throw a "Root element was missing" Exception
			ms.Seek( 0, SeekOrigin.Begin );

			//Create the DataSet and read the XML into it.
			DataSet ds = new DataSet();
			ds.ReadXml( ms );

			return ds;
		}

		/// <summary>
		/// Converts an item into a DataSet
		/// </summary>
		/// <param name="item">The object to serialize</param>
		/// <param name="tyoe">The object type</param>
		/// <returns>The DataSet the object was serialized to.</returns>
		public static DataSet ConvertToDataSet( object item, Type type )
		{
			//Create a memory stream to store 
			//the serialized object
			MemoryStream ms = new MemoryStream();
			//Create the serializer
			XmlSerializer serializer = new XmlSerializer( type );

			//Serialize the object to the stream.
			serializer.Serialize( ms, item );

			//This is important, we need to 
			//Seek back to position 0 so that
			//the DataSet will start at 0 when 
			//reading the XML, without this the 
			//DataSet will start at ms.Length 
			//and throw a "Root element was missing" Exception
			ms.Seek( 0, SeekOrigin.Begin );

			//Create the DataSet and read the XML into it.
			DataSet ds = new DataSet();
			ds.ReadXml( ms );

			return ds;
		}

		/// <summary>
		/// Converts a DataSet into an object
		/// </summary>
		/// <typeparam name="T">The type of object to deserialize.</typeparam>
		/// <param name="dataSet">The DataSet to deserialize from.</param>
		/// <returns>The deserialized object.</returns>
		public static T ConvertDataSet<T>( DataSet dataSet ) where T : new()
		{
			if ( dataSet.Tables.Count <= 0 )
				return default( T );

			//Get the XML from the DataSet
			string xml = dataSet.GetXml();

			return ConvertXmlToType<T>( xml );
		}

		public static T ConvertXmlToType<T>( string xml )
		{
			//Get the byte[] data from the xml string
			byte[] buffer = Encoding.ASCII.GetBytes( xml );

			//Create a memory stream to store the xml
			MemoryStream ms = new MemoryStream( buffer );

			//Create the serializer
			XmlSerializer serializer = new XmlSerializer( typeof( T ) );

			//Deserialize the object from the stream
			T t = (T)serializer.Deserialize( ms );

			return t;

		}

		public static object ConvertXmlToType( string xml, Type type )
		{
			//Get the byte[] data from the xml string
			byte[] buffer = Encoding.ASCII.GetBytes( xml );

			//Create a memory stream to store the xml
			MemoryStream ms = new MemoryStream( buffer );

			//Create the serializer
			XmlSerializer serializer = new XmlSerializer( type );

			//Deserialize the object from the stream
			object o = serializer.Deserialize( ms );

			return o;

		}
		#endregion
                
		#region Conversion
		public static bool TryConvert<TConverFrom, UConvertTo>( TConverFrom convertFrom, out UConvertTo convertTo )
		{
			convertTo = default( UConvertTo );
			bool converted = false;

			TypeConverter converter = TypeDescriptor.GetConverter( convertFrom );

			if ( converter.CanConvertTo( typeof( UConvertTo ) ) )
			{
				convertTo = (UConvertTo)converter.ConvertTo( convertFrom, typeof( UConvertTo ) );
				converted = true;
			}

			return converted;
		}

		public static bool TryConvert( Type convertFrom, object from, Type convertTo, out object to )
		{
			to = null;
			bool converted = false;

			TypeConverter converter = TypeDescriptor.GetConverter( convertTo );

			if ( converter.CanConvertFrom( convertFrom ) )
			{
				to = converter.ConvertFrom( from );
				converted = true;
			}

			return converted;
		}

		public static TConverFrom ConvertReferenceType<TConverFrom, UConvertTo>( UConvertTo value )
		{
			if ( value == null )
			{
				return default( TConverFrom );
			}
			else if ( typeof( TConverFrom ).IsAssignableFrom( value.GetType() ) == true )
			{
				return (TConverFrom)((object)value);
			}

			return default( TConverFrom );
		}

		public static TConverFrom ConvertValueType<TConverFrom, UConvertTo>( UConvertTo value )
		{
			IConvertible convertible = value as IConvertible;

			if ( convertible != null )
			{
				return (TConverFrom)System.Convert.ChangeType( convertible, typeof( TConverFrom ) );
			}

			TypeConverter converter = TypeDescriptor.GetConverter( value );

			if ( converter.CanConvertTo( typeof( TConverFrom ) ) )
			{
				return (TConverFrom)converter.ConvertTo( value, typeof( TConverFrom ) );
			}

			if ( value == null )
			{
				throw new InvalidCastException( string.Format( CultureInfo.InvariantCulture,
					"Unable to cast type '{0}'.", typeof( TConverFrom ).Name ) );
			}

			return default( TConverFrom );
		}

		public static TConverFrom? ConvertNullableType<TConverFrom, UConvertTo>( UConvertTo value ) where TConverFrom : struct
		{
			if ( value == null )
			{
				return null;
			}

			IConvertible convertible = value as IConvertible;

			if ( convertible != null )
			{
				return (TConverFrom)System.Convert.ChangeType( convertible, typeof( TConverFrom ) );
			}

			TypeConverter converter = TypeDescriptor.GetConverter( value );

			if ( converter.CanConvertTo( typeof( TConverFrom ) ) )
			{
				return (TConverFrom)converter.ConvertTo( value, typeof( TConverFrom ) );
			}

			return new TConverFrom?( (TConverFrom)((object)value) );
		}
		#endregion

		public static Type[] GetTypesFromAppDomain( string typeName )
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

			List<Type> types = new List<Type>();

			for ( int i = 0; i < assemblies.Length; i++ )
			{
				Type[] typeArray = (from t in assemblies[i].GetTypes()
									where t.Name.Equals( typeName, StringComparison.CurrentCultureIgnoreCase )
									select t).ToArray();

				if ( typeArray != null )
				{
					types.AddRange( typeArray );
				}
			}

			return types.ToArray();
		}

		public static string ProperSpace( string text )
		{
			StringBuilder sb = new StringBuilder();
			string lowered = text.ToLower();

			for ( int i = 0; i < text.Length; i++ )
			{
				string a = text.Substring( i, 1 );
				string b = lowered.Substring( i, 1 );
				if ( a != b )
					sb.Append( " " );

				sb.Append( a );
			}

			return sb.ToString().Trim();
		}

		public static bool CheckValidEmail( string email )
		{
			Regex r = new Regex( "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$" );
			Match m = r.Match( email );

			return !m.Success;
		}

		public static byte[] Compress( byte[] data )
		{
			MemoryStream ms = new MemoryStream();
			DeflateStream compressedStream = new DeflateStream( ms, CompressionMode.Compress, true );

			compressedStream.Write( data, 0, data.Length );
			compressedStream.Close();

			byte[] compressedData = new byte[ms.Length];
			ms.Position = 0;
			ms.Read( compressedData, 0, compressedData.Length );
            
			return compressedData;
		}

		public static byte[] DeCompress( byte[] data, int originalSize )
		{
			MemoryStream ms = new MemoryStream( data );

			DeflateStream decompressedStream = new DeflateStream( ms,
						CompressionMode.Decompress );

			byte[] buffer = new byte[originalSize];
			ms.Position = 0;
			ReadAllBytesFromStream( decompressedStream, buffer );

			return buffer;
		}

		public static int ReadAllBytesFromStream( Stream stream, byte[] buffer )
		{
			int offset = 0;
			int bytesRead;

			while ( (bytesRead = stream.Read( buffer, offset, buffer.Length - offset )) > 0 )
			{
				offset += bytesRead;
			}

			return offset;
		}

		private static byte[] _64Encode = new byte[] { 
            0x41, 0x42, 0x43, 0x44, 0x45, 70,   0x47, 0x48, 0x49, 0x4a, 0x4b, 0x4c, 0x4d, 0x4e, 0x4f, 80, 
            0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 90,   0x61, 0x62, 0x63, 100,  0x65, 0x66, 
            0x67, 0x68, 0x69, 0x6a, 0x6b, 0x6c, 0x6d, 110,  0x6f, 0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 
            0x77, 120,  0x79, 0x7a, 0x30, 0x31, 50,   0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x2b, 0x2f
         };

		public static bool IsBase64Encoded( byte b )
		{
			for ( int i = 0; i < _64Encode.Length; i++ )
			{
				if ( _64Encode[i].Equals( b ) )
				{
					return true;
				}
			}

			return false;
		}

		private static int GetEmbeddedNullStringLengthAnsi( string s )
		{
			int index = s.IndexOf( '\0' );

			if ( index > -1 )
			{
				string str = s.Substring( 0, index );
				string str2 = s.Substring( index + 1 );
				return ((GetPInvokeStringLength( str ) + GetEmbeddedNullStringLengthAnsi( str2 )) + 1);
			}

			return GetPInvokeStringLength( s );
		}

		public static int GetPInvokeStringLength( string s )
		{
			if ( s == null )
			{
				return 0;
			}

			if ( Marshal.SystemDefaultCharSize == 2 )
			{
				return s.Length;
			}

			if ( s.Length == 0 )
			{
				return 0;
			}

			if ( s.IndexOf( '\0' ) > -1 )
			{
				return GetEmbeddedNullStringLengthAnsi( s );
			}

			return NativeMethods.lstrlen( s );
		}

		public static int HighWord( int n )
		{
			return ((n >> 0x10) & 0xffff);
		}

		public static int HighWord( IntPtr n )
		{
			return HighWord( (int)((long)n) );
		}

		public static int LowWord( int n )
		{
			return (n & 0xffff);
		}

		public static int LowWord( IntPtr n )
		{
			return LowWord( (int)((long)n) );
		}

		public static int MakeLong( int low, int high )
		{
			return ((high << 0x10) | (low & 0xffff));
		}

		public static IntPtr MakeLParam( int low, int high )
		{
			return (IntPtr)((high << 0x10) | (low & 0xffff));
		}

		public static int SignedHighWord( int n )
		{
			return (short)((n >> 0x10) & 0xffff);
		}

		public static int SignedHighWord( IntPtr n )
		{
			return SignedHighWord( (int)((long)n) );
		}

		public static int SignedLowWord( int n )
		{
			return (short)(n & 0xffff);
		}

		public static int SignedLowWord( IntPtr n )
		{
			return SignedLowWord( (int)((long)n) );
		}
	}		
}
