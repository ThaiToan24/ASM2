using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ASM2.frmlogin;

namespace ASM2
{
    public partial class frmOrder : Form
    {
        public frmOrder()
        {
            InitializeComponent();
        }

        private void LoadDataOrder()
        {
            
            string query = "SELECT OrderID, OrderDate, CustomerID, StaffID FROM Orders";
            using (SqlConnection connection = new DatabaseConnection().GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            lvListOrder.Items.Clear();
                            while (reader.Read())
                            {
                                string orderID = reader["OrderID"].ToString();
                                string customerID = reader["CustomerID"].ToString();
                                string staffID = reader["StaffID"].ToString();

                                // Handle the date conversion with a null check
                                DateTime dateTime = reader["OrderDate"] != DBNull.Value ? Convert.ToDateTime(reader["OrderDate"]) : DateTime.MinValue;

                                ListViewItem item = new ListViewItem(orderID);
                                item.SubItems.Add(dateTime.ToString("yyyy-MM-dd"));
                                item.SubItems.Add(customerID);
                                item.SubItems.Add(staffID);
                                lvListOrder.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Order data: " + ex.Message + "\n" + ex.StackTrace);
                }
            }

        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            LoadDataOrder();
        }

        private void AddOrder()
        {
            
            string orderID = txtOrderID.Text; // If OrderID is auto-generated, you can remove this line
            DateTime orderDate = dtpOrderDate.Value;  // Use the DateTime value from DateTimePicker
            string customerID = txtCustomerID.Text;
            string staffID = txtStaffID.Text;

            string query = "INSERT INTO Orders (OrderDate, CustomerID, StaffID) VALUES (@OrderDate, @CustomerID, @StaffID)";
            DatabaseConnection dbConnection = new DatabaseConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Remove OrderID from the parameters if it's auto-generated in the database
                        // If OrderID is not auto-generated, you can use the following line:
                        // cmd.Parameters.AddWithValue("@OrderID", orderID);

                        // Pass orderDate as a DateTime parameter
                        cmd.Parameters.AddWithValue("@OrderDate", orderDate);
                        cmd.Parameters.AddWithValue("@CustomerID", customerID);
                        cmd.Parameters.AddWithValue("@StaffID", staffID);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order added successfully!");
                        LoadDataOrder();  // Ensure this method is properly reloading the data
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding order: " + ex.Message);
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddOrder();
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

        private void DeleteOrder()
        {
            string orderID = lvListOrder.SelectedItems[0].SubItems[0].Text;
            string query = "DELETE FROM Orders WHERE OrderID = @OrderID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order deleted successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error when deleting Order: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteOrder();
        }
        private void UpdateOrder()
        {
            

            string orderID = txtOrderID.Text;
            string orderDate = dtpOrderDate.Text; // Ensure this is in the correct format
            string customerID = txtCustomerID.Text;
            string staffID = txtStaffID.Text;

            string query = "UPDATE Orders SET OrderDate = @OrderDate, CustomerID = @CustomerID, StaffID = @StaffID " +
                "WHERE OrderID = @OrderID";

            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Parameterize input values
                        cmd.Parameters.Add("@OrderID", SqlDbType.VarChar).Value = orderID;
                        cmd.Parameters.Add("@OrderDate", SqlDbType.DateTime).Value = DateTime.Parse(orderDate); // Ensure correct format
                        cmd.Parameters.Add("@CustomerID", SqlDbType.VarChar).Value = customerID;
                        cmd.Parameters.Add("@StaffID", SqlDbType.VarChar).Value = staffID;

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Order updated successfully!");
                        LoadDataOrder();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("SQL Error: " + ex.Message);
                    // Log exception here
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: " + ex.Message);
                    // Log exception here
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateOrder();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearOrderInput();
        }
        private void ClearOrderInput()
        {
            txtOrderID.Clear();
            //dtpOrderDate.Clear();
            txtStaffID.Clear();
            txtCustomerID.Clear();
            txtOrderID.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            SearchOrder(searchText);
        }

        private void SearchOrder(string searchText)
        {
            string query = "SELECT OrderID, OrderDate, StaffID, CustomerID FROM Orders WHERE OrderID LIKE @SearchKeyword";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        string searchValue = txtSearch.Text.Trim(); // Use the '%' character to search for similar
                        cmd.Parameters.AddWithValue("@SearchKeyword", searchValue);

                      //  cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            lvListOrder.Items.Clear();
                            while (reader.Read())
                            {
                                string orderID = reader["OrderID"].ToString();
                                string orderDate = reader["OrderDate"].ToString();
                                string staffID = reader["StaffID"].ToString();
                                string customerID = reader["CustomerID"].ToString();

                                ListViewItem item = new ListViewItem(orderID);
                                //item.SubItems.Add(orderID);
                                item.SubItems.Add(orderDate);
                                item.SubItems.Add(staffID);
                                item.SubItems.Add(customerID);
                                lvListOrder.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while searching Order: " + ex.Message);
                }
            }
        }

        private void lvListOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvListOrder.SelectedItems.Count > 0)
            {
                // Lấy mục được chọn
                ListViewItem selectedItem = lvListOrder.SelectedItems[0];

                // Hiển thị thông tin từ mục đã chọn lên các TextBox
                txtOrderID.Text = selectedItem.SubItems[0].Text;  
                dtpOrderDate.Text = selectedItem.SubItems[1].Text; 
                txtStaffID.Text = selectedItem.SubItems[2].Text;  
                txtCustomerID.Text = selectedItem.SubItems[3].Text;      
                
            }
        }
    }
}
