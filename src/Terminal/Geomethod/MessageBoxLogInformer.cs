using System;
using System.Data;
using System.Windows.Forms;
using Geomethod;
using Terminal;

namespace Geomethod.Windows.Forms
{
	/// <summary>
	/// Summary description for MessageBoxLogInformer.
	/// </summary>
	public class MessageBoxLogInformer: ILogInformer
	{
		public MessageBoxLogInformer()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		#region ILogInformer Members

		public void Show(Geomethod.LogType logType, string subType, string msg)
		{
			MessageBox.Show(msg,App.ProductName);
		}

		#endregion
	}

	public class DataGridUtils
	{
		public static DataRow CurrentDataRow(DataGrid dg)
		{
			DataTable dt=dg.DataSource as DataTable;
			CurrencyManager cm=dg.BindingContext[dt] as CurrencyManager;
			if(cm!=null && cm.Count>0)
			{
				DataRowView drv=cm.Current as DataRowView;
				if(drv!=null)
				{
					return drv.Row;
				}
			}
			return null;
		}
		public static void SelectCurRow(DataGrid dataGrid)
		{
			int i=dataGrid.CurrentRowIndex;
			dataGrid.Select(i);
		}
		public static void SelectRow(DataGrid dg,DataRow dr)
		{
			DataTable dt=dg.DataSource as DataTable;
			for(int i=0;i<dt.Rows.Count;i++)
			{
				if(dt.Rows[i]==dr)
				{
					dg.CurrentRowIndex=i;
					return;
				}
			}
		}
	}
}
