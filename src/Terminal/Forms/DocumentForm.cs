using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using Geomethod.Windows.Forms;
using Geomethod;
using Geomethod.Data;
using Terminal.Model;

namespace Terminal.Forms
{
	/// <summary>
	/// Summary description for DocumentForm.
	/// </summary>
	public class DocumentForm : System.Windows.Forms.Form, IScanHandler
	{
		static bool discarded=false;
		enum SortKey{None,Articul,Name,Code}
		bool ito=false;

		TabPage SelectedPage{get{return tabControl.TabPages[tabControl.SelectedIndex];}}

		Document doc;
		DataTable dtFact=new DataTable("Fact");
		DataTable dtPlan=new DataTable("Plan");
		DbDataAdapter dataAdapter;
		Hashtable mappingNames=new Hashtable();
		SortKey factSortKey=SortKey.Name;
		SortKey planSortKey=SortKey.Name;
		bool updated=false;

		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tpFact;
		private System.Windows.Forms.TabPage tpPlan;
		private System.Windows.Forms.DataGrid dgFact;
		private System.Windows.Forms.DataGrid dgPlan;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle2;
		private System.Windows.Forms.DataGridTextBoxColumn colCount2;
		private System.Windows.Forms.DataGridTextBoxColumn colProductName;
		private System.Windows.Forms.DataGridTextBoxColumn colCount;
		private System.Windows.Forms.DataGridTextBoxColumn colUnit;
		private System.Windows.Forms.DataGridTextBoxColumn colProductName2;
		private System.Windows.Forms.DataGridTextBoxColumn colUnitName2;
		private System.Windows.Forms.DataGridTextBoxColumn colProductCode;
		private System.Windows.Forms.DataGridTextBoxColumn colProductCode2;
		private System.Windows.Forms.ToolBarButton tbbDelete;
		private System.Windows.Forms.ToolBarButton tbbDeleteAll;
		private System.Windows.Forms.ToolBarButton tbbSave;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolBarButton tbbComplete;
		private System.Windows.Forms.DataGridTextBoxColumn colArticul;
		private System.Windows.Forms.DataGridTextBoxColumn colArticul2;
		private System.Windows.Forms.ToolBarButton tbbSortByArticul;
		private System.Windows.Forms.ToolBarButton tbbSortByName;
		private System.Windows.Forms.ToolBarButton tbbSortByCode;
		private System.Windows.Forms.ToolBarButton tbbProp;
		private System.Windows.Forms.Label lblNom;
		private System.Windows.Forms.Label lblItog;
		private System.Windows.Forms.Label lblNomVal;
		private System.Windows.Forms.Label lblItogVal;
		private System.Windows.Forms.Label lblRec;
		private System.Windows.Forms.TabPage tpDelivery;
		private System.Windows.Forms.Button btnFill;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblStoreName;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label lblStoreSectionName;
		private System.Windows.Forms.Label lblOrderToStore;
		private System.Windows.Forms.Label lblContractorName;
		private System.Windows.Forms.Label lblSpoilageFlag;
		private System.Windows.Forms.Label lblContainer;
		private System.Windows.Forms.Label lblMakeOfCar;
		private System.Windows.Forms.Label lblTruckNumber;
		private System.Windows.Forms.DataGridTextBoxColumn colHandledFlagStr;
		private System.Windows.Forms.DataGridTextBoxColumn colHandledFlagStr2;
		private System.Windows.Forms.ToolBarButton tbbAdd;
	
		DataRow CurrentFactDataRow{get{return DataGridUtils.CurrentDataRow(dgFact);}}

		public Document Document{get{return doc;}}

