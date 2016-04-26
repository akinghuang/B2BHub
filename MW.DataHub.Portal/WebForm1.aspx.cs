using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


using System.Collections.Generic;
namespace MW.DataHub.Portal
{
    public class Segment
    {
        List<string> items = new List<string>();
        string _segName = "XX";
        public string SegmentName { get { return _segName; } }
        
        public Segment(string segName)
        {
            _segName = segName;
        }
        public void setElementAt(int index,object element)
        {
            string source = element.ToString();
            setElementAt(index, source);
        }
        public void setElementAt(int index, string element)
        {
            index -= 1;

            if (index < items.Count)
                items[index] = element;
            else
            {
                int listCount = items.Count;
                for (int i = 0; i < index - listCount; i++)
                {
                    items.Add("");
                }
                items.Add(element);
            }
        }
        public string getElementAt(int index)
        {
            if (index < items.Count)
                return items[index];
            else
                return null;
        }

        public string ToString(char sepChar, char endChar)
        {
            System.Text.StringBuilder lsb = new System.Text.StringBuilder(_segName);
            foreach (string s in items)
            {
                lsb.AppendFormat("{0}{1}", sepChar, s);
            }
            lsb.Append(endChar);
            return lsb.ToString();
        }
        
    }

    public class EDITemplate
    {
        protected const string errorProductLine = "Error Productline:{0} in segment:{1}.";
        protected char _sepChar = '*';
        protected char _endChar = '^';
        protected DateTime CurrentDateTime;
        private string TMPISA = "$$$$ISANo";
        protected string _ISA13 = "";
        protected List<Segment> ISAList = new List<Segment>();
        protected List<string> ErrList = new List<string>();

        public EDITemplate()
        {
        }
        public EDITemplate(char sepChar, char endChar)
        {
            _sepChar = sepChar;
            _endChar = endChar;
        }

        public string padSpaces(string element, int length)
        {
            return element.PadRight(length, ' ');
        }
        public string padZero(string element, int length)
        {
            return element.PadLeft(length, '0');
        }
        public string substring(string str, int length)
        {
            if (str.Length > length)
                return str.Substring(0, length);
            else
                return str;
        }

        public string removeDecimal(decimal number)
        {
            return removeDecimal(number.ToString());
        }
        public string removeDecimal(string number)
        {
            string[] s = number.Split('.');
            string part2 = "";
            if (s.Length > 1)
                part2 = s[1];
            return s[0] + part2.PadRight(2,'0').Substring(0,2);
        }

        public string ISA_Sender_ID { get; set; }
        public string ISA_Receiver_ID { get; set; }
        public string GS_Sender_ID { get; set; }
        public string GS_Receiver_ID { get; set; }

        /// <summary>
        /// Add ISA Segment
        /// </summary>
        /// <param name="list"></param>
        public virtual void AddISA()
        {
            #region ISA
            Segment seg = new Segment("ISA");
            seg.setElementAt(1, "00");
            seg.setElementAt(2, padSpaces("", 10));
            seg.setElementAt(3, "00");
            seg.setElementAt(4, padSpaces("", 10));
            seg.setElementAt(5, "ZZ"); //Not 
            seg.setElementAt(6, padSpaces(ISA_Sender_ID, 15));
            seg.setElementAt(7, "ZZ");
            seg.setElementAt(8, padSpaces(ISA_Receiver_ID, 15));
            seg.setElementAt(9, CurrentDateTime.ToString("yyMMdd"));
            seg.setElementAt(10, CurrentDateTime.ToString("HHmm"));
            seg.setElementAt(11, "U");
            seg.setElementAt(12, "00401");
            seg.setElementAt(13, TMPISA);
            seg.setElementAt(14, "0");
            seg.setElementAt(15, "P");
            seg.setElementAt(16, ":");
            ISAList.Add(seg);
            #endregion

            AddGS();

            #region IEA
            seg = new Segment("IEA");
            seg.setElementAt(1, "1");
            seg.setElementAt(2, TMPISA);
            ISAList.Add(seg);
            #endregion
        }

