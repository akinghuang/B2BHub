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
    #region Entity of OMS
    [Serializable]
    public class EntityOMS : EntityShipment
    {
        private string _Vessel;
        public string Vessel
        {
            get { return _Vessel; }
            set { _Vessel = value; }
        }

        private string _Voyage;
        public string Voyage
        {
            get { return _Voyage; }
            set { _Voyage = value; }
        }


        private int _FinalDSTN;
        public int FinalDSTN
        {
            get { return _FinalDSTN; }
            set { _FinalDSTN = value; }
        }

        private string _MoveType;
        /// <summary>
        /// CFS/CY CFS/CFS CY/CY CY/CFS
        /// CFS == LCL  CY == FCL
        /// </summary>
        public string MoveType
        {
            get { return _MoveType; }
            set { _MoveType = value; }
        }

    }
    #endregion Entity of OMS

    public partial interface IBPValuePlus : IBase<object>
    {
        EntityOMS OMSOEHBLByHBLID(string StationID, int HBLID);
    }

    partial class clsBPValuePlus : clsBase<object>, IBPValuePlus
    {
        #region MBL Part
        public DataTable OMSOEMBLGetByMBLID(string StationID, int MBLID)
        {
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select M.StationID,M.ID MBLID,'AE' Mode
,M.MBLNo,M.LotNo,M.Shipper,M.CNEE
,M.PCS,M.PCSUOM,M.GWT,M.VWT,M.CWT,M.WTUOM
,M.PortOfDEPT,M.PortOfDSTN
From AEMBL M "
+ " where M.StationID='{0}' and M.ID='{1}'"
                , StationID, MBLID);
            DataSet lds = this.DB.ExecuteDataSet(lsb.ToString());
            return lds.Tables[0];
        }

        public DataTable OMSOIMBLGetByMBLID(string StationID, int MBLID)
        {
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select M.StationID,M.ID MBLID,'AE' Mode
,M.MBLNo,M.LotNo,M.Shipper,M.CNEE
,M.PCS,M.PCSUOM,M.GWT,M.VWT,M.CWT,M.WTUOM
,M.PortOfDEPT,M.PortOfDSTN
From AIMBL M "
+ " where M.StationID='{0}' and M.ID='{1}'"
                , StationID, MBLID);
            DataSet lds = this.DB.ExecuteDataSet(lsb.ToString());
            return lds.Tables[0];
        }

        #endregion


        #region HBL Part
        private List<EntityOMS> BuildOMSSelectList(IDataReader idr)
        {
            List<EntityOMS> lists = new List<EntityOMS>();
            while (idr.Read())
            {
                EntityOMS entity = new EntityOMS();
                int index = 0;

                index = idr.GetOrdinal("StationID");
                if (!idr.IsDBNull(index))
                {
                    entity.StationID = idr.GetString(index);
                }

                index = idr.GetOrdinal("HBLID");
                if (!idr.IsDBNull(index))
                {
                    entity.HouseID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("HBLNo");
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
                    entity.ShipperInfo = idr.GetString(index);
                }

                index = idr.GetOrdinal("CNEE");
                if (!idr.IsDBNull(index))
                {
                    entity.CNEE = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("CNEEInfo");
                if (!idr.IsDBNull(index))
                {
                    entity.CNEEInfo = idr.GetString(index);
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

                index = idr.GetOrdinal("FinalDSTN");
                if (!idr.IsDBNull(index))
                {
                    entity.FinalDSTN = idr.GetInt32(index);
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

                index = idr.GetOrdinal("WTUOM");
                if (index>=0 && !idr.IsDBNull(index))
                {
                    entity.WTUOM = idr.GetString(index);
                }

                index = idr.GetOrdinal("DESC");
                if (index >= 0 && !idr.IsDBNull(index))
                {
                    entity.DESC = idr.GetString(index);
                }

                index = idr.GetOrdinal("FreightPaytype");
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

                index = idr.GetOrdinal("Movetype");
                if (index >= 0 && !idr.IsDBNull(index))
                {
                    entity.MoveType = idr.GetString(index);
                }
                

                #region Master
                index = idr.GetOrdinal("MStationID");
                if (!idr.IsDBNull(index))
                {
                    entity.MStationID = idr.GetString(index);
                }

                index = idr.GetOrdinal("MBLID");
                if (!idr.IsDBNull(index))
                {
                    entity.MasterID = idr.GetInt32(index);
                }

                index = idr.GetOrdinal("MBLNo");
                if (!idr.IsDBNull(index))
                {
                    entity.MasterNo = idr.GetString(index);
                }

                index = idr.GetOrdinal("LotNo");
                if (!idr.IsDBNull(index))
                {
                    entity.LotNo = idr.GetString(index);
                }

                index = idr.GetOrdinal("Vessel");
                if (!idr.IsDBNull(index))
                {
                    entity.Vessel = idr.GetString(index);
                }

                index = idr.GetOrdinal("Voyage");
                if (!idr.IsDBNull(index))
                {
                    entity.Voyage = idr.GetString(index);
                }
                #endregion
                lists.Add(entity);








            }
            return lists;
        }


        public EntityOMS OMSOEHBLByHBLID(string StationID, int HBLID)
        {
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select H.StationID,H.ID HBLID,H.HBLNo,'OE' ModeCode,'H' IDType
,H.Customer,H.SHPR Shipper,P.shpr ShipperInfo,H.CNEE,P.cnee CNEEInfo
,H.PLoading PortOfDEPT,H.PDischarge PortOfDSTN,H.PReceipt PlaceOfReceipt,H.PDelivery PlaceOfDelivery,H.FinalDest FinalDSTN
,H.TradeType TradeTerm,H.ServiceType ServiceLevel
,(select top 1 Remark1 from resm.dbo.SACodeMaintain where Class='NatureOfGoods' and Contents='OMS' and Title=H.NatureOfGoodsType) NatureOfGoods
,(Select Sum(PCS) from OESO where Sofor ='HBL'  and HBLID = H.ID ) PCS,'' PCSUOM
,(Select Sum(weight) from OESO where Sofor ='HBL' and HBLID = H.ID) GWT,'' WTUOM
,H.Movetype,H.FreightPaytype ,'' [DESC],'' Other, (select MoveCode from resm..smtradeterm where TradeTermCode=H.TradeType) as Move
,M.StationID MStationID,H.MBLID,M.MBLNo,M.LotNo
,M.OceanVoyage Voyage,M.OceanVessel Vessel
From OEHBL H 
Left Join OEFlading P on H.StationID=P.StationID and P.HBLID=H.ID and oetype ='H'
Left Join OEMBL M on H.StationID=M.StationID and H.MBLID=M.ID"
+ " where H.StationID='{0}' and H.ID='{1}'"
                , StationID, HBLID);

            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityOMS> lists = BuildOMSSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;
        }

        public EntityOMS OMSOEDBLByMBLID(string StationID, int MBLID)
        {
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select null StationID,null HBLID,null HBLNo,'OE' ModeCode,'D' IDType
,M.Customer,M.SHPR Shipper,P.shpr ShipperInfo,M.CNEE,P.cnee CNEEInfo
,M.PLoading PortOfDEPT,M.PDischarge PortOfDSTN,M.PReceipt PlaceOfReceipt,M.PDelivery PlaceOfDelivery,M.FinalDest FinalDSTN
,M.TradeType TradeTerm,M.ServiceType ServiceLevel
,(select top 1 Remark1 from resm.dbo.SACodeMaintain where Class='NatureOfGoods' and Contents='OMS' and Title=M.NatureOfGoodsType) NatureOfGoods
,(Select Sum(PCS) from OESO where Sofor ='DMBL'  and MBLID = M.ID ) PCS,'' PCSUOM
,(Select Sum(weight) from OESO where Sofor ='DMBL' and MBLID = M.ID) GWT,'' WTUOM
,M.Movetype,M.FreightPaytype ,'' [DESC],'' Other, (select MoveCode from resm..smtradeterm where TradeTermCode=M.TradeType) as Move
,M.StationID MStationID,M.ID MBLID,M.MBLNo,M.LotNo
,M.OceanVoyage Voyage,M.OceanVessel Vessel
From OEMBL M 
Left Join OEFlading P on M.StationID=P.StationID and P.HBLID=M.ID and oetype ='D'"
+ " where M.MBLType='DMBL' and M.StationID='{0}' and M.ID='{1}'"
                , StationID, MBLID);

            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityOMS> lists = BuildOMSSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;
        }

        public EntityOMS OMSOIHBLByHBLID(string StationID, int HBLID)
        {
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select H.StationID,H.ID HBLID,H.HBLNo,'OI' ModeCode,'H' IDType
,H.Customer,H.SHPR Shipper,P.shpr ShipperInfo,H.CNEE,P.cnee CNEEInfo
,H.PLoading PortOfDEPT,H.PDischarge PortOfDSTN,H.PReceipt PlaceOfReceipt,H.PDelivery PlaceOfDelivery,H.FinalDest FinalDSTN
,H.TradeType TradeTerm,H.ServiceType ServiceLevel
,(select top 1 Remark1 from resm.dbo.SACodeMaintain where Class='NatureOfGoods' and Contents='OMS' and Title=H.NatureOfGoodsType) NatureOfGoods
,(Select Sum(PCS) from OESO where Sofor ='HBL'  and HBLID = H.ID ) PCS,'' PCSUOM
,(Select Sum(weight) from OESO where Sofor ='HBL' and HBLID = H.ID) GWT,'' WTUOM
,H.Movetype,H.FreightPaytype ,'' [DESC],'' Other, (select MoveCode from resm..smtradeterm where TradeTermCode=H.TradeType) as Move
,M.StationID MStationID,H.MBLID,M.MBLNo,M.LotNo
,M.OceanVoyage Voyage,M.OceanVessel Vessel
From OIHBL H 
Left Join OEFlading P on H.StationID=P.StationID and P.HBLID=H.ID and oetype ='H'
Left Join OIMBL M on H.StationID=M.StationID and H.MBLID=M.ID"
+ " where H.StationID='{0}' and H.ID='{1}'"
                , StationID, HBLID);

            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityOMS> lists = BuildOMSSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;
        }

        public EntityOMS OMS3OByTPID(string StationID, int TPID)
        {
            StringBuilder lsb = new StringBuilder();
            lsb.AppendFormat(@"select H.StationID,H.ID HBLID,H.HouseNo HBLNo,'3O' ModeCode,'H' IDType
,H.MasterNo MBLNo,H.Lot LotNo
,H.CustomerID Customer,H.Shipper,p.shpr ShipperInfo,H.CNEE,p.cnee CNEEInfo
,H.POL PortOfDEPT,H.POD PortOfDSTN,H.Receipt PlaceOfReceipt,H.Delivery PlaceOfDelivery,null FinalDSTN
,'' TradeTerm,'' ServiceLevel
,'' NatureOfGoods
,(Select Sum(PCS) from TPOceanDataContainer where TPID = H.ID ) PCS,'' PCSUOM
,(Select Sum(convert(decimal,wt)) from TPOceanDataContainer where TPID = H.ID) GWT,'' WTUOM
,H.Movetype,'' FreightPaytype ,H.[Description] [DESC],'' Other, '' as Move
,null MStationID,null MBLID
,H.Voyage Voyage,H.Vessel Vessel
From TPOceanData H
Left Join TPOceanPreview P on H.ID=P.HBLID"
+ " where H.StationID='{0}' and H.ID='{1}'"
                , StationID, TPID);

            IDataReader idr = this.DB.ExecuteReader(lsb.ToString());
            List<EntityOMS> lists = BuildOMSSelectList(idr);
            if (lists.Count() > 0)
                return lists[0];
            else
                return null;
        }
        #endregion
    }
}
