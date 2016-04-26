using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MW.DataHub.BO.Common
{
    public class DBCommon
    {
        protected Database db;
        public DBCommon()
        {
            db = DatabaseFactory.CreateDatabase("EDIPlatform");
        }
    }
}
