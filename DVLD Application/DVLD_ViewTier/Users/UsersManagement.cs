using DVLD_BusinessTier;
using DVLD_ViewTier.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVLD_ViewTier.Users
{
   
    public partial class UsersManagement : Form
    {
        private static DataTable Users = UserController.GetAllUsers();

        static Control currentSelectedFilter = null;

        public UsersManagement()
        {
            InitializeComponent();
        }

        private void UsersManagement_Load(object sender, EventArgs e)
        {
            cbUserFilters.SelectedIndex = 0;
            LoadDataToGridView();
            lbRecordCount.Text = $"# Records : {Users.Rows.Count}";
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
        private void UpdateRowInDataTable(int id , string userName , bool isActive)
        {
            try
            {
                DataRow row = Users.Rows.Find(id);
                row["UserName"] = userName;
                row["IsActive"] = isActive;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.DataStatusChanged += AddRowToDataTable;
            addUser.ShowDialog();
        }
        private void AddRowToDataTable(int id , string UserName , bool IsActive)
        {
            DataRow newRow = Users.NewRow();

            newRow["UserID"] = id;
            newRow["UserName"] = UserName;
            newRow["IsActive"] = IsActive;

            Users.Rows.Add(newRow);
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
                    ((ComboBox)inputControl).Items.Add(new KeyValuePair<string, int>("All", -1));

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

                if (selectedValue != -1)
                {
                    Users = UserController.GetUsersBasedOnFilter("IsActive", selectedValue.ToString());
                }
                else
                {
                    Users = UserController.GetAllUsers();
                }
                LoadDataToGridView();
            }

        }


        private void dgvUsersList_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvUsersList.IsCurrentCellDirty && dgvUsersList.CurrentCell is DataGridViewCheckBoxCell)
            {
                dgvUsersList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvUsersList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Get the row index of the row that was clicked
                var hitTestInfo = dgvUsersList.HitTest(e.X, e.Y);

                // Check if the click was on a row
                if (hitTestInfo.RowIndex >= 0)
                {
                    // Select the clicked row
                    dgvUsersList.ClearSelection();
                    dgvUsersList.Rows[hitTestInfo.RowIndex].Selected = true;

                    // Optionally, set the current cell if needed
                    dgvUsersList.CurrentCell = dgvUsersList.Rows[hitTestInfo.RowIndex].Cells[0];
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                int id = (int)(dgvUsersList.CurrentRow.Cells[0].Value);
                EditUser editScreen = new EditUser(id);
                editScreen.DataStatusChanged += UpdateRowInDataTable;
                editScreen.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : No User Selected");
            }
}

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)(dgvUsersList.CurrentRow.Cells[0].Value);
                if (UserController.DeleteUser(id))
                {
                    MessageBox.Show("User Deleted Successfully");
                }
                else
                {
                    MessageBox.Show("Error : Failed to delete the user");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : No User Selected");
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int id = (int)(dgvUsersList.CurrentRow.Cells[0].Value);
                ChangePassword changePassword = new ChangePassword(id);
                changePassword.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : No User Selected");
            }
        }
    }
}
