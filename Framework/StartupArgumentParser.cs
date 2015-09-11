using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Collections;
using System.Reflection;

namespace ConnectUO.Framework
{
	/// <summary>
	/// Enumerates possible command line argument formats.
	/// </summary>
	[Flags]
	public enum StartupArgumentFormats
	{
		/// <summary>
		/// Name / value pairs. Eg. -foo=bar
		/// </summary>
		NamedValue = 0x1,
		/// <summary>
		/// Flags. Eg. -abcd means options a, b, c and d are on.
		/// </summary>
		Flags = 0x2,
		/// <summary>
		/// All supported formats.
		/// </summary>
		All = 0x7FFF
	}

	/// <summary>
	/// Collection of accepted flag name/values combinations.
	/// </summary>
	/// <remarks>The key is the flag identifier and the value is all possible flag values. If you want no flag identifier, simply use an empty string as key.</remarks>
	public class StartupArgumentFlagCollection : StringDictionary
	{
		/// <summary>
		/// Initializes a new instance of the FlagCollection.
		/// </summary>
		public StartupArgumentFlagCollection() : base() {}
	}

	/// <summary>
	/// Represents a parsing class for command line arguments.
	/// </summary>
	public class StartupArgumentParser
    {
        public const string FlagNameCaptureName = "flagName";
        public const string FlagsCaptureName = "flags";
        public const string PrefixCaptureName = "prefix";
        public const string ArgumentNameCaptureName = "name";
        public const string ArgumentValueCaptureName = "value";

		private StringDictionary _handled = null;
		private StringDictionary _unhandled = null;
        private bool _ignoreCase = false;

        private bool _useOnlyCustomPattern = false;
        private char _literalStringSymbol = '@';
        private string _customPattern = null;

        private char[] _allowedPrefixes = null;
        private char[] _assignSymbols = null;

        private StartupArgumentFormats _allowedFormats = StartupArgumentFormats.All;
        private StartupArgumentFlagCollection _flags = null;

        /// <summary>
        /// Gets the argument(s) that have been automatically set by AutoSetMembers method.
        /// </summary>
        public StringDictionary HandledArguments
        {
            get { return _handled; }
        }

        /// <summary>
        /// Gets the argument(s) that have not been automatically set by AutoSetMembers method.
        /// </summary>
        public StringDictionary UnhandledArguments
        {
            get { return _unhandled; }
        }

        /// <summary>
        /// Gets or sets the accepted argument format(s).
        /// </summary>
        public StartupArgumentFormats ArgumentFormats
        {
            get { return _allowedFormats; }
            set { _allowedFormats = value; }
        }

        /// <summary>
        /// Gets or sets the accepted prefix(es).
        /// </summary>
        public char[] AllowedPrefixes
        {
            get { return _allowedPrefixes; }
            set { _allowedPrefixes = value; }
        }

        /// <summary>
        /// Gets or sets the assignation symbol(s).
        /// </summary>
        public char[] AssignSymbols
        {
            get { return _assignSymbols; }
            set { _assignSymbols = value; }
        }

        /// <summary>
        /// Gets or sets the case sensitive status.
        /// </summary>
        public bool IgnoreCase
        {
            get { return _ignoreCase; }
            set { _ignoreCase = value; }
        }

        /// <summary>
        /// Gets or sets the litteral string symbol.
        /// </summary>
        public char LiteralStringSymbol
        {
            get { return _literalStringSymbol; }
            set { _literalStringSymbol = value; }
        }

        /// <summary>
        /// Gets or sets the custom pattern.
        /// </summary>
        public string CustomPattern
        {
            get { return _customPattern; }
            set { _customPattern = value; }
        }

        /// <summary>
        /// Gets or sets the override status of the custom pattern.
        /// </summary>
        public bool UseOnlyCustomPattern
        {
            get { return _useOnlyCustomPattern; }
            set { _useOnlyCustomPattern = value; }
        }

		/// <summary>
		/// Parses an array of arguments, typically obtained from Main method of console application.
		/// </summary>
		/// <remarks>By default, all supported formats will be accepted and parsing is case sensitive.</remarks>
		public StartupArgumentParser() : this(StartupArgumentFormats.All, false, null, null) {}

		/// <summary>
		/// Parses an array of arguments, typically obtained from Main method of console application.
		/// </summary>
		/// <param name="format">The valid format(s).</param>
		public StartupArgumentParser(StartupArgumentFormats format) : this(format, false, null, null) {}

