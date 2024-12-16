using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM2
{
    public partial class frmHome : Form
    {
        public frmHome()
        {
            InitializeComponent();
        }

        private void btnTies_Click(object sender, EventArgs e)
        {
            frmTies  frmTies = new frmTies();
            frmTies.Show();
            this.Close();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            frmCategory frmCategory = new frmCategory();
            frmCategory.Show();
            this.Close();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            frmCustomer frmCustomer = new frmCustomer();    
            frmCustomer.Show();
            this.Close();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            frmStaff frmStaff = new frmStaff();
            frmStaff.Show();
            this.Close();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            frmOrder frmOrder = new frmOrder();
            frmOrder.Show();
            this.Close();
        }

        private void btnOderdetails_Click(object sender, EventArgs e)
        {
            frmOrderDetail frmOrderDetail = new frmOrderDetail();
            frmOrderDetail.Show();
            this.Close();
        }

        private void btnProviders_Click(object sender, EventArgs e)
        {
            frmProviders frmProviders = new frmProviders();
            frmProviders.Show();
            this.Close();
        }

        private void btnTiesDetail_Click(object sender, EventArgs e)
        {
            frmTiesDetails frmTiesDetails = new frmTiesDetails();
            frmTiesDetails.Show();
            this.Close();
        }
    }
}