        /// <summary>
        /// Add GS Segment
        /// </summary>
        /// <param name="list"></param>
        public virtual void AddGS()
        {
            #region GS
            //GS*IA*DMER*BBA5900*20140513*1141*000004316*X*004010^
            Segment seg = new Segment("GS");
            seg.setElementAt(1, "IA");
            seg.setElementAt(2, GS_Sender_ID);
            seg.setElementAt(3, GS_Receiver_ID);
            seg.setElementAt(4, CurrentDateTime.ToString("yyyyMMdd"));
            seg.setElementAt(5, CurrentDateTime.ToString("HHmm"));
            seg.setElementAt(6, TMPISA);
            seg.setElementAt(7, "X");
            seg.setElementAt(8, "004010");
            ISAList.Add(seg);
            #endregion

            //Set to loop for later
            GSLoop();

            #region GE
            //GE*1*0000^
            seg = new Segment("GE");
            seg.setElementAt(1, "1");
            seg.setElementAt(2, TMPISA);
            ISAList.Add(seg);
            #endregion
        }

        /// <summary>
        /// This is based for loop the GS Segment to add the ST Segment
        /// </summary>
        /// <param name="list"></param>
        public virtual void GSLoop()
        {
            int ST = 1;
            List<Segment> stList = AddST(ST);

            //Add ST to Main List
            foreach (Segment segment in stList)
            {
                ISAList.Add(segment);
            }

        }

        /// <summary>
        /// This is base ST Segment
        /// </summary>
        /// <param name="Sequence"></param>
        public virtual List<Segment> AddST(int Sequence)
        {
            return null;
        }

        public override string ToString()
        {
            System.Text.StringBuilder lsb = new System.Text.StringBuilder();
            foreach (Segment seg in ISAList)
            {
                lsb.AppendFormat("{0}{1}", seg.ToString(this._sepChar, this._endChar), Environment.NewLine);
            }

            return lsb.ToString(); //.Replace(TMPISA,_ISA13);
        }

        public string ErrorListToString()
        {
            System.Text.StringBuilder lsb = new System.Text.StringBuilder();
            foreach (string name in ErrList)
                lsb.AppendLine(name);

            return lsb.ToString();
        }
    }
    public class EDI110Template : EDITemplate
    {
        private string ProductLine;
        private MW.DataHub.API.ValuePlus.EntityInvoice InvoiceEntity;
        private MW.DataHub.API.ValuePlus.EntityAMS AMSEntity;
        private MW.DataHub.API.ValuePlus.EntityOMS OMSEntity;

        public bool MandatoryPO { get; set; }

        public void ProcessEDI110(MW.DataHub.API.ValuePlus.EntityInvoice entity, int ISA13)
        {
            switch (entity.ModeCode)
            {
                case "AE":
                case "AI":
                case "3A":
                    ProductLine = "AMS";
                    AMSEntity = entity.ItemsAMS[0];
                    break;
                case "OE":
                case "OI":
                case "3O":
                    ProductLine = "OMS";
                    OMSEntity = entity.ItemsOMS[0];
                    break;
                default:
                    break;
            }



            //need change it
            //string ISA13 = "000004316";
            InvoiceEntity = entity;

            _ISA13 = padZero(ISA13.ToString(), 9);
            CurrentDateTime = DateTime.Now;
            //Write ISA
            //ISA*00*          *00*          *ZZ*DIMERCO        *ZZ*NETGEAR        *140513*1141*U*00401*000004316*0*P*:^
            //string ISA_Sender_ID = "DIMERCO";
            //string ISA_Receiver_ID = "NETGEAR";

            ErrList.Clear();
            ISAList.Clear();
            AddISA();

            string s = this.ToString();
            if (this.ErrList.Count > 0)
            {
                this.ErrorListToString();
            }
            System.Console.Write(s);
        }


