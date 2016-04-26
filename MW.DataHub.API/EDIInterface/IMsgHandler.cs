using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MW.DataHub.EDIInterface
{
    public delegate void OnError(String ErrorType,String KeyValue, String ErrorMessage, bool TriggerNotification,bool SendToAdminOnly);
    public interface IMsgHandler
    {
        /// <summary>
        /// Parse recieved message content into dataset format
        /// </summary>
        /// <param name="ProjectPara">Project runtime paramesters </param>
        /// <param name="TaskPara">Task runtime parameters</param>
        /// <param name="FileFullPath">The file full path to be process</param>
        /// <param name="returnMessage">return message</param>
        /// <returns>Parsed and formatted data</returns>
        DataSet ParseMessage(String ProjectPara,String TaskPara,string FileFullPath,ref String returnMessage);
        /// <summary>
        /// Pack business data into EDI format message
        /// </summary>
        /// <param name="ProjectPara">Project runtime paramesters</param>
        /// <param name="Taskpara">Task runtime parameters</param>
        /// <param name="BizData">business data</param>
        /// <param name="returnMessage">return message</param>
        /// <returns></returns>
        string PackMessage(String ProjectPara, String Taskpara, DataSet BizData, ref String returnMessage);

        event OnError OnError;
        
    }
}
