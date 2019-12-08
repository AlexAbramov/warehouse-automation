using System;
using System.Data;
using System.Text;
using Terminal;

namespace Geomethod.Data
{
	public class SqlLog : ILog
	{
		#region Fields
		ConnectionFactory connFactory=null;
		#endregion

		#region Construction
		public SqlLog(ConnectionFactory connFactory) { this.connFactory = connFactory; }
		#endregion

		#region ILog Members
		public void Write(LogType logType, string subType, string msg)
		{
			if (connFactory != null)
			{
				using (GmConnection conn = connFactory.CreateConnection())
				{
					GmCommand cmd = conn.CreateCommand("insert into Log values (@Time,@TerminalId,@UserId,@LogType,@SubType,@Message)");
					cmd.AddDateTime("Time", DateTime.Now);
					cmd.AddInt("TerminalId", App.TerminalId);
					IDbDataParameter par=cmd.AddInt("UserId", App.UserId);
					if(App.UserId==0) par.Value=DBNull.Value;
					cmd.AddInt("LogType", (int)logType);
					cmd.AddString("SubType", subType, FieldLength.subType);
					cmd.AddString("Message", msg,FieldLength.message);
					cmd.ExecuteNonQuery();
				}
			}
		}

		public void Close()
		{
			connFactory = null;
		}

		#endregion
	}

}
