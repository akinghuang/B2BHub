using System;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.BO.Entity
{
	[Serializable]
	public partial class EntityBPProjectChange
	{
		public EntityBPProjectChange(){}

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
		private System.String _ChangeTitleField;
		public System.String ChangeTitle
		{
			get { return this._ChangeTitleField; }
			set { this._ChangeTitleField=value; }
		}
		private System.String _ChangeContentField;
		public System.String ChangeContent
		{
			get { return this._ChangeContentField; }
			set { this._ChangeContentField=value; }
		}
		private System.String _ChangeSuorceField;
		public System.String ChangeSuorce
		{
			get { return this._ChangeSuorceField; }
			set { this._ChangeSuorceField=value; }
		}
		private System.String _OwnerField;
		public System.String Owner
		{
			get { return this._OwnerField; }
			set { this._OwnerField=value; }
		}
		private Nullable<System.DateTime> _StartDTField;
		public Nullable<System.DateTime> StartDT
		{
			get { return this._StartDTField; }
			set { this._StartDTField=value; }
		}
		private Nullable<System.DateTime> _TargetDTField;
		public Nullable<System.DateTime> TargetDT
		{
			get { return this._TargetDTField; }
			set { this._TargetDTField=value; }
		}
		private Nullable<System.DateTime> _CompleteDTField;
		public Nullable<System.DateTime> CompleteDT
		{
			get { return this._CompleteDTField; }
			set { this._CompleteDTField=value; }
		}
		private Nullable<System.DateTime> _OnlineDTField;
		public Nullable<System.DateTime> OnlineDT
		{
			get { return this._OnlineDTField; }
			set { this._OnlineDTField=value; }
		}
		private System.String _SourceCodeField;
		public System.String SourceCode
		{
			get { return this._SourceCodeField; }
			set { this._SourceCodeField=value; }
		}
		private System.DateTime _CreatedDTField;
		public System.DateTime CreatedDT
		{
			get { return this._CreatedDTField; }
			set { this._CreatedDTField=value; }
		}
		private Nullable<System.DateTime> _UpdatedDTField;
		public Nullable<System.DateTime> UpdatedDT
		{
			get { return this._UpdatedDTField; }
			set { this._UpdatedDTField=value; }
		}
		private System.Int32 _CreatedByField;
		public System.Int32 CreatedBy
		{
			get { return this._CreatedByField; }
			set { this._CreatedByField=value; }
		}
		private Nullable<System.Int32> _UpdatedByField;
		public Nullable<System.Int32> UpdatedBy
		{
			get { return this._UpdatedByField; }
			set { this._UpdatedByField=value; }
		}
	}
}
