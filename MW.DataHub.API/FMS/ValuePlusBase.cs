using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MW.DataHub.API.BO;
using System.Data;

namespace MW.DataHub.API.ValuePlus
{
    #region Entity
    public enum PortType { SeaPort, AirPort }

    [Serializable]
    public class EntityShipment
    {
        private string _StationID;
        public string StationID
        {
            get { return _StationID; }
            set { _StationID = value; }
        }

        private int _HouseID;
        public int HouseID
        {
            get { return _HouseID; }
            set { _HouseID = value; }
        }

        private string _HouseNo;
        public string HouseNo
        {
            get { return _HouseNo; }
            set { _HouseNo = value; }
        }

        private string _ModeCode;
        public string ModeCode
        {
            get { return _ModeCode; }
            set { _ModeCode = value; }
        }

        private string _IDType;
        public string IDType
        {
            get { return _IDType; }
            set { _IDType = value; }
        }

        private string _Customer;
        public string Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }

        private int _Shipper;
        public int Shipper
        {
            get { return _Shipper; }
            set { _Shipper = value; }
        }

        private int _Shipper_ALT;
        public int Shipper_ALT
        {
            get { return _Shipper_ALT; }
            set { _Shipper_ALT = value; }
        }

        private string _ShipperInfo;
        public string ShipperInfo
        {
            get { return _ShipperInfo; }
            set { _ShipperInfo = value; }
        }

        private int _CNEE;
        public int CNEE
        {
            get { return _CNEE; }
            set { _CNEE = value; }
        }

        private int _CNEE_ALT;
        public int CNEE_ALT
        {
            get { return _CNEE_ALT; }
            set { _CNEE_ALT = value; }
        }

        private string _CNEEInfo;
        public string CNEEInfo
        {
            get { return _CNEEInfo; }
            set { _CNEEInfo = value; }
        }

        private int _PlaceOfReceipt;
        public int PlaceOfReceipt
        {
            get { return _PlaceOfReceipt; }
            set { _PlaceOfReceipt = value; }
        }

        private int _PortOfDEPT;
        public int PortOfDEPT
        {
            get { return _PortOfDEPT; }
            set { _PortOfDEPT = value; }
        }

        private int _PortOfDSTN;
        public int PortOfDSTN
        {
            get { return _PortOfDSTN; }
            set { _PortOfDSTN = value; }
        }

        private int _PlaceOfDelivery;
        public int PlaceOfDelivery
        {
            get { return _PlaceOfDelivery; }
            set { _PlaceOfDelivery = value; }
        }

        private string _TradeTerm;
        public string TradeTerm
        {
            get { return _TradeTerm; }
            set { _TradeTerm = value; }
        }

        private string _ServiceLevel;
        public string ServiceLevel
        {
            get { return _ServiceLevel; }
            set { _ServiceLevel = value; }
        }

        private string _NatureOfGoods;
        public string NatureOfGoods
        {
            get { return _NatureOfGoods; }
            set { _NatureOfGoods = value; }
        }
        
        private int _PCS;
        public int PCS
        {
            get { return _PCS; }
            set { _PCS = value; }
        }

        private string _PCSUOM;
        public string PCSUOM
        {
            get { return _PCSUOM; }
            set { _PCSUOM = value; }
        }

        private Nullable< decimal> _GWT;
        public Nullable<decimal> GWT
        {
            get { return _GWT; }
            set { _GWT = value; }
        }

        private string _WTUOM;
        public string WTUOM
        {
            get { return _WTUOM; }
            set { _WTUOM = value; }
        }

        private string _FreightType;
        public string FreightPayType
        {
            get { return _FreightType; }
            set { _FreightType = value; }
        }

        private string _OtherType;
        public string OtherPayType
        {
            get { return _OtherType; }
            set { _OtherType = value; }
        }

        private string _Desc;
        public string DESC
        {
            get { return _Desc; }
            set { _Desc = value; }
        }

        private string _Movement;
        /// <summary>
        /// DD,DP,PD,PP
        /// </summary>
        public string Movement
        {
            get { return _Movement; }
            set { _Movement = value; }
        }

