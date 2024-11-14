using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_ViewTier
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (LoginForm loginForm = new LoginForm())
            {
                // Show the login form as a dialog  
                if (loginForm.ShowDialog() == DialogResult.OK) // Assuming OK is the result after a successful login  
                {
                    // After successful login, show the MainScreen  
                    Application.Run(new MainScreen.MainScreen());
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
