using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MW.DataHub.EDIInterface
{
    
    public interface IBizHandler
    {
        /// <summary>
        /// Process business data from partner message into business system
        /// </summary>
        /// <param name="ProjectPara">Project runtime paramesters </param>
        /// <param name="TaskPara">Task runtime parameters</param>
        /// <param name="dsRevData"></param>
        /// <param name="returnMessage">return message</param>
        /// <returns>true: success; false: fail</returns>
        bool ImportData(String ProjectPara,String TaskPara,DataSet dsRevData,ref String returnMessage);
        /// <summary>
        /// Extract data from business system database
        /// </summary>
        /// <param name="ProjectPara">Project runtime paramesters</param>
        /// <param name="TaskPara">Task runtime parameters</param>
        /// <param name="returnMessage">returnMessage</param>
        /// <returns>Business data</returns>
        DataSet ExtractData(String ProjectPara, String TaskPara, ref String returnMessage);
        bool CommonFun(String ProjectPara, String TaskPara, ref String returnMessage);
        event OnError OnError;
    }
}
