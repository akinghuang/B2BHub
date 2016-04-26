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
#region Entity of Other Module
    [Serializable]
    public class EntityOtherShipment : EntityShipment
    {

    }
#endregion

   public partial interface IBPValuePlus : IBase<object>
    {
        
    }

   partial class clsBPValuePlus : clsBase<object>, IBPValuePlus
   {
       private List<EntityOtherShipment> BuildOtherSelectList(IDataReader idr)
       {
           List<EntityOtherShipment> lists = new List<EntityOtherShipment>();
           while (idr.Read())
           {
               EntityOtherShipment entity = new EntityOtherShipment();
               int index = 0;

               index = idr.GetOrdinal("StationID");
               if (!idr.IsDBNull(index))
               {
                   entity.StationID = idr.GetString(index);
               }

               index = idr.GetOrdinal("HAWBID");
               if (!idr.IsDBNull(index))
               {
                   entity.HouseID = idr.GetInt32(index);
               }

               lists.Add(entity);
           }
           return lists;
       }
   }
}
