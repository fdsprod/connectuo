using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;
using System.Globalization;

namespace ConnectUO.Framework
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class StartupArgumentMemberAttribute : Attribute
	{
		/// <summary>
		/// Supported <see cref="System.Reflection.BindingFlags"/>.
		/// </summary>
		public const BindingFlags SupportedBindingFlags = 
			  BindingFlags.DeclaredOnly
			| BindingFlags.Instance
			| BindingFlags.NonPublic
			| BindingFlags.Public
			| BindingFlags.Static
			| BindingFlags.FlattenHierarchy;

		/// <summary>
		/// Supported <see cref="System.Reflection.MemberTypes"/>.
		/// </summary>
		public const MemberTypes SupportedMemberTypes =
			MemberTypes.Field
			| MemberTypes.Property;

        private static ResourceManager _resourseManager;
        private static CultureInfo _culture;

		private List<string> _aliases = null;
        private string _description = null;
        private string _resourceId = null;
        private object _id = null;
        private bool _switchMeansFalse = false;

		/// <summary>
        /// Creates a new instance of <see cref="StartupArgumentMemberAttribute"/> class with no related command line argument aliases.
		/// </summary>
		public StartupArgumentMemberAttribute()
		{
			_aliases = new List<string>();
		}

		/// <summary>
        /// Creates a new insance of <see cref="StartupArgumentMemberAttribute"/> class with one or more possible related command line argument aliases.
		/// </summary>
		/// <param name="aliases">One or more possible related command line argument aliases.</param>
        public StartupArgumentMemberAttribute(params String[] aliases)
		{
			_aliases = new List<string>(aliases);
		}

		/// <summary>
		/// Gets the possible related command line argument aliases.
		/// </summary>
		public List<string> Aliases
		{
			get {return _aliases;}
		}

		/// <summary>
		/// Gets or sets the description of the command line argument.
		/// </summary>
		public String Description
		{
			get {return _description;}
			set {_description = value;}
		}

		/// <summary>
		/// Indicates the meaning of a command line switch.
		/// </summary>
		public bool SwitchMeansFalse
		{
			get {return _switchMeansFalse;}
			set {_switchMeansFalse = value;}
		}

		/// <summary>
		/// Gets or sets the ID of this instance.
		/// </summary>
		public object ID
		{
			get {return _id;}
			set {_id = value;}
		}
        
		/// <summary>
		/// Gets or sets the <see cref="System.Globalization.ResourceManager"/> to be used for retrieving culture aware aliases.
        /// <seealso cref="StartupArgumentMemberAttribute.Culture"/>
        /// <seealso cref="StartupArgumentMemberAttribute.ResID"/>
		/// </summary>
        public static ResourceManager ResourceManager
		{
			get {return _resourseManager;}
			set {_resourseManager = value;}
		}

		/// <summary>
		/// Gets or sets the <see cref="System.Globalization.CultureInfo"/> to be used for retrieving culture aware aliases.
        /// <seealso cref="StartupArgumentMemberAttribute.Resources"/>
        /// <seealso cref="StartupArgumentMemberAttribute.ResID"/>
		/// </summary>
		public static CultureInfo Culture
		{
			get {return _culture;}
			set {_culture = value;}
		}

		/// <summary>
		/// Gets or sets the resource ID to be used for retrieving culture aware aliases.
        /// <seealso cref="StartupArgumentMemberAttribute.Resources"/>
        /// <seealso cref="StartupArgumentMemberAttribute.Culture"/>
		/// </summary>
		public String ResourceId
		{
			get {return _resourceId;}
			set {_resourceId = value;}
		}
	}
}