		/// <summary>
		/// Parses an array of arguments, typically obtained from Main method of console application.
		/// </summary>
		/// <param name="format">The valid format(s).</param>
		/// <param name="ignoreCase">Indicates if parsing is case sensitive.</param>
		public StartupArgumentParser(StartupArgumentFormats format, bool ignoreCase) : this(format, ignoreCase, null, null) {}

		/// <summary>
		/// Parses an array of arguments, typically obtained from Main method of console application.
		/// </summary>
		/// <param name="format">The valid format(s).</param>
		/// <param name="ignoreCase">Indicates if parsing is case sensitive.</param>
		/// <param name="flags">Accepted flags (named or not) and their respective values.</param>
		public StartupArgumentParser(StartupArgumentFormats format, bool ignoreCase, StartupArgumentFlagCollection flags) : this(format, ignoreCase, flags, null) {}

		/// <summary>
		/// Parses an array of arguments, typically obtained from Main method of console application.
		/// </summary>
		/// <param name="format">The valid format(s).</param>
		/// <param name="flags">Accepted flags (named or not) and their respective values.</param>
		/// <param name="ignoreCase">Indicates if parsing is case sensitive.</param>
		/// <param name="customPattern">An additional regular expression pattern to be used when parsing arguments. It will have priority over the standard pattern.</param>
		/// <remarks>In your pattern, use capture name constants made public by this class.</remarks>
		public StartupArgumentParser(StartupArgumentFormats format, bool ignoreCase, StartupArgumentFlagCollection flags, string customPattern)
		{
			_handled = new StringDictionary();
			_unhandled = new StringDictionary();

			_allowedFormats = format;
			_ignoreCase = ignoreCase;
			_flags = flags;
			_customPattern = customPattern;

			// default values

			_allowedPrefixes = new char[] {'-', '/'};
			_assignSymbols = new char[] {'=', ':'};
		}

		/// <summary>
		/// Parses an array of arguments, typically obtained from Main method of console application.
		/// </summary>
		/// <param name="customPattern">The custom pattern to be used.</param>
		/// <param name="useOnlyCustomPattern">Indicates if the custom pattern override the built-in pattern.</param>
		/// <remarks>In your pattern, use capture name constants made public by this class.</remarks>
		public StartupArgumentParser(string customPattern, bool useOnlyCustomPattern)
		{
			_handled = new StringDictionary();
			_unhandled = new StringDictionary();

			_customPattern = customPattern;
			_useOnlyCustomPattern = useOnlyCustomPattern;
		}

		/// <summary>
		/// Parses the array of arguments.
		/// </summary>
		/// <param name="args">The array of arguments.</param>
		/// <returns>The dictionary holding parsed arguments.</returns>
		public StringDictionary Parse(string[] args)
		{
			int noNameCount = 0;

			Regex argex = BuildPattern();

			foreach(string arg in args)
			{
				Match match = argex.Match(arg);

				if (!match.Success)
					throw new ArgumentException("Invalid argument format: " + arg);
				else
				{
					if (match.Groups[PrefixCaptureName].Success)
					{
						if (match.Groups[FlagsCaptureName].Success)
							_unhandled.Add(match.Groups[FlagNameCaptureName].Value, match.Groups[FlagsCaptureName].Value);
						else
							_unhandled.Add(match.Groups[ArgumentNameCaptureName].Value, match.Groups[ArgumentValueCaptureName].Value);
					}
					else
					{
						_unhandled.Add(noNameCount.ToString(), match.Groups[ArgumentValueCaptureName].Value);
						noNameCount++;
					}
				}
			}

			return _unhandled;
		}

