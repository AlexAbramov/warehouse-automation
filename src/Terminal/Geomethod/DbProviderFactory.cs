using System;
using System.Data;
using System.Data.SqlClient;//.SqlServerCe;
using System.Data.Common;

namespace Geomethod.Data
{
	/// <summary>
	/// Summary description for DbProviderFactory.
	/// </summary>
	public interface DbProviderFactory
	{
		IDbConnection CreateConnection();
		DbDataAdapter CreateDataAdapter();
	}

/*	public class SqlServerCEProviderFactory: DbProviderFactory
	{
		#region DbProviderFactory Members

		public IDbConnection CreateConnection()
		{
			return new SqlCeConnection();
		}

		public DbDataAdapter CreateDataAdapter()
		{
			return new SqlCeDataAdapter();
		}

		#endregion

		public SqlServerCEProviderFactory()// log ignored
		{
		}

	}*/


	public class SqlServerProviderFactory: DbProviderFactory
	{
		#region DbProviderFactory Members

		public IDbConnection CreateConnection()
		{
			return new SqlConnection();
		}

		public DbDataAdapter CreateDataAdapter()
		{
			return new SqlDataAdapter();
		}

		#endregion

		public SqlServerProviderFactory()// log ignored
		{
		}

	}

}
