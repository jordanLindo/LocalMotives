using DataAccessUtilities;
using System;
using System.Collections.Generic;

namespace LogicLayerUtilities
{
	public static class ListManager
    {
        public static List<string> GetTimes()
        {
			try
			{
				return ListAccessor.RetrieveHours();
			}
			catch (Exception ex)
			{

				throw new ApplicationException("Times Not Found.",ex);
			}
        }
    }
}
