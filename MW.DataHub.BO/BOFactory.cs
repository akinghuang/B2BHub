using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Data;

using System.Linq;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MW.DataHub.BO
{

    public class BOFactory
    {
        public BOFactory()
        {

        }

        public static IBPActivities GetBPActivities()
        {
            return new clsBPActivities();
        }

        public static IBPProjectTask GetBPProjectTask()
        {
            return new clsBPProjectTask();
        }

        public static IBPUser GetBPUser()
        {
            return new clsBPUser();
        }

        public static IBPProject GetBPProject()
        {
            return new clsBPProject();
        }

        public static IBPProjectChange GetBPProjectChange()
        {
            return new clsBPProjectChange();
        }

        public static IBPProjectLog GetBPProjectLog()
        {
            return new clsBPProjectLog();
        }

        public static IBPProjectNtf GetBPProjectNtf()
        {
            return new clsBPProjectNtf();
        }

        public static IBPActivities GetIBPActivities()
        {
            return new clsBPActivities();
        }

        public static IBPActivitiyLog GetIBPActivitiesLog()
        {
            return new clsBPActivitiyLog();
        }

        public static IBPMailLog GetBPProjectMailLog()
        {
            return new clsBPMailLog();
        }

        public static IBPMailAttach GetBPMailAttach()
        {
            return new clsBPMailAttach();
        }
    }

}
