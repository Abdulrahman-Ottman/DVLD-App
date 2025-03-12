using DVLD_DataAccessTier;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BusinessTier
{
    public class ApplicationController
    {
        public static int addLocalDrivingLicenseApplication(Dictionary<string,string> data)
        {
            return clsApplication.addLocalDrivingLicenseApplication(data);
        }

        public static DataTable GetAllLocalApplications()
        {
            return clsApplication.GetAllLocalApplications();
        }

        public static DataTable GetLocalApplicationsOnFilter(string filter , string value)
        {
            return clsApplication.GetLocalApplicationsOnFilter(filter,value);
        }
        public static Dictionary<int, bool> getApplicationPassedTests(string id)
        {
            return clsApplication.getApplicationPassedTests(id);
        }

        public static DataTable GetTestAppointments(string TestTypeID,string applicationID)
        {
            return clsApplication.GetTestAppointments(TestTypeID, applicationID);
        }
        public static bool saveTestAppointment(int testTypeID, DateTime date, int applicationID, float paidFees)
        {
            return clsApplication.saveTestAppointment(testTypeID, date, applicationID, paidFees);
        }
        public static bool TakeTest(int TestAppointmentID, bool TestResult, string Notes)
        {
            return clsApplication.TakeTest(TestAppointmentID,TestResult,Notes);
        }

    }
}
