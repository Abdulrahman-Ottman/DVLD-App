using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//notes:
//applications status : 1-new 2-canceled 3-completed
//Do delegates for forms with refresh buttons :)
namespace DVLD_DataAccessTier
{
    public class clsSettings
    {
        public static string connectionString { get; set; } = "Server=.;Database=DVLD;User Id=sa;Password=abood;MultipleActiveResultSets=True;";
        public static SqlConnection connection { get; set; } = new SqlConnection(connectionString);
        public static clsUser currentUser { get; set; }
    }
}
