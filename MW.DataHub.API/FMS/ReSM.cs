using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MW.DataHub.API.BO;
using System.Data;

namespace MW.DataHub.API.ValuePlus
{
    #region Entity
    [Serializable]
    public class EntityCustomerAddress
    {
        private int _CustomerID;
        public int CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        private int _AltAddressID;
        public int AltAddressID
        {
            get { return _AltAddressID; }
            set { _AltAddressID = value; }
        }

        private string _CustomerName;
        public string CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }


        private string _CustomerAddress1;
        public string CustomerAddress1
        {
            get { return _CustomerAddress1; }
            set { _CustomerAddress1 = value; }
        }

        private string _CustomerAddress2;
        public string CustomerAddress2
        {
            get { return _CustomerAddress2; }
            set { _CustomerAddress2 = value; }
        }

        private Nullable<int> _CityID;
        public Nullable<int> CityID
        {
            get { return _CityID; }
            set { _CityID = value; }
        }

        private string _CityName;
        public string CityName
        {
            get { return _CityName; }
            set { _CityName = value; }
        }

        private string _CityCode;
        public string CityCode
        {
            get { return _CityCode; }
            set { _CityCode = value; }
        }

        private Nullable<int> _StateID;
        public Nullable<int> StateID
        {
            get { return _StateID; }
            set { _StateID = value; }
        }
        private string _StateCode;
        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value; }
        }
        private string _StateName;
        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
        }
        private Nullable<int> _CountryID;
        public Nullable<int> CountryID
        {
            get { return _CountryID; }
            set { _CountryID = value; }
        }
        private string _CountryCode;
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
        private string _Postal;
        public string Postal
        {
            get { return _Postal; }
            set { _Postal = value; }
        }

        public bool PostalMandatory { get; set; }

    }

    [Serializable]
    public class EntityCityInfo
    {
        private Nullable<int> _CityID;
        public Nullable<int> CityID
        {
            get { return _CityID; }
            set { _CityID = value; }
        }

        private string _CityName;
        public string CityName
        {
            get { return _CityName; }
            set { _CityName = value; }
        }

        private string _CityCode;
        public string CityCode
        {
            get { return _CityCode; }
            set { _CityCode = value; }
        }

        private Nullable<int> _StateID;
        public Nullable<int> StateID
        {
            get { return _StateID; }
            set { _StateID = value; }
        }
        private string _StateCode;
        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value; }
        }
        private string _StateName;
        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
        }
        private Nullable<int> _CountryID;
        public Nullable<int> CountryID
        {
            get { return _CountryID; }
            set { _CountryID = value; }
        }
        private string _CountryCode;
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
    }

    [Serializable]
    public class EntityPortInfo
    {
        private string _PortType;
        /// <summary>
        /// PortType is two type: AirPort or SeaPort
        /// </summary>
        public string PortType
        {
            get { return _PortType; }
            set { _PortType = value; }
        }

        private Nullable<int> _PortID;
        public Nullable<int> PortID
        {
            get { return _PortID; }
            set { _PortID = value; }
        }

        private string _PortName;
        public string PortName
        {
            get { return _PortName; }
            set { _PortName = value; }
        }

        private string _PortCode;
        public string PortCode
        {
            get { return _PortCode; }
            set { _PortCode = value; }
        }

        private Nullable<int> _CityID;
        public Nullable<int> CityID
        {
            get { return _CityID; }
            set { _CityID = value; }
        }

        private string _CityName;
        public string CityName
        {
            get { return _CityName; }
            set { _CityName = value; }
        }

        private string _CityCode;
        public string CityCode
        {
            get { return _CityCode; }
            set { _CityCode = value; }
        }

        private Nullable<int> _StateID;
        public Nullable<int> StateID
        {
            get { return _StateID; }
            set { _StateID = value; }
        }
        private string _StateCode;
        public string StateCode
        {
            get { return _StateCode; }
            set { _StateCode = value; }
        }
        private string _StateName;
        public string StateName
        {
            get { return _StateName; }
            set { _StateName = value; }
        }
        private Nullable<int> _CountryID;
        public Nullable<int> CountryID
        {
            get { return _CountryID; }
            set { _CountryID = value; }
        }
        private string _CountryCode;
        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }
    }

    [Serializable]
    public class EntityMilestone
    {
        private string _StationID;
        public string StationID
        {
            get { return _StationID; }
            set { _StationID = value; }
        }

        private string _KeyValue;
        /// <summary>
        /// PortType is two type: AirPort or SeaPort
        /// </summary>
        public string KeyValue
        {
            get { return _KeyValue; }
            set { _KeyValue = value; }
        }

        private int _MilestoneID;
        public int MilestoneID
        {
            get { return _MilestoneID; }
            set { _MilestoneID = value; }
        }

        private DateTime _Milestone;
        public DateTime Milestone
        {
            get { return _Milestone; }
            set { _Milestone = value; }
        }

        private int _UTCOffSet;
        public int UTCOffSet
        {
            get { return _UTCOffSet; }
            set { _UTCOffSet = value; }
        }

    }
    #endregion Entity

    public partial interface IBPReSM : IBase<object>
    {
       EntityCustomerAddress SMCustomerAddress_Alt(int CustomerID, int AltAddressID);
       List<EntityCustomerAddress> BuildCustomerAddressSelectList(IDataReader idr);
       EntityCityInfo SMCityInfo(int CityID);
       EntityPortInfo SMPortInfo(int PortID, PortType portType);
       Dictionary<int,EntityMilestone> SMMilestoneList(string KeyValue);
    }

    partial class clsBPReSM : clsBase<object>, IBPReSM
    {   
        public clsBPReSM()
        {
            this.DB.DatabaseName = "ReSM";
            if (DB.TrustSQL)
            {
                DB.ServerIP = this.DB.ServerIP;
                DB.DBName = this.DB.DBName;
                DB.TrustSQL = DB.TrustSQL;
            }
            else if (!(string.IsNullOrEmpty(DB.ServerIP) || string.IsNullOrEmpty(DB.DBName) || string.IsNullOrEmpty(DB.UserID)))
            {
                DB.ServerIP = this.DB.ServerIP;
                DB.DBName = this.DB.DBName;
                DB.UserID = this.DB.UserID;
                DB.UserPassword = this.DB.UserPassword;
            }
            else if (!string.IsNullOrEmpty(DB.DatabaseName))
                DB.DatabaseName = DB.DatabaseName;
        }
        public clsBPReSM(string datbaseName) { DB.DatabaseName = datbaseName; }

        public List<EntityCustomerAddress> BuildCustomerAddressSelectList(IDataReader idr)
        {
            List<EntityCustomerAddress> lists = new List<EntityCustomerAddress>();
            while (idr.Read())
            {
                EntityCustomerAddress entity = new EntityCustomerAddress();
                int index = 0;

                index = idr.GetOrdinal("CustomerID");
                if (!idr.IsDBNull(index))
                {
                    entity.CustomerID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("AltAddressID");
                if (!idr.IsDBNull(index))
                {
                    entity.AltAddressID = idr.GetInt32(index);
                }


                index = idr.GetOrdinal("CustomerName");
                if (!idr.IsDBNull(index))
                {
                    entity.CustomerName = idr.GetString(index);
                }

                index = idr.GetOrdinal("CustomerAddress1");
                if (!idr.IsDBNull(index))
                {
                    entity.CustomerAddress1 = idr.GetString(index);
                }

                index = idr.GetOrdinal("CustomerAddress2");
                if (!idr.IsDBNull(index))
                {
                    entity.CustomerAddress2 = idr.GetString(index);
                }

                index = idr.GetOrdinal("CityID");
                if (!idr.IsDBNull(index))
                {
                    entity.CityID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("CityCode");
                if (!idr.IsDBNull(index))
                {
                    entity.CityCode = idr.GetString(index);
                }

                index = idr.GetOrdinal("CityName");
                if (!idr.IsDBNull(index))
                {
                    entity.CityName = idr.GetString(index);
                }

                index = idr.GetOrdinal("StateID");
                if (!idr.IsDBNull(index))
                {
                    entity.StateID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("StateCode");
                if (!idr.IsDBNull(index))
                {
                    entity.StateCode = idr.GetString(index);
                    if (entity.StateCode == "N$")
                        entity.StateCode = "";
                }

                //index = idr.GetOrdinal("StateName");
                //if (!idr.IsDBNull(index))
                //{
                //    entity.StateName = idr.GetString(index);
                //}

                index = idr.GetOrdinal("CountryID");
                if (!idr.IsDBNull(index))
                {
                    entity.CountryID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("CountryCode");
                if (!idr.IsDBNull(index))
                {
                    entity.CountryCode = idr.GetString(index);
                }

                index = idr.GetOrdinal("Postal");
                if (!idr.IsDBNull(index))
                {
                    entity.Postal = idr.GetString(index);
                }

                lists.Add(entity);
            }
            return lists;
        }
        public List<EntityCityInfo> BuildCitySelectList(IDataReader idr)
        {
            List<EntityCityInfo> lists = new List<EntityCityInfo>();
            while (idr.Read())
            {
                EntityCityInfo entity = new EntityCityInfo();
                int index = 0;

                index = idr.GetOrdinal("CityID");
                if (!idr.IsDBNull(index))
                {
                    entity.CityID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("CityCode");
                if (!idr.IsDBNull(index))
                {
                    entity.CityCode = idr.GetString(index);
                }

                index = idr.GetOrdinal("CityName");
                if (!idr.IsDBNull(index))
                {
                    entity.CityName = idr.GetString(index);
                }

                index = idr.GetOrdinal("StateID");
                if (!idr.IsDBNull(index))
                {
                    entity.StateID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("StateCode");
                if (!idr.IsDBNull(index))
                {
                    entity.StateCode = idr.GetString(index);
                }

                index = idr.GetOrdinal("StateName");
                if (!idr.IsDBNull(index))
                {
                    entity.StateName = idr.GetString(index);
                }

                index = idr.GetOrdinal("CountryID");
                if (!idr.IsDBNull(index))
                {
                    entity.CountryID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("CountryCode");
                if (!idr.IsDBNull(index))
                {
                    entity.CountryCode = idr.GetString(index);
                }

                lists.Add(entity);
            }
            return lists;
        }
        public List<EntityPortInfo> BuildPortSelectList(IDataReader idr)
        {
            List<EntityPortInfo> lists = new List<EntityPortInfo>();
            while (idr.Read())
            {
                EntityPortInfo entity = new EntityPortInfo();
                int index = 0;

                index = idr.GetOrdinal("PortType");
                if (!idr.IsDBNull(index))
                {
                    entity.PortType = idr.GetString(index);
                }

                index = idr.GetOrdinal("PortID");
                if (!idr.IsDBNull(index))
                {
                    entity.PortID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("PortCode");
                if (!idr.IsDBNull(index))
                {
                    entity.PortCode = idr.GetString(index);
                }

                index = idr.GetOrdinal("PortName");
                if (!idr.IsDBNull(index))
                {
                    entity.PortName = idr.GetString(index);
                }

                index = idr.GetOrdinal("CityID");
                if (!idr.IsDBNull(index))
                {
                    entity.CityID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("CityCode");
                if (!idr.IsDBNull(index))
                {
                    entity.CityCode = idr.GetString(index);
                }

                index = idr.GetOrdinal("CityName");
                if (!idr.IsDBNull(index))
                {
                    entity.CityName = idr.GetString(index);
                }

                index = idr.GetOrdinal("StateID");
                if (!idr.IsDBNull(index))
                {
                    entity.StateID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("StateCode");
                if (!idr.IsDBNull(index))
                {
                    entity.StateCode = idr.GetString(index);
                }

                index = idr.GetOrdinal("StateName");
                if (!idr.IsDBNull(index))
                {
                    entity.StateName = idr.GetString(index);
                }

                index = idr.GetOrdinal("CountryID");
                if (!idr.IsDBNull(index))
                {
                    entity.CountryID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("CountryCode");
                if (!idr.IsDBNull(index))
                {
                    entity.CountryCode = idr.GetString(index);
                }

                lists.Add(entity);
            }
            return lists;
        }

        public Dictionary<int, EntityMilestone> BuildMilestoneSelectList(IDataReader idr)
        {
            Dictionary<int, EntityMilestone> lists = new Dictionary<int, EntityMilestone>();
            while (idr.Read())
            {
                EntityMilestone entity = new EntityMilestone();
                int index = 0;
                index = idr.GetOrdinal("MilestoneID");
                if (!idr.IsDBNull(index))
                {
                    entity.MilestoneID = idr.GetInt32(index);
                }

                if(lists.ContainsKey(entity.MilestoneID))
                    continue;                

                index = idr.GetOrdinal("KeyValue");
                if (!idr.IsDBNull(index))
                {
                    entity.KeyValue = idr.GetString(index);
                }

                index = idr.GetOrdinal("MilestoneTime");
                if (!idr.IsDBNull(index))
                {
                    entity.Milestone = idr.GetDateTime(index);
                }

                index = idr.GetOrdinal("StationID");
                if (!idr.IsDBNull(index))
                {
                    entity.StationID = idr.GetString(index);
                }

                index = idr.GetOrdinal("UTCOffSet");
                if (!idr.IsDBNull(index))
                {
                    entity.UTCOffSet = idr.GetInt32(index);
                }

                lists.Add(entity.MilestoneID, entity);
            }
            return lists;
        }

        private EntityCustomerAddress SMCustomerAddress_Alt(int AltAddressID)
        {
            ///共三处
            StringBuilder lsb = new StringBuilder("");
            lsb.AppendFormat(@" select CU.CustomerID,0 AltAddressID,CustomerName
,isnull(CustomerAddress1,'') as CustomerAddress1
,isnull(CustomerAddress2,'') + isnull(CustomerAddress3,'') as CustomerAddress2
,Cu.CityID, CI.CityCode, isnull(CI.CityName,City) CityName
,convert(int,Cu.State) StateID,case when CC.CountryCode in ('CA','US') then isnull(CS.statecode,'') else '' end as StateCode
,convert(int,Cu.Country) CountryID,isnull(CC.CountryCode,'') as CountryCode
,CU.zip as Postal" + 
                   " from Resm.dbo.smcustomeraddress  CU " +
                           " Left join resm.dbo.smcity CI on CI.HQID = CU.CityID " +
                           " Left join resm.dbo.smstate CS on CS.HQID = CU.State " +
                           " Left join resm.dbo.smCountry CC on CC.HQID = CU.Country " +
                           " where CU.hqid ='{0}'",AltAddressID);
            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityCustomerAddress> lists = this.BuildCustomerAddressSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;
        }
        private EntityCustomerAddress SMCustomerAddress_Def(int CustomerID)
        {
            ///共三处
            StringBuilder lsb = new StringBuilder("");
            lsb.AppendFormat(@" select CU.HQID CustomerID,0 AltAddressID,CustomerName
,isnull(CustomerAddress1,'') as CustomerAddress1
,isnull(CustomerAddress2,'') + isnull(CustomerAddress3,'') as CustomerAddress2
,Cu.CityID, CI.CityCode, isnull(CI.CityName,City) CityName
,convert(int,Cu.State) StateID,case when CC.CountryCode in ('CA','US') then isnull(CS.statecode,'') else '' end as StateCode
,convert(int,Cu.Country) CountryID,isnull(CC.CountryCode,'') as CountryCode
,CU.zip as Postal" +
                            " from resm.dbo.smcustomer CU " +
                            " Left join resm.dbo.smcity CI on CI.HQID = CU.CityID " +
                            " Left join resm.dbo.smstate CS on CS.HQID = CU.State " +
                            " Left join resm.dbo.smCountry CC on CC.HQID = CU.Country " +
                            " where CU.hqid ='{0}'",CustomerID);


  


            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityCustomerAddress> lists = this.BuildCustomerAddressSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;          
        }
        public EntityCustomerAddress SMCustomerAddress_Alt(int CustomerID, int AltAddressID)
        {
            if (!(CustomerID > 0))
                throw new Exception("Parameter error for CustomerID");

            if (AltAddressID > 0)
                return this.SMCustomerAddress_Alt(AltAddressID);
            else
                return this.SMCustomerAddress_Def(CustomerID);


        }


        public EntityCityInfo SMCityInfo(int CityID)
        {
            if (!(CityID > 0))
                throw new Exception("Parameter error for CityID");

            StringBuilder lsb = new StringBuilder("");
            lsb.AppendFormat(@" Select C.HQID CityID, C.CityCode, C.CityName
,S.HQID StateID, S.StateCode, S.StateName
,Co.HQID CountryID, Co.CountryCode, Co.CountryName
From ReSM.dbo.SMCity C
Left Join ReSM..SMState S on S.HQID=C.StateID
Left Join ReSM..SMCountry Co on Co.HQID=C.CountryID
Where C.hqid ='{0}'", CityID);
            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityCityInfo> lists = this.BuildCitySelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;
        }

        private EntityPortInfo SMSeaPortInfo(int PortID)
        {
            if (!(PortID > 0))
                throw new Exception("Parameter error for SeaPort ID");

            StringBuilder lsb = new StringBuilder("");
            lsb.AppendFormat(@" Select 'SeaPort' PortType,P.HQID PortID, P.SeaPortCode PortCode, P.SeaPortName PortName
,C.HQID CityID, C.CityCode, C.CityName
,S.HQID StateID, S.StateCode, S.StateName
,Co.HQID CountryID, Co.CountryCode, Co.CountryName
From ReSM.dbo.SMSeaPort P
Left Join ReSM.dbo.SMCity C on C.HQID=P.CityID
Left Join ReSM..SMState S on S.HQID=C.StateID
Left Join ReSM..SMCountry Co on Co.HQID=C.CountryID
Where P.hqid ='{0}'", PortID);
            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityPortInfo> lists = this.BuildPortSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;
        }
        private EntityPortInfo SMAirPortInfo(int PortID)
        {
            if (!(PortID > 0))
                throw new Exception("Parameter error for SeaPort ID");

            StringBuilder lsb = new StringBuilder("");
            lsb.AppendFormat(@" Select 'AirPort' PortType,P.HQID PortID, P.AirPortCode PortCode, P.AirPortName PortName
,C.HQID CityID, C.CityCode, C.CityName
,S.HQID StateID, S.StateCode, S.StateName
,Co.HQID CountryID, Co.CountryCode, Co.CountryName
From ReSM.dbo.SMAirPort P
Left Join ReSM.dbo.SMCity C on C.HQID=P.CityID
Left Join ReSM..SMState S on S.HQID=C.StateID
Left Join ReSM..SMCountry Co on Co.HQID=C.CountryID
Where P.hqid ='{0}'", PortID);
            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityPortInfo> lists = this.BuildPortSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;
        }

        public EntityPortInfo SMPortInfo(int PortID, PortType portType)
        {
            if (portType == PortType.SeaPort)
                return SMSeaPortInfo(PortID);
            else
                return SMAirPortInfo(PortID);
        }


        public Dictionary<int,EntityMilestone> SMMilestoneList(string KeyValue){

            if (string.IsNullOrEmpty(KeyValue))
                throw new Exception("Parameter error for Milestone KeyValue, can not empty");

            StringBuilder lsb = new StringBuilder("");
            lsb.AppendFormat(@" select StationID,KeyValue,MilestoneID,MilestoneTime
,IrrReason,ApprovedBy,ExtraValue1,ExtraValue2
,UTCOffSet,Status from SMMilestoneHistory
Where KeyValue='{0}' and Status='1'
Order by milestoneid,status", KeyValue.Replace("-","").Replace(" ",""));
            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            Dictionary<int,EntityMilestone> lists = this.BuildMilestoneSelectList(idr);
            
            return lists;            
        }
    }
}
