using System;
using System.Text;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace Terminal.Forms
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button bClose;
	
		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
			this.lblInfo = new System.Windows.Forms.Label();
			this.bClose = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			// 
			// lblInfo
			// 
			this.lblInfo.Location = new System.Drawing.Point(40, 4);
			this.lblInfo.Size = new System.Drawing.Size(192, 178);
			this.lblInfo.Text = "Warehouse Automation.";
			// 
			// bClose
			// 
			this.bClose.Location = new System.Drawing.Point(160, 184);
			this.bClose.Text = "Close";
			this.bClose.Click += new System.EventHandler(this.bClose_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(4, 4);
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			// 
			// AboutForm
			// 
			this.ClientSize = new System.Drawing.Size(234, 206);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.bClose);
			this.Location = new System.Drawing.Point(5, 30);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "About program";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AboutForm_KeyDown);
			this.Load += new System.EventHandler(this.AboutForm_Load);

		}
		#endregion

		private void bClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void AboutForm_Load(object sender, System.EventArgs e)
		{
			string productVersion=/*StringUtils.TrimVersion*/(Assembly.GetExecutingAssembly().GetName().Version.ToString());
			string clrVersion=/*StringUtils.TrimVersion*/(Environment.Version.ToString());
			StringBuilder sb=new StringBuilder(1024);
			sb.Append("Warehouse Automation.");
			sb.Append("\r\n(C) 2006 ");
			sb.Append("\r\n\r\nProduct version: "+productVersion);
			sb.Append("\r\nCLR version: "+clrVersion);
			sb.Append("\r\n\r\nTerminal ID: "+App.TerminalId);
			sb.Append("\r\nUser ID: "+App.UserId);
			lblInfo.Text=sb.ToString();
		}
		void OnKey(KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter:
					Close();
					break;
				case Keys.Escape:
					Close();
					break;
			}
		}
		private void AboutForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}
	}
}
