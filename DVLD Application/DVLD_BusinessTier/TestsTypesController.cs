using System;
using System.Collections.Generic;
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

        public static Dictionary<string,string> FindTestByID(int id)
        {
            return clsTestType.FindTestByID(id);
        }

        public static bool UpdateTest(int id , string title , string description , string fees)
        {
            return clsTestType.UpdateTest(id, title, description, fees);
        }
    }
}
