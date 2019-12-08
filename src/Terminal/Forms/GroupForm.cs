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
	/// Summary description for GroupForm.
	/// </summary>
	public class GroupForm : System.Windows.Forms.Form
	{
		Hashtable htProducts=new Hashtable(1<<12);// productCode,product
		ArrayList rootProducts=new ArrayList();
		
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label lblRec;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.TreeView treeView;
	
		internal Product selectedProduct=null;
		Product CurrentProduct{get{return treeView.SelectedNode!=null? treeView.SelectedNode.Tag as Product : null;}}

		public GroupForm()
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
			this.treeView = new System.Windows.Forms.TreeView();
			this.lblRec = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(160, 244);
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(4, 244);
			this.btnOk.Text = "OK";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// lblRec
			// 
			this.lblRec.Location = new System.Drawing.Point(2, 208);
			this.lblRec.Size = new System.Drawing.Size(234, 34);
			// 
			// treeView
			// 
			this.treeView.ImageIndex = -1;
			this.treeView.SelectedImageIndex = -1;
			this.treeView.Size = new System.Drawing.Size(238, 206);
			this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(82, 244);
			this.btnClear.Text = "Reset";
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// GroupForm
			// 
			this.ClientSize = new System.Drawing.Size(238, 262);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.treeView);
			this.Controls.Add(this.lblRec);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "Product category selection";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductsForm_KeyDown);
			this.Load += new System.EventHandler(this.ProductsForm_Load);

		}
		#endregion

		void OnOk()
		{
			base.DialogResult=DialogResult.OK;			
			selectedProduct=this.CurrentProduct;
			Close();
		}
		
		void Cancel()
		{
			base.DialogResult=DialogResult.Cancel;
			Close();
		}
		
		private void btnOk_Click(object sender, System.EventArgs e)
		{
			OnOk();
//			base.DialogResult=DialogResult.OK;

/*			DataRow dr=CurrentDataRow;
			if(dr!=null)
			{
				productCode=(string)dr["ProductCode"];
				productName=(string)dr["ProductName"];
				base.DialogResult=DialogResult.OK;
			}*/
//			selectedProduct=this.CurrentProduct;
//			Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			Cancel();
		}

		private void ProductsForm_Load(object sender, System.EventArgs e)
		{
			Utils.OnFormLoad(this);
			LoadData();
			UpdateControls();
			treeView.KeyDown+=new KeyEventHandler(treeView_KeyDown);
		}

		void LoadData()
		{
			using(GmConnection conn=App.ConnectionFactory.CreateConnection())
			{
				GmCommand cmd=conn.CreateCommand("select ProductCode, Name as ProductName, ParentProductCode from Product where DeletedFlag=0 and GroupFlag=1");
				

				using(IDataReader dr=cmd.ExecuteReader())
				{
					while(dr.Read())
					{
						Product p=new Product(dr);
						htProducts.Add(p.Code,p);
					}
				}
			}
			foreach(Product p in htProducts.Values)
			{
				if(p.ParentCode!=null)
				{					
					Product parent=htProducts[p.ParentCode] as Product;
					p.SetParent(parent);
				}
				else rootProducts.Add(p);
			}
			treeView.BeginUpdate();
			BuildTree(treeView.Nodes, rootProducts);
			treeView.EndUpdate();
			UpdateControls();
			UpdateStatus();
		}

		void BuildTree(TreeNodeCollection nodes,IEnumerable products)// iterative
		{
			foreach(Product p in products)
			{
				TreeNode tn=new TreeNode(p.Name);
				tn.Tag=p;
				nodes.Add(tn);
				if(p.Products!=null)
				{
					BuildTree(tn.Nodes,p.Products);
				}
			}
		}

		void UpdateStatus()
		{
			Product p=CurrentProduct;
			if(p!=null)
			{
				lblRec.Text=string.Format("{0} {1}",p.Code,p.Name);
			}
			else
			{
				lblRec.Text="";
			}
		}

		void UpdateControls()
		{
			Product p=CurrentProduct;
			btnOk.Enabled=p!=null;
		}

		void OnKey(KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter:
					OnOk();
					break;

				case Keys.Escape:
					Cancel();
					break;

			}
//			lblSearch.Text=lblSearch.Text.ToLower();
//			dataGrid.Focus();
		}

		private void ProductsForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			base.DialogResult=DialogResult.OK;
			selectedProduct=null;
			Close();
		}

		private void treeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			UpdateStatus();
			UpdateControls();
			//this.TopLevelControl.Focus();
		}

		private void treeView_KeyDown(object sender, KeyEventArgs e)
		{
			OnKey(e);
		}
	}
}
