using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static ASM2.frmlogin;

namespace ASM2
{
    public partial class frmTies : Form
    {
        public frmTies()
        {
            InitializeComponent();
        }

        private void LoadDataToList()
        {
            string query = "SELECT TieID, TieName, CategoryID, ProviderID, Price, StockQuantity FROM Ties";

            // Sử dụng lớp DatabaseConnection để lấy chuỗi kết nối
            using (SqlConnection connection = new DatabaseConnection().GetConnection())
            {
                try
                {
                    connection.Open(); // Mở kết nối
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            lvListTie.Items.Clear();
                            while (reader.Read())
                            {
                                // Lấy từng cột dữ liệu
                                string tieID = reader["TieID"].ToString();
                                string tieName = reader["TieName"].ToString();
                                string categoryID = reader["CategoryID"].ToString();
                                string providerID = reader["ProviderID"].ToString();
                                string price = reader["Price"].ToString();
                                string stockQuantity = reader["StockQuantity"].ToString();

                                // Tạo một mục mới cho ListView
                                ListViewItem item = new ListViewItem(tieID);
                                item.SubItems.Add(tieName);
                                item.SubItems.Add(categoryID);
                                item.SubItems.Add(providerID);
                                item.SubItems.Add(price);
                                item.SubItems.Add(stockQuantity);

                                // Thêm mục vào ListView
                                lvListTie.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hiển thị thông báo lỗi nếu có
                    MessageBox.Show("Error loading data from Tie table: " + ex.Message);
                }
            }
        }

        private void AddTie()
        {
            string tiesID = txtTieID.Text;
            string tieName = txtTieName.Text;
            string categoryID = txtCategoryID.Text;
            string providerID = txtProviderID.Text;
            string price = txtPrice.Text;
            string StockQuantity = txtStockQuantity.Text;

            string query = "INSERT INTO Ties (TieName, CategoryID, ProviderID, Price, StockQuantity) VALUES (@TieName, @CategoryID, @ProviderID, @Price, @StockQuantity)";
            DatabaseConnection dbConnection = new DatabaseConnection();
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TiesID", tiesID);
                        cmd.Parameters.AddWithValue("@TieName", tieName);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                        cmd.Parameters.AddWithValue("@ProviderID", providerID);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("StockQuantity", StockQuantity);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tie was added successfully!");
                        LoadDataToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding Tie: " + ex.Message);
                }
            }
        }

        private void frmTies_Load(object sender, EventArgs e)
        {
            LoadDataToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTie();
        }

        private void DeleteTies()
        {
            string tieID = lvListTie.SelectedItems[0].SubItems[0].Text;

            string query = "DELETE FROM Ties WHERE TieID = @TieID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TieID", tieID);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tie was successfully deleted!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting Tie: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteTies();
        }

        private void SearchTies(string searchText)
        {
            string query = "SELECT TieID, TieName, CategoryID, ProviderID, Price, StockQuantity FROM Ties WHERE TieID LIKE @SearchText OR TieName LIKE @SearchText";
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
                            lvListTie.Items.Clear();

                            while (reader.Read())
                            {
                                string tieID = reader["TieID"].ToString();
                                string tieName = reader["TieName"].ToString();
                                string categoryID = reader["CategoryID"].ToString();
                                string providerID = reader["ProviderID"].ToString();
                                string price = reader["Price"].ToString();
                                string stockQuantity = reader["StockQuantity"].ToString();

                                ListViewItem item = new ListViewItem(tieID);
                                item.SubItems.Add(tieName);
                                item.SubItems.Add(categoryID);
                                item.SubItems.Add(providerID);
                                item.SubItems.Add(price);
                                item.SubItems.Add(stockQuantity);

                                lvListTie.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while searching for Tie: " + ex.Message);
                }
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            SearchTies(searchText);
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearTieInput();
        }
        private void ClearTieInput()
        {
            txtTieID.Clear();
            txtTieName.Clear();
            txtCategoryID.Clear();
            txtProviderID.Clear();
            txtPrice.Clear();
            txtStockQuantity.Clear();
            txtTieID.Focus();
        }

        private void UpdateTieLoad()
        {
            string tieID = txtTieID.Text;
            string tieName = txtTieName.Text;
            string categoryID = txtCategoryID.Text;
            string providerID = txtProviderID.Text;
            string price = txtPrice.Text;
            string stockQuantity = txtStockQuantity.Text;
            string query = "UPDATE Ties SET TieName = @TieName, CategoryID = @CategoryID, " +
                "ProviderID = @ProviderID, " +
                "Price = @Price, StockQuantity = @StockQuantity WHERE TieID = @TieID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@TieID", tieID);
                        cmd.Parameters.AddWithValue("@TieName", tieName);
                        cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                        cmd.Parameters.AddWithValue("@ProviderID", providerID);
                        cmd.Parameters.AddWithValue("@Price", price);
                        cmd.Parameters.AddWithValue("@StockQuantity", stockQuantity);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tie has been updated successfully!");
                        LoadDataToList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while updating Tie: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateTieLoad();
        }

        private void lvListTie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvListTie.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvListTie.SelectedItems[0];
                txtTieID.Text = selectedItem.SubItems[0].Text;
                txtTieName.Text = selectedItem.SubItems[1].Text;
                txtCategoryID.Text = selectedItem.SubItems[2].Text;
                txtProviderID.Text = selectedItem.SubItems[3].Text;
                txtPrice.Text = selectedItem.SubItems[4].Text;
                txtStockQuantity.Text = selectedItem.SubItems[5].Text;

            }
        }
    }
}
