using System;
using System.Windows.Forms;
using Geomethod;

namespace Geomethod.Windows.Forms
{
	public class WaitCursor: IDisposable
	{
		Cursor initialCursor;
		IStatus status=null;

		void Init(IStatus status)
		{
			initialCursor=Cursor.Current;
			Cursor.Current=Cursors.WaitCursor;
			this.status=status;
		}

		public WaitCursor()
		{
			Init(null);
		}

		public WaitCursor(IStatus status,string message)
		{
			Init(status);
			status.Status=message;
		}

		public void Close()
		{
			Cursor.Current=initialCursor;
			if(status!=null) status.Status="";
		}

		#region IDisposable Members

		public void Dispose()
		{
			Close();
		}

		#endregion
	}

	public interface IStatus
	{
		string Status{get;set;}
	}
}
