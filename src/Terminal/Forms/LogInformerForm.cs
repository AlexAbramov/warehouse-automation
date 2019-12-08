using System;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using Geomethod;

namespace Terminal.Forms
{
	/// <summary>
	/// Summary description for LogInformerForm.
	/// </summary>
	public class LogInformerForm : System.Windows.Forms.Form
	{
		#region Static
		static Hashtable ignored=new Hashtable();
		static string GetKey(LogType logType,string subType){return logType.ToString()+subType;}
		public static bool IsIgnored(LogType logType,string subType)
		{
			return ignored.ContainsKey(GetKey(logType,subType));
		}
		static void Ignore(LogType logType,string subType)
		{
			string key=GetKey(logType,subType);
			if(!ignored.ContainsKey(key))
			{
				ignored.Add(key,null);
			}
		}
		#endregion

		LogType logType;
		string subType;

		private System.Windows.Forms.Button btnIgnore;
		private System.Windows.Forms.TextBox tbMessage;
		private System.Windows.Forms.Button btnOk;
	
/*		LogInformerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}*/


		public LogInformerForm(LogType logType, string subType, string msg)
		{
			this.logType=logType;
			this.subType=subType;
			InitializeComponent();
			tbMessage.Text=msg;
			switch(logType)
			{
				case LogType.Info:
					SetTitle("Info");
					break;
				case LogType.Warning:
					SetTitle("Warning");
					break;
				case LogType.Error:
					SetTitle("Error");
					break;
				case LogType.Exception:
					SetTitle("Exception");
					break;
			}
		}

		void SetTitle(string s)
		{
			base.Text=App.ProductName+' '+s;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tbMessage = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnIgnore = new System.Windows.Forms.Button();
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(4, 244);
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnIgnore
			// 
			this.btnIgnore.Location = new System.Drawing.Point(140, 244);
			this.btnIgnore.Size = new System.Drawing.Size(92, 20);
			this.btnIgnore.Text = "Ignore";
			this.btnIgnore.Visible = false;
			this.btnIgnore.Click += new System.EventHandler(this.btnIgnore_Click);
			// 
			// tbMessage
			// 
			this.tbMessage.Location = new System.Drawing.Point(2, 0);
			this.tbMessage.Multiline = true;
			this.tbMessage.ReadOnly = true;
			this.tbMessage.Size = new System.Drawing.Size(234, 240);
			this.tbMessage.Text = "";
			// 
			// LogInformerForm
			// 
			this.ClientSize = new System.Drawing.Size(238, 262);
			this.Controls.Add(this.tbMessage);
			this.Controls.Add(this.btnIgnore);
			this.Controls.Add(this.btnOk);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Load += new System.EventHandler(this.LogInformerForm_Load);

		}
		#endregion

		private void LogInformerForm_Load(object sender, System.EventArgs e)
		{
			Utils.OnFormLoad(this);
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnIgnore_Click(object sender, System.EventArgs e)
		{
			Ignore(logType,subType);
			Close();
		}
	}

	public class FormLogInformer : ILogInformer
	{
		public void Show(Geomethod.LogType logType, string subType, string msg)
		{
			if (!LogInformerForm.IsIgnored(logType, subType))
			{
				LogInformerForm form = new LogInformerForm(logType, subType, msg);
				form.ShowDialog();
			}
		}
	}
}
