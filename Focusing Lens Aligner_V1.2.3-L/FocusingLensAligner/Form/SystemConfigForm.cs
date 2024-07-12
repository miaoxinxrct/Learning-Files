using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace FocusingLensAligner
{
    public partial class SystemConfigForm : Form
    {
        AxisConfigCollection axisconfigs = new AxisConfigCollection();

        public SystemConfigForm()
        {
            InitializeComponent();
        }

        #region Load System Config 
        private void LoadInstrumentConfig(SystemConfig loadsystemconfig)
        {
            chkbox_Keithley2400_1.Checked = loadsystemconfig.InstrumentConfig.Keithley2401_1_Valid;            
            chkbox_Keithley2400_2.Checked = loadsystemconfig.InstrumentConfig.Keithley2401_2_Valid;            
            chkbox_Keithley2400_3.Checked = loadsystemconfig.InstrumentConfig.Keithley2401_3_Valid;
            chkbox_Keithley2400_4.Checked = loadsystemconfig.InstrumentConfig.Keithley2401_4_Valid;
            chkbox_Keithley2510_1.Checked = loadsystemconfig.InstrumentConfig.Keithley2510_1_Valid;
            chkbox_Keithley2510_2.Checked = loadsystemconfig.InstrumentConfig.Keithley2510_2_Valid;
            chkbox_KeySightE364X.Checked = loadsystemconfig.InstrumentConfig.KeySightE364x_Valid;
            chkbox_I2C.Checked = loadsystemconfig.InstrumentConfig.I2C_Valid;
            chkbox_FI2CUSB.Checked = loadsystemconfig.InstrumentConfig.FI2CUSB_Valid;
            chkbox_DriverBoard.Checked = loadsystemconfig.InstrumentConfig.DriverBoard_Valid;
            chkbox_ForceSensor.Checked = loadsystemconfig.InstrumentConfig.ForceSensor_Valid;
            chkbox_BoxGripper.Checked = loadsystemconfig.InstrumentConfig.IAIBoxGripper_Valid;
            chkbox_LensGripper.Checked = loadsystemconfig.InstrumentConfig.IAILensGripper_Valid;
            chkbox_UV.Checked = loadsystemconfig.InstrumentConfig.UVController_Valid;            
            chkbox_Scanner.Checked = loadsystemconfig.InstrumentConfig.QRCodeScanner_Valid;            
            chkbox_SMCCardMotion.Checked = loadsystemconfig.InstrumentConfig.SMCCardMotion_Valid;
            chkbox_GhoptoIRCamera.Checked = loadsystemconfig.InstrumentConfig.GhoptoIRCamera_Vaild;
            tb_GripperMotionDelay.Text = loadsystemconfig.InstrumentConfig.GripperMotionDelay.ToString();
            tb_LightSourceDelay.Text = loadsystemconfig.InstrumentConfig.LightSourceDelay.ToString();
        }

        private void LoadSystemOperationConfig(SystemConfig loadsystemconfig)
        {
            chkbox_DoorSafeMode.Checked = loadsystemconfig.SystemOperationConfig.doorsafemode;
            chkbox_ParalellProcess.Checked = loadsystemconfig.SystemOperationConfig.paralellprocess;
            chkbox_RecordLogFile.Checked = loadsystemconfig.SystemOperationConfig.recordlogfile;
            chkbox_GripperSecurityDetection.Checked = loadsystemconfig.SystemOperationConfig.grippersecuritydetection;
        }

        private void LoadAxisSafetyPositionConfig(SystemConfig loadsystemconfig)
        {
            cmb_Axis.Items.Clear();
            foreach (string item in Enum.GetNames(typeof(FocusingLensAligner.Enum_AxisName)))
            {
                cmb_Axis.Items.Add(item);
            }
            cmb_Axis.SelectedIndex = 0;        
            tb_SafetyPosition.Text = loadsystemconfig.AxisSafetyPosConfig.Axis_0.SafetyPosition.ToString("f3");
            tb_SafetySpeedPercent.Text = loadsystemconfig.AxisSafetyPosConfig.AxisSpeedPercent.ToString();
        }

        private void LoadUpCameraCalibrationConfig(SystemConfig loadsystemconfig)
        {
            tb_UpCamPosX1.Text = loadsystemconfig.UpCameraCalibrateConfig.CameraPos.X1.ToString("f3");
            tb_UpCamPosY1.Text = loadsystemconfig.UpCameraCalibrateConfig.CameraPos.Y1.ToString("f3");
            tb_UpCamPosZ1.Text = loadsystemconfig.UpCameraCalibrateConfig.CameraPos.Z1.ToString("f3");
            tb_UpCamCalUpRingLight.Text = loadsystemconfig.UpCameraCalibrateConfig.upringlightval.ToString();
            tb_UpCamCalUpSpotLight.Text = loadsystemconfig.UpCameraCalibrateConfig.upspotlightval.ToString();
            tb_UpCamCalDnRingLight.Text = loadsystemconfig.UpCameraCalibrateConfig.dnringlightval.ToString();
            tb_UpCamCalDnSpotLight.Text = loadsystemconfig.UpCameraCalibrateConfig.dnspotlightval.ToString();
            tb_UpCamXscale.Text = loadsystemconfig.UpCameraCalibrateConfig.xscale.ToString("f6");
            tb_UpCamYscale.Text = loadsystemconfig.UpCameraCalibrateConfig.yscale.ToString("f6");
            tb_UpCamGridStep.Text = loadsystemconfig.UpCameraCalibrateConfig.gridstep.ToString("f2");
        }

        private void LoadDownCameraCalibrationConfig(SystemConfig loadsystemconfig)
        {
            tb_DnCamPosX1.Text = loadsystemconfig.DownCameraCalibrateConfig.CameraPos.X1.ToString("f3");                        
            tb_DnCamPosY1.Text = loadsystemconfig.DownCameraCalibrateConfig.CameraPos.Y1.ToString("f3");
            tb_DnCamPosZ1.Text = loadsystemconfig.DownCameraCalibrateConfig.CameraPos.Z1.ToString("f3");
            tb_DnCamCalUpRingLight.Text = loadsystemconfig.DownCameraCalibrateConfig.upringlightval.ToString();
            tb_DnCamCalUpSpotLight.Text = loadsystemconfig.DownCameraCalibrateConfig.upspotlightval.ToString();
            tb_DnCamCalDnRingLight.Text = loadsystemconfig.DownCameraCalibrateConfig.dnringlightval.ToString();
            tb_DnCamCalDnSpotLight.Text = loadsystemconfig.DownCameraCalibrateConfig.dnspotlightval.ToString();
            tb_DnCamXscale.Text = loadsystemconfig.DownCameraCalibrateConfig.xscale.ToString("f6");
            tb_DnCamYscale.Text = loadsystemconfig.DownCameraCalibrateConfig.yscale.ToString("f6");
            tb_DnCamGridStep.Text = loadsystemconfig.DownCameraCalibrateConfig.gridstep.ToString("f2");
        }

        private void LoadIRCameraCaptureCardConfig(SystemConfig loadsystemconfig)
        {
            tb_IRCameraCaptureCard_BoxWindowBrightness.Text = loadsystemconfig.IRCameraCaptureCardConfig.ViewBoxWindowBrightness.ToString();
            tb_IRCameraCaptureCard_BoxWindowContrast.Text = loadsystemconfig.IRCameraCaptureCardConfig.ViewBoxWindowContrast.ToString();
            tb_IRCameraCaptureCard_BoxWindowDelay.Text = loadsystemconfig.IRCameraCaptureCardConfig.ViewBoxWindowDelay.ToString("f3");
            tb_IRCameraCaptureCard_LaserSpotBrightness.Text = loadsystemconfig.IRCameraCaptureCardConfig.ViewLaserSpotBrightness.ToString();
            tb_IRCameraCaptureCard_LaserSpotContrast.Text = loadsystemconfig.IRCameraCaptureCardConfig.ViewLaserSpotContrast.ToString();
            tb_IRCameraCaptureCard_LaserSpotDelay.Text = loadsystemconfig.IRCameraCaptureCardConfig.ViewLaserSpotDelay.ToString("f3");
            tb_IRCameraCaptureCard_LensInsideBoxBrightness.Text = loadsystemconfig.IRCameraCaptureCardConfig.ViewLensInsideBoxBrightness.ToString();
            tb_IRCameraCaptureCard_LensInsideBoxContrast.Text = loadsystemconfig.IRCameraCaptureCardConfig.ViewLensInsideBoxContrast.ToString();
            tb_IRCameraCaptureCard_LensInsideBoxDelay.Text = loadsystemconfig.IRCameraCaptureCardConfig.ViewLensInsideBoxDelay.ToString("f3");
        }

        private void LoadGhoptoIRCameraConfig(SystemConfig loadsystemconfig)
        {
            tb_GhoptoIRCameraViewBoxWindowIntegration.Text = loadsystemconfig.GhoptoIRCameraConfig.ViewBoxWindowIntegration.ToString();
            tb_GhoptoIRCameraViewBoxWindowGain.Text = loadsystemconfig.GhoptoIRCameraConfig.ViewBoxWindowGain.ToString();
            tb_GhoptoIRCameraViewBoxWindowBias.Text = loadsystemconfig.GhoptoIRCameraConfig.ViewBoxWindowBias.ToString();
            tb_GhoptoIRCameraViewLaserSpotIntegration.Text = loadsystemconfig.GhoptoIRCameraConfig.ViewLaserSpotIntegration.ToString();
            tb_GhoptoIRCameraViewLaserSpotGain.Text = loadsystemconfig.GhoptoIRCameraConfig.ViewLaserSpotGain.ToString();
            tb_GhoptoIRCameraViewLaserSpotBias.Text = loadsystemconfig.GhoptoIRCameraConfig.ViewLaserSpotBias.ToString();
            tb_GhoptoIRCameraViewLensInsideBoxIntegration.Text = loadsystemconfig.GhoptoIRCameraConfig.ViewLensInsideBoxIntegration.ToString();
            tb_GhoptoIRCameraViewLensInsideBoxGain.Text = loadsystemconfig.GhoptoIRCameraConfig.ViewLensInsideBoxGain.ToString();
            tb_GhoptoIRCameraViewLensInsideBoxBias.Text = loadsystemconfig.GhoptoIRCameraConfig.ViewLensInsideBoxBias.ToString();
        }

        private void LoadBoxGripperConfig(SystemConfig loadsystemconfig)
        {
            tb_BoxGripX1Pos1.Text = loadsystemconfig.BoxGripperConfig.GripperBlockPos.X1.ToString("f3");
            tb_BoxGripY1Pos1.Text = loadsystemconfig.BoxGripperConfig.GripperBlockPos.Y1.ToString("f3");
            tb_BoxGripZ1Pos1.Text = loadsystemconfig.BoxGripperConfig.GripperBlockPos.Z1.ToString("f3");
            tb_BoxGripX1Pos2.Text = loadsystemconfig.BoxGripperConfig.UpCameraViewBlockPos.X1.ToString("f3");
            tb_BoxGripY1Pos2.Text = loadsystemconfig.BoxGripperConfig.UpCameraViewBlockPos.Y1.ToString("f3");
            tb_BoxGripZ1Pos2.Text = loadsystemconfig.BoxGripperConfig.UpCameraViewBlockPos.Z1.ToString("f3");
            tb_BoxGripUpRingLight.Text = loadsystemconfig.BoxGripperConfig.upringlightval.ToString();
            tb_BoxGripUpSpotLight.Text = loadsystemconfig.BoxGripperConfig.upspotlightval.ToString();
            tb_BoxGripSize.Text = string.Empty;
            tb_BoxGripOffsetX.Text = loadsystemconfig.BoxGripperConfig.offsetx.ToString("f3");
            tb_BoxGripOffsetY.Text = loadsystemconfig.BoxGripperConfig.offsety.ToString("f3");
        }

        private void LoadLensGripperConfig(SystemConfig loadsystemconfig)
        {
            tb_LensGripX2Pos1.Text = loadsystemconfig.LensGripperConfig.GripperLensPos.X2.ToString("f3");
            tb_LensGripY2Pos1.Text = loadsystemconfig.LensGripperConfig.GripperLensPos.Y2.ToString("f3");
            tb_LensGripZ2Pos1.Text = loadsystemconfig.LensGripperConfig.GripperLensPos.Z2.ToString("f3");
            tb_LensGripX1Pos2.Text = loadsystemconfig.LensGripperConfig.UpCameraViewLensPos.X1.ToString("f3");
            tb_LensGripY1Pos2.Text = loadsystemconfig.LensGripperConfig.UpCameraViewLensPos.Y1.ToString("f3");
            tb_LensGripZ1Pos2.Text = loadsystemconfig.LensGripperConfig.UpCameraViewLensPos.Z1.ToString("f3");
            tb_LensGripUpRingLight.Text = loadsystemconfig.LensGripperConfig.upringlightval.ToString();
            tb_LensGripUpSpotLight.Text = loadsystemconfig.LensGripperConfig.upspotlightval.ToString();
            tb_LensGripperCentrePixelPosX.Text = string.Empty;
            tb_LensGripperCentrePixelPosY.Text = string.Empty;
            tb_LensGripOffsetX.Text = loadsystemconfig.LensGripperConfig.offsetx.ToString("f3");
            tb_LensGripOffsetY.Text = loadsystemconfig.LensGripperConfig.offsety.ToString("f3");
        }

        private void LoadDispenserConfig(SystemConfig loadsystemconfig)
        {
            tb_DispenserX1Pos1.Text = loadsystemconfig.DispenserConfig.DispenserPinToEpoxyPos.X1.ToString("f3");
            tb_DispenserY1Pos1.Text = loadsystemconfig.DispenserConfig.DispenserPinToEpoxyPos.Y1.ToString("f3");
            tb_DispenserZ1Pos1.Text = loadsystemconfig.DispenserConfig.DispenserPinToEpoxyPos.Z1.ToString("f3");
            tb_DispenserU1Pos1.Text = loadsystemconfig.DispenserConfig.DispenserPinToEpoxyPos.U1.ToString("f3");
            tb_DispenserX2Pos1.Text = loadsystemconfig.DispenserConfig.DispenserPinToEpoxyPos.X2.ToString("f3");
            tb_DispenserY2Pos1.Text = loadsystemconfig.DispenserConfig.DispenserPinToEpoxyPos.Y2.ToString("f3");
            tb_DispenserZ2Pos1.Text = loadsystemconfig.DispenserConfig.DispenserPinToEpoxyPos.Z2.ToString("f3");
            tb_DispenserX1Pos2.Text = loadsystemconfig.DispenserConfig.UpCameraViewEpoxyPos.X1.ToString("f3");
            tb_DispenserY1Pos2.Text = loadsystemconfig.DispenserConfig.UpCameraViewEpoxyPos.Y1.ToString("f3");
            tb_DispenserZ1Pos2.Text = loadsystemconfig.DispenserConfig.UpCameraViewEpoxyPos.Z1.ToString("f3");
            tb_DispenserUpRingLight.Text = loadsystemconfig.DispenserConfig.upringlightval.ToString();
            tb_DispenserUpSpotLight.Text = loadsystemconfig.DispenserConfig.upspotlightval.ToString();
            tb_DispenserEpoxySpotSize.Text = string.Empty;
            tb_DispenserOffsetX.Text = loadsystemconfig.DispenserConfig.offsetx.ToString("f3");
            tb_DispenserOffsetY.Text = loadsystemconfig.DispenserConfig.offsety.ToString("f3");
            tb_DipEpoxyX1Pos.Text = loadsystemconfig.DispenserConfig.epoxyDipPos.X1.ToString("f3");
            tb_DipEpoxyY1Pos.Text = loadsystemconfig.DispenserConfig.epoxyDipPos.Y1.ToString("f3");
            tb_DipEpoxyZ1Pos.Text = loadsystemconfig.DispenserConfig.epoxyDipPos.Z1.ToString("f3");
            tb_DipEpoxyU1Pos.Text = loadsystemconfig.DispenserConfig.epoxyDipPos.U1.ToString("f3");
            tb_WipeEpoxyX1Pos.Text = loadsystemconfig.DispenserConfig.epoxyWipePos.X1.ToString("f3");
            tb_WipeEpoxyY1Pos.Text = loadsystemconfig.DispenserConfig.epoxyWipePos.Y1.ToString("f3");
            tb_WipeEpoxyZ1Pos.Text = loadsystemconfig.DispenserConfig.epoxyWipePos.Z1.ToString("f3");
            tb_WipeEpoxyU1Pos.Text = loadsystemconfig.DispenserConfig.epoxyWipePos.U1.ToString("f3");
            tb_EpoxyTestRowCount.Text = loadsystemconfig.DispenserConfig.epoxytestrowcount.ToString();
            tb_EpoxyTestRowSpace.Text = loadsystemconfig.DispenserConfig.epoxytestrowspace.ToString("f2");
            tb_EpoxyTestColCount.Text = loadsystemconfig.DispenserConfig.epoxytestcolumncount.ToString();
            tb_EpoxyTestColSpace.Text = loadsystemconfig.DispenserConfig.epoxytestcolumnspace.ToString("f2");
            tb_EpoxyTestDipTime.Text = loadsystemconfig.DispenserConfig.epoxytestdiptime.ToString("f1");
            chkbox_AutoEpoxyDipHeightCompensation.Checked = loadsystemconfig.DispenserConfig.autoEpoxyDipHeightCompensation;
            tb_AutoEpoxyDipCount.Text = loadsystemconfig.DispenserConfig.autoEpoxyDipCount.ToString();
            tb_AutoEpoxyDipCompensationHeight.Text = loadsystemconfig.DispenserConfig.autoEpoxyDipCompensationHeight.ToString("f3");
            tb_AutoEpoxyDipZ1HeightLimit.Text = loadsystemconfig.DispenserConfig.autoEpoxyDipZ1HeightLimit.ToString("f3");
            tb_AutoEpoxyDipZ1RealtimeHeight.Text = GlobalParameters.processdata.realtimeEpoxyDipHeight.ToString("f3");
        }

        private void LoadManageProductConfig(SystemConfig loadsystemconfig)
        {
            //Load Product List
            string path = Application.StartupPath.ToString() + @"\Config\ProductConfig";
            List<FileInfo> productlist = GeneralFunction.GetFileInfos(path, ".xml");
            List<String> productnamelst = new List<string>();

            foreach (FileInfo productname in productlist)
            {
                string product = productname.Name.Remove(productname.Name.LastIndexOf("."));
                lb_ProductList.Items.Add(product);
                productnamelst.Add(product);
                cmb_CurrentProduct.Items.Add(product);
            }
            if(loadsystemconfig.ManageProductConfig.currentproduct!=null)
                cmb_CurrentProduct.SelectedIndex = cmb_CurrentProduct.Items.IndexOf(loadsystemconfig.ManageProductConfig.currentproduct);
        }
        #endregion

        #region Save System Config
        private void SaveInstrumentConfig(ref SystemConfig savesystemconfig)
        {            
            savesystemconfig.InstrumentConfig.Keithley2401_1_Valid = chkbox_Keithley2400_1.Checked;
            savesystemconfig.InstrumentConfig.Keithley2401_2_Valid = chkbox_Keithley2400_2.Checked;
            savesystemconfig.InstrumentConfig.Keithley2401_3_Valid = chkbox_Keithley2400_3.Checked;
            savesystemconfig.InstrumentConfig.Keithley2401_4_Valid = chkbox_Keithley2400_4.Checked;
            savesystemconfig.InstrumentConfig.Keithley2510_1_Valid = chkbox_Keithley2510_1.Checked;
            savesystemconfig.InstrumentConfig.Keithley2510_2_Valid = chkbox_Keithley2510_2.Checked;
            savesystemconfig.InstrumentConfig.KeySightE364x_Valid = chkbox_KeySightE364X.Checked;
            savesystemconfig.InstrumentConfig.I2C_Valid = chkbox_I2C.Checked;
            savesystemconfig.InstrumentConfig.FI2CUSB_Valid = chkbox_FI2CUSB.Checked;
            savesystemconfig.InstrumentConfig.DriverBoard_Valid = chkbox_DriverBoard.Checked;
            savesystemconfig.InstrumentConfig.ForceSensor_Valid = chkbox_ForceSensor.Checked;
            savesystemconfig.InstrumentConfig.IAIBoxGripper_Valid = chkbox_BoxGripper.Checked;
            savesystemconfig.InstrumentConfig.IAILensGripper_Valid = chkbox_LensGripper.Checked;
            savesystemconfig.InstrumentConfig.UVController_Valid = chkbox_UV.Checked;
            savesystemconfig.InstrumentConfig.QRCodeScanner_Valid = chkbox_Scanner.Checked;
            savesystemconfig.InstrumentConfig.SMCCardMotion_Valid = chkbox_SMCCardMotion.Checked;
            savesystemconfig.InstrumentConfig.GhoptoIRCamera_Vaild = chkbox_GhoptoIRCamera.Checked;
            savesystemconfig.InstrumentConfig.GripperMotionDelay = Math.Abs(int.Parse(tb_GripperMotionDelay.Text));
            savesystemconfig.InstrumentConfig.LightSourceDelay = Math.Abs(int.Parse(tb_LightSourceDelay.Text));
        }

        public void SaveAxisSafetyPositionConfig(ref SystemConfig savesystemconfig)
        {
            int index = cmb_Axis.SelectedIndex;
            string AxisName = cmb_Axis.Text;
            double SafetyPosition = double.Parse(tb_SafetyPosition.Text);
            double AxisSpeedPercent = int.Parse(tb_SafetySpeedPercent.Text);
            if (AxisSpeedPercent < 50)
            {
                savesystemconfig.AxisSafetyPosConfig.AxisSpeedPercent = 50;
            }
            else if (AxisSpeedPercent > 100)
            {
                savesystemconfig.AxisSafetyPosConfig.AxisSpeedPercent = 100;
            }
            else
            {
                savesystemconfig.AxisSafetyPosConfig.AxisSpeedPercent = AxisSpeedPercent;
            }

            switch (index)
            {
                case 0:
                    savesystemconfig.AxisSafetyPosConfig.Axis_0.AxisName = AxisName;
                    savesystemconfig.AxisSafetyPosConfig.Axis_0.SafetyPosition = SafetyPosition;
                    break;
                case 1:
                    savesystemconfig.AxisSafetyPosConfig.Axis_1.AxisName = AxisName;
                    savesystemconfig.AxisSafetyPosConfig.Axis_1.SafetyPosition = SafetyPosition;
                    break;
                case 2:
                    savesystemconfig.AxisSafetyPosConfig.Axis_2.AxisName = AxisName;
                    savesystemconfig.AxisSafetyPosConfig.Axis_2.SafetyPosition = SafetyPosition;
                    break;
                case 3:
                    savesystemconfig.AxisSafetyPosConfig.Axis_3.AxisName = AxisName;
                    savesystemconfig.AxisSafetyPosConfig.Axis_3.SafetyPosition = SafetyPosition;
                    break;
                case 4:
                    savesystemconfig.AxisSafetyPosConfig.Axis_4.AxisName = AxisName;
                    savesystemconfig.AxisSafetyPosConfig.Axis_4.SafetyPosition = SafetyPosition;
                    break;
                case 5:
                    savesystemconfig.AxisSafetyPosConfig.Axis_5.AxisName = AxisName;
                    savesystemconfig.AxisSafetyPosConfig.Axis_5.SafetyPosition = SafetyPosition;
                    break;
                case 6:
                    savesystemconfig.AxisSafetyPosConfig.Axis_6.AxisName = AxisName;
                    savesystemconfig.AxisSafetyPosConfig.Axis_6.SafetyPosition = SafetyPosition;
                    break;
                case 7:
                    savesystemconfig.AxisSafetyPosConfig.Axis_7.AxisName = AxisName;
                    savesystemconfig.AxisSafetyPosConfig.Axis_7.SafetyPosition = SafetyPosition;
                    break;
            }
        }

        private void SaveSystemOperationConfig(ref SystemConfig savesystemconfig)
        {
            savesystemconfig.SystemOperationConfig.doorsafemode = chkbox_DoorSafeMode.Checked;
            savesystemconfig.SystemOperationConfig.paralellprocess = chkbox_ParalellProcess.Checked;
            savesystemconfig.SystemOperationConfig.recordlogfile = chkbox_RecordLogFile.Checked;
            savesystemconfig.SystemOperationConfig.grippersecuritydetection = chkbox_GripperSecurityDetection.Checked;
        }

        private void SaveUpCameraCalibrationConfig(ref SystemConfig savesystemconfig)
        {
            savesystemconfig.UpCameraCalibrateConfig.CameraPos.X1 = double.Parse(tb_UpCamPosX1.Text);
            savesystemconfig.UpCameraCalibrateConfig.CameraPos.Y1 = double.Parse(tb_UpCamPosY1.Text);
            savesystemconfig.UpCameraCalibrateConfig.CameraPos.Z1 = double.Parse(tb_UpCamPosZ1.Text);
            savesystemconfig.UpCameraCalibrateConfig.upringlightval = int.Parse(tb_UpCamCalUpRingLight.Text);
            savesystemconfig.UpCameraCalibrateConfig.upspotlightval = int.Parse(tb_UpCamCalUpSpotLight.Text);
            savesystemconfig.UpCameraCalibrateConfig.dnringlightval = int.Parse(tb_UpCamCalDnRingLight.Text);
            savesystemconfig.UpCameraCalibrateConfig.dnspotlightval = int.Parse(tb_UpCamCalDnSpotLight.Text);
            savesystemconfig.UpCameraCalibrateConfig.xscale = double.Parse(tb_UpCamXscale.Text);
            savesystemconfig.UpCameraCalibrateConfig.yscale = double.Parse(tb_UpCamYscale.Text);
            savesystemconfig.UpCameraCalibrateConfig.gridstep = double.Parse(tb_UpCamGridStep.Text);
        }

        private void SaveDownCameraCalibrationConfig(ref SystemConfig savesystemconfig)
        {
            savesystemconfig.DownCameraCalibrateConfig.CameraPos.X1 = double.Parse(tb_DnCamPosX1.Text);
            savesystemconfig.DownCameraCalibrateConfig.CameraPos.Y1 = double.Parse(tb_DnCamPosY1.Text);
            savesystemconfig.DownCameraCalibrateConfig.CameraPos.Z1 = double.Parse(tb_DnCamPosZ1.Text);
            savesystemconfig.DownCameraCalibrateConfig.upringlightval = int.Parse(tb_DnCamCalUpRingLight.Text);
            savesystemconfig.DownCameraCalibrateConfig.upspotlightval = int.Parse(tb_DnCamCalUpSpotLight.Text);
            savesystemconfig.DownCameraCalibrateConfig.dnringlightval = int.Parse(tb_DnCamCalDnRingLight.Text);
            savesystemconfig.DownCameraCalibrateConfig.dnspotlightval = int.Parse(tb_DnCamCalDnSpotLight.Text);
            savesystemconfig.DownCameraCalibrateConfig.xscale = double.Parse(tb_DnCamXscale.Text);
            savesystemconfig.DownCameraCalibrateConfig.yscale = double.Parse(tb_DnCamYscale.Text);
            savesystemconfig.DownCameraCalibrateConfig.gridstep = double.Parse(tb_DnCamGridStep.Text);
        }

        private void SaveIRCameraCaptureCardConfig(ref SystemConfig savesystemconfig)
        {
            savesystemconfig.IRCameraCaptureCardConfig.ViewBoxWindowBrightness = double.Parse(tb_IRCameraCaptureCard_BoxWindowBrightness.Text);
            savesystemconfig.IRCameraCaptureCardConfig.ViewBoxWindowContrast = double.Parse(tb_IRCameraCaptureCard_BoxWindowContrast.Text);
            savesystemconfig.IRCameraCaptureCardConfig.ViewBoxWindowDelay = double.Parse(tb_IRCameraCaptureCard_BoxWindowDelay.Text);
            savesystemconfig.IRCameraCaptureCardConfig.ViewLaserSpotBrightness = double.Parse(tb_IRCameraCaptureCard_LaserSpotBrightness.Text);
            savesystemconfig.IRCameraCaptureCardConfig.ViewLaserSpotContrast = double.Parse(tb_IRCameraCaptureCard_LaserSpotContrast.Text);
            savesystemconfig.IRCameraCaptureCardConfig.ViewLaserSpotDelay = double.Parse(tb_IRCameraCaptureCard_LaserSpotDelay.Text);
            savesystemconfig.IRCameraCaptureCardConfig.ViewLensInsideBoxBrightness = double.Parse(tb_IRCameraCaptureCard_LensInsideBoxBrightness.Text);
            savesystemconfig.IRCameraCaptureCardConfig.ViewLensInsideBoxContrast = double.Parse(tb_IRCameraCaptureCard_LensInsideBoxContrast.Text);
            savesystemconfig.IRCameraCaptureCardConfig.ViewLensInsideBoxDelay = double.Parse(tb_IRCameraCaptureCard_LensInsideBoxDelay.Text);
        }

        private void SaveGhoptoIRCameraConfig(ref SystemConfig savesystemconfig)
        {
            savesystemconfig.GhoptoIRCameraConfig.ViewBoxWindowIntegration = uint.Parse(tb_GhoptoIRCameraViewBoxWindowIntegration.Text);
            savesystemconfig.GhoptoIRCameraConfig.ViewBoxWindowGain = uint.Parse(tb_GhoptoIRCameraViewBoxWindowGain.Text);
            savesystemconfig.GhoptoIRCameraConfig.ViewBoxWindowBias = uint.Parse(tb_GhoptoIRCameraViewBoxWindowBias.Text);
            savesystemconfig.GhoptoIRCameraConfig.ViewLaserSpotIntegration = uint.Parse(tb_GhoptoIRCameraViewLaserSpotIntegration.Text);
            savesystemconfig.GhoptoIRCameraConfig.ViewLaserSpotGain = uint.Parse(tb_GhoptoIRCameraViewLaserSpotGain.Text);
            savesystemconfig.GhoptoIRCameraConfig.ViewLaserSpotBias = uint.Parse(tb_GhoptoIRCameraViewLaserSpotBias.Text);
            savesystemconfig.GhoptoIRCameraConfig.ViewLensInsideBoxIntegration = uint.Parse(tb_GhoptoIRCameraViewLensInsideBoxIntegration.Text);
            savesystemconfig.GhoptoIRCameraConfig.ViewLensInsideBoxGain = uint.Parse(tb_GhoptoIRCameraViewLensInsideBoxGain.Text);
            savesystemconfig.GhoptoIRCameraConfig.ViewLensInsideBoxBias = uint.Parse(tb_GhoptoIRCameraViewLensInsideBoxBias.Text);
        }

        private void SaveBoxGripperConfig( ref SystemConfig savesystemconfig)
        {
            savesystemconfig.BoxGripperConfig.GripperBlockPos.X1 = double.Parse(tb_BoxGripX1Pos1.Text);         
            savesystemconfig.BoxGripperConfig.GripperBlockPos.Y1 = double.Parse(tb_BoxGripY1Pos1.Text);
            savesystemconfig.BoxGripperConfig.GripperBlockPos.Z1 = double.Parse(tb_BoxGripZ1Pos1.Text);
            savesystemconfig.BoxGripperConfig.UpCameraViewBlockPos.X1 = double.Parse(tb_BoxGripX1Pos2.Text);             
            savesystemconfig.BoxGripperConfig.UpCameraViewBlockPos.Y1 = double.Parse(tb_BoxGripY1Pos2.Text);
            savesystemconfig.BoxGripperConfig.UpCameraViewBlockPos.Z1 = double.Parse(tb_BoxGripZ1Pos2.Text);
            savesystemconfig.BoxGripperConfig.upringlightval = int.Parse(tb_BoxGripUpRingLight.Text);                 
            savesystemconfig.BoxGripperConfig.upspotlightval = int.Parse(tb_BoxGripUpSpotLight.Text);
            savesystemconfig.BoxGripperConfig.offsetx = double.Parse(tb_BoxGripOffsetX.Text);
            savesystemconfig.BoxGripperConfig.offsety = double.Parse(tb_BoxGripOffsetY.Text);                        
        }

        private void SaveLensGripperConfig(ref SystemConfig savesystemconfig)
        {
            savesystemconfig.LensGripperConfig.GripperLensPos.X2 = double.Parse(tb_LensGripX2Pos1.Text);
            savesystemconfig.LensGripperConfig.GripperLensPos.Y2 = double.Parse(tb_LensGripY2Pos1.Text);
            savesystemconfig.LensGripperConfig.GripperLensPos.Z2 = double.Parse(tb_LensGripZ2Pos1.Text);
            savesystemconfig.LensGripperConfig.UpCameraViewLensPos.X1 = double.Parse(tb_LensGripX1Pos2.Text);
            savesystemconfig.LensGripperConfig.UpCameraViewLensPos.Y1 = double.Parse(tb_LensGripY1Pos2.Text);
            savesystemconfig.LensGripperConfig.UpCameraViewLensPos.Z1 = double.Parse(tb_LensGripZ1Pos2.Text);
            savesystemconfig.LensGripperConfig.upringlightval = int.Parse(tb_LensGripUpRingLight.Text);
            savesystemconfig.LensGripperConfig.upspotlightval = int.Parse(tb_LensGripUpSpotLight.Text);
            savesystemconfig.LensGripperConfig.offsetx = double.Parse(tb_LensGripOffsetX.Text);
            savesystemconfig.LensGripperConfig.offsety = double.Parse(tb_LensGripOffsetY.Text);
        }

        private void SaveDispenserConfig(ref SystemConfig savesystemconfig)
        {
            savesystemconfig.DispenserConfig.DispenserPinToEpoxyPos.X1 = double.Parse(tb_DispenserX1Pos1.Text);
            savesystemconfig.DispenserConfig.DispenserPinToEpoxyPos.Y1 = double.Parse(tb_DispenserY1Pos1.Text);     
            savesystemconfig.DispenserConfig.DispenserPinToEpoxyPos.Z1 = double.Parse(tb_DispenserZ1Pos1.Text);
            savesystemconfig.DispenserConfig.DispenserPinToEpoxyPos.U1 = double.Parse(tb_DispenserU1Pos1.Text);
            savesystemconfig.DispenserConfig.DispenserPinToEpoxyPos.X2 = double.Parse(tb_DispenserX2Pos1.Text);
            savesystemconfig.DispenserConfig.DispenserPinToEpoxyPos.Y2 = double.Parse(tb_DispenserY2Pos1.Text);
            savesystemconfig.DispenserConfig.DispenserPinToEpoxyPos.Z2 = double.Parse(tb_DispenserZ2Pos1.Text);
            savesystemconfig.DispenserConfig.UpCameraViewEpoxyPos.X1 = double.Parse(tb_DispenserX1Pos2.Text);
            savesystemconfig.DispenserConfig.UpCameraViewEpoxyPos.Y1 = double.Parse(tb_DispenserY1Pos2.Text);
            savesystemconfig.DispenserConfig.UpCameraViewEpoxyPos.Z1 = double.Parse(tb_DispenserZ1Pos2.Text);
            savesystemconfig.DispenserConfig.upringlightval = int.Parse(tb_DispenserUpRingLight.Text);
            savesystemconfig.DispenserConfig.upspotlightval = int.Parse(tb_DispenserUpSpotLight.Text);
            savesystemconfig.DispenserConfig.offsetx = double.Parse(tb_DispenserOffsetX.Text);
            savesystemconfig.DispenserConfig.offsety = double.Parse(tb_DispenserOffsetY.Text);
            savesystemconfig.DispenserConfig.epoxyDipPos.X1 = double.Parse(tb_DipEpoxyX1Pos.Text);
            savesystemconfig.DispenserConfig.epoxyDipPos.Y1 = double.Parse(tb_DipEpoxyY1Pos.Text);
            savesystemconfig.DispenserConfig.epoxyDipPos.Z1 = double.Parse(tb_DipEpoxyZ1Pos.Text);
            savesystemconfig.DispenserConfig.epoxyDipPos.U1 = double.Parse(tb_DipEpoxyU1Pos.Text);
            savesystemconfig.DispenserConfig.epoxyWipePos.X1 = double.Parse(tb_WipeEpoxyX1Pos.Text);
            savesystemconfig.DispenserConfig.epoxyWipePos.Y1 = double.Parse(tb_WipeEpoxyY1Pos.Text);
            savesystemconfig.DispenserConfig.epoxyWipePos.Z1 = double.Parse(tb_WipeEpoxyZ1Pos.Text);
            savesystemconfig.DispenserConfig.epoxyWipePos.U1 = double.Parse(tb_WipeEpoxyU1Pos.Text);
            savesystemconfig.DispenserConfig.epoxytestrowcount = int.Parse(tb_EpoxyTestRowCount.Text);
            savesystemconfig.DispenserConfig.epoxytestrowspace = double.Parse(tb_EpoxyTestRowSpace.Text);
            savesystemconfig.DispenserConfig.epoxytestcolumncount = int.Parse(tb_EpoxyTestColCount.Text);
            savesystemconfig.DispenserConfig.epoxytestcolumnspace = double.Parse(tb_EpoxyTestColSpace.Text);
            savesystemconfig.DispenserConfig.epoxytestdiptime = double.Parse(tb_EpoxyTestDipTime.Text);
            savesystemconfig.DispenserConfig.autoEpoxyDipHeightCompensation = chkbox_AutoEpoxyDipHeightCompensation.Checked;
            savesystemconfig.DispenserConfig.autoEpoxyDipCount = int.Parse(tb_AutoEpoxyDipCount.Text);
            savesystemconfig.DispenserConfig.autoEpoxyDipCompensationHeight = double.Parse(tb_AutoEpoxyDipCompensationHeight.Text);
            savesystemconfig.DispenserConfig.autoEpoxyDipZ1HeightLimit = double.Parse(tb_AutoEpoxyDipZ1HeightLimit.Text);
            //实时蘸胶Z1轴高度值刷新
            GlobalParameters.processdata.realtimeEpoxyDipHeight = double.Parse(tb_DipEpoxyZ1Pos.Text);
        }

        private void SaveManageProductConfig(ref SystemConfig savesystemconfig)
        {
            if(cmb_CurrentProduct.Text.Trim()!="")
                savesystemconfig.ManageProductConfig.currentproduct = cmb_CurrentProduct.SelectedItem.ToString();      
        }
        
        #endregion

        public void UpdateAxisSafetyPositionConfig(SystemConfig savesystemconfig)
        {
            int index = cmb_Axis.SelectedIndex;
            switch (index)
            {
                case 0:
                    tb_SafetyPosition.Text = savesystemconfig.AxisSafetyPosConfig.Axis_0.SafetyPosition.ToString("f3");
                    break;
                case 1:
                    tb_SafetyPosition.Text = savesystemconfig.AxisSafetyPosConfig.Axis_1.SafetyPosition.ToString("f3");
                    break;
                case 2:
                    tb_SafetyPosition.Text = savesystemconfig.AxisSafetyPosConfig.Axis_2.SafetyPosition.ToString("f3");
                    break;
                case 3:
                    tb_SafetyPosition.Text = savesystemconfig.AxisSafetyPosConfig.Axis_3.SafetyPosition.ToString("f3");
                    break;
                case 4:
                    tb_SafetyPosition.Text = savesystemconfig.AxisSafetyPosConfig.Axis_4.SafetyPosition.ToString("f3");
                    break;
                case 5:
                    tb_SafetyPosition.Text = savesystemconfig.AxisSafetyPosConfig.Axis_5.SafetyPosition.ToString("f3");
                    break;
                case 6:
                    tb_SafetyPosition.Text = savesystemconfig.AxisSafetyPosConfig.Axis_6.SafetyPosition.ToString("f3");
                    break;
                case 7:
                    tb_SafetyPosition.Text = savesystemconfig.AxisSafetyPosConfig.Axis_7.SafetyPosition.ToString("f3");
                    break;
            }
        }

        private void SystemConfigForm_Load(object sender, EventArgs e)
        {
            bool res = GeneralFunction.LoadSystemConfig();
            if (res)
            {
                LoadInstrumentConfig(GlobalParameters.systemconfig);
                LoadAxisSafetyPositionConfig(GlobalParameters.systemconfig);
                LoadSystemOperationConfig(GlobalParameters.systemconfig);
                LoadUpCameraCalibrationConfig(GlobalParameters.systemconfig);
                LoadDownCameraCalibrationConfig(GlobalParameters.systemconfig);
                LoadIRCameraCaptureCardConfig(GlobalParameters.systemconfig);
                LoadGhoptoIRCameraConfig(GlobalParameters.systemconfig);
                LoadBoxGripperConfig(GlobalParameters.systemconfig);
                LoadLensGripperConfig(GlobalParameters.systemconfig);
                LoadDispenserConfig(GlobalParameters.systemconfig);
                LoadManageProductConfig(GlobalParameters.systemconfig);
                gb_GhoptoIRCamera.Enabled = GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild;
            }
            else
            {
                MessageBox.Show("调取系统配置文件失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string filepath = GeneralFunction.GetConfigFilePath("SystemConfig.xml");
            SaveInstrumentConfig(ref GlobalParameters.systemconfig);
            SaveAxisSafetyPositionConfig(ref GlobalParameters.systemconfig);
            SaveSystemOperationConfig(ref GlobalParameters.systemconfig);
            SaveUpCameraCalibrationConfig(ref GlobalParameters.systemconfig);
            SaveDownCameraCalibrationConfig(ref GlobalParameters.systemconfig);
            SaveIRCameraCaptureCardConfig(ref GlobalParameters.systemconfig);
            SaveGhoptoIRCameraConfig(ref GlobalParameters.systemconfig);
            SaveBoxGripperConfig(ref GlobalParameters.systemconfig);
            SaveLensGripperConfig(ref GlobalParameters.systemconfig);
            SaveDispenserConfig(ref GlobalParameters.systemconfig);
            SaveManageProductConfig(ref GlobalParameters.systemconfig);
            bool res = STD_IGeneralTool.GeneralTool.TryToSave<SystemConfig>(GlobalParameters.systemconfig, filepath);
            if (!res)
            {
                MessageBox.Show("保存系统配置文件失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("保存系统配置文件成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_Axis_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAxisSafetyPositionConfig(GlobalParameters.systemconfig);
        }

        private void btn_LoadUpCamPos_Click(object sender, EventArgs e)
        {
            tb_UpCamPosX1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_UpCamPosY1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_UpCamPosZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_LoadDnCamPos_Click(object sender, EventArgs e)
        {
            tb_DnCamPosX1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_DnCamPosY1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_DnCamPosZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_MovetoUpCameraCalibratePos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            double posx = double.Parse(tb_UpCamPosX1.Text);
            double posy = double.Parse(tb_UpCamPosY1.Text);
            double posz = double.Parse(tb_UpCamPosZ1.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_UpCameraCalibrate_Click(object sender, EventArgs e)
        {
            double pixelSize = 0;
            double UpRingValue = double.Parse(tb_UpCamCalUpRingLight.Text);
            double UpSpotValue = double.Parse(tb_UpCamCalUpSpotLight.Text);
            double DnRingValue = double.Parse(tb_UpCamCalDnRingLight.Text);
            double DnSpotValue = double.Parse(tb_UpCamCalDnSpotLight.Text);
            double GridStep = double.Parse(tb_UpCamGridStep.Text);

            bool res = GlobalFunction.ProcessFlow.VisionCameraCalibrate(UpRingValue, UpSpotValue, DnRingValue, DnSpotValue, GridStep, true, ref pixelSize);
            if (res == true)
            {
                GlobalFunction.updateStatusDelegate("上相机分辨率校准成功", Enum_MachineStatus.NORMAL);
                tb_UpCamXscale.Text = pixelSize.ToString("f6");
                tb_UpCamYscale.Text = pixelSize.ToString("f6");
            }
            else
            {
                string str = "上相机分辨率校准失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_MovetoDownCameraCalibratePos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            double posx = double.Parse(tb_DnCamPosX1.Text);
            double posy = double.Parse(tb_DnCamPosY1.Text);
            double posz = double.Parse(tb_DnCamPosZ1.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_DnCamCalibrate_Click(object sender, EventArgs e)
        {
            double PixelSize = 0;
            double UpRingValue = double.Parse(tb_DnCamCalUpRingLight.Text);
            double UpSpotValue = double.Parse(tb_DnCamCalUpSpotLight.Text);
            double DnRingValue = double.Parse(tb_DnCamCalDnRingLight.Text);
            double DnSpotValue = double.Parse(tb_DnCamCalDnSpotLight.Text);
            double GridStep = double.Parse(tb_DnCamGridStep.Text);

            bool res = GlobalFunction.ProcessFlow.VisionCameraCalibrate(UpRingValue, UpSpotValue, DnRingValue, DnSpotValue, GridStep, false, ref PixelSize);
            if (res == true)
            {
                GlobalFunction.updateStatusDelegate("下相机分辨率校准成功", Enum_MachineStatus.NORMAL);
                tb_DnCamXscale.Text = PixelSize.ToString("f6");
                tb_DnCamYscale.Text = PixelSize.ToString("f6");
            }
            else
            {
                string str = "下相机分辨率校准失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_BoxGripLoadPos1_Click(object sender, EventArgs e)
        {
            tb_BoxGripX1Pos1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_BoxGripY1Pos1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_BoxGripZ1Pos1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_BoxGripMoveToPos1_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            double posx = double.Parse(tb_BoxGripX1Pos1.Text);
            double posy = double.Parse(tb_BoxGripY1Pos1.Text);
            double posz = double.Parse(tb_BoxGripZ1Pos1.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void btn_BoxGripLoadPos2_Click(object sender, EventArgs e)
        {
            tb_BoxGripX1Pos2.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_BoxGripY1Pos2.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_BoxGripZ1Pos2.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_BoxGripMoveToPos2_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            double posx = double.Parse(tb_BoxGripX1Pos2.Text);
            double posy = double.Parse(tb_BoxGripY1Pos2.Text);
            double posz = double.Parse(tb_BoxGripZ1Pos2.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void btn_BoxGripFind_Click(object sender, EventArgs e)
        {
            bool res = false;
            string str = string.Empty;
            VisionResult.EpoxyResult gripperBlockResult = new VisionResult.EpoxyResult();
            double UpRingValue = double.Parse(tb_BoxGripUpRingLight.Text);
            double UpSpotValue = double.Parse(tb_BoxGripUpSpotLight.Text);

            res = GlobalFunction.ProcessFlow.VisionRecognizeBoxGripper(UpRingValue, UpSpotValue, ref gripperBlockResult);
            if (res == false)
            {
                str = "上相机识别Box夹爪标定块中心孔失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GlobalFunction.updateStatusDelegate("上相机识别Box夹爪标定块中心孔成功", Enum_MachineStatus.NORMAL);
                double dia = gripperBlockResult.diameter * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
                str = "Box夹爪标定块中心孔直径: " + dia.ToString("f3") + "mm";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                tb_BoxGripSize.Text = dia.ToString("f3");
            }
        }

        private void btn_BoxGripToCenter_Click(object sender, EventArgs e)
        {
            bool res = false;
            string str = string.Empty;
            VisionResult.EpoxyResult gripperBlockResult = new VisionResult.EpoxyResult();
            double UpRingValue = double.Parse(tb_BoxGripUpRingLight.Text);
            double UpSpotValue = double.Parse(tb_BoxGripUpSpotLight.Text);

            //移动上相机之前识别
            res = GlobalFunction.ProcessFlow.VisionRecognizeBoxGripper(UpRingValue, UpSpotValue, ref gripperBlockResult);
            if (res == false)
            {
                str = "上相机识别Box夹爪标定块中心孔失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //移动上相机中心对准标定块中心孔
                double dx = (gripperBlockResult.imagecenter.X - gripperBlockResult.center.X) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
                double dy = (gripperBlockResult.imagecenter.Y - gripperBlockResult.center.Y) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.yscale;
                GlobalFunction.MotionTools.Motion_MoveX1Distance(-dx, false, false, true);
                GlobalFunction.MotionTools.Motion_MoveY1Distance(dy, false, false, true);

                //移动上相机之后再次识别
                res = GlobalFunction.ProcessFlow.VisionRecognizeBoxGripper(UpRingValue, UpSpotValue, ref gripperBlockResult);
                if (res == false)
                {
                    str = "上相机识别Box夹爪标定块中心孔失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    GlobalFunction.updateStatusDelegate("上相机中心对准Box夹爪标定块中心成功", Enum_MachineStatus.NORMAL);
                    double dia = gripperBlockResult.diameter * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
                    str = "Box夹爪标定块中心孔直径: " + dia.ToString("f3") + "mm";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                    tb_BoxGripSize.Text = dia.ToString("f3");                    
                    tb_BoxGripX1Pos2.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
                    tb_BoxGripY1Pos2.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
                    tb_BoxGripZ1Pos2.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
                }
            }
        }

        private void btn_AutoCalibrateBoxGrip_Click(object sender, EventArgs e)
        {
            double posx1 = double.Parse(tb_BoxGripX1Pos1.Text);
            double posy1 = double.Parse(tb_BoxGripY1Pos1.Text);
            double posx2 = double.Parse(tb_BoxGripX1Pos2.Text);
            double posy2 = double.Parse(tb_BoxGripY1Pos2.Text);
            tb_BoxGripOffsetX.Text = (posx1 - posx2).ToString("f3");
            tb_BoxGripOffsetY.Text = (posy1 - posy2).ToString("f3");
        }

        private void btn_LensGripLoadPos1_Click(object sender, EventArgs e)
        {
            tb_LensGripX2Pos1.Text = GlobalFunction.MotionTools.Motion_GetX2Pos().ToString("f3");
            tb_LensGripY2Pos1.Text = GlobalFunction.MotionTools.Motion_GetY2Pos().ToString("f3");
            tb_LensGripZ2Pos1.Text = GlobalFunction.MotionTools.Motion_GetZ2Pos().ToString("f3");
        }

        private void btn_LensGripMoveToPos1_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            double posx = double.Parse(tb_LensGripX2Pos1.Text);
            double posy = double.Parse(tb_LensGripY2Pos1.Text);
            double posz = double.Parse(tb_LensGripZ2Pos1.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX2Y2Z2ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X2_Y2_Z2轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_LensGripLoadPos2_Click(object sender, EventArgs e)
        {
            tb_LensGripX1Pos2.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_LensGripY1Pos2.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_LensGripZ1Pos2.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_LensGripMovetoPos2_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            double posx = double.Parse(tb_LensGripX1Pos2.Text);
            double posy = double.Parse(tb_LensGripY1Pos2.Text);
            double posz = double.Parse(tb_LensGripZ1Pos2.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_LensGripFind_Click(object sender, EventArgs e)
        {
            bool res = false;
            string str = string.Empty;
            VisionResult.GripperResult gripperResult = new VisionResult.GripperResult();
            double UpRingValue = double.Parse(tb_LensGripUpRingLight.Text);
            double UpSpotValue = double.Parse(tb_LensGripUpSpotLight.Text);

            res = GlobalFunction.ProcessFlow.VisionRecognizeLensGripper(UpRingValue, UpSpotValue, ref gripperResult);
            if (res == false)
            {
                tb_LensGripperCentrePixelPosX.Text = string.Empty;
                tb_LensGripperCentrePixelPosY.Text = string.Empty;
                str = "上相机识别Lens夹爪中心失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GlobalFunction.updateStatusDelegate("上相机识别Lens夹爪中心成功", Enum_MachineStatus.NORMAL);
                tb_LensGripperCentrePixelPosX.Text = gripperResult.center.X.ToString("f3");
                tb_LensGripperCentrePixelPosY.Text = gripperResult.center.Y.ToString("f3");
            }
        }

        private void btn_LensGripToCenter_Click(object sender, EventArgs e)
        {
            bool res = false;
            string str = string.Empty;
            VisionResult.GripperResult gripperResult = new VisionResult.GripperResult();
            double UpRingValue = double.Parse(tb_LensGripUpRingLight.Text);
            double UpSpotValue = double.Parse(tb_LensGripUpSpotLight.Text);

            //上相机移动之前识别Lens夹爪中心
            res = GlobalFunction.ProcessFlow.VisionRecognizeLensGripper(UpRingValue, UpSpotValue, ref gripperResult);
            if (res == false)
            {
                tb_LensGripperCentrePixelPosX.Text = string.Empty;
                tb_LensGripperCentrePixelPosY.Text = string.Empty;
                str = "上相机识别Lens夹爪中心失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //移动上相机中心对准Lens夹爪中心
                double dx = (gripperResult.imagecenter.X - gripperResult.center.X) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
                double dy = (gripperResult.imagecenter.Y - gripperResult.center.Y) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.yscale;
                GlobalFunction.MotionTools.Motion_MoveX1Distance(-dx, false, false, true);
                GlobalFunction.MotionTools.Motion_MoveY1Distance(dy, false, false, true);

                //上相机移动之后再次识别Lens夹爪中心
                res = GlobalFunction.ProcessFlow.VisionRecognizeLensGripper(UpRingValue, UpSpotValue, ref gripperResult);
                if (res == false)
                {
                    str = "上相机识别Lens夹爪中心失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    GlobalFunction.updateStatusDelegate("上相机中心对准Lens夹爪中心成功", Enum_MachineStatus.NORMAL);
                    tb_LensGripperCentrePixelPosX.Text = gripperResult.center.X.ToString("f3");
                    tb_LensGripperCentrePixelPosY.Text = gripperResult.center.Y.ToString("f3");
                    tb_LensGripX1Pos2.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
                    tb_LensGripY1Pos2.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
                    tb_LensGripZ1Pos2.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
                }
            }
        }

        private void btn_AutoCalibrateLensGrip_Click(object sender, EventArgs e)
        {
            double posx1 = double.Parse(tb_LensGripX2Pos1.Text);
            double posy1 = double.Parse(tb_LensGripY2Pos1.Text);
            double posx2 = double.Parse(tb_LensGripX1Pos2.Text);
            double posy2 = double.Parse(tb_LensGripY1Pos2.Text);
            tb_LensGripOffsetX.Text = (posx1 + posx2).ToString("f3");
            tb_LensGripOffsetY.Text = (posy1 + posy2).ToString("f3");
        }

        private void btn_DispenserLoadPos1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请确认U1轴的点胶角度值无变化或者已重新标定过胶针", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tb_DispenserU1Pos1.Text = GlobalFunction.MotionTools.Motion_GetU1Pos().ToString("f3"); 
            }
            tb_DispenserX1Pos1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_DispenserY1Pos1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_DispenserZ1Pos1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");            
            tb_DispenserX2Pos1.Text = GlobalFunction.MotionTools.Motion_GetX2Pos().ToString("f3");
            tb_DispenserY2Pos1.Text = GlobalFunction.MotionTools.Motion_GetY2Pos().ToString("f3");
            tb_DispenserZ2Pos1.Text = GlobalFunction.MotionTools.Motion_GetZ2Pos().ToString("f3");
        }

        private void btn_DispenserMoveToPos1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请确保胶池防尘盖已取走", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;
            double posu = 0;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            posx = double.Parse(tb_DispenserX2Pos1.Text);
            posy = double.Parse(tb_DispenserY2Pos1.Text);
            posz = double.Parse(tb_DispenserZ2Pos1.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX2Y2Z2ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X2_Y2_Z2轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            posx = double.Parse(tb_DispenserX1Pos1.Text);
            posy = double.Parse(tb_DispenserY1Pos1.Text);
            posz = double.Parse(tb_DispenserZ1Pos1.Text);
            posu = double.Parse(tb_DispenserU1Pos1.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1U1ToLocation(posx, posy, posu, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_U1轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                MessageBox.Show("Z1轴运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_DispenserLoadPos2_Click(object sender, EventArgs e)
        {
            tb_DispenserX1Pos2.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_DispenserY1Pos2.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_DispenserZ1Pos2.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_DispenserMovetoPos2_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            double posx = double.Parse(tb_DispenserX1Pos2.Text);
            double posy = double.Parse(tb_DispenserY1Pos2.Text);
            double posz = double.Parse(tb_DispenserZ1Pos2.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_Z1轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_DispenserFind_Click(object sender, EventArgs e)
        {
            bool res = false;
            string str = string.Empty;
            VisionResult.EpoxyResult epoxyResult = new VisionResult.EpoxyResult();
            double UpRingValue = double.Parse(tb_DispenserUpRingLight.Text);
            double UpSpotValue = double.Parse(tb_DispenserUpSpotLight.Text);

            res = GlobalFunction.ProcessFlow.VisionRecognizeEpoxySpot(UpRingValue, UpSpotValue, ref epoxyResult);
            if (res == false)
            {
                tb_DispenserEpoxySpotSize.Text = string.Empty;
                str = "上相机识别胶斑失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                GlobalFunction.updateStatusDelegate("上相机识别胶斑成功", Enum_MachineStatus.NORMAL);
                double dia = epoxyResult.diameter * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
                str = "胶斑直径: " + dia.ToString("f3") + "mm";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                tb_DispenserEpoxySpotSize.Text = dia.ToString("f3");
            } 
        }

        private void btn_DispenserToCenter_Click(object sender, EventArgs e)
        {
            bool res = false;
            string str = string.Empty;
            VisionResult.EpoxyResult epoxyResult = new VisionResult.EpoxyResult();
            double UpRingValue = double.Parse(tb_DispenserUpRingLight.Text);
            double UpSpotValue = double.Parse(tb_DispenserUpSpotLight.Text);

            //上相机移动之前识别胶斑中心
            res = GlobalFunction.ProcessFlow.VisionRecognizeEpoxySpot(UpRingValue, UpSpotValue, ref epoxyResult);
            if (res == false)
            {
                tb_DispenserEpoxySpotSize.Text = string.Empty;
                str = "上相机识别胶斑失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //移动上相机中心对准胶斑中心
                double dx = (epoxyResult.imagecenter.X - epoxyResult.center.X) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
                double dy = (epoxyResult.imagecenter.Y - epoxyResult.center.Y) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.yscale;
                GlobalFunction.MotionTools.Motion_MoveX1Distance(-dx, false, false, true);
                GlobalFunction.MotionTools.Motion_MoveY1Distance(dy, false, false, true);

                //上相机移动之后再次识别胶斑中心
                res = GlobalFunction.ProcessFlow.VisionRecognizeEpoxySpot(UpRingValue, UpSpotValue, ref epoxyResult);
                if (res == false)
                {
                    str = "上相机识别胶斑失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    MessageBox.Show(str, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    GlobalFunction.updateStatusDelegate("上相机中心对准胶斑中心成功", Enum_MachineStatus.NORMAL);
                    double dia = epoxyResult.diameter * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
                    str = "胶斑直径: " + dia.ToString("f3") + "mm";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                    tb_DispenserEpoxySpotSize.Text = dia.ToString("f3");
                    tb_DispenserX1Pos2.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
                    tb_DispenserY1Pos2.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
                    tb_DispenserZ1Pos2.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
                }
            }
        }

        private void btn_AutoCalibrateDispenser_Click(object sender, EventArgs e)
        {
            double posx1 = double.Parse(tb_DispenserX1Pos1.Text);
            double posy1 = double.Parse(tb_DispenserY1Pos1.Text);
            double posx2 = double.Parse(tb_DispenserX1Pos2.Text);
            double posy2 = double.Parse(tb_DispenserY1Pos2.Text); 
            tb_DispenserOffsetX.Text = (posx1 - posx2).ToString("f3");
            tb_DispenserOffsetY.Text = (posy1 - posy2).ToString("f3");
        }

        private void btn_LoadDipEpoxyPos_Click(object sender, EventArgs e)
        {
            tb_DipEpoxyX1Pos.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_DipEpoxyY1Pos.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_DipEpoxyZ1Pos.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
            tb_DipEpoxyU1Pos.Text = GlobalFunction.MotionTools.Motion_GetU1Pos().ToString("f3");
            tb_AutoEpoxyDipZ1RealtimeHeight.Text = tb_DipEpoxyZ1Pos.Text;
        }

        private void btn_MoveToDipEpoxyPos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            double posx = double.Parse(tb_DipEpoxyX1Pos.Text);
            double posy = double.Parse(tb_DipEpoxyY1Pos.Text);
            double posz = double.Parse(tb_DipEpoxyZ1Pos.Text);
            double posu = double.Parse(tb_DipEpoxyU1Pos.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1U1ToLocation(posx, posy, posu, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_U1轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                MessageBox.Show("Z1轴运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_LoadWipeEpoxyPos_Click(object sender, EventArgs e)
        {
            tb_WipeEpoxyX1Pos.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            tb_WipeEpoxyY1Pos.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            tb_WipeEpoxyZ1Pos.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_MoveToWipeEpoxyPos_Click(object sender, EventArgs e)
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            double posx = double.Parse(tb_WipeEpoxyX1Pos.Text);
            double posy = double.Parse(tb_WipeEpoxyY1Pos.Text);
            double posz = double.Parse(tb_WipeEpoxyZ1Pos.Text);
            double posu = double.Parse(tb_WipeEpoxyU1Pos.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1U1ToLocation(posx, posy, posu, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_U1轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                MessageBox.Show("Z1轴运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void btn_EpoxyTest_Click(object sender, EventArgs e)
        {
            //自动点胶测试(蘸胶+点胶+识别胶斑+擦胶)
            EpoxyTestForm epoxyTest = new EpoxyTestForm();
            epoxyTest.TopLevel = true;
            epoxyTest.StartPosition = FormStartPosition.CenterScreen;
            epoxyTest.ShowDialog();

            //强制刷新一下Z1蘸胶高度值，有可能已发生变化
            tb_DipEpoxyZ1Pos.Text = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.Z1.ToString("f3");

            //刷新实时蘸胶高度
            tb_AutoEpoxyDipZ1RealtimeHeight.Text = GlobalParameters.processdata.realtimeEpoxyDipHeight.ToString("f3");
        }

        private void btn_LoadAutoEpoxyDipHeightLimitPos_Click(object sender, EventArgs e)
        {
            tb_AutoEpoxyDipZ1HeightLimit.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
        }

        private void btn_MoveToAutoEpoxyDipHeightLimitPos_Click(object sender, EventArgs e)
        {
            bool res = false;

            double posx = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.X1;
            double posy = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.Y1;            
            double posu = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.U1;
            double posz = double.Parse(tb_AutoEpoxyDipZ1HeightLimit.Text);
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1U1ToLocation(posx, posy, posu, true, true);
            if (res == false)
            {
                MessageBox.Show("X1_Y1_U1轴组合运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                MessageBox.Show("Z1轴运动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Modify_Click(object sender, EventArgs e)
        {
            string newName = tb_NewProductName.Text.Trim();
            string oldName = "";
            if (newName == "")
            {
                MessageBox.Show("产品名称不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (lb_ProductList.SelectedIndex==-1)
            {
                MessageBox.Show("请选择一个产品名称","警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                oldName = lb_ProductList.SelectedItem.ToString();
            }
            //Check whether file already exists.
            if (lb_ProductList.Items.Contains(newName))
            {
                MessageBox.Show("产品名称已存在，请重新命名", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string oldFilePath = GeneralFunction.GetConfigFilePath("ProductConfig\\") + oldName + ".xml";
            string newFilePath = GeneralFunction.GetConfigFilePath("ProductConfig\\") + newName + ".xml";
            if (File.Exists(oldFilePath))
            {

                File.Move(oldFilePath, newFilePath);
            }
            else
            {
                MessageBox.Show("产品工艺配置文件不存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            UpdateProductlist();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            bool res = false;
            string filename = tb_AddName.Text.Trim();
            if (filename == "")
            {
                MessageBox.Show("产品名称不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            //Check whether file already exists.
            if (lb_ProductList.Items.Contains(filename))
            {
                MessageBox.Show("产品名称已存在，请重新命名", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string filepath = GeneralFunction.GetConfigFilePath("ProductConfig\\" + filename + ".xml");
            res = STD_IGeneralTool.GeneralTool.TryToSave<ProductConfig>(GlobalParameters.productconfig, filepath);
            if (res)
            {
                MessageBox.Show("增加产品名称成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("增加产品名称失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //Update Product Name List
            UpdateProductlist();
        }

        private void Btn_Copy_Click(object sender, EventArgs e)
        {
            string copyName = tb_CopyName.Text.Trim();
            string oldName = "";
            if (copyName == "")
            {
                MessageBox.Show("产品名称不能为空", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (lb_ProductList.SelectedIndex == -1)
            {
                MessageBox.Show("请选择一个产品", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                oldName = lb_ProductList.SelectedItem.ToString();
            }

            //Check whether file already exists.
            if (lb_ProductList.Items.Contains(copyName))
            {
                MessageBox.Show("产品名称已存在，请重新命名", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string oldFilePath = GeneralFunction.GetConfigFilePath("ProductConfig\\") + oldName + ".xml";
            string copyFilePath = GeneralFunction.GetConfigFilePath("ProductConfig\\") + copyName + ".xml";
            if (File.Exists(oldFilePath))
            {

                File.Copy(oldFilePath, copyFilePath);
            }
            else
            {
                MessageBox.Show("产品工艺配置文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UpdateProductlist();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            string removeName = "";
            if (MessageBox.Show("是否删除当前所选产品名称", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (lb_ProductList.SelectedIndex == -1)
            {
                MessageBox.Show("请选择一个产品名称", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                removeName = lb_ProductList.SelectedItem.ToString();
            }

            if (removeName == GlobalParameters.systemconfig.ManageProductConfig.currentproduct)
            {
                MessageBox.Show("当前选择的产品名称正被使用中，无法删除", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string removeFilePath = GeneralFunction.GetConfigFilePath("ProductConfig\\") + removeName + ".xml";
            string backupFilePath = GeneralFunction.GetConfigFilePath("ProductConfig\\ProductConfigBackup\\") + removeName + ".xml";
            if (File.Exists(backupFilePath))
            {
                File.Delete(backupFilePath);
            }
            else
            {
                File.Move(removeFilePath, backupFilePath);
            }
            UpdateProductlist();
        }

        private void btn_Change_Click(object sender, EventArgs e)
        {
            bool res = false;
            string errMsg = "";
            string currentName = cmb_CurrentProduct.Text;
            string[] elementStr = new string[3];
            string newValue = "";
            string filepath = GeneralFunction.GetConfigFilePath("SystemConfig.xml");
            string prodFilepath = GeneralFunction.GetConfigFilePath("ProductConfig\\" + currentName + ".xml");
            if (File.Exists(prodFilepath) == false)
            {
                MessageBox.Show("产品工艺配置文件不存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            elementStr[0] = "manageproductConfig";
            elementStr[1] = "currentproduct";
            elementStr[2] = "";
            newValue = currentName;
            GeneralFunction.UpdateXmlFileElement(filepath, elementStr, newValue, ref errMsg);
            if (errMsg != "")
            {
                MessageBox.Show(errMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            res = GeneralFunction.LoadProductConfig(currentName + ".xml");
            if (res)
            {
                MessageBox.Show("更改产品名称成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("更改产品名称失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void UpdateProductlist()
        {
            string path = Application.StartupPath.ToString() + @"\Config\ProductConfig";
            List<FileInfo> productlist = GeneralFunction.GetFileInfos(path, ".xml");
            List<String> productnamelst = new List<string>();

            lb_ProductList.Items.Clear();
            foreach (FileInfo productname in productlist)
            {
                string product = productname.Name.Remove(productname.Name.LastIndexOf("."));
                lb_ProductList.Items.Add(product);
                productnamelst.Add(product);
                cmb_CurrentProduct.Items.Add(product);
            }
        }
    }
}