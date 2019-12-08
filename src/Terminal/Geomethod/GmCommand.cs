using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public class GmCommand
	{
		#region Fields
		GmConnection conn;
		IDbCommand cmd;
		#endregion

		#region Properties
		public GmConnection Connection { get { return conn; } set { conn = value;} }
		public IDbCommand DbCommand{get{return cmd;}}
		public string CommandText{get{return cmd.CommandText;}set{SetCommandText(value);}}
		#endregion

		#region Construction
		public GmCommand(GmConnection conn)
		{
			this.conn=conn;
			this.cmd=conn.DbConnection.CreateCommand();
			this.DbCommand.CommandTimeout=conn.ConnectionFactory.CommandTimeout;
		}
		public GmCommand(GmConnection conn, string cmdText): this(conn)
		{
			SetCommandText(cmdText);
		}
		#endregion

		#region Utils
		void SetCommandText(string cmdText)
		{
			cmdText=cmdText.Trim();
//			if (SqlQueries.Loaded && cmdText.StartsWith("_")) cmdText = SqlQueries.Get(cmdText);
			if(IsStoredProcedure(cmdText))
			{
				cmd.CommandType=CommandType.StoredProcedure;
			}
//			else conn.PreProcessCommandText(ref cmdText);
			cmd.CommandText=cmdText;
		}
		static bool IsStoredProcedure(string s)
		{
			bool breakOpened=false;
			bool symbolFound=false;
			for(int i=0;i<s.Length;i++)
			{
				switch(s[i])
				{
					case '[':
						if(i>0) return false;
						breakOpened=true;
						break;
					case ']':
						if(!breakOpened) return false;
						breakOpened=false;
						break;
					case ' ':
						if(!breakOpened) return false;
						break;
					case '(':
					case ')':
						return false;
					default:
						symbolFound=true;
						break;
				}
			}
			return symbolFound && !breakOpened;
		}
		void CheckConn()
		{
			if (conn.DbConnection.State == ConnectionState.Closed) conn.DbConnection.Open();
		}
		#endregion

		#region Execute
		public int ExecuteNonQuery() { CheckConn(); return cmd.ExecuteNonQuery(); }
		public IDataReader ExecuteReader() { CheckConn(); return cmd.ExecuteReader(); }
		public IDataReader ExecuteReader(CommandBehavior behavior) { CheckConn(); return cmd.ExecuteReader(behavior); }
		public object ExecuteScalar() { CheckConn(); return cmd.ExecuteScalar(); }
		#endregion

		#region Parameters
		public IDbDataParameter AddBool(string parName)
		{
			IDbDataParameter par=cmd.CreateParameter();
			par.ParameterName='@'+parName;
			par.DbType=DbType.Boolean;
			cmd.Parameters.Add(par);
			return par;
		}
		public IDbDataParameter AddBool(string parName,bool val)
		{
			IDbDataParameter par=cmd.CreateParameter();
			par.ParameterName='@'+parName;
			par.DbType=DbType.Boolean;
			par.Value=val;
			cmd.Parameters.Add(par);
			return par;
		}
		public IDbDataParameter AddInt(string parName)
		{
			IDbDataParameter par=cmd.CreateParameter();
			par.ParameterName='@'+parName;
			par.DbType=DbType.Int32;
			cmd.Parameters.Add(par);
			return par;
		}
		public IDbDataParameter AddInt(string parName,int val)
		{
			IDbDataParameter par=AddInt(parName);
			par.Value=val;
			return par;
		}
		public IDbDataParameter AddDecimal(string parName,decimal val)
		{
			IDbDataParameter par=AddDecimal(parName);
			par.Value=val;
			return par;
		}
		public IDbDataParameter AddDecimal(string parName)
		{
			IDbDataParameter par=cmd.CreateParameter();
			par.ParameterName='@'+parName;
			par.DbType=DbType.Decimal;
			cmd.Parameters.Add(par);
			return par;
		}
		public IDbDataParameter AddNull(string parName, DbType dbType)
		{
			IDbDataParameter par = cmd.CreateParameter();
			par.ParameterName = '@' + parName;
			par.DbType = dbType;
			par.Value = DBNull.Value;
			cmd.Parameters.Add(par);
			return par;
		}
		public IDbDataParameter AddString(string parName)
		{
			IDbDataParameter par=cmd.CreateParameter();
			par.ParameterName='@'+parName;
			par.DbType=DbType.String;
			cmd.Parameters.Add(par);
			return par;
		}
		public IDbDataParameter AddString(string parName, string val)
		{
			IDbDataParameter par=AddString(parName);
			par.Value=val;
			return par;
		}
		public IDbDataParameter AddString(string parName, string val, int size)
		{
			IDbDataParameter par=AddString(parName,val);
			par.Size=size;
			return par;
		}
		public IDbDataParameter AddDateTime(string parName,DateTime val)
		{
			IDbDataParameter par=cmd.CreateParameter();
			par.ParameterName='@'+parName;
			par.DbType=DbType.DateTime;
			par.Value=val;
			cmd.Parameters.Add(par);
			return par;
		}
		public IDbDataParameter AddGuid(string parName,Guid val)
		{
			IDbDataParameter par=cmd.CreateParameter();
			par.ParameterName='@'+parName;
			par.DbType=DbType.Guid;
			par.Value=val;
			cmd.Parameters.Add(par);
			return par;
		}
		public IDbDataParameter AddBinary(string parName)
		{
			IDbDataParameter par=cmd.CreateParameter();
			par.ParameterName='@'+parName;
			par.DbType=DbType.Binary;
			cmd.Parameters.Add(par);
			return par;
		}
		public IDbDataParameter AddBinary(string parName,byte[] bytes)
		{
			IDbDataParameter par=cmd.CreateParameter();
			par.ParameterName='@'+parName;
			par.DbType=DbType.Binary;
			cmd.Parameters.Add(par);
			par.Value=bytes;
			par.Size=bytes.Length;
			return par;
		}
		#endregion
	}
}
