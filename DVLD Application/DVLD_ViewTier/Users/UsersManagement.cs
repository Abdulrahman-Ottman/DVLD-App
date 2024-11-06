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
        static Control currentSelectedFilter = null;

        public UsersManagement()
        {
            InitializeComponent();
        }

        private void UsersManagement_Load(object sender, EventArgs e)
        {
            keyColumns[0] = Users.Columns["Id"];
            Users.PrimaryKey = keyColumns;
            cbUserFilters.SelectedIndex = 0;
            LoadDataToGridView();
        }
        private void LoadDataToGridView()
        {
            dgvUsersList.DataSource = Users;
            dgvUsersList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void ReFetchAllUsersData()
        {
             Users = UserController.GetAllUsers();
            LoadDataToGridView();
        }
    

        private void button1_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentSelectedFilter != null) { 
                this.Controls.Remove(currentSelectedFilter);
            }
            Control inputControl = null;
            switch (cbUserFilters.Text)
            {
                case "None":
                    break;
                case "User Name":
                     inputControl = new TextBox
                    {
                        Name = "tbUserName",
                        Width = 100,
                        Location = new Point(195, 157)
                    };
                    ((TextBox)inputControl).TextChanged += inputControl_TextChanged;
                    break;
                case "Is Active":
                    inputControl = new ComboBox
                    {
                        Name = "cbIsActive",
                        Width = 100,
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Location = new Point(195, 157)
                    };
                    ((ComboBox)inputControl).Items.Add(new KeyValuePair<string, int>("True", 1));
                    ((ComboBox)inputControl).Items.Add(new KeyValuePair<string, int>("False", 0));

                    ((ComboBox)inputControl).DisplayMember = "Key";
                    ((ComboBox)inputControl).ValueMember = "Value";

                    ((ComboBox)inputControl).SelectedIndexChanged += inputControl_SelectedIndexChanged;
                    
                    break;
                default:
                    throw new ArgumentException($"Filter '{cbUserFilters.Text}' is not supported.");
            }
           
            currentSelectedFilter = inputControl;
            this.Controls.Add(inputControl);
            if (inputControl is ComboBox combo)
            {
                combo.SelectedIndex = 0;
                inputControl_SelectedIndexChanged(combo, new EventArgs());
                return;
            }

            ReFetchAllUsersData();
        }
        private void inputControl_TextChanged(object sender , EventArgs e) {
            Users = UserController.GetUsersBasedOnFilter("UserName" , ((TextBox)sender).Text);
            LoadDataToGridView();
        }
        private void inputControl_SelectedIndexChanged(object sender , EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;

            if (comboBox != null && comboBox.SelectedItem is KeyValuePair<string, int> selectedItem)
            {
                int selectedValue = selectedItem.Value;

                Users = UserController.GetUsersBasedOnFilter("IsActive", selectedValue.ToString());
                LoadDataToGridView();
            }

        }
    }
}