		/// <summary>
		/// Builds the pattern to be used when parsing each argument.
		/// </summary>
		/// <returns>A Regex instance to be used for parsing arguments.</returns>
		private Regex BuildPattern()
		{
			// The whole parsing string (with all possible argument formats) :
			// ---------------------------------------------------------------
			// (CUSTOM_PATTERN)
			// |(^(?<prefix>[PREFIXES])(?<flagName>)FLAG_NAMES)(?<flags>[FlagsCaptureName]+)$)
			// |(^(?<prefix>[PREFIXES])(?<name>[^EQUAL_SYMBOLS]+)([EQUAL_SYMBOLS](?<value>.+))?$)
			// |(LITERAL_STRING_SYMBOL?(?<value>.*))
			//
			// Again, but commented :
			// ----------------------
			// (CUSTOM_PATTERN)|				# custom pattern, if any (it has priority over standard pattern)
			//
			// foreach flag in FlagCollection :
			//
			// (^
			// (?<prefix>[PREFIXES])			# mandatory prefix
			// (?<flagName>)FLAG_NAMES)			# flag name
			// (?<flags>[FlagsCaptureName]+)				# flag value
			// $)|
			//
			// (^
			// (?<prefix>[PREFIXES])			# mandatory prefix
			// (?<name>[^EQUAL_SYMBOLS]+)		# argument name (which includes flag name/values)
			// ([EQUAL_SYMBOLS](?<value>.+))?	# argument value, if any
			// $)
			//
			// |(
			// LITERAL_STRING_SYMBOL?			# optional @ caracter indicating literal string
			// (?<value>.*)						# standalone value (will be given an index when parsed in Parse() method)
			// )

			RegexOptions regexOptions = RegexOptions.ExplicitCapture;

			if (_ignoreCase)
				regexOptions |= RegexOptions.IgnoreCase;

			if (_useOnlyCustomPattern)
				return new Regex(_customPattern, regexOptions);
			else
			{
				StringBuilder argPattern = new StringBuilder();

				// build prefixes pattern
				StringBuilder prefixesBuilder = new StringBuilder();

				if (_allowedPrefixes != null)
					if (_allowedPrefixes.Length != 0)
					{
						prefixesBuilder.Append("(?<" + PrefixCaptureName + ">[");
					
						foreach (char prefix in _allowedPrefixes)
							prefixesBuilder.Append(prefix);

						prefixesBuilder.Append("])");
					}
			
				string prefixes = prefixesBuilder.ToString();

				// build equality symbols pattern
				string equalSymbols = '[' + new string(_assignSymbols) + ']';

				// build custom pattern
				if (_customPattern != null)
				{
					argPattern.Append('(');
					argPattern.Append(_customPattern);
					argPattern.Append(")|");
				}

				// build flag pattern(s)
				if ((_allowedFormats & StartupArgumentFormats.Flags) != 0)
				{
					foreach (DictionaryEntry flag in _flags)
					{
						// pattern template : (^PREFIX_PATTERN(?<flagName>KEY)(?<flags>[ArgumentValueCaptureName]+))|
						// eg. (^(?<prefix>[-/])(?<flagName>a)(?<flags>[ABCDEFG]+))|

						argPattern.Append("(^");
						argPattern.Append(prefixes);
						argPattern.Append("(?<" + FlagNameCaptureName + ">");
						argPattern.Append(flag.Key);
						argPattern.Append(")(?<" + FlagsCaptureName + ">[");
						argPattern.Append(flag.Value);
						argPattern.Append("]+)$)|");
					}
				}

				// named arguments pattern
				argPattern.Append("(^");
				argPattern.Append(prefixes);

				if ((_allowedFormats & StartupArgumentFormats.NamedValue) != 0)
				{
					argPattern.Append("(?<" + ArgumentNameCaptureName + ">[^");
					argPattern.Append(_assignSymbols);
					argPattern.Append("]+)");

					argPattern.Append("([");
					argPattern.Append(_assignSymbols);
					argPattern.Append("](?<" + ArgumentValueCaptureName + ">.+))?");
				}
				else
				{
					argPattern.Append("(?<" + ArgumentNameCaptureName + ">.+)");
				}

				argPattern.Append("$)");

				// standalone values
				argPattern.Append("|(@?(?<" + ArgumentValueCaptureName + ">.*))");

				return new Regex(argPattern.ToString(), regexOptions);
			}
		}

		/// <summary>
		/// Automatically sets members for the provided <see cref="System.Reflection.Assembly"/>.
		/// </summary>
		/// <param name="assembly">The <see cref="System.Reflection.Assembly"/> to process.</param>
		public void AutoSetMembers(Assembly assembly)
		{
			Type[] types = assembly.GetTypes();

			foreach (Type type in types)
				AutoSetMembers(type);
		}

		/// <summary>
		/// Automatically sets members for the provided <see cref="System.Type"/>.
		/// </summary>
		/// <param name="type">The <see cref="System.Type"/> to process.</param>
		/// <remarks>Only static members will be processed.</remarks>
		public void AutoSetMembers(Type type)
		{
			MemberInfo[] members = type.FindMembers(StartupArgumentMemberAttribute.SupportedMemberTypes, StartupArgumentMemberAttribute.SupportedBindingFlags, Type.FilterName, "*");

			foreach (MemberInfo member in members)
				AutoSetMembers(type, member);
		}

