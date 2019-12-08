using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public class GmConnection: IDisposable
	{
		#region Fields
		ConnectionFactory connectionFactory;
		IDbConnection dbConnection;
		#endregion

		#region Properties
		public ConnectionFactory ConnectionFactory { get { return connectionFactory; } }
		public string ConnectionString { get { return dbConnection.ConnectionString; } }
		public IDbConnection DbConnection { get { return dbConnection; } }
		#endregion		

		#region Construction
		public GmConnection(ConnectionFactory connectionFactory)
		{
			this.connectionFactory = connectionFactory;
			dbConnection = connectionFactory.DbProviderFactory.CreateConnection();
			dbConnection.ConnectionString = connectionFactory.ConnectionString;
		}

		public void Close() { if (dbConnection != null) dbConnection.Close(); }
		#endregion

		#region Commands
		public GmCommand CreateCommand(){return new GmCommand(this);}
		public GmCommand CreateCommand(string cmdText)
		{
			GmCommand cmd=null;
/*			if(CommandCache.enabled)
			{ 
				cmd=CommandCache.Get(cmdText);
				if(cmd!=null)
				{
					cmd.Connection=this;
					return cmd;
				}
			}*/
			cmd=new GmCommand(this, cmdText);
//			if(CommandCache.enabled) CommandCache.Save(cmdText,cmd);
			return cmd;
		}
		#endregion

		#region Execute
		public int ExecuteNonQuery(string cmdText){return CreateCommand(cmdText).ExecuteNonQuery();}
		public IDataReader ExecuteReader(string cmdText){return CreateCommand(cmdText).ExecuteReader();}
		public IDataReader ExecuteReader(string cmdText,CommandBehavior behavior){return CreateCommand(cmdText).ExecuteReader(behavior);}
		public object ExecuteScalar(string cmdText){return CreateCommand(cmdText).ExecuteScalar();}
		public DbDataAdapter CreateDataAdapter() 
		{
			return connectionFactory.DbProviderFactory.CreateDataAdapter();
		}
		public DbDataAdapter CreateDataAdapter(string cmdText) 
		{
			GmCommand cmd=CreateCommand(cmdText);
			return CreateDataAdapter(cmd);
		}
		public DbDataAdapter CreateDataAdapter(GmCommand cmd) 
		{
			DbDataAdapter da = CreateDataAdapter();
			(da as IDbDataAdapter).SelectCommand = cmd.DbCommand;
			return da;
		}
		public DbDataAdapter Fill(string cmdText,DataTable dt)
		{
			DbDataAdapter da = CreateDataAdapter(cmdText);
			da.Fill(dt);
			return da;
		}
		#endregion

		#region Utils
		public int GetId(int tableId)
		{
			return IdGenerator.Instance.Get(this,tableId);
		}
		#endregion

		#region IDisposable Members

		void IDisposable.Dispose()
		{
			Close();
		}

		#endregion

	}
}
