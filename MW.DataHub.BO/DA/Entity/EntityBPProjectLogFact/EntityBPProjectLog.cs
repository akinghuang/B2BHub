using System;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.BO.Entity
{
	[Serializable]
	public partial class EntityBPProjectLog
	{
		public EntityBPProjectLog(){}

		private System.Int32 _IDField=-1;
		public System.Int32 ID
		{
			get { return this._IDField; }
			set { this._IDField=value; }
		}
		private System.DateTime _LogTimeField;
		public System.DateTime LogTime
		{
			get { return this._LogTimeField; }
			set { this._LogTimeField=value; }
		}
		private System.String _LogTypeField;
		public System.String LogType
		{
			get { return this._LogTypeField; }
			set { this._LogTypeField=value; }
		}
		private System.String _KeyValueField;
		public System.String KeyValue
		{
			get { return this._KeyValueField; }
			set { this._KeyValueField=value; }
		}
		private System.String _LogField;
		public System.String Log
		{
			get { return this._LogField; }
			set { this._LogField=value; }
		}
		private System.String _SourceField;
		public System.String Source
		{
			get { return this._SourceField; }
			set { this._SourceField=value; }
		}
		private System.Int32 _ProjectIDField;
		public System.Int32 ProjectID
		{
			get { return this._ProjectIDField; }
			set { this._ProjectIDField=value; }
		}
	}
}
