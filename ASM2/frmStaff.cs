using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static ASM2.frmlogin;

namespace ASM2
{
    public partial class frmStaff : Form
    {
        public frmStaff()
        {
            InitializeComponent();
        }

        private void LoadStaffToList()
        {
            string query = "SELECT StaffID, StaffName, PhoneNumber FROM Staff";
            using (SqlConnection connection = new DatabaseConnection().GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            lvListStaff.Items.Clear();
                            while (reader.Read())
                            {
                                // Lấy dữ liệu từ từng dòng
                                string staffID = reader["StaffID"].ToString();
                                string staffName = reader["StaffName"].ToString();
                                string phoneNumber = reader["PhoneNumber"].ToString();

                                // Tạo ListViewItem mới
                                ListViewItem item = new ListViewItem(staffID);
                                item.SubItems.Add(staffName);
                                item.SubItems.Add(phoneNumber);

                                // Thêm vào ListView
                                lvListStaff.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Staff data: " + ex.Message);
                }
            }
        }

        private void frmStaff_Load_1(object sender, EventArgs e)
        {
            LoadStaffToList();
        }

        private void AddStaff()
        {
            string staffID = txtStaffID.Text;
            string staffName = txtStaffName.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string query = "INSERT INTO Staff (StaffName, PhoneNumber) VALUES (@StaffName, @PhoneNumber)";

            DatabaseConnection dbConnection = new DatabaseConnection();
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@StaffID", staffID);
                        cmd.Parameters.AddWithValue("@StaffName", staffName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Staff added successfully!");
                        LoadStaffToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding Staff: " + ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddStaff();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
         this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome();
            frmHome.Show();
            this.Hide();
        }
        private void DeleteStaff()
        {
            string staffID = lvListStaff.SelectedItems[0].SubItems[0].Text;
            string query = "DELETE FROM Staff WHERE StaffID = @StaffID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@StaffID", staffID);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Staff has been deleted successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting Staff: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteStaff();
        }
        
        private void SearchStaff(string searchText)
        {
            string query = "SELECT StaffID, StaffName, PhoneNumber FROM Staff WHERE StaffID LIKE @SearchText OR StaffID LIKE @SearchText ";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            lvListStaff.Items.Clear();
                            while (reader.Read())
                            {
                                string staffID = reader["StaffID"].ToString();
                                string staffName = reader["StaffName"].ToString();
                                string phoneNumber = reader["PhoneNumber"].ToString();
                                ListViewItem item = new ListViewItem(staffID);
                                item.SubItems.Add(staffName);
                                item.SubItems.Add(phoneNumber);
                                lvListStaff.Items.Add(item);
                            }
                        }
                    }        
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while searching for Staff: " + ex.Message);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            SearchStaff(searchText);
        }

        private void UpdateStaff()
        {
            string staffID = txtStaffID.Text;
            string staffName = txtStaffName.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string query = "UPDATE Staff SET StaffName = @StaffName, PhoneNumber = @PhoneNumber WHERE StaffID = @StaffID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@StaffID", staffID);
                        cmd.Parameters.AddWithValue("@StaffName", staffName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Staff has been updated successfully!");
                        LoadStaffToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating Staff: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateStaff();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtStaffID.Clear();
            txtStaffName.Clear();
            txtPhoneNumber.Clear();
        }

        private void lvListStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvListStaff.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvListStaff.SelectedItems[0];
                txtStaffID.Text = selectedItem.SubItems[0].Text;
                txtStaffName.Text = selectedItem.SubItems[1].Text;
                txtPhoneNumber.Text = selectedItem.SubItems[2].Text;

            }
        }
    }
}


