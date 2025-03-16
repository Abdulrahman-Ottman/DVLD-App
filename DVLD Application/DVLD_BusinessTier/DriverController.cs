using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessTier;
namespace DVLD_BusinessTier
{
    public class DriverController
    {

        public static DataTable getAllDrivers(string NationalNumberFilter=null)
        {
            return clsDriver.getAllDrivers(NationalNumberFilter);
        }
    }
}
