using System;
using System.Collections.Generic;
using System.Text;

namespace MW.DataHub.BO.Entity
{
	[Serializable]
	public partial class EntityBPProjectTask
	{
		public EntityBPProjectTask(){}

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
		private Nullable<System.Boolean> _RunningField;
		public Nullable<System.Boolean> Running
		{
			get { return this._RunningField; }
			set { this._RunningField=value; }
		}
		private System.String _TaskNameField;
		public System.String TaskName
		{
			get { return this._TaskNameField; }
			set { this._TaskNameField=value; }
		}
		private System.String _TaskDescField;
		public System.String TaskDesc
		{
			get { return this._TaskDescField; }
			set { this._TaskDescField=value; }
		}
		private System.String _ProtocolField;
		public System.String Protocol
		{
			get { return this._ProtocolField; }
			set { this._ProtocolField=value; }
		}
		private System.String _IOField;
		public System.String IO
		{
			get { return this._IOField; }
			set { this._IOField=value; }
		}
		private System.Int32 _SequenceField;
		public System.Int32 Sequence
		{
			get { return this._SequenceField; }
			set { this._SequenceField=value; }
		}
		private System.String _MsgHandlerField;
		public System.String MsgHandler
		{
			get { return this._MsgHandlerField; }
			set { this._MsgHandlerField=value; }
		}
		private System.String _BizHandlerField;
		public System.String BizHandler
		{
			get { return this._BizHandlerField; }
			set { this._BizHandlerField=value; }
		}
		private System.String _RuntimeParasField;
		public System.String RuntimeParas
		{
			get { return this._RuntimeParasField; }
			set { this._RuntimeParasField=value; }
		}
		private System.String _RServerField;
		public System.String RServer
		{
			get { return this._RServerField; }
			set { this._RServerField=value; }
		}
		private System.String _RFolderField;
		public System.String RFolder
		{
			get { return this._RFolderField; }
			set { this._RFolderField=value; }
		}
		private Nullable<System.Int32> _RPortField;
		public Nullable<System.Int32> RPort
		{
			get { return this._RPortField; }
			set { this._RPortField=value; }
		}
		private System.String _RUIDField;
		public System.String RUID
		{
			get { return this._RUIDField; }
			set { this._RUIDField=value; }
		}
		private System.String _RPWDField;
		public System.String RPWD
		{
			get { return this._RPWDField; }
			set { this._RPWDField=value; }
		}
		private System.String _RCertField;
		public System.String RCert
		{
			get { return this._RCertField; }
			set { this._RCertField=value; }
		}
		private System.String _FileExtField;
		public System.String FileExt
		{
			get { return this._FileExtField; }
			set { this._FileExtField=value; }
		}
		private System.String _LFolderField;
		public System.String LFolder
		{
			get { return this._LFolderField; }
			set { this._LFolderField=value; }
		}
		private System.String _ScheduleTypeField;
		public System.String ScheduleType
		{
			get { return this._ScheduleTypeField; }
			set { this._ScheduleTypeField=value; }
		}
		private Nullable<System.Int32> _ScheduleMonthField;
		public Nullable<System.Int32> ScheduleMonth
		{
			get { return this._ScheduleMonthField; }
			set { this._ScheduleMonthField=value; }
		}
		private System.String _ScheduleTimeField;
		public System.String ScheduleTime
		{
			get { return this._ScheduleTimeField; }
			set { this._ScheduleTimeField=value; }
		}
		private Nullable<System.DateTime> _LastRunTimeField;
		public Nullable<System.DateTime> LastRunTime
		{
			get { return this._LastRunTimeField; }
			set { this._LastRunTimeField=value; }
		}
		private System.String _StatusField;
		public System.String Status
		{
			get { return this._StatusField; }
			set { this._StatusField=value; }
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
		private System.String _ScheduleWeekDayField;
		public System.String ScheduleWeekDay
		{
			get { return this._ScheduleWeekDayField; }
			set { this._ScheduleWeekDayField=value; }
		}
		private Nullable<System.Int32> _ScheduleIntervalField;
		public Nullable<System.Int32> ScheduleInterval
		{
			get { return this._ScheduleIntervalField; }
			set { this._ScheduleIntervalField=value; }
		}
		private Nullable<System.DateTime> _NextRunTimeField;
		public Nullable<System.DateTime> NextRunTime
		{
			get { return this._NextRunTimeField; }
			set { this._NextRunTimeField=value; }
		}
	}
}
