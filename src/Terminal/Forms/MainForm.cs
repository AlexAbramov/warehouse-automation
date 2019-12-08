using System;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using Geomethod;
using Geomethod.Data;
using Geomethod.Windows.Forms;
using System.IO;
using Terminal.Model;


namespace Terminal.Forms
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{	//Config conf=new Config("Terminal.config.txt");
		static int timeleft=0;
		static bool ignoreClosingHandler=false;

		DataTable dtDocuments=null;
		DataTable dtDocumentsUpdated=new DataTable("Documents");


		private System.Windows.Forms.DataGridTextBoxColumn colNumber;
		private System.Windows.Forms.DataGrid dataGrid;
		private System.Windows.Forms.DataGridTextBoxColumn colUrgentFlag;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle;
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolBarButton tbbOpen;
		private System.Windows.Forms.DataGridTextBoxColumn colDate;
		private System.Windows.Forms.DataGridTextBoxColumn colDocumentType;
		private System.Windows.Forms.DataGridTextBoxColumn colDocumentState;
		private System.Windows.Forms.ToolBarButton tbbAbout;
		private System.Windows.Forms.DataGridTextBoxColumn dcTruckNumber;
		private System.Windows.Forms.DataGridTextBoxColumn dcContractorName;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButton2;
		private System.Windows.Forms.ToolBarButton tbbSortByDate;
		private System.Windows.Forms.ToolBarButton tbbSortByCar;
		private System.Windows.Forms.ToolBarButton tbbSortByAgent;
		private System.Windows.Forms.Label lblMakeOfCar;
		private System.Windows.Forms.Label lblTruckNumber;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label lblOrderToStore;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lblContractorName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Timer timer;
	
		public MainForm()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.timer = new System.Windows.Forms.Timer();
			this.dataGrid = new System.Windows.Forms.DataGrid();
			this.dataGridTableStyle = new System.Windows.Forms.DataGridTableStyle();
			this.colDate = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colDocumentType = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colUrgentFlag = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colNumber = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dcTruckNumber = new System.Windows.Forms.DataGridTextBoxColumn();
			this.colDocumentState = new System.Windows.Forms.DataGridTextBoxColumn();
			this.dcContractorName = new System.Windows.Forms.DataGridTextBoxColumn();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.tbbSortByDate = new System.Windows.Forms.ToolBarButton();
			this.tbbSortByCar = new System.Windows.Forms.ToolBarButton();
			this.tbbSortByAgent = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.tbbOpen = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton2 = new System.Windows.Forms.ToolBarButton();
			this.tbbAbout = new System.Windows.Forms.ToolBarButton();
			this.imageList = new System.Windows.Forms.ImageList();
			this.lblMakeOfCar = new System.Windows.Forms.Label();
			this.lblTruckNumber = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.lblOrderToStore = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblContractorName = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			// 
			// timer
			// 
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// dataGrid
			// 
			this.dataGrid.Location = new System.Drawing.Point(0, 28);
			this.dataGrid.Size = new System.Drawing.Size(238, 168);
			this.dataGrid.TableStyles.Add(this.dataGridTableStyle);
			this.dataGrid.Click += new System.EventHandler(this.dataGrid_Click);
			this.dataGrid.CurrentCellChanged += new System.EventHandler(this.dataGrid_CurrentCellChanged);
			this.dataGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGrid_KeyDown);
			this.dataGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGrid_MouseDown);
			// 
			// dataGridTableStyle
			// 
			this.dataGridTableStyle.GridColumnStyles.Add(this.colDate);
			this.dataGridTableStyle.GridColumnStyles.Add(this.colDocumentType);
			this.dataGridTableStyle.GridColumnStyles.Add(this.colUrgentFlag);
			this.dataGridTableStyle.GridColumnStyles.Add(this.colNumber);
			this.dataGridTableStyle.GridColumnStyles.Add(this.dcTruckNumber);
			this.dataGridTableStyle.GridColumnStyles.Add(this.colDocumentState);
			this.dataGridTableStyle.MappingName = "Documents";
			// 
			// colDate
			// 
			this.colDate.HeaderText = "Date";
			this.colDate.MappingName = "DateStr";
			this.colDate.NullText = "(null)";
			this.colDate.Width = 46;
			// 
			// colDocumentType
			// 
			this.colDocumentType.HeaderText = "Type";
			this.colDocumentType.MappingName = "DocumentTypeShortName";
			this.colDocumentType.NullText = "(null)";
			this.colDocumentType.Width = 11;
			// 
			// colUrgentFlag
			// 
			this.colUrgentFlag.HeaderText = "Urgent";
			this.colUrgentFlag.MappingName = "UrgentFlagStr";
			this.colUrgentFlag.NullText = "(null)";
			this.colUrgentFlag.Width = 10;
			// 
			// colNumber
			// 
			this.colNumber.HeaderText = "DocID";
			this.colNumber.MappingName = "Number";
			this.colNumber.NullText = "(null)";
			this.colNumber.Width = 55;
			// 
			// dcTruckNumber
			// 
			this.dcTruckNumber.HeaderText = "Truck ID";
			this.dcTruckNumber.MappingName = "TruckNumber";
			this.dcTruckNumber.NullText = "(null)";
			this.dcTruckNumber.Width = 55;
			// 
			// colDocumentState
			// 
			this.colDocumentState.MappingName = "DocumentStateShortName";
			this.colDocumentState.NullText = "(null)";
			this.colDocumentState.Width = 10;
			// 
			// dcContractorName
			// 
			this.dcContractorName.NullText = "(null)";
			// 
			// toolBar
			// 
			this.toolBar.Buttons.Add(this.tbbSortByDate);
			this.toolBar.Buttons.Add(this.tbbSortByCar);
			this.toolBar.Buttons.Add(this.tbbSortByAgent);
			this.toolBar.Buttons.Add(this.toolBarButton1);
			this.toolBar.Buttons.Add(this.tbbOpen);
			this.toolBar.Buttons.Add(this.toolBarButton2);
			this.toolBar.Buttons.Add(this.tbbAbout);
			this.toolBar.ImageList = this.imageList;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// tbbSortByDate
			// 
			this.tbbSortByDate.ImageIndex = 2;
			// 
			// tbbSortByCar
			// 
			this.tbbSortByCar.ImageIndex = 3;
			// 
			// tbbSortByAgent
			// 
			this.tbbSortByAgent.ImageIndex = 4;
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbOpen
			// 
			this.tbbOpen.ImageIndex = 0;
			// 
			// toolBarButton2
			// 
			this.toolBarButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbbAbout
			// 
			this.tbbAbout.ImageIndex = 1;
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
			// lblMakeOfCar
			// 
			this.lblMakeOfCar.Location = new System.Drawing.Point(48, 236);
			this.lblMakeOfCar.Size = new System.Drawing.Size(72, 16);
			// 
			// lblTruckNumber
			// 
			this.lblTruckNumber.Location = new System.Drawing.Point(172, 236);
			this.lblTruckNumber.Size = new System.Drawing.Size(64, 16);
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label11.Location = new System.Drawing.Point(2, 236);
			this.label11.Size = new System.Drawing.Size(44, 16);
			this.label11.Text = "Make:";
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label13.Location = new System.Drawing.Point(122, 236);
			this.label13.Size = new System.Drawing.Size(48, 16);
			this.label13.Text = "Tag:";
			// 
			// lblOrderToStore
			// 
			this.lblOrderToStore.Location = new System.Drawing.Point(62, 198);
			this.lblOrderToStore.Size = new System.Drawing.Size(174, 35);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label7.Location = new System.Drawing.Point(2, 198);
			this.label7.Size = new System.Drawing.Size(57, 16);
			this.label7.Text = "Task:";
			// 
			// lblContractorName
			// 
			this.lblContractorName.Location = new System.Drawing.Point(80, 254);
			this.lblContractorName.Size = new System.Drawing.Size(156, 16);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
			this.label5.Location = new System.Drawing.Point(2, 254);
			this.label5.Size = new System.Drawing.Size(75, 16);
			this.label5.Text = "Counterpart:";
			// 
			// MainForm
			// 
			this.ClientSize = new System.Drawing.Size(238, 469);
			this.Controls.Add(this.lblContractorName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.lblMakeOfCar);
			this.Controls.Add(this.lblTruckNumber);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.lblOrderToStore);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.dataGrid);
			this.Controls.Add(this.toolBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Text = "Terminal";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.MainForm_Closing);
			this.Load += new System.EventHandler(this.MainForm_Load);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			Utils.OnFormLoad(this);
			App.Init(this);
			if(App.UserId==0)
			{
				ignoreClosingHandler=true;
				Close();
				return;
			}
			dtDocumentsUpdated.Columns.Add("DateStr",typeof(string));
			dtDocumentsUpdated.Columns.Add("UrgentFlagStr",typeof(string));

			this.dataGrid.Focus();
			timer.Interval=App.Config.SyncTime;
			timer.Enabled=true;
		}

		void SyncData()
		{
			ArrayList scanList=new ArrayList();
			lock(App.scanQueue.SyncRoot)
			{
				while(App.scanQueue.Count>0)
				{
					scanList.Add(App.scanQueue.Dequeue());
				}
			}
			if(scanList.Count>0) 
			{
				lock(App.ScanHandlerSyncRoot)// lock ScanHandler set access
				{
					if(App.ScanHandler!=null)
					{
						try
						{
							App.ignoreScan=true;// ignore scan while processing messages
							foreach(ScanData sd in scanList)
							{
								App.ScanHandler.OnScanned(sd);// process messages
							}
						}
						finally
						{
							App.ignoreScan=false;
						}
					}
				}
			}
		}

		private void timer_Tick(object sender, System.EventArgs e)
		{
			OnTick();
		}

		void OnTick()
		{
			try
			{
				if (timeleft>=0)
				{	
					timeleft-=App.Config.PollTime;
					App.Instance.UpdateTerminalId();
					UpdateDocuments();				
				}
							
				timeleft+=this.timer.Interval;
				SyncData();				
			}
			catch(Exception ex)
			{
				timer.Enabled=false;
				Log.Exception(ex);
				ignoreClosingHandler=true;
				Close();
			}
		}

		void UpdateControls()
		{
			int curDocId=CurrentDocumentId;
			tbbOpen.Enabled=curDocId!=0;
		}

		void Sort(ToolBarButton tbb)
		{
			string sort="";
			if(tbb==this.tbbSortByDate) sort="Date";
			else if(tbb==this.tbbSortByCar) sort="TruckNumber";
			else if(tbb==this.tbbSortByAgent) sort="ContractorName";
			else return;

			this.dtDocuments.DefaultView.Sort=sort;
			this.tbbSortByDate.Pushed=false;
			this.tbbSortByCar.Pushed=false;
			this.tbbSortByAgent.Pushed=false;
			tbb.Pushed=true;
			DataGridUtils.SelectCurRow(dataGrid);
			UpdateInfo();
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if(e.Button==tbbOpen) 
			{
				OpenDocument();
			}
			else if (e.Button==tbbAbout)
			{
				AboutForm about=new AboutForm();
				about.ShowDialog();
			}
			else Sort(e.Button);

			this.dataGrid.Focus();
		}

		void OpenDocument()
		{
			try
			{
				DataRow dr=CurrentDataRow;
				if(dr!=null)
				{
					DataRow drClone=this.dtDocuments.NewRow();
					drClone.ItemArray=(object[])dr.ItemArray.Clone();
					Document doc=new Document(drClone);
					DocumentForm form=new DocumentForm(doc);
					form.ShowDialog();
					timeleft=0;
				}
			}
			catch(Exception ex)
			{
				App.ScanHandler=null;
				Log.Exception(ex);
			}
			finally
			{
				App.ScanHandler=null;
			}
		}

		DataRow CurrentDataRow
		{
			get
			{
				if(dtDocuments==null || dtDocuments.Columns.IndexOf("DocumentId")<0) return null;
				return DataGridUtils.CurrentDataRow(this.dataGrid);
			}
		}

		int CurrentDocumentId
		{
			get
			{
				DataRow dr=CurrentDataRow;
  			return dr!=null ? (int)dr["DocumentId"] : 0;
			}
			set
			{
				if(dtDocuments!=null)
				{
					for(int i=0; i<dtDocuments.Rows.Count;i++)
					{
						DataRow dr=dtDocuments.Rows[i];
						int docId=(int)dr["DocumentId"];
						if(docId==value)
						{
							dataGrid.CurrentRowIndex=i;
							break;
						}
					}
				}
			}
		}

		void UpdateDocuments()
		{
			int docId=this.CurrentDocumentId;			
			Document.UpdateDocuments(this.dtDocumentsUpdated);
			bool newDoc,urgent;
			if(AreDocumentsChanged(out newDoc,out urgent))
			{
				bool initialLoad=this.dtDocuments==null;
				if(initialLoad)
				{
					dtDocuments=DataTableUtils.Clone(dtDocumentsUpdated);
					Sort(this.tbbSortByDate);
				}
				else DataTableUtils.CopyRows(dtDocumentsUpdated,dtDocuments);
				dataGrid.DataSource=this.dtDocuments;
				CurrentDocumentId=docId;
				DataGridUtils.SelectCurRow(dataGrid);
				UpdateInfo();
				UpdateControls();
				if(!initialLoad)
				{
					if(urgent)
					{
						Utils.MessageBoxExcl("Urgent request received.");
						//					Microsoft.VisualBasic.Interaction.Beep();
					}
					else if(newDoc)
					{
						Utils.MessageBoxExcl("New request received.");
					}
					if(App.ScanHandler!=null)
					{
						DocumentForm form=App.ScanHandler as DocumentForm;
						if(form!=null)
						{
							DocumentState docStateId=(DocumentState)form.Document.DocumentStateId;
							if(docStateId==DocumentState.Discarded||docStateId==DocumentState.Removed)
							{
								int openedDocId=form.Document.DocumentId;
								bool hasOpenedDoc=false;
								foreach(DataRow dr in dtDocuments.Rows)
								{
									Document doc=new Document(dr);
									if(doc.DocumentId==openedDocId)
									{
										hasOpenedDoc=true;
										break;
									}
								}
								if(!hasOpenedDoc)
								{
									Utils.MessageBoxExcl("Request canceled. Please close the form.");
									form.Discard();
								}
							}
						}
					}
				}
			}
		}

		bool AreDocumentsChanged(out bool newDoc, out bool urgent)
		{
			newDoc=false;
			urgent=false;
			bool changed=false;
			if(dtDocuments==null || dtDocumentsUpdated.Columns.Count!=dtDocuments.Columns.Count)
			{
				newDoc=dtDocumentsUpdated.Rows.Count>0;
				return true;
			}
			if(dtDocumentsUpdated.Rows.Count!=dtDocuments.Rows.Count)
			{
				changed=true;
			}
			Hashtable ht=new Hashtable();
			foreach(DataRow dr in dtDocuments.Rows)
			{
				Document doc=new Document(dr);
				ht.Add(doc.DocumentId, doc);
			}
			foreach(DataRow dr in dtDocumentsUpdated.Rows)
			{
				Document doc=new Document(dr);
				if(ht.ContainsKey(doc.DocumentId))
				{
					Document doc0=(Document)ht[doc.DocumentId];
					if(doc.DocumentStateId!=doc0.DocumentStateId)
					{
						changed=true;
					}
					if(doc.UrgentFlag!=doc0.UrgentFlag)
					{
						changed=true;
						if(doc.UrgentFlag) urgent=true;
					}
				}
				else
				{
					newDoc=true;
					changed=true;
					if(doc.UrgentFlag) urgent=true;
				}
			}
			return changed;
		}

		private void dataGrid_CurrentCellChanged(object sender, System.EventArgs e)
		{
			DataGridUtils.SelectCurRow(dataGrid);
			UpdateInfo();
		}

		void UpdateInfo()
		{
			DataRow dr=this.CurrentDataRow;
			if(dr!=null)
			{
				Document doc=new Document(dr);
				this.lblOrderToStore.Text=doc.OrderToStore;
				this.lblMakeOfCar.Text=doc.MakeOfCar;
				this.lblTruckNumber.Text=doc.TruckNumber;
				this.lblContractorName.Text=doc.ContractorName;
			}
			else
			{
				this.lblOrderToStore.Text="";
				this.lblMakeOfCar.Text="";
				this.lblTruckNumber.Text="";
				this.lblContractorName.Text="";
				//				lblInfo.Text="";
			}
		}

		private void dataGrid_Click(object sender, System.EventArgs e)
		{
		}

		private void dataGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(e.Button==MouseButtons.Left)
			{
			}
		}

		private void dataGrid_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			OnKey(e);
		}

		void OnKey(KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter:
					this.OpenDocument();
					break;
				case Keys.Escape:
					Close();
					break;
			}
			this.dataGrid.Focus();
		}

		private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(ignoreClosingHandler) return;
			if(Utils.Confirm("Are you sure you want to exit?"))
			{
				this.timer.Enabled=false;
				this.timer.Dispose();
				timer=null;
				if(App.Instance!=null) App.Instance.Dispose();
			}
			else e.Cancel=true;
		}
	}
}
