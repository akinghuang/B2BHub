using System;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.BO.Entity
{
	[Serializable]
	public partial class EntityBPMailAttach
	{
		public EntityBPMailAttach(){}

		private System.Int32 _IDField=-1;
		public System.Int32 ID
		{
			get { return this._IDField; }
			set { this._IDField=value; }
		}
		private System.Guid _MailGUIDField;
		public System.Guid MailGUID
		{
			get { return this._MailGUIDField; }
			set { this._MailGUIDField=value; }
		}
		private System.String _FilePathField;
		public System.String FilePath
		{
			get { return this._FilePathField; }
			set { this._FilePathField=value; }
		}
		private System.String _FileNameField;
		public System.String FileName
		{
			get { return this._FileNameField; }
			set { this._FileNameField=value; }
		}
		private System.String _FileContentTypeField;
		public System.String FileContentType
		{
			get { return this._FileContentTypeField; }
			set { this._FileContentTypeField=value; }
		}
		private Nullable<System.Int32> _FileLengthField;
		public Nullable<System.Int32> FileLength
		{
			get { return this._FileLengthField; }
			set { this._FileLengthField=value; }
		}
		private System.Byte[] _FileContentField;
		public System.Byte[] FileContent
		{
			get { return this._FileContentField; }
			set { this._FileContentField=value; }
		}
		private Nullable<System.DateTime> _CreatedDateField;
		public Nullable<System.DateTime> CreatedDate
		{
			get { return this._CreatedDateField; }
			set { this._CreatedDateField=value; }
		}
	}
}
