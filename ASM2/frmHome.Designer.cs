namespace ASM2
{
    partial class frmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStaff = new System.Windows.Forms.Button();
            this.lbHome = new System.Windows.Forms.Label();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnOrder = new System.Windows.Forms.Button();
            this.btnCategory = new System.Windows.Forms.Button();
            this.btnTies = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOderdetails = new System.Windows.Forms.Button();
            this.btnTiesDetail = new System.Windows.Forms.Button();
            this.btnProviders = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStaff
            // 
            this.btnStaff.Location = new System.Drawing.Point(12, 291);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Size = new System.Drawing.Size(124, 74);
            this.btnStaff.TabIndex = 0;
            this.btnStaff.Text = "Staff";
            this.btnStaff.UseVisualStyleBackColor = true;
            this.btnStaff.Click += new System.EventHandler(this.btnStaff_Click);
            // 
            // lbHome
            // 
            this.lbHome.AutoSize = true;
            this.lbHome.Font = new System.Drawing.Font("MV Boli", 28.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHome.Location = new System.Drawing.Point(296, 33);
            this.lbHome.Name = "lbHome";
            this.lbHome.Size = new System.Drawing.Size(153, 62);
            this.lbHome.TabIndex = 1;
            this.lbHome.Text = "Home";
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(12, 211);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(124, 74);
            this.btnCustomer.TabIndex = 3;
            this.btnCustomer.Text = "Customer";
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(154, 211);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(124, 74);
            this.btnOrder.TabIndex = 4;
            this.btnOrder.Text = "Order";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnCategory
            // 
            this.btnCategory.Location = new System.Drawing.Point(12, 129);
            this.btnCategory.Name = "btnCategory";
            this.btnCategory.Size = new System.Drawing.Size(124, 74);
            this.btnCategory.TabIndex = 5;
            this.btnCategory.Text = "Category";
            this.btnCategory.UseVisualStyleBackColor = true;
            this.btnCategory.Click += new System.EventHandler(this.btnCategory_Click);
            // 
            // btnTies
            // 
            this.btnTies.Location = new System.Drawing.Point(154, 131);
            this.btnTies.Name = "btnTies";
            this.btnTies.Size = new System.Drawing.Size(124, 74);
            this.btnTies.TabIndex = 6;
            this.btnTies.Text = "Ties";
            this.btnTies.UseVisualStyleBackColor = true;
            this.btnTies.Click += new System.EventHandler(this.btnTies_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ASM2.Properties.Resources.ca_vat_1;
            this.pictureBox1.Location = new System.Drawing.Point(376, 129);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(412, 295);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btnOderdetails
            // 
            this.btnOderdetails.Location = new System.Drawing.Point(154, 291);
            this.btnOderdetails.Name = "btnOderdetails";
            this.btnOderdetails.Size = new System.Drawing.Size(124, 74);
            this.btnOderdetails.TabIndex = 7;
            this.btnOderdetails.Text = "Oderdetails";
            this.btnOderdetails.UseVisualStyleBackColor = true;
            this.btnOderdetails.Click += new System.EventHandler(this.btnOderdetails_Click);
            // 
            // btnTiesDetail
            // 
            this.btnTiesDetail.Location = new System.Drawing.Point(154, 371);
            this.btnTiesDetail.Name = "btnTiesDetail";
            this.btnTiesDetail.Size = new System.Drawing.Size(124, 74);
            this.btnTiesDetail.TabIndex = 10;
            this.btnTiesDetail.Text = "TiesDetail";
            this.btnTiesDetail.UseVisualStyleBackColor = true;
            this.btnTiesDetail.Click += new System.EventHandler(this.btnTiesDetail_Click);
            // 
            // btnProviders
            // 
            this.btnProviders.Location = new System.Drawing.Point(12, 371);
            this.btnProviders.Name = "btnProviders";
            this.btnProviders.Size = new System.Drawing.Size(124, 74);
            this.btnProviders.TabIndex = 11;
            this.btnProviders.Text = "Providers";
            this.btnProviders.UseVisualStyleBackColor = true;
            this.btnProviders.Click += new System.EventHandler(this.btnProviders_Click);
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnProviders);
            this.Controls.Add(this.btnTiesDetail);
            this.Controls.Add(this.btnOderdetails);
            this.Controls.Add(this.btnTies);
            this.Controls.Add(this.btnCategory);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.btnCustomer);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbHome);
            this.Controls.Add(this.btnStaff);
            this.Name = "frmHome";
            this.Text = "frmHome";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.Label lbHome;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.Button btnCategory;
        private System.Windows.Forms.Button btnTies;
        private System.Windows.Forms.Button btnOderdetails;
        private System.Windows.Forms.Button btnTiesDetail;
        private System.Windows.Forms.Button btnProviders;
    }
}