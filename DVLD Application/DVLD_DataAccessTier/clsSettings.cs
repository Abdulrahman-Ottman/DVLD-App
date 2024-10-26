using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccessTier
{
    public class clsSettings
    {
        public static string connectionString { get; set; } = "Server=.;Database=DVLD;User Id=sa;Password=abood";
        public static SqlConnection connection { get; set; } = new SqlConnection(connectionString);
    }
}
