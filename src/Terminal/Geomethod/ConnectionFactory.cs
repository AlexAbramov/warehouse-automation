using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Geomethod.Data
{
	public class ConnectionFactory
	{
		DbProviderFactory providerFactory;
		string connStr;
		int commandTimeout=30;

		public DbProviderFactory DbProviderFactory { get { return providerFactory; } }
		public string ConnectionString { get { return connStr; } }
		public int CommandTimeout {get {return commandTimeout; }set{commandTimeout=value;} }

			public ConnectionFactory(DbProviderFactory providerFactory, string connStr)
		{
			this.providerFactory = providerFactory;
			this.connStr=connStr;
		}

		public GmConnection CreateConnection()
		{
			return new GmConnection(this);
		}
	}
}