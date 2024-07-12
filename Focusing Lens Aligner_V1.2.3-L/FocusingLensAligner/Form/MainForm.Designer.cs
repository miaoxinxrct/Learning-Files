namespace FocusingLensAligner
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.OperationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_LoadEngineeringForm = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_LoadEpoxyTestForm = new System.Windows.Forms.ToolStripMenuItem();
            this.ModuleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.motionConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_MotionSystemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_AxisConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.iOConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_IOCardTypeConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_IOPortConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_CameraConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_LightSourceConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_ForceSensorConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_SourceMeterConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_UVControllerConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_GripperControllerConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_FI2CUSBDeviceConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_SMCCardMotionConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_QRCodeScannerConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_TECControllerConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_CamstarConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_VisionProcessConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.ConfigurationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_ProductConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.DropDownMenuItem_SystemConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.cogRecordDisplay1 = new Cognex.VisionPro.CogRecordDisplay();
            this.pnl_SourceMeter = new System.Windows.Forms.Panel();
            this.tb_SourceMeterCurrent = new System.Windows.Forms.TextBox();
            this.tb_SourceMeterVoltage = new System.Windows.Forms.TextBox();
            this.lab_SourceMeter = new System.Windows.Forms.Label();
            this.PictureBox_Channel3LaserOn = new System.Windows.Forms.PictureBox();
            this.PictureBox_Channel2LaserOn = new System.Windows.Forms.PictureBox();
            this.PictureBox_Channel1LaserOn = new System.Windows.Forms.PictureBox();
            this.PictureBox_Channel0LaserOn = new System.Windows.Forms.PictureBox();
            this.Button_Channel3LaserOnOff = new System.Windows.Forms.Button();
            this.Button_Channel2LaserOnOff = new System.Windows.Forms.Button();
            this.Button_Channel1LaserOnOff = new System.Windows.Forms.Button();
            this.Button_Channel0LaserOnOff = new System.Windows.Forms.Button();
            this.GroupBox_ShortcutButton = new System.Windows.Forms.GroupBox();
            this.Button_RecognizeBoxWindowCenter = new System.Windows.Forms.Button();
            this.Button_Z2AxisSafetyUpMoving = new System.Windows.Forms.Button();
            this.Button_Z1AxisSafetyUpMoving = new System.Windows.Forms.Button();
            this.Button_EpoxyDipCylinderControl = new System.Windows.Forms.Button();
            this.Button_BoxGripperCylinderControl = new System.Windows.Forms.Button();
            this.Button_LensDiscard = new System.Windows.Forms.Button();
            this.Button_DispenserEpoxyPinOutOfBox = new System.Windows.Forms.Button();
            this.Button_LensOutOfBox = new System.Windows.Forms.Button();
            this.Button_RecognizeLaserSpotCenter = new System.Windows.Forms.Button();
            this.Button_LensGripperControl = new System.Windows.Forms.Button();
            this.Button_BoxGripperControl = new System.Windows.Forms.Button();
            this.Button_PogoPinControl = new System.Windows.Forms.Button();
            this.Button_EpoxyTest = new System.Windows.Forms.Button();
            this.GroupBox_ChannelLaserOnOff = new System.Windows.Forms.GroupBox();
            this.chkbox_Chl0Valid = new System.Windows.Forms.CheckBox();
            this.chkbox_Chl3Valid = new System.Windows.Forms.CheckBox();
            this.chkbox_Chl2Valid = new System.Windows.Forms.CheckBox();
            this.chkbox_Chl1Valid = new System.Windows.Forms.CheckBox();
            this.pnl_ProductionOperatin = new System.Windows.Forms.Panel();
            this.Button_HomeAxes = new System.Windows.Forms.Button();
            this.Button_AxesGoSafetyPosition = new System.Windows.Forms.Button();
            this.Button_Stop = new System.Windows.Forms.Button();
            this.Button_Start = new System.Windows.Forms.Button();
            this.GroupBox_Materials = new System.Windows.Forms.GroupBox();
            this.gb_BoxTray = new System.Windows.Forms.GroupBox();
            this.pnl_BoxTray = new System.Windows.Forms.Panel();
            this.lab_Split1 = new System.Windows.Forms.Label();
            this.CheckBox_AllBoxLoad = new System.Windows.Forms.CheckBox();
            this.gb_LensTray = new System.Windows.Forms.GroupBox();
            this.pnl_LensTray = new System.Windows.Forms.Panel();
            this.CheckBox_AllLensLoad = new System.Windows.Forms.CheckBox();
            this.GroupBox_WorkFlow = new System.Windows.Forms.GroupBox();
            this.ComboBox_SelectedLensIndex = new System.Windows.Forms.ComboBox();
            this.Label_SelectedLensIndex = new System.Windows.Forms.Label();
            this.ComboBox_SelectedBoxIndex = new System.Windows.Forms.ComboBox();
            this.Label_SelectedBoxIndex = new System.Windows.Forms.Label();
            this.ListBox_Stepflow = new System.Windows.Forms.ListBox();
            this.GroupBox_SystemInfo = new System.Windows.Forms.GroupBox();
            this.Button_EmptySystemInfo = new System.Windows.Forms.Button();
            this.lb_SystemInfo = new System.Windows.Forms.ListBox();
            this.GroupBox_ProductPrepare = new System.Windows.Forms.GroupBox();
            this.cmb_ListProduct = new System.Windows.Forms.ComboBox();
            this.lab_SN = new System.Windows.Forms.Label();
            this.lab_Product = new System.Windows.Forms.Label();
            this.lab_EmpID = new System.Windows.Forms.Label();
            this.tb_EmployeeID = new System.Windows.Forms.TextBox();
            this.tb_ProductSN = new System.Windows.Forms.TextBox();
            this.Button_RemoveDC = new System.Windows.Forms.Button();
            this.lv_DCInfo = new System.Windows.Forms.ListView();
            this.col0 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lab_DCLot = new System.Windows.Forms.Label();
            this.tb_DCLot = new System.Windows.Forms.TextBox();
            this.GroupBox_DCList = new System.Windows.Forms.GroupBox();
            this.GroupBox_SystemStatus = new System.Windows.Forms.GroupBox();
            this.lab_Status = new System.Windows.Forms.Label();
            this.lab_EStop = new System.Windows.Forms.Label();
            this.lab_Home = new System.Windows.Forms.Label();
            this.lab_LedTower = new System.Windows.Forms.Label();
            this.lab_LedEStop = new System.Windows.Forms.Label();
            this.lab_LedHome = new System.Windows.Forms.Label();
            this.CheckBox_ShowPressureForce = new System.Windows.Forms.CheckBox();
            this.timer_MainForm = new System.Windows.Forms.Timer(this.components);
            this.DCTimer = new System.Windows.Forms.Timer(this.components);
            this.tb_pressureforce = new System.Windows.Forms.TextBox();
            this.Label_PressureForceUnit = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).BeginInit();
            this.pnl_SourceMeter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Channel3LaserOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Channel2LaserOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Channel1LaserOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Channel0LaserOn)).BeginInit();
            this.GroupBox_ShortcutButton.SuspendLayout();
            this.GroupBox_ChannelLaserOnOff.SuspendLayout();
            this.pnl_ProductionOperatin.SuspendLayout();
            this.GroupBox_Materials.SuspendLayout();
            this.gb_BoxTray.SuspendLayout();
            this.pnl_BoxTray.SuspendLayout();
            this.gb_LensTray.SuspendLayout();
            this.GroupBox_WorkFlow.SuspendLayout();
            this.GroupBox_SystemInfo.SuspendLayout();
            this.GroupBox_ProductPrepare.SuspendLayout();
            this.GroupBox_DCList.SuspendLayout();
            this.GroupBox_SystemStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OperationMenuItem,
            this.ModuleMenuItem,
            this.ConfigurationMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(18, 826);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(233, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // OperationMenuItem
            // 
            this.OperationMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DropDownMenuItem_LoadEngineeringForm,
            this.DropDownMenuItem_LoadEpoxyTestForm});
            this.OperationMenuItem.Name = "OperationMenuItem";
            this.OperationMenuItem.Size = new System.Drawing.Size(72, 20);
            this.OperationMenuItem.Text = "Operation";
            // 
            // DropDownMenuItem_LoadEngineeringForm
            // 
            this.DropDownMenuItem_LoadEngineeringForm.Name = "DropDownMenuItem_LoadEngineeringForm";
            this.DropDownMenuItem_LoadEngineeringForm.Size = new System.Drawing.Size(137, 22);
            this.DropDownMenuItem_LoadEngineeringForm.Text = "Engineering";
            this.DropDownMenuItem_LoadEngineeringForm.Click += new System.EventHandler(this.DropDownMenuItem_LoadEngineeringForm_Click);
            // 
            // DropDownMenuItem_LoadEpoxyTestForm
            // 
            this.DropDownMenuItem_LoadEpoxyTestForm.Name = "DropDownMenuItem_LoadEpoxyTestForm";
            this.DropDownMenuItem_LoadEpoxyTestForm.Size = new System.Drawing.Size(137, 22);
            this.DropDownMenuItem_LoadEpoxyTestForm.Text = "Epoxy Test";
            this.DropDownMenuItem_LoadEpoxyTestForm.Click += new System.EventHandler(this.DropDownMenuItem_LoadEpoxyTestForm_Click);
            // 
            // ModuleMenuItem
            // 
            this.ModuleMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.motionConfigToolStripMenuItem,
            this.iOConfigToolStripMenuItem,
            this.DropDownMenuItem_CameraConfig,
            this.DropDownMenuItem_LightSourceConfig,
            this.DropDownMenuItem_ForceSensorConfig,
            this.DropDownMenuItem_SourceMeterConfig,
            this.DropDownMenuItem_UVControllerConfig,
            this.DropDownMenuItem_GripperControllerConfig,
            this.DropDownMenuItem_FI2CUSBDeviceConfig,
            this.DropDownMenuItem_SMCCardMotionConfig,
            this.DropDownMenuItem_QRCodeScannerConfig,
            this.DropDownMenuItem_TECControllerConfig,
            this.DropDownMenuItem_CamstarConfig,
            this.DropDownMenuItem_VisionProcessConfig});
            this.ModuleMenuItem.Enabled = false;
            this.ModuleMenuItem.Name = "ModuleMenuItem";
            this.ModuleMenuItem.Size = new System.Drawing.Size(60, 20);
            this.ModuleMenuItem.Text = "Module";
            // 
            // motionConfigToolStripMenuItem
            // 
            this.motionConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DropDownMenuItem_MotionSystemConfig,
            this.DropDownMenuItem_AxisConfig});
            this.motionConfigToolStripMenuItem.Name = "motionConfigToolStripMenuItem";
            this.motionConfigToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.motionConfigToolStripMenuItem.Text = "Motion Config";
            // 
            // DropDownMenuItem_MotionSystemConfig
            // 
            this.DropDownMenuItem_MotionSystemConfig.Name = "DropDownMenuItem_MotionSystemConfig";
            this.DropDownMenuItem_MotionSystemConfig.Size = new System.Drawing.Size(193, 22);
            this.DropDownMenuItem_MotionSystemConfig.Text = "Motion System Config";
            this.DropDownMenuItem_MotionSystemConfig.Click += new System.EventHandler(this.DropDownMenuItem_MotionSystemConfig_Click);
            // 
            // DropDownMenuItem_AxisConfig
            // 
            this.DropDownMenuItem_AxisConfig.Name = "DropDownMenuItem_AxisConfig";
            this.DropDownMenuItem_AxisConfig.Size = new System.Drawing.Size(193, 22);
            this.DropDownMenuItem_AxisConfig.Text = "Axis Config";
            this.DropDownMenuItem_AxisConfig.Click += new System.EventHandler(this.DropDownMenuItem_AxisConfig_Click);
            // 
            // iOConfigToolStripMenuItem
            // 
            this.iOConfigToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DropDownMenuItem_IOCardTypeConfig,
            this.DropDownMenuItem_IOPortConfig});
            this.iOConfigToolStripMenuItem.Name = "iOConfigToolStripMenuItem";
            this.iOConfigToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.iOConfigToolStripMenuItem.Text = "I/O Config";
            // 
            // DropDownMenuItem_IOCardTypeConfig
            // 
            this.DropDownMenuItem_IOCardTypeConfig.Name = "DropDownMenuItem_IOCardTypeConfig";
            this.DropDownMenuItem_IOCardTypeConfig.Size = new System.Drawing.Size(165, 22);
            this.DropDownMenuItem_IOCardTypeConfig.Text = "Card Type Config";
            this.DropDownMenuItem_IOCardTypeConfig.Click += new System.EventHandler(this.DropDownMenuItem_IOCardTypeConfig_Click);
            // 
            // DropDownMenuItem_IOPortConfig
            // 
            this.DropDownMenuItem_IOPortConfig.Name = "DropDownMenuItem_IOPortConfig";
            this.DropDownMenuItem_IOPortConfig.Size = new System.Drawing.Size(165, 22);
            this.DropDownMenuItem_IOPortConfig.Text = "I/O Port Config";
            this.DropDownMenuItem_IOPortConfig.Click += new System.EventHandler(this.DropDownMenuItem_IOPortConfig_Click);
            // 
            // DropDownMenuItem_CameraConfig
            // 
            this.DropDownMenuItem_CameraConfig.Name = "DropDownMenuItem_CameraConfig";
            this.DropDownMenuItem_CameraConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_CameraConfig.Text = "Camera Config";
            this.DropDownMenuItem_CameraConfig.Click += new System.EventHandler(this.DropDownMenuItem_CameraConfig_Click);
            // 
            // DropDownMenuItem_LightSourceConfig
            // 
            this.DropDownMenuItem_LightSourceConfig.Name = "DropDownMenuItem_LightSourceConfig";
            this.DropDownMenuItem_LightSourceConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_LightSourceConfig.Text = "Light Source Config";
            this.DropDownMenuItem_LightSourceConfig.Click += new System.EventHandler(this.DropDownMenuItem_LightSourceConfig_Click);
            // 
            // DropDownMenuItem_ForceSensorConfig
            // 
            this.DropDownMenuItem_ForceSensorConfig.Name = "DropDownMenuItem_ForceSensorConfig";
            this.DropDownMenuItem_ForceSensorConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_ForceSensorConfig.Text = "Force Sensor Config";
            this.DropDownMenuItem_ForceSensorConfig.Click += new System.EventHandler(this.DropDownMenuItem_ForceSensorConfig_Click);
            // 
            // DropDownMenuItem_SourceMeterConfig
            // 
            this.DropDownMenuItem_SourceMeterConfig.Name = "DropDownMenuItem_SourceMeterConfig";
            this.DropDownMenuItem_SourceMeterConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_SourceMeterConfig.Text = "Source Meter Config";
            this.DropDownMenuItem_SourceMeterConfig.Click += new System.EventHandler(this.DropDownMenuItem_SourceMeterConfig_Click);
            // 
            // DropDownMenuItem_UVControllerConfig
            // 
            this.DropDownMenuItem_UVControllerConfig.Name = "DropDownMenuItem_UVControllerConfig";
            this.DropDownMenuItem_UVControllerConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_UVControllerConfig.Text = "UV Controller Config";
            this.DropDownMenuItem_UVControllerConfig.Click += new System.EventHandler(this.DropDownMenuItem_UVControllerConfig_Click);
            // 
            // DropDownMenuItem_GripperControllerConfig
            // 
            this.DropDownMenuItem_GripperControllerConfig.Name = "DropDownMenuItem_GripperControllerConfig";
            this.DropDownMenuItem_GripperControllerConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_GripperControllerConfig.Text = "Gripper Controller Config";
            this.DropDownMenuItem_GripperControllerConfig.Click += new System.EventHandler(this.DropDownMenuItem_GripperControllerConfig_Click);
            // 
            // DropDownMenuItem_FI2CUSBDeviceConfig
            // 
            this.DropDownMenuItem_FI2CUSBDeviceConfig.Name = "DropDownMenuItem_FI2CUSBDeviceConfig";
            this.DropDownMenuItem_FI2CUSBDeviceConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_FI2CUSBDeviceConfig.Text = "FI2CUSB Device Config";
            this.DropDownMenuItem_FI2CUSBDeviceConfig.Click += new System.EventHandler(this.DropDownMenuItem_FI2CUSBDeviceConfig_Click);
            // 
            // DropDownMenuItem_SMCCardMotionConfig
            // 
            this.DropDownMenuItem_SMCCardMotionConfig.Name = "DropDownMenuItem_SMCCardMotionConfig";
            this.DropDownMenuItem_SMCCardMotionConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_SMCCardMotionConfig.Text = "SMC Card Motion Config";
            this.DropDownMenuItem_SMCCardMotionConfig.Click += new System.EventHandler(this.DropDownMenuItem_SMCCardMotionConfig_Click);
            // 
            // DropDownMenuItem_QRCodeScannerConfig
            // 
            this.DropDownMenuItem_QRCodeScannerConfig.Name = "DropDownMenuItem_QRCodeScannerConfig";
            this.DropDownMenuItem_QRCodeScannerConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_QRCodeScannerConfig.Text = "QR Coder Scanner Config";
            this.DropDownMenuItem_QRCodeScannerConfig.Click += new System.EventHandler(this.DropDownMenuItem_QRCodeScannerConfig_Click);
            // 
            // DropDownMenuItem_TECControllerConfig
            // 
            this.DropDownMenuItem_TECControllerConfig.Name = "DropDownMenuItem_TECControllerConfig";
            this.DropDownMenuItem_TECControllerConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_TECControllerConfig.Text = "TEC Controller Config";
            this.DropDownMenuItem_TECControllerConfig.Click += new System.EventHandler(this.DropDownMenuItem_TECControllerConfig_Click);
            // 
            // DropDownMenuItem_CamstarConfig
            // 
            this.DropDownMenuItem_CamstarConfig.Name = "DropDownMenuItem_CamstarConfig";
            this.DropDownMenuItem_CamstarConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_CamstarConfig.Text = "Camstar Config";
            this.DropDownMenuItem_CamstarConfig.Click += new System.EventHandler(this.DropDownMenuItem_CamstarConfig_Click);
            // 
            // DropDownMenuItem_VisionProcessConfig
            // 
            this.DropDownMenuItem_VisionProcessConfig.Name = "DropDownMenuItem_VisionProcessConfig";
            this.DropDownMenuItem_VisionProcessConfig.Size = new System.Drawing.Size(209, 22);
            this.DropDownMenuItem_VisionProcessConfig.Text = "Vision Process Config";
            this.DropDownMenuItem_VisionProcessConfig.Click += new System.EventHandler(this.DropDownMenuItem_VisionProcessConfig_Click);
            // 
            // ConfigurationMenuItem
            // 
            this.ConfigurationMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DropDownMenuItem_ProductConfig,
            this.DropDownMenuItem_SystemConfig});
            this.ConfigurationMenuItem.Name = "ConfigurationMenuItem";
            this.ConfigurationMenuItem.Size = new System.Drawing.Size(93, 20);
            this.ConfigurationMenuItem.Text = "Configuration";
            // 
            // DropDownMenuItem_ProductConfig
            // 
            this.DropDownMenuItem_ProductConfig.Name = "DropDownMenuItem_ProductConfig";
            this.DropDownMenuItem_ProductConfig.Size = new System.Drawing.Size(155, 22);
            this.DropDownMenuItem_ProductConfig.Text = "Product Config";
            this.DropDownMenuItem_ProductConfig.Click += new System.EventHandler(this.DropDownMenuItem_ProductConfig_Click);
            // 
            // DropDownMenuItem_SystemConfig
            // 
            this.DropDownMenuItem_SystemConfig.Name = "DropDownMenuItem_SystemConfig";
            this.DropDownMenuItem_SystemConfig.Size = new System.Drawing.Size(155, 22);
            this.DropDownMenuItem_SystemConfig.Text = "System Config";
            this.DropDownMenuItem_SystemConfig.Click += new System.EventHandler(this.DropDownMenuItem_SystemConfig_Click);
            // 
            // cogRecordDisplay1
            // 
            this.cogRecordDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapLowerRoiLimit = 0D;
            this.cogRecordDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogRecordDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapUpperRoiLimit = 1D;
            this.cogRecordDisplay1.DoubleTapZoomCycleLength = 2;
            this.cogRecordDisplay1.DoubleTapZoomSensitivity = 2.5D;
            this.cogRecordDisplay1.Location = new System.Drawing.Point(12, 15);
            this.cogRecordDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.None;
            this.cogRecordDisplay1.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay1.Name = "cogRecordDisplay1";
            this.cogRecordDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay1.OcxState")));
            this.cogRecordDisplay1.Size = new System.Drawing.Size(673, 492);
            this.cogRecordDisplay1.TabIndex = 17;
            // 
            // pnl_SourceMeter
            // 
            this.pnl_SourceMeter.BackColor = System.Drawing.Color.Black;
            this.pnl_SourceMeter.Controls.Add(this.tb_SourceMeterCurrent);
            this.pnl_SourceMeter.Controls.Add(this.tb_SourceMeterVoltage);
            this.pnl_SourceMeter.Controls.Add(this.lab_SourceMeter);
            this.pnl_SourceMeter.Location = new System.Drawing.Point(1014, 598);
            this.pnl_SourceMeter.Name = "pnl_SourceMeter";
            this.pnl_SourceMeter.Size = new System.Drawing.Size(239, 60);
            this.pnl_SourceMeter.TabIndex = 18;
            // 
            // tb_SourceMeterCurrent
            // 
            this.tb_SourceMeterCurrent.BackColor = System.Drawing.Color.Black;
            this.tb_SourceMeterCurrent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_SourceMeterCurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_SourceMeterCurrent.ForeColor = System.Drawing.SystemColors.Window;
            this.tb_SourceMeterCurrent.Location = new System.Drawing.Point(131, 25);
            this.tb_SourceMeterCurrent.Name = "tb_SourceMeterCurrent";
            this.tb_SourceMeterCurrent.Size = new System.Drawing.Size(105, 24);
            this.tb_SourceMeterCurrent.TabIndex = 21;
            this.tb_SourceMeterCurrent.Text = "+0.000A";
            // 
            // tb_SourceMeterVoltage
            // 
            this.tb_SourceMeterVoltage.BackColor = System.Drawing.Color.Black;
            this.tb_SourceMeterVoltage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_SourceMeterVoltage.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_SourceMeterVoltage.ForeColor = System.Drawing.SystemColors.Window;
            this.tb_SourceMeterVoltage.Location = new System.Drawing.Point(18, 25);
            this.tb_SourceMeterVoltage.Name = "tb_SourceMeterVoltage";
            this.tb_SourceMeterVoltage.Size = new System.Drawing.Size(99, 24);
            this.tb_SourceMeterVoltage.TabIndex = 3;
            this.tb_SourceMeterVoltage.Text = "0.000V";
            // 
            // lab_SourceMeter
            // 
            this.lab_SourceMeter.AutoSize = true;
            this.lab_SourceMeter.BackColor = System.Drawing.Color.Red;
            this.lab_SourceMeter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_SourceMeter.ForeColor = System.Drawing.Color.White;
            this.lab_SourceMeter.Location = new System.Drawing.Point(1, 1);
            this.lab_SourceMeter.Name = "lab_SourceMeter";
            this.lab_SourceMeter.Size = new System.Drawing.Size(100, 16);
            this.lab_SourceMeter.TabIndex = 20;
            this.lab_SourceMeter.Text = "Source Meter";
            // 
            // PictureBox_Channel3LaserOn
            // 
            this.PictureBox_Channel3LaserOn.BackgroundImage = global::FocusingLensAligner.Properties.Resources.Grayled;
            this.PictureBox_Channel3LaserOn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBox_Channel3LaserOn.Location = new System.Drawing.Point(193, 129);
            this.PictureBox_Channel3LaserOn.Name = "PictureBox_Channel3LaserOn";
            this.PictureBox_Channel3LaserOn.Size = new System.Drawing.Size(25, 25);
            this.PictureBox_Channel3LaserOn.TabIndex = 71;
            this.PictureBox_Channel3LaserOn.TabStop = false;
            // 
            // PictureBox_Channel2LaserOn
            // 
            this.PictureBox_Channel2LaserOn.BackgroundImage = global::FocusingLensAligner.Properties.Resources.Grayled;
            this.PictureBox_Channel2LaserOn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBox_Channel2LaserOn.Location = new System.Drawing.Point(193, 94);
            this.PictureBox_Channel2LaserOn.Name = "PictureBox_Channel2LaserOn";
            this.PictureBox_Channel2LaserOn.Size = new System.Drawing.Size(25, 25);
            this.PictureBox_Channel2LaserOn.TabIndex = 70;
            this.PictureBox_Channel2LaserOn.TabStop = false;
            // 
            // PictureBox_Channel1LaserOn
            // 
            this.PictureBox_Channel1LaserOn.BackgroundImage = global::FocusingLensAligner.Properties.Resources.Grayled;
            this.PictureBox_Channel1LaserOn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBox_Channel1LaserOn.Location = new System.Drawing.Point(193, 59);
            this.PictureBox_Channel1LaserOn.Name = "PictureBox_Channel1LaserOn";
            this.PictureBox_Channel1LaserOn.Size = new System.Drawing.Size(25, 25);
            this.PictureBox_Channel1LaserOn.TabIndex = 69;
            this.PictureBox_Channel1LaserOn.TabStop = false;
            // 
            // PictureBox_Channel0LaserOn
            // 
            this.PictureBox_Channel0LaserOn.BackgroundImage = global::FocusingLensAligner.Properties.Resources.Grayled;
            this.PictureBox_Channel0LaserOn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBox_Channel0LaserOn.Location = new System.Drawing.Point(193, 24);
            this.PictureBox_Channel0LaserOn.Name = "PictureBox_Channel0LaserOn";
            this.PictureBox_Channel0LaserOn.Size = new System.Drawing.Size(25, 25);
            this.PictureBox_Channel0LaserOn.TabIndex = 68;
            this.PictureBox_Channel0LaserOn.TabStop = false;
            // 
            // Button_Channel3LaserOnOff
            // 
            this.Button_Channel3LaserOnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Channel3LaserOnOff.Location = new System.Drawing.Point(95, 127);
            this.Button_Channel3LaserOnOff.Name = "Button_Channel3LaserOnOff";
            this.Button_Channel3LaserOnOff.Size = new System.Drawing.Size(77, 30);
            this.Button_Channel3LaserOnOff.TabIndex = 7;
            this.Button_Channel3LaserOnOff.Tag = "3";
            this.Button_Channel3LaserOnOff.Text = "打开";
            this.Button_Channel3LaserOnOff.UseVisualStyleBackColor = true;
            this.Button_Channel3LaserOnOff.Click += new System.EventHandler(this.Button_ChannelLaserOnOff_Click);
            // 
            // Button_Channel2LaserOnOff
            // 
            this.Button_Channel2LaserOnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Channel2LaserOnOff.Location = new System.Drawing.Point(95, 92);
            this.Button_Channel2LaserOnOff.Name = "Button_Channel2LaserOnOff";
            this.Button_Channel2LaserOnOff.Size = new System.Drawing.Size(77, 30);
            this.Button_Channel2LaserOnOff.TabIndex = 6;
            this.Button_Channel2LaserOnOff.Tag = "2";
            this.Button_Channel2LaserOnOff.Text = "打开";
            this.Button_Channel2LaserOnOff.UseVisualStyleBackColor = true;
            this.Button_Channel2LaserOnOff.Click += new System.EventHandler(this.Button_ChannelLaserOnOff_Click);
            // 
            // Button_Channel1LaserOnOff
            // 
            this.Button_Channel1LaserOnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Channel1LaserOnOff.Location = new System.Drawing.Point(95, 57);
            this.Button_Channel1LaserOnOff.Name = "Button_Channel1LaserOnOff";
            this.Button_Channel1LaserOnOff.Size = new System.Drawing.Size(77, 30);
            this.Button_Channel1LaserOnOff.TabIndex = 5;
            this.Button_Channel1LaserOnOff.Tag = "1";
            this.Button_Channel1LaserOnOff.Text = "打开";
            this.Button_Channel1LaserOnOff.UseVisualStyleBackColor = true;
            this.Button_Channel1LaserOnOff.Click += new System.EventHandler(this.Button_ChannelLaserOnOff_Click);
            // 
            // Button_Channel0LaserOnOff
            // 
            this.Button_Channel0LaserOnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Channel0LaserOnOff.Location = new System.Drawing.Point(95, 22);
            this.Button_Channel0LaserOnOff.Name = "Button_Channel0LaserOnOff";
            this.Button_Channel0LaserOnOff.Size = new System.Drawing.Size(77, 30);
            this.Button_Channel0LaserOnOff.TabIndex = 4;
            this.Button_Channel0LaserOnOff.Tag = "0";
            this.Button_Channel0LaserOnOff.Text = "打开";
            this.Button_Channel0LaserOnOff.UseVisualStyleBackColor = true;
            this.Button_Channel0LaserOnOff.Click += new System.EventHandler(this.Button_ChannelLaserOnOff_Click);
            // 
            // GroupBox_ShortcutButton
            // 
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_RecognizeBoxWindowCenter);
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_Z2AxisSafetyUpMoving);
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_Z1AxisSafetyUpMoving);
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_EpoxyDipCylinderControl);
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_BoxGripperCylinderControl);
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_LensDiscard);
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_DispenserEpoxyPinOutOfBox);
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_LensOutOfBox);
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_RecognizeLaserSpotCenter);
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_LensGripperControl);
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_BoxGripperControl);
            this.GroupBox_ShortcutButton.Controls.Add(this.Button_PogoPinControl);
            this.GroupBox_ShortcutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GroupBox_ShortcutButton.Location = new System.Drawing.Point(415, 586);
            this.GroupBox_ShortcutButton.Name = "GroupBox_ShortcutButton";
            this.GroupBox_ShortcutButton.Size = new System.Drawing.Size(270, 251);
            this.GroupBox_ShortcutButton.TabIndex = 21;
            this.GroupBox_ShortcutButton.TabStop = false;
            this.GroupBox_ShortcutButton.Text = "快捷操作";
            // 
            // Button_RecognizeBoxWindowCenter
            // 
            this.Button_RecognizeBoxWindowCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_RecognizeBoxWindowCenter.Location = new System.Drawing.Point(10, 169);
            this.Button_RecognizeBoxWindowCenter.Name = "Button_RecognizeBoxWindowCenter";
            this.Button_RecognizeBoxWindowCenter.Size = new System.Drawing.Size(120, 30);
            this.Button_RecognizeBoxWindowCenter.TabIndex = 20;
            this.Button_RecognizeBoxWindowCenter.Text = "识别产品Box窗口";
            this.Button_RecognizeBoxWindowCenter.UseVisualStyleBackColor = true;
            this.Button_RecognizeBoxWindowCenter.Click += new System.EventHandler(this.Button_RecognizeBoxWindowCenter_Click);
            // 
            // Button_Z2AxisSafetyUpMoving
            // 
            this.Button_Z2AxisSafetyUpMoving.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Z2AxisSafetyUpMoving.Location = new System.Drawing.Point(140, 133);
            this.Button_Z2AxisSafetyUpMoving.Name = "Button_Z2AxisSafetyUpMoving";
            this.Button_Z2AxisSafetyUpMoving.Size = new System.Drawing.Size(120, 30);
            this.Button_Z2AxisSafetyUpMoving.TabIndex = 19;
            this.Button_Z2AxisSafetyUpMoving.Text = "步进Z轴回安全位";
            this.Button_Z2AxisSafetyUpMoving.UseVisualStyleBackColor = true;
            this.Button_Z2AxisSafetyUpMoving.Click += new System.EventHandler(this.Button_Z2AxisMoveToSafetyPosition_Click);
            // 
            // Button_Z1AxisSafetyUpMoving
            // 
            this.Button_Z1AxisSafetyUpMoving.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_Z1AxisSafetyUpMoving.Location = new System.Drawing.Point(10, 133);
            this.Button_Z1AxisSafetyUpMoving.Name = "Button_Z1AxisSafetyUpMoving";
            this.Button_Z1AxisSafetyUpMoving.Size = new System.Drawing.Size(120, 30);
            this.Button_Z1AxisSafetyUpMoving.TabIndex = 18;
            this.Button_Z1AxisSafetyUpMoving.Text = "伺服Z轴回安全位";
            this.Button_Z1AxisSafetyUpMoving.UseVisualStyleBackColor = true;
            this.Button_Z1AxisSafetyUpMoving.Click += new System.EventHandler(this.Button_Z1AxisMoveToSafetyPosition_Click);
            // 
            // Button_EpoxyDipCylinderControl
            // 
            this.Button_EpoxyDipCylinderControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_EpoxyDipCylinderControl.Location = new System.Drawing.Point(140, 97);
            this.Button_EpoxyDipCylinderControl.Name = "Button_EpoxyDipCylinderControl";
            this.Button_EpoxyDipCylinderControl.Size = new System.Drawing.Size(120, 30);
            this.Button_EpoxyDipCylinderControl.TabIndex = 17;
            this.Button_EpoxyDipCylinderControl.Text = "胶针下降";
            this.Button_EpoxyDipCylinderControl.UseVisualStyleBackColor = true;
            this.Button_EpoxyDipCylinderControl.Click += new System.EventHandler(this.Button_EpoxyDipCylinderControl_Click);
            // 
            // Button_BoxGripperCylinderControl
            // 
            this.Button_BoxGripperCylinderControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_BoxGripperCylinderControl.Location = new System.Drawing.Point(10, 97);
            this.Button_BoxGripperCylinderControl.Name = "Button_BoxGripperCylinderControl";
            this.Button_BoxGripperCylinderControl.Size = new System.Drawing.Size(120, 30);
            this.Button_BoxGripperCylinderControl.TabIndex = 16;
            this.Button_BoxGripperCylinderControl.Text = "Box夹爪下降";
            this.Button_BoxGripperCylinderControl.UseVisualStyleBackColor = true;
            this.Button_BoxGripperCylinderControl.Click += new System.EventHandler(this.Button_BoxGripperCylinderControl_Click);
            // 
            // Button_LensDiscard
            // 
            this.Button_LensDiscard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_LensDiscard.Location = new System.Drawing.Point(140, 25);
            this.Button_LensDiscard.Name = "Button_LensDiscard";
            this.Button_LensDiscard.Size = new System.Drawing.Size(120, 30);
            this.Button_LensDiscard.TabIndex = 15;
            this.Button_LensDiscard.Text = "Lens抛料";
            this.Button_LensDiscard.UseVisualStyleBackColor = true;
            this.Button_LensDiscard.Click += new System.EventHandler(this.Button_LensDiscard_Click);
            // 
            // Button_DispenserEpoxyPinOutOfBox
            // 
            this.Button_DispenserEpoxyPinOutOfBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_DispenserEpoxyPinOutOfBox.Location = new System.Drawing.Point(140, 205);
            this.Button_DispenserEpoxyPinOutOfBox.Name = "Button_DispenserEpoxyPinOutOfBox";
            this.Button_DispenserEpoxyPinOutOfBox.Size = new System.Drawing.Size(120, 30);
            this.Button_DispenserEpoxyPinOutOfBox.TabIndex = 13;
            this.Button_DispenserEpoxyPinOutOfBox.Text = "胶针移出产品Box";
            this.Button_DispenserEpoxyPinOutOfBox.UseVisualStyleBackColor = true;
            this.Button_DispenserEpoxyPinOutOfBox.Click += new System.EventHandler(this.Button_DispenserEpoxyPinOutOfBox_Click);
            // 
            // Button_LensOutOfBox
            // 
            this.Button_LensOutOfBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_LensOutOfBox.Location = new System.Drawing.Point(10, 205);
            this.Button_LensOutOfBox.Name = "Button_LensOutOfBox";
            this.Button_LensOutOfBox.Size = new System.Drawing.Size(120, 30);
            this.Button_LensOutOfBox.TabIndex = 12;
            this.Button_LensOutOfBox.Text = "Lens移出产品Box";
            this.Button_LensOutOfBox.UseVisualStyleBackColor = true;
            this.Button_LensOutOfBox.Click += new System.EventHandler(this.Button_LensOutOfBox_Click);
            // 
            // Button_RecognizeLaserSpotCenter
            // 
            this.Button_RecognizeLaserSpotCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_RecognizeLaserSpotCenter.Location = new System.Drawing.Point(140, 169);
            this.Button_RecognizeLaserSpotCenter.Name = "Button_RecognizeLaserSpotCenter";
            this.Button_RecognizeLaserSpotCenter.Size = new System.Drawing.Size(120, 30);
            this.Button_RecognizeLaserSpotCenter.TabIndex = 11;
            this.Button_RecognizeLaserSpotCenter.Text = "识别光斑中心";
            this.Button_RecognizeLaserSpotCenter.UseVisualStyleBackColor = true;
            this.Button_RecognizeLaserSpotCenter.Click += new System.EventHandler(this.Button_RecognizeLaserSpotCenter_Click);
            // 
            // Button_LensGripperControl
            // 
            this.Button_LensGripperControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_LensGripperControl.Location = new System.Drawing.Point(140, 61);
            this.Button_LensGripperControl.Name = "Button_LensGripperControl";
            this.Button_LensGripperControl.Size = new System.Drawing.Size(120, 30);
            this.Button_LensGripperControl.TabIndex = 10;
            this.Button_LensGripperControl.Text = "Lens夹爪张开";
            this.Button_LensGripperControl.UseVisualStyleBackColor = true;
            this.Button_LensGripperControl.Click += new System.EventHandler(this.Button_LensGripperControl_Click);
            // 
            // Button_BoxGripperControl
            // 
            this.Button_BoxGripperControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_BoxGripperControl.Location = new System.Drawing.Point(10, 61);
            this.Button_BoxGripperControl.Name = "Button_BoxGripperControl";
            this.Button_BoxGripperControl.Size = new System.Drawing.Size(120, 30);
            this.Button_BoxGripperControl.TabIndex = 9;
            this.Button_BoxGripperControl.Text = "Box夹爪张开";
            this.Button_BoxGripperControl.UseVisualStyleBackColor = true;
            this.Button_BoxGripperControl.Click += new System.EventHandler(this.Button_BoxGripperControl_Click);
            // 
            // Button_PogoPinControl
            // 
            this.Button_PogoPinControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_PogoPinControl.Location = new System.Drawing.Point(10, 25);
            this.Button_PogoPinControl.Name = "Button_PogoPinControl";
            this.Button_PogoPinControl.Size = new System.Drawing.Size(120, 30);
            this.Button_PogoPinControl.TabIndex = 8;
            this.Button_PogoPinControl.Text = "产品加电";
            this.Button_PogoPinControl.UseVisualStyleBackColor = true;
            this.Button_PogoPinControl.Click += new System.EventHandler(this.Button_PogoPinControl_Click);
            // 
            // Button_EpoxyTest
            // 
            this.Button_EpoxyTest.BackColor = System.Drawing.Color.Yellow;
            this.Button_EpoxyTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_EpoxyTest.Location = new System.Drawing.Point(1139, 557);
            this.Button_EpoxyTest.Name = "Button_EpoxyTest";
            this.Button_EpoxyTest.Size = new System.Drawing.Size(100, 30);
            this.Button_EpoxyTest.TabIndex = 14;
            this.Button_EpoxyTest.Text = "点胶测试";
            this.Button_EpoxyTest.UseVisualStyleBackColor = false;
            this.Button_EpoxyTest.Click += new System.EventHandler(this.Button_EpoxyTest_Click);
            // 
            // GroupBox_ChannelLaserOnOff
            // 
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.chkbox_Chl0Valid);
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.chkbox_Chl3Valid);
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.chkbox_Chl2Valid);
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.chkbox_Chl1Valid);
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.PictureBox_Channel3LaserOn);
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.PictureBox_Channel2LaserOn);
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.PictureBox_Channel1LaserOn);
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.PictureBox_Channel0LaserOn);
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.Button_Channel3LaserOnOff);
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.Button_Channel0LaserOnOff);
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.Button_Channel2LaserOnOff);
            this.GroupBox_ChannelLaserOnOff.Controls.Add(this.Button_Channel1LaserOnOff);
            this.GroupBox_ChannelLaserOnOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GroupBox_ChannelLaserOnOff.Location = new System.Drawing.Point(1014, 665);
            this.GroupBox_ChannelLaserOnOff.Name = "GroupBox_ChannelLaserOnOff";
            this.GroupBox_ChannelLaserOnOff.Size = new System.Drawing.Size(239, 172);
            this.GroupBox_ChannelLaserOnOff.TabIndex = 22;
            this.GroupBox_ChannelLaserOnOff.TabStop = false;
            this.GroupBox_ChannelLaserOnOff.Text = "通道控制";
            // 
            // chkbox_Chl0Valid
            // 
            this.chkbox_Chl0Valid.AutoSize = true;
            this.chkbox_Chl0Valid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_Chl0Valid.Location = new System.Drawing.Point(18, 28);
            this.chkbox_Chl0Valid.Name = "chkbox_Chl0Valid";
            this.chkbox_Chl0Valid.Size = new System.Drawing.Size(59, 17);
            this.chkbox_Chl0Valid.TabIndex = 75;
            this.chkbox_Chl0Valid.Text = "通道0";
            this.chkbox_Chl0Valid.UseVisualStyleBackColor = true;
            // 
            // chkbox_Chl3Valid
            // 
            this.chkbox_Chl3Valid.AutoSize = true;
            this.chkbox_Chl3Valid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_Chl3Valid.Location = new System.Drawing.Point(18, 133);
            this.chkbox_Chl3Valid.Name = "chkbox_Chl3Valid";
            this.chkbox_Chl3Valid.Size = new System.Drawing.Size(59, 17);
            this.chkbox_Chl3Valid.TabIndex = 74;
            this.chkbox_Chl3Valid.Text = "通道3";
            this.chkbox_Chl3Valid.UseVisualStyleBackColor = true;
            // 
            // chkbox_Chl2Valid
            // 
            this.chkbox_Chl2Valid.AutoSize = true;
            this.chkbox_Chl2Valid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_Chl2Valid.Location = new System.Drawing.Point(18, 98);
            this.chkbox_Chl2Valid.Name = "chkbox_Chl2Valid";
            this.chkbox_Chl2Valid.Size = new System.Drawing.Size(59, 17);
            this.chkbox_Chl2Valid.TabIndex = 73;
            this.chkbox_Chl2Valid.Text = "通道2";
            this.chkbox_Chl2Valid.UseVisualStyleBackColor = true;
            // 
            // chkbox_Chl1Valid
            // 
            this.chkbox_Chl1Valid.AutoSize = true;
            this.chkbox_Chl1Valid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbox_Chl1Valid.Location = new System.Drawing.Point(18, 64);
            this.chkbox_Chl1Valid.Name = "chkbox_Chl1Valid";
            this.chkbox_Chl1Valid.Size = new System.Drawing.Size(59, 17);
            this.chkbox_Chl1Valid.TabIndex = 72;
            this.chkbox_Chl1Valid.Text = "通道1";
            this.chkbox_Chl1Valid.UseVisualStyleBackColor = true;
            // 
            // pnl_ProductionOperatin
            // 
            this.pnl_ProductionOperatin.Controls.Add(this.Button_HomeAxes);
            this.pnl_ProductionOperatin.Controls.Add(this.Button_AxesGoSafetyPosition);
            this.pnl_ProductionOperatin.Controls.Add(this.Button_Stop);
            this.pnl_ProductionOperatin.Controls.Add(this.Button_Start);
            this.pnl_ProductionOperatin.Location = new System.Drawing.Point(989, 15);
            this.pnl_ProductionOperatin.Name = "pnl_ProductionOperatin";
            this.pnl_ProductionOperatin.Size = new System.Drawing.Size(270, 133);
            this.pnl_ProductionOperatin.TabIndex = 23;
            // 
            // Button_HomeAxes
            // 
            this.Button_HomeAxes.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_HomeAxes.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Button_HomeAxes.Location = new System.Drawing.Point(141, 69);
            this.Button_HomeAxes.Name = "Button_HomeAxes";
            this.Button_HomeAxes.Size = new System.Drawing.Size(123, 60);
            this.Button_HomeAxes.TabIndex = 24;
            this.Button_HomeAxes.Text = "轴复位";
            this.Button_HomeAxes.UseVisualStyleBackColor = true;
            this.Button_HomeAxes.Click += new System.EventHandler(this.Button_HomeAxes_Click);
            // 
            // Button_AxesGoSafetyPosition
            // 
            this.Button_AxesGoSafetyPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_AxesGoSafetyPosition.Location = new System.Drawing.Point(6, 69);
            this.Button_AxesGoSafetyPosition.Name = "Button_AxesGoSafetyPosition";
            this.Button_AxesGoSafetyPosition.Size = new System.Drawing.Size(123, 60);
            this.Button_AxesGoSafetyPosition.TabIndex = 23;
            this.Button_AxesGoSafetyPosition.Text = "回原点";
            this.Button_AxesGoSafetyPosition.UseVisualStyleBackColor = true;
            this.Button_AxesGoSafetyPosition.Click += new System.EventHandler(this.Button_AxesGoSafetyPosition_Click);
            // 
            // Button_Stop
            // 
            this.Button_Stop.BackColor = System.Drawing.Color.Red;
            this.Button_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Stop.ForeColor = System.Drawing.Color.Black;
            this.Button_Stop.Location = new System.Drawing.Point(141, 3);
            this.Button_Stop.Name = "Button_Stop";
            this.Button_Stop.Size = new System.Drawing.Size(123, 60);
            this.Button_Stop.TabIndex = 22;
            this.Button_Stop.Text = "停止";
            this.Button_Stop.UseVisualStyleBackColor = false;
            this.Button_Stop.Click += new System.EventHandler(this.Button_Stop_Click);
            // 
            // Button_Start
            // 
            this.Button_Start.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Button_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Start.ForeColor = System.Drawing.Color.Black;
            this.Button_Start.Location = new System.Drawing.Point(6, 3);
            this.Button_Start.Name = "Button_Start";
            this.Button_Start.Size = new System.Drawing.Size(123, 60);
            this.Button_Start.TabIndex = 21;
            this.Button_Start.Text = "开始";
            this.Button_Start.UseVisualStyleBackColor = false;
            this.Button_Start.Click += new System.EventHandler(this.Button_Start_Click);
            // 
            // GroupBox_Materials
            // 
            this.GroupBox_Materials.Controls.Add(this.gb_BoxTray);
            this.GroupBox_Materials.Controls.Add(this.gb_LensTray);
            this.GroupBox_Materials.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox_Materials.Location = new System.Drawing.Point(698, 261);
            this.GroupBox_Materials.Name = "GroupBox_Materials";
            this.GroupBox_Materials.Size = new System.Drawing.Size(552, 270);
            this.GroupBox_Materials.TabIndex = 24;
            this.GroupBox_Materials.TabStop = false;
            this.GroupBox_Materials.Text = "原材料";
            // 
            // gb_BoxTray
            // 
            this.gb_BoxTray.Controls.Add(this.pnl_BoxTray);
            this.gb_BoxTray.Controls.Add(this.CheckBox_AllBoxLoad);
            this.gb_BoxTray.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_BoxTray.Location = new System.Drawing.Point(14, 17);
            this.gb_BoxTray.Name = "gb_BoxTray";
            this.gb_BoxTray.Size = new System.Drawing.Size(258, 245);
            this.gb_BoxTray.TabIndex = 1;
            this.gb_BoxTray.TabStop = false;
            this.gb_BoxTray.Text = "Box 盘";
            // 
            // pnl_BoxTray
            // 
            this.pnl_BoxTray.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_BoxTray.Controls.Add(this.lab_Split1);
            this.pnl_BoxTray.Location = new System.Drawing.Point(6, 19);
            this.pnl_BoxTray.Name = "pnl_BoxTray";
            this.pnl_BoxTray.Size = new System.Drawing.Size(246, 219);
            this.pnl_BoxTray.TabIndex = 3;
            // 
            // lab_Split1
            // 
            this.lab_Split1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lab_Split1.Location = new System.Drawing.Point(119, 5);
            this.lab_Split1.Name = "lab_Split1";
            this.lab_Split1.Size = new System.Drawing.Size(5, 207);
            this.lab_Split1.TabIndex = 81;
            this.lab_Split1.Text = "                                       ";
            // 
            // CheckBox_AllBoxLoad
            // 
            this.CheckBox_AllBoxLoad.AutoSize = true;
            this.CheckBox_AllBoxLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBox_AllBoxLoad.Location = new System.Drawing.Point(170, 0);
            this.CheckBox_AllBoxLoad.Name = "CheckBox_AllBoxLoad";
            this.CheckBox_AllBoxLoad.Size = new System.Drawing.Size(80, 17);
            this.CheckBox_AllBoxLoad.TabIndex = 0;
            this.CheckBox_AllBoxLoad.Text = "全选(上料)";
            this.CheckBox_AllBoxLoad.UseVisualStyleBackColor = true;
            this.CheckBox_AllBoxLoad.Click += new System.EventHandler(this.CheckBox_AllBoxLoad_Click);
            // 
            // gb_LensTray
            // 
            this.gb_LensTray.Controls.Add(this.pnl_LensTray);
            this.gb_LensTray.Controls.Add(this.CheckBox_AllLensLoad);
            this.gb_LensTray.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_LensTray.Location = new System.Drawing.Point(279, 17);
            this.gb_LensTray.Name = "gb_LensTray";
            this.gb_LensTray.Size = new System.Drawing.Size(258, 245);
            this.gb_LensTray.TabIndex = 0;
            this.gb_LensTray.TabStop = false;
            this.gb_LensTray.Text = "Lens 盘";
            // 
            // pnl_LensTray
            // 
            this.pnl_LensTray.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_LensTray.Location = new System.Drawing.Point(7, 19);
            this.pnl_LensTray.Name = "pnl_LensTray";
            this.pnl_LensTray.Size = new System.Drawing.Size(244, 219);
            this.pnl_LensTray.TabIndex = 2;
            // 
            // CheckBox_AllLensLoad
            // 
            this.CheckBox_AllLensLoad.AutoSize = true;
            this.CheckBox_AllLensLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBox_AllLensLoad.Location = new System.Drawing.Point(199, 0);
            this.CheckBox_AllLensLoad.Name = "CheckBox_AllLensLoad";
            this.CheckBox_AllLensLoad.Size = new System.Drawing.Size(50, 17);
            this.CheckBox_AllLensLoad.TabIndex = 1;
            this.CheckBox_AllLensLoad.Text = "全选";
            this.CheckBox_AllLensLoad.UseVisualStyleBackColor = true;
            this.CheckBox_AllLensLoad.Click += new System.EventHandler(this.CheckBox_AllLensLoad_Click);
            // 
            // GroupBox_WorkFlow
            // 
            this.GroupBox_WorkFlow.Controls.Add(this.ComboBox_SelectedLensIndex);
            this.GroupBox_WorkFlow.Controls.Add(this.Label_SelectedLensIndex);
            this.GroupBox_WorkFlow.Controls.Add(this.ComboBox_SelectedBoxIndex);
            this.GroupBox_WorkFlow.Controls.Add(this.Label_SelectedBoxIndex);
            this.GroupBox_WorkFlow.Controls.Add(this.ListBox_Stepflow);
            this.GroupBox_WorkFlow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox_WorkFlow.Location = new System.Drawing.Point(698, 541);
            this.GroupBox_WorkFlow.Name = "GroupBox_WorkFlow";
            this.GroupBox_WorkFlow.Size = new System.Drawing.Size(305, 296);
            this.GroupBox_WorkFlow.TabIndex = 25;
            this.GroupBox_WorkFlow.TabStop = false;
            this.GroupBox_WorkFlow.Text = "工作流程";
            // 
            // ComboBox_SelectedLensIndex
            // 
            this.ComboBox_SelectedLensIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_SelectedLensIndex.FormattingEnabled = true;
            this.ComboBox_SelectedLensIndex.Location = new System.Drawing.Point(246, 13);
            this.ComboBox_SelectedLensIndex.MaxDropDownItems = 9;
            this.ComboBox_SelectedLensIndex.Name = "ComboBox_SelectedLensIndex";
            this.ComboBox_SelectedLensIndex.Size = new System.Drawing.Size(42, 21);
            this.ComboBox_SelectedLensIndex.TabIndex = 32;
            // 
            // Label_SelectedLensIndex
            // 
            this.Label_SelectedLensIndex.AutoSize = true;
            this.Label_SelectedLensIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_SelectedLensIndex.Location = new System.Drawing.Point(214, 17);
            this.Label_SelectedLensIndex.Name = "Label_SelectedLensIndex";
            this.Label_SelectedLensIndex.Size = new System.Drawing.Size(30, 13);
            this.Label_SelectedLensIndex.TabIndex = 31;
            this.Label_SelectedLensIndex.Text = "Lens";
            // 
            // ComboBox_SelectedBoxIndex
            // 
            this.ComboBox_SelectedBoxIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComboBox_SelectedBoxIndex.FormattingEnabled = true;
            this.ComboBox_SelectedBoxIndex.Location = new System.Drawing.Point(146, 13);
            this.ComboBox_SelectedBoxIndex.MaxDropDownItems = 9;
            this.ComboBox_SelectedBoxIndex.Name = "ComboBox_SelectedBoxIndex";
            this.ComboBox_SelectedBoxIndex.Size = new System.Drawing.Size(42, 21);
            this.ComboBox_SelectedBoxIndex.TabIndex = 30;
            // 
            // Label_SelectedBoxIndex
            // 
            this.Label_SelectedBoxIndex.AutoSize = true;
            this.Label_SelectedBoxIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_SelectedBoxIndex.Location = new System.Drawing.Point(119, 17);
            this.Label_SelectedBoxIndex.Name = "Label_SelectedBoxIndex";
            this.Label_SelectedBoxIndex.Size = new System.Drawing.Size(25, 13);
            this.Label_SelectedBoxIndex.TabIndex = 4;
            this.Label_SelectedBoxIndex.Text = "Box";
            // 
            // ListBox_Stepflow
            // 
            this.ListBox_Stepflow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ListBox_Stepflow.FormattingEnabled = true;
            this.ListBox_Stepflow.Location = new System.Drawing.Point(6, 39);
            this.ListBox_Stepflow.Name = "ListBox_Stepflow";
            this.ListBox_Stepflow.Size = new System.Drawing.Size(293, 251);
            this.ListBox_Stepflow.TabIndex = 2;
            this.ListBox_Stepflow.DoubleClick += new System.EventHandler(this.ListBox_StepFlow_DoubleClick);
            // 
            // GroupBox_SystemInfo
            // 
            this.GroupBox_SystemInfo.Controls.Add(this.Button_EmptySystemInfo);
            this.GroupBox_SystemInfo.Controls.Add(this.lb_SystemInfo);
            this.GroupBox_SystemInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox_SystemInfo.Location = new System.Drawing.Point(12, 518);
            this.GroupBox_SystemInfo.Name = "GroupBox_SystemInfo";
            this.GroupBox_SystemInfo.Size = new System.Drawing.Size(388, 319);
            this.GroupBox_SystemInfo.TabIndex = 26;
            this.GroupBox_SystemInfo.TabStop = false;
            this.GroupBox_SystemInfo.Text = "机台信息";
            // 
            // Button_EmptySystemInfo
            // 
            this.Button_EmptySystemInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Button_EmptySystemInfo.Location = new System.Drawing.Point(304, 274);
            this.Button_EmptySystemInfo.Name = "Button_EmptySystemInfo";
            this.Button_EmptySystemInfo.Size = new System.Drawing.Size(55, 30);
            this.Button_EmptySystemInfo.TabIndex = 9;
            this.Button_EmptySystemInfo.Text = "清空";
            this.Button_EmptySystemInfo.UseVisualStyleBackColor = true;
            this.Button_EmptySystemInfo.Click += new System.EventHandler(this.Button_EmptySystemInfo_Click);
            // 
            // lb_SystemInfo
            // 
            this.lb_SystemInfo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lb_SystemInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_SystemInfo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lb_SystemInfo.FormattingEnabled = true;
            this.lb_SystemInfo.HorizontalExtent = 500;
            this.lb_SystemInfo.HorizontalScrollbar = true;
            this.lb_SystemInfo.Location = new System.Drawing.Point(6, 16);
            this.lb_SystemInfo.Name = "lb_SystemInfo";
            this.lb_SystemInfo.Size = new System.Drawing.Size(376, 290);
            this.lb_SystemInfo.TabIndex = 3;
            this.lb_SystemInfo.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lb_SystemInfo_DrawItem);
            // 
            // GroupBox_ProductPrepare
            // 
            this.GroupBox_ProductPrepare.Controls.Add(this.cmb_ListProduct);
            this.GroupBox_ProductPrepare.Controls.Add(this.lab_SN);
            this.GroupBox_ProductPrepare.Controls.Add(this.lab_Product);
            this.GroupBox_ProductPrepare.Controls.Add(this.lab_EmpID);
            this.GroupBox_ProductPrepare.Controls.Add(this.tb_EmployeeID);
            this.GroupBox_ProductPrepare.Controls.Add(this.tb_ProductSN);
            this.GroupBox_ProductPrepare.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox_ProductPrepare.Location = new System.Drawing.Point(698, 12);
            this.GroupBox_ProductPrepare.Name = "GroupBox_ProductPrepare";
            this.GroupBox_ProductPrepare.Size = new System.Drawing.Size(266, 133);
            this.GroupBox_ProductPrepare.TabIndex = 27;
            this.GroupBox_ProductPrepare.TabStop = false;
            this.GroupBox_ProductPrepare.Text = "产品准备";
            // 
            // cmb_ListProduct
            // 
            this.cmb_ListProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ListProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_ListProduct.FormattingEnabled = true;
            this.cmb_ListProduct.Location = new System.Drawing.Point(68, 19);
            this.cmb_ListProduct.Name = "cmb_ListProduct";
            this.cmb_ListProduct.Size = new System.Drawing.Size(184, 26);
            this.cmb_ListProduct.TabIndex = 3;
            // 
            // lab_SN
            // 
            this.lab_SN.AutoSize = true;
            this.lab_SN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_SN.Location = new System.Drawing.Point(10, 96);
            this.lab_SN.Name = "lab_SN";
            this.lab_SN.Size = new System.Drawing.Size(56, 18);
            this.lab_SN.TabIndex = 6;
            this.lab_SN.Text = "序列号";
            // 
            // lab_Product
            // 
            this.lab_Product.AutoSize = true;
            this.lab_Product.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_Product.Location = new System.Drawing.Point(10, 23);
            this.lab_Product.Name = "lab_Product";
            this.lab_Product.Size = new System.Drawing.Size(55, 18);
            this.lab_Product.TabIndex = 4;
            this.lab_Product.Text = "产   品";
            // 
            // lab_EmpID
            // 
            this.lab_EmpID.AutoSize = true;
            this.lab_EmpID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_EmpID.Location = new System.Drawing.Point(10, 60);
            this.lab_EmpID.Name = "lab_EmpID";
            this.lab_EmpID.Size = new System.Drawing.Size(56, 18);
            this.lab_EmpID.TabIndex = 5;
            this.lab_EmpID.Text = "员工号";
            // 
            // tb_EmployeeID
            // 
            this.tb_EmployeeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_EmployeeID.Location = new System.Drawing.Point(68, 57);
            this.tb_EmployeeID.Name = "tb_EmployeeID";
            this.tb_EmployeeID.Size = new System.Drawing.Size(184, 24);
            this.tb_EmployeeID.TabIndex = 7;
            this.tb_EmployeeID.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_EmployeeID_KeyUp);
            // 
            // tb_ProductSN
            // 
            this.tb_ProductSN.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_ProductSN.Location = new System.Drawing.Point(68, 94);
            this.tb_ProductSN.Name = "tb_ProductSN";
            this.tb_ProductSN.Size = new System.Drawing.Size(184, 24);
            this.tb_ProductSN.TabIndex = 8;
            // 
            // Button_RemoveDC
            // 
            this.Button_RemoveDC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_RemoveDC.Location = new System.Drawing.Point(109, 59);
            this.Button_RemoveDC.Name = "Button_RemoveDC";
            this.Button_RemoveDC.Size = new System.Drawing.Size(73, 26);
            this.Button_RemoveDC.TabIndex = 17;
            this.Button_RemoveDC.Text = "删除";
            this.Button_RemoveDC.UseVisualStyleBackColor = true;
            this.Button_RemoveDC.Click += new System.EventHandler(this.Button_RemoveDC_Click);
            // 
            // lv_DCInfo
            // 
            this.lv_DCInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.col0,
            this.col1,
            this.col2});
            this.lv_DCInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv_DCInfo.HideSelection = false;
            this.lv_DCInfo.Location = new System.Drawing.Point(230, 15);
            this.lv_DCInfo.Name = "lv_DCInfo";
            this.lv_DCInfo.Size = new System.Drawing.Size(311, 78);
            this.lv_DCInfo.TabIndex = 16;
            this.lv_DCInfo.UseCompatibleStateImageBehavior = false;
            this.lv_DCInfo.View = System.Windows.Forms.View.Details;
            // 
            // col0
            // 
            this.col0.Text = "DC Num";
            this.col0.Width = 125;
            // 
            // col1
            // 
            this.col1.Text = "DC PN";
            this.col1.Width = 119;
            // 
            // col2
            // 
            this.col2.Text = "Time (Min)";
            this.col2.Width = 62;
            // 
            // lab_DCLot
            // 
            this.lab_DCLot.AutoSize = true;
            this.lab_DCLot.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_DCLot.Location = new System.Drawing.Point(9, 23);
            this.lab_DCLot.Name = "lab_DCLot";
            this.lab_DCLot.Size = new System.Drawing.Size(54, 16);
            this.lab_DCLot.TabIndex = 9;
            this.lab_DCLot.Text = "DC Lot";
            // 
            // tb_DCLot
            // 
            this.tb_DCLot.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_DCLot.Location = new System.Drawing.Point(68, 19);
            this.tb_DCLot.Name = "tb_DCLot";
            this.tb_DCLot.Size = new System.Drawing.Size(151, 24);
            this.tb_DCLot.TabIndex = 10;
            this.tb_DCLot.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_DCLot_KeyUp);
            // 
            // GroupBox_DCList
            // 
            this.GroupBox_DCList.Controls.Add(this.Button_RemoveDC);
            this.GroupBox_DCList.Controls.Add(this.lv_DCInfo);
            this.GroupBox_DCList.Controls.Add(this.lab_DCLot);
            this.GroupBox_DCList.Controls.Add(this.tb_DCLot);
            this.GroupBox_DCList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.GroupBox_DCList.Location = new System.Drawing.Point(698, 151);
            this.GroupBox_DCList.Name = "GroupBox_DCList";
            this.GroupBox_DCList.Size = new System.Drawing.Size(552, 104);
            this.GroupBox_DCList.TabIndex = 28;
            this.GroupBox_DCList.TabStop = false;
            this.GroupBox_DCList.Text = "DC物料";
            // 
            // GroupBox_SystemStatus
            // 
            this.GroupBox_SystemStatus.Controls.Add(this.lab_Status);
            this.GroupBox_SystemStatus.Controls.Add(this.lab_EStop);
            this.GroupBox_SystemStatus.Controls.Add(this.lab_Home);
            this.GroupBox_SystemStatus.Controls.Add(this.lab_LedTower);
            this.GroupBox_SystemStatus.Controls.Add(this.lab_LedEStop);
            this.GroupBox_SystemStatus.Controls.Add(this.lab_LedHome);
            this.GroupBox_SystemStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GroupBox_SystemStatus.Location = new System.Drawing.Point(415, 519);
            this.GroupBox_SystemStatus.Name = "GroupBox_SystemStatus";
            this.GroupBox_SystemStatus.Size = new System.Drawing.Size(270, 56);
            this.GroupBox_SystemStatus.TabIndex = 29;
            this.GroupBox_SystemStatus.TabStop = false;
            this.GroupBox_SystemStatus.Text = "机台状态";
            // 
            // lab_Status
            // 
            this.lab_Status.AutoSize = true;
            this.lab_Status.Location = new System.Drawing.Point(213, 28);
            this.lab_Status.Name = "lab_Status";
            this.lab_Status.Size = new System.Drawing.Size(33, 13);
            this.lab_Status.TabIndex = 5;
            this.lab_Status.Text = "状态";
            // 
            // lab_EStop
            // 
            this.lab_EStop.AutoSize = true;
            this.lab_EStop.Location = new System.Drawing.Point(137, 29);
            this.lab_EStop.Name = "lab_EStop";
            this.lab_EStop.Size = new System.Drawing.Size(33, 13);
            this.lab_EStop.TabIndex = 4;
            this.lab_EStop.Text = "急停";
            // 
            // lab_Home
            // 
            this.lab_Home.AutoSize = true;
            this.lab_Home.Location = new System.Drawing.Point(42, 29);
            this.lab_Home.Name = "lab_Home";
            this.lab_Home.Size = new System.Drawing.Size(46, 13);
            this.lab_Home.TabIndex = 3;
            this.lab_Home.Text = "轴复位";
            // 
            // lab_LedTower
            // 
            this.lab_LedTower.AutoSize = true;
            this.lab_LedTower.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_LedTower.ForeColor = System.Drawing.Color.Gray;
            this.lab_LedTower.Location = new System.Drawing.Point(185, 22);
            this.lab_LedTower.Name = "lab_LedTower";
            this.lab_LedTower.Size = new System.Drawing.Size(34, 25);
            this.lab_LedTower.TabIndex = 2;
            this.lab_LedTower.Text = "🛑";
            // 
            // lab_LedEStop
            // 
            this.lab_LedEStop.AutoSize = true;
            this.lab_LedEStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_LedEStop.ForeColor = System.Drawing.Color.Gray;
            this.lab_LedEStop.Location = new System.Drawing.Point(108, 22);
            this.lab_LedEStop.Name = "lab_LedEStop";
            this.lab_LedEStop.Size = new System.Drawing.Size(34, 25);
            this.lab_LedEStop.TabIndex = 1;
            this.lab_LedEStop.Text = "🛑";
            // 
            // lab_LedHome
            // 
            this.lab_LedHome.AutoSize = true;
            this.lab_LedHome.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_LedHome.ForeColor = System.Drawing.Color.Gray;
            this.lab_LedHome.Location = new System.Drawing.Point(13, 22);
            this.lab_LedHome.Name = "lab_LedHome";
            this.lab_LedHome.Size = new System.Drawing.Size(34, 25);
            this.lab_LedHome.TabIndex = 0;
            this.lab_LedHome.Text = "🛑";
            // 
            // CheckBox_ShowPressureForce
            // 
            this.CheckBox_ShowPressureForce.AutoSize = true;
            this.CheckBox_ShowPressureForce.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CheckBox_ShowPressureForce.Location = new System.Drawing.Point(1032, 539);
            this.CheckBox_ShowPressureForce.Name = "CheckBox_ShowPressureForce";
            this.CheckBox_ShowPressureForce.Size = new System.Drawing.Size(91, 17);
            this.CheckBox_ShowPressureForce.TabIndex = 30;
            this.CheckBox_ShowPressureForce.Text = "显示压力值";
            this.CheckBox_ShowPressureForce.UseVisualStyleBackColor = true;
            // 
            // timer_MainForm
            // 
            this.timer_MainForm.Interval = 500;
            this.timer_MainForm.Tick += new System.EventHandler(this.timer_MainForm_Tick);
            // 
            // DCTimer
            // 
            this.DCTimer.Interval = 60000;
            this.DCTimer.Tick += new System.EventHandler(this.DCTimer_Tick);
            // 
            // tb_pressureforce
            // 
            this.tb_pressureforce.BackColor = System.Drawing.Color.Black;
            this.tb_pressureforce.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_pressureforce.ForeColor = System.Drawing.Color.LightGreen;
            this.tb_pressureforce.Location = new System.Drawing.Point(1030, 562);
            this.tb_pressureforce.Name = "tb_pressureforce";
            this.tb_pressureforce.ReadOnly = true;
            this.tb_pressureforce.Size = new System.Drawing.Size(59, 24);
            this.tb_pressureforce.TabIndex = 31;
            // 
            // Label_PressureForceUnit
            // 
            this.Label_PressureForceUnit.AutoSize = true;
            this.Label_PressureForceUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_PressureForceUnit.Location = new System.Drawing.Point(1092, 567);
            this.Label_PressureForceUnit.Name = "Label_PressureForceUnit";
            this.Label_PressureForceUnit.Size = new System.Drawing.Size(13, 13);
            this.Label_PressureForceUnit.TabIndex = 32;
            this.Label_PressureForceUnit.Text = "g";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 859);
            this.Controls.Add(this.Label_PressureForceUnit);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tb_pressureforce);
            this.Controls.Add(this.CheckBox_ShowPressureForce);
            this.Controls.Add(this.GroupBox_SystemStatus);
            this.Controls.Add(this.GroupBox_DCList);
            this.Controls.Add(this.GroupBox_ProductPrepare);
            this.Controls.Add(this.GroupBox_SystemInfo);
            this.Controls.Add(this.Button_EpoxyTest);
            this.Controls.Add(this.GroupBox_WorkFlow);
            this.Controls.Add(this.GroupBox_Materials);
            this.Controls.Add(this.pnl_ProductionOperatin);
            this.Controls.Add(this.GroupBox_ChannelLaserOnOff);
            this.Controls.Add(this.GroupBox_ShortcutButton);
            this.Controls.Add(this.pnl_SourceMeter);
            this.Controls.Add(this.cogRecordDisplay1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Tag = "1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).EndInit();
            this.pnl_SourceMeter.ResumeLayout(false);
            this.pnl_SourceMeter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Channel3LaserOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Channel2LaserOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Channel1LaserOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Channel0LaserOn)).EndInit();
            this.GroupBox_ShortcutButton.ResumeLayout(false);
            this.GroupBox_ChannelLaserOnOff.ResumeLayout(false);
            this.GroupBox_ChannelLaserOnOff.PerformLayout();
            this.pnl_ProductionOperatin.ResumeLayout(false);
            this.GroupBox_Materials.ResumeLayout(false);
            this.gb_BoxTray.ResumeLayout(false);
            this.gb_BoxTray.PerformLayout();
            this.pnl_BoxTray.ResumeLayout(false);
            this.gb_LensTray.ResumeLayout(false);
            this.gb_LensTray.PerformLayout();
            this.GroupBox_WorkFlow.ResumeLayout(false);
            this.GroupBox_WorkFlow.PerformLayout();
            this.GroupBox_SystemInfo.ResumeLayout(false);
            this.GroupBox_ProductPrepare.ResumeLayout(false);
            this.GroupBox_ProductPrepare.PerformLayout();
            this.GroupBox_DCList.ResumeLayout(false);
            this.GroupBox_DCList.PerformLayout();
            this.GroupBox_SystemStatus.ResumeLayout(false);
            this.GroupBox_SystemStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ConfigurationMenuItem;
        public Cognex.VisionPro.CogRecordDisplay cogRecordDisplay1;
        private System.Windows.Forms.ToolStripMenuItem OperationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_LoadEngineeringForm;
        private System.Windows.Forms.ToolStripMenuItem ModuleMenuItem;
        private System.Windows.Forms.ToolStripMenuItem motionConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_MotionSystemConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_AxisConfig;
        private System.Windows.Forms.ToolStripMenuItem iOConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_IOCardTypeConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_IOPortConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_CameraConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_SourceMeterConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_UVControllerConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_LightSourceConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_GripperControllerConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_SMCCardMotionConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_TECControllerConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_QRCodeScannerConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_CamstarConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_ForceSensorConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_ProductConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_SystemConfig;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_FI2CUSBDeviceConfig;
        private System.Windows.Forms.Panel pnl_SourceMeter;
        private System.Windows.Forms.TextBox tb_SourceMeterCurrent;
        private System.Windows.Forms.TextBox tb_SourceMeterVoltage;
        private System.Windows.Forms.Label lab_SourceMeter;
        private System.Windows.Forms.Button Button_Channel0LaserOnOff;
        private System.Windows.Forms.Button Button_Channel3LaserOnOff;
        private System.Windows.Forms.Button Button_Channel2LaserOnOff;
        private System.Windows.Forms.Button Button_Channel1LaserOnOff;
        private System.Windows.Forms.PictureBox PictureBox_Channel0LaserOn;
        private System.Windows.Forms.PictureBox PictureBox_Channel3LaserOn;
        private System.Windows.Forms.PictureBox PictureBox_Channel2LaserOn;
        private System.Windows.Forms.PictureBox PictureBox_Channel1LaserOn;
        private System.Windows.Forms.GroupBox GroupBox_ShortcutButton;
        private System.Windows.Forms.Button Button_RecognizeLaserSpotCenter;
        private System.Windows.Forms.Button Button_LensGripperControl;
        private System.Windows.Forms.Button Button_BoxGripperControl;
        private System.Windows.Forms.Button Button_PogoPinControl;
        private System.Windows.Forms.Button Button_LensDiscard;
        private System.Windows.Forms.Button Button_EpoxyTest;
        private System.Windows.Forms.Button Button_DispenserEpoxyPinOutOfBox;
        private System.Windows.Forms.Button Button_LensOutOfBox;
        private System.Windows.Forms.GroupBox GroupBox_ChannelLaserOnOff;
        private System.Windows.Forms.Panel pnl_ProductionOperatin;
        private System.Windows.Forms.Button Button_HomeAxes;
        private System.Windows.Forms.Button Button_AxesGoSafetyPosition;
        private System.Windows.Forms.Button Button_Stop;
        private System.Windows.Forms.Button Button_Start;
        private System.Windows.Forms.GroupBox GroupBox_Materials;
        private System.Windows.Forms.GroupBox gb_BoxTray;
        private System.Windows.Forms.Panel pnl_BoxTray;
        private System.Windows.Forms.CheckBox CheckBox_AllBoxLoad;
        private System.Windows.Forms.GroupBox gb_LensTray;
        private System.Windows.Forms.Panel pnl_LensTray;
        private System.Windows.Forms.CheckBox CheckBox_AllLensLoad;
        private System.Windows.Forms.Label lab_Split1;
        private System.Windows.Forms.GroupBox GroupBox_WorkFlow;
        private System.Windows.Forms.ListBox ListBox_Stepflow;
        private System.Windows.Forms.GroupBox GroupBox_SystemInfo;
        private System.Windows.Forms.ListBox lb_SystemInfo;
        private System.Windows.Forms.GroupBox GroupBox_ProductPrepare;
        private System.Windows.Forms.ComboBox cmb_ListProduct;
        private System.Windows.Forms.Label lab_SN;
        private System.Windows.Forms.Label lab_Product;
        private System.Windows.Forms.Label lab_EmpID;
        private System.Windows.Forms.TextBox tb_EmployeeID;
        private System.Windows.Forms.TextBox tb_ProductSN;
        private System.Windows.Forms.Button Button_RemoveDC;
        private System.Windows.Forms.ListView lv_DCInfo;
        private System.Windows.Forms.ColumnHeader col0;
        private System.Windows.Forms.ColumnHeader col1;
        private System.Windows.Forms.ColumnHeader col2;
        private System.Windows.Forms.Label lab_DCLot;
        private System.Windows.Forms.TextBox tb_DCLot;
        private System.Windows.Forms.GroupBox GroupBox_DCList;
        private System.Windows.Forms.GroupBox GroupBox_SystemStatus;
        private System.Windows.Forms.Label lab_Status;
        private System.Windows.Forms.Label lab_EStop;
        private System.Windows.Forms.Label lab_Home;
        private System.Windows.Forms.Label lab_LedTower;
        private System.Windows.Forms.Label lab_LedEStop;
        private System.Windows.Forms.Label lab_LedHome;
        private System.Windows.Forms.Label Label_SelectedBoxIndex;
        private System.Windows.Forms.ComboBox ComboBox_SelectedBoxIndex;
        private System.Windows.Forms.Button Button_Z1AxisSafetyUpMoving;
        private System.Windows.Forms.Button Button_EpoxyDipCylinderControl;
        private System.Windows.Forms.Button Button_BoxGripperCylinderControl;
        private System.Windows.Forms.Button Button_Z2AxisSafetyUpMoving;
        private System.Windows.Forms.Button Button_RecognizeBoxWindowCenter;
        private System.Windows.Forms.CheckBox CheckBox_ShowPressureForce;
        private System.Windows.Forms.ComboBox ComboBox_SelectedLensIndex;
        private System.Windows.Forms.Label Label_SelectedLensIndex;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_LoadEpoxyTestForm;
        private System.Windows.Forms.Timer timer_MainForm;
        private System.Windows.Forms.CheckBox chkbox_Chl0Valid;
        private System.Windows.Forms.CheckBox chkbox_Chl3Valid;
        private System.Windows.Forms.CheckBox chkbox_Chl2Valid;
        private System.Windows.Forms.CheckBox chkbox_Chl1Valid;
        private System.Windows.Forms.Timer DCTimer;
        private System.Windows.Forms.TextBox tb_pressureforce;
        private System.Windows.Forms.Label Label_PressureForceUnit;
        private System.Windows.Forms.ToolStripMenuItem DropDownMenuItem_VisionProcessConfig;
        private System.Windows.Forms.Button Button_EmptySystemInfo;
    }
}