		/// <summary>
		/// Automatically sets members for the provided class instance.
		/// </summary>
		/// <param name="instance">The class instance to process. Must not be null.</param>
		/// <remarks>Both static and instance members will be processed.</remarks>
		public void AutoSetMembers(object instance)
		{
			MemberInfo[] members = instance.GetType().FindMembers(StartupArgumentMemberAttribute.SupportedMemberTypes, StartupArgumentMemberAttribute.SupportedBindingFlags, Type.FilterName, "*");

			foreach (MemberInfo member in members)
				AutoSetMembers(instance, member);
		}

		/// <summary>
		/// Automatically sets member of the provided class instance or <see cref="System.Type"/>.
		/// </summary>
		/// <param name="classToProcess">The class instance or <see cref="System.Type"/> to process.</param>
		/// <param name="member">The member which will be set. Must be a field or a property.</param>
		/// <remarks>Both static and instance members are accepted.</remarks>
		public void AutoSetMembers(object classToProcess, MemberInfo member)
		{
			StartupArgumentMemberAttribute attrib = Attribute.GetCustomAttribute(member, typeof(StartupArgumentMemberAttribute)) as StartupArgumentMemberAttribute;

			if (attrib != null)
			{
				if (attrib.ResourceId != null && StartupArgumentMemberAttribute.ResourceManager != null)
                    attrib.Aliases.Add(StartupArgumentMemberAttribute.ResourceManager.GetString(attrib.ResourceId, StartupArgumentMemberAttribute.Culture));

				string argValue = null;
				bool found = false;

				foreach (string alias in attrib.Aliases)
				{
					if (_unhandled.ContainsKey(alias))
					{
						argValue = (string)_unhandled[alias];

						_handled.Add(alias, argValue);
						_unhandled.Remove(alias);

						found = true;
						break;
					}
					else if (_handled.ContainsKey(alias))
					{
						argValue = (string)_handled[alias];

						found = true;
						break;
					}
				}

				if (found)
				{
					Type memberType = null;

					switch (member.MemberType)
					{
						case MemberTypes.Property:
							memberType = ((PropertyInfo)member).PropertyType;
							break;

						case MemberTypes.Field:
							memberType = ((FieldInfo)member).FieldType;
							break;
					}

					if (memberType == typeof(bool))
					{
						if (argValue == "")
							SetMemberValue(classToProcess, member, !attrib.SwitchMeansFalse);
						else if (argValue == Boolean.FalseString || argValue == Boolean.TrueString)
							SetMemberValue(classToProcess, member, Boolean.Parse(argValue));
						else
							// last chance ... if can't parse it as integer, an exception will be raised
							SetMemberValue(classToProcess, member, Int32.Parse(argValue) != 0);
					}
					else if (memberType == typeof(string))
						SetMemberValue(classToProcess, member, argValue);
					else if (memberType.IsEnum)
					{
						object value = Enum.Parse(memberType, argValue, _ignoreCase);
						SetMemberValue(classToProcess, member, value);
					}
					else if (memberType.IsValueType)
						SetMemberValue(classToProcess, member, Convert.ChangeType(argValue, memberType));
				}
			}
		}

		/// <summary>
		/// Sets the static or instance member (property or field) to the specified value.
		/// </summary>
		/// <param name="instance">The class instance or <see cref="System.Type"/> to be used.</param>
		/// <param name="memberInfo">The member to be set.</param>
		/// <param name="value">The new value of the member.</param>
		private void SetMemberValue(object instance, MemberInfo memberInfo, object value)
		{
			if (memberInfo is PropertyInfo)
			{
				PropertyInfo pi = (PropertyInfo) memberInfo;

				if (pi.CanWrite)
				{
					MethodInfo methodInfo = pi.GetSetMethod(true);

					BindingFlags bindingFlags = BindingFlags.SetProperty;

					if (methodInfo.IsStatic)
						bindingFlags |= BindingFlags.Static;

					pi.SetValue(instance, value, bindingFlags, null, null, null);
				}
			}
			else if (memberInfo is FieldInfo)
			{
				FieldInfo fi = (FieldInfo) memberInfo;

				BindingFlags bindingFlags = BindingFlags.SetField;

				if (fi.IsStatic)
					bindingFlags |= BindingFlags.Static;

				fi.SetValue(instance, value, bindingFlags, null, null);
			}
		}
        
		/// <summary>
		/// Clears all saved arguments (both handled and unhandled).
		/// </summary>
		public void Clear()
		{
			_handled.Clear();
			_unhandled.Clear();
		}
	}
}
