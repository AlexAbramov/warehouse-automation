using System;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Terminal.Model
{
	/// <summary>
	/// Summary description for Product.
	/// </summary>
	public class Product
	{
		Product parent=null;
		ArrayList products=null;
		string code;
		string name;
		string parentCode=null;
		
		public Product Parent{get{return parent;}}
		public IEnumerable Products{get{return products;}}
		public string Code{get{return code;}}
		public string Name{get{return name;}}
		public string ParentCode{get{return parentCode;}}

		public Product(IDataReader dr)
		{
			int i=0;
			code=dr.GetString(i++);
			name=dr.GetString(i++);
			if(!dr.IsDBNull(i)) parentCode=dr.GetString(i++);
		}

		public Product(string code, string name)
		{
			this.code=code;
			this.name=name;
		}

		internal void SetParent(Product p)
		{
			if(p.products==null) p.products=new ArrayList();
			p.products.Add(this);
			parent=p;
		}
	}
}