        public override List<Segment> AddST(int Sequence)
        {
            List<Segment> stList = new List<Segment>();
            string STLoopID = padZero(Sequence.ToString(), 4);
            #region ST
            //ST*110*0001^
            Segment seg = new Segment("ST");
            seg.setElementAt(1, "110");
            seg.setElementAt(2, STLoopID);
            stList.Add(seg);
            #endregion

            //
            AddB3(stList);
            //Add Party 

            string[] PartyInfo = new string[] { "BT", "SH", "CN" };
            foreach (string party in PartyInfo)
            {
                AddParty(stList, party);
            }


            //N9 AC
            //N9*AC*4089078000^
            seg = new Segment("N9");
            seg.setElementAt(1, "AC");
            seg.setElementAt(2, "4089078000");
            stList.Add(seg);


            //N9 PO

            seg = new Segment("N9");
            seg.setElementAt(1, "PO");
            string strPO = "";
            foreach (MW.DataHub.API.ValuePlus.EntityPO s in AMSEntity.POList)
            {
                if (strPO.Length > 0)
                    strPO += (strPO.Length > 0 ? "" : ",") + s.PONo;
            }

            if (strPO != "")
            {
                seg.setElementAt(2, strPO);  // PO information
                stList.Add(seg);
            }
            else if (MandatoryPO)
                ErrList.Add(string.Format("Could not find PO number for invoice:{0} , HAWB:{1}"
                    , InvoiceEntity.InvoiceNo, InvoiceEntity.HouseNo));

            //N9 Airwaybill
            //N9*AW*DIM044023213^
            seg = new Segment("N9");
            seg.setElementAt(1, "AW");
            seg.setElementAt(2, InvoiceEntity.HouseNo);
            stList.Add(seg);

            int LX = 1;
            AddLX(stList, LX);

            #region SE
            seg = new Segment("SE");
            seg.setElementAt(1, (stList.Count + 1).ToString());
            seg.setElementAt(2, STLoopID);
            stList.Add(seg);
            #endregion

            return stList;
        }

        public void AddB3(List<Segment> list)
        {
            #region B3
            //B3**04414006399*DIM044023213*CC**20140429*155736****DMER^
            Segment seg = new Segment("B3");
            seg.setElementAt(2, InvoiceEntity.InvoiceNo);
            seg.setElementAt(3, InvoiceEntity.HouseNo);
            seg.setElementAt(4, "CC");
            seg.setElementAt(6,InvoiceEntity.InvoiceDate.Value.ToString("yyyyMMdd"));
            seg.setElementAt(7,removeDecimal(InvoiceEntity.InvoiceAmount)); // removeDecimal(l_invhd->GetDouble("ar_amt")));
            seg.setElementAt(11, "DMER");
            list.Add(seg);
            #endregion

            #region B3A
            //B3A*20^
            seg = new Segment("B3A");
            if (InvoiceEntity.ModeCode == "AI" && InvoiceEntity.IDType == "H")
                seg.setElementAt(1, "21");
            else
                seg.setElementAt(1, "20");            
            list.Add(seg);
            #endregion           

        }

