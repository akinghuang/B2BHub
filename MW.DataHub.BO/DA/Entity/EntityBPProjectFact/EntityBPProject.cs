using System;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.BO.Entity
{
	[Serializable]
	public partial class EntityBPProject
	{
		public EntityBPProject(){}

		private System.Int32 _IDField=-1;
		public System.Int32 ID
		{
			get { return this._IDField; }
			set { this._IDField=value; }
		}
		private System.Int32 _SequenceField;
		public System.Int32 Sequence
		{
			get { return this._SequenceField; }
			set { this._SequenceField=value; }
		}
		private System.String _ProjectNameField;
		public System.String ProjectName
		{
			get { return this._ProjectNameField; }
			set { this._ProjectNameField=value; }
		}
		private System.String _ProjectDescField;
		public System.String ProjectDesc
		{
			get { return this._ProjectDescField; }
			set { this._ProjectDescField=value; }
		}
		private System.String _OwnerField;
		public System.String Owner
		{
			get { return this._OwnerField; }
			set { this._OwnerField=value; }
		}
		private System.String _AdminMailField;
		public System.String AdminMail
		{
			get { return this._AdminMailField; }
			set { this._AdminMailField=value; }
		}
		private System.String _UserMailField;
		public System.String UserMail
		{
			get { return this._UserMailField; }
			set { this._UserMailField=value; }
		}
		private System.String _RuntimeParasField;
		public System.String RuntimeParas
		{
			get { return this._RuntimeParasField; }
			set { this._RuntimeParasField=value; }
		}
		private System.String _StatusField;
		public System.String Status
		{
			get { return this._StatusField; }
			set { this._StatusField=value; }
		}
		private System.String _HostMachineIDField;
		public System.String HostMachineID
		{
			get { return this._HostMachineIDField; }
			set { this._HostMachineIDField=value; }
		}
		private System.String _ProcessIDField;
		public System.String ProcessID
		{
			get { return this._ProcessIDField; }
			set { this._ProcessIDField=value; }
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
		private Nullable<System.Int32> _UpdateByField;
		public Nullable<System.Int32> UpdateBy
		{
			get { return this._UpdateByField; }
			set { this._UpdateByField=value; }
		}
	}
}
