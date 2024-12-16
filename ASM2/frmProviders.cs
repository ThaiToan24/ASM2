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
    public partial class frmProviders : Form
    {
        public frmProviders()
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

        private void frmProviders_Load(object sender, EventArgs e)
        {
            LoadDataProviders();
        }
        private void LoadDataProviders()
        {
            string query = "SELECT ProviderID, ProviderName, ContactPerson, PhoneNumber, Address FROM Providers";
            using (SqlConnection connection = new DatabaseConnection().GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            lvListProviders.Items.Clear();
                            while (reader.Read())
                            {
                                string providerID = reader["ProviderID"].ToString();
                                string providerName = reader["ProviderName"].ToString();
                                string contactPerson = reader["ContactPerson"].ToString();
                                string phoneNumber = reader["PhoneNumber"].ToString();
                                string address = reader["Address"].ToString();

                                ListViewItem item = new ListViewItem(providerID);
                                item.SubItems.Add(providerName);
                                item.SubItems.Add(contactPerson);
                                item.SubItems.Add(phoneNumber);
                                item.SubItems.Add(address);
                                lvListProviders.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading Provider data: " + ex.Message);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddProviders();
        }
        private void AddProviders()
        {
            string providerID = txtProviderID.Text;
            string providerName = txtProviderName.Text;
            string contactPerson = txtContactPerson.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string address = txtAddress.Text;
            string query = "INSERT INTO Providers (ProviderName, ContactPerson, PhoneNumber, Address) VALUES (@ProviderName, @ContactPerson, @PhoneNumber, @Address)";
            DatabaseConnection dbConnection = new DatabaseConnection();
            using (SqlConnection connection = dbConnection.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProviderID", providerID);
                        cmd.Parameters.AddWithValue("@ProviderName", providerName);
                        cmd.Parameters.AddWithValue("@ContactPerson", contactPerson);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Providers added successfully!");
                        LoadDataProviders();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding provider: " + ex.Message);
                }
            }
        }

        private void UpdateProviders()
        {
            string providerID = txtProviderID.Text;
            string providerName = txtProviderName.Text;
            string contactPerson = txtContactPerson.Text;
            string phoneNumber = txtPhoneNumber.Text;
            string address = txtAddress.Text;
            string query = "UPDATE Providers SET " +
                "ProviderName = @ProviderName, " +
                "ContactPerson = @ContactPerson, PhoneNumber = @PhoneNumber," +
                "Address = @Address WHERE ProviderID = @ProviderID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProviderID", providerID);
                        cmd.Parameters.AddWithValue("@ProviderName", providerName);
                        cmd.Parameters.AddWithValue("@ContactPerson", contactPerson);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        cmd.Parameters.AddWithValue("@Address", address);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Provider update successfully!");
                        LoadDataProviders();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while updating Provider: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateProviders();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteProviders();
        }
        private void DeleteProviders()
        {
            string ProviderID = lvListProviders.SelectedItems[0].SubItems[0].Text;
            string query = "DELETE FROM Providers WHERE ProviderID = @ProviderID";
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProviderID", ProviderID);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Provider delete successfully!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting Provider: " + ex.Message);
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearPrivdersInput();
        }
        private void ClearPrivdersInput()
        {
            txtProviderID.Clear();
            txtProviderName.Clear();
            txtContactPerson.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            txtProviderID.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text;
            SearchProviders(searchText);
        }

        private void SearchProviders(string searchText)
        {
            string query = "SELECT ProviderID, ProviderName, ContactPerson, PhoneNumber, Address FROM Providers WHERE ProviderID LIKE @SearchKeyword OR ProviderID LIKE @SearchKeyword";
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
                            lvListProviders.Items.Clear();
                            while (reader.Read())
                            {
                                string providerID = reader["ProviderID"].ToString();
                                string providerName = reader["ProviderName"].ToString();
                                string contactPerson = reader["ContactPerson"].ToString();
                                string phoneNumber = reader["PhoneNumber"].ToString();
                                string address = reader["Address"].ToString();

                                ListViewItem item = new ListViewItem(providerID);
                                item.SubItems.Add(providerName);
                                item.SubItems.Add(contactPerson);
                                item.SubItems.Add(phoneNumber);
                                item.SubItems.Add(address);
                                lvListProviders.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while searching for Provider: " + ex.Message);
                }
            }
        }


        private void lvListProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvListProviders.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvListProviders.SelectedItems[0];
                txtProviderID.Text = selectedItem.SubItems[0].Text;
                txtProviderName.Text = selectedItem.SubItems[1].Text;
                txtContactPerson.Text = selectedItem.SubItems[2].Text;
                txtPhoneNumber.Text = selectedItem.SubItems[3].Text;
                txtAddress.Text = selectedItem.SubItems[4].Text;

            }
        }
    }
}

