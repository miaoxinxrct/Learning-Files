using System;
using System.Drawing;
using System.Windows.Forms;
using Cognex.VisionPro;

namespace FocusingLensAligner
{
    public partial class EngineerForm : Form
    {
        int uvcount = 0;
        Timer uvtimer = new Timer();
        public EngineerForm()
        {
            InitializeComponent();
        }

        private void EngineerForm_Load(object sender, EventArgs e)
        {
            uvtimer.Interval = 1000;
            uvtimer.Enabled = false;
            tb_UVTimeLeft.ForeColor = Color.SpringGreen;
            if (GlobalParameters.systemconfig.InstrumentConfig.SMCCardMotion_Valid == true && GlobalParameters.HardwareInitialStatus.SMCCardMotion_InitialStatus == true)
            {
                PogoPin_In.Enabled = true;
                PogoPin_Out.Enabled = true;
                PogoPin_ResetMotion.Enabled = true;
            }
            else
            {
                PogoPin_In.Enabled = false;
                PogoPin_Out.Enabled = false;
                PogoPin_ResetMotion.Enabled = false;
            }
            if (GlobalParameters.systemconfig.InstrumentConfig.QRCodeScanner_Valid == false)
            {
                TriggleQRCodeScanner.Enabled = false;
            }
            if (GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus == false)
            {
                led_BoxGripperControllerOnline.BackgroundImage = Properties.Resources.Grayled;
                BoxGripperControllerOpen.Enabled = true;
                BoxGripperControllerClose.Enabled = false;
            }
            else
            {
                led_BoxGripperControllerOnline.BackgroundImage = Properties.Resources.Greenled;
                BoxGripperControllerOpen.Enabled = false;
                BoxGripperControllerClose.Enabled = true;
            }
            if (GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus == false)
            {
                led_LensGripperControllerOnline.BackgroundImage = Properties.Resources.Grayled;
                LensGripperControllerOpen.Enabled = true;
                LensGripperControllerClose.Enabled = false;
            }
            else
            {
                led_LensGripperControllerOnline.BackgroundImage = Properties.Resources.Greenled;
                LensGripperControllerOpen.Enabled = false;
                LensGripperControllerClose.Enabled = true;
            }
            if (GlobalParameters.HardwareInitialStatus.QRCodeScanner_InitialStatus == false)
            {
                led_QRCodeScannerOnline.BackgroundImage = Properties.Resources.Grayled;
                OpenQRCodeScanner.Enabled = true;
                CloseQRCodeScanner.Enabled = false;
            }
            else
            {
                led_QRCodeScannerOnline.BackgroundImage = Properties.Resources.Greenled;
                OpenQRCodeScanner.Enabled = false;
                CloseQRCodeScanner.Enabled = true;
            }

            IRCameraViewObject.Items.Clear();
            IRCameraViewObject.Items.AddRange(Enum.GetNames(typeof(Enum_DnCameraViewObject)));
            IRCameraViewObject.SelectedIndex = 0;

            this.ActiveControl = this.EngPnlClose;
        }

        private void EngPnlClose_Click(object sender, EventArgs e)
        {            
            this.Hide();
        }

