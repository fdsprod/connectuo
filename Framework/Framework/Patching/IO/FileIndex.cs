using System;
using System.IO;
//using ConnectUO.Config;
using ConnectUO;
using ConnectUO.Framework;
using ConnectUO.Framework.Data;
using ConnectUO.Framework.Services;
using Ninject;

namespace CUODesktop.PatchLib
{
	public class FileIndex
	{
		private Entry3D[] m_Index;
		private Stream m_Stream;

		public int IndexCount { get { return m_Index.Length; } }

        public int Length { get { return m_Index.Length; } }
		public Stream Stream{ get{ return m_Stream; } }

		public void Close()
		{
			if( m_Stream != null )
				m_Stream.Close();
		}

		public void Dispose()
		{
			m_Index = null;

			if( m_Stream != null ) 
				m_Stream.Dispose();
		}

        public Stream Seek(int index, out int length, out int extra, out bool patched)
        {
            if (index < 0 || index >= m_Index.Length)
            {
                length = extra = 0;
                patched = false;
                return null;
            }

            Entry3D e = m_Index[index];

            if (e.lookup < 0)
            {
                length = extra = 0;
                patched = false;
                return null;
            }

            length = e.length & 0x7FFFFFFF;
            extra = e.extra;

            if (m_Stream == null)
            {
                length = extra = 0;
                patched = false;
                return null;
            }

            patched = false;

            m_Stream.Seek(e.lookup, SeekOrigin.Begin);
            return m_Stream;
        }

		public Stream Seek( int index )
		{
			if ( index < 0 || index >= m_Index.Length )
				return null;
	
			Entry3D e = m_Index[index];

			if ( e.lookup < 0 )
				return null;

			if ( m_Stream == null )
				return null;		

			m_Stream.Seek( e.lookup, SeekOrigin.Begin );
			return m_Stream;
		}

		public FileIndex(IKernel kernel, string idxFile, string mulFile, int length)
		{
			m_Index = new Entry3D[length];

            string uofolder = kernel.Get<ISettingsService>().UltimaOnlineDirectory;

            if( String.IsNullOrEmpty(uofolder) )
                throw new Exception("An installation of Ultima Online was not found on this machine" );

            string idxPath = Path.Combine(uofolder, idxFile);
            string mulPath = Path.Combine(uofolder, mulFile);

			if( idxPath != null && mulPath != null )
			{
				using( FileStream index = new FileStream(idxPath, FileMode.Open, FileAccess.Read, FileShare.Read) )
				{
					BinaryReader bin = new BinaryReader(index);
					m_Stream = new FileStream(mulPath, FileMode.Open, FileAccess.Read, FileShare.Read);

					int count = (int)( index.Length / 12 );

					for( int i = 0; i < count && i < length; ++i )
					{
						m_Index[i].lookup = bin.ReadInt32();
						m_Index[i].length = bin.ReadInt32();
						m_Index[i].extra = bin.ReadInt32();
					}

					for( int i = count; i < length; ++i )
					{
						m_Index[i].lookup = -1;
						m_Index[i].length = -1;
						m_Index[i].extra = -1;
					}
				}
			}
		}

        public Entry3D this[int index]
        {
            get { return m_Index[index]; }
        }
	}

	public struct Entry3D
	{
		public int lookup;
		public int length;
		public int extra;
	}
}