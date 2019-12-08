using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Text;

namespace Geomethod.Data
{
	public class GeomethodDataException : GeomethodException
	{
		public GeomethodDataException(string message) : base(message) { }
	}

	public class DataTableUtils
	{
		public static DataTable Clone(DataTable dt)
		{
			DataTable dt2=(DataTable)dt.Clone();
			CopyRows(dt,dt2);
			return dt2;
		}
		public static void CopyRows(DataTable dt, DataTable dt2)
		{
			dt2.Rows.Clear();
			foreach(DataRow dr in dt.Rows)
			{
				dt2.Rows.Add((object[])dr.ItemArray.Clone());
			}
		}		
	}

/*	public class DataUtils
	{
		public static void RemoveItems(DataTable dataTable, int[] removedItems)
		{
			ArrayList removedRows = new ArrayList();
			foreach (DataRow row in dataTable.Rows)
			{
				int id = (int)row[0];
				if (removedItems.Contains(id)) removedRows.Add(row);
			}
			foreach (DataRow row in removedRows)
			{
				dataTable.Rows.Remove(row);
			}
		}

	}*/
}
