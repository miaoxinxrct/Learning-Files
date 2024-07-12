namespace FocusingLensAligner
{
    partial class AxisConfigForm
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
            this.txb_AxisName = new System.Windows.Forms.TextBox();
            this.cmb_AxisID = new System.Windows.Forms.ComboBox();
            this.cmb_AxisType = new System.Windows.Forms.ComboBox();
            this.cmb_HomeMode = new System.Windows.Forms.ComboBox();
            this.cmb_HomeDirection = new System.Windows.Forms.ComboBox();
            this.pnl_AxisParameterSet = new System.Windows.Forms.Panel();
            this.lab_HomeDirection = new System.Windows.Forms.Label();
            this.lab_HomeMode = new System.Windows.Forms.Label();
            this.lab_AxisID = new System.Windows.Forms.Label();
            this.lab_AxisType = new System.Windows.Forms.Label();
            this.lab_AixsName = new System.Windows.Forms.Label();
            this.pnl_AxisConfigOperation = new System.Windows.Forms.Panel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Modify = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.dgv_AxisConfigDisplay = new System.Windows.Forms.DataGridView();
            this.pnl_AxisParameterSet.SuspendLayout();
            this.pnl_AxisConfigOperation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AxisConfigDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // txb_AxisName
            // 
            this.txb_AxisName.Location = new System.Drawing.Point(79, 14);
            this.txb_AxisName.Name = "txb_AxisName";
            this.txb_AxisName.Size = new System.Drawing.Size(108, 20);
            this.txb_AxisName.TabIndex = 0;
            // 
            // cmb_AxisID
            // 
            this.cmb_AxisID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_AxisID.FormattingEnabled = true;
            this.cmb_AxisID.Location = new System.Drawing.Point(504, 14);
            this.cmb_AxisID.Name = "cmb_AxisID";
            this.cmb_AxisID.Size = new System.Drawing.Size(108, 21);
            this.cmb_AxisID.TabIndex = 1;
            // 
            // cmb_AxisType
            // 
            this.cmb_AxisType.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_AxisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_AxisType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmb_AxisType.FormattingEnabled = true;
            this.cmb_AxisType.Location = new System.Drawing.Point(287, 14);
            this.cmb_AxisType.Name = "cmb_AxisType";
            this.cmb_AxisType.Size = new System.Drawing.Size(108, 21);
            this.cmb_AxisType.TabIndex = 2;
            // 
            // cmb_HomeMode
            // 
            this.cmb_HomeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_HomeMode.FormattingEnabled = true;
            this.cmb_HomeMode.Location = new System.Drawing.Point(79, 52);
            this.cmb_HomeMode.Name = "cmb_HomeMode";
            this.cmb_HomeMode.Size = new System.Drawing.Size(108, 21);
            this.cmb_HomeMode.TabIndex = 3;
            // 
            // cmb_HomeDirection
            // 
            this.cmb_HomeDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_HomeDirection.FormattingEnabled = true;
            this.cmb_HomeDirection.Location = new System.Drawing.Point(287, 52);
            this.cmb_HomeDirection.Name = "cmb_HomeDirection";
            this.cmb_HomeDirection.Size = new System.Drawing.Size(108, 21);
            this.cmb_HomeDirection.TabIndex = 4;
            // 
            // pnl_AxisParameterSet
            // 
            this.pnl_AxisParameterSet.Controls.Add(this.lab_HomeDirection);
            this.pnl_AxisParameterSet.Controls.Add(this.lab_HomeMode);
            this.pnl_AxisParameterSet.Controls.Add(this.lab_AxisID);
            this.pnl_AxisParameterSet.Controls.Add(this.lab_AxisType);
            this.pnl_AxisParameterSet.Controls.Add(this.lab_AixsName);
            this.pnl_AxisParameterSet.Controls.Add(this.cmb_HomeMode);
            this.pnl_AxisParameterSet.Controls.Add(this.cmb_HomeDirection);
            this.pnl_AxisParameterSet.Controls.Add(this.cmb_AxisType);
            this.pnl_AxisParameterSet.Controls.Add(this.txb_AxisName);
            this.pnl_AxisParameterSet.Controls.Add(this.cmb_AxisID);
            this.pnl_AxisParameterSet.Location = new System.Drawing.Point(5, 4);
            this.pnl_AxisParameterSet.Name = "pnl_AxisParameterSet";
            this.pnl_AxisParameterSet.Size = new System.Drawing.Size(619, 88);
            this.pnl_AxisParameterSet.TabIndex = 5;
            // 
            // lab_HomeDirection
            // 
            this.lab_HomeDirection.AutoSize = true;
            this.lab_HomeDirection.Location = new System.Drawing.Point(201, 56);
            this.lab_HomeDirection.Name = "lab_HomeDirection";
            this.lab_HomeDirection.Size = new System.Drawing.Size(80, 13);
            this.lab_HomeDirection.TabIndex = 9;
            this.lab_HomeDirection.Text = "Home Direction";
            // 
            // lab_HomeMode
            // 
            this.lab_HomeMode.AutoSize = true;
            this.lab_HomeMode.Location = new System.Drawing.Point(8, 56);
            this.lab_HomeMode.Name = "lab_HomeMode";
            this.lab_HomeMode.Size = new System.Drawing.Size(65, 13);
            this.lab_HomeMode.TabIndex = 8;
            this.lab_HomeMode.Text = "Home Mode";
            // 
            // lab_AxisID
            // 
            this.lab_AxisID.AutoSize = true;
            this.lab_AxisID.Location = new System.Drawing.Point(458, 18);
            this.lab_AxisID.Name = "lab_AxisID";
            this.lab_AxisID.Size = new System.Drawing.Size(40, 13);
            this.lab_AxisID.TabIndex = 7;
            this.lab_AxisID.Text = "Axis ID";
            // 
            // lab_AxisType
            // 
            this.lab_AxisType.AutoSize = true;
            this.lab_AxisType.Location = new System.Drawing.Point(228, 18);
            this.lab_AxisType.Name = "lab_AxisType";
            this.lab_AxisType.Size = new System.Drawing.Size(53, 13);
            this.lab_AxisType.TabIndex = 6;
            this.lab_AxisType.Text = "Axis Type";
            // 
            // lab_AixsName
            // 
            this.lab_AixsName.AutoSize = true;
            this.lab_AixsName.Location = new System.Drawing.Point(16, 18);
            this.lab_AixsName.Name = "lab_AixsName";
            this.lab_AixsName.Size = new System.Drawing.Size(57, 13);
            this.lab_AixsName.TabIndex = 5;
            this.lab_AixsName.Text = "Axis Name";
            // 
            // pnl_AxisConfigOperation
            // 
            this.pnl_AxisConfigOperation.Controls.Add(this.btn_Save);
            this.pnl_AxisConfigOperation.Controls.Add(this.btn_Modify);
            this.pnl_AxisConfigOperation.Controls.Add(this.btn_Delete);
            this.pnl_AxisConfigOperation.Controls.Add(this.btn_Add);
            this.pnl_AxisConfigOperation.Location = new System.Drawing.Point(5, 98);
            this.pnl_AxisConfigOperation.Name = "pnl_AxisConfigOperation";
            this.pnl_AxisConfigOperation.Size = new System.Drawing.Size(619, 56);
            this.pnl_AxisConfigOperation.TabIndex = 6;
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.Location = new System.Drawing.Point(505, 4);
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
            this.btn_Modify.Location = new System.Drawing.Point(339, 4);
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
            this.btn_Delete.Location = new System.Drawing.Point(173, 4);
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
            // dgv_AxisConfigDisplay
            // 
            this.dgv_AxisConfigDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_AxisConfigDisplay.Location = new System.Drawing.Point(5, 160);
            this.dgv_AxisConfigDisplay.Name = "dgv_AxisConfigDisplay";
            this.dgv_AxisConfigDisplay.Size = new System.Drawing.Size(619, 224);
            this.dgv_AxisConfigDisplay.TabIndex = 7;
            this.dgv_AxisConfigDisplay.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_AxisConfigDisplay_CellClick);
            // 
            // AxisConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 388);
            this.Controls.Add(this.dgv_AxisConfigDisplay);
            this.Controls.Add(this.pnl_AxisConfigOperation);
            this.Controls.Add(this.pnl_AxisParameterSet);
            this.Name = "AxisConfigForm";
            this.Text = "AxisConfigForm";
            this.Load += new System.EventHandler(this.AxisConfigFrm_Load);
            this.pnl_AxisParameterSet.ResumeLayout(false);
            this.pnl_AxisParameterSet.PerformLayout();
            this.pnl_AxisConfigOperation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_AxisConfigDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txb_AxisName;
        private System.Windows.Forms.ComboBox cmb_AxisID;
        private System.Windows.Forms.ComboBox cmb_AxisType;
        private System.Windows.Forms.ComboBox cmb_HomeMode;
        private System.Windows.Forms.ComboBox cmb_HomeDirection;
        private System.Windows.Forms.Panel pnl_AxisParameterSet;
        private System.Windows.Forms.Label lab_HomeDirection;
        private System.Windows.Forms.Label lab_HomeMode;
        private System.Windows.Forms.Label lab_AxisID;
        private System.Windows.Forms.Label lab_AxisType;
        private System.Windows.Forms.Label lab_AixsName;
        private System.Windows.Forms.Panel pnl_AxisConfigOperation;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Modify;
        private System.Windows.Forms.Button btn_Delete;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.DataGridView dgv_AxisConfigDisplay;
    }
}