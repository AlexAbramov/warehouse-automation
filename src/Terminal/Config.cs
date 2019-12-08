using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.IO;

namespace Terminal
{
	public class Config
	{
		public const string cCmd="c";
		public const string configFileName="Terminal.config.txt";
		static readonly Encoding encoding=Encoding.Default;

		#region Fields
		int terminalId;
		string connectionString;
		int syncTime;
		int pollTime;
		int commandTimeout;
		Size screenSize;
		int debugMode;
		bool openCountFormAfterScan;
		int cs;
		#endregion

		#region Properties
		public int TerminalId { get { return terminalId; } }
		public string ConnectionString{get{return connectionString;}}
		public int SyncTime {get {return syncTime;}}
		public int PollTime { get { return pollTime;} }
		public int CommandTimeout {get {return commandTimeout;}}
		public Size ScreenSize { get { return screenSize; } }
		public int DebugMode { get { return debugMode;} }
		public bool OpenCountFormAfterScan { get { return openCountFormAfterScan;} }
		public int CS{get{return cs;}}
		#endregion

		#region Construction
		public Config(string filePath)// log ignored
		{
			if(!File.Exists(filePath))
			{
				throw new TerminalException("Configuration file not found: "+filePath);
			}
			try
			{
				using(StreamReader sr=new StreamReader(filePath,encoding))
				{
					terminalId=int.Parse(ReadLine(sr));
					connectionString=ReadLine(sr);
					cs=int.Parse(ReadLine(sr));
					syncTime=int.Parse(ReadLine(sr));
					pollTime=int.Parse(ReadLine(sr));
					commandTimeout=int.Parse(ReadLine(sr));
					string[] ss=ReadLine(sr).Split(';');
					screenSize=new Size(int.Parse(ss[0]),int.Parse(ss[1]));
					debugMode=int.Parse(ReadLine(sr));
					openCountFormAfterScan=int.Parse(ReadLine(sr))>0;
				}
				if(cs==1)
				{
					ArrayList lines=new ArrayList(16);
					using(StreamReader sr=new StreamReader(filePath,encoding))
					{
						string s;
						while((s=sr.ReadLine())!=null)
						{
							lines.Add(s);
						}
					}
					lines[1]=Enc(connectionString)+"\t"+ReadComment(lines[1].ToString());
					lines[2]="2\t"+ReadComment(lines[2].ToString());
					using(StreamWriter sw=new StreamWriter(filePath,false,encoding))
					{
						foreach(string line in lines) sw.WriteLine(line);
					}
				}
				else if(cs==2)
			  {
					this.connectionString=Dec(connectionString);
				}
			}
			catch(Exception ex)
			{
				throw new TerminalException("Configuration file unexpected format: "+filePath+"\r\n"+ ex.ToString());
			}
		}

		string ReadComment(string s)
		{
			int pos=s.LastIndexOf("--");
			return pos<0 ? "" : s.Substring(pos);
		}

		static string Enc(string s)
		{
			StringBuilder sb=new StringBuilder(128);
			Random rnd=new Random(128);
			foreach(char c in s)
			{
				byte r=(byte)rnd.Next(128);
				byte b=(byte)(c+r);
				sb.Append(b.ToString("X"));
			}
			return sb.ToString();
		}

		static string Dec(string s)
		{
			StringBuilder sb=new StringBuilder(128);
			Random rnd=new Random(128);
			for(int i=0;i<s.Length;i+=2)
			{
				string str=s.Substring(i,2);
				byte b=byte.Parse(str,System.Globalization.NumberStyles.HexNumber);
				byte r=(byte)rnd.Next(128);
				char c=(char)(b-r);
				sb.Append(c);
			}
			return sb.ToString();
		}

		string ReadLine(StreamReader sr)
		{
			string s=sr.ReadLine();
			int pos=s.LastIndexOf("--");
			return pos<0 ? s : s.Substring(0,pos).Trim();
		}

		#endregion
	}
}
