using System;
using System.Data;
using System.Data.Common;
using DIMERCO.SDK2.Data;

namespace MW.DataHub.BO
{
	public partial interface IBPUserProject : IBase<Entity.EntityBPUserProject>
	{
		
	}

	partial class clsBPUserProject : clsBase<Entity.EntityBPUserProject>, IBPUserProject
	{
		

	}
}
