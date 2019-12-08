using System;
using System.Data.SqlClient;
using System.Collections;
using System.Text;
using System.IO;
using Terminal;

namespace Geomethod
{
	public enum LogType{Info,Warning,Error,Exception,Custom};

	public interface ILog 
	{
		void Write(LogType logType, string subType, string msg);
		void Close();
	}

	public interface ILogInformer 
	{
		void Show(LogType logType, string subType, string msg);
	}

	public class Log
	{
		#region Fields
		public static ILog log=null;
		public static ILog log2=null;
		public static ILogInformer logInformer=null;
		public static ILogInformer logInformer2=null;
		static bool[] displayed={false,false,true,true,false};
		#endregion

		#region Settings
		public static bool IsDisplayed(LogType logType){return displayed[(int)logType];}
		public static void SetDisplayed(LogType logType,bool displayed){Log.displayed[(int)logType]=displayed; }
		#endregion

		#region Construction
		public static void Close()
		{
			if (log != null)
			{
				try
				{
					log.Close();
				}
				catch
				{
				}
				log = null;
			}
			if (log2 != null)
			{
				try
				{
					log2.Close();
				}
				catch
				{
				}
				log2 = null;
			}
			if (logInformer != null) { logInformer = null; }
			if (logInformer2 != null) { logInformer2 = null; }
		}
		#endregion

		#region Logging
		public static void Info(string msg,string subType){Put(LogType.Info,subType,msg);}
		public static void Warning(string msg,string subType){Put(LogType.Warning,subType,msg);}
		public static void Error(string msg,string subType){Put(LogType.Error,subType,msg);}
		public static void Exception(Exception ex){Put(LogType.Exception,ex.GetType().Name,ExToString(ex));}
		public static void Custom(string subType, string msg){Put(LogType.Custom,subType,msg);}
		public static void Put(LogType logType, string subType, string msg)
		{
			Write(logType,subType,msg);
			if(IsDisplayed(logType))
			{
				Show(logType,subType,msg);
			}
		}
		static void Write(LogType logType, string subType, string msg)
		{
			ILog l=log!=null? log : log2;
			ILogInformer li=logInformer!=null? logInformer : logInformer2;
			if(l!=null)
			{
				try
				{
					l.Write(logType, subType, msg);
				}
				catch(Exception ex)
				{
					if(l==log && log2!=null)
					{
						try
						{
							log2.Write(logType, subType, msg);
							log2.Write(LogType.Exception,ex.GetType().Name,ExToString(ex));
							if(IsDisplayed(LogType.Exception))
							{
								Show(LogType.Exception,ex.GetType().Name,ExToString(ex));
							}
						}
						catch
						{
						}
					}
				}
			}
		}
		static void Show(LogType logType, string subType, string msg)
		{
			ILogInformer li=logInformer!=null? logInformer : logInformer2;
			if(li!=null)
			{
				li.Show(logType,subType,msg);
			}
		}
		#endregion

		#region Aux
		public static string ExToString(Exception ex)
		{
			string s=ex.ToString();
			if(ex is SqlException)
			{
				s="Connection problem. Please contact system administrator.";
				SqlException sqlEx=(SqlException)ex;
				s+=SqlErrorsToString(sqlEx);
			}
			if(ex.InnerException!=null)
			{
				s+="\r\nInnerException: "+ex.InnerException.ToString(); 
			}
			return s;
		}
		static string SqlErrorsToString(SqlException ex)
		{
			if(ex.Errors.Count==0) return "";
			StringBuilder sb=new StringBuilder(512);
			sb.Append("\r\nDetails:\r\n");
			foreach (SqlError er in ex.Errors)
			{
				sb.Append(
					" Message: " + er.Message +
					" Server: " + er.Server +
					" Procedure: " + er.Procedure +
					" LineNumber: " + er.LineNumber.ToString()+
					" Source: " + er.Source + 
					" Number: " + er.Number.ToString() +
					" State: " + er.State.ToString() +
					" Class: " + er.Class.ToString() +
					";");
			}			
			return sb.ToString();
		}
		#endregion
	}
}
