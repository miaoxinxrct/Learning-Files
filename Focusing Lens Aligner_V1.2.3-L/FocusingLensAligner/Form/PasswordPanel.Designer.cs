namespace FocusingLensAligner
{
    partial class PasswordPanel
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
            this.lb_Password = new System.Windows.Forms.Label();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.btn_ConfirmPassword = new System.Windows.Forms.Button();
            this.btn_DeletePasswordString = new System.Windows.Forms.Button();
            this.chkbox_ShowPasswordString = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lb_Password
            // 
            this.lb_Password.AutoSize = true;
            this.lb_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Password.Location = new System.Drawing.Point(28, 27);
            this.lb_Password.Name = "lb_Password";
            this.lb_Password.Size = new System.Drawing.Size(98, 13);
            this.lb_Password.TabIndex = 0;
            this.lb_Password.Text = "请输入验证密码";
            // 
            // tb_Password
            // 
            this.tb_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Password.Location = new System.Drawing.Point(31, 50);
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.Size = new System.Drawing.Size(265, 26);
            this.tb_Password.TabIndex = 1;
            // 
            // btn_ConfirmPassword
            // 
            this.btn_ConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ConfirmPassword.Location = new System.Drawing.Point(186, 100);
            this.btn_ConfirmPassword.Name = "btn_ConfirmPassword";
            this.btn_ConfirmPassword.Size = new System.Drawing.Size(82, 37);
            this.btn_ConfirmPassword.TabIndex = 2;
            this.btn_ConfirmPassword.Text = "确认";
            this.btn_ConfirmPassword.UseVisualStyleBackColor = true;
            this.btn_ConfirmPassword.Click += new System.EventHandler(this.btn_ConfirmPassword_Click);
            // 
            // btn_DeletePasswordString
            // 
            this.btn_DeletePasswordString.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DeletePasswordString.Location = new System.Drawing.Point(59, 100);
            this.btn_DeletePasswordString.Name = "btn_DeletePasswordString";
            this.btn_DeletePasswordString.Size = new System.Drawing.Size(82, 37);
            this.btn_DeletePasswordString.TabIndex = 3;
            this.btn_DeletePasswordString.Text = "清除输入";
            this.btn_DeletePasswordString.UseVisualStyleBackColor = true;
            this.btn_DeletePasswordString.Click += new System.EventHandler(this.btn_DeletePasswordString_Click);
            // 
            // chkbox_ShowPasswordString
            // 
            this.chkbox_ShowPasswordString.AutoSize = true;
            this.chkbox_ShowPasswordString.Location = new System.Drawing.Point(187, 26);
            this.chkbox_ShowPasswordString.Name = "chkbox_ShowPasswordString";
            this.chkbox_ShowPasswordString.Size = new System.Drawing.Size(98, 17);
            this.chkbox_ShowPasswordString.TabIndex = 4;
            this.chkbox_ShowPasswordString.Text = "显示输入字符";
            this.chkbox_ShowPasswordString.UseVisualStyleBackColor = true;
            this.chkbox_ShowPasswordString.CheckedChanged += new System.EventHandler(this.chkbox_ShowPasswordString_CheckedChanged);
            // 
            // PasswordPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 159);
            this.Controls.Add(this.chkbox_ShowPasswordString);
            this.Controls.Add(this.btn_DeletePasswordString);
            this.Controls.Add(this.btn_ConfirmPassword);
            this.Controls.Add(this.tb_Password);
            this.Controls.Add(this.lb_Password);
            this.Name = "PasswordPanel";
            this.Text = "PasswordPanel";
            this.Load += new System.EventHandler(this.PasswordPanel_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_Password;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Button btn_ConfirmPassword;
        private System.Windows.Forms.Button btn_DeletePasswordString;
        private System.Windows.Forms.CheckBox chkbox_ShowPasswordString;
    }
}