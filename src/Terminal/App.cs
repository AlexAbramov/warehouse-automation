using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Geomethod.Windows.Forms;
using Geomethod.Data;
using Geomethod;
using Terminal.Forms;
using Terminal.Model;
using Terminal.API;

namespace Terminal
{
	public sealed class App: IDisposable
	{
		#region Static
		static App instance = null;
		static string baseDirectory=null;
		public static void Init(MainForm mainForm){new App(mainForm); }
		public static App Instance { get { if (instance == null) throw new TerminalException("Application not initialized."); return instance; } }
		public static string BaseDirectory{get{if(baseDirectory==null) baseDirectory=GetBaseDirectory(); return baseDirectory;}}// log ignored
		public static Config Config { get { return Instance.config; } }
		public static MainForm MainForm { get { return Instance.mainForm; } set { Instance.mainForm = value; } }
		public static ConnectionFactory ConnectionFactory { get { return Instance.connectionFactory; } }
		public static string ProductName { get { return "Terminal"; } }
		public static Engine Engine { get { return Instance.engine; } }
		public static int TerminalId { get { return Config.TerminalId; } }
		public static int UserId { get { return Instance.userId; } }
		public static IScanHandler ScanHandler{ get { return Instance.scanHandler; } set{lock(scanHandlerSyncRoot){Instance.scanHandler=value;}}}
		public static GmConnection CreateConnection() { return ConnectionFactory.CreateConnection(); }
		public static object ScanHandlerSyncRoot { get{return scanHandlerSyncRoot;} }
		internal static Queue scanQueue=new Queue();
		internal static bool ignoreScan=false;
		static object scanHandlerSyncRoot=new object();
		#endregion

		#region Fields
		Config config;
		MainForm mainForm;
		ConnectionFactory connectionFactory;
		Engine engine=new Engine();
		int userId=0;
		private DcdEvent dcdEvent;
		private DcdHandle hDcd;
		IScanHandler scanHandler=null;
		#endregion

		#region Construction
		App(MainForm mainForm)
		{
			if (App.instance != null) throw new TerminalException("Application already initialized.");
			App.instance = this;
			using(WaitCursor wc=new WaitCursor())
			{
				this.mainForm=mainForm;
				
				Log.log2=new FileLog(BaseDirectory+"Log.txt");
				Log.logInformer2=new MessageBoxLogInformer();
				Log.logInformer=new FormLogInformer();
				config = new Config(BaseDirectory+Config.configFileName);
				if(config.CS==1) return;
				connectionFactory=new ConnectionFactory(new SqlServerProviderFactory(), config.ConnectionString);

				connectionFactory.CommandTimeout=config.CommandTimeout;

				using(GmConnection conn=connectionFactory.CreateConnection())
				{
					conn.DbConnection.Open();// connection test
				}
				Log.log=new SqlLog(connectionFactory);
				Log.Info("Application started.","AppStarted");

				// Scanner
				InitScanner();
			}
			// Login
			if(Config.DebugMode>0 && System.Diagnostics.Debugger.IsAttached)// autologin
			{
//				Login("ivan","111");
				Login("01","1");
			}
			else
			{
				LoginForm form=new LoginForm(); form.ShowDialog();
			}
			if(IsMultyLoginDenied())
			{
				userId=0;
				Utils.MessageBoxExcl("Already authenticated.");
			}
		}

		public void InitScanner()
		{
			try
			{
				hDcd = new DcdHandle(DcdDeviceCap.Exists | DcdDeviceCap.Barcode);
			
				// Now that we've got a connection to a barcode reading device, assign a
				// method for the DcdEvent.  A recurring request is used so that we will
				// continue to get barcode data until our dialog is closed.
				DcdRequestType reqType = (DcdRequestType)1 | DcdRequestType.PostRecurring;

				// Initialize event
				dcdEvent = new DcdEvent(hDcd, reqType);
				dcdEvent.Scanned += new DcdScanned(dcdEvent_Scanned);
				Log.Info("Barcode scanner detected.","Scan");
			}
			catch(Exception ex)
			{
				TerminalException ex2=new TerminalException("Barcode scanner not detected.",ex);
				Log.Exception(ex2);
			}
		}

