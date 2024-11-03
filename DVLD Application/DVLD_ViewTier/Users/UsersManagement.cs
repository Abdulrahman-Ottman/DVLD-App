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

namespace DVLD_ViewTier.Users
{
   
    public partial class UsersManagement : Form
    {
        private DataTable Users = UserController.GetAllUsers();
        static DataColumn[] keyColumns = new DataColumn[1];


        public UsersManagement()
        {
            InitializeComponent();
        }

        private void UsersManagement_Load(object sender, EventArgs e)
        {
            keyColumns[0] = Users.Columns["Id"];
            Users.PrimaryKey = keyColumns;
            dgvUsersList.DataSource = Users;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.ShowDialog();
        }
    }
}
