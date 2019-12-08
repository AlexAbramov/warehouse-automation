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
	public class ProductsForm : System.Windows.Forms.Form
	{
		DataTable dataTable=new DataTable("Products");		
		internal Product selectedProduct=null;
		static Product filter=null;
		Hashtable mappingNames=new Hashtable();
		bool keepSearch=false;

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn colProductName;
		private System.Windows.Forms.DataGridTextBoxColumn colProductCode;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ToolBarButton tbbCode;
		private System.Windows.Forms.ToolBarButton tbbName;
		private System.Windows.Forms.ToolBarButton tbbArticul;
		private System.Windows.Forms.ToolBarButton tbbName2;
		private System.Windows.Forms.Label lblRec;
		private System.Windows.Forms.Label lblSearch;
		private System.Windows.Forms.DataGridTextBoxColumn colArticul;
		private System.Windows.Forms.ToolBarButton tbbSep;
		private System.Windows.Forms.ToolBarButton tbbSep2;
		private System.Windows.Forms.ToolBarButton tbbGroup;
		private System.Windows.Forms.Label lblGroup;
		private System.Windows.Forms.ToolBarButton tbbAntiGroup;
		private System.Windows.Forms.DataGrid dataGrid;
	
		DataRow CurrentDataRow{get{return DataGridUtils.CurrentDataRow(dataGrid);}}

		public ProductsForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProductsForm));
			this.dataGrid = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.colProductCode = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colArticul = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colProductName = new System.Windows.Forms.DataGridTextBoxColumn();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.imageList = new System.Windows.Forms.ImageList();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.tbbCode = new System.Windows.Forms.ToolBarButton();
			this.tbbName = new System.Windows.Forms.ToolBarButton();
			this.tbbSep = new System.Windows.Forms.ToolBarButton();
			this.tbbArticul = new System.Windows.Forms.ToolBarButton();
			this.tbbName2 = new System.Windows.Forms.ToolBarButton();
			this.tbbSep2 = new System.Windows.Forms.ToolBarButton();
			this.tbbGroup = new System.Windows.Forms.ToolBarButton();
			this.tbbAntiGroup = new System.Windows.Forms.ToolBarButton();
			this.lblRec = new System.Windows.Forms.Label();
			this.lblSearch = new System.Windows.Forms.Label();
			this.lblGroup = new System.Windows.Forms.Label();
			// 
			// dataGrid
			// 
			this.dataGrid.Location = new System.Drawing.Point(0, 24);
			this.dataGrid.Size = new System.Drawing.Size(238, 162);
			this.dataGrid.TableStyles.Add(this.dataGridTableStyle1);
			this.dataGrid.CurrentCellChanged += new System.EventHandler(this.dgProducts_CurrentCellChanged);
			this.dataGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dataGrid_KeyPress);
			this.dataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid_KeyDown);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colProductCode);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colArticul);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colProductName);
			this.dataGridTableStyle1.MappingName = "Products";
			// 
			// colProductCode
			// 
			this.colProductCode.HeaderText = "Code";
			this.colProductCode.MappingName = "ProductCode";
			this.colProductCode.NullText = "(null)";
			// 
			// colArticul
			// 
			this.colArticul.HeaderText = "Articul";
			this.colArticul.MappingName = "Articul";
			this.colArticul.NullText = "(null)";
			// 
			// colProductName
			// 
			this.colProductName.HeaderText = "Product";
			this.colProductName.MappingName = "ProductName";
			this.colProductName.NullText = "(null)";
			this.colProductName.Width = 140;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(162, 242);
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(86, 242);
			this.btnOk.Text = "OK";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// imageList
			// 
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
			this.imageList.ImageSize = new System.Drawing.Size(20, 20);
			// 
			// toolBar
			// 
			this.toolBar.Buttons.Add(this.tbbCode);
			this.toolBar.Buttons.Add(this.tbbName);
			this.toolBar.Buttons.Add(this.tbbSep);
			this.toolBar.Buttons.Add(this.tbbArticul);
			this.toolBar.Buttons.Add(this.tbbName2);
			this.toolBar.Buttons.Add(this.tbbSep2);
			this.toolBar.Buttons.Add(this.tbbGroup);
			this.toolBar.Buttons.Add(this.tbbAntiGroup);
			this.toolBar.ImageList = this.imageList;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// tbbCode
			// 
			this.tbbCode.ImageIndex = 1;
			// 
			// tbbName
			// 
			this.tbbName.ImageIndex = 2;
			// 
			// tbbSep
			// 
			this.tbbSep.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbArticul
			// 
			this.tbbArticul.ImageIndex = 0;
			// 
			// tbbName2
			// 
			this.tbbName2.ImageIndex = 2;
			// 
			// tbbSep2
			// 
			this.tbbSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbGroup
			// 
			this.tbbGroup.ImageIndex = 3;
			// 
			// tbbAntiGroup
			// 
			this.tbbAntiGroup.Enabled = false;
			this.tbbAntiGroup.ImageIndex = 4;
			// 
			// lblRec
			// 
			this.lblRec.Location = new System.Drawing.Point(2, 208);
			this.lblRec.Size = new System.Drawing.Size(234, 32);
			// 
			// lblSearch
			// 
			this.lblSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.lblSearch.Location = new System.Drawing.Point(2, 242);
			this.lblSearch.Size = new System.Drawing.Size(80, 20);
			// 
			// lblGroup
			// 
			this.lblGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
			this.lblGroup.ForeColor = System.Drawing.Color.Blue;
			this.lblGroup.Location = new System.Drawing.Point(2, 190);
			this.lblGroup.Size = new System.Drawing.Size(234, 15);
			this.lblGroup.Text = "Product not found in group";
			this.lblGroup.Visible = false;
			// 
			// ProductsForm
			// 
			this.ClientSize = new System.Drawing.Size(238, 308);
			this.Controls.Add(this.lblGroup);
			this.Controls.Add(this.lblSearch);
			this.Controls.Add(this.lblRec);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.dataGrid);
			this.Controls.Add(this.toolBar);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "Product selection";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductsForm_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ProductsForm_KeyPress);
			this.Load += new System.EventHandler(this.ProductsForm_Load);

		}
		#endregion

		void OnOk()
		{
			DataRow dr=CurrentDataRow;
			if(dr!=null)
			{
				string productCode=(string)dr["ProductCode"];
				string productName=(string)dr["ProductName"];
				selectedProduct=new Product(productCode,productName);
				base.DialogResult=DialogResult.OK;
			}
			Close();
		}
		
		private void btnOk_Click(object sender, System.EventArgs e)
		{
			OnOk();
			/*DataRow dr=CurrentDataRow;
			if(dr!=null)
			{
				string productCode=(string)dr["ProductCode"];
				string productName=(string)dr["ProductName"];
				selectedProduct=new Product(productCode,productName);
				base.DialogResult=DialogResult.OK;
			}
			Close();*/
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

		void AddMappingName(DataGridColumnStyle col)
		{
			mappingNames.Add(col,col.MappingName);
		}

		string GetMappingName(DataGridColumnStyle col){return (string)mappingNames[col];}

		void SetVisible(DataGridColumnStyle col, bool vis)
		{
			col.MappingName=vis?GetMappingName(col):null;
		}

		private void ProductsForm_Load(object sender, System.EventArgs e)
		{
			Utils.OnFormLoad(this);
			AddMappingName(this.colProductCode);
			AddMappingName(this.colArticul);
			LoadData();
			UpdateControls();
			DataGridUtils.SelectCurRow(dataGrid);
		}

/*		void LoadData()
		{
			using(GmConnection conn=App.ConnectionFactory.CreateConnection())
			{	
				dataTable.Clear();
				GmCommand cmd=conn.CreateCommand("select ProductCode, Articul, Name as ProductName from Product where DeletedFlag=0 and GroupFlag=0");
				DbDataAdapter da=conn.CreateDataAdapter(cmd);				
				da.Fill(dataTable);				
			}

			dataGrid.DataSource=dataTable;
			UpdateControls();
			ButtonClick(this.tbbCode);
			UpdateStatus();
		}*/

		void LoadData ()
		{
			dataTable.Clear();
			string query = "select ProductCode, Articul, Name as ProductName from Product where DeletedFlag=0 and GroupFlag=0 ";
			using(GmConnection conn=App.ConnectionFactory.CreateConnection())
			{
				string fullCode=null;
				GmCommand cmd;

				if(filter!=null)
				{
					cmd=conn.CreateCommand("select FullCode from Product where ProductCode=@ProductCode");
					cmd.AddString("ProductCode",filter.Code);
										
					fullCode=cmd.ExecuteScalar() as string;
					if(fullCode!=null)
					{
						query=query+ " and FullCode like @FullCode";
					}
				}
				cmd=conn.CreateCommand(query);

				if(fullCode!=null)
				{
					cmd.AddString("FullCode",fullCode+"%");
				}
				DbDataAdapter da=conn.CreateDataAdapter(cmd);				
				da.Fill(dataTable);
			}

			dataGrid.DataSource=dataTable;
			UpdateControls();
			ButtonClick(this.tbbCode);
			UpdateStatus();
		
		}

		void UpdateStatus()
		{
			DataRow dr=DataGridUtils.CurrentDataRow(dataGrid);
			if(dr!=null)
			{
				lblRec.Text=string.Format("{{{0}/{1}}} {2}",dr["ProductCode"],dr["Articul"],dr["ProductName"]);
			}
			else
			{
				lblRec.Text="";
				lblGroup.Text="";
			}
		}

		private void dgProducts_CurrentCellChanged(object sender, System.EventArgs e)
		{
			DataGridUtils.SelectCurRow(this.dataGrid);
			if(!keepSearch)
			{
				ClearSearch();
			}
			UpdateStatus();
		}

		void UpdateControls()
		{
			btnOk.Enabled=dataTable.Rows.Count>0 && dataGrid.CurrentRowIndex>=0;
			bool hasFilter=filter!=null;
			tbbAntiGroup.Enabled=hasFilter;
			dataGrid.Height=hasFilter?165:180;
			lblGroup.Text=filter!=null? filter.Name:"";
			lblGroup.Visible=hasFilter;					
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			ButtonClick(e.Button);
			dataGrid.Focus();
		}

		void SetPushed(ToolBarButton tbb, bool pushed)
		{
			tbb.Pushed=pushed;
		}
	/*	string GetFullGroup(Product pr)
		{
			if (pr.ParentCode==null) {return pr.Name}
			else 
			{
				
			return GetFullCode()+"/"+pr.Name
			
			}
		}*/

		void ButtonClick(ToolBarButton tbb)
		{
			if(tbb==this.tbbCode) UpdateGrid(tbb,true,GetMappingName(colProductCode));
			else if(tbb==this.tbbName) UpdateGrid(tbb,true,colProductName.MappingName);
			else if(tbb==this.tbbArticul) UpdateGrid(tbb,false,GetMappingName(colArticul));
			else if(tbb==this.tbbName2) UpdateGrid(tbb,false,colProductName.MappingName);			
			else if (tbb==this.tbbGroup)
			{
				GroupForm form=new GroupForm();
				if(form.ShowDialog()==DialogResult.OK)
				{
					filter=form.selectedProduct;
					LoadData();			
				}
			}
			else if (tbb==this.tbbAntiGroup)
			{
				filter=null;
				LoadData();
			}
			UpdateControls();
	  }

		void UpdateGrid(ToolBarButton tbb,bool showCode,string colName)
		{
			SetPushed(tbbCode,false);
			SetPushed(tbbName,false);
			SetPushed(tbbArticul,false);
			SetPushed(tbbName2,false);
			SetPushed(tbb,true);
			ClearSearch();
			UpdateColumns(showCode);
			SortGrid(colName);
		}

		void UpdateColumns(bool showCode)
		{
			this.SetVisible(this.colProductCode,showCode);
			this.SetVisible(this.colArticul,!showCode);
		}

		void SortGrid(string colName)
		{
			DataRow dr=DataGridUtils.CurrentDataRow(this.dataGrid);
			dataTable.DefaultView.Sort=colName;
			DataGridUtils.SelectRow(dataGrid,dr);
		}

		void ClearSearch()
		{
			lblSearch.Text="";
		}

		void SearchRecord()
		{
			if(lblSearch.Text.Length>0)
			{
				string colName=this.colProductName.MappingName;
				if(this.tbbCode.Pushed)
				{
					colName=this.colProductCode.MappingName;
				}
				else if(this.tbbArticul.Pushed)
				{
					colName=this.colArticul.MappingName;
				}
				string order=colName;
				string cond=string.Format("{0} like '{1}*'",colName,lblSearch.Text);
				DataRow[] rows=dataTable.Select(cond, order);
				if(rows.Length>0)
				{
					keepSearch=true;
					DataGridUtils.SelectRow(dataGrid,rows[0]);
					keepSearch=false;
				}
			}
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
				case Keys.Back:
					if(lblSearch.Text.Length>0)
					{
						lblSearch.Text=lblSearch.Text.Substring(0,lblSearch.Text.Length-1);
						SearchRecord();
					}
					break;
			}
			dataGrid.Focus();
		}

		void OnKey(KeyPressEventArgs e)
		{
			char c=e.KeyChar;
			const string tokens=" _/";
			if(char.IsLetterOrDigit(c)||tokens.IndexOf(c)>=0)
			{
				lblSearch.Text+=c;
				SearchRecord();
			}
			dataGrid.Focus();
		}

		private void dataGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
		  OnKey(e);
		}

		private void ProductsForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}

		private void dataGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			OnKey(e);
		}

		private void ProductsForm_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			OnKey(e);
		}
	}
}
