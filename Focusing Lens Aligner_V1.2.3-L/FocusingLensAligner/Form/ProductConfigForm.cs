using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace FocusingLensAligner
{
    public partial class ProductConfigForm : Form
    {
        //private MainForm mainForm = null;

        public ProductConfigForm()
        {
            InitializeComponent();        
        }

        private void ProductConfigForm_Load(object sender, EventArgs e)
        {
            SetEnum_WorkFlowList();
            LoadProcessConfig(GlobalParameters.productconfig);
            LoadBoxLensConfig(GlobalParameters.productconfig);
            LoadPositionConfig(GlobalParameters.productconfig);
            LoadDataLogConfig(GlobalParameters.productconfig);
        }

        private void ProductConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
        }

        #region Load Product Config

        public void SetEnum_WorkFlowList()
        {
            lb_Enum_WorkFlowList.Items.Clear();
            foreach (var temp in Enum.GetNames(typeof(Enum_WorkFlowList)))
            {
                lb_Enum_WorkFlowList.Items.Add(GeneralFunction.GetWorkflowName(temp));
            }
        }

        public void LoadProcessConfig(ProductConfig loadproductconfig)
        {
            switch (loadproductconfig.processConfig.powerway)
            {
                case Enum_PowerWay.VOLTAGE_I2C:
                    chkbox_VoltI2C.Checked = true;
                    chkbox_VoltFI2CUSB.Checked = false;
                    chkbox_VoltIO.Checked = false;
                    chkbox_CurrentPower.Checked = false;
                    chkbox_DriverBoard.Checked = false;
                    chkbox_EUIBoard.Checked = false;
                    chkbox_RampPowerOn.Enabled = true;
                    tb_Vol.Text = loadproductconfig.processConfig.voltageval.ToString("f2");
                    tb_I2CCurrent.Text = loadproductconfig.processConfig.i2cval.ToString();
                    tb_Vol.Enabled = true;
                    tb_I2CCurrent.Enabled = true;
                    tb_IOCurrent.Enabled = false;
                    tb_Current.Enabled = false;
                    tb_Temp.Enabled = false;
                    break;

                case Enum_PowerWay.VOLTAGE_FI2CUSB:
                    chkbox_VoltI2C.Checked = false;
                    chkbox_VoltFI2CUSB.Checked = true;
                    chkbox_VoltIO.Checked = false;
                    chkbox_CurrentPower.Checked = false;
                    chkbox_DriverBoard.Checked = false;
                    chkbox_EUIBoard.Checked = false;
                    chkbox_RampPowerOn.Enabled = true;
                    tb_Vol.Text = loadproductconfig.processConfig.voltageval.ToString("f2");
                    tb_I2CCurrent.Text = loadproductconfig.processConfig.i2cval.ToString();
                    tb_Vol.Enabled = true;
                    tb_I2CCurrent.Enabled = true;
                    tb_IOCurrent.Enabled = false;
                    tb_Current.Enabled = false;
                    tb_Temp.Enabled = false;
                    break;

                case Enum_PowerWay.CURRENT_IO:
                    chkbox_VoltI2C.Checked = false;
                    chkbox_VoltFI2CUSB.Checked = false;
                    chkbox_VoltIO.Checked = true;
                    chkbox_CurrentPower.Checked = false;
                    chkbox_DriverBoard.Checked = false;
                    chkbox_EUIBoard.Checked = false;
                    chkbox_RampPowerOn.Checked = false;
                    chkbox_RampPowerOn.Enabled = false;
                    tb_Vol.Text = loadproductconfig.processConfig.voltageval.ToString("f2");
                    tb_IOCurrent.Text = loadproductconfig.processConfig.iocurrentval.ToString("f3");
                    tb_Vol.Enabled = true;
                    tb_I2CCurrent.Enabled = false;
                    tb_IOCurrent.Enabled = true;
                    tb_Current.Enabled = false;
                    tb_Temp.Enabled = false;
                    break;

                case Enum_PowerWay.CURRENT_NO:
                    chkbox_VoltI2C.Checked = false;
                    chkbox_VoltFI2CUSB.Checked = false;
                    chkbox_VoltIO.Checked = false;
                    chkbox_CurrentPower.Checked = true;
                    chkbox_DriverBoard.Checked = false;
                    chkbox_EUIBoard.Checked = false;
                    chkbox_RampPowerOn.Checked = false;
                    chkbox_RampPowerOn.Enabled = false;
                    tb_Vol.Text = loadproductconfig.processConfig.voltageval.ToString("f2");
                    tb_Current.Text = loadproductconfig.processConfig.currentval.ToString("f3");
                    tb_Vol.Enabled = true;
                    tb_I2CCurrent.Enabled = false;
                    tb_IOCurrent.Enabled = false;
                    tb_Current.Enabled = true;
                    tb_Temp.Enabled = false;
                    break;

                case Enum_PowerWay.DRIVERBOARD:
                    chkbox_VoltI2C.Checked = false;
                    chkbox_VoltFI2CUSB.Checked = false;
                    chkbox_VoltIO.Checked = false;
                    chkbox_CurrentPower.Checked = false;
                    chkbox_DriverBoard.Checked = true;
                    chkbox_EUIBoard.Checked = false;
                    chkbox_RampPowerOn.Checked = false;
                    chkbox_RampPowerOn.Enabled = false;
                    tb_Temp.Text = loadproductconfig.processConfig.temperatureval.ToString("f2");
                    tb_Vol.Enabled = false;
                    tb_I2CCurrent.Enabled = false;
                    tb_IOCurrent.Enabled = false;
                    tb_Current.Enabled = false;
                    tb_Temp.Enabled = true;
                    break;

                case Enum_PowerWay.EUIBOARD:
                    chkbox_VoltI2C.Checked = false;
                    chkbox_VoltFI2CUSB.Checked = false;
                    chkbox_VoltIO.Checked = false;
                    chkbox_CurrentPower.Checked = false;
                    chkbox_DriverBoard.Checked = false;
                    chkbox_EUIBoard.Checked = true;
                    chkbox_RampPowerOn.Enabled = true;
                    tb_Vol.Enabled = false;
                    tb_I2CCurrent.Enabled = false;
                    tb_IOCurrent.Enabled = false;
                    tb_Current.Enabled = false;
                    tb_Temp.Enabled = false;
                    break;
            }

            //Workflow Setting
            if (loadproductconfig.processConfig.workflowStepNum != 0)
            {
                lb_ChooseFlowList.Items.Clear();
                for (int i = 0; i < loadproductconfig.processConfig.workflowStepNum; i++)
                {
                    if (loadproductconfig.processConfig.workflowArray[i] != -1)
                    {
                        lb_ChooseFlowList.Items.Add(GeneralFunction.GetWorkflowName(Enum.GetName(typeof(Enum_WorkFlowList), loadproductconfig.processConfig.workflowArray[i])));
                    }
                }
                cmb_ThreadSelect.SelectedIndex = 0;
                string currThread = cmb_ThreadSelect.SelectedItem.ToString().Trim();
                int threadIndex = cmb_ThreadSelect.SelectedIndex;
                lb_ThreadFlowList.Items.Clear();
                switch (currThread)
                {
                    case "Thread1":
                        if (GlobalParameters.productconfig.processConfig.thread1.threadenale == true)
                        {
                            string[] th1Arr = loadproductconfig.processConfig.thread1.threadflow.Split(',');
                            for (int i = 0; i < th1Arr.Length; i++)
                            {
                                if (th1Arr[i] != "")
                                    lb_ThreadFlowList.Items.Add(GeneralFunction.GetWorkflowName(Enum.GetName(typeof(Enum_WorkFlowList), int.Parse(th1Arr[i]))));
                            }
                            chkbox_ThreadEnable.Checked = loadproductconfig.processConfig.thread1.threadenale;
                        }
                        break;
                    case "Thread2":
                        if (GlobalParameters.productconfig.processConfig.thread2.threadenale == true)
                        {
                            string[] th2Arr = loadproductconfig.processConfig.thread2.threadflow.Split(',');
                            for (int i = 0; i < th2Arr.Length; i++)
                            {
                                if (th2Arr[i] != "")
                                    lb_ThreadFlowList.Items.Add(GeneralFunction.GetWorkflowName(Enum.GetName(typeof(Enum_WorkFlowList), int.Parse(th2Arr[i]))));
                            }
                            chkbox_ThreadEnable.Checked = loadproductconfig.processConfig.thread2.threadenale;
                        }
                        break;
                    case "Thread3":
                        if (GlobalParameters.productconfig.processConfig.thread3.threadenale == true)
                        {
                            string[] th3Arr = loadproductconfig.processConfig.thread3.threadflow.Split(',');
                            for (int i = 0; i < th3Arr.Length; i++)
                            {
                                if (th3Arr[i] != "")
                                    lb_ThreadFlowList.Items.Add(GeneralFunction.GetWorkflowName(Enum.GetName(typeof(Enum_WorkFlowList), int.Parse(th3Arr[i]))));
                            }
                            chkbox_ThreadEnable.Checked = loadproductconfig.processConfig.thread3.threadenale;
                        }
                        break;
                }
            }

            //Source Meter Setting
            tb_CurrCompliance.Text = loadproductconfig.processConfig.currentCompliance.ToString("f2");
            chkbox_RampPowerOn.Checked = loadproductconfig.processConfig.rampPowerOnEnable;
            tb_RampSteps.Text = loadproductconfig.processConfig.rampSteps.ToString();
            tb_RampDelay.Text = loadproductconfig.processConfig.rampDelay.ToString("f2");

            //PogoPin Position Setting
            switch (loadproductconfig.processConfig.pogopinPosition)
            {
                case Enum_PogoPinPosition.SIDE_PIN:
                    chkbox_SidePin.Checked = true;
                    chkbox_TopPin.Checked = false;
                    chkbox_SideTopPin.Checked = false;
                    chkbox_PCBA_Pin.Checked = false;
                    break;
                case Enum_PogoPinPosition.TOP_PIN:
                    chkbox_SidePin.Checked = false;
                    chkbox_TopPin.Checked = true;
                    chkbox_SideTopPin.Checked = false;
                    chkbox_PCBA_Pin.Checked = false;
                    break;
                case Enum_PogoPinPosition.SIDETOP_PIN:
                    chkbox_SidePin.Checked = false;
                    chkbox_TopPin.Checked = false;
                    chkbox_SideTopPin.Checked = true;
                    chkbox_PCBA_Pin.Checked = false;
                    break;
                case Enum_PogoPinPosition.PCBA_PIN:
                    chkbox_SidePin.Checked = false;
                    chkbox_TopPin.Checked = false;
                    chkbox_SideTopPin.Checked = false;
                    chkbox_PCBA_Pin.Checked = true;
                    break;
            }

            //PogoPin Lifetime Setting
            tb_PogoPinUsedCount.Text = loadproductconfig.processConfig.pogopinUsedCount.ToString();
            if (loadproductconfig.processConfig.pogopinLifetime == 0)
            {
                loadproductconfig.processConfig.pogopinLifetime = 5000;
            }
            tb_PogoPinLifetime.Text = loadproductconfig.processConfig.pogopinLifetime.ToString();

            //Epoxy Spot Setting
            tb_MinDia.Text = loadproductconfig.processConfig.minEpoxyDia.ToString("f2");
            tb_MaxDia.Text = loadproductconfig.processConfig.maxEpoxyDia.ToString("f2");
            tb_RemindTime.Text = loadproductconfig.processConfig.remindTime.ToString();
            chkbox_ManualPreDispensing.Checked = loadproductconfig.processConfig.manualPreDispensing;

            //UV Setting
            tb_UVPower.Text = loadproductconfig.processConfig.uvPower.ToString();
            tb_UVTime.Text = loadproductconfig.processConfig.uvTime.ToString();
            tb_UVCount.Text = loadproductconfig.processConfig.uvCount.ToString();

            //Alignment Way Setting
            switch (loadproductconfig.processConfig.alignmentWay)
            {
                case Enum_AlignmentWay.WINDOW_LENS:
                    chkbox_WindowToLensAlignment.Checked = true;
                    chkbox_LaserToLensAlignment.Checked = false;
                    chkbox_LaserToLaserAlignment.Checked = false;
                    break;

                case Enum_AlignmentWay.LASER_LENS:
                    chkbox_WindowToLensAlignment.Checked = false;
                    chkbox_LaserToLensAlignment.Checked = true;
                    chkbox_LaserToLaserAlignment.Checked = false;
                    break;

                case Enum_AlignmentWay.LASER_LASER:
                    chkbox_WindowToLensAlignment.Checked = false;
                    chkbox_LaserToLensAlignment.Checked = false;
                    chkbox_LaserToLaserAlignment.Checked = true;
                    break;
            }

            //Laser Intensity Setting
            tb_ChlCount.Text = loadproductconfig.processConfig.channelCount.ToString();
            tb_ChannelID0.Text = loadproductconfig.processConfig.laserIntensity[0].channelID.ToString();
            chkbox_CH0.Checked = loadproductconfig.processConfig.laserIntensity[0].outputEnable;
            tb_LaserCurrentDAC0.Text = loadproductconfig.processConfig.laserIntensity[0].currentDAC.ToString();
            tb_ChannelID1.Text = loadproductconfig.processConfig.laserIntensity[1].channelID.ToString();
            chkbox_CH1.Checked = loadproductconfig.processConfig.laserIntensity[1].outputEnable;
            tb_LaserCurrentDAC1.Text = loadproductconfig.processConfig.laserIntensity[1].currentDAC.ToString();
            tb_ChannelID2.Text = loadproductconfig.processConfig.laserIntensity[2].channelID.ToString();
            chkbox_CH2.Checked = loadproductconfig.processConfig.laserIntensity[2].outputEnable;
            tb_LaserCurrentDAC2.Text = loadproductconfig.processConfig.laserIntensity[2].currentDAC.ToString();
            tb_ChannelID3.Text = loadproductconfig.processConfig.laserIntensity[3].channelID.ToString();
            chkbox_CH3.Checked = loadproductconfig.processConfig.laserIntensity[3].outputEnable;
            tb_LaserCurrentDAC3.Text = loadproductconfig.processConfig.laserIntensity[3].currentDAC.ToString();
            tb_ChannelID4.Text = loadproductconfig.processConfig.laserIntensity[4].channelID.ToString();
            chkbox_CH4.Checked = loadproductconfig.processConfig.laserIntensity[4].outputEnable;
            tb_LaserCurrentDAC4.Text = loadproductconfig.processConfig.laserIntensity[4].currentDAC.ToString();
            tb_ChannelID5.Text = loadproductconfig.processConfig.laserIntensity[5].channelID.ToString();
            chkbox_CH5.Checked = loadproductconfig.processConfig.laserIntensity[5].outputEnable;
            tb_LaserCurrentDAC5.Text = loadproductconfig.processConfig.laserIntensity[5].currentDAC.ToString();
            tb_ChannelID6.Text = loadproductconfig.processConfig.laserIntensity[6].channelID.ToString();
            chkbox_CH6.Checked = loadproductconfig.processConfig.laserIntensity[6].outputEnable;
            tb_LaserCurrentDAC6.Text = loadproductconfig.processConfig.laserIntensity[6].currentDAC.ToString();
            tb_ChannelID7.Text = loadproductconfig.processConfig.laserIntensity[7].channelID.ToString();
            chkbox_CH7.Checked = loadproductconfig.processConfig.laserIntensity[7].outputEnable;
            tb_LaserCurrentDAC7.Text = loadproductconfig.processConfig.laserIntensity[7].currentDAC.ToString();

            //Laser Spot Position Balance Mode
            switch (loadproductconfig.processConfig.laserSpotPosBalanceMode)
            {
                case Enum_LaserSpotPosBalanceMode.MEAN:
                    chkbox_SpotMean.Checked = true;
                    chkbox_SpotMiddle.Checked = false;
                    break;

                case Enum_LaserSpotPosBalanceMode.MIDDLE:
                    chkbox_SpotMean.Checked = false;
                    chkbox_SpotMiddle.Checked = true;
                    break;
            }

            //DC Setting
            tb_DCCount.Text = loadproductconfig.processConfig.dcCount.ToString();

            //Press Lens Setting
            tb_PressLensSpeedPercent.Text = loadproductconfig.processConfig.pressLensSpeedPercent.ToString();
            tb_PressLensTime.Text = loadproductconfig.processConfig.pressLensTime.ToString();
            tb_TouchStep.Text = loadproductconfig.processConfig.pressLensTouchStep.ToString("f3");
            tb_TouchLensForce.Text = loadproductconfig.processConfig.pressLensTouchForce.ToString();
            tb_PressLensStep.Text = loadproductconfig.processConfig.pressLensStep.ToString("f3");
            tb_PressLensForce.Text = loadproductconfig.processConfig.pressLensForce.ToString();
            tb_ProtectStep.Text = loadproductconfig.processConfig.pressLensProtectStep.ToString("f3");
            tb_ProtectForce.Text = loadproductconfig.processConfig.pressLensProtectForce.ToString();
            tb_AlarmForce.Text = loadproductconfig.processConfig.pressLensAlarmForce.ToString();
            tb_EpoxyThickness.Text = loadproductconfig.processConfig.pressLensEpoxyThickness.ToString("f3");
            cmb_PressLensProtectMode.Items.Clear();
            foreach (string item in Enum.GetNames(typeof(Enum_PressLensProtectMode)))
            {
                cmb_PressLensProtectMode.Items.Add(item);
            }
            string protectmode = loadproductconfig.processConfig.pressLensProtectMode.ToString();
            cmb_PressLensProtectMode.SelectedIndex = Convert.ToInt16(Enum.Parse(typeof(Enum_PressLensProtectMode), protectmode));

            //Process Option
            chkbox_CheckLaserPositionBeforeLens.Checked = loadproductconfig.processConfig.checkLaserPositionBeforeLens2;
            chkbox_CompensateLaserWindowOffset.Checked = loadproductconfig.processConfig.compensateLaserWindowOffset;
            tb_LaserOffsetSpec1.Text = loadproductconfig.processConfig.laserSpec1.ToString("f3");
            tb_LaserOffsetSpec2.Text = loadproductconfig.processConfig.laserSpec2.ToString("f3");
            chkbox_CheckLaserPositionAfterLens.Checked = loadproductconfig.processConfig.checkLaserPositionAfterLens2;
            chkbox_RecordAllLaserOffsetBeforeLens.Checked = loadproductconfig.processConfig.recordAllLaserOffsetBeforeLens2;
            chkbox_RecordAllLaserOffsetAfterLens.Checked = loadproductconfig.processConfig.recordAllLaserOffsetAfterLens2;
            chkbox_ProcessDataUploadIntoDatabase.Checked = loadproductconfig.processConfig.processDataUploadIntoDatabase; 
            chkbox_MES.Checked = loadproductconfig.processConfig.mesEnable;
            chkbox_CheckSN.Checked = loadproductconfig.processConfig.checkSN;
            chkbox_MoveOut.Checked = loadproductconfig.processConfig.moveoutEnable;
            chkbox_ReadSN.Checked = loadproductconfig.processConfig.readSN;
            chkbox_AutoHold.Checked = loadproductconfig.processConfig.autoHold;
        }

        public void LoadBoxLensConfig(ProductConfig loadproductconfig)
        {
            #region Box Config

            //Pick Box Offset
            tb_PickBoxOffsetX.Text = loadproductconfig.boxlensConfig.pickBoxOffsetX.ToString("f2");
            tb_PickBoxOffsetY.Text = loadproductconfig.boxlensConfig.pickBoxOffsetY.ToString("f2");

            //Box Tray Config
            tb_BoxTrayRowCount.Text = loadproductconfig.boxlensConfig.boxTray.rowcount.ToString();
            tb_BoxTrayColCount.Text = loadproductconfig.boxlensConfig.boxTray.colcount.ToString();
            tb_BoxTrayRowSpace.Text = loadproductconfig.boxlensConfig.boxTray.rowspace.ToString("f2");
            tb_BoxTrayColSpace.Text = loadproductconfig.boxlensConfig.boxTray.colspace.ToString("f2");
            tb_BoxTrayMiddleSpace.Text = loadproductconfig.boxlensConfig.boxTrayMiddleSpace.ToString("f2");

            //Downward View Box Parameters
            tb_UpCameraViewBoxUpSpot.Text = loadproductconfig.boxlensConfig.UpCameraViewBoxUpSpot.ToString();
            tb_UpCameraViewBoxUpRing.Text = loadproductconfig.boxlensConfig.UpCameraViewBoxUpRing.ToString();
            tb_BoxFeaturePointPixelPosX.Text = string.Empty;
            tb_BoxFeaturePointPixelPosY.Text = string.Empty;
            chkbox_UpCameraViewAllBox.Checked = loadproductconfig.boxlensConfig.UpCameraViewAllBox;

            //Upward View Box Window Parameters
            tb_DownCameraViewWindowDnSpot.Text = loadproductconfig.boxlensConfig.DnCameraViewBoxWindowDnSpot.ToString();
            tb_DownCameraViewWindowDnRing.Text = loadproductconfig.boxlensConfig.DnCameraViewBoxWindowDnRing.ToString();
            tb_BoxWindowCentrePixelPosX.Text = string.Empty;
            tb_BoxWindowCentrePixelPosY.Text = string.Empty;
            tb_BoxWindowDiameter.Text = string.Empty;
            tb_BoxWindowCentreOffset.Text = string.Empty;
            tb_BoxWindowCentreOffsetLimit.Text = loadproductconfig.boxlensConfig.BoxWindowCentreOffsetLimit.ToString();
            chkbox_CheckBoxWindowCentreOffset.Checked = loadproductconfig.boxlensConfig.CheckBoxWindowCentreOffset;

            //Upward View Laser Spot Parameters
            tb_DownCameraViewLaserSpotMaxThreshold.Text = loadproductconfig.boxlensConfig.DnCameraViewLaserSpotMaxThreshold.ToString();
            tb_DownCameraViewLaserSpotMinThreshold.Text = loadproductconfig.boxlensConfig.DnCameraViewLaserSpotMinThreshold.ToString();
            tb_LaserSpotCentrePixelPosX.Text = string.Empty;
            tb_LaserSpotCentrePixelPosX.Text = string.Empty;

            //QR Code Scanner Test Result
            tb_TestSN.Text = string.Empty;

            #endregion

            #region Lens Config

            //Lens Tray Config
            tb_LensTrayRowCount.Text = loadproductconfig.boxlensConfig.lensTray.rowcount.ToString();
            tb_LensTrayColCount.Text = loadproductconfig.boxlensConfig.lensTray.colcount.ToString();
            tb_LensTrayRowSpace.Text = loadproductconfig.boxlensConfig.lensTray.rowspace.ToString("f2");
            tb_LensTrayColSpace.Text = loadproductconfig.boxlensConfig.lensTray.colspace.ToString("f2");

            //Downward View Lens Parameters
            tb_UpCameraViewLensUpSpot.Text = loadproductconfig.boxlensConfig.UpCameraViewLensUpSpot.ToString();
            tb_UpCameraViewLensUpRing.Text = loadproductconfig.boxlensConfig.UpCameraViewLensUpRing.ToString();
            tb_LensInTrayCentrePointPixelPosX.Text = string.Empty;
            tb_LensInTrayCentrePointPixelPosY.Text = string.Empty;
            chkbox_UpCameraViewAllLens.Checked = loadproductconfig.boxlensConfig.UpCameraViewAllLens;

            //Upward View Lens Parameters
            tb_DownCameraViewLensDnSpot.Text = loadproductconfig.boxlensConfig.DnCameraViewLensDnSpot.ToString();
            tb_DownCameraViewLensDnRing.Text = loadproductconfig.boxlensConfig.DnCameraViewLensDnRing.ToString();
            chkbox_DownCameraViewLensPatMatch.Checked = loadproductconfig.boxlensConfig.DnCameraViewLensPatMatch;
            tb_LensInBoxCentrePointPixelPosX.Text = string.Empty;
            tb_LensInBoxCentrePointPixelPosY.Text = string.Empty;

            //Upward View Epoxy Parameters
            tb_DownCameraViewEpoxyDnSpot.Text = loadproductconfig.boxlensConfig.DnCameraViewEpoxyDnSpot.ToString();
            tb_DownCameraViewEpoxyDnRing.Text = loadproductconfig.boxlensConfig.DnCameraViewEpoxyDnRing.ToString();

            #endregion

        }

        public void LoadPositionConfig(ProductConfig loadproductconfig)
        {
            #region Box Position

            //View First Box In Tray Position
            tb_ViewBoxPosX1.Text = loadproductconfig.positionConfig.UpCameraViewBoxInTrayPosition.X1.ToString("f3");
            tb_ViewBoxPosY1.Text = loadproductconfig.positionConfig.UpCameraViewBoxInTrayPosition.Y1.ToString("f3");
            tb_ViewBoxPosZ1.Text = loadproductconfig.positionConfig.UpCameraViewBoxInTrayPosition.Z1.ToString("f3");

            //Box Pick From & Place To Tray Z Height
            tb_PickBoxZ1.Text = loadproductconfig.positionConfig.pickBoxZ1Height.ToString("f3");

            //Box to Safe Position
            tb_BoxSafePosX1.Text = loadproductconfig.positionConfig.boxSafePosition.X1.ToString("f3");
            tb_BoxSafePosY1.Text = loadproductconfig.positionConfig.boxSafePosition.Y1.ToString("f3");
            tb_BoxSafePosZ1.Text = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_2.SafetyPosition.ToString("f3");
            tb_BoxSafePosU1.Text = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.U1.ToString("f3");            
            tb_BoxSafePosX2.Text = loadproductconfig.positionConfig.boxSafePosition.X2.ToString("f3");
            tb_BoxSafePosY2.Text = loadproductconfig.positionConfig.boxSafePosition.Y2.ToString("f3");
            tb_BoxSafePosZ2.Text = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_5.SafetyPosition.ToString("f3");

            //Box Place To Nest Position
            tb_PlaceBoxToNestPosX1.Text = loadproductconfig.positionConfig.boxInNestPosition.X1.ToString("f3");
            tb_PlaceBoxToNestPosY1.Text = loadproductconfig.positionConfig.boxInNestPosition.Y1.ToString("f3");
            tb_PlaceBoxToNestPosZ1.Text = loadproductconfig.positionConfig.boxInNestPosition.Z1.ToString("f3");
            tb_PlaceBoxToNestSpeedPercent.Text = loadproductconfig.positionConfig.boxIntoNestSpeedPercent.ToString();

            //Box Pick From Nest Position
            tb_PickBoxFromNestPosX1.Text = loadproductconfig.positionConfig.pickBoxFromNestPosition.X1.ToString("f3");
            tb_PickBoxFromNestPosY1.Text = loadproductconfig.positionConfig.pickBoxFromNestPosition.Y1.ToString("f3");
            tb_PickBoxFromNestPosZ1.Text = loadproductconfig.positionConfig.pickBoxFromNestPosition.Z1.ToString("f3");

            //Place Box to Scanner Pos
            tb_BoxScanPosX1.Text = loadproductconfig.positionConfig.boxScanPosition.X1.ToString("f3");
            tb_BoxScanPosY1.Text = loadproductconfig.positionConfig.boxScanPosition.Y1.ToString("f3");
            tb_BoxScanPosZ1.Text = loadproductconfig.positionConfig.boxScanPosition.Z1.ToString("f3");

            #endregion

            #region Lens Position

            //View First Lens in Tray Position
            tb_ViewLensPosX1.Text = loadproductconfig.positionConfig.UpCameraViewLensInTrayPosition.X1.ToString("f3");
            tb_ViewLensPosY1.Text = loadproductconfig.positionConfig.UpCameraViewLensInTrayPosition.Y1.ToString("f3");
            tb_ViewLensPosZ1.Text = loadproductconfig.positionConfig.UpCameraViewLensInTrayPosition.Z1.ToString("f3");

            //Lens Pick From & Place To Tray Z Height
            tb_PickLensZ2.Text = loadproductconfig.positionConfig.pickLensZ2Height.ToString("f3");

            //Lens To Safe Position
            tb_LensSafePosX2.Text = loadproductconfig.positionConfig.lensSafePosition.X2.ToString("f3");
            tb_LensSafePosY2.Text = loadproductconfig.positionConfig.lensSafePosition.Y2.ToString("f3");
            tb_LensSafePosZ2.Text = loadproductconfig.positionConfig.lensSafePosition.Z2.ToString("f3");

            //First Step Lens Into Box Position Offset
            tb_Step1LensIntoBoxOffsetX2.Text = loadproductconfig.positionConfig.step1LensPlaceOffsetX.ToString("f3");
            tb_Step1LensIntoBoxOffsetY2.Text = loadproductconfig.positionConfig.step1LensPlaceOffsetY.ToString("f3");

            //Second Step Lens Into Box Position Offset            
            tb_Step2LensIntoBoxOffsetX2.Text = loadproductconfig.positionConfig.step2LensPlaceOffsetX.ToString("f3");
            tb_Step2LensIntoBoxOffsetY2.Text = loadproductconfig.positionConfig.step2LensPlaceOffsetY.ToString("f3");
            tb_Step2LensIntoBoxOffsetZ2.Text = loadproductconfig.positionConfig.step2LensPlaceOffsetZ.ToString("f3");

            //Final Step Lens Into Box Position Offset
            tb_FinalLensIntoBoxOffsetX2.Text = loadproductconfig.positionConfig.finalLensPlaceOffsetX.ToString("f3");
            tb_FinalLensIntoBoxOffsetY2.Text = loadproductconfig.positionConfig.finalLensPlaceOffsetY.ToString("f3");

            //Lens Place Into Box Protection
            tb_PressLensRelativeDistanceLimitZ2.Text = loadproductconfig.positionConfig.pressLensDistanceLimit.ToString("f3");
            tb_PlaceLensIntoBoxSpeedPercent.Text = loadproductconfig.positionConfig.moveLensIntoBoxSpeedPercent.ToString();

            //Lens Discard Position
            tb_CleanLensDiscardX2.Text = loadproductconfig.positionConfig.cleanLensDicardPosition.X2.ToString("f3");
            tb_CleanLensDiscardY2.Text = loadproductconfig.positionConfig.cleanLensDicardPosition.Y2.ToString("f3");
            tb_CleanLensDiscardZ2.Text = loadproductconfig.positionConfig.cleanLensDicardPosition.Z2.ToString("f3");
            tb_DirtyLensDiscardX2.Text = loadproductconfig.positionConfig.dirtyLensDicardPosition.X2.ToString("f3");
            tb_DirtyLensDiscardY2.Text = loadproductconfig.positionConfig.dirtyLensDicardPosition.Y2.ToString("f3");
            tb_DirtyLensDiscardZ2.Text = loadproductconfig.positionConfig.dirtyLensDicardPosition.Z2.ToString("f3");

            #endregion

            #region Dispenser Position

            //Dispenser To Box Z Height
            tb_DispenserBoxZ1.Text = loadproductconfig.positionConfig.dispenserBoxZ1Height.ToString("f3");

            //Dispenser To Box Offset
            tb_DispenserToBoxOffsetX1.Text = loadproductconfig.positionConfig.dispenserToBoxOffsetX.ToString("f3");
            tb_DispenserToBoxOffsetY1.Text = loadproductconfig.positionConfig.dispenserToBoxOffsetY.ToString("f3");

            //Dispenser To Safe Position
            tb_DispenserSafePosX1.Text = loadproductconfig.positionConfig.dispenserSafePosition.X1.ToString("f3");
            tb_DispenserSafePosY1.Text = loadproductconfig.positionConfig.dispenserSafePosition.Y1.ToString("f3");
            tb_DispenserSafePosZ1.Text = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_2.SafetyPosition.ToString("f3");
            tb_DispenserSafePosU1.Text = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.U1.ToString("f3");
            tb_DispenserSafePosX2.Text = loadproductconfig.positionConfig.dispenserSafePosition.X2.ToString("f3");
            tb_DispenserSafePosY2.Text = loadproductconfig.positionConfig.dispenserSafePosition.Y2.ToString("f3");
            tb_DispenserSafePosZ2.Text = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_5.SafetyPosition.ToString("f3"); 

            //Dispenser Box Protection
            tb_DispenserToBoxSpeedPercent.Text = loadproductconfig.positionConfig.dispenserToBoxSpeedPercent.ToString();
            tb_DispenserBoxZ1Up.Text = loadproductconfig.positionConfig.dispenserBoxZ1Up.ToString("f3");

            #endregion

        }

        public void LoadDataLogConfig(ProductConfig loadproductconfig)
        {
            chkbox_DatabaseEnable.Checked = loadproductconfig.datalogConfig.databaseenable;
            tb_DataSource.Text = loadproductconfig.datalogConfig.database.datasource;
            tb_UserID.Text = loadproductconfig.datalogConfig.database.userid;
            tb_Password.Text = loadproductconfig.datalogConfig.database.password;
        }

        #endregion

        #region Save Product Config

        public void SaveProcessConfig(ref ProductConfig saveproductconfig)
        {
            foreach (Control c in gb_PowerConfig.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox chk = (c as CheckBox);
                    if (chk.Checked)
                    {
                        string powerway = chk.Tag.ToString();
                        saveproductconfig.processConfig.powerway = (Enum_PowerWay)Enum.Parse(typeof(Enum_PowerWay), powerway);
                        saveproductconfig.processConfig.voltageval = double.Parse(tb_Vol.Text);
                        saveproductconfig.processConfig.i2cval = int.Parse(tb_I2CCurrent.Text);
                        saveproductconfig.processConfig.currentval = double.Parse(tb_Current.Text);
                        break;
                    }
                }
            }

            //Workflow Setting
            int i = 0;
            string str = string.Empty;
            saveproductconfig.processConfig.workflowArray = Enumerable.Repeat(-1, saveproductconfig.processConfig.workflowArray.Length).ToArray();
            foreach (var temp in lb_ChooseFlowList.Items)
            {
                //str = GeneralFunction.GetWorkflowNum(temp.ToString());
                //int tmp = (int)Enum.Parse(typeof(Enum_WorkFlowList), str);
                int tmp = GeneralFunction.GetWorkflowNum(temp.ToString());
                saveproductconfig.processConfig.workflowArray[i] = tmp;
                i++;
            }
            saveproductconfig.processConfig.workflowStepNum = lb_ChooseFlowList.Items.Count;

            if (saveproductconfig.processConfig.workflowStepNum != 0)
            {
                string threadstr = "";
                foreach (var temp in lb_ThreadFlowList.Items)
                {
                    if (threadstr == "")
                    {
                        //threadstr = ((int)Enum.Parse(typeof(Enum_WorkFlowList), GeneralFunction.GetWorkflowNum(temp.ToString()))).ToString();
                        threadstr = GeneralFunction.GetWorkflowNum(temp.ToString()).ToString();
                    }
                    else
                    {
                        //threadstr = threadstr + "," + ((int)Enum.Parse(typeof(Enum_WorkFlowList), GeneralFunction.GetWorkflowNum(temp.ToString()))).ToString();
                        threadstr = threadstr + "," + GeneralFunction.GetWorkflowNum(temp.ToString()).ToString();
                    }

                }
                string currThread = cmb_ThreadSelect.SelectedItem.ToString().Trim();
                int threadIndex = cmb_ThreadSelect.SelectedIndex;

                switch (currThread)
                {
                    case "Thread1":
                        saveproductconfig.processConfig.thread1.threadflow = threadstr;
                        saveproductconfig.processConfig.thread1.threadenale = chkbox_ThreadEnable.Checked;
                        break;
                    case "Thread2":
                        saveproductconfig.processConfig.thread2.threadflow = threadstr;
                        saveproductconfig.processConfig.thread2.threadenale = chkbox_ThreadEnable.Checked;
                        break;
                    case "Thread3":
                        saveproductconfig.processConfig.thread3.threadflow = threadstr;
                        saveproductconfig.processConfig.thread3.threadenale = chkbox_ThreadEnable.Checked;
                        break;
                }
            }

            //Source Meter Setting
            saveproductconfig.processConfig.currentCompliance = double.Parse(tb_CurrCompliance.Text);
            saveproductconfig.processConfig.rampPowerOnEnable = chkbox_RampPowerOn.Checked;
            saveproductconfig.processConfig.rampSteps = int.Parse(tb_RampSteps.Text);
            saveproductconfig.processConfig.rampDelay = double.Parse(tb_RampDelay.Text);

            //PogoPin Position Setting
            foreach (CheckBox chkbox in gb_PogoPinPositionSetting.Controls)
            {
                if (chkbox.Checked)
                {
                    saveproductconfig.processConfig.pogopinPosition = (Enum_PogoPinPosition)Enum.Parse(typeof(Enum_PogoPinPosition), chkbox.Tag.ToString());
                    break;
                }
            }

            //PogoPin Lifetime Setting
            saveproductconfig.processConfig.pogopinUsedCount = int.Parse(tb_PogoPinUsedCount.Text);
            saveproductconfig.processConfig.pogopinLifetime = int.Parse(tb_PogoPinLifetime.Text);

            //Epoxy Spot Setting
            saveproductconfig.processConfig.minEpoxyDia = double.Parse(tb_MinDia.Text);
            saveproductconfig.processConfig.maxEpoxyDia = double.Parse(tb_MaxDia.Text);
            saveproductconfig.processConfig.remindTime = int.Parse(tb_RemindTime.Text);
            saveproductconfig.processConfig.manualPreDispensing = chkbox_ManualPreDispensing.Checked;

            //UV Setting
            saveproductconfig.processConfig.uvPower = int.Parse(tb_UVPower.Text);
            saveproductconfig.processConfig.uvTime = int.Parse(tb_UVTime.Text);
            saveproductconfig.processConfig.uvCount = int.Parse(tb_UVCount.Text);

            //Alignment Way Setting
            foreach (CheckBox chkbox in gb_AlignmentWaySetting.Controls)
            {
                if (chkbox.Checked)
                {
                    saveproductconfig.processConfig.alignmentWay = (Enum_AlignmentWay)Enum.Parse(typeof(Enum_AlignmentWay), chkbox.Tag.ToString());
                    break;
                }
            }

            //Laser Intensity Setting
            saveproductconfig.processConfig.channelCount = int.Parse(tb_ChlCount.Text);
            saveproductconfig.processConfig.laserIntensity[0].outputEnable = chkbox_CH0.Checked;
            saveproductconfig.processConfig.laserIntensity[0].channelID = int.Parse(tb_ChannelID0.Text);
            saveproductconfig.processConfig.laserIntensity[0].currentDAC = int.Parse(tb_LaserCurrentDAC0.Text);
            saveproductconfig.processConfig.laserIntensity[1].outputEnable = chkbox_CH1.Checked;
            saveproductconfig.processConfig.laserIntensity[1].channelID = int.Parse(tb_ChannelID1.Text);
            saveproductconfig.processConfig.laserIntensity[1].currentDAC = int.Parse(tb_LaserCurrentDAC1.Text);
            saveproductconfig.processConfig.laserIntensity[2].outputEnable = chkbox_CH2.Checked;
            saveproductconfig.processConfig.laserIntensity[2].channelID = int.Parse(tb_ChannelID2.Text);
            saveproductconfig.processConfig.laserIntensity[2].currentDAC = int.Parse(tb_LaserCurrentDAC2.Text);
            saveproductconfig.processConfig.laserIntensity[3].outputEnable = chkbox_CH3.Checked;
            saveproductconfig.processConfig.laserIntensity[3].channelID = int.Parse(tb_ChannelID3.Text);
            saveproductconfig.processConfig.laserIntensity[3].currentDAC = int.Parse(tb_LaserCurrentDAC3.Text);
            saveproductconfig.processConfig.laserIntensity[4].outputEnable = chkbox_CH4.Checked;
            saveproductconfig.processConfig.laserIntensity[4].channelID = int.Parse(tb_ChannelID4.Text);
            saveproductconfig.processConfig.laserIntensity[4].currentDAC = int.Parse(tb_LaserCurrentDAC4.Text);
            saveproductconfig.processConfig.laserIntensity[5].outputEnable = chkbox_CH5.Checked;
            saveproductconfig.processConfig.laserIntensity[5].channelID = int.Parse(tb_ChannelID5.Text);
            saveproductconfig.processConfig.laserIntensity[5].currentDAC = int.Parse(tb_LaserCurrentDAC5.Text);
            saveproductconfig.processConfig.laserIntensity[6].outputEnable = chkbox_CH6.Checked;
            saveproductconfig.processConfig.laserIntensity[6].channelID = int.Parse(tb_ChannelID6.Text);
            saveproductconfig.processConfig.laserIntensity[6].currentDAC = int.Parse(tb_LaserCurrentDAC6.Text);
            saveproductconfig.processConfig.laserIntensity[7].outputEnable = chkbox_CH7.Checked;
            saveproductconfig.processConfig.laserIntensity[7].channelID = int.Parse(tb_ChannelID7.Text);
            saveproductconfig.processConfig.laserIntensity[7].currentDAC = int.Parse(tb_LaserCurrentDAC7.Text);

            //Laser Spot Position Balance Mode Setting
            string spotBalanceMode = string.Empty;
            if (chkbox_SpotMean.Checked == true)
            {
                spotBalanceMode = "MEAN";
            }
            else
            {
                spotBalanceMode = "MIDDLE";
            }
            saveproductconfig.processConfig.laserSpotPosBalanceMode = (Enum_LaserSpotPosBalanceMode)Enum.Parse(typeof(Enum_LaserSpotPosBalanceMode), spotBalanceMode);


            //DC Setting
            saveproductconfig.processConfig.dcCount = int.Parse(tb_DCCount.Text);

            //Press Lens Setting
            saveproductconfig.processConfig.pressLensSpeedPercent = int.Parse(tb_PressLensSpeedPercent.Text);
            saveproductconfig.processConfig.pressLensTime = int.Parse(tb_PressLensTime.Text);
            saveproductconfig.processConfig.pressLensTouchStep = double.Parse(tb_TouchStep.Text);
            saveproductconfig.processConfig.pressLensTouchForce = int.Parse(tb_TouchLensForce.Text);
            saveproductconfig.processConfig.pressLensStep = double.Parse(tb_PressLensStep.Text);
            saveproductconfig.processConfig.pressLensForce = int.Parse(tb_PressLensForce.Text);
            saveproductconfig.processConfig.pressLensProtectStep = double.Parse(tb_ProtectStep.Text);
            saveproductconfig.processConfig.pressLensProtectForce = int.Parse(tb_ProtectForce.Text);
            saveproductconfig.processConfig.pressLensAlarmForce = int.Parse(tb_AlarmForce.Text);
            saveproductconfig.processConfig.pressLensEpoxyThickness = double.Parse(tb_EpoxyThickness.Text);
            string protectmode = cmb_PressLensProtectMode.SelectedItem.ToString().Trim();
            saveproductconfig.processConfig.pressLensProtectMode = (Enum_PressLensProtectMode)Enum.Parse(typeof(Enum_PressLensProtectMode), protectmode);
            
            //Process Option
            saveproductconfig.processConfig.checkLaserPositionBeforeLens2 = chkbox_CheckLaserPositionBeforeLens.Checked;
            saveproductconfig.processConfig.compensateLaserWindowOffset = chkbox_CompensateLaserWindowOffset.Checked;
            saveproductconfig.processConfig.laserSpec1 = double.Parse(tb_LaserOffsetSpec1.Text);
            saveproductconfig.processConfig.laserSpec2 = double.Parse(tb_LaserOffsetSpec2.Text);
            saveproductconfig.processConfig.checkLaserPositionAfterLens2 = chkbox_CheckLaserPositionAfterLens.Checked;
            saveproductconfig.processConfig.recordAllLaserOffsetBeforeLens2 = chkbox_RecordAllLaserOffsetBeforeLens.Checked;
            saveproductconfig.processConfig.recordAllLaserOffsetAfterLens2 = chkbox_RecordAllLaserOffsetAfterLens.Checked;
            saveproductconfig.processConfig.processDataUploadIntoDatabase = chkbox_ProcessDataUploadIntoDatabase.Checked;
            saveproductconfig.processConfig.mesEnable = chkbox_MES.Checked;
            saveproductconfig.processConfig.checkSN = chkbox_CheckSN.Checked;
            saveproductconfig.processConfig.moveoutEnable = chkbox_MoveOut.Checked;
            saveproductconfig.processConfig.readSN = chkbox_ReadSN.Checked;
            saveproductconfig.processConfig.autoHold = chkbox_AutoHold.Checked;
        }

        public void SaveBoxLensConfig(ref ProductConfig saveproductconfig)
        {
            #region Box Config

            //Pick Box Offset
            saveproductconfig.boxlensConfig.pickBoxOffsetX = double.Parse(tb_PickBoxOffsetX.Text);
            saveproductconfig.boxlensConfig.pickBoxOffsetY = double.Parse(tb_PickBoxOffsetY.Text);

            //Box Tray Config
            saveproductconfig.boxlensConfig.boxTray.rowcount = int.Parse(tb_BoxTrayRowCount.Text);
            saveproductconfig.boxlensConfig.boxTray.colcount = int.Parse(tb_BoxTrayColCount.Text);
            saveproductconfig.boxlensConfig.boxTray.rowspace = double.Parse(tb_BoxTrayRowSpace.Text);
            saveproductconfig.boxlensConfig.boxTray.colspace = double.Parse(tb_BoxTrayColSpace.Text);
            saveproductconfig.boxlensConfig.boxTrayMiddleSpace = double.Parse(tb_BoxTrayMiddleSpace.Text);

            //Downward View Box Parameters
            saveproductconfig.boxlensConfig.UpCameraViewBoxUpSpot = int.Parse(tb_UpCameraViewBoxUpSpot.Text);
            saveproductconfig.boxlensConfig.UpCameraViewBoxUpRing = int.Parse(tb_UpCameraViewBoxUpRing.Text);
            saveproductconfig.boxlensConfig.UpCameraViewAllBox = chkbox_UpCameraViewAllBox.Checked;
            
            //Upward View Box Window Parameters
            saveproductconfig.boxlensConfig.DnCameraViewBoxWindowDnSpot = int.Parse(tb_DownCameraViewWindowDnSpot.Text);
            saveproductconfig.boxlensConfig.DnCameraViewBoxWindowDnRing = int.Parse(tb_DownCameraViewWindowDnRing.Text);
            saveproductconfig.boxlensConfig.CheckBoxWindowCentreOffset = chkbox_CheckBoxWindowCentreOffset.Checked;
            saveproductconfig.boxlensConfig.BoxWindowCentreOffsetLimit = double.Parse(tb_BoxWindowCentreOffsetLimit.Text);

            //Upward View Laser Spot Parameters
            saveproductconfig.boxlensConfig.DnCameraViewLaserSpotMaxThreshold = int.Parse(tb_DownCameraViewLaserSpotMaxThreshold.Text);
            saveproductconfig.boxlensConfig.DnCameraViewLaserSpotMinThreshold = int.Parse(tb_DownCameraViewLaserSpotMinThreshold.Text);

            #endregion

            #region Lens Config

            //Lens Tray Config
            saveproductconfig.boxlensConfig.lensTray.rowcount = int.Parse(tb_LensTrayRowCount.Text);
            saveproductconfig.boxlensConfig.lensTray.colcount = int.Parse(tb_LensTrayColCount.Text);
            saveproductconfig.boxlensConfig.lensTray.rowspace = double.Parse(tb_LensTrayRowSpace.Text);
            saveproductconfig.boxlensConfig.lensTray.colspace = double.Parse(tb_LensTrayColSpace.Text);

            //Downward View Lens Parameters
            saveproductconfig.boxlensConfig.UpCameraViewLensUpSpot = int.Parse(tb_UpCameraViewLensUpSpot.Text);
            saveproductconfig.boxlensConfig.UpCameraViewLensUpRing = int.Parse(tb_UpCameraViewLensUpRing.Text);
            saveproductconfig.boxlensConfig.UpCameraViewAllLens = chkbox_UpCameraViewAllLens.Checked;

            //Upward View Lens Parameters
            saveproductconfig.boxlensConfig.DnCameraViewLensDnSpot = int.Parse(tb_DownCameraViewLensDnSpot.Text);
            saveproductconfig.boxlensConfig.DnCameraViewLensDnRing = int.Parse(tb_DownCameraViewLensDnRing.Text);
            saveproductconfig.boxlensConfig.DnCameraViewLensPatMatch = chkbox_DownCameraViewLensPatMatch.Checked;

            //Upward View Epoxy Parameters
            saveproductconfig.boxlensConfig.DnCameraViewEpoxyDnSpot = int.Parse(tb_DownCameraViewEpoxyDnSpot.Text);
            saveproductconfig.boxlensConfig.DnCameraViewEpoxyDnRing = int.Parse(tb_DownCameraViewEpoxyDnRing.Text);

            #endregion
        }

        public void SavePositionConfig(ref ProductConfig saveproductconfig)
        {
            #region Box Position

            //View First Box In Tray Position
            saveproductconfig.positionConfig.UpCameraViewBoxInTrayPosition.X1 = double.Parse(tb_ViewBoxPosX1.Text);
            saveproductconfig.positionConfig.UpCameraViewBoxInTrayPosition.Y1 = double.Parse(tb_ViewBoxPosY1.Text);
            saveproductconfig.positionConfig.UpCameraViewBoxInTrayPosition.Z1 = double.Parse(tb_ViewBoxPosZ1.Text);

            //Box Pick From & Place To Tray Z Height
            saveproductconfig.positionConfig.pickBoxZ1Height = double.Parse(tb_PickBoxZ1.Text);

            //Box to Safe Position
            saveproductconfig.positionConfig.boxSafePosition.X1 = double.Parse(tb_BoxSafePosX1.Text);
            saveproductconfig.positionConfig.boxSafePosition.Y1 = double.Parse(tb_BoxSafePosY1.Text);
            saveproductconfig.positionConfig.boxSafePosition.Z1 = double.Parse(tb_BoxSafePosZ1.Text);
            saveproductconfig.positionConfig.boxSafePosition.U1 = double.Parse(tb_BoxSafePosU1.Text);
            saveproductconfig.positionConfig.boxSafePosition.X2 = double.Parse(tb_BoxSafePosX2.Text);
            saveproductconfig.positionConfig.boxSafePosition.Y2 = double.Parse(tb_BoxSafePosY2.Text);
            saveproductconfig.positionConfig.boxSafePosition.Z2 = double.Parse(tb_BoxSafePosZ2.Text);

            //Box Place To Nest Position
            saveproductconfig.positionConfig.boxInNestPosition.X1 = double.Parse(tb_PlaceBoxToNestPosX1.Text);
            saveproductconfig.positionConfig.boxInNestPosition.Y1 = double.Parse(tb_PlaceBoxToNestPosY1.Text);
            saveproductconfig.positionConfig.boxInNestPosition.Z1 = double.Parse(tb_PlaceBoxToNestPosZ1.Text);
            saveproductconfig.positionConfig.boxIntoNestSpeedPercent = int.Parse(tb_PlaceBoxToNestSpeedPercent.Text);

            //Box Pick From Nest Position
            saveproductconfig.positionConfig.pickBoxFromNestPosition.X1 = double.Parse(tb_PickBoxFromNestPosX1.Text);
            saveproductconfig.positionConfig.pickBoxFromNestPosition.Y1 = double.Parse(tb_PickBoxFromNestPosY1.Text);
            saveproductconfig.positionConfig.pickBoxFromNestPosition.Z1 = double.Parse(tb_PickBoxFromNestPosZ1.Text);

            //Place Box to Scanner Pos
            saveproductconfig.positionConfig.boxScanPosition.X1 = double.Parse(tb_BoxScanPosX1.Text);
            saveproductconfig.positionConfig.boxScanPosition.Y1 = double.Parse(tb_BoxScanPosY1.Text);
            saveproductconfig.positionConfig.boxScanPosition.Z1 = double.Parse(tb_BoxScanPosZ1.Text);

            #endregion

            #region Lens Position

            //View First Lens in Tray Position
            saveproductconfig.positionConfig.UpCameraViewLensInTrayPosition.X1 = double.Parse(tb_ViewLensPosX1.Text);
            saveproductconfig.positionConfig.UpCameraViewLensInTrayPosition.Y1 = double.Parse(tb_ViewLensPosY1.Text);
            saveproductconfig.positionConfig.UpCameraViewLensInTrayPosition.Z1 = double.Parse(tb_ViewLensPosZ1.Text);

            //Lens Pick From & Place To Tray Z Height
            saveproductconfig.positionConfig.pickLensZ2Height = double.Parse(tb_PickLensZ2.Text);

            //Lens To Safe Position
            saveproductconfig.positionConfig.lensSafePosition.X2 = double.Parse(tb_LensSafePosX2.Text);
            saveproductconfig.positionConfig.lensSafePosition.Y2 = double.Parse(tb_LensSafePosY2.Text);
            saveproductconfig.positionConfig.lensSafePosition.Z2 = double.Parse(tb_LensSafePosZ2.Text);

            //First Step Lens Into Box Position Offset
            saveproductconfig.positionConfig.step1LensPlaceOffsetX = double.Parse(tb_Step1LensIntoBoxOffsetX2.Text);
            saveproductconfig.positionConfig.step1LensPlaceOffsetY = double.Parse(tb_Step1LensIntoBoxOffsetY2.Text);

            //Second Step Lens Into Box Position Offset            
            saveproductconfig.positionConfig.step2LensPlaceOffsetX = double.Parse(tb_Step2LensIntoBoxOffsetX2.Text);
            saveproductconfig.positionConfig.step2LensPlaceOffsetY = double.Parse(tb_Step2LensIntoBoxOffsetY2.Text);
            saveproductconfig.positionConfig.step2LensPlaceOffsetZ = double.Parse(tb_Step2LensIntoBoxOffsetZ2.Text);

            //Final Step Lens Into Box Position Offset
            saveproductconfig.positionConfig.finalLensPlaceOffsetX = double.Parse(tb_FinalLensIntoBoxOffsetX2.Text);
            saveproductconfig.positionConfig.finalLensPlaceOffsetY = double.Parse(tb_FinalLensIntoBoxOffsetY2.Text);

            //Lens Place Into Box Protection
            saveproductconfig.positionConfig.pressLensDistanceLimit = double.Parse(tb_PressLensRelativeDistanceLimitZ2.Text);
            saveproductconfig.positionConfig.moveLensIntoBoxSpeedPercent = int.Parse(tb_PlaceLensIntoBoxSpeedPercent.Text);

            //Lens Discard Position
            saveproductconfig.positionConfig.cleanLensDicardPosition.X2 = double.Parse(tb_CleanLensDiscardX2.Text);
            saveproductconfig.positionConfig.cleanLensDicardPosition.Y2 = double.Parse(tb_CleanLensDiscardY2.Text);
            //saveproductconfig.positionConfig.cleanLensDicardPosition.Z2 = double.Parse(tb_CleanLensDiscardZ2.Text);
            saveproductconfig.positionConfig.cleanLensDicardPosition.Z2 = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_5.SafetyPosition;
            saveproductconfig.positionConfig.dirtyLensDicardPosition.X2 = double.Parse(tb_DirtyLensDiscardX2.Text);
            saveproductconfig.positionConfig.dirtyLensDicardPosition.Y2 = double.Parse(tb_DirtyLensDiscardY2.Text);
            //saveproductconfig.positionConfig.dirtyLensDicardPosition.Z2 = double.Parse(tb_DirtyLensDiscardZ2.Text);
            saveproductconfig.positionConfig.dirtyLensDicardPosition.Z2 = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_5.SafetyPosition;

            #endregion

            #region Dispenser Position

            //Dispenser To Box Z Height
            saveproductconfig.positionConfig.dispenserBoxZ1Height = double.Parse(tb_DispenserBoxZ1.Text);

            //Dispenser To Box Offset
            saveproductconfig.positionConfig.dispenserToBoxOffsetX = double.Parse(tb_DispenserToBoxOffsetX1.Text);
            saveproductconfig.positionConfig.dispenserToBoxOffsetY = double.Parse(tb_DispenserToBoxOffsetY1.Text);

            //Dispenser To Safe Position
            saveproductconfig.positionConfig.dispenserSafePosition.X1 = double.Parse(tb_DispenserSafePosX1.Text);
            saveproductconfig.positionConfig.dispenserSafePosition.Y1 = double.Parse(tb_DispenserSafePosY1.Text);
            saveproductconfig.positionConfig.dispenserSafePosition.Z1 = double.Parse(tb_DispenserSafePosZ1.Text);
            saveproductconfig.positionConfig.dispenserSafePosition.U1 = double.Parse(tb_DispenserSafePosU1.Text);
            saveproductconfig.positionConfig.dispenserSafePosition.X2 = double.Parse(tb_DispenserSafePosX2.Text);
            saveproductconfig.positionConfig.dispenserSafePosition.Y2 = double.Parse(tb_DispenserSafePosY2.Text);
            saveproductconfig.positionConfig.dispenserSafePosition.Z2 = double.Parse(tb_DispenserSafePosZ2.Text);

            //Dispenser Box Protection
            saveproductconfig.positionConfig.dispenserToBoxSpeedPercent = int.Parse(tb_DispenserToBoxSpeedPercent.Text);
            saveproductconfig.positionConfig.dispenserBoxZ1Up = double.Parse(tb_DispenserBoxZ1Up.Text);

            #endregion
        }

        public void SaveDataLogConfig(ref ProductConfig saveproductconfig)
        {
            saveproductconfig.datalogConfig.databaseenable = chkbox_DatabaseEnable.Checked;
            saveproductconfig.datalogConfig.database.datasource = tb_DataSource.Text.Trim();
            saveproductconfig.datalogConfig.database.userid = tb_UserID.Text.Trim();
            saveproductconfig.datalogConfig.database.password = tb_Password.Text.Trim();
        }

        #endregion

        private void BtnSave_Click(object sender, EventArgs e)
        {
            bool res = false;
            string filepath = GeneralFunction.GetConfigFilePath("ProductConfig\\" + GlobalParameters.systemconfig.ManageProductConfig.currentproduct + ".xml");
            int id = TabProductConfig.SelectedIndex;
            switch (id)
            {
                case 0:
                    SaveProcessConfig(ref GlobalParameters.productconfig);
                    break;
                case 1:
                    SaveBoxLensConfig(ref GlobalParameters.productconfig);
                    break;
                case 2:
                    SavePositionConfig(ref GlobalParameters.productconfig);
                    break;
                case 3:
                    SaveDataLogConfig(ref GlobalParameters.productconfig);
                    break;
            }

            res = STD_IGeneralTool.GeneralTool.TryToSave<ProductConfig>(GlobalParameters.productconfig, filepath);

            if (!res)
            {
                MessageBox.Show("Save Product Config Fail.", "Fail");
            }
            else
            {
                MessageBox.Show("Save Product Config Success", "Success");
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CheckBox_Checked(object sender, EventArgs e)
        {
            if ((sender as CheckBox).Checked == true)
            {
                foreach (Control c in (sender as Control).Parent.Controls)
                {
                    if (c is CheckBox)
                    {
                        if (c != sender)
                        {
                            CheckBox chk = (c as CheckBox);
                            chk.Checked = false;
                        }
                    }
                }
            }
        }

        private void cmb_ThreadSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            string currThread = cmb_ThreadSelect.SelectedItem.ToString().Trim();
            int threadIndex = cmb_ThreadSelect.SelectedIndex;

            lb_ThreadFlowList.Items.Clear();
            switch (currThread)
            {
                case "Thread1":
                    if (GlobalParameters.productconfig.processConfig.thread1.threadenale == true)
                    {
                        string[] th1Arr = GlobalParameters.productconfig.processConfig.thread1.threadflow.Split(',');
                        for (int i = 0; i < th1Arr.Length; i++)
                        {
                            if (th1Arr[i] != "")
                                lb_ThreadFlowList.Items.Add(GeneralFunction.GetWorkflowName(Enum.GetName(typeof(Enum_WorkFlowList), int.Parse(th1Arr[i]))));
                        }
                        chkbox_ThreadEnable.Checked = GlobalParameters.productconfig.processConfig.thread1.threadenale;
                    }
                    break;
                case "Thread2":
                    if (GlobalParameters.productconfig.processConfig.thread2.threadenale == true)
                    {
                        string[] th2Arr = GlobalParameters.productconfig.processConfig.thread2.threadflow.Split(',');
                        for (int i = 0; i < th2Arr.Length; i++)
                        {
                            if (th2Arr[i] != "")
                                lb_ThreadFlowList.Items.Add(GeneralFunction.GetWorkflowName(Enum.GetName(typeof(Enum_WorkFlowList), int.Parse(th2Arr[i]))));
                        }
                        chkbox_ThreadEnable.Checked = GlobalParameters.productconfig.processConfig.thread2.threadenale;
                    }
                    break;
                case "Thread3":
                    if (GlobalParameters.productconfig.processConfig.thread3.threadenale == true)
                    {
                        string[] th3Arr = GlobalParameters.productconfig.processConfig.thread3.threadflow.Split(',');
                        for (int i = 0; i < th3Arr.Length; i++)
                        {
                            if (th3Arr[i] != "")
                                lb_ThreadFlowList.Items.Add(GeneralFunction.GetWorkflowName(Enum.GetName(typeof(Enum_WorkFlowList), int.Parse(th3Arr[i]))));
                        }
                        chkbox_ThreadEnable.Checked = GlobalParameters.productconfig.processConfig.thread3.threadenale;
                    }
                    break;
            }

        }

        private void btn_AddFlow_Click(object sender, EventArgs e)
        {
            if (chkbox_Thread.Checked)
            {
                if (lb_Enum_WorkFlowList.SelectedIndex != -1)
                {
                    string currentflow = lb_Enum_WorkFlowList.SelectedItem.ToString();

                    if (lb_ThreadFlowList.Items.Contains(currentflow))
                    {
                        DialogResult result = MessageBox.Show("Current workflow already exists.Do you want to go on?", "Warning", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            lb_ThreadFlowList.Items.Add(currentflow);
                        }
                    }
                    else
                    {
                        lb_ThreadFlowList.Items.Add(currentflow);
                    }
                }
            }
            else
            {
                if (lb_Enum_WorkFlowList.SelectedIndex != -1)
                {
                    string currentflow = lb_Enum_WorkFlowList.SelectedItem.ToString();

                    if (lb_ChooseFlowList.Items.Contains(currentflow))
                    {
                        DialogResult result = MessageBox.Show("Current workflow already exists.Do you want to go on?", "Warning", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            lb_ChooseFlowList.Items.Add(currentflow);
                        }
                    }
                    else
                    {
                        lb_ChooseFlowList.Items.Add(currentflow);
                    }
                }
            }
            
        }

        private void btn_RemoveFlow_Click(object sender, EventArgs e)
        {
            if (chkbox_Thread.Checked)
            {
                lb_ThreadFlowList.Items.Remove(lb_ThreadFlowList.SelectedItem);
            }
            else
            {
                lb_ChooseFlowList.Items.Remove(lb_ChooseFlowList.SelectedItem);
            }        
        }

        private void btn_ClearFlow_Click(object sender, EventArgs e)
        {
            lb_ChooseFlowList.Items.Clear();             
        }

        private void btn_MoveFlowUp_Click(object sender, EventArgs e)
        {
            if (lb_ChooseFlowList.SelectedIndex != -1 && lb_ChooseFlowList.SelectedIndex != 0)
            {
                int index = lb_ChooseFlowList.SelectedIndex;
                string preflow = lb_ChooseFlowList.Items[index - 1].ToString();
                lb_ChooseFlowList.Items.RemoveAt(index - 1);
                lb_ChooseFlowList.Items.Insert(index, preflow);
            }
        }

        private void btn_MoveFlowDown_Click(object sender, EventArgs e)
        {
            if (lb_ChooseFlowList.SelectedIndex != -1 && lb_ChooseFlowList.SelectedIndex != lb_ChooseFlowList.Items.Count - 1)
            {
                int index = lb_ChooseFlowList.SelectedIndex;
                string nextflow = lb_ChooseFlowList.Items[index + 1].ToString();
                lb_ChooseFlowList.Items.RemoveAt(index + 1);
                lb_ChooseFlowList.Items.Insert(index, nextflow);
            }
        }

        private void btn_ThreadMoveFlowUp_Click(object sender, EventArgs e)
        {
            if (lb_ThreadFlowList.SelectedIndex != -1 && lb_ThreadFlowList.SelectedIndex != 0)
            {
                int index = lb_ThreadFlowList.SelectedIndex;
                string preflow = lb_ThreadFlowList.Items[index - 1].ToString();
                lb_ThreadFlowList.Items.RemoveAt(index - 1);
                lb_ThreadFlowList.Items.Insert(index, preflow);
            }
        }

        private void btn_ThreadMoveFlowDown_Click(object sender, EventArgs e)
        {
            if (lb_ThreadFlowList.SelectedIndex != -1 && lb_ThreadFlowList.SelectedIndex != lb_ThreadFlowList.Items.Count - 1)
            {
                int index = lb_ThreadFlowList.SelectedIndex;
                string nextflow = lb_ThreadFlowList.Items[index + 1].ToString();
                lb_ThreadFlowList.Items.RemoveAt(index + 1);
                lb_ThreadFlowList.Items.Insert(index, nextflow);
            }
        }

        private void btn_ThreadClearFlow_Click(object sender, EventArgs e)
        {
            lb_ThreadFlowList.Items.Clear();
        }

        private void btn_ResetPogoPinUsedCount_Click(object sender, EventArgs e)
        {
            //加电Pin针累计使用次数清零
            tb_PogoPinUsedCount.Text = "0";
            tb_PogoPinUsedCount.Refresh();
        }

        private void btn_FindBoxInTray_Click(object sender, EventArgs e)
        {
            //上相机识别物料盘中产品盒子左上角位置(像素坐标)
            bool res = false;
            string str = string.Empty;
            VisionResult.BoxResult BoxResult = new VisionResult.BoxResult();
            double UpRingValue = double.Parse(tb_UpCameraViewBoxUpRing.Text);
            double UpSpotValue = double.Parse(tb_UpCameraViewBoxUpSpot.Text);

            res = GlobalFunction.ProcessFlow.VisionRecognizeBoxInTray(UpRingValue, UpSpotValue, ref BoxResult);
            if (res == false)
            {
                tb_BoxFeaturePointPixelPosX.Text = string.Empty;
                tb_BoxFeaturePointPixelPosY.Text = string.Empty;
                str = "上相机识别物料盘中产品Box失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误");
            }
            else
            {
                tb_BoxFeaturePointPixelPosX.Text = BoxResult.point.X.ToString("f3");
                tb_BoxFeaturePointPixelPosY.Text = BoxResult.point.Y.ToString("f3");
                GlobalFunction.updateStatusDelegate("上相机识别物料盘中产品Box成功", Enum_MachineStatus.NORMAL);
            }
        }

        private void btn_FindBoxWindow_Click(object sender, EventArgs e)
        {            
            //动态调整下相机工作条件
            bool res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.Window);
            if (res == false)
            {
                return;
            }

            //下相机识别产品盒子窗口中心位置(像素坐标)和窗口直径(像素尺寸)
            string str = string.Empty;
            double DnRingValue = double.Parse(tb_DownCameraViewWindowDnRing.Text);
            double DnSpotValue = double.Parse(tb_DownCameraViewWindowDnSpot.Text);

            VisionResult.BoxResult windowResult = new VisionResult.BoxResult();
            res = GlobalFunction.ProcessFlow.VisionRecognizeBoxWindow(DnRingValue, DnSpotValue, ref windowResult);
            if (res == false)
            {
                tb_BoxWindowCentrePixelPosX.Text = string.Empty;
                tb_BoxWindowCentrePixelPosY.Text = string.Empty;
                tb_BoxWindowDiameter.Text = string.Empty;
                tb_BoxWindowCentreOffset.Text = string.Empty;
                str = "下相机识别产品Box窗口中心失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误");
            }
            else
            {
                GlobalFunction.updateStatusDelegate("下相机识别产品Box窗口中心成功", Enum_MachineStatus.NORMAL);
                tb_BoxWindowCentrePixelPosX.Text = windowResult.point.X.ToString("f3");
                tb_BoxWindowCentrePixelPosY.Text = windowResult.point.Y.ToString("f3");
                double dia = windowResult.diameter * GlobalParameters.systemconfig.DownCameraCalibrateConfig.xscale;
                tb_BoxWindowDiameter.Text = dia.ToString("f3");
                str = "产品Box窗口直径: " + dia.ToString("f3") + "mm";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                double Offset_x = Math.Abs(windowResult.point.X - windowResult.imagecenter.X) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.xscale;
                double Offset_y = Math.Abs(windowResult.point.Y - windowResult.imagecenter.Y) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.yscale;
                double Offset_distance = Math.Sqrt(Math.Pow(Offset_x, 2) + Math.Pow(Offset_y, 2));
                tb_BoxWindowCentreOffset.Text = Offset_distance.ToString("f3");
                str = "产品Box窗口中心偏离下相机中心: " + Offset_distance.ToString("f3") + "mm";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
            }
        }

        private void btn_FindLaserSpot_Click(object sender, EventArgs e)
        {
            //动态调整下相机工作条件
            bool res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.LaserSpot);
            if (res == false)
            {
                return;
            }

            //下相机识别光斑中心位置(像素坐标)
            string str = string.Empty;
            VisionResult.LaserSpotResult laserSpotResult = new VisionResult.LaserSpotResult();
            double MaxThreshold = double.Parse(tb_DownCameraViewLaserSpotMaxThreshold.Text);
            double MinThreshold = double.Parse(tb_DownCameraViewLaserSpotMinThreshold.Text);

            res = GlobalFunction.ProcessFlow.VisionRecognizeLaserSpot(MinThreshold, MaxThreshold, ref laserSpotResult);
            if (res == false)
            {
                tb_LaserSpotCentrePixelPosX.Text = string.Empty;
                tb_LaserSpotCentrePixelPosY.Text = string.Empty;
                str = "下相机识别光斑中心失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误");
            }
            else
            {
                tb_LaserSpotCentrePixelPosX.Text = laserSpotResult.center.X.ToString("f3");
                tb_LaserSpotCentrePixelPosY.Text = laserSpotResult.center.Y.ToString("f3");
                GlobalFunction.updateStatusDelegate("下相机识别光斑中心成功", Enum_MachineStatus.NORMAL);
            }
        }

        private void btn_FindLensInTray_Click(object sender, EventArgs e)
        {
            //上相机识别物料盘中Lens中心位置(像素坐标)
            bool res = false;
            string str = string.Empty;
            VisionResult.LensResult lensResult = new VisionResult.LensResult();

            double UpRingValue = double.Parse(tb_UpCameraViewLensUpRing.Text);
            double UpSpotValue = double.Parse(tb_UpCameraViewLensUpSpot.Text);
            res = GlobalFunction.ProcessFlow.VisionRecognizeLensInTray(UpRingValue, UpSpotValue, ref lensResult);
            if (res == false)
            {
                tb_LensInTrayCentrePointPixelPosX.Text = string.Empty;
                tb_LensInTrayCentrePointPixelPosY.Text = string.Empty;
                str = "上相机识别物料盘中Lens失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误");
            }
            else
            {
                tb_LensInTrayCentrePointPixelPosX.Text = lensResult.center.X.ToString("f3");
                tb_LensInTrayCentrePointPixelPosY.Text = lensResult.center.Y.ToString("f3");
                GlobalFunction.updateStatusDelegate("上相机识别物料盘中Lens成功", Enum_MachineStatus.NORMAL);
            }
        }

        private void btn_FindLensInBox_Click(object sender, EventArgs e)
        {
            //动态调整下相机工作条件
            bool res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.Lens);
            if (res == false)
            {
                return;
            }

            //下相机识别产品盒子中Lens中心位置(像素坐标)
            string str = string.Empty;
            VisionResult.LensResult lensResult = new VisionResult.LensResult();
            double DnRingValue = double.Parse(tb_DownCameraViewLensDnRing.Text);
            double DnSpotValue = double.Parse(tb_DownCameraViewLensDnSpot.Text);

            res = GlobalFunction.ProcessFlow.VisionRecognizeLensInBox(DnRingValue, DnSpotValue, ref lensResult);
            if (res == false)
            {
                tb_LensInBoxCentrePointPixelPosX.Text = string.Empty;
                tb_LensInBoxCentrePointPixelPosY.Text = string.Empty;
                str = "下相机识别Lens中心失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误");
            }
            else
            {
                tb_LensInBoxCentrePointPixelPosX.Text = lensResult.center.X.ToString("f3");
                tb_LensInBoxCentrePointPixelPosY.Text = lensResult.center.Y.ToString("f3");
                GlobalFunction.updateStatusDelegate("下相机识别Lens中心成功", Enum_MachineStatus.NORMAL);
            }
        }

        private void btn_TestScanSN_Click(object sender, EventArgs e)
        {
            //测试扫码器
            int errorCode = 0;
            string QRCode = string.Empty;
            string str = string.Empty;

            if (GlobalParameters.systemconfig.InstrumentConfig.QRCodeScanner_Valid == true)
            {
                tb_TestSN.Text = string.Empty;
                bool res = GlobalFunction.QRCodeScannerTools.QRCodeScanner_ScanCode("DM70S", ref QRCode, ref errorCode);
                if (res == false)
                {
                    str = "扫码测试失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    MessageBox.Show(str, "错误");
                }
                else
                {
                    tb_TestSN.Text = QRCode;
                    GlobalFunction.updateStatusDelegate("扫码测试成功: " + QRCode, Enum_MachineStatus.NORMAL);
                }
            }
            else
            {
                str = "扫码器未初始化，扫码测试失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误");
            }
        }

        private void btn_LoadViewBoxPos_Click(object sender, EventArgs e)
        {
            tb_ViewBoxPosX1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_ViewBoxPosY1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_ViewBoxPosZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_MoveToViewBoxPos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            double posx = GlobalParameters.productconfig.positionConfig.UpCameraViewBoxInTrayPosition.X1;
            double posy = GlobalParameters.productconfig.positionConfig.UpCameraViewBoxInTrayPosition.Y1;
            double posz = GlobalParameters.productconfig.positionConfig.UpCameraViewBoxInTrayPosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误");
            }
        }

        private void btn_LoadPickBoxZ_Click(object sender, EventArgs e)
        {
            tb_PickBoxZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_MoveToPickBoxZ_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            double posx = GlobalParameters.productconfig.positionConfig.UpCameraViewBoxInTrayPosition.X1
                          + GlobalParameters.systemconfig.BoxGripperConfig.offsetx
                          + GlobalParameters.productconfig.boxlensConfig.pickBoxOffsetX;
            double posy = GlobalParameters.productconfig.positionConfig.UpCameraViewBoxInTrayPosition.Y1
                          + GlobalParameters.systemconfig.BoxGripperConfig.offsety 
                          + GlobalParameters.productconfig.boxlensConfig.pickBoxOffsetY;
            double posz = GlobalParameters.productconfig.positionConfig.pickBoxZ1Height;
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误");
            }
        }

        private void btn_LoadBoxSafePos_Click(object sender, EventArgs e)
        {
            tb_BoxSafePosX1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_BoxSafePosY1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_BoxSafePosZ1.Text = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_2.SafetyPosition.ToString("f3");
            tb_BoxSafePosU1.Text = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.U1.ToString("f3");
            tb_BoxSafePosX2.Text = GlobalFunction.MotionTools.Motion_GetX2Pos().ToString("f3");
            tb_BoxSafePosY2.Text = GlobalFunction.MotionTools.Motion_GetY2Pos().ToString("f3");
            tb_BoxSafePosZ2.Text = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_5.SafetyPosition.ToString("f3");
        }

        private void btn_MoveToBoxSafePos_Click(object sender, EventArgs e)
        {
            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;
            double posu = 0;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            //X1_Y1轴运动至远离产品Box的安全位置(包含Z1轴安全上抬)
            posx = GlobalParameters.productconfig.positionConfig.boxSafePosition.X1;
            posy = GlobalParameters.productconfig.positionConfig.boxSafePosition.Y1;            
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1ToLocation(posx, posy, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1轴组合运动失败", "错误");
                return;
            }

            //U1轴旋转至点胶角度
            posu = GlobalParameters.productconfig.positionConfig.boxSafePosition.U1;
            res = GlobalFunction.MotionTools.Motion_MoveU1ToLocation(posu, true);
            if (res == false)
            {
                MessageBox.Show("U1轴旋转失败", "错误");
                return;
            }

            //X2_Y2_Z2轴运动至远离产品Box的安全位置
            posx = GlobalParameters.productconfig.positionConfig.boxSafePosition.X2;
            posy = GlobalParameters.productconfig.positionConfig.boxSafePosition.Y2;
            posz = GlobalParameters.productconfig.positionConfig.boxSafePosition.Z2;
            res = GlobalFunction.MotionTools.Motion_MoveX2Y2Z2ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X2_Y2_Z2轴组合运动失败", "错误");
                return;
            }

            //Z1轴运动至远离产品Box的安全位置
            posz = GlobalParameters.productconfig.positionConfig.boxSafePosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                MessageBox.Show("Z1轴运动失败", "错误");
            }
        }

        private void btn_LoadBoxInNestPos_Click(object sender, EventArgs e)
        {
            tb_PlaceBoxToNestPosX1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_PlaceBoxToNestPosY1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_PlaceBoxToNestPosZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_MoveToBoxInNestPos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            //设置各轴速度百分比(一般放慢速度以保证安全和定位精度)
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", GlobalParameters.productconfig.positionConfig.boxIntoNestSpeedPercent);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y1", GlobalParameters.productconfig.positionConfig.boxIntoNestSpeedPercent);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z1", GlobalParameters.productconfig.positionConfig.boxIntoNestSpeedPercent);

            double posx = GlobalParameters.productconfig.positionConfig.boxInNestPosition.X1;
            double posy = GlobalParameters.productconfig.positionConfig.boxInNestPosition.Y1;
            double posz = GlobalParameters.productconfig.positionConfig.boxInNestPosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误");
            }
            
            //恢复各轴正常运动速度百分比
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", 100);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y1", 100);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z1", 100);
        }

        private void btn_LoadPickBoxFromNestPos_Click(object sender, EventArgs e)
        {
            tb_PickBoxFromNestPosX1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_PickBoxFromNestPosY1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_PickBoxFromNestPosZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_MoveToPickBoxFromNestPos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            //设置各轴速度百分比(一般放慢速度以保证安全和定位精度)
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", GlobalParameters.productconfig.positionConfig.boxIntoNestSpeedPercent);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y1", GlobalParameters.productconfig.positionConfig.boxIntoNestSpeedPercent);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z1", GlobalParameters.productconfig.positionConfig.boxIntoNestSpeedPercent);

            double posx = GlobalParameters.productconfig.positionConfig.pickBoxFromNestPosition.X1;
            double posy = GlobalParameters.productconfig.positionConfig.pickBoxFromNestPosition.Y1;
            double posz = GlobalParameters.productconfig.positionConfig.pickBoxFromNestPosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误");
            }
           
            //恢复各轴正常运动速度百分比
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", 100);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y1", 100);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z1", 100);
        }

        private void btn_LoadBoxScanPos_Click(object sender, EventArgs e)
        {
            tb_BoxScanPosX1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_BoxScanPosY1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_BoxScanPosZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_MoveToBoxScanPos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            double posx = GlobalParameters.productconfig.positionConfig.boxScanPosition.X1;
            double posy = GlobalParameters.productconfig.positionConfig.boxScanPosition.Y1;
            double posz = GlobalParameters.productconfig.positionConfig.boxScanPosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误");
            }
        }

        private void btn_LoadViewLensPos_Click(object sender, EventArgs e)
        {
            tb_ViewLensPosX1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_ViewLensPosY1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_ViewLensPosZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_MoveToViewLensPos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            double posx = GlobalParameters.productconfig.positionConfig.UpCameraViewLensInTrayPosition.X1;
            double posy = GlobalParameters.productconfig.positionConfig.UpCameraViewLensInTrayPosition.Y1;
            double posz = GlobalParameters.productconfig.positionConfig.UpCameraViewLensInTrayPosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误");
            }
        }

        private void btn_LoadPickLensZ_Click(object sender, EventArgs e)
        {
            tb_PickLensZ2.Text = GlobalFunction.MotionTools.Motion_GetZ2Pos().ToString("f3");
        }

        private void btn_MoveToPickLensZ_Click(object sender, EventArgs e)
        {
            bool res = false;
            int row = 0;
            int col = 0;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            double posx = GlobalParameters.systemconfig.LensGripperConfig.offsetx
                          - GlobalParameters.productconfig.positionConfig.UpCameraViewLensInTrayPosition.X1
                          - col * GlobalParameters.productconfig.boxlensConfig.lensTray.colspace;
            double posy = GlobalParameters.systemconfig.LensGripperConfig.offsety
                          - GlobalParameters.productconfig.positionConfig.UpCameraViewLensInTrayPosition.Y1
                          - row * GlobalParameters.productconfig.boxlensConfig.lensTray.rowspace;
            double posz = GlobalParameters.productconfig.positionConfig.pickLensZ2Height;
            res = GlobalFunction.MotionTools.Motion_MoveX2Y2Z2ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X2_Y2_Z2轴组合运动失败", "错误");
            }
        }

        private void btn_LoadLensSafePos_Click(object sender, EventArgs e)
        {
            tb_LensSafePosX2.Text = GlobalFunction.MotionTools.Motion_GetX2Pos().ToString("f3");
            tb_LensSafePosY2.Text = GlobalFunction.MotionTools.Motion_GetY2Pos().ToString("f3");
            tb_LensSafePosZ2.Text = GlobalFunction.MotionTools.Motion_GetZ2Pos().ToString("f3");
        }

        private void btn_MoveToLensSafePos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            double posx = GlobalParameters.productconfig.positionConfig.lensSafePosition.X2;
            double posy = GlobalParameters.productconfig.positionConfig.lensSafePosition.Y2;
            double posz = GlobalParameters.productconfig.positionConfig.lensSafePosition.Z2;
            res = GlobalFunction.MotionTools.Motion_MoveX2Y2Z2ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X2_Y2_Z2轴组合运动失败", "错误");
            }
        }

        private void btn_LoadDispenserBoxPosZ_Click(object sender, EventArgs e)
        {
            tb_DispenserBoxZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_MoveToDispenserBoxPosZ_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            if (MessageBox.Show("请确认Nest中已有产品Box，并且已被锁紧固定", "Warning", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            //动态调整下相机工作条件
            res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.Window);
            if (res == false)
            {
                MessageBox.Show("图像采集卡或下相机参数调整失败", "错误");
                return;
            }

            string str = string.Empty;
            VisionResult.BoxResult WindowResult = new VisionResult.BoxResult();
            double DnSpotValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewBoxWindowDnSpot;
            double DnRingValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewBoxWindowDnRing;
            res = GlobalFunction.ProcessFlow.VisionRecognizeBoxWindow(DnRingValue, DnSpotValue, ref WindowResult);
            if (res == false)
            {
                str = "下相机识别产品Box窗口中心失败";
                MessageBox.Show(str, "错误");
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                return;
            }

            //计算Box窗口中心与下相机中心的偏移距离:(图像像素中心坐标 - Box窗口中心) * 像素分辨率
            double dx = (WindowResult.imagecenter.X - WindowResult.point.X) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.xscale;
            double dy = (WindowResult.imagecenter.Y - WindowResult.point.Y) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.yscale;

            //计算Box窗口中心实际位置：下相机的中心位置 + Box窗口中心与下相机中心的偏移距离，注意下相机的图像与实际坐标系中的图像x方向上是镜像的
            double windowcentre_x = GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.X1 + dx;
            double windowcentre_y = GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.Y1 + dy;

            //计算胶针需要到达的位置：Box窗口中心实际位置 + 胶针与上相机中心偏差距离 + Offset设置
            double dispenserpos_x = windowcentre_x + GlobalParameters.systemconfig.DispenserConfig.offsetx + GlobalParameters.productconfig.positionConfig.dispenserToBoxOffsetX;
            double dispenserpos_y = windowcentre_y + GlobalParameters.systemconfig.DispenserConfig.offsety + GlobalParameters.productconfig.positionConfig.dispenserToBoxOffsetY;
            double dispenserpos_z = GlobalParameters.productconfig.positionConfig.dispenserBoxZ1Height;

            //各轴移动到点胶安全位
            res = GlobalFunction.ProcessFlow.DispenserMoveToSafePosition();
            if (res == false)
            {
                MessageBox.Show("胶针移动到点胶安全位置失败", "错误");
                return;
            }

            //胶针运动到安全移入产品Box的高度
            double dispenserpos_safez = dispenserpos_z - Math.Abs(GlobalParameters.productconfig.positionConfig.dispenserBoxZ1Up);
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("Z1", dispenserpos_safez, true);

            //胶针前后方向到位
            res = GlobalFunction.MotionTools.Motion_MoveY1ToLocation(dispenserpos_y, true);
            if (res == false)
            {
                MessageBox.Show("Y1轴运动失败", "错误");
                return;
            }

            //胶针下降
            res = GlobalFunction.IOControlTools.EpoxyDipUpDown(true, true);
            if (res == false)
            {
                MessageBox.Show("胶针气缸下降失败", "错误");
                return;
            }
            GeneralFunction.Delay(500);

            //设置X1和Z1轴速度百分比(一般放慢速度以保证安全和定位精度)
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", GlobalParameters.productconfig.positionConfig.dispenserToBoxSpeedPercent);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z1", GlobalParameters.productconfig.positionConfig.dispenserToBoxSpeedPercent);

            //X1轴运动至点胶位置(胶针按指定速度缓慢进入产品Box)
            res = GlobalFunction.MotionTools.Motion_MoveX1ToLocation(dispenserpos_x, true);
            if (res == false)
            {
                MessageBox.Show("X1轴运动失败", "错误");
            }
            else
            {
                //Z1轴下降至标定的点胶高度
                res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(dispenserpos_z, true);
                if (res == false)
                {
                    MessageBox.Show("Z1轴运动失败", "错误");
                }
            }

            //恢复X1和Z1轴速度百分比
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", 100);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z1", 100);
        }

        private void btn_LoadDispenserSafePos_Click(object sender, EventArgs e)
        {
            tb_DispenserSafePosX1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_DispenserSafePosY1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_DispenserSafePosZ1.Text = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_2.SafetyPosition.ToString("f3");
            tb_DispenserSafePosU1.Text = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.U1.ToString("f3");
            tb_DispenserSafePosX2.Text = GlobalFunction.MotionTools.Motion_GetX2Pos().ToString("f3");
            tb_DispenserSafePosY2.Text = GlobalFunction.MotionTools.Motion_GetY2Pos().ToString("f3");
            tb_DispenserSafePosZ2.Text = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_5.SafetyPosition.ToString("f3");
        }

        private void btn_MoveToDispenserSafePos_Click(object sender, EventArgs e)
        {           
            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;
            double posu = 0;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            //X1_Y1轴运动至远离产品Box的安全位置
            posx = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.X1;
            posy = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Y1;
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1ToLocation(posx, posy, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1轴组合运动失败", "错误");
                return;
            }

            //U1轴旋转至点胶角度
            posu = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.U1;
            res = GlobalFunction.MotionTools.Motion_MoveU1ToLocation(posu, true);
            if (res == false)
            {
                MessageBox.Show("U1轴旋转失败", "错误");
                return;
            }

            //X2_Y2_Z2轴运动至远离产品Box的安全位置
            posx = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.X2;
            posy = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Y2;
            posz = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Z2;
            res = GlobalFunction.MotionTools.Motion_MoveX2Y2Z2ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X2_Y2_Z2轴组合运动失败", "错误");
                return;
            }

            //Z1轴运动至远离产品Box的安全位置
            posz = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                MessageBox.Show("Z1轴运动失败", "错误");
            }
        }

        private void btn_LoadCleanLensDiscardPos_Click(object sender, EventArgs e)
        {
            tb_CleanLensDiscardX2.Text = GlobalFunction.MotionTools.Motion_GetX2Pos().ToString("f3");
            tb_CleanLensDiscardY2.Text = GlobalFunction.MotionTools.Motion_GetY2Pos().ToString("f3");
            //干净Lens抛料时Z2轴位置固定为系统标定的安全高度
            tb_CleanLensDiscardZ2.Text = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_5.SafetyPosition.ToString("f3");
        }

        private void btn_MoveToCleanLensDiscardPos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            //X2轴运动至抛料位置(先移动X2轴主要是让夹爪先移出产品Box，防止夹爪撞盒子壁)
            double posx = GlobalParameters.productconfig.positionConfig.cleanLensDicardPosition.X2;
            res = GlobalFunction.MotionTools.Motion_MoveX2ToLocation(posx, true);
            if (res == false)
            {
                MessageBox.Show("X2轴运动失败", "错误");
                return;
            }

            //Z2轴上抬至安全位置
            res = GlobalFunction.MotionTools.Motion_Z2UpToSafe();
            if (res == false)
            {
                MessageBox.Show("Z2轴运动失败", "错误");
                return;
            }

            //Y2轴运动至抛料位置
            double posy = GlobalParameters.productconfig.positionConfig.cleanLensDicardPosition.Y2;
            res = GlobalFunction.MotionTools.Motion_MoveY2ToLocation(posy, true);
            if (res == false)
            {
                MessageBox.Show("Y2轴运动失败", "错误");
            }
        }

        private void btn_LoadDirtyLensDiscardPos_Click(object sender, EventArgs e)
        {
            tb_DirtyLensDiscardX2.Text = GlobalFunction.MotionTools.Motion_GetX2Pos().ToString("f3");
            tb_DirtyLensDiscardY2.Text = GlobalFunction.MotionTools.Motion_GetY2Pos().ToString("f3");
            //脏污Lens抛料时Z2轴位置固定为系统标定的安全高度
            tb_DirtyLensDiscardZ2.Text = GlobalParameters.systemconfig.AxisSafetyPosConfig.Axis_5.SafetyPosition.ToString("f3");
        }

        private void btn_MoveToDirtyLensDiscardPos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                MessageBox.Show("Box夹爪气缸或胶针气缸上抬失败", "错误");
                return;
            }

            //X2轴运动至抛料位置(先移动X2轴主要是让夹爪先移出产品Box，防止夹爪撞盒子壁)
            double posx = GlobalParameters.productconfig.positionConfig.dirtyLensDicardPosition.X2;
            res = GlobalFunction.MotionTools.Motion_MoveX2ToLocation(posx, true);
            if (res == false)
            {
                MessageBox.Show("X2轴运动失败", "错误");
                return;
            }

            //Z2轴上抬至安全位置
            res = GlobalFunction.MotionTools.Motion_Z2UpToSafe();
            if (res == false)
            {
                MessageBox.Show("Z2轴运动失败", "错误");
                return;
            }

            //Y2轴运动至抛料位置
            double posy = GlobalParameters.productconfig.positionConfig.dirtyLensDicardPosition.Y2;
            res = GlobalFunction.MotionTools.Motion_MoveY2ToLocation(posy, true);
            if (res == false)
            {
                MessageBox.Show("Y2轴运动失败", "错误");
            }
        }

        private void tb_ConnectTest_Click(object sender, EventArgs e)
        {
            string dataSource = GlobalParameters.productconfig.datalogConfig.database.datasource;
            string userID = GlobalParameters.productconfig.datalogConfig.database.userid;
            string password = GlobalParameters.productconfig.datalogConfig.database.password;
            string connStr = "Data Source=" + dataSource + ";User ID=" + userID + ";PassWord=" + password;

            SqlConnection conn = new SqlConnection(connStr);
            conn.Open();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                MessageBox.Show("数据库连接测试通过", "提示");
            }
            else
            {
                MessageBox.Show("数据库连接测试失败", "错误");
            }
        }
    }
}
