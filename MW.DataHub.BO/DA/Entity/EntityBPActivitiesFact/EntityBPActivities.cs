using System;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.BO.Entity
{
	[Serializable]
	public partial class EntityBPActivities
	{
		public EntityBPActivities(){}

		private System.Int32 _IDField=-1;
		public System.Int32 ID
		{
			get { return this._IDField; }
			set { this._IDField=value; }
		}
		private System.Int32 _ProjectIDField;
		public System.Int32 ProjectID
		{
			get { return this._ProjectIDField; }
			set { this._ProjectIDField=value; }
		}
		private System.Int32 _TaskIDField;
		public System.Int32 TaskID
		{
			get { return this._TaskIDField; }
			set { this._TaskIDField=value; }
		}
		private System.String _StatusField;
		public System.String Status
		{
			get { return this._StatusField; }
			set { this._StatusField=value; }
		}
		private System.DateTime _LastRunStartDTField;
		public System.DateTime LastRunStartDT
		{
			get { return this._LastRunStartDTField; }
			set { this._LastRunStartDTField=value; }
		}
		private System.DateTime _LastRunEndDTField;
		public System.DateTime LastRunEndDT
		{
			get { return this._LastRunEndDTField; }
			set { this._LastRunEndDTField=value; }
		}
		private Nullable<System.Boolean> _LastRunStatusField;
		public Nullable<System.Boolean> LastRunStatus
		{
			get { return this._LastRunStatusField; }
			set { this._LastRunStatusField=value; }
		}
		private System.String _LastRunResultField;
		public System.String LastRunResult
		{
			get { return this._LastRunResultField; }
			set { this._LastRunResultField=value; }
		}
		private Nullable<System.DateTime> _LastSuccessDTField;
		public Nullable<System.DateTime> LastSuccessDT
		{
			get { return this._LastSuccessDTField; }
			set { this._LastSuccessDTField=value; }
		}
		private Nullable<System.Int32> _RunFailTimesField;
		public Nullable<System.Int32> RunFailTimes
		{
			get { return this._RunFailTimesField; }
			set { this._RunFailTimesField=value; }
		}
	}
}
