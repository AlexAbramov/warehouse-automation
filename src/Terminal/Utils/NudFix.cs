using System;
using System.Windows.Forms;

namespace Terminal
{
	/// <summary>
	/// Summary description for NudFix.
	/// </summary>
	public class NudFix
	{
		NumericUpDown nud;
		Button btnBackspace;
		Button btnClear;
		
		public NudFix(NumericUpDown nud,Button btnBackspace,Button btnClear)
		{
			this.nud=nud;
			this.btnBackspace=btnBackspace;
			this.btnClear=btnClear;
			nud.KeyPress+=new KeyPressEventHandler(nud_KeyPress);
			btnBackspace.Click+=new EventHandler(btnBackspace_Click);
			btnClear.Click+=new EventHandler(btnClear_Click);
		}

		private void nud_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(nud.Text=="0")
			{
				nud.Text=e.KeyChar.ToString();
				e.Handled=true;
			}
		}

		private void btnBackspace_Click(object sender, EventArgs e)
		{
			string s=nud.Text;
			nud.Text=s.Length>1?s.Substring(0,s.Length-1):"";
			nud.Focus();
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			nud.Value=0;
			nud.Focus();
		}
	}
}
