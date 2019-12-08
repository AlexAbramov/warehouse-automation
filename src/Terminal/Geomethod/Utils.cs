using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace Geomethod
{
	public class Utils
	{
		static string baseDir = ".\\";
		public static string BaseDirectory
		{
			get
			{
				return baseDir;
			}
			set{baseDir=value;}
		}
		public static int FloatToInt(float f)
		{
			float[] ff={f};
			int[] ii=new int[1];
			Buffer.BlockCopy(ff,0,ii,0,4);
			return ii[0];
		}
		public static float IntToFloat(int i)
		{
			int[] ii={i};
			float[] ff=new float[1];
			Buffer.BlockCopy(ii,0,ff,0,4);
			return ff[0];
		}
	}

	public class PathUtils
	{
		public static string BaseDirectory { get { return Utils.BaseDirectory; } }
		public static string GetExtension(string path)
		{
			string ext = Path.GetExtension(path);
			StringUtils.RemoveFirstChar(ref ext);
			return ext;
		}
		public static string AbsFilePath(string filePath)
		{
			if (filePath.IndexOf(':') > 0) return filePath;
			if (filePath.StartsWith("\\")) filePath = filePath.Remove(0, 1);
			return BaseDirectory + filePath;
		}
		public static string TrimFilePath(string filePath)
		{
			string s1 = filePath.ToLower();
			string s2 = BaseDirectory.ToLower();
			int pos = s1.IndexOf(s2);
			if (pos >= 0)
			{
				pos += s2.Length;
				return filePath.Substring(pos);
			}
			return filePath;
		}
	}

	public class StringUtils
	{
		public static string TrimVersion(string s)
		{
			int i=s.LastIndexOf('.');
			if(i>0) s=s.Substring(0,i);
			return s;
		}
		public static void RemoveHotKey(ref string val)
		{
			int pos = val.IndexOf('&');
			if (pos >= 0) val = val.Remove(pos, 1);
		}
		public static void RemoveLastChar(ref string s)
		{
			if (s.Length > 0) s = s.Substring(0, s.Length - 1);
		}
		public static void RemoveFirstChar(ref string s)
		{
			if (s.Length > 0) s = s.Substring(1);
		}
		public static string NotNullString(string s) { return s == null ? "" : s; }
	}

	public class DateTimeUtils
	{
		public static DateTime Parse(string str)
		{
			string [] ss=str.Split("/ :.".ToCharArray());
			if(ss.Length!=7)
			{
				throw new Exception("Wrong DateTime format :"+str);
			}
			int[] ii=new int[7];
			for(int i=0;i<ii.Length;i++) ii[i]=int.Parse(ss[i]);
			return new DateTime(ii[0],ii[1],ii[2],ii[3],ii[4],ii[5],ii[6]);
		}
		public static string ToString(DateTime dt)
		{
			return string.Format("{0}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}.{6:000}",dt.Year,dt.Month,dt.Day,dt.Hour,dt.Minute,dt.Second,dt.Millisecond);
		}
		public static string Now
		{
			get{return ToString(DateTime.Now);}
		}
	}

	public class GeomethodException: Exception
	{
		public GeomethodException(string message): base(message){}
	}
}