		public void Exit()
		{
			Log.Info("Application closed.","AppClosed");
			Log.Close();
		}
		#endregion

		static string GetBaseDirectory()// log ignored
		{
			string codeBase=System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
			return Path.GetDirectoryName(codeBase)+"\\";
		}

		public bool Login(string login, string psw)
		{
			using(GmConnection conn=App.ConnectionFactory.CreateConnection())
			{
				GmCommand cmd=conn.CreateCommand("select UserCode from [User] where Login=@Login and Password=@Password");
				
				cmd.AddString("Login",login);
				cmd.AddString("Password",psw);
				object obj=cmd.ExecuteScalar();
				if(obj is int)
				{
					userId=(int)obj;
				}
				else userId=0;
			}
			string msg=string.Format("User authorization '{0}': {1}",login, UserId);
			Log.Info(msg,"User");

			return UserId!=0;
		}

		public bool IsMultyLoginDenied()
		{
			bool denied=false;
			if(UserId!=0)
			{
				using(GmConnection conn=App.ConnectionFactory.CreateConnection())
				{
					GmCommand cmd=conn.CreateCommand("select MultiLoginFlag, lastupdate, GETDATE() from [User] where UserCode=@UserCode");
					cmd.AddInt("UserCode",UserId);
					using(IDataReader dr=cmd.ExecuteReader())
					{
						if(dr.Read())
						{
							int i=0;
							bool multiLoginFlag=dr.GetBoolean(i++);
							if(multiLoginFlag)
							{
								DateTime lastUpdate=dr.GetDateTime(i++);
								DateTime curTime=dr.GetDateTime(i++);
								TimeSpan ts=curTime-lastUpdate;
								denied= ts.TotalMilliseconds<this.config.PollTime*2;//+config.CommandTimeout*1000
							}
						}
					}
				}
			}
			return denied;
		}

		public void UpdateTerminalId()
		{
			using(GmConnection conn=App.ConnectionFactory.CreateConnection())
			{
				GmCommand cmd=conn.CreateCommand("update [User] set TerminalId=@TerminalId where UserCode=@UserId");

				cmd.AddInt("TerminalId",TerminalId);
				cmd.AddInt("UserId",UserId);
				cmd.ExecuteNonQuery();
			}
		}

		/// <summary>
		/// This method will be called when the DcdEvent is invoked.
		/// </summary>
		private void dcdEvent_Scanned(object sender, DcdEventArgs e)
		{
			if(App.ignoreScan || App.ScanHandler==null) return;
			CodeId cID = CodeId.NoData;
			string dcdData = string.Empty;

			// Obtain the string and code id.
			try
			{
				dcdData = hDcd.ReadString(e.RequestID, ref cID);
				lock(scanQueue.SyncRoot)
				{
					scanQueue.Enqueue(new ScanData(cID.ToString(),dcdData));
				}
			}
			catch(Exception ex)
			{
				Log.Exception(ex);
			}
		}

		#region IDisposable Members

		public void Dispose()
		{
			if(dcdEvent!=null)
			{
				dcdEvent.Dispose();
				dcdEvent=null;
			}
			if(hDcd!=null)
			{
				hDcd.Dispose();
				hDcd=null;
			}
		}

		#endregion
	}

	public class TerminalException : Exception
	{
		public TerminalException(string msg)
			: base(msg)
		{
		}
		public TerminalException(string msg, Exception innerException)
			: base(msg, innerException)
		{
		}
	}

	public struct ScanData
	{
		public string barCodeType;
		public string barCode;
		public ScanData(string barCodeType, string barCode)
		{
			this.barCodeType=barCodeType;
			this.barCode=barCode;
		}
	}

	public interface IScanHandler
	{
		void OnScanned(ScanData sd);
	}
}
