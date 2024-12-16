using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsTestType
    {
        public static DataTable GetAllTypes()
        {
            string query = "select * from TestTypes";
            SqlCommand command = new SqlCommand(query , clsSettings.connection);
            return clsHelpers.TestsTypesCommandExecuter(command);
        }

    }
}
