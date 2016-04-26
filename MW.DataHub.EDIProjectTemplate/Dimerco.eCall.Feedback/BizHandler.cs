using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DIMERCO.B2B.EDIInterface;
namespace DIMERCO.eCall.Feedback
{
    public class BizHandler:IBizHandler
    {
        public event DIMERCO.B2B.EDIInterface.OnError OnError;
        public BizHandler()
        {
            OnError += new DIMERCO.B2B.EDIInterface.OnError(BizHandler_OnError);
        }

        void BizHandler_OnError(string ErrorType, string KeyValue, string ErrorMessage, bool TriggerNotification, bool SendToAdminOnly)
        {
            //throw new NotImplementedException();
        }

        public bool ImportData(String ProjectPara, String TaskPara, DataSet dsRevData, ref String returnMessage)
        {
            //Do something to import data into database here for incoming task
            return false;
        }
        public bool CommonFun(String ProjectPara, String TaskPara, ref String returnMessage)
        {
            //Do general processes here
            return false;
        }
        public DataSet ExtractData(String ProjectPara, String TaskPara, ref String returnMessage)
        {
            //Do something to get data from database here for outgoing task
            return new DataSet();
        }
    }
}
