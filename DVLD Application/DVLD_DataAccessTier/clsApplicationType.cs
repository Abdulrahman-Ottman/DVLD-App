using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsApplicationType
    {

        public static DataTable GetAllTypes()
        {
            DataTable results;
            string query = "Select * from ApplicationTypes";
            SqlCommand command = new SqlCommand(query , clsSettings.connection);
            results = clsHelpers.ApplicationsTypesCommandExecuter(command);

            return results;
        }
    }
}
