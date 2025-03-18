using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccessTier;
using static DVLD_DataAccessTier.clsLicense;

namespace DVLD_BusinessTier
{
    public class LicenseController
    {

        public static Dictionary<string, string> getLicenseInfo(int applicationID)
        {
            return clsLicense.getLicenseInfo(applicationID);
        }
        public static Dictionary<string, string> getLicenseInfoByLicenseID(int LicenseID,string LicenseClass=null,bool IsActive=false)
        {
            return clsLicense.getLicenseInfoByLicenseID(LicenseID,LicenseClass,IsActive);
        }

        public static DataTable getLocalLicenseHistory(string NationalNumber)
        {
            return clsLicense.getLocalLicenseHistory(NationalNumber);
        }
        public static DataTable getInternationalLicenseHistory(string NationalNo)
        {
            return clsLicense.getInternationalLicenseHistory(NationalNo);
        }
        public static bool issueInternationalLicense(int LocalLicenseID, int DriverID)
        {
            return clsLicense.issueInternationalLicense(LocalLicenseID, DriverID);
        }
        public static bool renewLicense(int LicenseID, int DriverID)
        {
            return clsLicense.renewLicense(LicenseID, DriverID);
        }
        public static bool replaceLicense(int LicenseID, int DriverID, int status)
        {
            if (status == 0)
            {
                return clsLicense.replaceLicense(LicenseID, DriverID, clsLicense.LicenseStatus.Damaged);
            }
            else
            {
                return clsLicense.replaceLicense(LicenseID, DriverID, clsLicense.LicenseStatus.Lost);
            }

        }
        public static bool detainLicense(int LicenseID, string fineFees)
        {
            return clsLicense.detainLicense(LicenseID, fineFees);
        }
        public static bool releaseLicense(int LicenseID, int DriverID)
        {
            return clsLicense.releaseLicense(LicenseID, DriverID);
        }
    }
}
