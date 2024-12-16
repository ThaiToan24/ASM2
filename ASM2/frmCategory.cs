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
    public partial class frmCategory : Form
    {
        public frmCategory()
        {
            InitializeComponent();
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            LoadDataCategory();
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

        private void LoadDataCategory()
        {
            
            string query = "SELECT CategoryID, CategoryName FROM Category";

            // Assuming DatabaseConnection is a custom class for connection
            using (SqlConnection connection = new DatabaseConnection().GetConnection())
            {
                try
                {
                    // Open the database connection
                    connection.Open();

                    // Prepare the command
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the command and retrieve data
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Clear existing items in the ListView to avoid duplication
                            lvListCategory.Items.Clear();

                            // Iterate through the rows of the result set
                            while (reader.Read())
                            {
                                // Read data from the current row
                                string categoryID = reader["CategoryID"].ToString();
                                string categoryName = reader["CategoryName"].ToString();

                                // Create a new ListViewItem with the categoryID as the first column
                                ListViewItem item = new ListViewItem(categoryID);

                                // Add the category name as a sub-item
                                item.SubItems.Add(categoryName);

                                // Add the ListViewItem to the ListView
                                lvListCategory.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Show error message if there's an exception
                    MessageBox.Show("An error occurred while loading data: " + ex.Message);
                }
            }


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddCategory();
        }
        private void AddCategory()
        {
            
            string categoryName = txtCategoryName.Text;  // We don't need CategoryID if it's auto-increment

            string query = "INSERT INTO Category (CategoryName) VALUES (@CategoryName)";

            // Assuming DatabaseConnection is a custom class to handle DB connections
            DatabaseConnection dbConnection = new DatabaseConnection();

            using (SqlConnection connection = dbConnection.GetConnection())
            {
                try
                {
                    connection.Open();

                    // Prepare the SQL command
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add the CategoryName parameter
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);

                        // Execute the command
                        cmd.ExecuteNonQuery();

                        // Optionally reload the data (make sure LoadDataCategory() refreshes your UI properly)
                        LoadDataCategory();

                        // Show success message
                        MessageBox.Show("Category added successfully!");
                    }
                }
                catch (Exception ex)
                {
                    // Show error message if something goes wrong
                    MessageBox.Show("Error adding category: " + ex.Message);
                }
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCategory();
        }
        private void UpdateCategory()
        {
            string categoryID = txtCategoryID.Text;
            string categoryName = txtCategoryName.Text;
            string query = "UPDATE Category SET CategoryName = @CategoryName WHERE CategoryID = @CategoryID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryID);
                        cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Category update successfully!");
                        LoadDataCategory();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while updating Category: " + ex.Message);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtCategoryID.Clear();
            txtCategoryName.Clear();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            SearchCategory(searchText);
        }
        private void SearchCategory(string searchText)
        {
            string query = "SELECT CategoryID, CategoryName FROM Category WHERE CategoryID LIKE @SearchKeyword";
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

                        // cmd.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            lvListCategory.Items.Clear();
                            while (reader.Read())
                            {
                                string categoryID = reader["CategoryID"].ToString();
                                string categoryName = reader["CategoryName"].ToString();

                                ListViewItem item = new ListViewItem(categoryID);
                                //item.SubItems.Add(orderID);
                                item.SubItems.Add(categoryID);
                                item.SubItems.Add(categoryName);
                                lvListCategory.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("error while searching category: " + ex.Message);
                }
            }
        }
        private void DeleteCategory()
        {
            string categoryID = lvListCategory.SelectedItems[0].SubItems[0].Text;
            string query = "DELETE FROM Category WHERE CategoryID = @CategoryID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@CategoryID", categoryID);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Category has been deleted successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting category: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteCategory();
        }

        

        private void lvListCategory_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            

            // Check if any item is selected in the ListView
            if (lvListCategory.SelectedItems.Count > 0)
            {
                // Get the first selected item
                ListViewItem selectedItem = lvListCategory.SelectedItems[0];

                // Set the TextBox values based on the selected item
                txtCategoryID.Text = selectedItem.SubItems[0].Text;
                txtCategoryName.Text = selectedItem.SubItems[1].Text;
            }
            else
            {
                // Optionally, clear the TextBox values if no item is selected
                txtCategoryID.Clear();
                txtCategoryName.Clear();
            }

        }
    }
}
