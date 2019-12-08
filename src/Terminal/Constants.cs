using System;

namespace Terminal
{
	public enum DocumentType{RTO=1,PTO=2,ITO=3}
	public enum DocumentState{New=1,Recorded=2,Complected=3,Discarded=4,Removed=5,Sent=6}

	public class Constants
	{
		public const int targetColumnsWidth=185;
		public const int minColWidth=20;
	}

	public class FieldLength
	{
		public const int subType=50;
		public const int message=4000;
	}	
}
