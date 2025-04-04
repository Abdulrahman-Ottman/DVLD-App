using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//notes:
//applications status : 1-new 2-canceled 3-completed
//Do delegates for forms with refresh buttons :)
//Handle store application images:
//1-replace the image name with GUID 
//2-store it in a separate folder 
//3-delete the old image if it is an update mode

//the filtering system that i made query the database every time and get the filtered results 
//but after that i learned that i can do filters in the dataTable so in the PeopleManagement screen
//I applied this way , and i kept it in others forms;
namespace DVLD_DataAccessTier
{
    public class clsSettings
    {
        public static string connectionString { get; set; } = "Server=.;Database=DVLD;User Id=sa;Password=abood;MultipleActiveResultSets=True;";
        public static SqlConnection connection { get; set; } = new SqlConnection(connectionString);
        public static clsUser currentUser { get; set; }
    }
}
