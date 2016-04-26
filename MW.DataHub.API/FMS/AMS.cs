using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DIMERCO.SDK2.Base;
using DIMERCO.SDK2.Data;
using MW.DataHub.API.BO;

namespace MW.DataHub.API.ValuePlus
{
    #region AMS Entity
    [Serializable]
    public class EntityAMS : EntityShipment
    {
        private Nullable<decimal> _VWT;
        public Nullable<decimal> VWT
        {
            get { return _VWT; }
            set { _VWT = value; }
        }

        private Nullable<decimal> _CWT;
        public Nullable<decimal> CWT
        {
            get { return _CWT; }
            set { _CWT = value; }
        }

        private string _ClassRate;
        public string ClassRate
        {
            get { return _ClassRate; }
            set { _ClassRate = value; }
        }

        public Nullable<decimal> Rate { get; set; }
        public EntityCarrierBooking CarrierBooking { get; set; }
        public List<EntityDimension> DIMList { get; set; }
    }

    public class EntityCarrierBooking
    {
        public int CBID { get; set; }
        public int PortOfDEPT { get; set; }
        public int PortOfDSTN { get; set; }
        public string BookedFLT { get; set; }
        public string OnboardFLT { get; set; }

    }

    public class EntityDimension
    {
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string UOM { get; set; }
        public int PCS { get; set; }

    }
    #endregion AMS Entity

    public partial interface IBPValuePlus : IBase<object>
    {
        /// <summary>
        /// 获取Air Export HAWB的信息
        /// </summary>
        /// <param name="StationID"></param>
        /// <param name="HAWBID"></param>
        /// <param name="IDType">主要区别是Direct和一般的区别</param>
        /// <returns></returns>
        EntityAMS AMSAEHAWBByHAWBID(string StationID, int HAWBID, string IDType);
        /// <summary>
        /// 获取Air IMport HAWB的信息,因为进口没有Direct的shipment，所以不带D的参数
        /// </summary>
        /// <param name="StationID"></param>
        /// <param name="HAWBID"></param>
        /// <returns></returns>
        EntityAMS AMSAIHAWBByHAWBID(string StationID, int HAWBID);
        /// <summary>
        /// 获取3PA的信息
        /// </summary>
        /// <param name="StationID"></param>
        /// <param name="TPID"></param>
        /// <returns></returns>
        EntityAMS AMS3AByTPID(string StationID, int TPID);
    }