        public void AddParty(List<Segment> list, string PartyType)
        {
            MW.DataHub.API.ValuePlus.EntityShipment Shipment = null;
            if (AMSEntity != null)
                Shipment = AMSEntity;
            else if (OMSEntity != null)
                Shipment = OMSEntity;

            MW.DataHub.API.ValuePlus.EntityCustomerAddress address = null;
            switch (PartyType)
            {
                case "BT":
                    address = InvoiceEntity.ItemsCustomerAddress[(string.IsNullOrEmpty(InvoiceEntity.BILLTO_Alt) ?
                        InvoiceEntity.BILLTO + ":0" : InvoiceEntity.BILLTO + ":" + InvoiceEntity.BILLTO_Alt)];
                    break;
                case "SH":
                    address = InvoiceEntity.ItemsCustomerAddress[(Shipment.Shipper_ALT > 0 ?
                        Shipment.Shipper.ToString() + ":" + Shipment.Shipper_ALT.ToString() : Shipment.Shipper.ToString() + ":0")];
                    break;
                case "CN":
                    address = InvoiceEntity.ItemsCustomerAddress[(Shipment.CNEE_ALT > 0 ?
                        Shipment.CNEE.ToString() + ":" + Shipment.CNEE_ALT.ToString() : Shipment.CNEE.ToString() + ":0")];
                    break;
                default:
                    throw new Exception(string.Format("Did not find this type of Part:{0} in AddParty", PartyType));
            }
            if (address == null)
                ErrList.Add(string.Format("Could not find the {0} information for Invoice:{1}, HAWB:{2}"
                    ,(PartyType=="BT"?"Bill To":(PartyType=="SH"?"Shipper":"CNEE")),InvoiceEntity.InvoiceNo, InvoiceEntity.HouseNo));

            #region N1
            //B3**04414006399*DIM044023213*CC**20140429*155736****DMER^
            Segment seg = new Segment("N1");
            //N1*BT*Netgear^
            seg.setElementAt(1, PartyType);
            seg.setElementAt(2, address.CustomerName);
            list.Add(seg);
            #endregion

            #region N3
            seg = new Segment("N3");
            //N3*350 E. PLUMERIA DRIVE^
            seg.setElementAt(1, substring(address.CustomerAddress1,55));            
            seg.setElementAt(2, substring(address.CustomerAddress2,55));
            list.Add(seg);
            #endregion

            #region N4
            seg = new Segment("N4");
            //N4*SAN JOSE*CA*95134*US^
            seg.setElementAt(1, address.CityName);
            seg.setElementAt(2, address.StateCode);
            //if Postal Code and Mandatory Country postal, it will appear error
            seg.setElementAt(3, address.Postal);
            seg.setElementAt(4, address.CountryCode);
            list.Add(seg);
            #endregion
        }