        private void EngineerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var tmp in GlobalFunction.CameraTools.CameraTool.CameraHandle)
            {
                if (tmp.Value.CameraIsGrabing)
                {
                    tmp.Value.Camera_StopGrab();
                }
            }
            e.Cancel = true;
        }

        private void EngineerForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                timer_EngineerForm.Start();
            }
            else
            {
                timer_EngineerForm.Stop();
            }
        }

        private void LiveCamera_Click(object sender, EventArgs e)
        {
            //相机实时模式切换
            bool res = false;
            
            CheckBox chkbox = (CheckBox)sender;
            string camname = chkbox.Tag.ToString();

            if (chkbox.Checked)
            {
                if (camname == "DnCamera")
                {
                    //根据视野中需要观察的目标动态调整下相机工作条件
                    string ObjectName = IRCameraViewObject.SelectedItem.ToString();
                    Enum_DnCameraViewObject ObjectID = (Enum_DnCameraViewObject)Enum.Parse(typeof(Enum_DnCameraViewObject), ObjectName);
                    res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(ObjectID);                    
                    if (res == false)
                    {
                        return;
                    }
                }
                res = GlobalFunction.CameraTools.Camera_StartGrab(camname);
                if (camname == "DnCamera")
                {
                    GlobalParameters.flagassembly.downcameraliveonflag = true;
                }
                else
                {
                    GlobalParameters.flagassembly.upcameraliveonflag = true;
                }
            }
            else
            {
                res = GlobalFunction.CameraTools.Camera_StopGrab(camname);
                if (camname == "DnCamera")
                {
                    GlobalParameters.flagassembly.downcameraliveonflag = false;
                }
                else
                {
                    GlobalParameters.flagassembly.upcameraliveonflag = false;
                }
            }
        }

        private void btn_SaveImage_Click(object sender, EventArgs e)
        {
            //截取相机当前帧并保存至本地bmp格式图像文件
            Bitmap bitmapTemp = null;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "bmp files|*.bmp";
            dialog.AddExtension = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    foreach (var tmp in GlobalFunction.CameraTools.CameraTool.CameraHandle)
                    {
                        if (tmp.Value.CameraIsGrabing)
                        {
                            tmp.Value.Camera_Snap(ref bitmapTemp);
                            if ((GlobalParameters.flagassembly.downcameraliveonflag == true) && (GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild == true))
                            {
                                //国惠宽波段红外相机图像预处理
                                GlobalParameters.cogtoolblock_GhoptoIRCamera.Inputs[0].Value = new CogImage8Grey(bitmapTemp);
                                GlobalParameters.cogtoolblock_GhoptoIRCamera.Run();
                                ICogImage image = GlobalParameters.cogtoolblock_GhoptoIRCamera.Outputs[0].Value as Cognex.VisionPro.ICogImage;
                                bitmapTemp = new Bitmap(image.ToBitmap());
                            }
                            bitmapTemp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void MoveSingleAxis_Click(object sender, EventArgs e)
        {
            //单轴按选定步距执行相对运动
            int dir = 1;
            double distance = 0.0;
            string str = (string)((Button)sender).Name;
            string axisname = str.Substring(0, str.IndexOf("_"));
            string dirstr = ((Button)sender).Tag.ToString().Trim();
            if (dirstr == "Backward")
            {
                dir = -1;
            }
            else
            {
                dir = 1;
            }
            distance = dir * double.Parse(StepSize.Text.ToString().Trim());
            if (axisname == "PogoPin")
            {
                int retErrorCode = 0;
                GlobalFunction.SMCCardMotionTools.SMCCardMotion_MoveDistance(axisname, distance, true, ref retErrorCode);
            }
            else
            {
                GlobalFunction.MotionTools.Motion_MoveDistance(axisname, distance, false);
            }
        }

        private void Step_Angle_SelectedIndexChanged(object sender, EventArgs e)
        {
            //步距下拉选择项变更时，步距文本框输入内容同步更新
            StepSize.Text = StepAngle.SelectedItem.ToString();
        }

        private void PogoPinResetMotion_Click(object sender, EventArgs e)
        {
            //加电Pin针运动轴复位
            bool res = false;
            int retErrorCode = 0;

            string str = (string)((Button)sender).Name;
            string axisname = str.Substring(0, str.IndexOf("_"));            
            res = GlobalFunction.SMCCardMotionTools.SMCCardMotion_Initial(axisname, ref retErrorCode);
            if (res == true)
            {
                res = GlobalFunction.SMCCardMotionTools.SMCCardMotion_HomeMove(axisname, true, ref retErrorCode);
                if (res == true)
                {
                    //移动至PogoPin初始待料位置-------------------------------------需要修改位置参数
                    double Position = 0;
                    res = GlobalFunction.SMCCardMotionTools.SMCCardMotion_MoveToLocation(axisname, Position, true, ref retErrorCode);
                }
            }
        }

        private void btn_Z1UpToSafetyPosition_Click(object sender, EventArgs e)
        {
            //Z1轴上抬到安全位置
            bool res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
        }

        private void btn_Z2UpToSafetyPosition_Click(object sender, EventArgs e)
        {
            //Z2轴上抬到安全位置
            bool res = GlobalFunction.ProcessFlow.MotionAxisZ2MoveToSafetyPosition();
        }

        private void HomeSingleAxis_Click(object sender, EventArgs e)
        {
            //单轴Home操作
            bool res = false;

            string axisname = AxisID.SelectedItem.ToString().Trim();
            if (axisname == "PogoPin")
            {
                if (GlobalParameters.systemconfig.InstrumentConfig.SMCCardMotion_Valid == true && GlobalParameters.HardwareInitialStatus.SMCCardMotion_InitialStatus == true)
                {
                    int retErrorCode = 0;
                    res = GlobalFunction.SMCCardMotionTools.SMCCardMotion_HomeMove(axisname, true, ref retErrorCode);
                }
                else
                {
                    res = true;
                }
            }
            else
            {
                res = GlobalFunction.MotionTools.Motion_HomeMove(axisname, true);
            }

            if (res == false)
            {
                MessageBox.Show(axisname + "轴Home复位失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(axisname + "轴Home复位成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void HomeAllAxes_Click(object sender, EventArgs e)
        {
            //所有轴执行Home操作
            bool res = false;

            res = GlobalFunction.MotionTools.Motion_HomeAll();
            if (res == true)
            {
                if (GlobalParameters.systemconfig.InstrumentConfig.SMCCardMotion_Valid == true && GlobalParameters.HardwareInitialStatus.SMCCardMotion_InitialStatus == true)
                {
                    int retErrorCode = 0;
                    res = GlobalFunction.SMCCardMotionTools.SMCCardMotion_HomeMove("PogoPin", true, ref retErrorCode);
                }
            }

            if (res == true)
            {
                MessageBox.Show("运动轴系统Home复位成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("运动轴系统Home复位失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LightSourceCurrentChange_Scroll(object sender, EventArgs e)
        {
            //光源驱动电流拖动条控制
            bool lightstatus = false;
            string lightname = ((TrackBar)sender).Tag.ToString();
            int current = ((TrackBar)sender).Value;

            switch (lightname)
            {
                case "UpRing":
                    tb_UpRingCurrent.Text = trkbar_UpRingLight.Value.ToString();
                    lightstatus = chk_UpRingOnOff.Checked;
                    break;
                case "DnRing":
                    tb_DnRingCurrent.Text = trkbar_DnRingLight.Value.ToString();
                    lightstatus = chk_DnRingOnOff.Checked;
                    break;
                case "UpSpot":
                    tb_UpSpotCurrent.Text = trkbar_UpSpotLight.Value.ToString();
                    lightstatus = chk_UpSpotOnOff.Checked;
                    break;
                case "DnSpot":
                    tb_DnSpotCurrent.Text = trkbar_DnSpotLight.Value.ToString();
                    lightstatus = chk_DnSpotOnOff.Checked;
                    break;
            }

            if (lightstatus)
            {
                GlobalFunction.LightSourcesTools.LightSource_Open(lightname, current);
            }
        }

        private void LightSourceCurrentChange_Input(object sender, EventArgs e)
        {
            //光源驱动电流值直接输入改变
            bool lightstatus = false;
            string lightname = ((TextBox)sender).Tag.ToString();
            if (((TextBox)sender).Text.ToString() == "") return;
            int current = int.Parse(((TextBox)sender).Text.ToString());

            if (current < 0 || current > 600) return;
            switch (lightname)
            {
                case "UpRing":
                    trkbar_UpRingLight.Value = int.Parse(tb_UpRingCurrent.Text.ToString());
                    lightstatus = chk_UpRingOnOff.Checked;
                    break;
                case "DnRing":
                    trkbar_DnRingLight.Value = int.Parse(tb_DnRingCurrent.Text.ToString());
                    lightstatus = chk_UpRingOnOff.Checked;
                    break;
                case "UpSpot":
                    trkbar_UpSpotLight.Value = int.Parse(tb_UpSpotCurrent.Text.ToString());
                    lightstatus = chk_UpRingOnOff.Checked;
                    break;
                case "DnSpot":
                    trkbar_DnSpotLight.Value = int.Parse(tb_DnSpotCurrent.Text.ToString());
                    lightstatus = chk_UpRingOnOff.Checked;
                    break;
            }
            if (lightstatus)
            {
                GlobalFunction.LightSourcesTools.LightSource_Open(lightname, current);
            }
        }

        private void LightSourceOpenClose_Click(object sender, EventArgs e)
        {
            //打开或者关闭指定光源
            bool res = false;
            bool on = ((CheckBox)sender).Checked;
            string lightname = ((CheckBox)sender).Tag.ToString();
            if (on)
            {
                int current = 0;
                switch (lightname)
                {
                    case "UpRing":
                        current = int.Parse(tb_UpRingCurrent.Text.ToString());
                        break;
                    case "DnRing":
                        current = int.Parse(tb_DnRingCurrent.Text.ToString());
                        break;
                    case "UpSpot":
                        current = int.Parse(tb_UpSpotCurrent.Text.ToString());
                        break;
                    case "DnSpot":
                        current = int.Parse(tb_DnSpotCurrent.Text.ToString());
                        break;
                }
                res = GlobalFunction.LightSourcesTools.LightSource_Open(lightname, current);
                if (res == false)
                {
                    MessageBox.Show("打开光源" + lightname + "失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                res = GlobalFunction.LightSourcesTools.LightSource_Close(lightname);
                if (res == false)
                {
                    MessageBox.Show("关闭光源" + lightname + "失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SignalTowerLampSet_Click(object sender, EventArgs e)
        {
            //信号塔LED灯控制
            string linename = ((Button)sender).Tag.ToString().Trim();
            string status = ((Button)sender).Text.ToString();
            bool on = false;
            if (status == "On")
            {
                ((Button)sender).Text = "Off";
                on = true;
            }
            else
            {
                ((Button)sender).Text = "On";
                on = false;
            }
            GlobalFunction.IOControlTools.WriteDOLine(linename, on, false);
        }

        private void chk_UV_CheckedChanged(object sender, EventArgs e)
        {
            //UV控制器
            bool res = false;
            bool on = chk_UV.Checked;
            bool chl1enable = chkbox_Chl1.Checked;
            bool chl2enable = chkbox_Chl2.Checked;
            string uvname = "UVController";

            if (GlobalParameters.systemconfig.InstrumentConfig.UVController_Valid == true)
            {
                if (on)
                {
                    chk_UV.Text = "UV Off";
                    if (chl1enable)
                    {
                        res = GlobalFunction.UVControllerTools.UVController_StartSingleChannelUV(uvname, "Channel1");
                        if (res == false)
                        {
                            MessageBox.Show("打开UV通道#1失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (chl2enable)
                    {
                        res = GlobalFunction.UVControllerTools.UVController_StartSingleChannelUV(uvname, "Channel2");
                        if (res == false)
                        {
                            MessageBox.Show("打开UV通道#2失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        return;
                    }
                    if (res)
                    {
                        tb_UVTimeLeft.ForeColor = Color.Red;
                        chkbox_Chl1.Enabled = false;
                        chkbox_Chl2.Enabled = false;
                        uvtimer.Enabled = true;
                    }
                }
                else
                {
                    chk_UV.Text = "UV On";
                    tb_UVTimeLeft.ForeColor = Color.SpringGreen;
                    res = GlobalFunction.UVControllerTools.UVController_StopBothChannelUV(uvname, "Channel1", "Channel2");
                    if (res == false)
                    {
                        MessageBox.Show("关闭UV失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    chkbox_Chl1.Enabled = true;
                    chkbox_Chl2.Enabled = true;
                    uvtimer.Enabled = false;
                    //tb_UVTimeLeft.Text = GlobalFunction.productconfig.processConfig.uvsteplist[0].uvtime.ToString();
                    //uvcount = GlobalFunction.productconfig.processConfig.uvsteplist[0].uvtime;
                }
            }
        }

        private void uvtimer_Tick(object sender, EventArgs e)
        {
            if (uvcount >= 0)
            {
                tb_UVTimeLeft.Text = uvcount.ToString();
            }
            else
            {
                uvtimer.Enabled = false;
                tb_UVTimeLeft.ForeColor = Color.SpringGreen;
                //tb_UVTimeLeft.Text = GlobalFunction.productconfig.processConfig.uvsteplist[0].uvtime.ToString();
                chkbox_Chl1.Enabled = true;
                chkbox_Chl2.Enabled = true;
                //uvcount = GlobalFunction.productconfig.processConfig.uvsteplist[0].uvtime;
                chk_UV.Text = "UV On";
                chk_UV.Checked = false;
            }
            uvcount--;
        }

        private void timer_EngineerForm_Tick(object sender, EventArgs e)
        {
            //工程界面信息状态刷新定时器响应
            bool on = false;

            //各运动轴当前位置更新           
            PosX1.Text = GlobalFunction.MotionTools.Motion_GetX1Pos().ToString("f3");
            PosY1.Text = GlobalFunction.MotionTools.Motion_GetY1Pos().ToString("f3");
            PosZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
            PosU1.Text = GlobalFunction.MotionTools.Motion_GetU1Pos().ToString("f3");
            PosX2.Text = GlobalFunction.MotionTools.Motion_GetX2Pos().ToString("f3");
            PosY2.Text = GlobalFunction.MotionTools.Motion_GetY2Pos().ToString("f3");
            PosZ2.Text = GlobalFunction.MotionTools.Motion_GetZ2Pos().ToString("f3");
            if (GlobalParameters.systemconfig.InstrumentConfig.SMCCardMotion_Valid == true && GlobalParameters.HardwareInitialStatus.SMCCardMotion_InitialStatus == true)
            {
                double pos = 0;
                int retErrorCode = 0;
                bool res = GlobalFunction.SMCCardMotionTools.SMCCardMotion_GetPosition("PogoPin", ref pos, ref retErrorCode);
                if (res == true)
                {
                    PosPogoPin.Text = pos.ToString("f3");
                }
            }

            //Box夹爪气缸传感器状态更新
            GlobalFunction.IOControlTools.ReadDILine("BoxGripperUpSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_BoxGripperUp, on, 0);
            GlobalFunction.IOControlTools.ReadDILine("BoxGripperDownSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_BoxGripperDown, on, 0);

            //胶针气缸传感器状态更新
            GlobalFunction.IOControlTools.ReadDILine("EpoxyDipUpSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_EpoxyDipUp, on, 0);
            GlobalFunction.IOControlTools.ReadDILine("EpoxyDipDownSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_EpoxyDipDown, on, 0);

            //UV气缸传感器状态更新
            GlobalFunction.IOControlTools.ReadDILine("UVInSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_UVIn, on, 0);
            GlobalFunction.IOControlTools.ReadDILine("UVOutSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_UVOut, on, 0);

            //Nest抱爪气缸传感器状态更新
            GlobalFunction.IOControlTools.ReadDILine("NestPullOnSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_NestPullOn, on, 0);
            GlobalFunction.IOControlTools.ReadDILine("NestPullOffSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_NestPullOff, on, 0);

            //PogoPin上下运动气缸传感器状态更新
            GlobalFunction.IOControlTools.ReadDILine("PogoPinUpSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_PogoPinUp, on, 0);
            GlobalFunction.IOControlTools.ReadDILine("PogoPinDownSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_PogoPinDown, on, 0);

            //PogoPin前后运动气缸传感器状态更新
            GlobalFunction.IOControlTools.ReadDILine("PogoPinOutSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_PogoPinOut, on, 0);
            GlobalFunction.IOControlTools.ReadDILine("PogoPinInSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_PogoPinIn, on, 0);

            //PogoPin接触传感器状态更新
            GlobalFunction.IOControlTools.ReadDILine("PogoPinContactSensor", ref on);
            GeneralFunction.SetIOPictureBoxImage(led_PogoPinContact, on, 2);

            //夹爪压力传感器读值更新
            if (GlobalParameters.systemconfig.InstrumentConfig.ForceSensor_Valid == true)
            {
                if (chkbox_GripperPressure.Checked == true)
                {
                    double pressureValue = 0;
                    int errorCode = 0;

                    bool res = GlobalFunction.ForceSensorTools.ForceSensor_GetPressureValue("GripperPressureSensor", ref pressureValue, ref errorCode);
                    if (res == true)
                    {
                        tb_GripperPressureSensor.Text = Math.Abs(pressureValue).ToString();
                    }
                }
            }
        }

        private void switch_BoxGripperUpDown_CheckedChanged(object sender, EventArgs e)
        {
            //Box夹爪气缸控制
            bool Down = ((CheckBox)sender).Checked;
            if (Down)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            GlobalFunction.IOControlTools.BoxGripperUpDown(Down, false);
        }

        private void switch_EpoxyDipUpDown_CheckedChanged(object sender, EventArgs e)
        {
            //胶针气缸控制
            bool Down = ((CheckBox)sender).Checked;
            if (Down)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            GlobalFunction.IOControlTools.EpoxyDipUpDown(Down, false);
        }

        private void switch_UVInOut_CheckedChanged(object sender, EventArgs e)
        {
            //UV气缸控制
            bool Out = ((CheckBox)sender).Checked;
            if (Out)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            GlobalFunction.IOControlTools.UVInOut(Out, false);
        }

        private void switch_NestPinOnOff_CheckedChanged(object sender, EventArgs e)
        {
            //Nest侧顶气缸控制
            bool On = ((CheckBox)sender).Checked;
            if (On)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            GlobalFunction.IOControlTools.NestPinOnOff(On, false);
        }

        private void switch_NestPullOnOff_CheckedChanged(object sender, EventArgs e)
        {
            //Nest抱爪气缸测试
            bool On = ((CheckBox)sender).Checked;
            if (On)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            GlobalFunction.IOControlTools.NestPullOnOff(On, false);
        }

        private void switch_PogoPinUpDown_CheckedChanged(object sender, EventArgs e)
        {
            //加电Pin针上下运动气缸控制
            bool Down = ((CheckBox)sender).Checked;
            if (Down)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            GlobalFunction.IOControlTools.PogoPinUpDown(Down, false);
        }

        private void switch_PogoPinInOut_CheckedChanged(object sender, EventArgs e)
        {
            //加电Pin针前后运动气缸控制
            bool Out = ((CheckBox)sender).Checked;
            if (Out)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            GlobalFunction.IOControlTools.PogoPinInOut(Out, false);
        }

        private void switch_PogoPinClampOnOff_CheckedChanged(object sender, EventArgs e)
        {
            //加电Pin针合紧气缸控制
            bool On = ((CheckBox)sender).Checked;
            if (On)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            GlobalFunction.IOControlTools.PogoPinClampOnOff(On, false);
        }

        private void switch_NestVacuumOnOff_CheckedChanged(object sender, EventArgs e)
        {
            //Nest真空发生器控制
            bool On = ((CheckBox)sender).Checked;
            if (On)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            GlobalFunction.IOControlTools.NestVacuumOnOff(On, false);
        }

        private void switch_EpoxyWipeOpenClose_CheckedChanged(object sender, EventArgs e)
        {
            //擦胶气缸控制
            bool Close = ((CheckBox)sender).Checked;
            if (Close)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            GlobalFunction.IOControlTools.EpoxyWipeOpenClose(Close, false);
        }

        private void switch_EpoxyOnOff_CheckedChanged(object sender, EventArgs e)
        {
            //点胶机IO控制
            bool On = ((CheckBox)sender).Checked;
            if (On)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            GlobalFunction.IOControlTools.EpoxyOnOff(On, false);
        }

        private void switch_SafetyDoorUpDown_CheckedChanged(object sender, EventArgs e)
        {
            //防护门上下运动控制
            bool Up = ((CheckBox)sender).Checked;
            if (Up)
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchup;
            }
            else
            {
                ((CheckBox)sender).BackgroundImage = Properties.Resources.switchdown;
            }
            GlobalFunction.IOControlTools.SafetyDoorUpDown(Up, false);
        }

        private void BoxGripperControllerOpen_Click(object sender, EventArgs e)
        {
            //连接Box夹爪控制器
            int errorCode = 0;
            bool res = false;

            if (GlobalParameters.systemconfig.InstrumentConfig.IAIBoxGripper_Valid == true && GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus == false)
            {
                //连接Box夹爪控制器（包含打开通信端口、设置通信模式、清除控制器内部所有报警信号）
                res = GlobalFunction.IAIGripperTools.ElectricalGripper_OpenDevice("BoxGripper", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("连接Box夹爪控制器失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //检查Box夹爪驱动轴伺服开启/关闭状态
                    bool servoOnStatus = false;
                    res = GlobalFunction.IAIGripperTools.ElectricalGripper_GetServoOnStatus("BoxGripper", ref servoOnStatus, ref errorCode);
                    if (res == true)
                    {
                        if (servoOnStatus == false)
                        {
                            //Box夹爪驱动轴伺服开启
                            res = GlobalFunction.IAIGripperTools.ElectricalGripper_SetServoOnSwitch("BoxGripper", 1, ref errorCode);
                            if (res == false)
                            {
                                MessageBox.Show("Box夹爪电机伺服开启失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        led_BoxGripperControllerOnline.BackgroundImage = Properties.Resources.Greenled;
                        BoxGripperControllerOpen.Enabled = false;
                        BoxGripperControllerClose.Enabled = true;
                        GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus = true;
                    }
                }
            }
        }

        private void BoxGripperControllerClose_Click(object sender, EventArgs e)
        {
            //断开Box夹爪控制器
            int errorCode = 0;

            if (GlobalParameters.systemconfig.InstrumentConfig.IAIBoxGripper_Valid == true && GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus == true)
            {
                bool res = GlobalFunction.IAIGripperTools.ElectricalGripper_CloseDevice("BoxGripper", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("断开Box夹爪控制器失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    led_BoxGripperControllerOnline.BackgroundImage = Properties.Resources.Grayled;
                    BoxGripperControllerOpen.Enabled = true;
                    BoxGripperControllerClose.Enabled = false;
                    GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus = false;
                }
            }
        }

        private void BoxGripperHome_Click(object sender, EventArgs e)
        {
            //Box夹爪执行Home操作
            int errorCode = 0;

            if (GlobalParameters.systemconfig.InstrumentConfig.IAIBoxGripper_Valid == true  && GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus == true)
            {
                bool res = GlobalFunction.IAIGripperTools.ElectricalGripper_HomeMove("BoxGripper", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("Box夹爪复位失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                GlobalParameters.flagassembly.boxongripperflag = false;
            }
        }

        private void BoxGripperClose_Click(object sender, EventArgs e)
        {
            //Box夹爪执行夹紧操作
            int errorCode = 0;

            if (GlobalParameters.systemconfig.InstrumentConfig.IAIBoxGripper_Valid == true && GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus == true)
            {
                bool res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("BoxGripper", "BoxGripperClose", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("Box夹爪夹取失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                GlobalParameters.flagassembly.boxongripperflag = true;
            }
        }

        private void BoxGripperOpen_Click(object sender, EventArgs e)
        {
            //Box夹爪执行松开操作
            int errorCode = 0;

            if (GlobalParameters.systemconfig.InstrumentConfig.IAIBoxGripper_Valid == true && GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus == true)
            {
                bool res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("BoxGripper", "BoxGripperOpen", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("Box夹爪张开失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                GlobalParameters.flagassembly.boxongripperflag = false;
            }
        }

        private void LensGripperControllerOpen_Click(object sender, EventArgs e)
        {
            //连接Lens夹爪控制器
            int errorCode = 0;
            bool res = false;

            if (GlobalParameters.systemconfig.InstrumentConfig.IAILensGripper_Valid == true && GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus == false)
            {
                //连接Lens夹爪控制器（包含打开通信端口、设置通信模式、清除控制器内部所有报警信号）
                res = GlobalFunction.IAIGripperTools.ElectricalGripper_OpenDevice("LensGripper", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("连接Lens夹爪控制器失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //检查Lens夹爪驱动轴伺服开启/关闭状态
                    bool servoOnStatus = false;
                    res = GlobalFunction.IAIGripperTools.ElectricalGripper_GetServoOnStatus("LensGripper", ref servoOnStatus, ref errorCode);
                    if (res == true)
                    {
                        if (servoOnStatus == false)
                        {
                            //Lens夹爪驱动轴伺服开启
                            res = GlobalFunction.IAIGripperTools.ElectricalGripper_SetServoOnSwitch("LensGripper", 1, ref errorCode);
                            if (res == false)
                            {
                                MessageBox.Show("Lens夹爪电机伺服开启失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        led_LensGripperControllerOnline.BackgroundImage = Properties.Resources.Greenled;
                        LensGripperControllerOpen.Enabled = false;
                        LensGripperControllerClose.Enabled = true;
                        GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus = true;
                    }
                }
            }
        }

        private void LensGripperControllerClose_Click(object sender, EventArgs e)
        {
            //断开Lens夹爪控制器
            int errorCode = 0;

            if (GlobalParameters.systemconfig.InstrumentConfig.IAILensGripper_Valid == true && GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus == true)
            {
                bool res = GlobalFunction.IAIGripperTools.ElectricalGripper_CloseDevice("LensGripper", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("断开Lens夹爪控制器失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    led_LensGripperControllerOnline.BackgroundImage = Properties.Resources.Grayled;
                    LensGripperControllerOpen.Enabled = true;
                    LensGripperControllerClose.Enabled = false;
                    GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus = false;
                }
            }
        }

        private void LensGripperHome_Click(object sender, EventArgs e)
        {
            //Lens夹爪执行Home操作
            int errorCode = 0;

            if (GlobalParameters.systemconfig.InstrumentConfig.IAILensGripper_Valid == true && GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus == true)
            {
                bool res = GlobalFunction.IAIGripperTools.ElectricalGripper_HomeMove("LensGripper", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("Lens夹爪复位失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                GlobalParameters.flagassembly.lensongripperflag = false;
            }
        }

        private void LensGripperClose_Click(object sender, EventArgs e)
        {
            //Lens夹爪执行夹紧操作
            int errorCode = 0;

            if (GlobalParameters.systemconfig.InstrumentConfig.IAILensGripper_Valid == true && GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus == true)
            {
                bool res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("LensGripper", "LensGripperClose", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("Lens夹爪夹取失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                GlobalParameters.flagassembly.lensongripperflag = true;
            }
        }

        private void LensGrippeOpen_Click(object sender, EventArgs e)
        {
            //Lens夹爪执行松开操作
            int errorCode = 0;

            if (GlobalParameters.systemconfig.InstrumentConfig.IAILensGripper_Valid == true && GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus == true)
            {
                bool res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("LensGripper", "LensGripperOpen", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("Lens夹爪张开失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                GlobalParameters.flagassembly.lensongripperflag = false;
            }
        }

        private void TriggleQRCodeScanner_Click(object sender, EventArgs e)
        {
            //触发扫码器
            int errorCode = 0;
            string QRCode = string.Empty;

            if (GlobalParameters.systemconfig.InstrumentConfig.QRCodeScanner_Valid == true)
            {
                tb_QRCode.Text = "";
                bool res = GlobalFunction.QRCodeScannerTools.QRCodeScanner_ScanCode("DM70S", ref QRCode, ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("扫码器扫码失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    tb_QRCode.Text = QRCode;
                }
            }
        }

        private void OpenQRCodeScanner_Click(object sender, EventArgs e)
        {
            //连接扫码器
            int errorCode = 0;
            bool res = false;

            if (GlobalParameters.systemconfig.InstrumentConfig.QRCodeScanner_Valid == true && GlobalParameters.HardwareInitialStatus.QRCodeScanner_InitialStatus == false)
            {
                res = GlobalFunction.QRCodeScannerTools.QRCodeScanner_Initial("DM70S", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("连接扫码器失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    led_QRCodeScannerOnline.BackgroundImage = Properties.Resources.Greenled;
                    OpenQRCodeScanner.Enabled = false;
                    CloseQRCodeScanner.Enabled = true;
                    GlobalParameters.HardwareInitialStatus.QRCodeScanner_InitialStatus = true;
                }
            }
        }

        private void CloseQRCodeScanner_Click(object sender, EventArgs e)
        {
            //断开扫码器
            int errorCode = 0;
            bool res = false;

            if (GlobalParameters.systemconfig.InstrumentConfig.QRCodeScanner_Valid == true && GlobalParameters.HardwareInitialStatus.QRCodeScanner_InitialStatus == true)
            {
                res = GlobalFunction.QRCodeScannerTools.QRCodeScanner_UnInitial("DM70S", ref errorCode);
                if (res == false)
                {
                    MessageBox.Show("断开扫码器失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    led_QRCodeScannerOnline.BackgroundImage = Properties.Resources.Grayled;
                    OpenQRCodeScanner.Enabled = true;
                    CloseQRCodeScanner.Enabled = false;
                    GlobalParameters.HardwareInitialStatus.QRCodeScanner_InitialStatus = false;
                }
            }
        }
    }
}