namespace FocusingLensAligner
{
    partial class IOConfigForm
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
            this.pnl_IOParameterSet = new System.Windows.Forms.Panel();
            this.lab_PortNum = new System.Windows.Forms.Label();
            this.lab_Channel = new System.Windows.Forms.Label();
            this.lab_Describe = new System.Windows.Forms.Label();
            this.cmb_Channel = new System.Windows.Forms.ComboBox();
            this.lab_InOut = new System.Windows.Forms.Label();
            this.lab_CardID = new System.Windows.Forms.Label();
            this.lab_IOName = new System.Windows.Forms.Label();
            this.cmb_InOut = new System.Windows.Forms.ComboBox();
            this.cmb_CardID = new System.Windows.Forms.ComboBox();
            this.cmb_PortNum = new System.Windows.Forms.ComboBox();
            this.txb_Describe = new System.Windows.Forms.TextBox();
            this.txb_IOName = new System.Windows.Forms.TextBox();
            this.pnl_IOConfigOperation = new System.Windows.Forms.Panel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Modify = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.dgv_IOConfigDisplay = new System.Windows.Forms.DataGridView();
            this.pnl_IOParameterSet.SuspendLayout();
            this.pnl_IOConfigOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IOConfigDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_IOParameterSet
            // 
            this.pnl_IOParameterSet.Controls.Add(this.lab_PortNum);
            this.pnl_IOParameterSet.Controls.Add(this.lab_Channel);
            this.pnl_IOParameterSet.Controls.Add(this.lab_Describe);
            this.pnl_IOParameterSet.Controls.Add(this.cmb_Channel);
            this.pnl_IOParameterSet.Controls.Add(this.lab_InOut);
            this.pnl_IOParameterSet.Controls.Add(this.lab_CardID);
            this.pnl_IOParameterSet.Controls.Add(this.lab_IOName);
            this.pnl_IOParameterSet.Controls.Add(this.cmb_InOut);
            this.pnl_IOParameterSet.Controls.Add(this.cmb_CardID);
            this.pnl_IOParameterSet.Controls.Add(this.cmb_PortNum);
            this.pnl_IOParameterSet.Controls.Add(this.txb_Describe);
            this.pnl_IOParameterSet.Controls.Add(this.txb_IOName);
            this.pnl_IOParameterSet.Location = new System.Drawing.Point(5, 4);
            this.pnl_IOParameterSet.Name = "pnl_IOParameterSet";
            this.pnl_IOParameterSet.Size = new System.Drawing.Size(572, 88);
            this.pnl_IOParameterSet.TabIndex = 0;
            // 
            // lab_PortNum
            // 
            this.lab_PortNum.AutoSize = true;
            this.lab_PortNum.Location = new System.Drawing.Point(199, 57);
            this.lab_PortNum.Name = "lab_PortNum";
            this.lab_PortNum.Size = new System.Drawing.Size(51, 13);
            this.lab_PortNum.TabIndex = 11;
            this.lab_PortNum.Text = "Port Num";
            // 
            // lab_Channel
            // 
            this.lab_Channel.AutoSize = true;
            this.lab_Channel.Location = new System.Drawing.Point(390, 57);
            this.lab_Channel.Name = "lab_Channel";
            this.lab_Channel.Size = new System.Drawing.Size(46, 13);
            this.lab_Channel.TabIndex = 10;
            this.lab_Channel.Text = "Channel";
            // 
            // lab_Describe
            // 
            this.lab_Describe.AutoSize = true;
            this.lab_Describe.Location = new System.Drawing.Point(3, 57);
            this.lab_Describe.Name = "lab_Describe";
            this.lab_Describe.Size = new System.Drawing.Size(60, 13);
            this.lab_Describe.TabIndex = 9;
            this.lab_Describe.Text = "Description";
            // 
            // cmb_Channel
            // 
            this.cmb_Channel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Channel.FormattingEnabled = true;
            this.cmb_Channel.Location = new System.Drawing.Point(443, 53);
            this.cmb_Channel.Name = "cmb_Channel";
            this.cmb_Channel.Size = new System.Drawing.Size(122, 21);
            this.cmb_Channel.TabIndex = 3;
            // 
            // lab_InOut
            // 
            this.lab_InOut.AutoSize = true;
            this.lab_InOut.Location = new System.Drawing.Point(390, 20);
            this.lab_InOut.Name = "lab_InOut";
            this.lab_InOut.Size = new System.Drawing.Size(38, 13);
            this.lab_InOut.TabIndex = 8;
            this.lab_InOut.Text = "In/Out";
            // 
            // lab_CardID
            // 
            this.lab_CardID.AutoSize = true;
            this.lab_CardID.Location = new System.Drawing.Point(199, 20);
            this.lab_CardID.Name = "lab_CardID";
            this.lab_CardID.Size = new System.Drawing.Size(43, 13);
            this.lab_CardID.TabIndex = 7;
            this.lab_CardID.Text = "Card ID";
            // 
            // lab_IOName
            // 
            this.lab_IOName.AutoSize = true;
            this.lab_IOName.Location = new System.Drawing.Point(3, 20);
            this.lab_IOName.Name = "lab_IOName";
            this.lab_IOName.Size = new System.Drawing.Size(49, 13);
            this.lab_IOName.TabIndex = 6;
            this.lab_IOName.Text = "IO Name";
            // 
            // cmb_InOut
            // 
            this.cmb_InOut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_InOut.FormattingEnabled = true;
            this.cmb_InOut.Location = new System.Drawing.Point(443, 16);
            this.cmb_InOut.Name = "cmb_InOut";
            this.cmb_InOut.Size = new System.Drawing.Size(122, 21);
            this.cmb_InOut.TabIndex = 5;
            // 
            // cmb_CardID
            // 
            this.cmb_CardID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CardID.FormattingEnabled = true;
            this.cmb_CardID.Location = new System.Drawing.Point(256, 16);
            this.cmb_CardID.Name = "cmb_CardID";
            this.cmb_CardID.Size = new System.Drawing.Size(122, 21);
            this.cmb_CardID.TabIndex = 4;
            // 
            // cmb_PortNum
            // 
            this.cmb_PortNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_PortNum.FormattingEnabled = true;
            this.cmb_PortNum.Location = new System.Drawing.Point(255, 53);
            this.cmb_PortNum.Name = "cmb_PortNum";
            this.cmb_PortNum.Size = new System.Drawing.Size(122, 21);
            this.cmb_PortNum.TabIndex = 2;
            // 
            // txb_Describe
            // 
            this.txb_Describe.Location = new System.Drawing.Point(69, 53);
            this.txb_Describe.Name = "txb_Describe";
            this.txb_Describe.Size = new System.Drawing.Size(122, 20);
            this.txb_Describe.TabIndex = 1;
            // 
            // txb_IOName
            // 
            this.txb_IOName.Location = new System.Drawing.Point(69, 16);
            this.txb_IOName.Name = "txb_IOName";
            this.txb_IOName.Size = new System.Drawing.Size(122, 20);
            this.txb_IOName.TabIndex = 0;
            // 
            // pnl_IOConfigOperation
            // 
            this.pnl_IOConfigOperation.Controls.Add(this.btn_Save);
            this.pnl_IOConfigOperation.Controls.Add(this.btn_Modify);
            this.pnl_IOConfigOperation.Controls.Add(this.btn_Delete);
            this.pnl_IOConfigOperation.Controls.Add(this.btn_Add);
            this.pnl_IOConfigOperation.Location = new System.Drawing.Point(5, 98);
            this.pnl_IOConfigOperation.Name = "pnl_IOConfigOperation";
            this.pnl_IOConfigOperation.Size = new System.Drawing.Size(572, 56);
            this.pnl_IOConfigOperation.TabIndex = 7;
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(457, 4);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(106, 46);
            this.btn_Save.TabIndex = 3;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Modify
            // 
            this.btn_Modify.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Modify.Location = new System.Drawing.Point(307, 4);
            this.btn_Modify.Name = "btn_Modify";
            this.btn_Modify.Size = new System.Drawing.Size(106, 46);
            this.btn_Modify.TabIndex = 2;
            this.btn_Modify.Text = "Modify";
            this.btn_Modify.UseVisualStyleBackColor = true;
            this.btn_Modify.Click += new System.EventHandler(this.btn_Modify_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(157, 4);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(106, 46);
            this.btn_Delete.TabIndex = 1;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Location = new System.Drawing.Point(7, 4);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(106, 46);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // dgv_IOConfigDisplay
            // 
            this.dgv_IOConfigDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_IOConfigDisplay.Location = new System.Drawing.Point(5, 160);
            this.dgv_IOConfigDisplay.Name = "dgv_IOConfigDisplay";
            this.dgv_IOConfigDisplay.Size = new System.Drawing.Size(572, 224);
            this.dgv_IOConfigDisplay.TabIndex = 8;
            this.dgv_IOConfigDisplay.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_IOConfigDisplay_CellClick);
            // 
            // IOConfigFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 388);
            this.Controls.Add(this.dgv_IOConfigDisplay);
            this.Controls.Add(this.pnl_IOConfigOperation);
            this.Controls.Add(this.pnl_IOParameterSet);
            this.Name = "IOConfigFrm";
            this.Text = "IOConfigFrm";
            this.Load += new System.EventHandler(this.IOConfigFrm_Load);
            this.pnl_IOParameterSet.ResumeLayout(false);
            this.pnl_IOParameterSet.PerformLayout();
            this.pnl_IOConfigOperation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_IOConfigDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_IOParameterSet;
        private System.Windows.Forms.ComboBox cmb_InOut;
        private System.Windows.Forms.ComboBox cmb_CardID;
        private System.Windows.Forms.ComboBox cmb_Channel;
        private System.Windows.Forms.ComboBox cmb_PortNum;
        private System.Windows.Forms.TextBox txb_Describe;
        private System.Windows.Forms.TextBox txb_IOName;
        private System.Windows.Forms.Label lab_PortNum;
        private System.Windows.Forms.Label lab_Channel;
        private System.Windows.Forms.Label lab_Describe;
        private System.Windows.Forms.Label lab_InOut;
        private System.Windows.Forms.Label lab_CardID;
        private System.Windows.Forms.Label lab_IOName;
        private System.Windows.Forms.Panel pnl_IOConfigOperation;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Modify;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.DataGridView dgv_IOConfigDisplay;
    }
}