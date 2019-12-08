using System;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Geomethod.Windows.Forms;
using Geomethod;
using Geomethod.Data;
using Terminal.Model;

namespace Terminal.Forms
{
	/// <summary>
	/// Summary description for ProductsForm.
	/// </summary>
	public class ProductUnitsForm : System.Windows.Forms.Form
	{
		string productCode;
		DataTable dataTable=new DataTable("ProductUnits");
		internal string unitCode="";
		internal string unitName="";
		internal decimal coef=0;
		decimal count;
		bool hasDecimals;
	
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

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.DataGrid dataGrid;
		private System.Windows.Forms.DataGridTextBoxColumn colUnitCode;
		private System.Windows.Forms.DataGridTextBoxColumn colCoef;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Button btnBackspace;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.TextBox tbCount;
		private System.Windows.Forms.DataGridTextBoxColumn colUnitName;
	
		DataRow CurrentDataRow{get{return DataGridUtils.CurrentDataRow(dataGrid);}}
		
		public ProductUnitsForm(string productCode,bool hasDecimals)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.productCode=productCode;
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
			this.dataGrid = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.colUnitCode = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colUnitName = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colCoef = new System.Windows.Forms.DataGridTextBoxColumn();
			this.lblCount = new System.Windows.Forms.Label();
			this.btnBackspace = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tbCount = new System.Windows.Forms.TextBox();
			// 
			// dataGrid
			// 
			this.dataGrid.Size = new System.Drawing.Size(238, 216);
			this.dataGrid.TableStyles.Add(this.dataGridTableStyle1);
			this.dataGrid.CurrentCellChanged += new System.EventHandler(this.dgProducts_CurrentCellChanged);
			this.dataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid_KeyDown);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colUnitCode);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colUnitName);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colCoef);
			this.dataGridTableStyle1.MappingName = "ProductUnits";
			// 
			// colUnitCode
			// 
			this.colUnitCode.HeaderText = "Unit Code";
			this.colUnitCode.MappingName = "UnitCode";
			this.colUnitCode.NullText = "(null)";
			// 
			// colUnitName
			// 
			this.colUnitName.HeaderText = "Unit Name";
			this.colUnitName.MappingName = "UnitName";
			this.colUnitName.NullText = "(null)";
			this.colUnitName.Width = 120;
			// 
			// colCoef
			// 
			this.colCoef.HeaderText = "Coeff";
			this.colCoef.MappingName = "Coef";
			this.colCoef.NullText = "(null)";
			this.colCoef.Width = 30;
			// 
			// lblCount
			// 
			this.lblCount.Location = new System.Drawing.Point(4, 218);
			this.lblCount.Size = new System.Drawing.Size(72, 20);
			this.lblCount.Text = "Count:";
			// 
			// btnBackspace
			// 
			this.btnBackspace.Location = new System.Drawing.Point(208, 246);
			this.btnBackspace.Size = new System.Drawing.Size(20, 20);
			this.btnBackspace.Text = "<-";
			this.btnBackspace.Visible = false;
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(232, 246);
			this.btnClear.Size = new System.Drawing.Size(20, 20);
			this.btnClear.Text = "x";
			this.btnClear.Visible = false;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(30, 244);
			this.btnOk.Text = "OK";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(134, 244);
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// tbCount
			// 
			this.tbCount.Location = new System.Drawing.Point(82, 218);
			this.tbCount.MaxLength = 12;
			this.tbCount.Size = new System.Drawing.Size(104, 20);
			this.tbCount.Text = "";
			// 
			// ProductUnitsForm
			// 
			this.ClientSize = new System.Drawing.Size(238, 268);
			this.Controls.Add(this.tbCount);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnBackspace);
			this.Controls.Add(this.lblCount);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.dataGrid);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "Measure unit selection";
			this.Load += new System.EventHandler(this.ProductsForm_Load);

		}
		#endregion

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

			DataRow dr=CurrentDataRow;
			if(dr!=null)
			{
				unitCode=(string)dr["UnitCode"];
				unitName=(string)dr["UnitName"];
				coef=(decimal)dr["Coef"];
				base.DialogResult=DialogResult.OK;
			}
			Close();
		}
		private void btnOk_Click(object sender, System.EventArgs e)
		{
			OnOk();
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

		private void ProductsForm_Load(object sender, System.EventArgs e)
		{
			Utils.OnFormLoad(this);
			LoadData();
			UpdateControls();
			DataGridUtils.SelectCurRow(dataGrid);
//			this.nudCount.KeyDown+=new KeyEventHandler(nudCount_KeyDown);
//			new NudFix(nudCount,btnBackspace,btnClear);
//			this.lblDecimal.Visible=hasDecimals;
//			this.tbDecimal.Visible=hasDecimals;
		}

		void LoadData()
		{
			using(GmConnection conn=App.ConnectionFactory.CreateConnection())
			{
				GmCommand cmd=conn.CreateCommand("select UnitCode, Name as UnitName, Coef from Unit where DeletedFlag=0 and ProductCode=@ProductCode");


				cmd.AddString("ProductCode",productCode);
				DbDataAdapter da=conn.CreateDataAdapter(cmd);
				da.Fill(dataTable);
			}
			dataGrid.DataSource=dataTable;
			UpdateControls();
			dataGrid.Focus();
		}

		private void dgProducts_CurrentCellChanged(object sender, System.EventArgs e)
		{
			DataGridUtils.SelectCurRow(this.dataGrid);
		}

		void UpdateControls()
		{
			btnOk.Enabled=dataTable.Rows.Count>0 && dataGrid.CurrentRowIndex>=0;
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
		private void nudCount_KeyDown(object sender, KeyEventArgs e)
		{
			OnKey(e);
		}

		private void dataGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}

		private void nudCount_ValueChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
