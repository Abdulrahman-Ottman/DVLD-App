using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsLicenseClasses
    {
        public static DataTable getAllLicenseClasses()
        {
            DataTable results;
            string query = "Select * from LicenseClasses";
            SqlCommand command = new SqlCommand(query, clsSettings.connection);
            results = clsHelpers.LicenseClassesCommandExecuter(command);
            return results;
        }
    }
}
