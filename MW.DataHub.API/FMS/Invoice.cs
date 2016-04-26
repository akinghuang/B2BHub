using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DIMERCO.SDK2.Base;
using DIMERCO.SDK2.Data;
using System.Data;
using MW.DataHub.API.BO;

namespace MW.DataHub.API.ValuePlus
{

    #region Entity of Invoice
    [Serializable]
    public class EntityInvoice
    {
        List<EntityAMS> _AMSItems = new List<EntityAMS>();
        public List<EntityAMS> ItemsAMS
        {
            get { return _AMSItems; }
        }

        List<EntityOMS> _OMSItems = new List<EntityOMS>();
        public List<EntityOMS> ItemsOMS
        {
            get { return _OMSItems; }
        }

        Dictionary<string, EntityInvoiceDetail> _InvoiceDetailItems = new Dictionary<string, EntityInvoiceDetail>();
        public Dictionary<string, EntityInvoiceDetail> ItemsInvoiceDetail
        {
            get { return _InvoiceDetailItems; }
        }

        Dictionary<string, EntityCustomerAddress> _CustomerAddressItems = new Dictionary<string, EntityCustomerAddress>();
        /// <summary>
        /// The Key Value is {0}:{1} , CustomerID, Alt_ID
        /// </summary>
        public Dictionary<string, EntityCustomerAddress> ItemsCustomerAddress
        {
            get { return _CustomerAddressItems; }
        }

        Dictionary<string, EntityCityInfo> _CityInfoItems = new Dictionary<string, EntityCityInfo>();
        /// <summary>
        /// The Key Value is {0} , CityID
        /// </summary>
        public Dictionary<string, EntityCityInfo> ItemsCityInfo
        {
            get { return _CityInfoItems; }
        }

        Dictionary<string, EntityPortInfo> _PortInfoItems = new Dictionary<string, EntityPortInfo>();
        /// <summary>
        /// The Key Value is {0}:{1} , PortType, PortID
        /// </summary>
        public Dictionary<string, EntityPortInfo> ItemsPortInfo
        {
            get { return _PortInfoItems; }
        }

        private string _StationID;
        public string StationID
        {
            get { return _StationID; }
            set { _StationID = value; }
        }

        private string _TempSeq;
        public string TempSeq
        {
            get { return _TempSeq; }
            set { _TempSeq = value; }
        }

        private string _ProductLine;
        public string ProductLine
        {
            get { return _ProductLine; }
            set { _ProductLine = value; }
        }

        private string _IDType;
        public string IDType
        {
            get { return _IDType; }
            set { _IDType = value; }
        }

        private int _SourceID;
        public int SourceID
        {
            get { return _SourceID; }
            set { _SourceID = value; }
        }

        private string _ModeCode;
        public string ModeCode
        {
            get { return _ModeCode; }
            set { _ModeCode = value; }
        }

        private string _InvoiceNo;
        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }

        private Nullable<DateTime> _InvoiceDate;
        public Nullable<DateTime> InvoiceDate
        {
            get { return _InvoiceDate; }
            set { _InvoiceDate = value; }
        }

        private string _InvoiceType;
        public string InvoiceType
        {
            get { return _InvoiceType; }
            set { _InvoiceType = value; }
        }

        private string _InvoiceCurrency;
        public string InvoiceCurrency
        {
            get { return _InvoiceCurrency; }
            set { _InvoiceCurrency = value; }
        }

        private string _MasterNo;
        public string MasterNo
        {
            get { return _MasterNo; }
            set { _MasterNo = value; }
        }

        private string _HouseNo;
        public string HouseNo
        {
            get { return _HouseNo; }
            set { _HouseNo = value; }
        }
        private string _LotNo;
        public string LotNo
        {
            get { return _LotNo; }
            set { _LotNo = value; }
        }
        private string _PayTerms;
        public string PayTerms
        {
            get { return _PayTerms; }
            set { _PayTerms = value; }
        }

        public decimal _InvoiceAmount;
        public decimal InvoiceAmount
        {
            get { return _InvoiceAmount; }
            set { _InvoiceAmount = value; }
        }

        public decimal _SalesAmount;
        public decimal SalesAmount
        {
            get { return _SalesAmount; }
            set { _SalesAmount = value; }
        }

        public decimal _VATAmount;
        public decimal VATAmount
        {
            get { return _VATAmount; }
            set { _VATAmount = value; }
        }