        public void AddLX(List<Segment> list, int Sequence)
        {
            #region LX
            //LX*1^
            Segment seg = new Segment("LX");
            seg.setElementAt(1, Sequence.ToString());
            list.Add(seg);
            #endregion

            //L5**CABLE MODEM^
            seg = new Segment("L5");
            string strDesc = "";
            if (this.ProductLine == "AMS")
                strDesc=this.AMSEntity.DESC;  //seg->setElementAt (2, l_hawb->GetStr ("descr"));
            else if (this.ProductLine == "OMS")
                strDesc= this.OMSEntity.DESC;
            else
                throw new Exception(string.Format(errorProductLine, ProductLine, "L5"));
            if (strDesc != "")
            {
                seg.setElementAt(2, strDesc);
                list.Add(seg);
            }
            else
                ErrList.Add(string.Format("Could not find the product description for invoice:{0}, HAWB:{1}"
                    , InvoiceEntity.InvoiceNo, InvoiceEntity.HouseNo));            


            //P1*DMER**PVG**HKG^
            seg = new Segment("P1");
            DateTime dt;
            if (this.ProductLine == "AMS")
            {
                //etd
                if (AMSEntity.MilestoneLists.ContainsKey(1100))
                {
                    dt = AMSEntity.MilestoneLists[1100].Milestone;
                    seg.setElementAt(2, dt.ToString("yyyyMMdd"));
                    seg.setElementAt(3, "011");
                    if (dt.ToString("HHmm") != "0000")
                        seg.setElementAt(4, dt.ToString("HHmm"));
                    list.Add(seg);
                }
                else
                    ErrList.Add(string.Format("Could not find ETD for invoice:{0}, HAWB:{1}"
                        , InvoiceEntity.InvoiceNo, InvoiceEntity.HouseNo));            
            }
            else
                throw new Exception(string.Format(errorProductLine, ProductLine, "P1"));


            //Write R1
            //R1*DMER**PVG**HKG^
            string from1 = this.InvoiceEntity.ItemsPortInfo[string.Format("{0}:{1}", MW.DataHub.API.ValuePlus.PortType.AirPort, this.AMSEntity.CarrierBooking.PortOfDEPT)].PortCode;
            string to1 = this.InvoiceEntity.ItemsPortInfo[string.Format("{0}:{1}", MW.DataHub.API.ValuePlus.PortType.AirPort, this.AMSEntity.CarrierBooking.PortOfDSTN)].PortCode;
            if (from1 != "" && to1 != "")
            {
                seg = new Segment("R1");
                seg.setElementAt(1, "DMER");
                seg.setElementAt(3, from1); //l_albook->GetStr ("from1"));
                seg.setElementAt(4, this.AMSEntity.CarrierBooking.OnboardFLT); //getCarrierCode (l_albook->GetStr ("book_with")));
                seg.setElementAt(5, to1); //l_albook->GetStr ("to1"));
                list.Add(seg);
            }
            else 
                ErrList.Add(string.Format("Could not Carrier Booking information for invoice:{0}, HAWB:{1}"
                        , InvoiceEntity.InvoiceNo, InvoiceEntity.HouseNo));            

            seg = new Segment("POD");
            //POD*20140502*1300*NETGEAR^
            if (this.ProductLine == "AMS")
            {
                //POD  Freight Release Date
                if (AMSEntity.MilestoneLists.ContainsKey(3000))
                {
                    dt = AMSEntity.MilestoneLists[3000].Milestone;
                    seg.setElementAt(1, dt.ToString("yyyyMMdd"));
                    if (dt.ToString("HHmm") != "0000")
                        seg.setElementAt(2, dt.ToString("HHmm"));

                    if(!string.IsNullOrEmpty(AMSEntity.FreightSignby))
                        seg.setElementAt(3, AMSEntity.FreightSignby);
                    else
                        ErrList.Add(string.Format("Could not find Sign by for invoice:{0}, HAWB:{1}"
                            , InvoiceEntity.InvoiceNo, InvoiceEntity.HouseNo));
                    list.Add(seg);
                }
                else
                    ErrList.Add(string.Format("Could not find POD for invoice:{0}, HAWB:{1}"
                            , InvoiceEntity.InvoiceNo, InvoiceEntity.HouseNo));            
            }
            else
                throw new Exception(string.Format(errorProductLine, ProductLine, "POD"));


            seg = new Segment("L0");
            //L0*1*Weight*KG*388.00*B*411.54*KG*2.00*CTN**KG*NA*2.00*CTN^
            seg.setElementAt(1, "1");
            seg.setElementAt(2, "Weight");
            seg.setElementAt(3, AMSEntity.WTUOM);
            seg.setElementAt(4, AMSEntity.GWT.Value.ToString());
            seg.setElementAt(5, "B");
            seg.setElementAt(6, AMSEntity.VWT.Value.ToString());
            seg.setElementAt(7, AMSEntity.WTUOM);
            seg.setElementAt(8, AMSEntity.PCS.ToString());
            seg.setElementAt(9, AMSEntity.PCSUOM);
            seg.setElementAt(11, AMSEntity.WTUOM);
            string service = "NA";
            seg.setElementAt(12, service);
            seg.setElementAt(13, AMSEntity.PCS.ToString());
            seg.setElementAt(14, AMSEntity.PCSUOM);
            list.Add(seg);

            seg = new Segment("L10");
            //L10*388.00*G*KG^
            seg.setElementAt(1, AMSEntity.GWT.Value.ToString());
            seg.setElementAt(2, "G");
            seg.setElementAt(3, AMSEntity.WTUOM);
            list.Add(seg);

            seg = new Segment("SL1");
            //SL1*NA^
            seg.setElementAt(1, service);
            seg.setElementAt(2, AMSEntity.NatureOfGoods);   //      seg->setElementAt (2, l_hawb->GetStr ("naturegoods"));
            list.Add(seg);

            if (InvoiceEntity.ItemsInvoiceDetail.Count > 0)
            {
                if (AMSEntity.Rate > 0)
                {
                    decimal cwt_Rate = AMSEntity.Rate.Value;
                    foreach (MW.DataHub.API.ValuePlus.EntityInvoiceDetail d in InvoiceEntity.ItemsInvoiceDetail.Values)
                    {
                        seg = new Segment("L1");
                        //L1*1*3.78*CC*1557.36****400*KG^

                        seg.setElementAt(1, "1");  //LineNo
                        seg.setElementAt(2, cwt_Rate.ToString());
                        seg.setElementAt(3, InvoiceEntity.InvoiceType); // l_invdt->GetStr ("sc_type"));
                        seg.setElementAt(4, InvoiceEntity.InvoiceAmount.ToString());
                        seg.setElementAt(8, "400");
                        seg.setElementAt(9, "KG");
                        list.Add(seg);
                    }
                }
                else
                    ErrList.Add(string.Format("Could not find Freight Rate for invoice:{0}, HAWB:{1}"
                           , InvoiceEntity.InvoiceNo, InvoiceEntity.HouseNo));
            }
            else
                ErrList.Add(string.Format("Could not find invoice detail item for invoice:{0}, HAWB:{1}"
                           , InvoiceEntity.InvoiceNo, InvoiceEntity.HouseNo));

            seg = new Segment("L3");
            //L3*412.00*KG***1557.36******2.00^
            seg.setElementAt(1, AMSEntity.CWT.Value.ToString());
            seg.setElementAt(2, AMSEntity.WTUOM);
            seg.setElementAt(5, InvoiceEntity.InvoiceAmount.ToString());
            seg.setElementAt(11, AMSEntity.PCS.ToString());
            list.Add(seg);

            if (AMSEntity.DIMList != null && AMSEntity.DIMList.Count > 0)
            {
                foreach (MW.DataHub.API.ValuePlus.EntityDimension dim in AMSEntity.DIMList)
                {
                    //Dimension
                    //L4*119*119*135*CM*8^
                    seg = new Segment("L4");

                    seg.setElementAt(1, dim.Length);
                    seg.setElementAt(2, dim.Width);
                    seg.setElementAt(3, dim.Height);
                    seg.setElementAt(4, dim.UOM);
                    seg.setElementAt(5, dim.PCS);
                    list.Add(seg);
                }
            }
            else
                ErrList.Add(string.Format("Could not find Dimension for invoice:{0}, HAWB:{1}"
                           , InvoiceEntity.InvoiceNo, InvoiceEntity.HouseNo));   
        }
    }

