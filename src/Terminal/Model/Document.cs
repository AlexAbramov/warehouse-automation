using System;
using System.Data;
using System.Data.Common;
using Geomethod.Data;

namespace Terminal.Model
{
	/// <summary>
	/// Summary description for Document.
	/// </summary>
	public class Document
	{
		const string cmdText=@"
select DocumentId, Date, DocumentType.ShortName as DocumentTypeShortName, Number, UrgentFlag, DocumentState.ShortName as DocumentStateShortName, Document.DocumentStateId, Document.DocumentTypeId, ContractorName, TruckNumber, Container, SpoilageFlag, MakeOfCar, OrderToStore, StoreSection.Name as StoreSectionName, StoreName, ProductQuality.Name as ProductQualityName
from [Document] 
left join DocumentType on Document.DocumentTypeId=DocumentType.DocumentTypeId
left join DocumentState on Document.DocumentStateId=DocumentState.DocumentStateId
left join StoreSection on Document.StoreSectionCode=StoreSection.StoreSectionCode
left join ProductQuality on Document.ProductQualityCode=ProductQuality.ProductQualityCode
where Document.DeletedFlag=0 and UserCode=@UserId and Document.DocumentStateId in (1,2) and ProductQuality.DeletedFlag=0
";
		public static void UpdateDocuments(DataTable dt)
		{
			dt.Rows.Clear();
			using(GmConnection conn=App.ConnectionFactory.CreateConnection())
			{
				GmCommand cmd = conn.CreateCommand(cmdText);
				cmd.AddInt("UserId", App.UserId);
				DbDataAdapter da=conn.CreateDataAdapter(cmd);
				da.Fill(dt);
							//	obj=conn.ExecuteScalar("select count(*) from [Log]");
			}

			foreach(DataRow dr in dt.Rows)
			{
//				dr["DateStr"]=string.Format("{0:dd/MM/yy hh:mm}",dr["Date"]);
				dr["DateStr"]=string.Format("{0:dd/MM/yy}",dr["Date"]);
				dr["UrgentFlagStr"]=(bool)dr["UrgentFlag"]?"+":"";
				dr["DocumentTypeShortName"]=dr["DocumentTypeShortName"].ToString().Substring(0,1);
				dr["DocumentStateShortName"]=(int)dr["DocumentStateId"]==(int)DocumentState.Recorded?"#":"";
			}
		}

		DataRow dr;//DocumentId, Date, DocumentType.ShortName as DocumentTypeShortName, Number, UrgentFlag, DocumentState.ShortName as DocumentStateShortName, Document.DocumentStateId
		public Document(DataRow dr)
		{
      this.dr=dr;
		}
		string GetString(DataRow dr,string colName){return dr.IsNull(colName)?"":(string)dr[colName];}

		public bool IsITO{get{return DocumentTypeId==(int)DocumentType.ITO;}}
		public int DocumentId{get{return (int)dr["DocumentId"];}}
		public int DocumentTypeId{get{return (int)dr["DocumentTypeId"];}}
		public int DocumentStateId{get{return (int)dr["DocumentStateId"];}set{dr["DocumentStateId"]=value;}}
		public string Number{get{return (string)dr["Number"];}}
		public string DocumentTypeShortName{get{return dr["DocumentTypeShortName"].ToString();}}
		public bool UrgentFlag{get{return (bool)dr["UrgentFlag"];}}
		public DateTime Date{get{return (DateTime)dr["Date"];}}
		public bool Saved{get{return DocumentStateId==(int)DocumentState.Recorded;}}
		public string ContractorName{get{return GetString(dr,"ContractorName");}}
		public string TruckNumber{get{return GetString(dr,"TruckNumber");}}
		public string Container{get{return GetString(dr,"Container");}}
		public string OrderToStore{get{return GetString(dr,"OrderToStore");}}
		public string MakeOfCar{get{return GetString(dr,"MakeOfCar");}}
		public bool IsCompleted{get {return DocumentStateId==(int)DocumentState.Complected;}}
		public bool SpoilageFlag{get{return GetBool(dr,"SpoilageFlag");}}
		bool GetBool(DataRow dr,string colName){return dr.IsNull(colName)?false:(bool)dr[colName];}
		public string StoreSectionName{get{return GetString(dr,"StoreSectionName");}}
		public string StoreName{get{return GetString(dr,"StoreName");}}
		public string ProductQualityName{get{return GetString(dr,"ProductQualityName");}}
		public void UpdateState(GmConnection conn)
		{
			GmCommand cmd=conn.CreateCommand("update Document set DocumentStateId=@DocumentStateId where DocumentId=@DocumentId");
			cmd.AddInt("DocumentId",DocumentId);
			cmd.AddInt("DocumentStateId",DocumentStateId);
			cmd.ExecuteNonQuery();
		}
	}
}