        private Nullable<DateTime> _UpdatedDate;
        public Nullable<DateTime> UpdatedDate
        {
            get { return _UpdatedDate; }
            set { _UpdatedDate = value; }
        }

        private string _Customer;
        public string Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }

        private string _BILLTO;
        public string BILLTO
        {
            get { return _BILLTO; }
            set { _BILLTO = value; }
        }

        private string _BILLTO_Alt;
        public string BILLTO_Alt
        {
            get { return _BILLTO_Alt; }
            set { _BILLTO_Alt = value; }
        }

        private string _BILLTOAddress;
        public string BILLTOAddress
        {
            get { return _BILLTOAddress; }
            set { _BILLTOAddress = value; }
        }

        private string _Remark;
        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
    }

    [Serializable]
    public class EntityInvoiceDetail
    {
        private string _StationID;
        public string StationID
        {
            get { return _StationID; }
            set { _StationID = value; }
        }

        private string _TempSeq;
        public string TempSeq
        {
            get { return _TempSeq; }
            set { _TempSeq = value; }
        }

        private int _ChargeID;
        public int ChargeID
        {
            get { return _ChargeID; }
            set { _ChargeID = value; }
        }

        private string _ChargeCode;
        public string ChargeCode
        {
            get { return _ChargeCode; }
            set { _ChargeCode = value; }
        }


        private int _Seq;
        public int Seq
        {
            get { return _Seq; }
            set { _Seq = value; }
        }

        public decimal _InvSAmount;
        public decimal InvSAmount
        {
            get { return _InvSAmount; }
            set { _InvSAmount = value; }
        }

        public decimal _InvSVATAmount;
        public decimal InvSVATAmount
        {
            get { return _InvSVATAmount; }
            set { _InvSVATAmount = value; }
        }

        public decimal _InvSTotalAmount;
        public decimal InvSTotalAmount
        {
            get { return _InvSTotalAmount; }
            set { _InvSTotalAmount = value; }
        }
    }
    #endregion

    public partial interface IBPValuePlus : IBase<object>
    {
        EntityInvoice InvocieInfoGet(string StationID, string InvoiceNo);
        List<EntityInvoiceDetail> InvocieDetailGet(string StationID, string TempSeq);

        /// <summary>
        /// Get the Invoice Information of Include all other shipment information and other detail.
        /// </summary>
        /// <param name="StationID">The StationID</param>
        /// <param name="InvoiceNo">InvoiceNo</param>
        /// <returns></returns>
        EntityInvoice InvoiceInfoGetAll(string StationID, string InvoiceNo);
    }

    partial class clsBPValuePlus : clsBase<object>, IBPValuePlus
    {
        private List<EntityInvoice> BuildInvoiceSelectList(IDataReader idr)
        {
            List<EntityInvoice> lists = new List<EntityInvoice>();
            while (idr.Read())
            {
                EntityInvoice entity = new EntityInvoice();
                int index = 0;

                index = idr.GetOrdinal("StationID");
                if (!idr.IsDBNull(index))
                {
                    entity.StationID = idr.GetString(index);
                }

                index = idr.GetOrdinal("TempSeq");
                if (!idr.IsDBNull(index))
                {
                    entity.TempSeq = idr.GetString(index);
                }

                index = idr.GetOrdinal("ProductLine");
                if (!idr.IsDBNull(index))
                {
                    entity.ProductLine = idr.GetString(index);
                }

                index = idr.GetOrdinal("IDType");
                if (!idr.IsDBNull(index))
                {
                    entity.IDType = idr.GetString(index);
                }

                index = idr.GetOrdinal("SourceID");
                if (!idr.IsDBNull(index))
                {
                    entity.SourceID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("ModeCode");
                if (!idr.IsDBNull(index))
                {
                    entity.ModeCode = idr.GetString(index);
                }


                index = idr.GetOrdinal("InvoiceNo");
                if (!idr.IsDBNull(index))
                {
                    entity.InvoiceNo = idr.GetString(index);
                }


                index = idr.GetOrdinal("InvoiceDate");
                if (!idr.IsDBNull(index))
                {
                    entity.InvoiceDate = idr.GetDateTime(index);
                }

                index = idr.GetOrdinal("InvoiceType");
                if (!idr.IsDBNull(index))
                {
                    entity.InvoiceType = idr.GetString(index);
                }

                index = idr.GetOrdinal("InvoiceCurrency");
                if (!idr.IsDBNull(index))
                {
                    entity.InvoiceCurrency = idr.GetString(index);
                }

                index = idr.GetOrdinal("House");
                if (!idr.IsDBNull(index))
                {
                    entity.HouseNo = idr.GetString(index);
                }

                index = idr.GetOrdinal("Master");
                if (!idr.IsDBNull(index))
                {
                    entity.MasterNo = idr.GetString(index);
                }

                index = idr.GetOrdinal("Lot");
                if (!idr.IsDBNull(index))
                {
                    entity.LotNo = idr.GetString(index);
                }

                index = idr.GetOrdinal("PayTerms");
                if (!idr.IsDBNull(index))
                {
                    entity.PayTerms = idr.GetString(index);
                }

                index = idr.GetOrdinal("InvoiceAmount");
                if (!idr.IsDBNull(index))
                {
                    entity.InvoiceAmount = idr.GetDecimal(index);
                }

                index = idr.GetOrdinal("SalesAmount");
                if (!idr.IsDBNull(index))
                {
                    entity.SalesAmount = idr.GetDecimal(index);
                }

                index = idr.GetOrdinal("VATAmount");
                if (!idr.IsDBNull(index))
                {
                    entity.VATAmount = idr.GetDecimal(index);
                }


                index = idr.GetOrdinal("UpdatedDate");
                if (!idr.IsDBNull(index))
                {
                    entity.UpdatedDate = idr.GetDateTime(index);
                }

                index = idr.GetOrdinal("CustomerID");
                if (!idr.IsDBNull(index))
                {
                    entity.Customer = idr.GetInt32(index).ToString();
                }

                index = idr.GetOrdinal("BILLTO");
                if (!idr.IsDBNull(index))
                {
                    entity.BILLTO = idr.GetInt32(index).ToString();
                }

                index = idr.GetOrdinal("BILLTO_ALT");
                if (!idr.IsDBNull(index))
                {
                    entity.BILLTO_Alt = idr.GetInt32(index).ToString();
                }

                index = idr.GetOrdinal("BillAddress");
                if (!idr.IsDBNull(index))
                {
                    entity.BILLTOAddress = idr.GetString(index);
                }

                index = idr.GetOrdinal("Remark");
                if (!idr.IsDBNull(index))
                {
                    entity.Remark = idr.GetString(index);
                }
                lists.Add(entity);

            }
            return lists;
        }
        private List<EntityInvoiceDetail> BuildInvoiceDetailSelectList(IDataReader idr)
        {
            List<EntityInvoiceDetail> lists = new List<EntityInvoiceDetail>();
            while (idr.Read())
            {
                EntityInvoiceDetail entity = new EntityInvoiceDetail();
                int index = 0;

                index = idr.GetOrdinal("StationID");
                if (!idr.IsDBNull(index))
                {
                    entity.StationID = idr.GetString(index);
                }

                index = idr.GetOrdinal("TempSeq");
                if (!idr.IsDBNull(index))
                {
                    entity.TempSeq = idr.GetString(index);
                }



                index = idr.GetOrdinal("ChargeID");
                if (!idr.IsDBNull(index))
                {
                    entity.ChargeID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("ChargeCode");
                if (!idr.IsDBNull(index))
                {
                    entity.ChargeCode = idr.GetString(index);
                }

                index = idr.GetOrdinal("Seq");
                if (!idr.IsDBNull(index))
                {
                    entity.Seq = idr.GetInt32(index);
                }


                index = idr.GetOrdinal("InvSAmount");
                if (!idr.IsDBNull(index))
                {
                    entity.InvSAmount = idr.GetDecimal(index);
                }

                index = idr.GetOrdinal("InvSVATAmount");
                if (!idr.IsDBNull(index))
                {
                    entity.InvSVATAmount = idr.GetDecimal(index);
                }

                index = idr.GetOrdinal("InvSTotalAmount");
                if (!idr.IsDBNull(index))
                {
                    entity.InvSTotalAmount = idr.GetDecimal(index);
                }

                lists.Add(entity);
            }
            return lists;
        }

        private string GetInvoiceInfoSQL()
        {

            #region info
            //select FM.InvoiceNo,FM.ProductLine,FM.IDType,FM.SourceID, 
            //InvoiceDate = (rtrim(convert(char,datepart(year,FM.InvoiceDate))) 
            //+ '-' + right('0' + rtrim(convert(char,datepart(month,FM.InvoiceDate))),2) 
            //+ '-' + right('0' + rtrim(convert(char,datepart(day,FM.InvoiceDate))),2)), 
            //FM.CurrencyCode,FM.InvoiceAmount,FM.Master,FM.House,
            //FM.ModeCode,FM.UpdatedDate,FM.[ID],FM.BILLTO,FM.InvoiceType,isnull(AEH.[DESC],'') as Remarks, 
            //r.convertrate as STransactionRate_USD, 
            //convert(decimal(18,2),round(FM.InvoiceAmount*r.convertrate,2)) as INVSTotalAmount_USD 
            //from FMOPSource FM 
            //left join ReSM..SMGlobalRate r 
            //on CONVERT(VARCHAR(10),FM.InvoiceDate,120)=CONVERT(VARCHAR(10),r.ratedate,120) 
            //left join aehawb AEH on AEH.id = FM.sourceid where FM.InvoiceNo = ''
            //and r.tocurrency ='USD' and r.Fromcurrency = FM.CurrencyCode

            //select CustomerName,addr = isnull(CustomerAddress1,'') +  isnull(CustomerAddress2,'') 
            //+ isnull(CustomerAddress3,''), CI.CityName,
            //(Select statecode from resm.dbo.smstate where hqid = CI.StateID) as StateCode,
            //(Select countrycode from resm.dbo.smCountry where hqid = CI.CountryID) as CountryCode,
            //CU.zip as Postal
            //from SMCustomerOne CU ,
            //Left join resm.dbo.smcity CI on CI.HQID = CU.CityID 
            //where CU.CustomerID ='xxxxxx'
            #endregion info

            return string.Format(@"Select FM.StationID,FM.TempSeq,FM.ProductLine,FM.IDType,convert(int,FM.SourceID) SourceID,FM.ModeCode
                    ,FM.InvoiceNo,FM.InvoiceDate,FM.InvoiceType,FM.CurrencyCode InvoiceCurrency,FM.InvoiceAmount,FM.SalesAmount,FM.VATAmount
                    ,FM.CustomerID,FM.Master,FM.House,FM.Lot,FM.PayTerms
                    ,FM.UpdatedDate,FM.BILLTO,0 BILLTO_ALT,FM.BillAddress,FM.Remark
                  
                    From FMOpsource FM
                    ");
        }

        public EntityInvoice InvocieInfoGet(string StationID, string InvoiceNo)
        {
            IDataReader idr = this.DB.ExecuteReader(
                string.Format(GetInvoiceInfoSQL() + " Where FM.StationID='{0}' and FM.InvoiceNo='{1}'", StationID, AdjustToSQL(InvoiceNo))
                );

            List<EntityInvoice> lists = BuildInvoiceSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;
        }
        public EntityInvoice InvocieInfoGet(string StationID, string IDType, string ModeCode, int SourceID)
        {
            IDataReader idr = this.DB.ExecuteReader(
                string.Format(GetInvoiceInfoSQL() + " Where FM.StationID='{0}' and FM.IDType='{1}' and FM.SourceID={2} and ModeCode='{3}'"
                    , StationID, IDType, SourceID, ModeCode)
                );

            List<EntityInvoice> lists = BuildInvoiceSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;
        }

        public List<EntityInvoiceDetail> InvocieDetailGet(string StationID, string TempSeq)
        {
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select top 10 D.StationID,D.TempSeq,D.ChargeID,CC.ChargeCode
                            ,D.Seq,D.InvSAmount,D.InvSVATAmount,D.InvSTotalAmount
                        from fminvoicedetail D
                        Inner join ReSM..SMChargeCode CC on D.ChargeID=CC.HQID
                        where StationID='{0}' and TempSeq='{1}'"
                , StationID, TempSeq);
            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());

            return BuildInvoiceDetailSelectList(idr);
        }
        
        public EntityInvoice InvoiceInfoGetAll(string StationID, string InvoiceNo)
        {
            EntityInvoice ldtInvoice = this.InvocieInfoGet(StationID, InvoiceNo);

            if (ldtInvoice == null)
                return null;

            #region Shipment Information
            EntityAMS ldtAMS = null;
            EntityOMS ldtOMS = null;

            string strStationID = ldtInvoice.StationID;
            int HouseID = ldtInvoice.SourceID;

            string IDType = ldtInvoice.IDType.ToUpper();
            if (IDType == "H" || IDType == "D" || IDType == "SM")
            {
                switch (ldtInvoice.ModeCode.ToUpper())
                {
                    case "AE":
                        ldtAMS = this.AMSAEHAWBByHAWBID(strStationID, HouseID, IDType);
                        break;
                    case "AI":
                        ldtAMS = this.AMSAIHAWBByHAWBID(strStationID, HouseID);
                        break;
                    case "3A":
                        ldtAMS = this.AMS3AByTPID(strStationID, HouseID);
                        break;
                    case "OE":
                        switch (ldtInvoice.IDType.ToUpper())
                        {
                            case "D":
                                ldtOMS = this.OMSOEDBLByMBLID(strStationID, HouseID);
                                break;
                            case "H":
                                ldtOMS = this.OMSOEHBLByHBLID(strStationID, HouseID);
                                break;
                        }
                        break;
                    case "OI":
                        switch (ldtInvoice.IDType.ToUpper())
                        {
                            case "H":
                                ldtOMS = this.OMSOIHBLByHBLID(strStationID, HouseID);
                                break;
                            case "D":
                                //throw new Exception("");
                                break;
                        }
                        break;
                    case "3O":
                        ldtOMS = this.OMS3OByTPID(strStationID, HouseID);
                        break;
                    default:
                        throw new Exception("Did not include this type of Mode:" + ldtInvoice.ModeCode.ToUpper());
                    //break;
                }

                EntityShipment ldtShipment = null;
                if (ldtAMS != null)
                {
                    if (ldtAMS.PortOfDEPT > 0)
                    {
                        string strCityID = string.Format("{0}", ldtAMS.PortOfDEPT);
                        if (!ldtInvoice.ItemsCityInfo.ContainsKey(strCityID))
                        {
                            EntityCityInfo enCity = mgrReSM.SMCityInfo(ldtAMS.PortOfDEPT);
                            if (enCity != null) ldtInvoice.ItemsCityInfo.Add(strCityID, enCity);
                        }
                    }

                    if (ldtAMS.PortOfDSTN > 0)
                    {
                        string strCityID = string.Format("{0}", ldtAMS.PortOfDSTN);
                        if (!ldtInvoice.ItemsCityInfo.ContainsKey(strCityID))
                        {
                            EntityCityInfo enCity = mgrReSM.SMCityInfo(ldtAMS.PortOfDSTN);
                            if (enCity != null) ldtInvoice.ItemsCityInfo.Add(strCityID, enCity);
                        }
                    }

                    ldtShipment = (EntityShipment)ldtAMS;
                    ldtInvoice.ItemsAMS.Add(ldtAMS);

                    if (ldtAMS.CarrierBooking!=null && ldtAMS.CarrierBooking.PortOfDEPT > 0)
                    {
                        string strPortID = string.Format("{0}:{1}", PortType.AirPort, ldtAMS.CarrierBooking.PortOfDEPT);
                        if (!ldtInvoice.ItemsPortInfo.ContainsKey(strPortID))
                        {
                            EntityPortInfo enPort = mgrReSM.SMPortInfo(ldtAMS.CarrierBooking.PortOfDEPT, PortType.AirPort);
                            if (enPort != null) ldtInvoice.ItemsPortInfo.Add(strPortID, enPort);
                        }
                    }

                    if (ldtAMS.CarrierBooking != null && ldtAMS.CarrierBooking.PortOfDSTN > 0)
                    {
                        string strPortID = string.Format("{0}:{1}", PortType.AirPort, ldtAMS.CarrierBooking.PortOfDSTN);
                        if (!ldtInvoice.ItemsPortInfo.ContainsKey(strPortID))
                        {
                            EntityPortInfo enPort = mgrReSM.SMPortInfo(ldtAMS.CarrierBooking.PortOfDSTN, PortType.AirPort);
                            if (enPort != null) ldtInvoice.ItemsPortInfo.Add(strPortID, enPort);
                        }
                    }
                }
                if (ldtOMS != null)
                {
                    if (ldtOMS.PortOfDEPT > 0)
                    {
                        string strCityID = string.Format("{0}:{1}", PortType.SeaPort, ldtOMS.PortOfDEPT);
                        if (!ldtInvoice.ItemsPortInfo.ContainsKey(strCityID))
                        {
                            EntityPortInfo enCity = mgrReSM.SMPortInfo(ldtOMS.PortOfDEPT, PortType.SeaPort);
                            if (enCity != null) ldtInvoice.ItemsPortInfo.Add(strCityID, enCity);
                        }
                    }

                    if (ldtOMS.PortOfDSTN > 0)
                    {
                        string strCityID = string.Format("{0}:{1}", PortType.SeaPort, ldtOMS.PortOfDSTN);
                        if (!ldtInvoice.ItemsPortInfo.ContainsKey(strCityID))
                        {
                            EntityPortInfo enCity = mgrReSM.SMPortInfo(ldtOMS.PortOfDSTN, PortType.SeaPort);
                            if (enCity != null) ldtInvoice.ItemsPortInfo.Add(strCityID, enCity);
                        }
                    }

                    ldtShipment = (EntityShipment)ldtOMS;
                    ldtInvoice.ItemsOMS.Add(ldtOMS);
                }

                if (ldtShipment != null)
                {

                    ldtShipment.MilestoneLists = mgrReSM.SMMilestoneList(ldtShipment.HouseNo);

                    if (ldtShipment.Shipper > 0)
                    {   
                        string strCustomerAddressKey = string.Format("{0}:{1}", ldtShipment.Shipper, ldtShipment.Shipper_ALT);
                        if (!ldtInvoice.ItemsCustomerAddress.ContainsKey(strCustomerAddressKey))
                        {
                            EntityCustomerAddress enAddress = this.SMCompanyAddress_Def(ldtShipment.Shipper,ldtShipment.Shipper_ALT);
                            if (enAddress != null) ldtInvoice.ItemsCustomerAddress.Add(strCustomerAddressKey, enAddress);
                        }
                    }
                    if (ldtShipment.CNEE > 0)
                    {
                        string strCustomerAddressKey = string.Format("{0}:{1}", ldtShipment.CNEE, ldtShipment.CNEE_ALT);
                        if (!ldtInvoice.ItemsCustomerAddress.ContainsKey(strCustomerAddressKey))
                        {
                            EntityCustomerAddress enAddress = this.SMCompanyAddress_Def(ldtShipment.CNEE, ldtShipment.CNEE_ALT);
                            if (enAddress != null) ldtInvoice.ItemsCustomerAddress.Add(strCustomerAddressKey, enAddress);
                        }
                    }

                    if (ldtShipment.PlaceOfDelivery > 0)
                    {
                        string strCityID = string.Format("{0}", ldtShipment.PlaceOfDelivery);
                        if (!ldtInvoice.ItemsCityInfo.ContainsKey(strCityID))
                        {
                            EntityCityInfo enCity = mgrReSM.SMCityInfo(ldtShipment.PlaceOfDelivery);
                            if (enCity != null) ldtInvoice.ItemsCityInfo.Add(strCityID, enCity);
                        }
                    }

                    if (ldtShipment.PlaceOfReceipt > 0)
                    {
                        string strCityID = string.Format("{0}", ldtShipment.PlaceOfReceipt);
                        if (!ldtInvoice.ItemsCityInfo.ContainsKey(strCityID))
                        {
                            EntityCityInfo enCity = mgrReSM.SMCityInfo(ldtShipment.PlaceOfReceipt);
                            if (enCity != null) ldtInvoice.ItemsCityInfo.Add(strCityID, enCity);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Did not include this type of " + (IDType == "" ? "Empty IDType" : IDType));
            }

            #endregion Shipment Information

            if (!string.IsNullOrEmpty(ldtInvoice.TempSeq))
            {
                List<EntityInvoiceDetail> InvDtS = this.InvocieDetailGet(StationID, ldtInvoice.TempSeq);
                foreach (EntityInvoiceDetail ld in InvDtS)
                {
                    ldtInvoice.ItemsInvoiceDetail.Add(ld.Seq.ToString(), ld);
                }
            }


            int BillTo = Convert.ToInt32(ldtInvoice.BILLTO);
            int BillToAltID = 0;
            if (BillTo > 0)
            {
                string strCustomerAddressKey = string.Format("{0}:{1}", BillTo, BillToAltID);
                if (!ldtInvoice.ItemsCustomerAddress.ContainsKey(strCustomerAddressKey))
                {
                    EntityCustomerAddress enAddress = mgrReSM.SMCustomerAddress_Alt(BillTo, BillToAltID);
                    if (enAddress != null) ldtInvoice.ItemsCustomerAddress.Add(strCustomerAddressKey, enAddress);
                }
            }
            //ldtInvoice.item

            return ldtInvoice;
        }

    }

}