    public class NetgearEDI110 : EDI110Template
    {
        //public NetgearEDI110(char sepChar, char endChar) :base (sepChar,endChar)
        //{
        //}



    }

    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MW.DataHub.API.ValuePlus.IBPValuePlus mgr = MW.DataHub.API.ValuePlus.VPFactory.GetBPValuePlus();
            //mgr.DB.CreateDatabaseByConnectionString("Data Source=10.130.1.23;Initial Catalog=eChainVP;User ID=sa;Password=dim1rc0@");
            //mgr.DB.CreateDatabaseByConnectionString("Data Source=10.1.8.16;Initial Catalog=eChainVP;User ID=sa;Password=dim1rc0@");
            mgr.ConnectByStationID("042");

            MW.DataHub.API.ValuePlus.EntityInvoice entity = mgr.InvoiceInfoGetAll("042", TextBox1.Text);

            Response.Write("DDD");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            MW.DataHub.API.ValuePlus.IBPValuePlus mgr = MW.DataHub.API.ValuePlus.VPFactory.GetBPValuePlus();
            mgr.DB.CreateDatabaseByConnectionString("Data Source=10.130.1.24;Initial Catalog=eChainVP_DFS;User ID=sa;Password=dim1rc0@");

            MW.DataHub.API.ValuePlus.EntityInvoice entity = mgr.InvoiceInfoGetAll("067", TextBox1.Text);