    partial class clsBPValuePlus : clsBase<object>, IBPValuePlus
    {
        private List<EntityAMS> BuildAMSSelectList(IDataReader idr)
        {
            List<EntityAMS> lists = new List<EntityAMS>();
            while (idr.Read())
            {
                EntityAMS entity = new EntityAMS();
                int index = 0;

                index = idr.GetOrdinal("StationID");
                if (!idr.IsDBNull(index))
                {
                    entity.StationID = idr.GetString(index);
                }

                index = idr.GetOrdinal("HAWBID");
                if (!idr.IsDBNull(index))
                {
                    entity.HouseID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("HAWBNo");
                if (!idr.IsDBNull(index))
                {
                    entity.HouseNo = idr.GetString(index);
                }

                index = idr.GetOrdinal("ModeCode");
                if (!idr.IsDBNull(index))
                {
                    entity.ModeCode = idr.GetString(index);
                }

                index = idr.GetOrdinal("IDType");
                if (!idr.IsDBNull(index))
                {
                    entity.IDType = idr.GetString(index);
                }

                index = idr.GetOrdinal("Customer");
                if (!idr.IsDBNull(index))
                {
                    entity.Customer = idr.GetInt32(index).ToString();
                }

                index = idr.GetOrdinal("Shipper");
                if (!idr.IsDBNull(index))
                {
                    entity.Shipper = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("ShipperInfo");
                if (!idr.IsDBNull(index))
                {
                    entity.ShipperInfo = idr.GetString(index).ToString();
                }

                index = idr.GetOrdinal("CNEE");
                if (!idr.IsDBNull(index))
                {
                    entity.CNEE = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("CNEEInfo");
                if (!idr.IsDBNull(index))
                {
                    entity.CNEEInfo = idr.GetString(index).ToString();
                }

                index = idr.GetOrdinal("PlaceOfReceipt");
                if (!idr.IsDBNull(index))
                {
                    entity.PlaceOfReceipt = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("PortOfDEPT");
                if (!idr.IsDBNull(index))
                {
                    entity.PortOfDEPT = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("PortOfDSTN");
                if (!idr.IsDBNull(index))
                {
                    entity.PortOfDSTN = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("PlaceOfDelivery");
                if (!idr.IsDBNull(index))
                {
                    entity.PlaceOfDelivery = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("TradeTerm");
                if (!idr.IsDBNull(index))
                {
                    entity.TradeTerm = idr.GetString(index);
                }

                index = idr.GetOrdinal("ServiceLevel");
                if (!idr.IsDBNull(index))
                {
                    entity.ServiceLevel = idr.GetString(index);
                }

                index = idr.GetOrdinal("NatureOfGoods");
                if (!idr.IsDBNull(index))
                {
                    entity.NatureOfGoods = idr.GetString(index);
                }



                index = idr.GetOrdinal("PCS");
                if (!idr.IsDBNull(index))
                {
                    entity.PCS = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("PCSUOM");
                if (index >= 0 && !idr.IsDBNull(index))
                {
                    entity.PCSUOM = idr.GetString(index);
                }

                index = idr.GetOrdinal("GWT");
                if (!idr.IsDBNull(index))
                {
                    entity.GWT = idr.GetDecimal(index);
                }

                index = idr.GetOrdinal("VWT");
                if (!idr.IsDBNull(index))
                {
                    entity.VWT = idr.GetDecimal(index);
                }

                index = idr.GetOrdinal("CWT");
                if (!idr.IsDBNull(index))
                {
                    entity.CWT = idr.GetDecimal(index);
                }

                index = idr.GetOrdinal("WTUOM");
                if (index >= 0 && !idr.IsDBNull(index))
                {
                    entity.WTUOM = idr.GetString(index);
                }

                 index = idr.GetOrdinal("DESC");
                if (index >= 0 && !idr.IsDBNull(index))
                {
                    entity.DESC = idr.GetString(index);
                }

                 index = idr.GetOrdinal("FRT");
                if (index >= 0 && !idr.IsDBNull(index))
                {
                    entity.FreightPayType = idr.GetString(index);
                }

                 index = idr.GetOrdinal("Other");
                if (index >= 0 && !idr.IsDBNull(index))
                {
                    entity.OtherPayType = idr.GetString(index);
                }

                 index = idr.GetOrdinal("Move");
                if (index >= 0 && !idr.IsDBNull(index))
                {
                    entity.Movement = idr.GetString(index);
                }

                index = idr.GetOrdinal("ClassRate");
                if (index >= 0 && !idr.IsDBNull(index))
                {
                    entity.ClassRate = idr.GetString(index);
                }

                index = idr.GetOrdinal("Rate");
                if (!idr.IsDBNull(index))
                {
                    entity.Rate = idr.GetDecimal(index);
                }

                index = idr.GetOrdinal("FreightSignby");
                if (index >= 0 && !idr.IsDBNull(index))
                {
                    entity.FreightSignby = idr.GetString(index);
                }
                
                #region Master
                index = idr.GetOrdinal("MStationID");
                if (!idr.IsDBNull(index))
                {
                    entity.MStationID = idr.GetString(index);
                }

                index = idr.GetOrdinal("MAWBID");
                if (!idr.IsDBNull(index))
                {
                    entity.MasterID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("MAWBNo");
                if (!idr.IsDBNull(index))
                {
                    entity.MasterNo = idr.GetString(index);
                }

                index = idr.GetOrdinal("LotNo");
                if (!idr.IsDBNull(index))
                {
                    entity.LotNo = idr.GetString(index);
                }

               
                #endregion
                lists.Add(entity);








            }
            return lists;
        }

        private List<EntityPO> BuildPOSelectList(IDataReader idr)
        {
            ///List<EntityPO>
            List<EntityPO> lists = new List<EntityPO>();
            while (idr.Read())
            {
                EntityPO entity = new EntityPO();
                int index = 0;

                index = idr.GetOrdinal("PONo");
                if (!idr.IsDBNull(index))
                {
                    entity.PONo = idr.GetString(index);
                }

                index = idr.GetOrdinal("CommInvNo");
                if (!idr.IsDBNull(index))
                {
                    entity.CommInvNo = idr.GetString(index);
                }

                index = idr.GetOrdinal("InvoiceDate");
                if (!idr.IsDBNull(index))
                {
                    entity.InvoiceDate = idr.GetDateTime(index);
                }
                lists.Add(entity);

            }
            return lists;
        }

        private List<EntityDimension> BuildDIMSelectList(IDataReader idr)
        {
            ///List<EntityPO>
            List<EntityDimension> lists = new List<EntityDimension>();
            while (idr.Read())
            {
                EntityDimension entity = new EntityDimension();
                int index = 0;

                index = idr.GetOrdinal("Width");
                if (!idr.IsDBNull(index))
                {
                    entity.Width = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("Length");
                if (!idr.IsDBNull(index))
                {
                    entity.Length = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("Height");
                if (!idr.IsDBNull(index))
                {
                    entity.Height = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("PCS");
                if (!idr.IsDBNull(index))
                {
                    entity.PCS = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("UOM");
                if (!idr.IsDBNull(index))
                {
                    entity.UOM = idr.GetString(index);
                }
                lists.Add(entity);

            }
            return lists;
        }

        private EntityCarrierBooking BuildCarrierBookingSelectList(IDataReader idr)
        {
            EntityCarrierBooking list = new EntityCarrierBooking();

            int iRow = 0;
            while (idr.Read())
            {
                int index = 0;

                if (iRow == 0)
                {
                    index = idr.GetOrdinal("BookedFLT");
                    if (!idr.IsDBNull(index))
                    {
                        list.BookedFLT = idr.GetString(index);
                    }

                    index = idr.GetOrdinal("OnboardFLT");
                    if (!idr.IsDBNull(index))
                    {
                        list.OnboardFLT = idr.GetString(index);
                    }

                    index = idr.GetOrdinal("PortOfDEPT");
                    if (!idr.IsDBNull(index))
                    {
                        list.PortOfDEPT = idr.GetInt32(index);
                    }
                }


                index = idr.GetOrdinal("PortOfDSTN");
                if (!idr.IsDBNull(index))
                {
                    list.PortOfDSTN = idr.GetInt32(index);
                }
            }
            return list;
        }
        

        #region MAWB Part
        public DataTable AMSAEMAWBGetByMAWBID(string StationID, int MAWBID)
        {
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select M.StationID,M.ID MAWBID,'AE' Mode
,M.MAWBNo,M.LotNo,M.Shipper,M.CNEE
,M.PCS,M.PCSUOM,M.GWT,M.VWT,M.CWT,M.WTUOM
,M.PortOfDEPT,M.PortOfDSTN
From AEMAWB M "
+ " where M.StationID='{0}' and M.ID='{1}'"
                , StationID, MAWBID);
            DataSet lds = this.DB.ExecuteDataSet(lsb.ToString());
            return lds.Tables[0];
        }
      
        public DataTable AMSAIMAWBGetByMAWBID(string StationID, int MAWBID)
        {
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select M.StationID,M.ID MAWBID,'AE' Mode
,M.MAWBNo,M.LotNo,M.Shipper,M.CNEE
,M.PCS,M.PCSUOM,M.GWT,M.VWT,M.CWT,M.WTUOM
,M.PortOfDEPT,M.PortOfDSTN
From AIMAWB M "
+ " where M.StationID='{0}' and M.ID='{1}'"
                , StationID, MAWBID);
            DataSet lds = this.DB.ExecuteDataSet(lsb.ToString());
            return lds.Tables[0];
        }

        #endregion


        #region HAWB Part
        public EntityAMS AMSAEHAWBByHAWBID(string StationID, int HAWBID, string IDType)
        {
            #region HAWB information
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select H.StationID,H.ID HAWBID,'AE' ModeCode,H.AWBType IDType
,case when H.AWBType='D' then M.MAWBNo else H.HAWBNo end HAWBNo
,H.Customer,H.Shipper,P.ShipperInfo,H.CNEE,P.CNEEInfo
,H.PortOfDEPT,H.PortOfDSTN,H.PlaceOfRCPT PlaceOfReceipt,H.PlaceOfDELV PlaceOfDelivery
,H.TradeTerm,H.ServiceLevel,'' NatureOfGoods
,H.ActPCS PCS,H.ActPCSUOM PCSUOM,convert(decimal(18,2),H.GWT) GWT,convert(decimal(18,2),H.VWT) VWT,convert(decimal(18,2),H.CWT) CWT,H.WTUOM
,H.[DESC],H.FRT,H.Other,H.Move,H.ClassRate
,convert(decimal(18,2),H.Rate) Rate,H.Currency
--,H.Remark,H.Marks
,(Select SCHCargoSignBy from AEPOD Where StationID=H.StationID and HAWBID=H.ID) FreightSignBy
,M.StationID MStationID,H.MAWBID,M.MAWBNo,M.LotNo
From AEHAWB H 
Left Join AEMAWB M on H.MAWBID=M.ID" + (this.IsCentralData ? " and H.DBID=M.DBID" : "")
                //一下为区分DAWBandHAWB
+ (IDType == "D" ?
" Left Join AEMAWBPreview P on P.StationID=M.StationID and P.MAWBID=M.ID" :
" Left Join AEHAWBPreview P on P.StationID=H.StationID and P.HAWBID=H.ID"
)
+ " where H.StationID='{0}' and H.ID='{1}'"
                , StationID, HAWBID);

            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityAMS> lists = BuildAMSSelectList(idr);
            if (lists.Count() == 0)
                return null;
            #endregion HAWB information

            EntityAMS list = lists[0];
            #region Carrier Booking
            lsb.Remove(0, lsb.Length);
            lsb.AppendFormat(@"Select ID CBID,DEPT PortOfDEPT,DSTN PortOfDSTN,BookedFLT,OnBoardFLT,ETD,ETA,ATD,ATA
From AECarrierBooking Where StationID='{0}' and MAWBID='{1}' order by ID"
                , list.MStationID, list.MasterID);

            idr = this.DB.ExecuteReader(lsb.ToString());
            EntityCarrierBooking listCB = BuildCarrierBookingSelectList(idr);
            list.CarrierBooking = listCB;

            #endregion Carrier Booking

            #region POInformation
            lsb.Remove(0, lsb.Length);
            lsb.AppendFormat(@"Select PONo,Item,Date InvoiceDate, InvoiceNo CommInvNo
from AEHAWBPO H
Where H.StationID='{0}' and H.HAWBID='{1}'"
                , StationID, HAWBID);

            idr = this.DB.ExecuteReader(lsb.ToString());
            list.POList = BuildPOSelectList(idr);
            #endregion


            #region Dimension
            lsb.Remove(0, lsb.Length);
            lsb.AppendFormat(@"Select convert(int,Length) Length,convert(int,Width) Width,convert(int,Height) Height, PCS,DIMUOM UOM
from AEHAWBDIM H
Where H.StationID='{0}' and H.HAWBID='{1}'"
                , StationID, HAWBID);

            idr = this.DB.ExecuteReader(lsb.ToString());
            list.DIMList = BuildDIMSelectList(idr);
            #endregion
            return list;
        }

        public EntityAMS AMSAIHAWBByHAWBID(string StationID, int HAWBID)
        {
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select H.StationID,H.ID HAWBID,'AI' ModeCode,H.AWBType IDType
,H.HAWBNo
,H.Customer,H.Shipper,P.ShipperInfo,H.CNEE,P.CNEEInfo
,H.PortOfDEPT,H.PortOfDSTN,H.PlaceOfRCPT PlaceOfReceipt,H.PlaceOfDELV PlaceOfDelivery
,H.TradeTerm,H.ServiceLevel,'' NatureOfGoods
,H.ActPCS PCS,H.ActPCSUOM PCSUOM,convert(decimal(18,2),H.GWT) GWT,convert(decimal(18,2),H.VWT) VWT,convert(decimal(18,2),H.CWT) CWT,H.WTUOM
,H.[DESC],H.FRT,H.Other,H.Move,H.ClassRate
,convert(decimal(18,2),H.Rate) Rate,H.Currency
--,H.Remark,H.Marks
,SCHCargoSignBy FreightSignBy
,M.StationID MStationID,H.MAWBID,M.MAWBNo,M.LotNo

From AIHAWB H 
Left Join AIHAWBPreview P on H.StationID=P.StationID and H.ID=P.HAWBID
Left Join AIMAWB M on H.MAWBID=M.ID" + (this.IsCentralData ? " and H.DBID=M.DBID" : "")
+ " where H.StationID='{0}' and H.ID='{1}'"
                , StationID, HAWBID);

            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityAMS> lists = BuildAMSSelectList(idr);
            if (lists.Count() == 0)
                return null;

            EntityAMS list = lists[0];
            #region Carrier Booking
            lsb.Remove(0, lsb.Length);

            lsb.AppendFormat(@"Select ID CBID,DEPT PortOfDEPT,DSTN PortOfDSTN,BookedFLT,OnBoardFLT,ETD,ETA,ATD,ATA
From AICarrierBooking Where StationID='{0}' and MAWBID='{1}' order by ID"
               , list.MStationID, list.MasterID);

            idr = this.DB.ExecuteReader(lsb.ToString());
            EntityCarrierBooking listCB = BuildCarrierBookingSelectList(idr);
            list.CarrierBooking = listCB;
            #endregion Carrier Booking

            #region POInformation
            lsb.Remove(0, lsb.Length);
            lsb.AppendFormat(@"Select PONo,Item,Date InvoiceDate, InvoiceNo CommInvNo
from AEHAWBPO H
Where H.StationID='{0}' and H.HAWBID='{1}'"
                   , StationID, HAWBID);

            idr = this.DB.ExecuteReader(lsb.ToString());
            list.POList = BuildPOSelectList(idr);
            #endregion


            #region Dimension
            lsb.Remove(0, lsb.Length);
            lsb.AppendFormat(@"Select convert(int,Length) Length,convert(int,Width) Width,convert(int,Height) Height, PCS,DIMUOM UOM
from AIHAWBDIM H
Where H.StationID='{0}' and H.HAWBID='{1}'"
                , StationID, HAWBID);

            idr = this.DB.ExecuteReader(lsb.ToString());
            list.DIMList = BuildDIMSelectList(idr);
            #endregion
            return list;
        }

        public EntityAMS AMS3AByTPID(string StationID, int TPID)
        {
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select H.StationID,H.ID HAWBID,'3A' ModeCode,H.SourceType IDType
,H.HouseNo HAWBNo
,H.CustomerID Customer,H.Shipper,'' ShipperInfo,H.CNEE,'' CNEEInfo
,H.Orgn PortOfDEPT,H.DSTN PortOfDSTN,H.Receipt PlaceOfReceipt,H.Delivery PlaceOfDelivery
,'' TradeTerm,H.ServiceLevel,'' NatureOfGoods
,H.ActPCS PCS,H.ActPCSUOM PCSUOM,null GWT,convert(decimal,H.VWT) VWT,convert(decimal,H.CWT) CWT,H.WTUOM
,H.netureOfGood [DESC],'' FRT,'' Other,'' Move,'' ClassRate
,'' Rate,'' Currency
--,H.Remark,H.Marks
,'' FreightSignBy
,'' MStationID,0 MAWBID,H.MasterNo MAWBNo,H.Lot LotNo
From TPAirData H 
where H.StationID='{0}' and H.ID='{1}'"
                , StationID, TPID);

            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityAMS> lists = BuildAMSSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;
        }

        #endregion
    }
}
