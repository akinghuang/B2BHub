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

    public partial interface IBase<T> : DIMERCO.SDK2.Base.ISDKBase<T>
    {

    }

    partial class clsBase<T> : DIMERCO.SDK2.Base.clsSDKBase<T>, IBase<T>
    {
        public clsBase()
        {
            this.DB.DatabaseName = "EDIPlatform";
        }
        public override T GetSQLEntityByHQID(int vHQID)
        {
            throw new NotImplementedException();
        }

        public override void RemoveSQLEntityByHQID(int vHQID)
        {
            throw new NotImplementedException();
        }

        public override int SaveSQLData(T objEntity, string LoginID)
        {
            throw new NotImplementedException();
        }

        protected override string ProcessSQLString(DIMERCO.SDK2.Base.ProcessCommandTypes commandTypes, T objEntity)
        {
            throw new NotImplementedException();
        }

    }


}
