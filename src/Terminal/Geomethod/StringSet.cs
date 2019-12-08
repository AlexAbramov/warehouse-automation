using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading;
using System.Resources;

namespace Geomethod
{
	public class StringSetNotFoundException : GeomethodException { public StringSetNotFoundException(string msg) : base(msg) { } }	
	public class StringSetNotLoadedException : GeomethodException { public StringSetNotLoadedException(string msg) : base(msg) { } }
	public class StringSetKeyNotFoundException : GeomethodException { public StringSetKeyNotFoundException(string msg) : base(msg) { } }
	
	public interface IStringSet
  {
		bool IsLoaded { get; }
		string Name { get; }
		ICollection Names{get;}
		string this[string key]{get;set;}
		bool IgnoreKeyNotFound { get; set; }
	}

	public class StringSet : IStringSet
	{
		#region Fields
		string name = null;
		ArrayList names = null;
	  Hashtable items=null;
		bool ignoreKeyNotFound = false;
		bool virtualMode = true;
		#endregion

		#region Properties
		public bool IsLoaded { get { return name!=null && items!=null; } }
		public string Name { get { return name; } }
		public ICollection Names { get { return names; } }
		public bool IgnoreKeyNotFound { get { return ignoreKeyNotFound; } set { ignoreKeyNotFound = value; } }
		public bool VirtualMode { get { return virtualMode; } set { virtualMode = value; } }
		#endregion

		#region Construction
		public StringSet()
		{
		}
		#endregion 

		#region Loading
		public void LoadFromResource(string resourceName, string stringSetName)
		{
  		Stream stream=Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
			LoadFromStream(stream, Encoding.UTF8, stringSetName);
		}
		public void LoadFromStream(Stream stream,Encoding encoding,string stringSetName)
		{
			using (StreamReader sr = new StreamReader(stream, encoding))
			{
				CsvConverter csvConverter = new CsvConverter();
				StringCollection row = new StringCollection();
				int colIndex = -1;
				for (int lineIndex = 0; ; lineIndex++)
				{
					string line = sr.ReadLine();
					if (line == null) break;
					if (line.Length == 0) continue;
					csvConverter.Parse(line, row);
					if (lineIndex == 0)// csv file header
					{
						colIndex = row.IndexOf(stringSetName);
						if (colIndex < 0) throw new StringSetNotFoundException(string.Format("String set name '{0}' not found in the header: {1}", stringSetName, line));
						for (int i = 1; i < row.Count; i++) names.Add(row[i]);
					}
					else 
					{
						if (items == null) items = new Hashtable(512);
						string key = row[0];
						string val = row[colIndex];
						items.Add(key, val);
					}
				}
			}
		}
		#endregion

		#region IStringSet Members
		string IStringSet.this[string key]
		{
			get
			{
				if (virtualMode) return AmendString(key);
				if (items == null) throw new StringSetNotLoadedException("String set not loaded. Requested key: "+key);
				string s = (string)items[key];
				if (s == null)
				{
					if (ignoreKeyNotFound) s = key;
					else throw new StringSetKeyNotFoundException("String set key not found: " + key);
				}
				return s;
			}
			set
			{
				items[key] = value;
			}
		}

		private string AmendString(string s)
		{
			StringBuilder sb = new StringBuilder(s.Length+5);
			char prevChar=char.MinValue;
			foreach (char c in s)
			{
				if(c!='_')
				{
					if(char.IsLower(prevChar) && char.IsUpper(c))
					{
						sb.Append(' ');
					}
					if (sb.Length == 0 && char.IsLower(c))
					{
						sb.Append(char.ToUpper(c));
					}
					else sb.Append(c);
				}
				prevChar = c;
			}
			return sb.ToString();
		}
		#endregion
	}

}
