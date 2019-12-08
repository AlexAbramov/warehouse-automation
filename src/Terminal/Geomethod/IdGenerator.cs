using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Text;

namespace Geomethod.Data
{
	public class IdGenerator
	{
		#region Static
		static IdGenerator instance;
		public static IdGenerator Instance { get { if (instance == null) throw new GeomethodDataException("IdGenerator not initialized."); return instance; } }
		public static void Init(IdGenerator instance) { IdGenerator.instance = instance; }
		#endregion

		string tableName;
		int minRecordId;
		int maxRecordId;
		int poolSize;
		Hashtable ids = new Hashtable(1 << 8);

		#region Construction
		public IdGenerator(string tableName, int minRecordId, int maxRecordId,int poolSize)
		{
			this.tableName = tableName;
			this.minRecordId = minRecordId;
			this.maxRecordId = maxRecordId;
			this.poolSize = poolSize;
		}
		public void Clear()
		{
			ids.Clear();
		}
		#endregion

		#region Serialization
		public void Load(GmConnection conn)
		{
			string query = string.Format("select * from [{0}]", tableName);
			using (IDataReader dr = conn.ExecuteReader(query))
			{
				while (dr.Read())
				{
					ids.Add(dr.GetInt32(0), dr.GetInt32(1));
				}
			}
		}
/*		public void Save(GmConnection conn)
		{
			int count = ids.Count;
			if (count > 0)
			{
				int[] keys = new int[count];
				int[] vals = new int[count];
				ids.Keys.CopyTo(keys, 0);
				ids.Values.CopyTo(vals, 0);
				for (int i = 0; i < count; i++)
				{
					DbCommand cmd = context.Conn.CreateCommandById("insertIntoGisIds");
					cmd.AddInt("TableId", keys[i]);
					cmd.AddInt("RecordId", vals[i]);
					cmd.ExecuteNonQuery();
				}
			}
		}*/
		#endregion

		#region Generation
		public int Get(GmConnection conn, int tableId)
		{
			int recordId=minRecordId;
			bool hasKey=ids.ContainsKey(tableId);
			if(hasKey) recordId=(int)ids[tableId];
			if (!hasKey || recordId % poolSize == 0)
			{
				recordId = GetPool(conn, tableId);
			}
			recordId++;
			ids[tableId] = recordId;
			return recordId;
		}
		int GetPool(GmConnection conn, int tableId)
		{
			int recordId;
			conn.DbConnection.Open();
			IDbTransaction tr = conn.DbConnection.BeginTransaction(IsolationLevel.Serializable);
			string query = string.Format("select RecordId from [{0}] where TableId=@TableId", tableName);
			GmCommand cmd = conn.CreateCommand(query);
			cmd.AddInt("TableId", tableId);
			cmd.DbCommand.Transaction = tr;
			try
			{
				object obj = cmd.ExecuteScalar();
				if (obj is int)
				{
					recordId = (int)obj;
					cmd.CommandText = string.Format("update [{0}] set RecordId=@RecordId where TableId=@TableId",tableName);
				}
				else
				{
					recordId = minRecordId;
					cmd.CommandText = string.Format("insert into [{0}] values (@TableId, @RecordId)", tableName);
				}
				cmd.AddInt("RecordId", recordId + poolSize);
				cmd.ExecuteNonQuery();
				tr.Commit();
			}
			catch (Exception ex)
			{
				tr.Rollback();
				throw ex;
			}
			return recordId;
		}
		#endregion
	}
}
