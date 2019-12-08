using System;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Geomethod;
using Geomethod.Data;
using Geomethod.Windows.Forms;
using Terminal.Model;
using Terminal.Forms;

namespace Terminal
{
	/// <summary>
	/// Summary description for Program.
	/// </summary>
	public class Program
	{
		public Program()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		static void Main(string[] args) 
		{
			try
			{		
				Application.Run(new MainForm());
			}
			catch(Exception ex)
			{
				Log.Exception(ex);
			}
			finally
			{
				Log.Close();
			}
		}
	}
}
