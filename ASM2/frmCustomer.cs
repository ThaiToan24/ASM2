using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static ASM2.frmlogin;

namespace ASM2
{
    public partial class frmCustomer : Form
    {
        public frmCustomer()
        {
            InitializeComponent();
        }

        private void LoadCustomerToListView()
        {
            // Sử dụng DatabaseConnection để lấy chuỗi kết nối
            DatabaseConnection dbConnection = new DatabaseConnection();

            // Câu truy vấn SQL
            string query = "SELECT CustomerID, CustomerName, PhoneNumber, Email, Address FROM Customer";

            using (SqlConnection connection = dbConnection.GetConnection()) // Lấy SqlConnection từ DatabaseConnection
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            lvListCustomer.Items.Clear(); // Xóa dữ liệu cũ trong ListView

                            // Đọc từng bản ghi từ SqlDataReader
                            while (reader.Read())
                            {
                                // Lấy dữ liệu từ các cột
                                string customerId = reader["CustomerID"].ToString();
                                string customerName = reader["CustomerName"].ToString();
                                string phoneNumber = reader["PhoneNumber"].ToString();
                                string email = reader["Email"].ToString();
                                string address = reader["Address"].ToString();

                                // Tạo một ListViewItem và thêm các cột vào
                                ListViewItem item = new ListViewItem(customerId); // Cột đầu tiên
                                item.SubItems.Add(customerName); // Thêm cột phụ
                                item.SubItems.Add(phoneNumber);
                                item.SubItems.Add(email);
                                item.SubItems.Add(address);

                                // Thêm item vào ListView
                                lvListCustomer.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Customer data: " + ex.Message);
                }
            }
        }

        private void AddCustomer( )
        {
            string customerID = txtCustomerID.Text;
            string customerName = txtCustomerName.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string email = textEmail.Text;
            string address = txtAddress.Text;

            string query = "INSERT INTO Customer (CustomerName, PhoneNumber, Email, Address) VALUES (@CustomerName, @PhoneNumber, @Email, @Address)";
            DatabaseConnection dbConnection = new DatabaseConnection();
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd.Parameters.AddWithValue("@CustomerName", customerName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Address", address);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Customer added successfully!");
                        LoadCustomerToListView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding customer: " + ex.Message);
                }
            }
        }
        private void UpdateCustomer()
        {
            string customerID = txtCustomerID.Text;
            string customerName = txtCustomerName.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string email = textEmail.Text.Trim();
            string address = txtAddress.Text.Trim();
            string query = "UPDATE Customer SET CustomerName = @CustomerName, PhoneNumber = @PhoneNumber, Email = @Email, Address = @Address WHERE CustomerID = @CustomerID";

            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd.Parameters.AddWithValue("@CustomerName", customerName);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Address", address);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Customer has been updated successfully!");
                        LoadCustomerToListView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating customer: " + ex.Message);
                }
            }
        }

        private void DeleteCustomer()
        {
            string customerID = lvListCustomer.SelectedItems[0].SubItems[0].Text;

            string query = "DELETE FROM Customer WHERE CustomerID = @CustomerID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Customer has been deleted successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting customer: " + ex.Message);
                }
            }
        }

        private void SearchCustomer(string searchText)
        {
            string query = "SELECT CustomerID, CustomerName, PhoneNumber, Email, Address FROM Customer WHERE CustomerID LIKE @SearchText OR CustomerName LIKE @SearchText";
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
                            lvListCustomer.Items.Clear(); // Xóa dữ liệu cũ trong ListView

                            while (reader.Read())
                            {
                                string customerId = reader["CustomerID"].ToString();
                                string customerName = reader["CustomerName"].ToString();
                                string phoneNumber = reader["PhoneNumber"].ToString();
                                string email = reader["Email"].ToString();
                                string address = reader["Address"].ToString();

                                ListViewItem item = new ListViewItem(customerId);
                                item.SubItems.Add(customerName);
                                item.SubItems.Add(phoneNumber);
                                item.SubItems.Add(email);
                                item.SubItems.Add(address);

                                lvListCustomer.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when searching for customers: " + ex.Message);
                }
            }
        }




        private void frmCustomer_Load(object sender, EventArgs e)
        {
            LoadCustomerToListView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCustomer();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCustomer();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCustomer();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            SearchCustomer(searchText);
        }

        private void lvListCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvListCustomer.SelectedItems.Count > 0)
            {
                // Lấy mục được chọn
                ListViewItem selectedItem = lvListCustomer.SelectedItems[0];

                // Hiển thị thông tin từ mục đã chọn lên các TextBox
                txtCustomerID.Text = selectedItem.SubItems[0].Text;   // CustomerID
                txtCustomerName.Text = selectedItem.SubItems[1].Text; // CustomerName
                txtPhoneNumber.Text = selectedItem.SubItems[2].Text;  // PhoneNumber
                textEmail.Text = selectedItem.SubItems[3].Text;        // Email
                txtAddress.Text = selectedItem.SubItems[4].Text;      // Address
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtCustomerID.Clear();
            txtCustomerName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            textEmail.Clear();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmHome frmHome = new frmHome();
            frmHome.Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