            Response.Write("DD");

        }

        protected void TT_Click(object sender, EventArgs e)
        {
            //EDI110 edi = new EDI110();
           // edi.Process110("044", "04414006399");

            string StationID = "016";
            string InvoiceNo = "01614010484";

            //StationID = "044";
            //InvoiceNo = "04414006399";

            InvoiceNo = TextBox1.Text;
            StationID = InvoiceNo.Substring(0, 3);


            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            MW.DataHub.API.ValuePlus.IBPValuePlus mgr = MW.DataHub.API.ValuePlus.VPFactory.GetBPValuePlus();
            mgr.IsCentralData = false;
            mgr.ConnectByStationID(StationID);
            MW.DataHub.API.ValuePlus.EntityInvoice entity = mgr.InvoiceInfoGetAll(StationID, InvoiceNo);

            //byte[] b = entity.to
            System.IO.MemoryStream write = new System.IO.MemoryStream();
            //write.Write(

            watch.Stop();
            System.Console.Write(watch.ElapsedMilliseconds);
            //Start to Read the entity information

            NetgearEDI110 edi = new NetgearEDI110();           
            edi.ISA_Sender_ID = "DIMERCO";
            edi.ISA_Receiver_ID = "NETGEAR";

            edi.GS_Receiver_ID = "BBA5900";
            edi.GS_Sender_ID = "DMER";
            edi.ProcessEDI110(entity, 4316);

            string s = edi.ToString();
            string e1 = edi.ErrorListToString();



            


           

            
      

     
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //Check the web2.config

            string fileName = Server.MapPath("/station.config");

            DataSet lds = new DataSet();
            if (System.IO.File.Exists(fileName))
                lds.ReadXml(fileName);
            else
            {
                DataTable ldt = new DataTable();
                ldt.Columns.Add("Key", Type.GetType("System.String"));
                ldt.Columns.Add("Value", Type.GetType("System.String"));
                ldt.TableName = "Setting";
                lds.Tables.Add(ldt);
            }

            lds.Tables[0].Rows.Add("SS","SS1");
            lds.WriteXml(fileName);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {


            MW.DataHub.API.Net.clsFTPHandler ftp = new MW.DataHub.API.Net.clsFTPHandler();
            MW.DataHub.API.Net.FtpLogin login = new MW.DataHub.API.Net.FtpLogin();
            login.Server = "ccnppdftp.ccnexchange.com";
            login.UserName = "gwtest19";
            login.Port = 21;
            login.Password = "Password";
            login.RemoteFolder = "In";
            login.LocalFolder = @"D:\log\201408";
            login.Direction = MW.DataHub.API.Net.FtpDirection.Upload;
            login.FTPTool = MW.DataHub.API.Net.FtpTools.FTPAPI;
            login.FileExtension = "*.bak";
            ftp.doFTP(login);
        }


    }







    public class EDI110
    {
        public string CustomerID { get; set; }
        private string IDType { get; set; }
        private string ModeCode { get; set; }

        public void Process110(string StationID, string InvoiceNo)
        {
            //Process110("042","");

            MW.DataHub.API.ValuePlus.IBPValuePlus mgr = MW.DataHub.API.ValuePlus.VPFactory.GetBPValuePlus();
            mgr.ConnectByStationID(StationID);
            MW.DataHub.API.ValuePlus.EntityInvoice entity = mgr.InvoiceInfoGetAll(StationID, InvoiceNo);


            


        }


    }

}
