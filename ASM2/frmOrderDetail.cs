using System;
using System.Collections;
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
    public partial class frmOrderDetail : Form
    {
        public frmOrderDetail()
        {
            InitializeComponent();
        }

        private void LoadDataOrderDetai()
        {
            string query = "SELECT OrderDetailID, OrderID, TieID, Quantity, Price FROM OrderDetails";
            using (SqlConnection connection = new DatabaseConnection().GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            lvListOrderDetail.Items.Clear();
                            while (reader.Read())
                            {
                                string orderDetailID = reader["OrderDetailID"].ToString();
                                string orderID = reader["OrderID"].ToString();
                                string tieID = reader["TieID"].ToString();
                                string quantity = reader["Quantity"].ToString();
                                string price = reader["Price"].ToString();
                                ListViewItem item = new ListViewItem(orderDetailID);
                                item.SubItems.Add(orderID);
                                item.SubItems.Add(tieID);
                                item.SubItems.Add((quantity));
                                item.SubItems.Add(price);
                                lvListOrderDetail.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Order Detail data: " + ex.Message);
                }
            }
        }
        private void frmOrderDetail_Load(object sender, EventArgs e)
        {
            LoadDataOrderDetai();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddOrderDetail();
        }
        private void AddOrderDetail()
        {
            string orderDetailID = txtOrderDetailID.Text;
            string order = txtOrder.Text;
            string tieID = txtTieID.Text;
            string quantity = txtQuantity.Text;
            string price = txtPrice.Text;
            string query = "INSERT INTO OrderDetails (OrderID, TieID, Quantity, Price) VALUES (@OrderID, @TieID, @Quantity, @Price)";
            DatabaseConnection dbConnection = new DatabaseConnection();
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);
                        cmd.Parameters.AddWithValue("@OrderID", order);
                        cmd.Parameters.AddWithValue("@TieID", tieID);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("OrderDetail added successfully!");
                        LoadDataOrderDetai();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding OrderDetail: " + ex.Message);
                }
            }
        }

        private void lvListOrderDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvListOrderDetail.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvListOrderDetail.SelectedItems[0];
                txtOrderDetailID.Text = selectedItem.SubItems[0].Text;
                txtOrder.Text = selectedItem.SubItems[1].Text;
                txtTieID.Text = selectedItem.SubItems[2].Text;
                txtQuantity.Text = selectedItem.SubItems[3].Text;
                txtPrice.Text = selectedItem.SubItems[4].Text;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateOrderDetail();
        }
        private void UpdateOrderDetail()
        {
            string orderDetail = txtOrderDetailID.Text;
            string order = txtOrder.Text;
            string tieID = txtTieID.Text;
            string quantity = txtQuantity.Text;
            string price = txtPrice.Text;
            string query = "UPDATE OrderDetails SET OrderID = @Order, TieID = @TieID, Quantity = @Quantity, Price = @Price WHERE OrderDetailID = @OrderDetail";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderDetail", orderDetail);
                        cmd.Parameters.AddWithValue("@Order", order);
                        cmd.Parameters.AddWithValue("@TieID", tieID);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("OrderDetail update successfully!");
                        LoadDataOrderDetai();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while updating Order Detail: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteOrderDetail();
        }
        private void DeleteOrderDetail()
        {
            string orderDetail = lvListOrderDetail.SelectedItems[0].SubItems[0].Text;
            string query = "DELETE FROM OrderDetails WHERE OrderDetailID = @OrderDetail";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@OrderDetail", orderDetail);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("OrderDetail delete successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting Order Detail: " + ex.Message);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearOrderDetailInput();
        }
        private void ClearOrderDetailInput()
        {
            txtOrderDetailID.Clear();
            txtOrder.Clear();
            txtTieID.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();
            txtOrderDetailID.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            SearchOrderDetail(searchText);
        }
        private void SearchOrderDetail(string searchText)
        {
            string query = "SELECT OrderDetailID, OrderID, TieID, Quantity, Price FROM OrderDetails WHERE OrderDetailID LIKE @SearchKeyword";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        string searchValue = txtSearch.Text.Trim();
                        cmd.Parameters.AddWithValue("@SearchKeyword", searchValue);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            lvListOrderDetail.Items.Clear();
                            while (reader.Read())
                            {
                                string orderDetail = reader["OrderDetailID"].ToString();
                                string order = reader["OrderID"].ToString();
                                string tieID = reader["TieID"].ToString();
                                string quantity = reader["Quantity"].ToString();
                                string price = reader["Price"].ToString();

                                ListViewItem item = new ListViewItem(orderDetail);
                                //item.SubItems.Add(orderID);
                                item.SubItems.Add(order);
                                item.SubItems.Add(tieID);
                                item.SubItems.Add(quantity);
                                item.SubItems.Add(price);
                                lvListOrderDetail.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while searching Order Detail: " + ex.Message);
                }
            }
        }
    }
}
