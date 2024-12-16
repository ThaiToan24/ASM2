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
    public partial class frmTiesDetails : Form
    {
        public frmTiesDetails()
        {
            InitializeComponent();
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


        private void LoadDataTiesDetails()
        {
            string query = "SELECT TieDetailID, TieID, OrderID, Quantity, Price FROM TiesDetail";
            using (SqlConnection connection = new DatabaseConnection().GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            lvListTieDetailID.Items.Clear();
                            while (reader.Read())
                            {
                                string tieDetailID = reader["TieDetailID"].ToString();
                                string tieID = reader["TieID"].ToString();
                                string orderID = reader["OrderID"].ToString();
                                string quantity = reader["Quantity"].ToString();
                                string price = reader["Price"].ToString();

                                ListViewItem item = new ListViewItem(tieDetailID);
                                item.SubItems.Add(tieID);
                                item.SubItems.Add(orderID);
                                item.SubItems.Add(quantity);
                                item.SubItems.Add(price);
                                lvListTieDetailID.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Tie Detail data: " + ex.Message);
                }
            }
        }

        private void frmTiesDetails_Load(object sender, EventArgs e)
        {
            LoadDataTiesDetails();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTiesDetails();
        }

        private void AddTiesDetails()
        {
            string tieID = txtTieID.Text;
            string orderID = txtOrderID.Text;
            string quantity = txtQuantity.Text;
            string price = txtPrice.Text;
            string query = "INSERT INTO TiesDetail (TieID, OrderID, Quantity, Price) VALUES (@TieID, @OrderID, @Quantity, @Price)";
            DatabaseConnection dbConnection = new DatabaseConnection();
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TieID", tieID);
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tie Detail added successfully!");
                        LoadDataTiesDetails();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding Tie Detail: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateTiesDetails();
        }
        private void UpdateTiesDetails()
        {
            string tieDetailID = txtTieDetailID.Text;
            string tieID = txtTieID.Text;
            string orderID = txtOrderID.Text;
            string quantity = txtQuantity.Text;
            string price = txtPrice.Text;
            string query = "UPDATE TiesDetail SET " +
                "TieID = @TieID, " +
                "OrderID = @OrderID, " +
                "Quantity = @Quantity, Price = @Price WHERE TieDetailID = @TieDetailID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TieDetailID", tieDetailID);
                        cmd.Parameters.AddWithValue("@TieID", tieID);
                        cmd.Parameters.AddWithValue("@OrderID", orderID);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("TiesDetail update successfully!");
                        LoadDataTiesDetails();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while updating Tie Detail: " + ex.Message);
                }
            }
        }

        private void lvListTieDetailID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvListTieDetailID.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvListTieDetailID.SelectedItems[0];

                txtTieDetailID.Text = selectedItem.SubItems[0].Text;
                txtTieID.Text = selectedItem.SubItems[1].Text;
                txtOrderID.Text = selectedItem.SubItems[2].Text;
                txtQuantity.Text = selectedItem.SubItems[3].Text;
                txtPrice.Text = selectedItem.SubItems[4].Text;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteTiesDetails();
        }
        private void DeleteTiesDetails()
        {
            string TieDetailID = lvListTieDetailID.SelectedItems[0].SubItems[0].Text;
            string query = "DELETE FROM TiesDetail WHERE TieDetailID = @TieDetailID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TieDetailID", TieDetailID);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("TiesDetail delete successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting Tie Detail: " + ex.Message);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearTiesDetailsInput();
        }
        private void ClearTiesDetailsInput()
        {
            txtTieDetailID.Clear();
            txtTieID.Clear();
            txtOrderID.Clear();
            txtQuantity.Clear();
            txtPrice.Clear();   
            txtTieDetailID.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            SearchTiesDetails(searchText);
        }
        private void SearchTiesDetails(string searchText)
        {
            string query = "SELECT TieDetailID, TieID, OrderID, Quantity, Price FROM TiesDetail WHERE TieDetailID LIKE @SearchKeyword";
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
                            lvListTieDetailID.Items.Clear();
                            while (reader.Read())
                            {
                                string tieDetailID = reader["TieDetailID"].ToString();
                                string tieID = reader["TieID"].ToString();
                                string orderID = reader["OrderID"].ToString();
                                string quantity = reader["Quantity"].ToString();
                                string price = reader["Price"].ToString();

                                ListViewItem item = new ListViewItem(tieDetailID);
                                item.SubItems.Add(tieID);
                                item.SubItems.Add(orderID);
                                item.SubItems.Add(quantity);
                                item.SubItems.Add(price);
                                lvListTieDetailID.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while searching Tie Detail: " + ex.Message);
                }
            }
        }
    }
}
