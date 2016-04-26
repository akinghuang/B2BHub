using System;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.API.BO.Entity
{
	[Serializable]
	public partial class EntityBPSetting
	{
		public EntityBPSetting(){}

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
		private System.String _SettingNameField;
		public System.String SettingName
		{
			get { return this._SettingNameField; }
			set { this._SettingNameField=value; }
		}
		private System.String _SMTPServerField;
		public System.String SMTPServer
		{
			get { return this._SMTPServerField; }
			set { this._SMTPServerField=value; }
		}
		private System.String _SenderField;
		public System.String Sender
		{
			get { return this._SenderField; }
			set { this._SenderField=value; }
		}
		private System.String _SenderNameField;
		public System.String SenderName
		{
			get { return this._SenderNameField; }
			set { this._SenderNameField=value; }
		}
		private System.String _EncodingField;
		public System.String Encoding
		{
			get { return this._EncodingField; }
			set { this._EncodingField=value; }
		}
		private System.String _UserNameField;
		public System.String UserName
		{
			get { return this._UserNameField; }
			set { this._UserNameField=value; }
		}
		private System.String _PasswordField;
		public System.String Password
		{
			get { return this._PasswordField; }
			set { this._PasswordField=value; }
		}
		private System.String _BodyFormatField;
		public System.String BodyFormat
		{
			get { return this._BodyFormatField; }
			set { this._BodyFormatField=value; }
		}
		private System.String _BCCField;
		public System.String BCC
		{
			get { return this._BCCField; }
			set { this._BCCField=value; }
		}
	}
}
