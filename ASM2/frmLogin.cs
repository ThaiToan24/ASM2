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
    public partial class frmlogin : Form
    {
        public frmlogin()
        {
            InitializeComponent();
        }

        public class DatabaseConnection
        {
            // Chuỗi kết nối đến SQL Server
            private readonly string connectionString;

            // Constructor khởi tạo chuỗi kết nối
            public DatabaseConnection()
            {
                connectionString = @"Server=TRANNGOTHAITOAN\TONTEN;Database=TiesSalesManagementSystem;Integrated Security=True;";
            }

            // Hàm trả về đối tượng SqlConnection
            public SqlConnection GetConnection()
            {
                return new SqlConnection(connectionString);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPasswork.Clear();
            txtUserName.Clear();
        }

        private void btnlogin_Click(object sender, EventArgs e)
        {
            // Lấy thông tin người dùng từ các TextBox
            string username = txtUserName.Text;
            string password = txtPasswork.Text;

            // Kiểm tra đăng nhập
            //bool isValid = DatabaseConnection.CheckLogin(username, password);

            if (CheckedLogin(username, password))
            {
                //MessageBox.Show("Đăng nhập thành công!");
                // Mở cửa sổ hoặc thực hiện hành động tiếp theo
                frmHome home = new frmHome();
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
            }
        }


        // Check login
        bool CheckedLogin(string username, string password)
        {
            // Lấy chuỗi kết nối từ DatabaseHelper
            DatabaseConnection conn = new DatabaseConnection();
            using (SqlConnection connection = conn.GetConnection())

            // Câu lệnh SQL để kiểm tra người dùng và mật khẩu


            // Mở kết nối và thực hiện truy vấn
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(1) FROM LOGIN WHERE Users = @username AND Password = @password";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Thêm tham số vào câu lệnh SQL
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        // Kiểm tra kết quả
                        int count = Convert.ToInt32(cmd.ExecuteScalar());  // Sẽ trả về 1 nếu tìm thấy người dùng hợp lệ, 0 nếu không
                        return count == 1;  // Nếu có người dùng hợp lệ, trả về true
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có (nếu không thể kết nối cơ sở dữ liệu)
                    Console.WriteLine("Lỗi kết nối: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
          
        
    

