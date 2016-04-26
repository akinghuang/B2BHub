using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;

using System.Linq;
using System.Data.Common;


namespace MW.DataHub.API.BO
{

    public class BOFactory
    {
        public BOFactory()
        {
        }

        public static IBPSetting GetBPSetting()
        {
            return new clsBPSetting();
        }

        public static IBPMailLog GetBPMailLog()
        {
            return new clsBPMailLog();
        }
        public static IBPMailFailed GetBPMailFailed()
        {
            return new clsBPMailFailed();
        }
        public static IBPMailAttach GetBPMailAttach()
        {
            return new clsBPMailAttach();
        }
    }

}
