namespace FocusingLensAligner
{
    partial class ForceSensorConfigForm
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
            this.pnl_ForceSensorConfigOperation = new System.Windows.Forms.Panel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Modify = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.dgv_ForceSensorConfigDisplay = new System.Windows.Forms.DataGridView();
            this.pnl_ForceSensorParameterSet = new System.Windows.Forms.Panel();
            this.cmb_Accuracy = new System.Windows.Forms.ComboBox();
            this.lab_ForceSensorDescription = new System.Windows.Forms.Label();
            this.lab_Accuracy = new System.Windows.Forms.Label();
            this.txb_ForceSensorDescription = new System.Windows.Forms.TextBox();
            this.lab_BaudRate = new System.Windows.Forms.Label();
            this.cmb_BaudRate = new System.Windows.Forms.ComboBox();
            this.lab_SerialPort = new System.Windows.Forms.Label();
            this.lab_ForceUnit = new System.Windows.Forms.Label();
            this.cmb_ForceUnit = new System.Windows.Forms.ComboBox();
            this.cmb_SerialPort = new System.Windows.Forms.ComboBox();
            this.lab_ForceSensorName = new System.Windows.Forms.Label();
            this.txb_ForceSensorName = new System.Windows.Forms.TextBox();
            this.pnl_ForceSensorConfigOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ForceSensorConfigDisplay)).BeginInit();
            this.pnl_ForceSensorParameterSet.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_ForceSensorConfigOperation
            // 
            this.pnl_ForceSensorConfigOperation.Controls.Add(this.btn_Save);
            this.pnl_ForceSensorConfigOperation.Controls.Add(this.btn_Modify);
            this.pnl_ForceSensorConfigOperation.Controls.Add(this.btn_Delete);
            this.pnl_ForceSensorConfigOperation.Controls.Add(this.btn_Add);
            this.pnl_ForceSensorConfigOperation.Location = new System.Drawing.Point(9, 120);
            this.pnl_ForceSensorConfigOperation.Name = "pnl_ForceSensorConfigOperation";
            this.pnl_ForceSensorConfigOperation.Size = new System.Drawing.Size(652, 67);
            this.pnl_ForceSensorConfigOperation.TabIndex = 47;
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(468, 16);
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
            this.btn_Modify.Location = new System.Drawing.Point(339, 16);
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
            this.btn_Delete.Location = new System.Drawing.Point(212, 16);
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
            this.btn_Add.Location = new System.Drawing.Point(81, 16);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(105, 38);
            this.btn_Add.TabIndex = 45;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // dgv_ForceSensorConfigDisplay
            // 
            this.dgv_ForceSensorConfigDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ForceSensorConfigDisplay.Location = new System.Drawing.Point(9, 193);
            this.dgv_ForceSensorConfigDisplay.Name = "dgv_ForceSensorConfigDisplay";
            this.dgv_ForceSensorConfigDisplay.Size = new System.Drawing.Size(652, 94);
            this.dgv_ForceSensorConfigDisplay.TabIndex = 48;
            this.dgv_ForceSensorConfigDisplay.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_ForceSensorConfigDisplay_CellClick);
            // 
            // pnl_ForceSensorParameterSet
            // 
            this.pnl_ForceSensorParameterSet.Controls.Add(this.cmb_Accuracy);
            this.pnl_ForceSensorParameterSet.Controls.Add(this.lab_ForceSensorDescription);
            this.pnl_ForceSensorParameterSet.Controls.Add(this.lab_Accuracy);
            this.pnl_ForceSensorParameterSet.Controls.Add(this.txb_ForceSensorDescription);
            this.pnl_ForceSensorParameterSet.Controls.Add(this.lab_BaudRate);
            this.pnl_ForceSensorParameterSet.Controls.Add(this.cmb_BaudRate);
            this.pnl_ForceSensorParameterSet.Controls.Add(this.lab_SerialPort);
            this.pnl_ForceSensorParameterSet.Controls.Add(this.lab_ForceUnit);
            this.pnl_ForceSensorParameterSet.Controls.Add(this.cmb_ForceUnit);
            this.pnl_ForceSensorParameterSet.Controls.Add(this.cmb_SerialPort);
            this.pnl_ForceSensorParameterSet.Controls.Add(this.lab_ForceSensorName);
            this.pnl_ForceSensorParameterSet.Controls.Add(this.txb_ForceSensorName);
            this.pnl_ForceSensorParameterSet.Location = new System.Drawing.Point(9, 12);
            this.pnl_ForceSensorParameterSet.Name = "pnl_ForceSensorParameterSet";
            this.pnl_ForceSensorParameterSet.Size = new System.Drawing.Size(652, 102);
            this.pnl_ForceSensorParameterSet.TabIndex = 46;
            // 
            // cmb_Accuracy
            // 
            this.cmb_Accuracy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Accuracy.FormattingEnabled = true;
            this.cmb_Accuracy.Location = new System.Drawing.Point(562, 53);
            this.cmb_Accuracy.Name = "cmb_Accuracy";
            this.cmb_Accuracy.Size = new System.Drawing.Size(48, 21);
            this.cmb_Accuracy.TabIndex = 40;
            // 
            // lab_ForceSensorDescription
            // 
            this.lab_ForceSensorDescription.AutoSize = true;
            this.lab_ForceSensorDescription.Location = new System.Drawing.Point(286, 19);
            this.lab_ForceSensorDescription.Name = "lab_ForceSensorDescription";
            this.lab_ForceSensorDescription.Size = new System.Drawing.Size(60, 13);
            this.lab_ForceSensorDescription.TabIndex = 39;
            this.lab_ForceSensorDescription.Text = "Description";
            // 
            // lab_Accuracy
            // 
            this.lab_Accuracy.AutoSize = true;
            this.lab_Accuracy.Location = new System.Drawing.Point(507, 55);
            this.lab_Accuracy.Name = "lab_Accuracy";
            this.lab_Accuracy.Size = new System.Drawing.Size(52, 13);
            this.lab_Accuracy.TabIndex = 38;
            this.lab_Accuracy.Text = "Accuracy";
            // 
            // txb_ForceSensorDescription
            // 
            this.txb_ForceSensorDescription.Location = new System.Drawing.Point(351, 16);
            this.txb_ForceSensorDescription.Name = "txb_ForceSensorDescription";
            this.txb_ForceSensorDescription.Size = new System.Drawing.Size(287, 20);
            this.txb_ForceSensorDescription.TabIndex = 34;
            // 
            // lab_BaudRate
            // 
            this.lab_BaudRate.AutoSize = true;
            this.lab_BaudRate.Location = new System.Drawing.Point(202, 55);
            this.lab_BaudRate.Name = "lab_BaudRate";
            this.lab_BaudRate.Size = new System.Drawing.Size(55, 13);
            this.lab_BaudRate.TabIndex = 32;
            this.lab_BaudRate.Text = "BaudRate";
            // 
            // cmb_BaudRate
            // 
            this.cmb_BaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_BaudRate.FormattingEnabled = true;
            this.cmb_BaudRate.Location = new System.Drawing.Point(262, 53);
            this.cmb_BaudRate.Name = "cmb_BaudRate";
            this.cmb_BaudRate.Size = new System.Drawing.Size(74, 21);
            this.cmb_BaudRate.TabIndex = 27;
            // 
            // lab_SerialPort
            // 
            this.lab_SerialPort.AutoSize = true;
            this.lab_SerialPort.Location = new System.Drawing.Point(57, 55);
            this.lab_SerialPort.Name = "lab_SerialPort";
            this.lab_SerialPort.Size = new System.Drawing.Size(55, 13);
            this.lab_SerialPort.TabIndex = 26;
            this.lab_SerialPort.Text = "Serial Port";
            // 
            // lab_ForceUnit
            // 
            this.lab_ForceUnit.AutoSize = true;
            this.lab_ForceUnit.Location = new System.Drawing.Point(364, 55);
            this.lab_ForceUnit.Name = "lab_ForceUnit";
            this.lab_ForceUnit.Size = new System.Drawing.Size(56, 13);
            this.lab_ForceUnit.TabIndex = 24;
            this.lab_ForceUnit.Text = "Force Unit";
            // 
            // cmb_ForceUnit
            // 
            this.cmb_ForceUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ForceUnit.FormattingEnabled = true;
            this.cmb_ForceUnit.Location = new System.Drawing.Point(425, 53);
            this.cmb_ForceUnit.Name = "cmb_ForceUnit";
            this.cmb_ForceUnit.Size = new System.Drawing.Size(48, 21);
            this.cmb_ForceUnit.TabIndex = 23;
            // 
            // cmb_SerialPort
            // 
            this.cmb_SerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SerialPort.FormattingEnabled = true;
            this.cmb_SerialPort.Location = new System.Drawing.Point(118, 51);
            this.cmb_SerialPort.Name = "cmb_SerialPort";
            this.cmb_SerialPort.Size = new System.Drawing.Size(60, 21);
            this.cmb_SerialPort.TabIndex = 22;
            // 
            // lab_ForceSensorName
            // 
            this.lab_ForceSensorName.AutoSize = true;
            this.lab_ForceSensorName.Location = new System.Drawing.Point(12, 20);
            this.lab_ForceSensorName.Name = "lab_ForceSensorName";
            this.lab_ForceSensorName.Size = new System.Drawing.Size(101, 13);
            this.lab_ForceSensorName.TabIndex = 20;
            this.lab_ForceSensorName.Text = "Force Sensor Name";
            // 
            // txb_ForceSensorName
            // 
            this.txb_ForceSensorName.Location = new System.Drawing.Point(118, 18);
            this.txb_ForceSensorName.Name = "txb_ForceSensorName";
            this.txb_ForceSensorName.Size = new System.Drawing.Size(122, 20);
            this.txb_ForceSensorName.TabIndex = 19;
            // 
            // ForceSensorConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 298);
            this.Controls.Add(this.pnl_ForceSensorConfigOperation);
            this.Controls.Add(this.dgv_ForceSensorConfigDisplay);
            this.Controls.Add(this.pnl_ForceSensorParameterSet);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ForceSensorConfigForm";
            this.Text = "ForceSensorConfigForm";
            this.Load += new System.EventHandler(this.ForceSensorConfigForm_Load);
            this.pnl_ForceSensorConfigOperation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ForceSensorConfigDisplay)).EndInit();
            this.pnl_ForceSensorParameterSet.ResumeLayout(false);
            this.pnl_ForceSensorParameterSet.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_ForceSensorConfigOperation;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Modify;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.DataGridView dgv_ForceSensorConfigDisplay;
        private System.Windows.Forms.Panel pnl_ForceSensorParameterSet;
        private System.Windows.Forms.Label lab_ForceSensorDescription;
        private System.Windows.Forms.Label lab_Accuracy;
        private System.Windows.Forms.TextBox txb_ForceSensorDescription;
        private System.Windows.Forms.Label lab_BaudRate;
        private System.Windows.Forms.ComboBox cmb_BaudRate;
        private System.Windows.Forms.Label lab_SerialPort;
        private System.Windows.Forms.Label lab_ForceUnit;
        private System.Windows.Forms.ComboBox cmb_ForceUnit;
        private System.Windows.Forms.ComboBox cmb_SerialPort;
        private System.Windows.Forms.Label lab_ForceSensorName;
        private System.Windows.Forms.TextBox txb_ForceSensorName;
        private System.Windows.Forms.ComboBox cmb_Accuracy;
    }
}