        private string _MStationID;
        public string MStationID
        {
            get { return _MStationID; }
            set { _MStationID = value; }
        }

        private Nullable<int> _MasterID;
        public Nullable<int> MasterID
        {
            get { return _MasterID; }
            set { _MasterID = value; }
        }

        private string _MasterNo;
        public string MasterNo
        {
            get { return _MasterNo; }
            set { _MasterNo = value; }
        }

        public string LotNo { get; set; }
        public string FreightSignby { get; set; }

        public string PONo { get; set; }        
        public Dictionary<int, EntityMilestone> MilestoneLists { get; set; }
        public List<EntityPO> POList { get; set; }
    }

    [Serializable]
    public class EntityPO
    {
        public string PONo
        {
            get;
            set;
        }
        public string CommInvNo
        {
            get;
            set;
        }
        public DateTime? InvoiceDate
        {
            get;
            set;
        }
    }

    #endregion Entity


   

    public class VPFactory
    {
        public static IBPValuePlus GetBPValuePlus()
        { 
            
            return new clsBPValuePlus();
        }
        public static IBPReSM GetBPReSM()
        {
            return new clsBPReSM();
        }
    }

    public partial interface IBPValuePlus : IBase<object>
    {
        bool IsCentralData { get; set; }
        EntityCustomerAddress SMCompanyAddress_Def(int CustomerID, int AltAddressID);
        void ConnectByStationID(string StationID);
    }

    partial class clsBPValuePlus : clsBase<object>, IBPValuePlus
    {   
        public clsBPValuePlus()
        {
            this.DB.DatabaseName = "eChainVP_Central";
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
        public clsBPValuePlus(string datbaseName) { DB.DatabaseName = datbaseName; }

        IBPReSM mgrReSM = VPFactory.GetBPReSM();
        public bool IsCentralData
        {
            get { return _CentralData; }
            set { _CentralData = value; }
        }
        private bool _CentralData = true;

        public void ConnectByStationID(string StationID)
        {
            string[] str = mgrReSM.DB.ExecuteScalarString("Select StationIP+':'+DBName from SMStation Where StationID='"+StationID+"'").Split(':');

            if (str.Length == 2)
            {
                this.DB.ServerIP = str[0];
                this.DB.DBName = str[1];
                this.DB.UserID = "sa";
                this.DB.UserPassword = "dim1rc0@";
                this.DB.CreateDatabase(str[0], str[1], "sa", "dim1rc0@");
            }
            else
                throw new Exception("Did not find the DB information for StationID:" + StationID);
        }

        protected string AdjustToSQL(string str)
        {
            return str.Replace("'", "''");
        }

        public EntityCustomerAddress SMCompanyAddress_Def(int CustomerID, int AltAddressID)
        {
            if (IsCentralData || AltAddressID > 0)
            {
                EntityCustomerAddress enAddress = mgrReSM.SMCustomerAddress_Alt(CustomerID, AltAddressID);
                return enAddress;
            }

            ///共三处
            StringBuilder lsb = new StringBuilder("");
            lsb.AppendFormat(@" select CU.CustomerID,0 AltAddressID,CU.CustomerName
,isnull(CustomerAddress1,'') as CustomerAddress1
,isnull(CustomerAddress2,'') + isnull(CustomerAddress3,'') as CustomerAddress2
,Cu.CityID, CI.CityCode, isnull(CI.CityName,City) CityName
,convert(int,Cu.State) StateID,case when CC.CountryCode in ('CA','US') then isnull(CS.statecode,'') else '' end as StateCode
,convert(int,Cu.Country) CountryID,isnull(CC.CountryCode,'') as CountryCode
,CU.zip as Postal" +
                   " from SMCustomerOne CU " +
                            " Left join resm.dbo.smcity CI on CI.HQID = CU.CityID " +
                            " Left join resm.dbo.smstate CS on CS.HQID = CU.State " +
                            " Left join resm.dbo.smCountry CC on CC.HQID = CU.Country " +
                            " where CU.CustomerID ='{0}'", CustomerID);


            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityCustomerAddress> lists = mgrReSM.BuildCustomerAddressSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;            
        }

    }
}
