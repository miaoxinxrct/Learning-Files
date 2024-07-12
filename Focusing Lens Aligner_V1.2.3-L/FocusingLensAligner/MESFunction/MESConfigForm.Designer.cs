namespace FocusingLensAligner
{
    partial class MESConfigForm
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
            this.gb_ValidationInfo = new System.Windows.Forms.GroupBox();
            this.lb_StepName = new System.Windows.Forms.Label();
            this.lb_ProdName = new System.Windows.Forms.Label();
            this.dgv_StepVal = new System.Windows.Forms.DataGridView();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.chkbox_MultiMoveIn = new System.Windows.Forms.CheckBox();
            this.chkbox_MoveIn = new System.Windows.Forms.CheckBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.tb_MultiStepName = new System.Windows.Forms.TextBox();
            this.tb_ProductName = new System.Windows.Forms.TextBox();
            this.tb_StepName = new System.Windows.Forms.TextBox();
            this.rbtn_MultiValid = new System.Windows.Forms.RadioButton();
            this.rbtn_SingleValid = new System.Windows.Forms.RadioButton();
            this.gb_EnvironmentInfo = new System.Windows.Forms.GroupBox();
            this.lb_Password = new System.Windows.Forms.Label();
            this.lb_UserName = new System.Windows.Forms.Label();
            this.lb_DataName = new System.Windows.Forms.Label();
            this.lb_ServerName = new System.Windows.Forms.Label();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.tb_UserName = new System.Windows.Forms.TextBox();
            this.tb_DatabaseName = new System.Windows.Forms.TextBox();
            this.tb_ServerName = new System.Windows.Forms.TextBox();
            this.rbtn_ProductEnvr = new System.Windows.Forms.RadioButton();
            this.rbtn_TestEnvr = new System.Windows.Forms.RadioButton();
            this.rbtn_DefineEnvr = new System.Windows.Forms.RadioButton();
            this.gb_DownloadInfo = new System.Windows.Forms.GroupBox();
            this.lb_ServerPath = new System.Windows.Forms.Label();
            this.chkbox_AutoLoadConfig = new System.Windows.Forms.CheckBox();
            this.tb_ServerPath = new System.Windows.Forms.TextBox();
            this.tb_InitializeInfo = new System.Windows.Forms.GroupBox();
            this.chkbox_InitialMES = new System.Windows.Forms.CheckBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.gb_ValidationInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_StepVal)).BeginInit();
            this.gb_EnvironmentInfo.SuspendLayout();
            this.gb_DownloadInfo.SuspendLayout();
            this.tb_InitializeInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_ValidationInfo
            // 
            this.gb_ValidationInfo.Controls.Add(this.lb_StepName);
            this.gb_ValidationInfo.Controls.Add(this.lb_ProdName);
            this.gb_ValidationInfo.Controls.Add(this.dgv_StepVal);
            this.gb_ValidationInfo.Controls.Add(this.btn_Delete);
            this.gb_ValidationInfo.Controls.Add(this.chkbox_MultiMoveIn);
            this.gb_ValidationInfo.Controls.Add(this.chkbox_MoveIn);
            this.gb_ValidationInfo.Controls.Add(this.btn_Add);
            this.gb_ValidationInfo.Controls.Add(this.tb_MultiStepName);
            this.gb_ValidationInfo.Controls.Add(this.tb_ProductName);
            this.gb_ValidationInfo.Controls.Add(this.tb_StepName);
            this.gb_ValidationInfo.Controls.Add(this.rbtn_MultiValid);
            this.gb_ValidationInfo.Controls.Add(this.rbtn_SingleValid);
            this.gb_ValidationInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_ValidationInfo.Location = new System.Drawing.Point(12, 12);
            this.gb_ValidationInfo.Name = "gb_ValidationInfo";
            this.gb_ValidationInfo.Size = new System.Drawing.Size(474, 261);
            this.gb_ValidationInfo.TabIndex = 2;
            this.gb_ValidationInfo.TabStop = false;
            this.gb_ValidationInfo.Text = "Validation Info";
            // 
            // lb_StepName
            // 
            this.lb_StepName.AutoSize = true;
            this.lb_StepName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_StepName.Location = new System.Drawing.Point(52, 88);
            this.lb_StepName.Name = "lb_StepName";
            this.lb_StepName.Size = new System.Drawing.Size(60, 13);
            this.lb_StepName.TabIndex = 13;
            this.lb_StepName.Text = "Step Name";
            // 
            // lb_ProdName
            // 
            this.lb_ProdName.AutoSize = true;
            this.lb_ProdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ProdName.Location = new System.Drawing.Point(52, 62);
            this.lb_ProdName.Name = "lb_ProdName";
            this.lb_ProdName.Size = new System.Drawing.Size(75, 13);
            this.lb_ProdName.TabIndex = 12;
            this.lb_ProdName.Text = "Product Name";
            // 
            // dgv_StepVal
            // 
            this.dgv_StepVal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_StepVal.Location = new System.Drawing.Point(17, 138);
            this.dgv_StepVal.Name = "dgv_StepVal";
            this.dgv_StepVal.Size = new System.Drawing.Size(371, 117);
            this.dgv_StepVal.TabIndex = 11;
            // 
            // btn_Delete
            // 
            this.btn_Delete.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_Delete.BackgroundImage = global::FocusingLensAligner.Properties.Resources.Delete;
            this.btn_Delete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Delete.Location = new System.Drawing.Point(394, 203);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(74, 52);
            this.btn_Delete.TabIndex = 10;
            this.btn_Delete.UseVisualStyleBackColor = false;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // chkbox_MultiMoveIn
            // 
            this.chkbox_MultiMoveIn.AutoSize = true;
            this.chkbox_MultiMoveIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_MultiMoveIn.Location = new System.Drawing.Point(52, 115);
            this.chkbox_MultiMoveIn.Name = "chkbox_MultiMoveIn";
            this.chkbox_MultiMoveIn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkbox_MultiMoveIn.Size = new System.Drawing.Size(150, 17);
            this.chkbox_MultiMoveIn.TabIndex = 9;
            this.chkbox_MultiMoveIn.Text = "                 Check Move In";
            this.chkbox_MultiMoveIn.UseVisualStyleBackColor = true;
            // 
            // chkbox_MoveIn
            // 
            this.chkbox_MoveIn.AutoSize = true;
            this.chkbox_MoveIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_MoveIn.Location = new System.Drawing.Point(356, 20);
            this.chkbox_MoveIn.Name = "chkbox_MoveIn";
            this.chkbox_MoveIn.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkbox_MoveIn.Size = new System.Drawing.Size(99, 17);
            this.chkbox_MoveIn.TabIndex = 8;
            this.chkbox_MoveIn.Text = "Check Move In";
            this.chkbox_MoveIn.UseVisualStyleBackColor = true;
            // 
            // btn_Add
            // 
            this.btn_Add.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_Add.BackgroundImage = global::FocusingLensAligner.Properties.Resources.Add;
            this.btn_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Add.Location = new System.Drawing.Point(394, 72);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(74, 52);
            this.btn_Add.TabIndex = 7;
            this.btn_Add.UseVisualStyleBackColor = false;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // tb_MultiStepName
            // 
            this.tb_MultiStepName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_MultiStepName.Location = new System.Drawing.Point(140, 89);
            this.tb_MultiStepName.Name = "tb_MultiStepName";
            this.tb_MultiStepName.Size = new System.Drawing.Size(210, 20);
            this.tb_MultiStepName.TabIndex = 4;
            // 
            // tb_ProductName
            // 
            this.tb_ProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ProductName.Location = new System.Drawing.Point(140, 59);
            this.tb_ProductName.Name = "tb_ProductName";
            this.tb_ProductName.Size = new System.Drawing.Size(210, 20);
            this.tb_ProductName.TabIndex = 3;
            // 
            // tb_StepName
            // 
            this.tb_StepName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_StepName.Location = new System.Drawing.Point(140, 16);
            this.tb_StepName.Name = "tb_StepName";
            this.tb_StepName.Size = new System.Drawing.Size(210, 20);
            this.tb_StepName.TabIndex = 2;
            // 
            // rbtn_MultiValid
            // 
            this.rbtn_MultiValid.AutoSize = true;
            this.rbtn_MultiValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_MultiValid.Location = new System.Drawing.Point(6, 42);
            this.rbtn_MultiValid.Name = "rbtn_MultiValid";
            this.rbtn_MultiValid.Size = new System.Drawing.Size(121, 17);
            this.rbtn_MultiValid.TabIndex = 1;
            this.rbtn_MultiValid.TabStop = true;
            this.rbtn_MultiValid.Text = "Multi Step Validation";
            this.rbtn_MultiValid.UseVisualStyleBackColor = true;
            this.rbtn_MultiValid.Click += new System.EventHandler(this.rbtn_MultiValid_Click);
            // 
            // rbtn_SingleValid
            // 
            this.rbtn_SingleValid.AutoSize = true;
            this.rbtn_SingleValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_SingleValid.Location = new System.Drawing.Point(6, 19);
            this.rbtn_SingleValid.Name = "rbtn_SingleValid";
            this.rbtn_SingleValid.Size = new System.Drawing.Size(128, 17);
            this.rbtn_SingleValid.TabIndex = 0;
            this.rbtn_SingleValid.TabStop = true;
            this.rbtn_SingleValid.Text = "Single Step Validation";
            this.rbtn_SingleValid.UseVisualStyleBackColor = true;
            this.rbtn_SingleValid.Click += new System.EventHandler(this.rbtn_SingleValid_Click);
            // 
            // gb_EnvironmentInfo
            // 
            this.gb_EnvironmentInfo.Controls.Add(this.lb_Password);
            this.gb_EnvironmentInfo.Controls.Add(this.lb_UserName);
            this.gb_EnvironmentInfo.Controls.Add(this.lb_DataName);
            this.gb_EnvironmentInfo.Controls.Add(this.lb_ServerName);
            this.gb_EnvironmentInfo.Controls.Add(this.tb_Password);
            this.gb_EnvironmentInfo.Controls.Add(this.tb_UserName);
            this.gb_EnvironmentInfo.Controls.Add(this.tb_DatabaseName);
            this.gb_EnvironmentInfo.Controls.Add(this.tb_ServerName);
            this.gb_EnvironmentInfo.Controls.Add(this.rbtn_ProductEnvr);
            this.gb_EnvironmentInfo.Controls.Add(this.rbtn_TestEnvr);
            this.gb_EnvironmentInfo.Controls.Add(this.rbtn_DefineEnvr);
            this.gb_EnvironmentInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_EnvironmentInfo.Location = new System.Drawing.Point(12, 279);
            this.gb_EnvironmentInfo.Name = "gb_EnvironmentInfo";
            this.gb_EnvironmentInfo.Size = new System.Drawing.Size(474, 126);
            this.gb_EnvironmentInfo.TabIndex = 3;
            this.gb_EnvironmentInfo.TabStop = false;
            this.gb_EnvironmentInfo.Text = "Environment Info";
            // 
            // lb_Password
            // 
            this.lb_Password.AutoSize = true;
            this.lb_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Password.Location = new System.Drawing.Point(232, 102);
            this.lb_Password.Name = "lb_Password";
            this.lb_Password.Size = new System.Drawing.Size(53, 13);
            this.lb_Password.TabIndex = 17;
            this.lb_Password.Text = "Password";
            // 
            // lb_UserName
            // 
            this.lb_UserName.AutoSize = true;
            this.lb_UserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_UserName.Location = new System.Drawing.Point(225, 74);
            this.lb_UserName.Name = "lb_UserName";
            this.lb_UserName.Size = new System.Drawing.Size(60, 13);
            this.lb_UserName.TabIndex = 16;
            this.lb_UserName.Text = "User Name";
            // 
            // lb_DataName
            // 
            this.lb_DataName.AutoSize = true;
            this.lb_DataName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_DataName.Location = new System.Drawing.Point(201, 48);
            this.lb_DataName.Name = "lb_DataName";
            this.lb_DataName.Size = new System.Drawing.Size(84, 13);
            this.lb_DataName.TabIndex = 15;
            this.lb_DataName.Text = "Database Name";
            // 
            // lb_ServerName
            // 
            this.lb_ServerName.AutoSize = true;
            this.lb_ServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ServerName.Location = new System.Drawing.Point(216, 20);
            this.lb_ServerName.Name = "lb_ServerName";
            this.lb_ServerName.Size = new System.Drawing.Size(69, 13);
            this.lb_ServerName.TabIndex = 14;
            this.lb_ServerName.Text = "Server Name";
            // 
            // tb_Password
            // 
            this.tb_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Password.Location = new System.Drawing.Point(291, 100);
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.Size = new System.Drawing.Size(177, 20);
            this.tb_Password.TabIndex = 8;
            // 
            // tb_UserName
            // 
            this.tb_UserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_UserName.Location = new System.Drawing.Point(291, 72);
            this.tb_UserName.Name = "tb_UserName";
            this.tb_UserName.Size = new System.Drawing.Size(177, 20);
            this.tb_UserName.TabIndex = 7;
            // 
            // tb_DatabaseName
            // 
            this.tb_DatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_DatabaseName.Location = new System.Drawing.Point(291, 46);
            this.tb_DatabaseName.Name = "tb_DatabaseName";
            this.tb_DatabaseName.Size = new System.Drawing.Size(177, 20);
            this.tb_DatabaseName.TabIndex = 6;
            // 
            // tb_ServerName
            // 
            this.tb_ServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ServerName.Location = new System.Drawing.Point(291, 16);
            this.tb_ServerName.Name = "tb_ServerName";
            this.tb_ServerName.Size = new System.Drawing.Size(177, 20);
            this.tb_ServerName.TabIndex = 5;
            // 
            // rbtn_ProductEnvr
            // 
            this.rbtn_ProductEnvr.AutoSize = true;
            this.rbtn_ProductEnvr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_ProductEnvr.Location = new System.Drawing.Point(6, 18);
            this.rbtn_ProductEnvr.Name = "rbtn_ProductEnvr";
            this.rbtn_ProductEnvr.Size = new System.Drawing.Size(175, 17);
            this.rbtn_ProductEnvr.TabIndex = 4;
            this.rbtn_ProductEnvr.TabStop = true;
            this.rbtn_ProductEnvr.Text = "Default Production Environment";
            this.rbtn_ProductEnvr.UseVisualStyleBackColor = true;
            this.rbtn_ProductEnvr.Click += new System.EventHandler(this.rbtn_ProductEnvr_Click);
            // 
            // rbtn_TestEnvr
            // 
            this.rbtn_TestEnvr.AutoSize = true;
            this.rbtn_TestEnvr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_TestEnvr.Location = new System.Drawing.Point(6, 48);
            this.rbtn_TestEnvr.Name = "rbtn_TestEnvr";
            this.rbtn_TestEnvr.Size = new System.Drawing.Size(145, 17);
            this.rbtn_TestEnvr.TabIndex = 3;
            this.rbtn_TestEnvr.TabStop = true;
            this.rbtn_TestEnvr.Text = "Default Test Environment";
            this.rbtn_TestEnvr.UseVisualStyleBackColor = true;
            this.rbtn_TestEnvr.Click += new System.EventHandler(this.rbtn_TestEnvr_Click);
            // 
            // rbtn_DefineEnvr
            // 
            this.rbtn_DefineEnvr.AutoSize = true;
            this.rbtn_DefineEnvr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtn_DefineEnvr.Location = new System.Drawing.Point(6, 74);
            this.rbtn_DefineEnvr.Name = "rbtn_DefineEnvr";
            this.rbtn_DefineEnvr.Size = new System.Drawing.Size(143, 17);
            this.rbtn_DefineEnvr.TabIndex = 2;
            this.rbtn_DefineEnvr.TabStop = true;
            this.rbtn_DefineEnvr.Text = "User Define Environment";
            this.rbtn_DefineEnvr.UseVisualStyleBackColor = true;
            this.rbtn_DefineEnvr.Click += new System.EventHandler(this.tb_DefineEnvr_Click);
            // 
            // gb_DownloadInfo
            // 
            this.gb_DownloadInfo.Controls.Add(this.lb_ServerPath);
            this.gb_DownloadInfo.Controls.Add(this.chkbox_AutoLoadConfig);
            this.gb_DownloadInfo.Controls.Add(this.tb_ServerPath);
            this.gb_DownloadInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_DownloadInfo.Location = new System.Drawing.Point(12, 411);
            this.gb_DownloadInfo.Name = "gb_DownloadInfo";
            this.gb_DownloadInfo.Size = new System.Drawing.Size(474, 71);
            this.gb_DownloadInfo.TabIndex = 4;
            this.gb_DownloadInfo.TabStop = false;
            this.gb_DownloadInfo.Text = "Download Info";
            // 
            // lb_ServerPath
            // 
            this.lb_ServerPath.AutoSize = true;
            this.lb_ServerPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_ServerPath.Location = new System.Drawing.Point(28, 22);
            this.lb_ServerPath.Name = "lb_ServerPath";
            this.lb_ServerPath.Size = new System.Drawing.Size(63, 13);
            this.lb_ServerPath.TabIndex = 10;
            this.lb_ServerPath.Text = "Server Path";
            // 
            // chkbox_AutoLoadConfig
            // 
            this.chkbox_AutoLoadConfig.AutoSize = true;
            this.chkbox_AutoLoadConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_AutoLoadConfig.Location = new System.Drawing.Point(97, 48);
            this.chkbox_AutoLoadConfig.Name = "chkbox_AutoLoadConfig";
            this.chkbox_AutoLoadConfig.Size = new System.Drawing.Size(183, 17);
            this.chkbox_AutoLoadConfig.TabIndex = 9;
            this.chkbox_AutoLoadConfig.Text = "Auto Download Latest Config File";
            this.chkbox_AutoLoadConfig.UseVisualStyleBackColor = true;
            // 
            // tb_ServerPath
            // 
            this.tb_ServerPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ServerPath.Location = new System.Drawing.Point(97, 19);
            this.tb_ServerPath.Name = "tb_ServerPath";
            this.tb_ServerPath.Size = new System.Drawing.Size(371, 20);
            this.tb_ServerPath.TabIndex = 6;
            // 
            // tb_InitializeInfo
            // 
            this.tb_InitializeInfo.Controls.Add(this.chkbox_InitialMES);
            this.tb_InitializeInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_InitializeInfo.Location = new System.Drawing.Point(12, 488);
            this.tb_InitializeInfo.Name = "tb_InitializeInfo";
            this.tb_InitializeInfo.Size = new System.Drawing.Size(474, 38);
            this.tb_InitializeInfo.TabIndex = 5;
            this.tb_InitializeInfo.TabStop = false;
            this.tb_InitializeInfo.Text = "InitializeInfo";
            // 
            // chkbox_InitialMES
            // 
            this.chkbox_InitialMES.AutoSize = true;
            this.chkbox_InitialMES.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_InitialMES.Location = new System.Drawing.Point(97, 15);
            this.chkbox_InitialMES.Name = "chkbox_InitialMES";
            this.chkbox_InitialMES.Size = new System.Drawing.Size(153, 17);
            this.chkbox_InitialMES.TabIndex = 9;
            this.chkbox_InitialMES.Text = "Initialize MES with Program";
            this.chkbox_InitialMES.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btn_Save.BackgroundImage = global::FocusingLensAligner.Properties.Resources.Save2;
            this.btn_Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Save.Location = new System.Drawing.Point(12, 532);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(74, 52);
            this.btn_Save.TabIndex = 1;
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btn_Exit.BackgroundImage = global::FocusingLensAligner.Properties.Resources.Exit;
            this.btn_Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_Exit.Location = new System.Drawing.Point(412, 532);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(74, 52);
            this.btn_Exit.TabIndex = 0;
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // MESConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(498, 587);
            this.Controls.Add(this.tb_InitializeInfo);
            this.Controls.Add(this.gb_DownloadInfo);
            this.Controls.Add(this.gb_EnvironmentInfo);
            this.Controls.Add(this.gb_ValidationInfo);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.btn_Exit);
            this.Name = "MESConfigForm";
            this.Text = "MESConfigForm";
            this.Load += new System.EventHandler(this.MESConfigForm_Load);
            this.gb_ValidationInfo.ResumeLayout(false);
            this.gb_ValidationInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_StepVal)).EndInit();
            this.gb_EnvironmentInfo.ResumeLayout(false);
            this.gb_EnvironmentInfo.PerformLayout();
            this.gb_DownloadInfo.ResumeLayout(false);
            this.gb_DownloadInfo.PerformLayout();
            this.tb_InitializeInfo.ResumeLayout(false);
            this.tb_InitializeInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.GroupBox gb_ValidationInfo;
        private System.Windows.Forms.GroupBox gb_EnvironmentInfo;
        private System.Windows.Forms.GroupBox gb_DownloadInfo;
        private System.Windows.Forms.GroupBox tb_InitializeInfo;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.CheckBox chkbox_MultiMoveIn;
        private System.Windows.Forms.CheckBox chkbox_MoveIn;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.TextBox tb_MultiStepName;
        private System.Windows.Forms.TextBox tb_ProductName;
        private System.Windows.Forms.TextBox tb_StepName;
        private System.Windows.Forms.RadioButton rbtn_MultiValid;
        private System.Windows.Forms.RadioButton rbtn_SingleValid;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.TextBox tb_UserName;
        private System.Windows.Forms.TextBox tb_DatabaseName;
        private System.Windows.Forms.TextBox tb_ServerName;
        private System.Windows.Forms.RadioButton rbtn_ProductEnvr;
        private System.Windows.Forms.RadioButton rbtn_TestEnvr;
        private System.Windows.Forms.RadioButton rbtn_DefineEnvr;
        private System.Windows.Forms.CheckBox chkbox_AutoLoadConfig;
        private System.Windows.Forms.TextBox tb_ServerPath;
        private System.Windows.Forms.CheckBox chkbox_InitialMES;
        private System.Windows.Forms.DataGridView dgv_StepVal;
        private System.Windows.Forms.Label lb_StepName;
        private System.Windows.Forms.Label lb_ProdName;
        private System.Windows.Forms.Label lb_Password;
        private System.Windows.Forms.Label lb_UserName;
        private System.Windows.Forms.Label lb_DataName;
        private System.Windows.Forms.Label lb_ServerName;
        private System.Windows.Forms.Label lb_ServerPath;
    }
}