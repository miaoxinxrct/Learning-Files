namespace FocusingLensAligner
{
    partial class EpoxyOffset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EpoxyOffset));
            this.lab_EpoxyDipPosZ1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lab_AdjustStepZ1 = new System.Windows.Forms.Label();
            this.cmb_AdjustStepZ1 = new System.Windows.Forms.ComboBox();
            this.lab_Z1Up = new System.Windows.Forms.Label();
            this.lab_Z1Down = new System.Windows.Forms.Label();
            this.tb_EpoxyDipPosZ1 = new System.Windows.Forms.TextBox();
            this.btn_StopEpoxy = new System.Windows.Forms.Button();
            this.btn_EpoxyOK = new System.Windows.Forms.Button();
            this.btn_Z1Up = new System.Windows.Forms.Button();
            this.btn_Z1Down = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lab_EpoxyDipPosZ1
            // 
            this.lab_EpoxyDipPosZ1.AutoSize = true;
            this.lab_EpoxyDipPosZ1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_EpoxyDipPosZ1.Location = new System.Drawing.Point(46, 13);
            this.lab_EpoxyDipPosZ1.Name = "lab_EpoxyDipPosZ1";
            this.lab_EpoxyDipPosZ1.Size = new System.Drawing.Size(45, 13);
            this.lab_EpoxyDipPosZ1.TabIndex = 136;
            this.lab_EpoxyDipPosZ1.Text = "Axis_Z1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(150, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 141;
            this.label1.Text = "mm";
            // 
            // lab_AdjustStepZ1
            // 
            this.lab_AdjustStepZ1.AutoSize = true;
            this.lab_AdjustStepZ1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_AdjustStepZ1.Location = new System.Drawing.Point(59, 47);
            this.lab_AdjustStepZ1.Name = "lab_AdjustStepZ1";
            this.lab_AdjustStepZ1.Size = new System.Drawing.Size(33, 13);
            this.lab_AdjustStepZ1.TabIndex = 140;
            this.lab_AdjustStepZ1.Text = "步距";
            // 
            // cmb_AdjustStepZ1
            // 
            this.cmb_AdjustStepZ1.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_AdjustStepZ1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_AdjustStepZ1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmb_AdjustStepZ1.FormattingEnabled = true;
            this.cmb_AdjustStepZ1.Items.AddRange(new object[] {
            "0.005",
            "0.001",
            "0.05",
            "0.01",
            "0.5",
            "0.1"});
            this.cmb_AdjustStepZ1.Location = new System.Drawing.Point(95, 43);
            this.cmb_AdjustStepZ1.MaxDropDownItems = 11;
            this.cmb_AdjustStepZ1.Name = "cmb_AdjustStepZ1";
            this.cmb_AdjustStepZ1.Size = new System.Drawing.Size(52, 21);
            this.cmb_AdjustStepZ1.TabIndex = 139;
            this.cmb_AdjustStepZ1.Text = "0.01";
            // 
            // lab_Z1Up
            // 
            this.lab_Z1Up.AutoSize = true;
            this.lab_Z1Up.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Z1Up.Location = new System.Drawing.Point(101, 84);
            this.lab_Z1Up.Name = "lab_Z1Up";
            this.lab_Z1Up.Size = new System.Drawing.Size(37, 13);
            this.lab_Z1Up.TabIndex = 137;
            this.lab_Z1Up.Text = "上升-";
            // 
            // lab_Z1Down
            // 
            this.lab_Z1Down.AutoSize = true;
            this.lab_Z1Down.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_Z1Down.Location = new System.Drawing.Point(101, 232);
            this.lab_Z1Down.Name = "lab_Z1Down";
            this.lab_Z1Down.Size = new System.Drawing.Size(40, 13);
            this.lab_Z1Down.TabIndex = 138;
            this.lab_Z1Down.Text = "下降+";
            // 
            // tb_EpoxyDipPosZ1
            // 
            this.tb_EpoxyDipPosZ1.BackColor = System.Drawing.Color.Black;
            this.tb_EpoxyDipPosZ1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_EpoxyDipPosZ1.ForeColor = System.Drawing.Color.Lime;
            this.tb_EpoxyDipPosZ1.Location = new System.Drawing.Point(94, 9);
            this.tb_EpoxyDipPosZ1.Name = "tb_EpoxyDipPosZ1";
            this.tb_EpoxyDipPosZ1.ReadOnly = true;
            this.tb_EpoxyDipPosZ1.Size = new System.Drawing.Size(53, 20);
            this.tb_EpoxyDipPosZ1.TabIndex = 133;
            this.tb_EpoxyDipPosZ1.Text = "0.000";
            // 
            // btn_StopEpoxy
            // 
            this.btn_StopEpoxy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_StopEpoxy.ForeColor = System.Drawing.Color.Black;
            this.btn_StopEpoxy.Location = new System.Drawing.Point(61, 264);
            this.btn_StopEpoxy.Name = "btn_StopEpoxy";
            this.btn_StopEpoxy.Size = new System.Drawing.Size(113, 30);
            this.btn_StopEpoxy.TabIndex = 142;
            this.btn_StopEpoxy.Text = "停止运行";
            this.btn_StopEpoxy.UseVisualStyleBackColor = true;
            this.btn_StopEpoxy.Click += new System.EventHandler(this.btn_StopEpoxy_Click);
            // 
            // btn_EpoxyOK
            // 
            this.btn_EpoxyOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EpoxyOK.ForeColor = System.Drawing.Color.Black;
            this.btn_EpoxyOK.Location = new System.Drawing.Point(60, 308);
            this.btn_EpoxyOK.Name = "btn_EpoxyOK";
            this.btn_EpoxyOK.Size = new System.Drawing.Size(113, 30);
            this.btn_EpoxyOK.TabIndex = 143;
            this.btn_EpoxyOK.Text = "继续";
            this.btn_EpoxyOK.UseVisualStyleBackColor = true;
            this.btn_EpoxyOK.Click += new System.EventHandler(this.btn_EpoxyOK_Click);
            // 
            // btn_Z1Up
            // 
            this.btn_Z1Up.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Z1Up.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Z1Up.BackgroundImage")));
            this.btn_Z1Up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Z1Up.Location = new System.Drawing.Point(99, 99);
            this.btn_Z1Up.Name = "btn_Z1Up";
            this.btn_Z1Up.Size = new System.Drawing.Size(41, 57);
            this.btn_Z1Up.TabIndex = 134;
            this.btn_Z1Up.UseVisualStyleBackColor = false;
            this.btn_Z1Up.Click += new System.EventHandler(this.btn_Z1Up_Click);
            // 
            // btn_Z1Down
            // 
            this.btn_Z1Down.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_Z1Down.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Z1Down.BackgroundImage")));
            this.btn_Z1Down.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Z1Down.Location = new System.Drawing.Point(99, 172);
            this.btn_Z1Down.Name = "btn_Z1Down";
            this.btn_Z1Down.Size = new System.Drawing.Size(41, 58);
            this.btn_Z1Down.TabIndex = 135;
            this.btn_Z1Down.UseVisualStyleBackColor = false;
            this.btn_Z1Down.Click += new System.EventHandler(this.btn_Z1Down_Click);
            // 
            // EpoxyOffset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 352);
            this.Controls.Add(this.btn_EpoxyOK);
            this.Controls.Add(this.btn_StopEpoxy);
            this.Controls.Add(this.lab_EpoxyDipPosZ1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lab_AdjustStepZ1);
            this.Controls.Add(this.cmb_AdjustStepZ1);
            this.Controls.Add(this.btn_Z1Up);
            this.Controls.Add(this.lab_Z1Up);
            this.Controls.Add(this.lab_Z1Down);
            this.Controls.Add(this.tb_EpoxyDipPosZ1);
            this.Controls.Add(this.btn_Z1Down);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EpoxyOffset";
            this.Text = "EpoxyOffset";
            this.Load += new System.EventHandler(this.EpoxyOffset_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_EpoxyDipPosZ1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lab_AdjustStepZ1;
        private System.Windows.Forms.ComboBox cmb_AdjustStepZ1;
        private System.Windows.Forms.Button btn_Z1Up;
        private System.Windows.Forms.Label lab_Z1Up;
        private System.Windows.Forms.Label lab_Z1Down;
        private System.Windows.Forms.TextBox tb_EpoxyDipPosZ1;
        private System.Windows.Forms.Button btn_Z1Down;
        private System.Windows.Forms.Button btn_StopEpoxy;
        private System.Windows.Forms.Button btn_EpoxyOK;
    }
}