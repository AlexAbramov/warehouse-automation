using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Terminal.Forms
{
	/// <summary>
	/// Summary description for LoginForm.
	/// </summary>
	public class LoginForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblLogin;
		private System.Windows.Forms.TextBox tbLogin;
		private System.Windows.Forms.TextBox tbPassword;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblPassword;
	
		public LoginForm()
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
			this.lblLogin = new System.Windows.Forms.Label();
			this.lblPassword = new System.Windows.Forms.Label();
			this.tbLogin = new System.Windows.Forms.TextBox();
			this.tbPassword = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			// 
			// lblLogin
			// 
			this.lblLogin.Location = new System.Drawing.Point(8, 16);
			this.lblLogin.Text = "Login:";
			// 
			// lblPassword
			// 
			this.lblPassword.Location = new System.Drawing.Point(8, 48);
			this.lblPassword.Text = "Password:";
			// 
			// tbLogin
			// 
			this.tbLogin.Location = new System.Drawing.Point(120, 16);
			this.tbLogin.Text = "";
			this.tbLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbLogin_KeyDown);
			this.tbLogin.TextChanged += new System.EventHandler(this.tbLogin_TextChanged);
			// 
			// tbPassword
			// 
			this.tbPassword.AcceptsReturn = true;
			this.tbPassword.Location = new System.Drawing.Point(120, 48);
			this.tbPassword.PasswordChar = '*';
			this.tbPassword.Text = "";
			this.tbPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbPassword_KeyDown);
			this.tbPassword.TextChanged += new System.EventHandler(this.tbPassword_TextChanged);
			// 
			// btnOk
			// 
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(32, 88);
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(136, 88);
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// LoginForm
			// 
			this.ClientSize = new System.Drawing.Size(238, 120);
			this.Controls.Add(this.tbLogin);
			this.Controls.Add(this.tbPassword);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.lblLogin);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "Authorization";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LoginForm_KeyDown);
			this.Load += new System.EventHandler(this.LoginForm_Load);

		}
		#endregion

		private void tbLogin_TextChanged(object sender, System.EventArgs e)
		{
		  UpdateControls();
		}
		void Login()
		{
			try
			{
				if(App.Instance.Login(tbLogin.Text,tbPassword.Text))
				{
					Close();
				}
				else Utils.MessageBoxExcl("Wrong credentials.");
			}
			catch(Exception ex)
			{
				Utils.MessageBoxExcl(ex.Message);
				Close();
			}
		}
		private void tbPassword_TextChanged(object sender, System.EventArgs e)
		{
			UpdateControls();		
		}

		void UpdateControls()
		{
			btnOk.Enabled=tbLogin.Text.Trim().Length>0 && tbPassword.Text.Trim().Length>0;
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			Login();			
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void LoginForm_Load(object sender, System.EventArgs e)
		{
			tbLogin.Focus();
		}
		void OnKey(KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter:					
					Login();
					break;
				case Keys.Escape:
					Close();
					break;
			}
		}

		private void LoginForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}

		private void tbPassword_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}

		private void tbLogin_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}
	}
}
