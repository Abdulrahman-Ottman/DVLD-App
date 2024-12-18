using DVLD_BusinessTier;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_ViewTier.Tests
{
    public partial class EditTestsTypes : Form
    {
        public delegate void TestTypes_DataUpdated(int id, string title,string description, string fees);
        public event TestTypes_DataUpdated DataUpdated;
        int TestID = -1;

        public EditTestsTypes(int id)
        {
            TestID = id;
            InitializeComponent();
        }

        private void EditTestsTypes_Load(object sender, EventArgs e)
        {
            Dictionary<string,string> data = TestsTypesController.FindTestByID(TestID);
            lbID.Text = TestID.ToString();
            tbTitle.Text = data["title"].ToString(); 
            tbDescription.Text = data["description"].ToString(); 
            tbFees.Text = data["fees"].ToString(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TestsTypesController.UpdateTest(TestID, tbTitle.Text, tbDescription.Text, tbFees.Text))
            {
                MessageBox.Show("Test has been updated");
                DataUpdated?.Invoke(TestID,tbTitle.Text,tbDescription.Text,tbFees.Text);
                this.Close();
            }else{
                MessageBox.Show("Failed to update the test");
            }
        }
    }
}
