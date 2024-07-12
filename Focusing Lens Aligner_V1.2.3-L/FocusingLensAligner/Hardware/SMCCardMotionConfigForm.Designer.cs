namespace FocusingLensAligner
{
    partial class SMCCardMotionConfigForm
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
            this.pnl_SMCCardMotionParameterSet = new System.Windows.Forms.Panel();
            this.lab_PositionDescription = new System.Windows.Forms.Label();
            this.txb_Description = new System.Windows.Forms.TextBox();
            this.lab_SerialPort = new System.Windows.Forms.Label();
            this.cmb_SerialPort = new System.Windows.Forms.ComboBox();
            this.lab_AxisName = new System.Windows.Forms.Label();
            this.txb_AxisName = new System.Windows.Forms.TextBox();
            this.dgv_SMCCardMotionConfigDisplay = new System.Windows.Forms.DataGridView();
            this.pnl_SMCCardMotionConfigOperation = new System.Windows.Forms.Panel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Modify = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.pnl_SMCCardMotionParameterSet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SMCCardMotionConfigDisplay)).BeginInit();
            this.pnl_SMCCardMotionConfigOperation.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_SMCCardMotionParameterSet
            // 
            this.pnl_SMCCardMotionParameterSet.Controls.Add(this.lab_PositionDescription);
            this.pnl_SMCCardMotionParameterSet.Controls.Add(this.txb_Description);
            this.pnl_SMCCardMotionParameterSet.Controls.Add(this.lab_SerialPort);
            this.pnl_SMCCardMotionParameterSet.Controls.Add(this.cmb_SerialPort);
            this.pnl_SMCCardMotionParameterSet.Controls.Add(this.lab_AxisName);
            this.pnl_SMCCardMotionParameterSet.Controls.Add(this.txb_AxisName);
            this.pnl_SMCCardMotionParameterSet.Location = new System.Drawing.Point(10, 11);
            this.pnl_SMCCardMotionParameterSet.Name = "pnl_SMCCardMotionParameterSet";
            this.pnl_SMCCardMotionParameterSet.Size = new System.Drawing.Size(534, 102);
            this.pnl_SMCCardMotionParameterSet.TabIndex = 20;
            // 
            // lab_PositionDescription
            // 
            this.lab_PositionDescription.AutoSize = true;
            this.lab_PositionDescription.Location = new System.Drawing.Point(25, 61);
            this.lab_PositionDescription.Name = "lab_PositionDescription";
            this.lab_PositionDescription.Size = new System.Drawing.Size(60, 13);
            this.lab_PositionDescription.TabIndex = 39;
            this.lab_PositionDescription.Text = "Description";
            // 
            // txb_Description
            // 
            this.txb_Description.Location = new System.Drawing.Point(91, 57);
            this.txb_Description.Name = "txb_Description";
            this.txb_Description.Size = new System.Drawing.Size(287, 20);
            this.txb_Description.TabIndex = 34;
            // 
            // lab_SerialPort
            // 
            this.lab_SerialPort.AutoSize = true;
            this.lab_SerialPort.Location = new System.Drawing.Point(235, 20);
            this.lab_SerialPort.Name = "lab_SerialPort";
            this.lab_SerialPort.Size = new System.Drawing.Size(55, 13);
            this.lab_SerialPort.TabIndex = 26;
            this.lab_SerialPort.Text = "Serial Port";
            // 
            // cmb_SerialPort
            // 
            this.cmb_SerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SerialPort.FormattingEnabled = true;
            this.cmb_SerialPort.Location = new System.Drawing.Point(296, 16);
            this.cmb_SerialPort.Name = "cmb_SerialPort";
            this.cmb_SerialPort.Size = new System.Drawing.Size(60, 21);
            this.cmb_SerialPort.TabIndex = 22;
            // 
            // lab_AxisName
            // 
            this.lab_AxisName.AutoSize = true;
            this.lab_AxisName.Location = new System.Drawing.Point(28, 21);
            this.lab_AxisName.Name = "lab_AxisName";
            this.lab_AxisName.Size = new System.Drawing.Size(57, 13);
            this.lab_AxisName.TabIndex = 20;
            this.lab_AxisName.Text = "Axis Name";
            // 
            // txb_AxisName
            // 
            this.txb_AxisName.Location = new System.Drawing.Point(91, 18);
            this.txb_AxisName.Name = "txb_AxisName";
            this.txb_AxisName.Size = new System.Drawing.Size(122, 20);
            this.txb_AxisName.TabIndex = 19;
            // 
            // dgv_SMCCardMotionConfigDisplay
            // 
            this.dgv_SMCCardMotionConfigDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SMCCardMotionConfigDisplay.Location = new System.Drawing.Point(10, 192);
            this.dgv_SMCCardMotionConfigDisplay.Name = "dgv_SMCCardMotionConfigDisplay";
            this.dgv_SMCCardMotionConfigDisplay.Size = new System.Drawing.Size(534, 104);
            this.dgv_SMCCardMotionConfigDisplay.TabIndex = 45;
            this.dgv_SMCCardMotionConfigDisplay.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_SMCCardMotionConfigDisplay_CellClick);
            // 
            // pnl_SMCCardMotionConfigOperation
            // 
            this.pnl_SMCCardMotionConfigOperation.Controls.Add(this.btn_Save);
            this.pnl_SMCCardMotionConfigOperation.Controls.Add(this.btn_Modify);
            this.pnl_SMCCardMotionConfigOperation.Controls.Add(this.btn_Delete);
            this.pnl_SMCCardMotionConfigOperation.Controls.Add(this.btn_Add);
            this.pnl_SMCCardMotionConfigOperation.Location = new System.Drawing.Point(10, 119);
            this.pnl_SMCCardMotionConfigOperation.Name = "pnl_SMCCardMotionConfigOperation";
            this.pnl_SMCCardMotionConfigOperation.Size = new System.Drawing.Size(534, 67);
            this.pnl_SMCCardMotionConfigOperation.TabIndex = 40;
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(408, 15);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(104, 37);
            this.btn_Save.TabIndex = 48;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Modify
            // 
            this.btn_Modify.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Modify.Location = new System.Drawing.Point(279, 15);
            this.btn_Modify.Name = "btn_Modify";
            this.btn_Modify.Size = new System.Drawing.Size(105, 38);
            this.btn_Modify.TabIndex = 47;
            this.btn_Modify.Text = "Modify";
            this.btn_Modify.UseVisualStyleBackColor = true;
            this.btn_Modify.Click += new System.EventHandler(this.btn_Modify_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Location = new System.Drawing.Point(151, 15);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(105, 38);
            this.btn_Delete.TabIndex = 46;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Location = new System.Drawing.Point(21, 15);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(105, 38);
            this.btn_Add.TabIndex = 45;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // SMCCardMotionConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 310);
            this.Controls.Add(this.pnl_SMCCardMotionConfigOperation);
            this.Controls.Add(this.dgv_SMCCardMotionConfigDisplay);
            this.Controls.Add(this.pnl_SMCCardMotionParameterSet);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SMCCardMotionConfigForm";
            this.Text = "SMCCardMotionConfigForm";
            this.Load += new System.EventHandler(this.SMCCardMotionConfigForm_Load);
            this.pnl_SMCCardMotionParameterSet.ResumeLayout(false);
            this.pnl_SMCCardMotionParameterSet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SMCCardMotionConfigDisplay)).EndInit();
            this.pnl_SMCCardMotionConfigOperation.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnl_SMCCardMotionParameterSet;
        private System.Windows.Forms.Label lab_SerialPort;
        private System.Windows.Forms.ComboBox cmb_SerialPort;
        private System.Windows.Forms.Label lab_AxisName;
        private System.Windows.Forms.TextBox txb_AxisName;
        private System.Windows.Forms.Label lab_PositionDescription;
        private System.Windows.Forms.TextBox txb_Description;
        private System.Windows.Forms.DataGridView dgv_SMCCardMotionConfigDisplay;
        private System.Windows.Forms.Panel pnl_SMCCardMotionConfigOperation;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Modify;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Add;
    }
}