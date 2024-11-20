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

namespace DVLD_ViewTier.Applications.Applications_Types
{
    public partial class EditApplicationsTypes : Form
    {
        int TypeID = -1;
        public EditApplicationsTypes(int id)
        {
            TypeID = id;
            InitializeComponent();
        }

        private void EditApplicationsTypes_Load(object sender, EventArgs e)
        {
            Dictionary<string , string> data = ApplicationsTypesController.FindTypeByID(TypeID);
            lbID.Text = TypeID.ToString();
            tbTitle.Text = data["title"];
            tbFees.Text = data["fees"];
        }
    }
}
