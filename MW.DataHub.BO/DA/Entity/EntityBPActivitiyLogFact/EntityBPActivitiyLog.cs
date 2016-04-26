using System;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.BO.Entity
{
	[Serializable]
	public partial class EntityBPActivitiyLog
	{
		public EntityBPActivitiyLog(){}

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
		private Nullable<System.DateTime> _RunStartDTField;
		public Nullable<System.DateTime> RunStartDT
		{
			get { return this._RunStartDTField; }
			set { this._RunStartDTField=value; }
		}
		private Nullable<System.DateTime> _RunEndDTField;
		public Nullable<System.DateTime> RunEndDT
		{
			get { return this._RunEndDTField; }
			set { this._RunEndDTField=value; }
		}
		private Nullable<System.Boolean> _RunStatusField;
		public Nullable<System.Boolean> RunStatus
		{
			get { return this._RunStatusField; }
			set { this._RunStatusField=value; }
		}
		private System.String _RunResultField;
		public System.String RunResult
		{
			get { return this._RunResultField; }
			set { this._RunResultField=value; }
		}
	}
}
