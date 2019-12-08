using System;
using System.Text;
using System.IO;
using Terminal;

namespace Geomethod
{
	public class FileLog: ILog
	{
		#region Fields
//		const string newline="\r\n";
		StreamWriter sw=null;
		#endregion

		#region Construction
		public FileLog(){Init(null);}
		public FileLog(string filePath){Init(filePath);}
		public void Init(string filePath)
		{
			sw=new StreamWriter(filePath,true,Encoding.UTF8);
		}
		#endregion

		#region ILog Members

		public void Write(LogType logType, string subType, string msg)
		{
			if(sw!=null)
			{
				sw.WriteLine(string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",DateTimeUtils.Now,App.TerminalId,App.UserId,logType,subType,msg));
				sw.Flush();
			}
		}

		public void Close()
		{
			if(sw!=null)
			{
				sw.Close();
				sw=null;
			}
		}

		#endregion
	}
}
