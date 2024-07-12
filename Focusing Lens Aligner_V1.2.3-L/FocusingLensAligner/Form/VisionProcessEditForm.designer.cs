namespace FocusingLensAligner
{
    partial class VisionProcessEditForm
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
            this.cogToolBlockEditV21 = new Cognex.VisionPro.ToolBlock.CogToolBlockEditV2();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Run = new System.Windows.Forms.Button();
            this.tb_ProductName = new System.Windows.Forms.TextBox();
            this.lab_ProductName = new System.Windows.Forms.Label();
            this.cmb_VisionProcessType = new System.Windows.Forms.ComboBox();
            this.lab_VisionProcessType = new System.Windows.Forms.Label();
            this.gbx_out = new System.Windows.Forms.GroupBox();
            this.lab_Unit5 = new System.Windows.Forms.Label();
            this.lab_Unit4 = new System.Windows.Forms.Label();
            this.lab_Unit3 = new System.Windows.Forms.Label();
            this.tb_PointY = new System.Windows.Forms.TextBox();
            this.lab_PointY = new System.Windows.Forms.Label();
            this.lab_Unit2 = new System.Windows.Forms.Label();
            this.lab_Unit1 = new System.Windows.Forms.Label();
            this.tb_Diameter = new System.Windows.Forms.TextBox();
            this.lab_Diameter = new System.Windows.Forms.Label();
            this.tb_PointX = new System.Windows.Forms.TextBox();
            this.lab_PointX = new System.Windows.Forms.Label();
            this.tb_ImageCenterY = new System.Windows.Forms.TextBox();
            this.lab_ImageCenterY = new System.Windows.Forms.Label();
            this.tb_ImageCenterX = new System.Windows.Forms.TextBox();
            this.lab_ImageCenterX = new System.Windows.Forms.Label();
            this.btnLoadImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).BeginInit();
            this.gbx_out.SuspendLayout();
            this.SuspendLayout();
            // 
            // cogToolBlockEditV21
            // 
            this.cogToolBlockEditV21.AllowDrop = true;
            this.cogToolBlockEditV21.ContextMenuCustomizer = null;
            this.cogToolBlockEditV21.Location = new System.Drawing.Point(2, 77);
            this.cogToolBlockEditV21.MinimumSize = new System.Drawing.Size(489, 0);
            this.cogToolBlockEditV21.Name = "cogToolBlockEditV21";
            this.cogToolBlockEditV21.ShowNodeToolTips = true;
            this.cogToolBlockEditV21.Size = new System.Drawing.Size(849, 448);
            this.cogToolBlockEditV21.SuspendElectricRuns = false;
            this.cogToolBlockEditV21.TabIndex = 0;
            // 
            // btn_Exit
            // 
            this.btn_Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Exit.Location = new System.Drawing.Point(743, 19);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(88, 40);
            this.btn_Exit.TabIndex = 35;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(649, 19);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(88, 40);
            this.btn_Save.TabIndex = 34;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Run
            // 
            this.btn_Run.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Run.Location = new System.Drawing.Point(555, 19);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(88, 40);
            this.btn_Run.TabIndex = 36;
            this.btn_Run.Text = "Run";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // tb_ProductName
            // 
            this.tb_ProductName.Enabled = false;
            this.tb_ProductName.Location = new System.Drawing.Point(88, 12);
            this.tb_ProductName.Name = "tb_ProductName";
            this.tb_ProductName.Size = new System.Drawing.Size(246, 20);
            this.tb_ProductName.TabIndex = 38;
            // 
            // lab_ProductName
            // 
            this.lab_ProductName.AutoSize = true;
            this.lab_ProductName.Enabled = false;
            this.lab_ProductName.Location = new System.Drawing.Point(10, 15);
            this.lab_ProductName.Name = "lab_ProductName";
            this.lab_ProductName.Size = new System.Drawing.Size(75, 13);
            this.lab_ProductName.TabIndex = 37;
            this.lab_ProductName.Text = "Product Name";
            // 
            // cmb_VisionProcessType
            // 
            this.cmb_VisionProcessType.FormattingEnabled = true;
            this.cmb_VisionProcessType.Location = new System.Drawing.Point(88, 45);
            this.cmb_VisionProcessType.Name = "cmb_VisionProcessType";
            this.cmb_VisionProcessType.Size = new System.Drawing.Size(246, 21);
            this.cmb_VisionProcessType.TabIndex = 40;
            this.cmb_VisionProcessType.SelectedIndexChanged += new System.EventHandler(this.cmb_VisionProcessType_SelectedIndexChanged);
            // 
            // lab_VisionProcessType
            // 
            this.lab_VisionProcessType.AutoSize = true;
            this.lab_VisionProcessType.Location = new System.Drawing.Point(13, 49);
            this.lab_VisionProcessType.Name = "lab_VisionProcessType";
            this.lab_VisionProcessType.Size = new System.Drawing.Size(72, 13);
            this.lab_VisionProcessType.TabIndex = 41;
            this.lab_VisionProcessType.Text = "Process Type";
            // 
            // gbx_out
            // 
            this.gbx_out.Controls.Add(this.lab_Unit5);
            this.gbx_out.Controls.Add(this.lab_Unit4);
            this.gbx_out.Controls.Add(this.lab_Unit3);
            this.gbx_out.Controls.Add(this.tb_PointY);
            this.gbx_out.Controls.Add(this.lab_PointY);
            this.gbx_out.Controls.Add(this.lab_Unit2);
            this.gbx_out.Controls.Add(this.lab_Unit1);
            this.gbx_out.Controls.Add(this.tb_Diameter);
            this.gbx_out.Controls.Add(this.lab_Diameter);
            this.gbx_out.Controls.Add(this.tb_PointX);
            this.gbx_out.Controls.Add(this.lab_PointX);
            this.gbx_out.Controls.Add(this.tb_ImageCenterY);
            this.gbx_out.Controls.Add(this.lab_ImageCenterY);
            this.gbx_out.Controls.Add(this.tb_ImageCenterX);
            this.gbx_out.Controls.Add(this.lab_ImageCenterX);
            this.gbx_out.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbx_out.Location = new System.Drawing.Point(2, 531);
            this.gbx_out.Name = "gbx_out";
            this.gbx_out.Size = new System.Drawing.Size(849, 64);
            this.gbx_out.TabIndex = 56;
            this.gbx_out.TabStop = false;
            this.gbx_out.Text = "Out Result";
            // 
            // lab_Unit5
            // 
            this.lab_Unit5.AutoSize = true;
            this.lab_Unit5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_Unit5.Location = new System.Drawing.Point(678, 43);
            this.lab_Unit5.Name = "lab_Unit5";
            this.lab_Unit5.Size = new System.Drawing.Size(23, 13);
            this.lab_Unit5.TabIndex = 56;
            this.lab_Unit5.Text = "mm";
            // 
            // lab_Unit4
            // 
            this.lab_Unit4.AutoSize = true;
            this.lab_Unit4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_Unit4.Location = new System.Drawing.Point(474, 43);
            this.lab_Unit4.Name = "lab_Unit4";
            this.lab_Unit4.Size = new System.Drawing.Size(28, 13);
            this.lab_Unit4.TabIndex = 55;
            this.lab_Unit4.Text = "pixel";
            // 
            // lab_Unit3
            // 
            this.lab_Unit3.AutoSize = true;
            this.lab_Unit3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_Unit3.Location = new System.Drawing.Point(474, 17);
            this.lab_Unit3.Name = "lab_Unit3";
            this.lab_Unit3.Size = new System.Drawing.Size(28, 13);
            this.lab_Unit3.TabIndex = 54;
            this.lab_Unit3.Text = "pixel";
            // 
            // tb_PointY
            // 
            this.tb_PointY.Enabled = false;
            this.tb_PointY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_PointY.Location = new System.Drawing.Point(411, 40);
            this.tb_PointY.Name = "tb_PointY";
            this.tb_PointY.Size = new System.Drawing.Size(60, 20);
            this.tb_PointY.TabIndex = 53;
            // 
            // lab_PointY
            // 
            this.lab_PointY.AutoSize = true;
            this.lab_PointY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_PointY.Location = new System.Drawing.Point(367, 43);
            this.lab_PointY.Name = "lab_PointY";
            this.lab_PointY.Size = new System.Drawing.Size(41, 13);
            this.lab_PointY.TabIndex = 52;
            this.lab_PointY.Text = "Point Y";
            // 
            // lab_Unit2
            // 
            this.lab_Unit2.AutoSize = true;
            this.lab_Unit2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_Unit2.Location = new System.Drawing.Point(278, 43);
            this.lab_Unit2.Name = "lab_Unit2";
            this.lab_Unit2.Size = new System.Drawing.Size(28, 13);
            this.lab_Unit2.TabIndex = 51;
            this.lab_Unit2.Text = "pixel";
            // 
            // lab_Unit1
            // 
            this.lab_Unit1.AutoSize = true;
            this.lab_Unit1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_Unit1.Location = new System.Drawing.Point(278, 17);
            this.lab_Unit1.Name = "lab_Unit1";
            this.lab_Unit1.Size = new System.Drawing.Size(28, 13);
            this.lab_Unit1.TabIndex = 50;
            this.lab_Unit1.Text = "pixel";
            // 
            // tb_Diameter
            // 
            this.tb_Diameter.Enabled = false;
            this.tb_Diameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Diameter.Location = new System.Drawing.Point(615, 40);
            this.tb_Diameter.Name = "tb_Diameter";
            this.tb_Diameter.Size = new System.Drawing.Size(60, 20);
            this.tb_Diameter.TabIndex = 49;
            // 
            // lab_Diameter
            // 
            this.lab_Diameter.AutoSize = true;
            this.lab_Diameter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_Diameter.Location = new System.Drawing.Point(563, 43);
            this.lab_Diameter.Name = "lab_Diameter";
            this.lab_Diameter.Size = new System.Drawing.Size(49, 13);
            this.lab_Diameter.TabIndex = 48;
            this.lab_Diameter.Text = "Diameter";
            // 
            // tb_PointX
            // 
            this.tb_PointX.Enabled = false;
            this.tb_PointX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_PointX.Location = new System.Drawing.Point(411, 14);
            this.tb_PointX.Name = "tb_PointX";
            this.tb_PointX.Size = new System.Drawing.Size(60, 20);
            this.tb_PointX.TabIndex = 47;
            // 
            // lab_PointX
            // 
            this.lab_PointX.AutoSize = true;
            this.lab_PointX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_PointX.Location = new System.Drawing.Point(367, 17);
            this.lab_PointX.Name = "lab_PointX";
            this.lab_PointX.Size = new System.Drawing.Size(41, 13);
            this.lab_PointX.TabIndex = 46;
            this.lab_PointX.Text = "Point X";
            // 
            // tb_ImageCenterY
            // 
            this.tb_ImageCenterY.Enabled = false;
            this.tb_ImageCenterY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ImageCenterY.Location = new System.Drawing.Point(215, 40);
            this.tb_ImageCenterY.Name = "tb_ImageCenterY";
            this.tb_ImageCenterY.Size = new System.Drawing.Size(60, 20);
            this.tb_ImageCenterY.TabIndex = 45;
            // 
            // lab_ImageCenterY
            // 
            this.lab_ImageCenterY.AutoSize = true;
            this.lab_ImageCenterY.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_ImageCenterY.Location = new System.Drawing.Point(132, 43);
            this.lab_ImageCenterY.Name = "lab_ImageCenterY";
            this.lab_ImageCenterY.Size = new System.Drawing.Size(80, 13);
            this.lab_ImageCenterY.TabIndex = 44;
            this.lab_ImageCenterY.Text = "Image Center Y";
            // 
            // tb_ImageCenterX
            // 
            this.tb_ImageCenterX.Enabled = false;
            this.tb_ImageCenterX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ImageCenterX.Location = new System.Drawing.Point(215, 14);
            this.tb_ImageCenterX.Name = "tb_ImageCenterX";
            this.tb_ImageCenterX.Size = new System.Drawing.Size(60, 20);
            this.tb_ImageCenterX.TabIndex = 43;
            // 
            // lab_ImageCenterX
            // 
            this.lab_ImageCenterX.AutoSize = true;
            this.lab_ImageCenterX.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_ImageCenterX.Location = new System.Drawing.Point(132, 17);
            this.lab_ImageCenterX.Name = "lab_ImageCenterX";
            this.lab_ImageCenterX.Size = new System.Drawing.Size(80, 13);
            this.lab_ImageCenterX.TabIndex = 42;
            this.lab_ImageCenterX.Text = "Image Center X";
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadImage.Location = new System.Drawing.Point(371, 19);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(115, 40);
            this.btnLoadImage.TabIndex = 57;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btn_LoadImage_Click);
            // 
            // VisionProcessEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 596);
            this.ControlBox = false;
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.gbx_out);
            this.Controls.Add(this.lab_VisionProcessType);
            this.Controls.Add(this.cmb_VisionProcessType);
            this.Controls.Add(this.tb_ProductName);
            this.Controls.Add(this.lab_ProductName);
            this.Controls.Add(this.btn_Run);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.cogToolBlockEditV21);
            this.Name = "VisionProcessEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VisionProcessEditForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VisionProcessEditForm_FormClosing);
            this.Load += new System.EventHandler(this.VisionProcessEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cogToolBlockEditV21)).EndInit();
            this.gbx_out.ResumeLayout(false);
            this.gbx_out.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Cognex.VisionPro.ToolBlock.CogToolBlockEditV2 cogToolBlockEditV21;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.TextBox tb_ProductName;
        private System.Windows.Forms.Label lab_ProductName;
        private System.Windows.Forms.ComboBox cmb_VisionProcessType;
        private System.Windows.Forms.Label lab_VisionProcessType;
        private System.Windows.Forms.GroupBox gbx_out;
        private System.Windows.Forms.TextBox tb_ImageCenterY;
        private System.Windows.Forms.Label lab_ImageCenterY;
        private System.Windows.Forms.TextBox tb_ImageCenterX;
        private System.Windows.Forms.Label lab_ImageCenterX;
        private System.Windows.Forms.TextBox tb_Diameter;
        private System.Windows.Forms.Label lab_Diameter;
        private System.Windows.Forms.TextBox tb_PointX;
        private System.Windows.Forms.Label lab_PointX;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Label lab_Unit2;
        private System.Windows.Forms.Label lab_Unit1;
        private System.Windows.Forms.Label lab_Unit5;
        private System.Windows.Forms.Label lab_Unit4;
        private System.Windows.Forms.Label lab_Unit3;
        private System.Windows.Forms.TextBox tb_PointY;
        private System.Windows.Forms.Label lab_PointY;
    }
}