using System;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.BO.Entity
{
	[Serializable]
	public partial class EntityBPProjectNtf
	{
		public EntityBPProjectNtf(){}

		private System.Int32 _IDField=-1;
		public System.Int32 ID
		{
			get { return this._IDField; }
			set { this._IDField=value; }
		}
		private System.String _SubjectField;
		public System.String Subject
		{
			get { return this._SubjectField; }
			set { this._SubjectField=value; }
		}
		private System.String _MailToField;
		public System.String MailTo
		{
			get { return this._MailToField; }
			set { this._MailToField=value; }
		}
		private System.String _MailCCField;
		public System.String MailCC
		{
			get { return this._MailCCField; }
			set { this._MailCCField=value; }
		}
		private System.String _MailBodyField;
		public System.String MailBody
		{
			get { return this._MailBodyField; }
			set { this._MailBodyField=value; }
		}
		private System.String _AttachmentsField;
		public System.String Attachments
		{
			get { return this._AttachmentsField; }
			set { this._AttachmentsField=value; }
		}
		private System.String _SendStatusField;
		public System.String SendStatus
		{
			get { return this._SendStatusField; }
			set { this._SendStatusField=value; }
		}
		private Nullable<System.DateTime> _SendDTField;
		public Nullable<System.DateTime> SendDT
		{
			get { return this._SendDTField; }
			set { this._SendDTField=value; }
		}
		private System.String _SendMsgField;
		public System.String SendMsg
		{
			get { return this._SendMsgField; }
			set { this._SendMsgField=value; }
		}
		private System.Int32 _ProjectIDField;
		public System.Int32 ProjectID
		{
			get { return this._ProjectIDField; }
			set { this._ProjectIDField=value; }
		}
	}
}
