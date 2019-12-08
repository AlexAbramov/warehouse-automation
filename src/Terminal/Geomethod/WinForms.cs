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
			DataTable dt=GetDataTable(dg);
			if(dt!=null)
			{
				CurrencyManager cm=dg.BindingContext[dt] as CurrencyManager;
				if(cm!=null && cm.Count>0)
				{
					DataRowView drv=cm.Current as DataRowView;
					if(drv!=null)
					{
						return drv.Row;
					}
				}
			}
			return null;
		}
		public static int GetNonDeletedRowsCount(DataTable dt)
		{
			int count=0;
			foreach(DataRow dr in dt.Rows)
			{
				if(dr.RowState!=DataRowState.Deleted)
				  count++;
			}
			return count;
		}

		public static void SelectCurRow(DataGrid dataGrid)
		{
			if(dataGrid.Visible)
			{
				DataTable dt=GetDataTable(dataGrid);
				if(dt!=null && GetNonDeletedRowsCount(dt)>0)
				{
					int i=dataGrid.CurrentRowIndex;
					if(i>=0) dataGrid.Select(i);
				}
				dataGrid.Focus();
			}
		}
		public static DataTable GetDataTable(DataGrid dataGrid)
		{
			if(dataGrid.DataSource is DataTable) return dataGrid.DataSource as DataTable;
			if(dataGrid.DataSource is DataView) return (dataGrid.DataSource as DataView).Table;
			return null;
		}
		public static void SelectRow(DataGrid dg,DataRow dr)
		{
			DataTable dt=GetDataTable(dg);

/*			CurrencyManager cm=dg.BindingContext[dt] as CurrencyManager;
			if(cm!=null && cm.Count>0)
			{
				cm.List
				DataRowView drv=cm.Current as DataRowView;
				if(drv!=null)
				{
					return drv.Row;
				}
			}*/

			int index=0;
			foreach(DataRowView drv in dt.DefaultView)
			{
				if(drv.Row==dr)
				{
					dg.CurrentRowIndex=index;
					break;
				}
				index++;
			}
			
/*			DataTable dt=GetDataTable(dg);
			for(int i=0;i<dt.Rows.Count;i++)
			{
				if(dt.Rows[i]==dr)
				{
					dg.CurrentRowIndex=i;
					return;
				}
			}*/
		}
		public static void SetNullText(DataGrid dg){SetNullText(dg,"");}
		public static void SetNullText(DataGrid dg,string nullText)
		{
			if(dg.TableStyles.Count>0)
			{
				DataGridTableStyle ts=dg.TableStyles[0];
				foreach(DataGridColumnStyle cs in ts.GridColumnStyles)
				{
					cs.NullText=nullText;
				}
			}
		}
		public static void ResizeColumn(DataGridColumnStyle resizableCol, DataGrid dg, int targetWidth)
		{
			if(dg.TableStyles.Count>0)
			{
				int width=0;
				DataGridTableStyle ts=dg.TableStyles[0];
				foreach(DataGridColumnStyle cs in ts.GridColumnStyles)
				{
					if(cs.MappingName!=null && cs.MappingName.Length>0) width+=cs.Width;
				}
				int colWidth=resizableCol.Width+targetWidth-width;
				if(colWidth<Constants.minColWidth) colWidth=Constants.minColWidth;
				resizableCol.Width=colWidth;
			}
		}
	}
}
