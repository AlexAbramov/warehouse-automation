using System;
using System.Data;
using System.Data.Common;
using Geomethod.Data;

namespace Terminal.Model
{
	/// <summary>
	/// Summary description for Document.
	/// </summary>
	public class DocumentProduct
	{
		const string selectCmdText=@"
select DocumentProductId,DocumentProduct.ProductCode, Product.Name as ProductName, DocumentProduct.UnitCode, Unit.Name as UnitName, DocumentProduct.Coef, [Count], Articul, HandledFlag
from DocumentProduct
left join Product on DocumentProduct.ProductCode=Product.ProductCode
left join Unit on DocumentProduct.UnitCode=Unit.UnitCode
where DocumentId=@DocumentId and FactFlag=@FactFlag
order by Product.Name";

		const string insertCmdText=@"
insert into DocumentProduct
(ProductCode, UnitCode, DocumentId, Coef, [Count], FactFlag, HandledFlag)
values 
(@ProductCode, @UnitCode, @DocumentId, @Coef, @Count, @FactFlag, @HandledFlag); SELECT SCOPE_IDENTITY() as 'DocumentProductId'";
			//select cast(@@Identity as int) as 'DocumentProductId'";

		const string deleteCmdText=@"
delete DocumentProduct
where DocumentProductId=@DocumentProductId";

		const string updateCmdText=@"
update DocumentProduct
set [Count]=@Count, HandledFlag=@HandledFlag
where DocumentProductId=@DocumentProductId";

		public static DbDataAdapter CreateDataAdapter(GmConnection conn,Document doc)
		{
			DbDataAdapter da=conn.CreateDataAdapter();

			GmCommand cmd = conn.CreateCommand(selectCmdText);
			cmd.AddInt("DocumentId", doc.DocumentId);
			cmd.AddBool("FactFlag", false);
			(da as IDbDataAdapter).SelectCommand=cmd.DbCommand;
			
			cmd = conn.CreateCommand(insertCmdText);
			cmd.AddString("ProductCode").SourceColumn="ProductCode";
			cmd.AddString("UnitCode").SourceColumn="UnitCode";
			cmd.AddInt("DocumentId", doc.DocumentId);
			cmd.AddDecimal("Coef").SourceColumn="Coef";
			cmd.AddDecimal("Count").SourceColumn="Count";
			cmd.AddBool("FactFlag", true);
			cmd.AddBool("HandledFlag").SourceColumn="HandledFlag";
			cmd.DbCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
			(da as IDbDataAdapter).InsertCommand=cmd.DbCommand;

			cmd = conn.CreateCommand(deleteCmdText);
			cmd.AddInt("DocumentProductId").SourceColumn="DocumentProductId";
			(da as IDbDataAdapter).DeleteCommand=cmd.DbCommand;

			cmd = conn.CreateCommand(updateCmdText);
			cmd.AddInt("DocumentProductId").SourceColumn="DocumentProductId";
			cmd.AddDecimal("Count").SourceColumn="Count";
			cmd.AddBool("HandledFlag").SourceColumn="HandledFlag";
			(da as IDbDataAdapter).UpdateCommand=cmd.DbCommand;

			return da;
		}

		DataRow dr;//DocumentProductId, ProductCode, ProductName, UnitCode, UnitName, Coef, Count, Articul
		public DocumentProduct(DataRow dr)
		{
			this.dr=dr;
		}

		public DataRow DataRow{get{return dr;}}
		public int DocumentProductId{get{return (int)dr["DocumentProductId"];}set{dr["DocumentProductId"]=value;}}
		public int DocumentId{get{return (int)dr["DocumentId"];}set{dr["DocumentId"]=value;}}
		public string ProductCode{get{return (string)dr["ProductCode"];}set{dr["ProductCode"]=value;}}
		public string ProductName{get{return (string)dr["ProductName"];}set{dr["ProductName"]=value;}}
		public string UnitCode{get{return (string)dr["UnitCode"];}set{dr["UnitCode"]=value;}}
		public string UnitName{get{return (string)dr["UnitName"];}set{dr["UnitName"]=value;}}
		public decimal Coef{get{return (decimal)dr["Coef"];}set{dr["Coef"]=value;}}
		public decimal Count{get{return (decimal)dr["Count"];} set{dr["Count"]=value;}}
		public bool HandledFlag{get{return (bool)dr["HandledFlag"];} set{dr["HandledFlag"]=value;}}
		public string Articul{get{return (string)dr["Articul"];} set{dr["Articul"]=value;}}
	}
}
