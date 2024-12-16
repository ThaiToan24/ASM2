namespace ASM2
{
    partial class frmlogin
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
            this.lbLogin = new System.Windows.Forms.Label();
            this.btnlogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lbUserName = new System.Windows.Forms.Label();
            this.lbPasswork = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPasswork = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbLogin
            // 
            this.lbLogin.AutoSize = true;
            this.lbLogin.Font = new System.Drawing.Font("MV Boli", 25.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLogin.Location = new System.Drawing.Point(219, 41);
            this.lbLogin.Name = "lbLogin";
            this.lbLogin.Size = new System.Drawing.Size(133, 55);
            this.lbLogin.TabIndex = 0;
            this.lbLogin.Text = "Login";
            this.lbLogin.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnlogin
            // 
            this.btnlogin.Font = new System.Drawing.Font("MV Boli", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogin.Location = new System.Drawing.Point(91, 332);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(142, 58);
            this.btnlogin.TabIndex = 1;
            this.btnlogin.Text = "Login";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("MV Boli", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(321, 332);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(142, 58);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lbUserName
            // 
            this.lbUserName.AutoSize = true;
            this.lbUserName.Font = new System.Drawing.Font("MV Boli", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUserName.Location = new System.Drawing.Point(36, 125);
            this.lbUserName.Name = "lbUserName";
            this.lbUserName.Size = new System.Drawing.Size(197, 44);
            this.lbUserName.TabIndex = 3;
            this.lbUserName.Text = "User Name";
            // 
            // lbPasswork
            // 
            this.lbPasswork.AutoSize = true;
            this.lbPasswork.Font = new System.Drawing.Font("MV Boli", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPasswork.Location = new System.Drawing.Point(36, 211);
            this.lbPasswork.Name = "lbPasswork";
            this.lbPasswork.Size = new System.Drawing.Size(164, 44);
            this.lbPasswork.TabIndex = 4;
            this.lbPasswork.Text = "Passwork";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(293, 144);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(293, 22);
            this.txtUserName.TabIndex = 5;
            // 
            // txtPasswork
            // 
            this.txtPasswork.Location = new System.Drawing.Point(293, 230);
            this.txtPasswork.Name = "txtPasswork";
            this.txtPasswork.Size = new System.Drawing.Size(293, 22);
            this.txtPasswork.TabIndex = 6;
            // 
            // frmlogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 451);
            this.Controls.Add(this.txtPasswork);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lbPasswork);
            this.Controls.Add(this.lbUserName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnlogin);
            this.Controls.Add(this.lbLogin);
            this.Name = "frmlogin";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbLogin;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lbUserName;
        private System.Windows.Forms.Label lbPasswork;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPasswork;
    }
}

