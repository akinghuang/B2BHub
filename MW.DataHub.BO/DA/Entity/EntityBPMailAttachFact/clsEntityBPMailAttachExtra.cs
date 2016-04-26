using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;
using System.Collections.Generic;

namespace MW.DataHub.BO
{
	public partial interface IBPMailAttach : IBase<Entity.EntityBPMailAttach>
	{
	}

	partial class clsBPMailAttach : clsBase<Entity.EntityBPMailAttach>, IBPMailAttach
	{
		

	}
}
