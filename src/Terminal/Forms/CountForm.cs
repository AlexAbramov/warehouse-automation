using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Terminal.Forms
{
	/// <summary>
	/// Summary description for CountForm.
	/// </summary>
	public class CountForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnBackspace;
		internal System.Windows.Forms.CheckBox chkHandled;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox tbCount;
		bool hasDecimals;
		decimal count;

		public decimal Value// sync2
		{
			get
			{
				return count;
			}
			set
			{
				count=value;
				if(hasDecimals)
				{
					tbCount.Text=Utils.RemoveZeros(count.ToString("F3"));
				}
				else
				{
					int i=(int)count;
					tbCount.Text=i.ToString();
				}

			}
		}

		public CountForm(bool hasDecimals)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.hasDecimals=hasDecimals;
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
			this.lblCount = new System.Windows.Forms.Label();
			this.btnBackspace = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.chkHandled = new System.Windows.Forms.CheckBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tbCount = new System.Windows.Forms.TextBox();
			// 
			// lblCount
			// 
			this.lblCount.Location = new System.Drawing.Point(8, 16);
			this.lblCount.Size = new System.Drawing.Size(72, 20);
			this.lblCount.Text = "Count:";
			// 
			// btnBackspace
			// 
			this.btnBackspace.Location = new System.Drawing.Point(192, 80);
			this.btnBackspace.Size = new System.Drawing.Size(20, 20);
			this.btnBackspace.Text = "<-";
			this.btnBackspace.Visible = false;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(216, 80);
			this.btnClear.Size = new System.Drawing.Size(20, 20);
			this.btnClear.Text = "x";
			this.btnClear.Visible = false;
			// 
			// chkHandled
			// 
			this.chkHandled.Location = new System.Drawing.Point(216, 40);
			this.chkHandled.Size = new System.Drawing.Size(216, 20);
			this.chkHandled.Text = "Completed";
			this.chkHandled.Visible = false;
			this.chkHandled.CheckStateChanged += new System.EventHandler(this.chkHandled_CheckStateChanged);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(32, 56);
			this.btnOk.Text = "OK";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(136, 56);
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// tbCount
			// 
			this.tbCount.Location = new System.Drawing.Point(88, 16);
			this.tbCount.MaxLength = 12;
			this.tbCount.Size = new System.Drawing.Size(104, 20);
			this.tbCount.Text = "";
			// 
			// CountForm
			// 
			this.ClientSize = new System.Drawing.Size(238, 88);
			this.Controls.Add(this.tbCount);
			this.Controls.Add(this.chkHandled);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnBackspace);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lblCount);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "Count";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CountForm_KeyDown);
			this.Load += new System.EventHandler(this.CountForm_Load);

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			OnOk();
		}

		void OnOk()
		{
			try // sync1
			{
				if(hasDecimals)
				{
					string s=tbCount.Text;
					s=s.Replace(",",".");
					count=decimal.Parse(s);
					count=Math.Round(count,3);
				}
				else
				{
					count=int.Parse(tbCount.Text);
				}
			}
			catch//(Exception ex)
			{				
				Utils.MessageBoxExcl("Wrong number format.");
				return;
			}
			base.DialogResult=DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			OnCancel();
		}

		void OnCancel()
		{
			base.DialogResult=DialogResult.Cancel;
			Close();
		}

		private void CountForm_Load(object sender, System.EventArgs e)
		{
			tbCount.Focus();
//			nudCount.KeyDown+=new KeyEventHandler(nudCount_KeyDown);
//			new NudFix(nudCount,btnBackspace,btnClear);
//!!!!			nudCount.
//			this.lblDecimal.Visible=hasDecimals;
//			this.tbDecimal.Visible=hasDecimals;
		}

		private void CountForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}	

		private void nudCount_KeyDown(object sender, KeyEventArgs e)
		{
			OnKey(e);
		}

		void OnKey(KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter:
					OnOk();
					break;
				case Keys.Escape:
					OnCancel();
					break;
			}
		}

		private void chkHandled_CheckStateChanged(object sender, System.EventArgs e)
		{
//			nudCount.Enabled=chkHandled.Checked;
		}
	}
}
