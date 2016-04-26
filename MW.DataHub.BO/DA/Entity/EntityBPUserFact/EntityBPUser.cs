using System;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.BO.Entity
{
	[Serializable]
	public partial class EntityBPUser
	{
		public EntityBPUser(){}

		private System.Int32 _IDField=-1;
		public System.Int32 ID
		{
			get { return this._IDField; }
			set { this._IDField=value; }
		}
		private System.String _UserIDField;
		public System.String UserID
		{
			get { return this._UserIDField; }
			set { this._UserIDField=value; }
		}
		private System.String _FullNameField;
		public System.String FullName
		{
			get { return this._FullNameField; }
			set { this._FullNameField=value; }
		}
		private System.String _PasswordField;
		public System.String Password
		{
			get { return this._PasswordField; }
			set { this._PasswordField=value; }
		}
		private System.String _CommentsField;
		public System.String Comments
		{
			get { return this._CommentsField; }
			set { this._CommentsField=value; }
		}
		private Nullable<System.DateTime> _LastLoginDTField;
		public Nullable<System.DateTime> LastLoginDT
		{
			get { return this._LastLoginDTField; }
			set { this._LastLoginDTField=value; }
		}
		private Nullable<System.Int32> _TotalLoginCountField;
		public Nullable<System.Int32> TotalLoginCount
		{
			get { return this._TotalLoginCountField; }
			set { this._TotalLoginCountField=value; }
		}
		private System.String _StatusField;
		public System.String Status
		{
			get { return this._StatusField; }
			set { this._StatusField=value; }
		}
		private System.String _LastLoginIPField;
		public System.String LastLoginIP
		{
			get { return this._LastLoginIPField; }
			set { this._LastLoginIPField=value; }
		}
	}
}
