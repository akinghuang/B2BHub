using System;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.API.BO.Entity
{
	[Serializable]
	public partial class EntityBPMailLog
	{
		public EntityBPMailLog(){}

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
		private System.Guid _MailGUIDField;
		public System.Guid MailGUID
		{
			get { return this._MailGUIDField; }
			set { this._MailGUIDField=value; }
		}
		private System.String _MailStatusField;
		public System.String MailStatus
		{
			get { return this._MailStatusField; }
			set { this._MailStatusField=value; }
		}
		private System.String _MailSenderField;
		public System.String MailSender
		{
			get { return this._MailSenderField; }
			set { this._MailSenderField=value; }
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
		private System.String _MailBCCField;
		public System.String MailBCC
		{
			get { return this._MailBCCField; }
			set { this._MailBCCField=value; }
		}
		private System.String _SubjectField;
		public System.String Subject
		{
			get { return this._SubjectField; }
			set { this._SubjectField=value; }
		}
		private System.String _MailBodyField;
		public System.String MailBody
		{
			get { return this._MailBodyField; }
			set { this._MailBodyField=value; }
		}
		private System.String _MailBodyFormatField;
		public System.String MailBodyFormat
		{
			get { return this._MailBodyFormatField; }
			set { this._MailBodyFormatField=value; }
		}
		private Nullable<System.DateTime> _CreatedDateField;
		public Nullable<System.DateTime> CreatedDate
		{
			get { return this._CreatedDateField; }
			set { this._CreatedDateField=value; }
		}
		private System.String _CreatedByField;
		public System.String CreatedBy
		{
			get { return this._CreatedByField; }
			set { this._CreatedByField=value; }
		}
		private Nullable<System.DateTime> _SendDateField;
		public Nullable<System.DateTime> SendDate
		{
			get { return this._SendDateField; }
			set { this._SendDateField=value; }
		}
		private System.String _SendByField;
		public System.String SendBy
		{
			get { return this._SendByField; }
			set { this._SendByField=value; }
		}
		private System.String _MailResultField;
		public System.String MailResult
		{
			get { return this._MailResultField; }
			set { this._MailResultField=value; }
		}
	}
}
