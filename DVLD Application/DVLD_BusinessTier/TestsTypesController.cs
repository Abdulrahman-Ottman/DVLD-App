using System;
using System.Data;

using DVLD_DataAccessTier;

namespace DVLD_BusinessTier
{
    public class TestsTypesController
    {
        public static DataTable GetAllTypes()
        {
            return clsTestType.GetAllTypes();
        }
    }
}
