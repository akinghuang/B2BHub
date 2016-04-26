using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using DIMERCO.B2B.EDIInterface;
namespace DIMERCO.eCall.Feedback
{
    public class MsgParser:IMsgHandler
    {
        public event DIMERCO.B2B.EDIInterface.OnError OnError;
        public MsgParser()
        {
            OnError += new DIMERCO.B2B.EDIInterface.OnError(BizHandler_OnError);
        }

        void BizHandler_OnError(string ErrorType, string KeyValue, string ErrorMessage, bool TriggerNotification, bool SendToAdminOnly)
        {
            //throw new NotImplementedException();
        }

        public DataSet ParseMessage(String ProjectPara, String TaskPara, string FileFullPath, ref String returnMessage)
        {
            //Do something to parse received flat file into dataset for incoming task
            return new DataSet();
        }
        public string PackMessage(String ProjectPara, String Taskpara, DataSet BizData, ref String returnMessage)
        {
            //Do something to serilize data into specified EDI format file for outgoing task
            return "";
        }
    }
}
