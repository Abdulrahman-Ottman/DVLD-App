using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DVLD_BusinessTier;
using DVLD_DataAccessTier;


namespace DVLD_BusinessTier
{
    public class ApplicationsTypesController
    {
        public static DataTable GetAllTypes()
        {
            return clsApplicationType.GetAllTypes();
        }

        public static Dictionary<string,string> FindTypeByID(int id)
        {
            return clsApplicationType.FindTypeByID(id);
        }

        public static bool UpdateType(int id , string title , string fees)
        {
            return clsApplicationType.UpdateType(id , title , fees);
        }
    }
}
