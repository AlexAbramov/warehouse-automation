using System;
using System.Windows.Forms;

namespace Terminal
{
	public class Utils
	{
		public static void OnFormLoad(Form form)
		{
//			form.Height=294;//244; 294
//			if(App.Config!=null) form.Size=App.Config.ScreenSize;
			form.WindowState=FormWindowState.Maximized;
		}

		public static bool Confirm(string msg)
		{
			return MessageBox.Show(msg,App.ProductName,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.Yes;
		}

		public static void MessageBoxExcl(string msg)
		{
			MessageBox.Show(msg,App.ProductName,MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
		}

		public static string TrimVersion(string s)
		{
			int i=s.LastIndexOf('.');
			if(i>0) s=s.Substring(0,i);
			return s;
		}

		public static string RemoveZeros(string s)
		{
			int pos=s.IndexOf('.');
			while(pos>=0)
			{	
				if(s.EndsWith("0"))
				{
					s=s.Substring(0,s.Length-1);
					continue;
				}
				if(s.EndsWith("."))
				{
					s=s.Substring(0,s.Length-1);
				}
				break;
			}
			return s;
		}
	}
}