		public DocumentForm(Document doc)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.doc=doc;
			ito=doc.IsITO;
			if(ito) this.tabControl.TabPages.RemoveAt(1);// this.tpPlan.Visible=false;
			discarded=false;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			App.ScanHandler=null;
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DocumentForm));
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.tbbSortByName = new System.Windows.Forms.ToolBarButton();
			this.tbbSortByArticul = new System.Windows.Forms.ToolBarButton();
			this.tbbSortByCode = new System.Windows.Forms.ToolBarButton();
			this.tbbAdd = new System.Windows.Forms.ToolBarButton();
			this.tbbDelete = new System.Windows.Forms.ToolBarButton();
			this.tbbDeleteAll = new System.Windows.Forms.ToolBarButton();
			this.tbbProp = new System.Windows.Forms.ToolBarButton();
			this.tbbSave = new System.Windows.Forms.ToolBarButton();
			this.tbbComplete = new System.Windows.Forms.ToolBarButton();
			this.imageList = new System.Windows.Forms.ImageList();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tpFact = new System.Windows.Forms.TabPage();
			this.dgFact = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.colProductName = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colArticul = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colProductCode = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colCount = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colUnit = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colHandledFlagStr = new System.Windows.Forms.DataGridTextBoxColumn();
			this.tpPlan = new System.Windows.Forms.TabPage();
			this.dgPlan = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle2 = new System.Windows.Forms.DataGridTableStyle();
			this.colProductName2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colArticul2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colProductCode2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colCount2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colUnitName2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colHandledFlagStr2 = new System.Windows.Forms.DataGridTextBoxColumn();
			this.tpDelivery = new System.Windows.Forms.TabPage();
			this.lblSpoilageFlag = new System.Windows.Forms.Label();
			this.lblContainer = new System.Windows.Forms.Label();
			this.lblMakeOfCar = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.lblTruckNumber = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.lblContractorName = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lblOrderToStore = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblStoreSectionName = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblStoreName = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnFill = new System.Windows.Forms.Button();
			this.lblNom = new System.Windows.Forms.Label();
			this.lblItog = new System.Windows.Forms.Label();
			this.lblNomVal = new System.Windows.Forms.Label();
			this.lblItogVal = new System.Windows.Forms.Label();
			this.lblRec = new System.Windows.Forms.Label();
			// 
			// toolBar
			// 
			this.toolBar.Buttons.Add(this.tbbSortByName);
			this.toolBar.Buttons.Add(this.tbbSortByArticul);
			this.toolBar.Buttons.Add(this.tbbSortByCode);
			this.toolBar.Buttons.Add(this.tbbAdd);
			this.toolBar.Buttons.Add(this.tbbDelete);
			this.toolBar.Buttons.Add(this.tbbDeleteAll);
			this.toolBar.Buttons.Add(this.tbbProp);
			this.toolBar.Buttons.Add(this.tbbSave);
			this.toolBar.Buttons.Add(this.tbbComplete);
			this.toolBar.ImageList = this.imageList;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// tbbSortByName
			// 
			this.tbbSortByName.ImageIndex = 8;
			// 
			// tbbSortByArticul
			// 
			this.tbbSortByArticul.ImageIndex = 9;
			// 
			// tbbSortByCode
			// 
			this.tbbSortByCode.ImageIndex = 10;
			// 
			// tbbAdd
			// 
			this.tbbAdd.ImageIndex = 0;
			// 
			// tbbDelete
			// 
			this.tbbDelete.ImageIndex = 1;
			// 
			// tbbDeleteAll
			// 
			this.tbbDeleteAll.ImageIndex = 2;
			// 
			// tbbProp
			// 
			this.tbbProp.ImageIndex = 5;
			// 
			// tbbSave
			// 
			this.tbbSave.ImageIndex = 3;
			// 
			// tbbComplete
			// 
			this.tbbComplete.ImageIndex = 4;
			// 
			// imageList
			// 
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource1"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource2"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource3"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource4"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource5"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource6"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource7"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource8"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource9"))));
			this.imageList.Images.Add(((System.Drawing.Image)(resources.GetObject("resource10"))));
			this.imageList.ImageSize = new System.Drawing.Size(20, 20);
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tpFact);
			this.tabControl.Controls.Add(this.tpPlan);
			this.tabControl.Controls.Add(this.tpDelivery);
			this.tabControl.Location = new System.Drawing.Point(0, 26);
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(238, 192);
			this.tabControl.GotFocus += new System.EventHandler(this.tabControl_GotFocus);
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tpFact
			// 
			this.tpFact.Controls.Add(this.dgFact);
			this.tpFact.Location = new System.Drawing.Point(4, 22);
			this.tpFact.Size = new System.Drawing.Size(230, 166);
			this.tpFact.Text = "Actual";
			// 
			// dgFact
			// 
			this.dgFact.Size = new System.Drawing.Size(230, 166);
			this.dgFact.TableStyles.Add(this.dataGridTableStyle1);
			this.dgFact.CurrentCellChanged += new System.EventHandler(this.dgFact_CurrentCellChanged);
			this.dgFact.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgFact_KeyDown);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colProductName);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colArticul);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colProductCode);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colCount);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colUnit);
			this.dataGridTableStyle1.GridColumnStyles.Add(this.colHandledFlagStr);
			this.dataGridTableStyle1.MappingName = "Fact";
			// 
			// colProductName
			// 
			this.colProductName.HeaderText = "Product";
			this.colProductName.MappingName = "ProductName";
			this.colProductName.NullText = "(null)";
			this.colProductName.Width = 130;
			// 
			// colArticul
			// 
			this.colArticul.HeaderText = "Articul";
			this.colArticul.MappingName = "Articul";
			this.colArticul.NullText = "(null)";
			// 
			// colProductCode
			// 
			this.colProductCode.HeaderText = "Code";
			this.colProductCode.MappingName = "ProductCode";
			this.colProductCode.NullText = "(null)";
			this.colProductCode.Width = 40;
			// 
			// colCount
			// 
			this.colCount.HeaderText = "Count";
			this.colCount.MappingName = "Count";
			this.colCount.NullText = "(null)";
			this.colCount.Width = 30;
			// 
			// colUnit
			// 
			this.colUnit.HeaderText = "Unit";
			this.colUnit.MappingName = "UnitName";
			this.colUnit.NullText = "(null)";
			this.colUnit.Width = 30;
			// 
			// colHandledFlagStr
			// 
			this.colHandledFlagStr.NullText = "(null)";
			this.colHandledFlagStr.Width = 10;
			// 
			// tpPlan
			// 
			this.tpPlan.Controls.Add(this.dgPlan);
			this.tpPlan.Location = new System.Drawing.Point(4, 22);
			this.tpPlan.Size = new System.Drawing.Size(230, 166);
			this.tpPlan.Text = "Plan";
			// 
			// dgPlan
			// 
			this.dgPlan.Size = new System.Drawing.Size(229, 166);
			this.dgPlan.TableStyles.Add(this.dataGridTableStyle2);
			this.dgPlan.CurrentCellChanged += new System.EventHandler(this.dgPlan_CurrentCellChanged);
			this.dgPlan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgPlan_KeyDown);
			// 
			// dataGridTableStyle2
			// 
			this.dataGridTableStyle2.GridColumnStyles.Add(this.colProductName2);
			this.dataGridTableStyle2.GridColumnStyles.Add(this.colArticul2);
			this.dataGridTableStyle2.GridColumnStyles.Add(this.colProductCode2);
			this.dataGridTableStyle2.GridColumnStyles.Add(this.colCount2);
			this.dataGridTableStyle2.GridColumnStyles.Add(this.colUnitName2);
			this.dataGridTableStyle2.GridColumnStyles.Add(this.colHandledFlagStr2);
			this.dataGridTableStyle2.MappingName = "Plan";
			// 
			// colProductName2
			// 
			this.colProductName2.HeaderText = "Product";
			this.colProductName2.MappingName = "ProductName";
			this.colProductName2.NullText = "(null)";
			this.colProductName2.Width = 120;
			// 
			// colArticul2
			// 
			this.colArticul2.HeaderText = "Articul";
			this.colArticul2.MappingName = "Articul";
			this.colArticul2.NullText = "(null)";
			// 
			// colProductCode2
			// 
			this.colProductCode2.HeaderText = "Code";
			this.colProductCode2.MappingName = "ProductCode";
			this.colProductCode2.NullText = "(null)";
			this.colProductCode2.Width = 40;
			// 
			// colCount2
			// 
			this.colCount2.HeaderText = "Count";
			this.colCount2.MappingName = "Count";
			this.colCount2.NullText = "(null)";
			this.colCount2.Width = 30;
			// 
			// colUnitName2
			// 
			this.colUnitName2.HeaderText = "Unit";
			this.colUnitName2.MappingName = "UnitName";
			this.colUnitName2.NullText = "(null)";
			this.colUnitName2.Width = 30;
			// 
			// colHandledFlagStr2
			// 
			this.colHandledFlagStr2.MappingName = "HandledFlagStr";
			this.colHandledFlagStr2.NullText = "(null)";
			this.colHandledFlagStr2.Width = 10;
			// 
			// tpDelivery
			// 
			this.tpDelivery.Controls.Add(this.lblSpoilageFlag);
			this.tpDelivery.Controls.Add(this.lblContainer);
			this.tpDelivery.Controls.Add(this.lblMakeOfCar);
			this.tpDelivery.Controls.Add(this.label9);
			this.tpDelivery.Controls.Add(this.lblTruckNumber);
			this.tpDelivery.Controls.Add(this.label11);
			this.tpDelivery.Controls.Add(this.lblContractorName);
			this.tpDelivery.Controls.Add(this.label13);
			this.tpDelivery.Controls.Add(this.label5);
			this.tpDelivery.Controls.Add(this.lblOrderToStore);
			this.tpDelivery.Controls.Add(this.label7);
			this.tpDelivery.Controls.Add(this.lblStoreSectionName);
			this.tpDelivery.Controls.Add(this.label3);
			this.tpDelivery.Controls.Add(this.lblStoreName);
			this.tpDelivery.Controls.Add(this.label1);
			this.tpDelivery.Controls.Add(this.btnFill);
			this.tpDelivery.Location = new System.Drawing.Point(4, 22);
			this.tpDelivery.Size = new System.Drawing.Size(230, 166);
			this.tpDelivery.Text = "More";
			// 
			// lblSpoilageFlag
			// 
			this.lblSpoilageFlag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.lblSpoilageFlag.ForeColor = System.Drawing.Color.Red;
			this.lblSpoilageFlag.Location = new System.Drawing.Point(102, 145);
			this.lblSpoilageFlag.Size = new System.Drawing.Size(126, 20);
			this.lblSpoilageFlag.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// lblContainer
			// 
			this.lblContainer.Location = new System.Drawing.Point(73, 127);
			this.lblContainer.Size = new System.Drawing.Size(154, 16);
			// 
			// lblMakeOfCar
			// 
			this.lblMakeOfCar.Location = new System.Drawing.Point(98, 110);
			this.lblMakeOfCar.Size = new System.Drawing.Size(130, 16);
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label9.Location = new System.Drawing.Point(1, 127);
			this.label9.Size = new System.Drawing.Size(70, 16);
			this.label9.Text = "Container:";
			// 
			// lblTruckNumber
			// 
			this.lblTruckNumber.Location = new System.Drawing.Point(97, 92);
			this.lblTruckNumber.Size = new System.Drawing.Size(131, 16);
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label11.Location = new System.Drawing.Point(1, 109);
			this.label11.Size = new System.Drawing.Size(94, 16);
			this.label11.Text = "Car make:";
			// 
			// lblContractorName
			// 
			this.lblContractorName.Location = new System.Drawing.Point(78, 74);
			this.lblContractorName.Size = new System.Drawing.Size(150, 16);
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label13.Location = new System.Drawing.Point(1, 92);
			this.label13.Size = new System.Drawing.Size(95, 16);
			this.label13.Text = "Car tag:";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(1, 74);
			this.label5.Size = new System.Drawing.Size(75, 16);
			this.label5.Text = "Counterpart:";
			// 
			// lblOrderToStore
			// 
			this.lblOrderToStore.Location = new System.Drawing.Point(60, 37);
			this.lblOrderToStore.Size = new System.Drawing.Size(168, 35);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label7.Location = new System.Drawing.Point(1, 38);
			this.label7.Size = new System.Drawing.Size(57, 16);
			this.label7.Text = "Task:";
			// 
			// lblStoreSectionName
			// 
			this.lblStoreSectionName.Location = new System.Drawing.Point(54, 20);
			this.lblStoreSectionName.Size = new System.Drawing.Size(174, 16);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label3.Location = new System.Drawing.Point(1, 20);
			this.label3.Size = new System.Drawing.Size(51, 16);
			this.label3.Text = "Section:";
			// 
			// lblStoreName
			// 
			this.lblStoreName.Location = new System.Drawing.Point(47, 1);
			this.lblStoreName.Size = new System.Drawing.Size(181, 16);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label1.Location = new System.Drawing.Point(1, 1);
			this.label1.Size = new System.Drawing.Size(44, 16);
			this.label1.Text = "Warehouse:";
			// 
			// btnFill
			// 
			this.btnFill.Location = new System.Drawing.Point(1, 143);
			this.btnFill.Size = new System.Drawing.Size(99, 18);
			this.btnFill.Text = "Fill actual";
			this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
			// 
			// lblNom
			// 
			this.lblNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.lblNom.Location = new System.Drawing.Point(0, 251);
			this.lblNom.Size = new System.Drawing.Size(32, 14);
			this.lblNom.Text = "Nominal:";
			// 
			// lblItog
			// 
			this.lblItog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.lblItog.Location = new System.Drawing.Point(114, 251);
			this.lblItog.Size = new System.Drawing.Size(39, 14);
			this.lblItog.Text = "Total:";
			// 
			// lblNomVal
			// 
			this.lblNomVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.lblNomVal.Location = new System.Drawing.Point(33, 251);
			this.lblNomVal.Size = new System.Drawing.Size(79, 14);
			this.lblNomVal.Text = "0/0";
			// 
			// lblItogVal
			// 
			this.lblItogVal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
			this.lblItogVal.Location = new System.Drawing.Point(155, 251);
			this.lblItogVal.Size = new System.Drawing.Size(80, 14);
			this.lblItogVal.Text = "0/0";
			// 
			// lblRec
			// 
			this.lblRec.Location = new System.Drawing.Point(2, 220);
			this.lblRec.Size = new System.Drawing.Size(234, 29);
			// 
			// DocumentForm
			// 
			this.ClientSize = new System.Drawing.Size(238, 797);
			this.Controls.Add(this.lblRec);
			this.Controls.Add(this.lblItogVal);
			this.Controls.Add(this.lblNomVal);
			this.Controls.Add(this.lblItog);
			this.Controls.Add(this.lblNom);
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.toolBar);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Text = "Document";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DocumentForm_KeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.DocumentForm_Closing);
			this.Load += new System.EventHandler(this.DocumentForm_Load);

		}
		#endregion
		
		void SetUpdated(){updated=true;}

		void AddMappingName(DataGridColumnStyle col)
		{
			mappingNames.Add(col,col.MappingName);
		}

		void SetVisible(DataGridColumnStyle col, bool vis)
		{
			col.MappingName=vis?(string)mappingNames[col]:null;
		}

		private void DocumentForm_Load(object sender, System.EventArgs e)
		{
			Utils.OnFormLoad(this);
			AddMappingName(this.colProductCode);
			AddMappingName(this.colProductCode2);
			AddMappingName(colArticul);
			AddMappingName(colArticul2);
			using(GmConnection conn=App.ConnectionFactory.CreateConnection())
			{	
				GmCommand cmd=conn.CreateCommand("select DocumentStateId from Document where DocumentId=@DocumentId");				
				cmd.AddInt("DocumentId",doc.DocumentId);
			

				doc.DocumentStateId=(int)cmd.ExecuteScalar();
				switch(doc.DocumentStateId)
				{
					case (int)DocumentState.New:
						break;
					case (int)DocumentState.Recorded:
						break;
					default:
						string s=string.Format("Request rejected.\r\ndocId={0}; stateId={1}",doc.DocumentTypeShortName,doc.DocumentStateId);
						throw new TerminalException(s);
				}
				dataAdapter=DocumentProduct.CreateDataAdapter(conn,doc);
				dataAdapter.Fill(dtPlan);
				((dataAdapter as IDbDataAdapter).SelectCommand.Parameters["@FactFlag"] as IDbDataParameter).Value=true;
				dataAdapter.Fill(dtFact);
			}
			Init(dtPlan);

			lblStoreName.Text=doc.StoreName;
			lblStoreSectionName.Text=doc.StoreSectionName;
			lblOrderToStore.Text=doc.OrderToStore;
			lblContractorName.Text=doc.ContractorName;
			lblTruckNumber.Text=doc.TruckNumber;
			lblMakeOfCar.Text=doc.MakeOfCar;
			lblContainer.Text=doc.Container;
			lblSpoilageFlag.Text=doc.ProductQualityName;

			this.dgFact.DataSource=dtFact;
			this.dgPlan.DataSource=dtPlan;
			DataGridUtils.SetNullText(dgFact);
			DataGridUtils.SetNullText(dgPlan);
			DataGridUtils.SelectCurRow(dgFact);
			DataGridUtils.SelectCurRow(dgPlan);

			SetVisible(this.colArticul,false);
			SetVisible(this.colProductCode,false);
			SetVisible(this.colArticul2,false);
			SetVisible(this.colProductCode2,false);

			OnTabChanged();
			UpdateTitle();
			App.ScanHandler=this;
		}

		void Init(DataTable dt)
		{
			dt.Columns.Add("HandledFlagStr",typeof(char));
			foreach(DataRow dr in dt.Rows)
			{
				dr["HandledFlagStr"]=(bool)dr["HandledFlag"]?'+':' ';
			}
		}

		void AddListViewRow(string s1, string s2)
		{
			string[] ss={s1,s2};
			ListViewItem lvi=new ListViewItem(ss);
//!!!!			listView.Items.Add(lvi);
		}

		void UpdateTitle()
		{
			base.Text=string.Format("{0} {1:d/M/yy h:mm} {2} ",doc.DocumentTypeShortName,doc.Date,doc.Number);
			if(doc.Saved) base.Text+="Saved";
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button==tbbAdd) Add();
			else if(e.Button==tbbDelete) Delete();
			else if(e.Button==tbbDeleteAll) DeleteAll();
			else if(e.Button==tbbSave) Save();
			else if(e.Button==tbbComplete) Complete();
			else if(e.Button==this.tbbSortByArticul) Sort(SortKey.Articul);
			else if(e.Button==this.tbbSortByCode) Sort(SortKey.Code);
			else if(e.Button==this.tbbSortByName) Sort(SortKey.Name);
			else if(e.Button==this.tbbProp) EditCount();
			FocusCurrentGrid();
		}

		void FocusCurrentGrid()
		{
			TabPage tp=SelectedPage;
			if(tp==this.tpFact)	dgFact.Focus();
			else if(tp==this.tpPlan) dgPlan.Focus();
			else this.Focus();
		}

		void EditCount()
		{
			if(SelectedPage==this.tpFact)
			{
				DataRow dr=DataGridUtils.CurrentDataRow(this.dgFact);
				if(dr!=null)
				{
					DocumentProduct dp=new DocumentProduct(dr);
					OpenCountForm(dp);
				}
			}
		}

		void ChangeHandledFlag()
		{
			if(SelectedPage==this.tpPlan)
			{
				DataRow dr=DataGridUtils.CurrentDataRow(this.dgPlan);
				if(dr!=null)
				{
					DocumentProduct dp=new DocumentProduct(dr);
					string msg=dp.HandledFlag?"Unset 'Completed'?":"Set 'Completed'?";
					if(Utils.Confirm(msg))
					{
						dp.HandledFlag=!dp.HandledFlag;
						dp.DataRow["HandledFlagStr"]=dp.HandledFlag?'+':' ';
						this.SetUpdated();
						UpdateControls();
					}
				}
			}
		}

		void OpenCountForm(DocumentProduct dp)
		{
			CountForm form=new CountForm(doc.IsITO);
			form.Value=dp.Count;
//			form.chkHandled.Checked=dp.HandledFlag;
			if(form.ShowDialog()==DialogResult.OK)
			{
				dp.Count= form.Value;
//				dp.HandledFlag=form.chkHandled.Checked;
//				dp.DataRow["HandledFlagStr"]=dp.HandledFlag?'+':' ';
				SetUpdated();
				UpdateControls();
			}
		}

		void Sort(SortKey key)
		{
			string sort="";
			switch(key)
			{
				case SortKey.Articul:
					sort="Articul";
					break;
				case SortKey.Code:
//					sort="ProductCode";
					sort="Count Asc";
					break;
				case SortKey.Name:
//					sort="ProductName";
					sort="Count Desc";
					break;
			}
			TabPage tp=SelectedPage;
			if(tp==this.tpFact)
			{
				factSortKey=key;
				dtFact.DefaultView.Sort=sort;
				DataGridUtils.SelectCurRow(dgFact);
			}
			else if(tp==this.tpPlan)
			{
				planSortKey=key;
				dtPlan.DefaultView.Sort=sort;
				DataGridUtils.SelectCurRow(dgPlan);
			}
			else
			{
				key=SortKey.None;
			}
			UpdateSortButtons(key);
			UpdateStatus();
		}

		void UpdateSortButtons(SortKey key)
		{
			this.tbbSortByArticul.Pushed=false;
			this.tbbSortByCode.Pushed=false;
			this.tbbSortByName.Pushed=false;

			switch(key)
			{
				case SortKey.Articul:
					this.tbbSortByArticul.Pushed=true;
					break;
				case SortKey.Code:
					this.tbbSortByCode.Pushed=true;
					break;
				case SortKey.Name:
					this.tbbSortByName.Pushed=true;
					break;
			}

		}

		internal void EmulateScan()
		{
			OnScanned(new ScanData("EAN13","2050169900022"));
		}

		void Add()
		{
			try
			{
				ProductsForm pf=new ProductsForm();
				if(pf.ShowDialog()==DialogResult.OK)
				{
					ProductUnitsForm puf=new ProductUnitsForm(pf.selectedProduct.Code,doc.IsITO);
					if(puf.ShowDialog()==DialogResult.OK)
					{
						AddRow(pf.selectedProduct.Code,pf.selectedProduct.Name,puf.unitCode,puf.unitName,puf.coef,puf.Value,false);
					}
				}
			}
			catch(Exception ex)
			{
				Log.Exception(ex);
			}
		}

		void AddRow(string productCode,string productName,string unitCode,string unitName, decimal coef,decimal count,bool openCountForm)
		{
			DataRow dr=FindDataRow(productCode,unitCode,dtFact);
			bool newRow=dr==null;
			if(newRow)
			{
				DataRow dr2=FindDataRow(productCode,unitCode,dtPlan);
				if(dr2==null)
				{
					if(!ito)
					{
						string msg=string.Format("Product '{0}', unit - '{1}' not found in plan. Add?",productName,unitName);
						if(!Utils.Confirm(msg)) return;
					}
				}
				dr=dtFact.NewRow();
			}
			DocumentProduct dp=new DocumentProduct(dr);
			if(newRow)
			{
				dp.ProductCode=productCode;
				dp.ProductName=productName;
				dp.UnitCode=unitCode;
				dp.UnitName=unitName;
				dp.Coef=coef;
				dp.Count=count;
				dp.HandledFlag=false;
				dtFact.Rows.Add(dr);
			}
			else dp.Count+=count;
			if(openCountForm)
			{
				OpenCountForm(dp);
			}
			DataGridUtils.SelectRow(dgFact,dr);

			SetUpdated();
			UpdateControls();
		}

		DataRow FindDataRow(string productCode,string unitCode,DataTable dataTable)
		{
			string cmdText=string.Format("ProductCode='{0}' and UnitCode='{1}'",productCode,unitCode);
			DataRow[] rows=dataTable.Select(cmdText);
			return rows.Length>0 ? rows[0] : null;
		}

		void Delete()
		{
			try
			{
				if(Utils.Confirm("Delete current record?"))
				{
					Delete(this.CurrentFactDataRow);
				}
			}
			catch(Exception ex)
			{
				Log.Exception(ex);
			}
			UpdateControls();
		}

		void DeleteAll()
		{
			try
			{
				if(Utils.Confirm("Delete all records?"))
				{
					using(WaitCursor wc=new WaitCursor())
					{
						ArrayList rows=new ArrayList(dtFact.Rows);
						foreach(DataRow dr in rows)
						{
							Delete(dr);
						}
					}
				}
			}
			catch(Exception ex)
			{
				Log.Exception(ex);
			}
			UpdateControls();
		}

		void Delete(DataRow dr)
		{
			if(dr!=null)
			{
				dr.Delete();//.RowState=(dr.RowState==DataRowState.Added)? DataRowState.Detached: DataRowState.Deleted;
				SetUpdated();
			}
		}

		void UpdateControls()
		{
			if(discarded) return;

			bool hasFactRecords=DataGridUtils.GetNonDeletedRowsCount(dtFact)>0;
			bool factPageSelected=false;
			if(this.SelectedPage==this.tpFact)
			{
				factPageSelected=true;
			}

			tbbDelete.Enabled=factPageSelected && hasFactRecords;
			tbbDeleteAll.Enabled=factPageSelected && hasFactRecords;
			tbbProp.Enabled=factPageSelected && hasFactRecords;
			tbbSave.Enabled=updated;
			UpdateStatus();
		}

		internal void Discard()
		{
			discarded=true;
			App.ScanHandler=null;
			foreach(Control c in Controls) c.Enabled=false;
			foreach(ToolBarButton tbb in toolBar.Buttons) tbb.Enabled=false;
		}

  	void Save()
		{
			if(discarded) return;
			try
			{
				if(updated)
				{
					using(GmConnection conn=App.ConnectionFactory.CreateConnection())
					{
						Save(conn);
					}
				}
			}
			catch(Exception ex)
			{
				Log.Exception(ex);
			}
		}
		
		void Save(GmConnection conn)
		{
			if(updated)
			{
				(dataAdapter as IDbDataAdapter).InsertCommand.Connection=conn.DbConnection;
				(dataAdapter as IDbDataAdapter).DeleteCommand.Connection=conn.DbConnection;
				(dataAdapter as IDbDataAdapter).UpdateCommand.Connection=conn.DbConnection;
				dataAdapter.Update(dtFact);

				SavePlan(conn);				

				doc.DocumentStateId=(int)DocumentState.Recorded;
				doc.UpdateState(conn);
				updated=false;
				UpdateTitle();
				UpdateControls();
			}
		}

		void SavePlan(GmConnection conn)
		{
			if(ito) return;
			if(updated)
			{
				foreach(DataRow dr in dtPlan.Rows)
				{
					if(dr.RowState==DataRowState.Modified)
					{
						DocumentProduct dp=new DocumentProduct(dr);
						GmCommand cmd=conn.CreateCommand("update DocumentProduct set HandledFlag=@HandledFlag where DocumentProductId=@DocumentProductId");
						cmd.AddInt("DocumentProductId",dp.DocumentProductId);
						cmd.AddBool("HandledFlag",dp.HandledFlag);
						cmd.ExecuteNonQuery();
						dr.AcceptChanges();
					}
				}
			}
		}

		bool IsCheckSumCorrect
		{
			get
			{
				if(ito) return true;
				return GetCheckSum(dtFact)==GetCheckSum(dtPlan);
			}
		}

		static decimal GetCheckSum(DataTable dt)
		{
			return GetCheckSum(dt,dt.Rows);
		}

		static decimal GetCheckSum(DataTable dt,string productCode)
		{
			string cond=string.Format("ProductCode='{0}'",productCode);
			return GetCheckSum(dt,dt.Select(cond));
		}

		static decimal GetCheckSum(DataTable dt,IEnumerable dataRows)
		{
			decimal sum=0;
			DataColumn dcCount=dt.Columns["Count"];
			DataColumn dcCoef=dt.Columns["Coef"];
			foreach(DataRow dr in dataRows)
			{
				if(dr.RowState==DataRowState.Deleted) continue;
				if(!dr.IsNull(dcCount) && !dr.IsNull(dcCoef))
				{
					sum+=(decimal)dr[dcCount] * (decimal)dr[dcCoef];
				}
			}
			return sum;
		}

		void Complete()
		{
			try
			{
				string msg="";
				if(!IsCheckSumCorrect)
				{
					msg+="Completed count doesn't match planned value!\r\n";
				}
				msg+="Complete request?";
				if(Utils.Confirm(msg))
				{
					using(GmConnection conn=App.ConnectionFactory.CreateConnection())
					{							
						Save(conn);
						doc.DocumentStateId=(int)DocumentState.Complected;
						doc.UpdateState(conn);
					}
					Close();
				}
			}
			catch(Exception ex)
			{
				Log.Exception(ex);
			}
		}

		void OnTabChanged()
		{
			SortKey key=SortKey.None;
			TabPage tp=SelectedPage;
			if(tp==this.tpFact)
			{
				key=this.factSortKey;
			}
			else if(tp==this.tpPlan)
			{
				key=this.planSortKey;
				SetPlanRecord();
			}
			this.UpdateSortButtons(key);
			UpdateControls();
		}

		void SetPlanRecord()
		{
			DataRow dr=DataGridUtils.CurrentDataRow(this.dgFact);
			if(dr!=null)
			{
				string productCode=dr["ProductCode"] as string;
				string cond=string.Format("ProductCode='{0}'",productCode);
				DataRow[] rows=dtPlan.Select(cond);
				if(rows.Length>0)
				{
					DataGridUtils.SelectRow(dgPlan,rows[0]);
				}
			}
		}

		private void tabControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			OnTabChanged();
		}

		#region IScanHandler Members

		public void OnScanned(ScanData sd)
		{
			const string cmdText=@"
select BarCode.ProductCode, Product.Name as ProductName, BarCode.UnitCode, Unit.Name as UnitName, Coef from BarCode 
left join Product on Product.ProductCode=BarCode.ProductCode 
left join Unit on Unit.UnitCode=BarCode.UnitCode 
where BarCode.DeletedFlag=0 and Value=@Value";
			try
			{
				//				MessageBox.Show(string.Format("barCodeType='{0}'; barCode='{1}'",barCodeType,barCode));
				DataTable dt=new DataTable();
				using(GmConnection conn=App.ConnectionFactory.CreateConnection())
				{
					GmCommand cmd=conn.CreateCommand(cmdText);
					cmd.AddString("Value",sd.barCode);
					DbDataAdapter da=conn.CreateDataAdapter(cmd);
					da.Fill(dt);
				}
				switch(dt.Rows.Count)
				{
					case 0:
						Utils.MessageBoxExcl("Barcode product not found: "+sd.barCode);
						break;
					case 1:
						DataRow dr=dt.Rows[0];
						string productCode=dr[0].ToString();
						string productName=dr[1].ToString();
						string unitCode=dr[2].ToString();
						string unitName=dr[3].ToString();
						decimal coef=(decimal)dr[4];
						decimal count=1;
						AddRow(productCode,productName,unitCode,unitName,coef,count,App.Config.OpenCountFormAfterScan);
						break;
					default:
						Utils.MessageBoxExcl("Barcode multiple products found: "+sd.barCode);
						break;
				}
			}
			catch(Exception ex)
			{
				Log.Exception(ex);
			}
			finally
			{
			}
		}

		#endregion

		private void DocumentForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(discarded) return;
			if(doc.IsCompleted || Utils.Confirm("Postpone request?"))
			{
				App.ScanHandler=null;
				if(!doc.IsCompleted) this.Save();
			}
			else e.Cancel=true;
		}

		private void dgFact_CurrentCellChanged(object sender, System.EventArgs e)
		{
			DataGridUtils.SelectCurRow(dgFact);
			UpdateStatus();
		}

		private void dgPlan_CurrentCellChanged(object sender, System.EventArgs e)
		{
			DataGridUtils.SelectCurRow(dgPlan);
			UpdateStatus();
		}

		void UpdateStatus()
		{
			DataRow dr=null;
//			string contStr="";
			TabPage tp=SelectedPage;
			if(tp==this.tpFact)
			{
				dr=DataGridUtils.CurrentDataRow(this.dgFact);
			}
			else if(tp==this.tpPlan)
			{
				dr=DataGridUtils.CurrentDataRow(this.dgPlan);
			}
			if(dr!=null)
			{
				string productCode=dr["ProductCode"] as string;
//				lblRec.Text=string.Format("{{{0}/{1}}}{2}\r\n{3}",productCode,dr["Articul"],contStr,dr["ProductName"]);				
				lblRec.Text=string.Format("{0}",dr["ProductName"]);				
				this.lblNomVal.Text=string.Format("{0} �� {1}",GetCheckSum(dtFact,productCode),GetCheckSum(dtPlan,productCode));
				this.lblItogVal.Text=string.Format("{0} �� {1}",GetCheckSum(dtFact),GetCheckSum(dtPlan));
			}
			else
			{
				lblRec.Text="";
				this.lblItogVal.Text="0 of "+GetCheckSum(dtPlan);
				this.lblNomVal.Text="0 of 0";//+GetCheckSum(dtPlan,productCode);
			}
		}

		void OnKey(KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Escape:					
					this.Close();
					break;		
			}
					
			if(discarded) return;

			switch(e.KeyCode)
			{
				case Keys.E:
				  this.EmulateScan();
					break;				
				case Keys.Enter:
					this.EditCount();
					break;
				case Keys.D3:
					this.Save();
					break;
				case Keys.Home:
					this.Complete();
					break;
				default:
					if((int)e.KeyCode==190) this.ChangeHandledFlag();
					break;
			}

			FocusCurrentGrid();
		}
	
		private void DocumentForm_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}

		private void dgFact_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}

		private void dgPlan_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}

		private void tabControl_GotFocus(object sender, System.EventArgs e)
		{
			FocusCurrentGrid();
		}

		private void btnFill_Click(object sender, System.EventArgs e)
		{
			try
			{
				if(Utils.Confirm("Complete actual using planned values?"))
				{
					using(WaitCursor wc=new WaitCursor())
					{
						foreach(DataRow dr in this.dtPlan.Rows)
						{
							DocumentProduct dp=new DocumentProduct(dr);
							if(FindDataRow(dp.ProductCode,dp.UnitCode,dtFact)==null)
							{
								AddRow(dp.ProductCode,dp.ProductName,dp.UnitCode,dp.UnitName, dp.Coef, 0, false);
							}
						}
					}
				}
			}
			catch(Exception ex)
			{
				Log.Exception(ex);
			}
		}
	}
}
