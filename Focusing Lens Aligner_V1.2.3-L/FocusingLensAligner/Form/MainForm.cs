using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using STD_IFrameWork;
using STD_ISourceMeter;
using Cognex.VisionPro;
using Cognex.VisionPro.Dimensioning;

namespace FocusingLensAligner
{
    public partial class MainForm : Form
    {
        Form engFrm = null;
        ProductConfigForm productConfigForm = new ProductConfigForm();

        string formresult = null;

        //产品Box物料选择控件组
        CheckBox[,] boxloadtray = null;
        CheckBox[,] boxunloadtray = null;

        //Lens物料选择控件组
        CheckBox[] lensloadtray = null;

        //DC物料信息集合
        DCList dcCollection = new DCList();

        public MainForm()
        {
            InitializeComponent();
        }

        #region 主框架加载
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //中文模式其中FlagAssembly属于各种标志位bool量的数据，静态变量
                GlobalParameters.flagassembly.chineseflag = true;

                //创建主界面图像显示窗口接口
                GlobalFunction.CogRecordDisplay = cogRecordDisplay1;
                
                //Step 0 创建框架入口
                cFrameWork.CreateIntarnce(this);

                //Step 1 订阅框架的事件及进行监听
                cFrameWork.OnFrameworkMessage += cFrameWork_OnFrameworkMessage;

                //Step 2 设置客户需要初始化的个数
                cFrameWork.SetInitializationComponentNumber(20);

                bool res = false;
                int retErrorCode = 0;

                //调取系统配置文件
                res = GeneralFunction.LoadSystemConfig();
                if (res)
                {
                    cFrameWork.SetInitializationPanelMessage(string.Format("Load {0} Component Success", "System Config"));
                }
                else
                {
                    cFrameWork.SetInitializationPanelMessage(string.Format("Load {0} Component Fail", "System Config"));
                }

                //调取产品配置文件
                res = GeneralFunction.LoadProductConfig(GlobalParameters.systemconfig.ManageProductConfig.currentproduct + ".xml");
                if (res)
                {
                    cFrameWork.SetInitializationPanelMessage(string.Format("Load {0} Component Success", "Product Config"));
                }
                else
                {
                    cFrameWork.SetInitializationPanelMessage(string.Format("Load {0} Component Fail", "Product Config"));
                }

                //初始化运动轴系统
                GlobalParameters.HardwareInitialStatus.MotionCard_InitialStatus = false;
                res = GlobalFunction.MotionTools.Motion_Initial();
                if (res == true)
                {
                    GlobalParameters.HardwareInitialStatus.MotionCard_InitialStatus = true;
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Motion Controller"));
                }
                else
                {
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Motion Controller"));

                }

                //初始化IO系统
                GlobalParameters.HardwareInitialStatus.IOCard_InitialStatus = false;
                res = GlobalFunction.IOControlTools.InitialDAQ();
                if (res == true)
                {
                    GlobalParameters.HardwareInitialStatus.IOCard_InitialStatus = true;
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "IO"));
                }
                else
                {
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "IO"));
                }

                //初始化相机系统
                GlobalParameters.HardwareInitialStatus.Camera_InitialStatus = false;
                res = GlobalFunction.CameraTools.Camera_Initial();
                if (res == true)
                {
                    GlobalFunction.CameraTools.Camera_SetExposure("DnCamera", GlobalParameters.systemconfig.IRCameraCaptureCardConfig.ViewBoxWindowBrightness);
                    GlobalFunction.CameraTools.Camera_SetGain("DnCamera", GlobalParameters.systemconfig.IRCameraCaptureCardConfig.ViewBoxWindowContrast);
                    GlobalParameters.HardwareInitialStatus.Camera_InitialStatus = true;
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Camera"));
                }
                else
                {
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Camera"));
                }

                //初始化国惠宽波段红外相机
                GlobalParameters.HardwareInitialStatus.GhoptoIRCamera_InitialStatus = false;
                if (GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild == true)
                {
                    GlobalFunction.GhoptoIRCameraTools.InitSDK();
                    res = GlobalFunction.GhoptoIRCameraTools.OpenCamera(ref GlobalParameters.GhoptoIRCameraHandle);
                    if (res == true)
                    {
                        GlobalParameters.HardwareInitialStatus.GhoptoIRCamera_InitialStatus = true;
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Ghopto IR Camera"));
                        //注：国惠宽波段红外相机在初始化时需要做一次快门校正操作，后续不再需要（频繁操作会影响相机内部快门机械寿命）
                        res = GlobalFunction.GhoptoIRCameraTools.AdjustParameter(GlobalParameters.GhoptoIRCameraHandle, Enum_DnCameraViewObject.Window, true);
                    }
                    else
                    {
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Ghopto IR Camera"));
                    }
                }

                //初始化视觉检测
                if (GlobalFunction.ImageProcessTools.ImageProcessInitial())
                {
                    GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus = true;
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Cognex Image Process Tool"));
                }
                else
                {
                    GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus = false;
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Cognex Image Process Tool"));
                }

                //初始化相机显示事件接口
                if (GlobalFunction.CameraTools != null)
                {
                    foreach (var tmp in GlobalFunction.CameraTools.CameraTool.CameraHandle)
                    {
                        tmp.Value.OnFrame += Image_OnFrame;
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Camera Display"));
                    }
                }
                else
                {
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Camera Display"));
                }

                //初始化光源系统
                GlobalParameters.HardwareInitialStatus.LightSources_InitialStatus = false;
                res = GlobalFunction.LightSourcesTools.LightSource_Initial();
                if (res == true)
                {
                    GlobalParameters.HardwareInitialStatus.LightSources_InitialStatus = true;
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Light Controller"));
                }
                else
                {
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Light Controller"));
                }

                //初始化IAI夹爪控制器
                GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus = false;
                GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus = false;
                if ((GlobalParameters.systemconfig.InstrumentConfig.IAIBoxGripper_Valid == true) || (GlobalParameters.systemconfig.InstrumentConfig.IAILensGripper_Valid == true))
                {
                    res = GlobalFunction.IAIGripperTools.ElectricalGripper_Initial(ref retErrorCode);
                    if (res == true)
                    {
                        if (GlobalParameters.systemconfig.InstrumentConfig.IAIBoxGripper_Valid == true)
                        {
                            res = GlobalFunction.IAIGripperTools.ElectricalGripper_SetServoOnSwitch("BoxGripper", 1, ref retErrorCode);
                            if (res == true)
                            {
                                GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus = true;
                                cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Box Gripper Controller"));
                            }
                        }
                        if (GlobalParameters.systemconfig.InstrumentConfig.IAILensGripper_Valid == true)
                        {
                            res = GlobalFunction.IAIGripperTools.ElectricalGripper_SetServoOnSwitch("LensGripper", 1, ref retErrorCode);
                            if (res == true)
                            {

                                GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus = true;
                                cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Lens Gripper Controller"));
                            }
                        }
                    }
                    else
                    {
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Box & Lens Gripper Controller"));
                    }
                }

                //初始化压力传感器
                GlobalParameters.HardwareInitialStatus.ForceSensor_InitialStatus = false;
                if (GlobalParameters.systemconfig.InstrumentConfig.ForceSensor_Valid == true)
                {
                    res = GlobalFunction.ForceSensorTools.ForceSensor_Initial("GripperPressureSensor", ref retErrorCode);
                    if (res == true)
                    {
                        GlobalParameters.HardwareInitialStatus.ForceSensor_InitialStatus = true;
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Pressure Sensor"));
                    }
                    else
                    {
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Pressure Sensor"));
                    }
                }

                //初始化UV系统
                GlobalParameters.HardwareInitialStatus.UVController_InitialStatus = false;
                if (GlobalParameters.systemconfig.InstrumentConfig.UVController_Valid == true)
                {
                    res = GlobalFunction.UVControllerTools.UVController_Initial();
                    if (res == true)
                    {
                        GlobalParameters.HardwareInitialStatus.UVController_InitialStatus = true;
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "UV Controller"));
                    }
                    else
                    {
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "UV Controller"));
                    }
                }

                //初始化加电源表
                GlobalParameters.HardwareInitialStatus.Keithley2401_1_InitialStatus = false;
                GlobalParameters.HardwareInitialStatus.Keithley2401_2_InitialStatus = false;
                GlobalParameters.HardwareInitialStatus.Keithley2401_3_InitialStatus = false;
                GlobalParameters.HardwareInitialStatus.Keithley2401_4_InitialStatus = false;
                GlobalParameters.HardwareInitialStatus.KeySightE364x_InitialStatus = false;
                res = GlobalFunction.SourceMetertools.InitialSourceMeter();
                if (res == true)
                {
                    if (GlobalParameters.systemconfig.InstrumentConfig.Keithley2401_1_Valid == true)
                    {
                        if (GlobalFunction.SourceMetertools.Keilthly240x_SetSourceTerminal("Keithley2401-1", true))
                        {
                            GlobalParameters.HardwareInitialStatus.Keithley2401_1_InitialStatus = true;
                            cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Source Meter"));
                        }
                        else
                        {
                            cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Source Meter"));
                        }
                    }
                    if (GlobalParameters.systemconfig.InstrumentConfig.Keithley2401_2_Valid == true)
                    {
                        if (GlobalFunction.SourceMetertools.Keilthly240x_SetSourceTerminal("Keithley2401-2", true))
                        {
                            GlobalParameters.HardwareInitialStatus.Keithley2401_2_InitialStatus = true;
                            cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Source Meter"));
                        }
                        else
                        {
                            cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Source Meter"));
                        }
                    }
                    if (GlobalParameters.systemconfig.InstrumentConfig.Keithley2401_3_Valid == true)
                    {
                        if (GlobalFunction.SourceMetertools.Keilthly240x_SetSourceTerminal("Keithley2401-3", true))
                        {
                            GlobalParameters.HardwareInitialStatus.Keithley2401_3_InitialStatus = true;
                            cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Source Meter"));
                        }
                        else
                        {
                            cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Source Meter"));
                        }
                    }
                    if (GlobalParameters.systemconfig.InstrumentConfig.Keithley2401_4_Valid == true)
                    {
                        if (GlobalFunction.SourceMetertools.Keilthly240x_SetSourceTerminal("Keithley2401-4", true))
                        {
                            GlobalParameters.HardwareInitialStatus.Keithley2401_4_InitialStatus = true;
                            cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Source Meter"));
                        }
                        else
                        {
                            cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Source Meter"));
                        }
                    }
                    if (GlobalParameters.systemconfig.InstrumentConfig.KeySightE364x_Valid == true)
                    {
                        GlobalParameters.HardwareInitialStatus.KeySightE364x_InitialStatus = true;
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "Source Meter"));
                    }
                }
                else
                {
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "Source Meter"));
                }

                //初始化二维码扫码器
                GlobalParameters.HardwareInitialStatus.QRCodeScanner_InitialStatus = false;
                if (GlobalParameters.systemconfig.InstrumentConfig.QRCodeScanner_Valid == true)
                {
                    res = GlobalFunction.QRCodeScannerTools.QRCodeScanner_Initial("DM70S", ref retErrorCode);
                    if (res == true)
                    {
                        GlobalParameters.HardwareInitialStatus.QRCodeScanner_InitialStatus = true;
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "QR Code Scanner"));
                    }
                    else
                    {
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "QR Code Scanner"));
                    }
                }

                //初始化FI2CUSB设备
                GlobalParameters.HardwareInitialStatus.FI2CUSB_InitialStatus = false;
                if (GlobalParameters.systemconfig.InstrumentConfig.FI2CUSB_Valid == true)
                {
                    res = GlobalFunction.FI2CUSBTools.FI2CUSB_Initial();
                    if (res == true)
                    {
                        GlobalParameters.HardwareInitialStatus.FI2CUSB_InitialStatus = true;
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "FI2CUSB Device"));
                    }
                    else
                    {
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "FI2CUSB Device"));
                    }
                }

                //初始化SMC卡片电机轴
                GlobalParameters.HardwareInitialStatus.SMCCardMotion_InitialStatus = false;
                if (GlobalParameters.systemconfig.InstrumentConfig.SMCCardMotion_Valid == true)
                {
                    res = GlobalFunction.SMCCardMotionTools.SMCCardMotion_Initial("PogoPin", ref retErrorCode);
                    if (res == true)
                    {
                        GlobalParameters.HardwareInitialStatus.SMCCardMotion_InitialStatus = true;
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "SMC Card Motion"));
                    }
                    else
                    {
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "SMC Card Motion"));
                    }
                }

                //初始化MES系统
                GlobalParameters.HardwareInitialStatus.MESFunctionTools_InitialStatus = false;
                if (GlobalParameters.productconfig.processConfig.mesEnable == true)
                {
                    res = GlobalFunction.MESTools.MES_Initial();
                    if (res == true)
                    {
                        cFrameWork.UserAdmin.MES = GlobalFunction.MESTools.mesmain;
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Success", "MES"));
                        GeneralFunction.LoadMESConfig();
                        GlobalParameters.HardwareInitialStatus.MESFunctionTools_InitialStatus = true;
                    }
                    else
                    {
                        cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0} Component Fail", "MES"));
                    }
                }

                //Step 4 加载用户界面控件
                for (int i = 0; i < STD_IFrameWork.cFrameWork.InitializationComponentNumber; i++)
                {
                    cFrameWork.SetInitializationPanelMessage(string.Format("Initial {0}  Compent", i.ToString()));
                }

                cFrameWork.SetTitleInfo("机台初始化成功，等待生产！！", STD_IFrameWork.eTitleType.Completed);

                //加载产品目录
                res = LoadProductNametoMainForm();
                if (res)
                {
                    //设置当前产品
                    cmb_ListProduct.SelectedIndex = cmb_ListProduct.Items.IndexOf(GlobalParameters.systemconfig.ManageProductConfig.currentproduct);
                }
                else
                {
                    MessageBox.Show("Load Product Name Fail!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //加载主界面上DCList信息框中DC物料本地保存信息
            if (GlobalParameters.productconfig.processConfig.mesEnable == true)
            {
                LoadDCListInformation();
            }

            //加载主界面上Box料盘勾选控件
            LoadBoxTrayStruct();

            //加载主界面上Lens料盘勾选控件
            LoadLensTrayStruct();

            //清除产品Box物料盘收料区状态
            ResetAllBoxUnloadStatus();

            //清除产品Box耦合组装最终结果
            ResetAllBoxFinalResult();

            //加载主界面上当前产品的工作流程
            LoadWorkflow();

            //加载主界面通道启用配置
            LoadChannelValidConfig();

            //初始化实时胶针蘸胶信息
            GlobalParameters.processdata.realtimeEpoxyDipHeight = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.Z1;
            GlobalParameters.processdata.realtimeEpoxyDipCount = 0;

            //建立主界面信息窗口记录刷新委托事件接口
            GlobalFunction.updateStatusDelegate = updateMachineStatus;
            lb_SystemInfo.DrawMode = DrawMode.OwnerDrawFixed;

            //建立主界面产品Box发料盘状态刷新委托事件接口
            GlobalFunction.updateBoxLoadTrayStatusDelegate = updateBoxLoadTrayStatus;

            //建立主界面产品Box收料盘状态刷新委托事件接口
            GlobalFunction.updateBoxUnloadTrayStatusDelegate = updateBoxUnloadTrayStatus;

            //建立主界面产品Lens料盘状态刷新委托事件接口
            GlobalFunction.updateLensLoadTrayStatusDelegate = updateLensLoadTrayStatus;

            //建立主界面加电源表窗口电压读数刷新委托事件接口
            GlobalFunction.updateSourceMeterVoltageReadingDelegate = updateSourceMeterVoltageReading;

            //建立主界面加电源表窗口电流读数刷新委托事件接口
            GlobalFunction.updateSourceMeterCurrentReadingDelegate = updateSourceMeterCurrentReading;

            //建立主界面产品SN信息刷新委托事件接口
            GlobalFunction.updateProdutSNDelegate = updateProdutSN;

            //建立主界面DC物料实时状态获取委托事件接口
            GlobalFunction.getDCTreeStatusDelegate = getDCTreeStatus;

            //建立主界面压力传感器读值刷新委托事件接口
            GlobalFunction.updatePressureForceReadingDelegate = updatePressureForceReading;

            //建立主界面机台状态指示Lamp刷新委托事件接口
            GlobalFunction.updateRunStatusLampDelegate = updateRunStatusLamp;

            //锁定主界面相关控件(执行过轴复位后才会解锁)
            LockMainForm(true);

            //指定主界面“轴复位”按钮为启动后的活动按钮
            Button_HomeAxes.Enabled = true;
            this.ActiveControl = this.Button_HomeAxes;

            //开启主界面后台状态监控定时器
            timer_MainForm.Enabled = true;

            //开启主界面后台DCList信息刷新定时器
            DCTimer.Enabled = GlobalParameters.productconfig.processConfig.mesEnable;

            //显示当前产品名称
            cFrameWork.ProductInfor = GlobalParameters.systemconfig.ManageProductConfig.currentproduct;

            //获取机台编号信息
            GlobalParameters.systemOperationInfo.machineid = cFrameWork.cFrameWorkParameter.RigName;

            //获取当前软件执行文件的版本号
            Version Ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            GlobalParameters.systemOperationInfo.swversion = Ver.Major.ToString() + "." + Ver.Minor.ToString() + "." + Ver.Build.ToString();

            //建立主界面User下拉菜单中User LogIn/Out选项的点击响应事件
            ToolStripMenuItem userToolStripMenuItem = cFrameWork.FindTargetMenu("User Log In/Out");
            userToolStripMenuItem.Click += UserToolStripMenuItem_Click;

            //建立全自动流程线程接口
            GlobalParameters.mainThread = new Thread(Thread_AutoRunWorkFlow);

            //初始化当前产品工艺数据
            GlobalFunction.ProcessFlow.StepFlow_InitializeFinalProcessData();

            //复位工作流程各运行中间标志
            GlobalFunction.ProcessFlow.ResetWorkFlowFlag();

            //机台初始状态为停机状态
            GlobalFunction.updateRunStatusLampDelegate(Enum_RunStatus.RUN_STOP);
        }
        #endregion

        #region 主框架卸载
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //关闭信号塔所有指示灯
            GlobalFunction.IOControlTools.SetSignalTowerLightStatus(Enum_SignalTowerLamp.AllTurnOff);

            //卸载硬件占用资源
            ReleaseHardwareResource();
        }
        #endregion

        #region 主框架事件信息记录
        bool cFrameWork_OnFrameworkMessage(STD_ILogManagement.LogEventArgs args)
        {
            lb_SystemInfo.Invoke(new Action(() =>
            {
                lb_SystemInfo.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " >>>" + string.Format("{0}:{1}", new object[] { args.Description, args.ReturnCode }));

            }));
            return true;
        }
        #endregion

        #region 释放硬件占用资源
        private void ReleaseHardwareResource()
        {
            int retErrorCode = 0;
            bool res = false;

            //卸载运动轴系统
            res = GlobalFunction.MotionTools.Motion_UnInitial();

            //卸载IO系统
            res = GlobalFunction.IOControlTools.CloseDAQ();

            //卸载相机系统
            res = GlobalFunction.CameraTools.Camera_CloseAll();

            //卸载国惠宽波段红外相机
            if ((GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild == true) && (GlobalParameters.HardwareInitialStatus.GhoptoIRCamera_InitialStatus = true))
            {
                res = GlobalFunction.GhoptoIRCameraTools.CloseCamera(GlobalParameters.GhoptoIRCameraHandle);
                GlobalFunction.GhoptoIRCameraTools.UnInitSDK();
            }

            //卸载IAI夹爪控制器
            if ((GlobalParameters.systemconfig.InstrumentConfig.IAIBoxGripper_Valid == true) || (GlobalParameters.systemconfig.InstrumentConfig.IAILensGripper_Valid == true))
            {
                res = GlobalFunction.IAIGripperTools.ElectricalGripper_UnInitial(ref retErrorCode);
            }

            //卸载光源控制器
            res = GlobalFunction.LightSourcesTools.LightSource_CloseAll();

            //卸载压力传感器
            if (GlobalParameters.systemconfig.InstrumentConfig.ForceSensor_Valid == true)
            {
                res = GlobalFunction.ForceSensorTools.ForceSensor_UnInitial("GripperPressureSensor", ref retErrorCode);
            }

            //卸载UV固化机
            if (GlobalParameters.systemconfig.InstrumentConfig.UVController_Valid == true)
            {
                res = GlobalFunction.UVControllerTools.UVController_UnInitial();
            }

            //卸载二维码扫码器
            if (GlobalParameters.systemconfig.InstrumentConfig.QRCodeScanner_Valid == true)
            {
                res = GlobalFunction.QRCodeScannerTools.QRCodeScanner_UnInitial("DM70S", ref retErrorCode);
            }

            //卸载FI2CUSB设备
            if (GlobalParameters.systemconfig.InstrumentConfig.FI2CUSB_Valid == true)
            {
                res = GlobalFunction.FI2CUSBTools.FI2CUSB_Close();
            }

            //卸载SMC卡片电机轴
            if (GlobalParameters.systemconfig.InstrumentConfig.SMCCardMotion_Valid == true)
            {
                res = GlobalFunction.SMCCardMotionTools.SMCCardMotion_UnInitial("PogoPin", ref retErrorCode);
            }
        }
        #endregion

        #region 加载主界面上DCList信息框中DC物料本地保存信息
        private void LoadDCListInformation()
        {
            bool res = true;
            string errmsg = string.Empty;
            string pn = string.Empty;
            string timeStr = string.Empty;
            int validTime = 0;

            GlobalFunction.MESTools.MES_LoadDCList(ref dcCollection);
            lv_DCInfo.Items.Clear();

            for (int i = 0; i < dcCollection.dcinfolist.Count; i++)
            {
                res = GlobalFunction.MESTools.MES_GetPNByDC(dcCollection.dcinfolist[i].dcNum, ref pn, ref errmsg);
                if (res == false)
                {
                    MessageBox.Show(errmsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                res = GlobalFunction.MESTools.MES_GetValidTimeByDC(dcCollection.dcinfolist[i].dcNum, ref timeStr, ref errmsg);
                if (res == false)
                {
                    MessageBox.Show(errmsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ListViewItem lvItem = new ListViewItem();
                lvItem.UseItemStyleForSubItems = false;
                lvItem.Text = dcCollection.dcinfolist[i].dcNum;
                lvItem.SubItems.Add(pn);
                if (int.Parse(timeStr) <= 0)
                {
                    timeStr = "0";
                }
                lvItem.SubItems.Add(timeStr);

                lv_DCInfo.Items.Add(lvItem);
                validTime = int.Parse(timeStr);
                if (validTime <= 0)
                {
                    lv_DCInfo.Items[i].SubItems[2].BackColor = Color.Red;
                }
                else if (validTime > 0 || validTime < GlobalParameters.productconfig.processConfig.remindTime)
                {
                    lv_DCInfo.Items[i].SubItems[2].BackColor = Color.Yellow;
                }
                else
                {
                    lv_DCInfo.Items[i].SubItems[2].BackColor = Color.Black;
                }
            }
        }
        #endregion

        #region 加载主界面上Box料盘勾选控件
        private void LoadBoxTrayStruct()
        {
            int boxtrayrowcount = GlobalParameters.productconfig.boxlensConfig.boxTray.rowcount;
            int boxtraycolcount = GlobalParameters.productconfig.boxlensConfig.boxTray.colcount;
            boxloadtray = new CheckBox[boxtrayrowcount, boxtraycolcount];
            boxunloadtray = new CheckBox[boxtrayrowcount, boxtraycolcount];
            ComboBox_SelectedBoxIndex.Items.Clear();

            for (int i = 0; i < boxtrayrowcount; i++)
            {
                for (int j = 0; j < boxtraycolcount; j++)
                {
                    //索引号：从1开始往上递增
                    int index = i * boxtraycolcount + j + 1;

                    //加载Box发料区控件
                    boxloadtray[i, j] = new CheckBox();
                    boxloadtray[i, j].Appearance = Appearance.Button;
                    boxloadtray[i, j].Name = "CheckBox_BoxLoad" + index.ToString();
                    boxloadtray[i, j].Tag = index;
                    boxloadtray[i, j].Text = index.ToString();
                    boxloadtray[i, j].TextAlign = ContentAlignment.MiddleCenter;
                    boxloadtray[i, j].Font = new Font(boxloadtray[i, j].Font.Name, 6, boxloadtray[i, j].Font.Style, boxloadtray[i, j].Font.Unit);
                    boxloadtray[i, j].FlatStyle = FlatStyle.Popup;
                    boxloadtray[i, j].ForeColor = SystemColors.ControlText;
                    boxloadtray[i, j].BackColor = SystemColors.Control;
                    boxloadtray[i, j].Height = 41;
                    boxloadtray[i, j].Width = 22;
                    boxloadtray[i, j].Location = new Point(204 - j * 32, 8 + i * 53);
                    boxloadtray[i, j].Enabled = true;
                    boxloadtray[i, j].Click += new EventHandler(CheckBox_BoxLoad_CheckedChanged);
                    pnl_BoxTray.Controls.Add(boxloadtray[i, j]);

                    //加载Box收料区控件
                    boxunloadtray[i, j] = new CheckBox();
                    boxunloadtray[i, j].Appearance = Appearance.Button;
                    boxunloadtray[i, j].Name = "CheckBox_BoxUnLoad" + index.ToString();
                    boxunloadtray[i, j].Tag = index;
                    boxunloadtray[i, j].FlatStyle = FlatStyle.Standard;
                    boxunloadtray[i, j].BackColor = SystemColors.Control;
                    boxunloadtray[i, j].Height = 41;
                    boxunloadtray[i, j].Width = 22;
                    boxunloadtray[i, j].Location = new Point(81 - j * 32, 8 + i * 53);
                    boxunloadtray[i, j].Enabled = false;
                    pnl_BoxTray.Controls.Add(boxunloadtray[i, j]);

                    //初始化Box发料盘状态参数
                    GlobalParameters.processdata.boxload[i, j].exists = false;
                    GlobalParameters.processdata.boxload[i, j].index = Convert.ToInt32(boxloadtray[i, j].Tag);
                    GlobalParameters.processdata.boxload[i, j].row = i;
                    GlobalParameters.processdata.boxload[i, j].col = j;
                    GlobalParameters.processdata.boxload[i, j].bechecked = false;

                    //初始化Box收料盘状态参数
                    GlobalParameters.processdata.boxunload[i, j].exists = false;
                    GlobalParameters.processdata.boxunload[i, j].index = Convert.ToInt32(boxunloadtray[i, j].Tag);
                    GlobalParameters.processdata.boxunload[i, j].row = i;
                    GlobalParameters.processdata.boxunload[i, j].col = j;

                    //同步加载主界面工作流程面板上Box选择控件的下拉选项
                    ComboBox_SelectedBoxIndex.Items.Add(index);
                    ComboBox_SelectedBoxIndex.SelectedIndex = 0;
                }
            }
        }
        #endregion

        #region 加载主界面上Lens料盘勾选控件
        private void LoadLensTrayStruct()
        {
            GlobalParameters.productconfig.boxlensConfig.lensTray.colcount = 1;
            int lenstrayrowcount = GlobalParameters.productconfig.boxlensConfig.lensTray.rowcount;
            int lenstraycolcount = GlobalParameters.productconfig.boxlensConfig.lensTray.colcount;
            lensloadtray = new CheckBox[lenstrayrowcount];
            Label[] labellensload = new Label[lenstrayrowcount];
            ComboBox_SelectedLensIndex.Items.Clear();

            for (int i = 0; i < lenstrayrowcount; i++)
            {
                //索引号：从1开始往上递增
                int index = i * lenstraycolcount + 1;

                //加载Lens发料区控件
                lensloadtray[i] = new CheckBox();
                lensloadtray[i].Appearance = Appearance.Button;
                lensloadtray[i].Name = "CheckBox_LensLoad" + index.ToString();
                lensloadtray[i].Tag = index;
                lensloadtray[i].FlatStyle = FlatStyle.Popup;
                lensloadtray[i].ForeColor = SystemColors.ControlText;
                lensloadtray[i].BackColor = SystemColors.Control;
                lensloadtray[i].Height = 14;
                lensloadtray[i].Width = 14;
                lensloadtray[i].Location = new Point(200 - i * 16, 8 + i * 17);
                lensloadtray[i].Enabled = true;
                lensloadtray[i].Click += new EventHandler(CheckBox_LensLoad_CheckedChanged);
                pnl_LensTray.Controls.Add(lensloadtray[i]);

                labellensload[i] = new Label();
                labellensload[i].Height = 14;
                labellensload[i].Width = 14;
                labellensload[i].Text = index.ToString();
                labellensload[i].TextAlign = ContentAlignment.MiddleLeft;
                labellensload[i].ForeColor = SystemColors.ControlText;
                labellensload[i].Font = new Font(labellensload[i].Font.Name, 6, labellensload[i].Font.Style, labellensload[i].Font.Unit);
                labellensload[i].Location = new Point(216 - i * 16, 8 + i * 17);
                pnl_LensTray.Controls.Add(labellensload[i]);

                //初始化Lens发料盘状态参数
                GlobalParameters.processdata.lensload[i].exists = false;
                GlobalParameters.processdata.lensload[i].index = Convert.ToInt32(lensloadtray[i].Tag);
                GlobalParameters.processdata.lensload[i].row = i;
                GlobalParameters.processdata.lensload[i].col = 0;
                GlobalParameters.processdata.lensload[i].bechecked = false;

                //同步加载主界面工作流程面板上Lens选择控件的下拉选项
                ComboBox_SelectedLensIndex.Items.Add(index);
                ComboBox_SelectedLensIndex.SelectedIndex = 0;
            }
        }
        #endregion

        #region 加载主界面上当前产品的工作流程
        private void LoadWorkflow()
        {
            //设置使用中文界面
            GlobalParameters.flagassembly.chineseflag = true;

            //加载当前产品工作流程
            ListBox_Stepflow.Items.Clear();
            for (int i = 0; i < GlobalParameters.productconfig.processConfig.workflowStepNum; i++)
            {
                if (GlobalParameters.productconfig.processConfig.workflowArray[i] != -1)
                {
                    string StepName = GeneralFunction.GetWorkflowName(Enum.GetName(typeof(Enum_WorkFlowList), GlobalParameters.productconfig.processConfig.workflowArray[i]));
                    ListBox_Stepflow.Items.Add(StepName);
                }
            }
        }
        #endregion

        #region 加载主界面产品名称下拉选项
        private bool LoadProductNametoMainForm()
        {
            bool res = false;
            string path = Application.StartupPath.ToString() + @"\Config\ProductConfig";

            List<FileInfo> productlist = GeneralFunction.GetFileInfos(path, ".xml");
            if (productlist.Count > 0)
            {
                foreach (FileInfo productname in productlist)
                {
                    string product = productname.Name.Remove(productname.Name.LastIndexOf("."));
                    cmb_ListProduct.Items.Add(product);
                }
                res = true;
            }
            else
            {
                res = false;
            }

            return res;
        }
        #endregion

        #region 加载主界面通道启用选项
        private void LoadChannelValidConfig()
        {
            chkbox_Chl0Valid.Checked = GlobalParameters.productconfig.processConfig.laserIntensity[0].outputEnable;
            Button_Channel0LaserOnOff.Enabled = GlobalParameters.productconfig.processConfig.laserIntensity[0].outputEnable;
            chkbox_Chl1Valid.Checked = GlobalParameters.productconfig.processConfig.laserIntensity[1].outputEnable;
            Button_Channel1LaserOnOff.Enabled = GlobalParameters.productconfig.processConfig.laserIntensity[1].outputEnable;
            chkbox_Chl2Valid.Checked = GlobalParameters.productconfig.processConfig.laserIntensity[2].outputEnable;
            Button_Channel2LaserOnOff.Enabled = GlobalParameters.productconfig.processConfig.laserIntensity[2].outputEnable;
            chkbox_Chl3Valid.Checked = GlobalParameters.productconfig.processConfig.laserIntensity[3].outputEnable;
            Button_Channel3LaserOnOff.Enabled = GlobalParameters.productconfig.processConfig.laserIntensity[3].outputEnable;
        }
        #endregion

        //----主界面图像控件

        #region 实时刷新图像
        private void Image_OnFrame(string DeviceName, Bitmap aFrame)
        {
            if (aFrame != null)
            {
                Bitmap bmp = (Bitmap)aFrame.Clone();
                CogImage8Grey cogImage8Grey = null;
                if ((GlobalParameters.flagassembly.downcameraliveonflag == true) && (GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild == true))
                {
                    //国惠宽波段红外相机图像预处理
                    GlobalParameters.cogtoolblock_GhoptoIRCamera.Inputs[0].Value = new CogImage8Grey(bmp);
                    GlobalParameters.cogtoolblock_GhoptoIRCamera.Run();
                    ICogImage image = GlobalParameters.cogtoolblock_GhoptoIRCamera.Outputs[0].Value as Cognex.VisionPro.ICogImage;
                    Bitmap GhoptoIRCameraImage = new Bitmap(image.ToBitmap());
                    cogImage8Grey = new CogImage8Grey(GhoptoIRCameraImage);
                    GhoptoIRCameraImage.Dispose();
                }
                else
                {
                    cogImage8Grey = new CogImage8Grey(bmp);
                }

                cogRecordDisplay1.BeginInvoke(new Action(() =>
                {
                    //图像合成十字中心线
                    Cognex.VisionPro.ToolBlock.CogToolBlock toolBlock = new Cognex.VisionPro.ToolBlock.CogToolBlock();
                    double width = cogImage8Grey.Width;
                    double height = cogImage8Grey.Height;
                    CogCreateLineTool Htool = new CogCreateLineTool();
                    Htool.Name = "Htool";
                    CogCreateLineTool Vtool = new CogCreateLineTool();
                    Vtool.Name = "Vtool";
                    Htool.OutputColor = CogColorConstants.Red;
                    Htool.Line.SetFromStartXYEndXY(0, height / 2.0, width, height / 2.0);

                    Vtool.OutputColor = CogColorConstants.Red;
                    Vtool.Line.SetFromStartXYEndXY(width / 2.0, 0, width / 2.0, height);
                    Htool.InputImage = cogImage8Grey;
                    Vtool.InputImage = cogImage8Grey;

                    toolBlock.Tools.Add(Vtool);
                    toolBlock.Tools.Add(Htool);
                    toolBlock.Inputs.Add((new Cognex.VisionPro.ToolBlock.CogToolBlockTerminal("InputImage", cogImage8Grey)));

                    Htool.InputImage = (CogImage8Grey)toolBlock.Inputs["InputImage"].Value;
                    Vtool.InputImage = (CogImage8Grey)toolBlock.Inputs["InputImage"].Value;
                    toolBlock.Run();
                    Application.DoEvents();
                    cogRecordDisplay1.Record = toolBlock.CreateLastRunRecord().SubRecords[0];

                }));

                bmp.Dispose();
            }
            GC.Collect();
        }
        #endregion

        //----主界面窗体User下拉菜单项功能函数

        #region 主界面窗体User下拉菜单项：User LogIn/Out
        private void UserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((STD_IFrameWork.cFrameWork.UserAdmin.loginMode == STD_IUserManagement.LoginModeEnum.ONLINE) && (STD_IFrameWork.cFrameWork.UserAdmin.loginFlag == true))
            {
                //如果在开启软件登录时勾选了Online选项，则将登录的员工号直接更新至主界面员工号信息栏并直接锁定
                tb_EmployeeID.Text = GlobalParameters.systemOperationInfo.useraccount = cFrameWork.UserAdmin.userID;
                tb_EmployeeID.Enabled = false;
            }
            else
            {
                //如果如果进入软件登录时未勾选Online选项，则清空主界面员工号信息栏并允许后续再输入更新
                tb_EmployeeID.Text = GlobalParameters.systemOperationInfo.useraccount = String.Empty;
                tb_EmployeeID.Enabled = true;
            }
        }
        #endregion

        //----主界面窗体Operation下拉菜单项功能函数

        #region 主界面窗体Operation下拉菜单项：工程调试面板
        private void DropDownMenuItem_LoadEngineeringForm_Click(object sender, EventArgs e)
        {
            if (engFrm == null || engFrm.IsDisposed)
            {
                engFrm = new EngineerForm();
                engFrm.StartPosition = FormStartPosition.CenterScreen;
                engFrm.Show();
                engFrm.TopMost = true;
            }
            else
            {
                engFrm.StartPosition = FormStartPosition.CenterScreen;
                engFrm.Show();
                engFrm.TopMost = true;
            }
        }
        #endregion

        #region 主界面窗体Operation下拉菜单项：点胶测试面板
        private void DropDownMenuItem_LoadEpoxyTestForm_Click(object sender, EventArgs e)
        {
            EpoxyTestForm epoxyTest = new EpoxyTestForm();
            epoxyTest.TopLevel = true;
            epoxyTest.StartPosition = FormStartPosition.CenterScreen;
            epoxyTest.ShowDialog();
        }
        #endregion

        //----主界面窗体Module下拉菜单项功能函数

        #region 主界面窗体Module下拉菜单项：运动控制卡配置
        private void DropDownMenuItem_MotionSystemConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.MotionTools.Motion_OpenMotionSetupPanel();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：运动轴参数配置
        private void DropDownMenuItem_AxisConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.MotionTools.Motion_OpenAxisConfigPanel();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：IO卡配置
        private void DropDownMenuItem_IOCardTypeConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.IOControlTools.OpenDAQSetupPanel();
        }

        private void DropDownMenuItem_IOPortConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.IOControlTools.OpenIOSetupPanel();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：相机配置
        private void DropDownMenuItem_CameraConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.CameraTools.Camera_OpenSetupPanel();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：光源控制器配置
        private void DropDownMenuItem_LightSourceConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.LightSourcesTools.LightSource_OpenSetupPanel();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：压力传感器配置
        private void DropDownMenuItem_ForceSensorConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.ForceSensorTools.ForceSensor_OpenSetupPanel();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：加电源表配置
        private void DropDownMenuItem_SourceMeterConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.SourceMetertools.OpenSourceMeterSetForm();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：UV控制器配置
        private void DropDownMenuItem_UVControllerConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.UVControllerTools.UVController_OpenSetupPanel();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：夹爪控制器配置
        private void DropDownMenuItem_GripperControllerConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.IAIGripperTools.ElectricalGripper_OpenSetupPanel();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：FI2CUSB通信设备配置
        private void DropDownMenuItem_FI2CUSBDeviceConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.FI2CUSBTools.FI2CUSB_OpenSetForm();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：SMC卡片电机轴控制器配置
        private void DropDownMenuItem_SMCCardMotionConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.SMCCardMotionTools.SMCCardMotion_OpenSetupPanel();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：TEC温度控制器配置
        private void DropDownMenuItem_TECControllerConfig_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region 主界面窗体Module下拉菜单项：扫码器配置
        private void DropDownMenuItem_QRCodeScannerConfig_Click(object sender, EventArgs e)
        {
            GlobalFunction.QRCodeScannerTools.QRCodeScanner_OpenSetupPanel();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：Camstar配置
        private void DropDownMenuItem_CamstarConfig_Click(object sender, EventArgs e)
        {
            MESConfigForm mesconfig = new MESConfigForm();
            mesconfig.TopLevel = true;
            mesconfig.StartPosition = FormStartPosition.CenterScreen;
            mesconfig.ShowDialog();
        }
        #endregion

        #region 主界面窗体Module下拉菜单项：视觉处理功能配置
        private void DropDownMenuItem_VisionProcessConfig_Click(object sender, EventArgs e)
        {
            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == true)
            {
                VisionProcessEditForm visionprocessedit = new VisionProcessEditForm();
                visionprocessedit.TopLevel = true;
                visionprocessedit.StartPosition = FormStartPosition.CenterScreen;
                visionprocessedit.ShowDialog();
            }
            else
            {
                MessageBox.Show("Cognex视觉工具未初始化", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        //----主界面窗体Configuration下拉菜单项功能函数

        #region 主界面窗体Configuration下拉菜单项：产品工艺参数配置
        private void DropDownMenuItem_ProductConfig_Click(object sender, EventArgs e)
        {
            if (productConfigForm == null || productConfigForm.IsDisposed)
            {
                productConfigForm = new ProductConfigForm();
            }
            productConfigForm.TopLevel = true;
            productConfigForm.StartPosition = FormStartPosition.CenterScreen;
            productConfigForm.ShowDialog();
        }
        #endregion

        #region 主界面窗体Configuration下拉菜单项：系统参数配置
        private void DropDownMenuItem_SystemConfig_Click(object sender, EventArgs e)
        {
            SystemConfigForm systemConfigForm = new SystemConfigForm();
            systemConfigForm.TopLevel = true;
            systemConfigForm.StartPosition = FormStartPosition.CenterScreen;
            systemConfigForm.ShowDialog();
        }
        #endregion

        //----主界面窗体各按钮控件功能函数

        #region 主界面勾选项：Box发料盘中单个Box勾选
        private void CheckBox_BoxLoad_CheckedChanged(object sender, EventArgs e)
        {
            bool on = ((CheckBox)sender).Checked;

            if (on == true)
            {
                ((CheckBox)sender).BackColor = Color.Green;
            }
            else
            {
                ((CheckBox)sender).BackColor = SystemColors.Control;
                CheckBox_AllBoxLoad.Checked = false;
            }

            for (int i = 0; i < GlobalParameters.productconfig.boxlensConfig.boxTray.rowcount; i++)
            {
                for (int j = 0; j < GlobalParameters.productconfig.boxlensConfig.boxTray.colcount; j++)
                {
                    GlobalParameters.processdata.boxload[i, j].index = Convert.ToInt32(boxloadtray[i, j].Tag);
                    GlobalParameters.processdata.boxload[i, j].row = i;
                    GlobalParameters.processdata.boxload[i, j].col = j;
                    GlobalParameters.processdata.boxload[i, j].exists = boxloadtray[i, j].Checked;
                }
            }
        }
        #endregion

        #region 主界面勾选项：Box发料盘中所有Box勾选
        private void CheckBox_AllBoxLoad_Click(object sender, EventArgs e)
        {
            bool on = ((CheckBox)sender).Checked;

            for (int i = 0; i < GlobalParameters.productconfig.boxlensConfig.boxTray.rowcount; i++)
            {
                for (int j = 0; j < GlobalParameters.productconfig.boxlensConfig.boxTray.colcount; j++)
                {
                    boxloadtray[i, j].Checked = on;
                    GlobalParameters.processdata.boxload[i, j].exists = on;
                    GlobalParameters.processdata.boxload[i, j].index = Convert.ToInt32(boxloadtray[i, j].Tag);
                    GlobalParameters.processdata.boxload[i, j].row = i;
                    GlobalParameters.processdata.boxload[i, j].col = j;
                    if (on == true)
                    {
                        boxloadtray[i, j].BackColor = Color.Green;
                    }
                    else
                    {
                        boxloadtray[i, j].BackColor = SystemColors.Control;
                    }
                }
            }
        }
        #endregion

        #region 主界面勾选项：Lens发料盘中单个Lens勾选
        private void CheckBox_LensLoad_CheckedChanged(object sender, EventArgs e)
        {
            bool on = ((CheckBox)sender).Checked;

            if (on == true)
            {
                ((CheckBox)sender).BackColor = Color.Green;
            }
            else
            {
                ((CheckBox)sender).BackColor = SystemColors.Control;
                CheckBox_AllLensLoad.Checked = false;
            }

            for (int i = 0; i < GlobalParameters.productconfig.boxlensConfig.lensTray.rowcount; i++)
            {
                GlobalParameters.processdata.lensload[i].index = Convert.ToInt32(lensloadtray[i].Tag);
                GlobalParameters.processdata.lensload[i].row = i;
                GlobalParameters.processdata.lensload[i].col = 0;
                GlobalParameters.processdata.lensload[i].exists = lensloadtray[i].Checked;
            }
        }
        #endregion

        #region 主界面勾选项：Lens发料盘中所有Lens勾选
        private void CheckBox_AllLensLoad_Click(object sender, EventArgs e)
        {
            bool on = ((CheckBox)sender).Checked;

            for (int i = 0; i < GlobalParameters.productconfig.boxlensConfig.lensTray.rowcount; i++)
            {
                lensloadtray[i].Checked = on;
                GlobalParameters.processdata.lensload[i].index = Convert.ToInt32(lensloadtray[i].Tag);
                GlobalParameters.processdata.lensload[i].row = i;
                GlobalParameters.processdata.lensload[i].col = 0;
                GlobalParameters.processdata.lensload[i].exists = lensloadtray[i].Checked;
                if (on == true)
                {
                    lensloadtray[i].BackColor = Color.Green;
                }
                else
                {
                    lensloadtray[i].BackColor = SystemColors.Control;
                }
            }
        }
        #endregion

        #region 主界面列表框：工作流程项双击事件响应
        private void ListBox_StepFlow_DoubleClick(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("确定要执行当前选定步骤", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                GlobalParameters.flagassembly.stopflag = false;
                GlobalParameters.flagassembly.continueflag = false;
                GlobalParameters.flagassembly.singlestepflag = true;
                string step = GeneralFunction.GetWorkflowNum(ListBox_Stepflow.SelectedItem.ToString()).ToString();
                Enum_WorkFlowList workflow = (Enum_WorkFlowList)Enum.Parse(typeof(Enum_WorkFlowList), step);
                GlobalParameters.processdata.currentBoxIndex = Convert.ToInt16(ComboBox_SelectedBoxIndex.SelectedItem);
                GlobalParameters.processdata.currentLensIndex = Convert.ToInt16(ComboBox_SelectedLensIndex.SelectedItem);
                GlobalParameters.processdata.currentProductSN = tb_ProductSN.Text.ToUpper().Trim();

                //将单步流程双击响应事件加载为独立的任务来执行
                Task.Factory.StartNew(new Action(() => {
                    AutoRunSingleStep(workflow);
                }));
                Task.WaitAll();
            }
            else if (res == DialogResult.No)
            {
                GlobalFunction.updateStatusDelegate("单步流程操作已被取消", Enum_MachineStatus.REMINDER);
            }
        }
        #endregion

        #region 主界面按钮：单通道LaserOnOff控制
        private void Button_ChannelLaserOnOff_Click(object sender, EventArgs e)
        {
            bool res = false;
            string str = (string)((Button)sender).Name;
            string CommandButtonName = str.Substring(str.IndexOf("_") + 1, str.Length - str.IndexOf("_") - 1);
            string CommandString = ((Button)sender).Text;
            int Channel = Convert.ToInt16(((Button)sender).Tag.ToString());
            GlobalParameters.processdata.currentChannel = Channel;

            if (CommandString == "打开")
            {
                res = GlobalFunction.ProcessFlow.LaserTurnOnOff(Channel, true);
                if (res == true)
                {
                    ((Button)sender).Text = "关闭";
                    ((Button)sender).ForeColor = SystemColors.Highlight;
                    ((Button)sender).BackColor = SystemColors.ButtonHighlight;
                    switch (CommandButtonName)
                    {
                        case "Channel0LaserOnOff":
                            PictureBox_Channel0LaserOn.BackgroundImage = Properties.Resources.Redled;
                            break;

                        case "Channel1LaserOnOff":
                            PictureBox_Channel1LaserOn.BackgroundImage = Properties.Resources.Redled;
                            break;

                        case "Channel2LaserOnOff":
                            PictureBox_Channel2LaserOn.BackgroundImage = Properties.Resources.Redled;
                            break;

                        case "Channel3LaserOnOff":
                            PictureBox_Channel3LaserOn.BackgroundImage = Properties.Resources.Redled;
                            break;
                    }

                    str = "产品Ch" + Channel.ToString() + "通道激光器打开成功";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                }
                else
                {
                    str = "产品Ch" + Channel.ToString() + "通道激光器打开失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                }
            }
            else
            {
                res = GlobalFunction.ProcessFlow.LaserTurnOnOff(Channel, false);
                if (res == true)
                {
                    ((Button)sender).Text = "打开";
                    ((Button)sender).ForeColor = SystemColors.ControlText;
                    ((Button)sender).BackColor = SystemColors.Control;
                    switch (CommandButtonName)
                    {
                        case "Channel0LaserOnOff":
                            PictureBox_Channel0LaserOn.BackgroundImage = Properties.Resources.Grayled;
                            break;

                        case "Channel1LaserOnOff":
                            PictureBox_Channel1LaserOn.BackgroundImage = Properties.Resources.Grayled;
                            break;

                        case "Channel2LaserOnOff":
                            PictureBox_Channel2LaserOn.BackgroundImage = Properties.Resources.Grayled;
                            break;

                        case "Channel3LaserOnOff":
                            PictureBox_Channel3LaserOn.BackgroundImage = Properties.Resources.Grayled;
                            break;
                    }

                    str = "产品Ch" + Channel.ToString() + "通道激光器关闭成功";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                }
                else
                {
                    str = "产品Ch" + Channel.ToString() + "通道激光器关闭失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                }
            }

            //获取当前产品加电方式
            string PowerWay = GlobalParameters.productconfig.processConfig.powerway.ToString();

            if ((res == true) && (PowerWay != "CURRENT_NO"))
            {
                //更新主界面加电源表读数
                if (GlobalParameters.HardwareInitialStatus.Keithley2401_1_InitialStatus == true)
                {
                    GlobalFunction.updateSourceMeterVoltageReadingDelegate();
                    GlobalFunction.updateSourceMeterCurrentReadingDelegate();
                }
            }
        }
        #endregion

        #region 主界面按钮：产品加电/下电控制
        private void Button_PogoPinControl_Click(object sender, EventArgs e)
        {
            bool res = false;

            Button_PogoPinControl.Enabled = false;
            switch (Button_PogoPinControl.Text)
            {
                case "产品加电":
                    res = GlobalFunction.ProcessFlow.StepFlow_PowerTurnOnOff(true);
                    if (res == true)
                    {
                        Button_PogoPinControl.Text = "产品下电";
                        Button_PogoPinControl.ForeColor = SystemColors.Highlight;
                        Button_PogoPinControl.BackColor = SystemColors.ButtonHighlight;
                        GlobalFunction.updateStatusDelegate("产品加电成功", Enum_MachineStatus.NORMAL);
                    }
                    else
                    {
                        GlobalFunction.updateStatusDelegate("产品加电失败", Enum_MachineStatus.ERROR);
                    }
                    break;

                case "产品下电":
                    res = GlobalFunction.ProcessFlow.StepFlow_PowerTurnOnOff(false);
                    if (res == true)
                    {
                        Button_PogoPinControl.Text = "产品加电";
                        Button_PogoPinControl.ForeColor = SystemColors.ControlText;
                        Button_PogoPinControl.BackColor = SystemColors.Control;
                        GlobalFunction.updateStatusDelegate("产品下电成功", Enum_MachineStatus.NORMAL);
                    }
                    else
                    {
                        GlobalFunction.updateStatusDelegate("产品下电失败", Enum_MachineStatus.ERROR);
                    }
                    break;
            }
            Button_PogoPinControl.Enabled = true;
        }
        #endregion

        #region 主界面按钮：Box夹爪张开控制
        private void Button_BoxGripperControl_Click(object sender, EventArgs e)
        {
            bool res = GlobalFunction.ProcessFlow.BoxGripperControl(true);
            if (res == true)
            {
                GlobalParameters.flagassembly.boxongripperflag = false;
            }
            else
            {
                MessageBox.Show("Box夹爪张开失败", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 主界面按钮：Lens夹爪张开控制
        private void Button_LensGripperControl_Click(object sender, EventArgs e)
        {
            bool res = GlobalFunction.ProcessFlow.LensGripperControl(true);
            if (res == true)
            {
                GlobalParameters.flagassembly.lensongripperflag = false;
            }
            else
            {
                MessageBox.Show("Lens夹爪张开失败", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region 主界面按钮：Box夹爪上下运动控制
        private void Button_BoxGripperCylinderControl_Click(object sender, EventArgs e)
        {
            bool res = false;

            if (Button_BoxGripperCylinderControl.Text == "Box夹爪下降")
            {
                if (MessageBox.Show("请确认Box夹爪气缸处在安全位置", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }

                res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(true);
                if (res == true)
                {
                    Button_BoxGripperCylinderControl.Text = "Box夹爪上抬";
                    Button_BoxGripperCylinderControl.ForeColor = SystemColors.Highlight;
                    Button_BoxGripperCylinderControl.BackColor = SystemColors.ButtonHighlight;
                }
            }
            else
            {
                res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(false);
                if (res == true)
                {
                    Button_BoxGripperCylinderControl.Text = "Box夹爪下降";
                    Button_BoxGripperCylinderControl.ForeColor = SystemColors.ControlText;
                    Button_BoxGripperCylinderControl.BackColor = SystemColors.Control;
                }
            }
        }
        #endregion

        #region 主界面按钮：胶针上下运动控制
        private void Button_EpoxyDipCylinderControl_Click(object sender, EventArgs e)
        {
            bool res = false;

            if (Button_EpoxyDipCylinderControl.Text == "胶针下降")
            {
                if (MessageBox.Show("请确认胶针气缸处在安全位置", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }

                res = GlobalFunction.ProcessFlow.DispenserEpoxyCylinderControl(true);
                if (res == true)
                {
                    Button_EpoxyDipCylinderControl.Text = "胶针上抬";
                    Button_EpoxyDipCylinderControl.ForeColor = SystemColors.Highlight;
                    Button_EpoxyDipCylinderControl.BackColor = SystemColors.ButtonHighlight;
                }
            }
            else
            {
                res = GlobalFunction.ProcessFlow.DispenserEpoxyCylinderControl(false);
                if (res == true)
                {
                    Button_EpoxyDipCylinderControl.Text = "胶针下降";
                    Button_EpoxyDipCylinderControl.ForeColor = SystemColors.ControlText;
                    Button_EpoxyDipCylinderControl.BackColor = SystemColors.Control;
                }
            }
        }
        #endregion

        #region 主界面按钮：伺服Z轴回安全位
        private void Button_Z1AxisMoveToSafetyPosition_Click(object sender, EventArgs e)
        {
            bool res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
        }
        #endregion

        #region 主界面按钮：步进Z轴回安全位
        private void Button_Z2AxisMoveToSafetyPosition_Click(object sender, EventArgs e)
        {
            bool res = GlobalFunction.ProcessFlow.MotionAxisZ2MoveToSafetyPosition();
        }
        #endregion

        #region 主界面按钮：Lens移出产品Box
        private void Button_LensOutOfBox_Click(object sender, EventArgs e)
        {
            //Lens夹爪移出产品Box返回安全位
            bool res = GlobalFunction.ProcessFlow.LensGripperOutOfBox();
        }
        #endregion        

        #region 主界面按钮：胶针移出产品Box
        private void Button_DispenserEpoxyPinOutOfBox_Click(object sender, EventArgs e)
        {
            //胶针移出产品Box返回到安全位
            bool res = GlobalFunction.ProcessFlow.DispenserEpoxyPinOutOfBox();
        }
        #endregion

        #region 主界面按钮：执行Lens抛料过程
        private void Button_LensDiscard_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("是否抛弃当前夹爪上的Lens", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            bool res = GlobalFunction.ProcessFlow.StepFlow_DiscardLens();
        }
        #endregion

        #region 主界面按钮：下相机识别产品Box窗口
        private void Button_RecognizeBoxWindowCenter_Click(object sender, EventArgs e)
        {
            bool res = GlobalFunction.ProcessFlow.StepFlow_DownCameraRecognizeBoxWindow();
        }
        #endregion

        #region 主界面按钮：下相机识别光斑中心位置
        private void Button_RecognizeLaserSpotCenter_Click(object sender, EventArgs e)
        {
            bool res = GlobalFunction.ProcessFlow.VisionRecognizeChannelLaserSpot(GlobalParameters.processdata.currentChannel);
        }
        #endregion

        #region 主界面按钮：清空信息显示窗口中所有记录
        private void Button_EmptySystemInfo_Click(object sender, EventArgs e)
        {
            lb_SystemInfo.Items.Clear();
        }
        #endregion

        #region 主界面按钮：回原点
        private void Button_AxesGoSafetyPosition_Click(object sender, EventArgs e)
        {
            bool res = false;
            string str = string.Empty;

            //检查急停开关状态
            if (GlobalFunction.IOControlTools.CheckEmergencySwitchOn() == true)
            {
                MessageBox.Show("请确认急停开关是否被按下", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //如果Lens夹爪还在产品Box内部
            if (GlobalParameters.flagassembly.lensgripperinboxflag == true)
            {
                MessageBox.Show("Lens夹爪未退出产品Box，需要人工处理", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //如果点胶针还在产品Box内部
            if (GlobalParameters.flagassembly.epoxypininboxflag == true)
            {
                MessageBox.Show("点胶针未退出产品Box，需要人工处理", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //强制将所有气缸安全上抬，防止轴运动时运动干涉而发生撞机事件
            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            if (GlobalParameters.flagassembly.homeflag == false)
            {
                MessageBox.Show("运动轴系统没有复位成功，请先执行轴系统复位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GlobalFunction.updateRunStatusLampDelegate(Enum_RunStatus.RUN_STOP);
                LockMainForm(true);
                Button_HomeAxes.Enabled = true;
                return;
            }

            res = GlobalFunction.MotionTools.Motion_AllMoveToSafe();
        }
        #endregion

        #region 主界面按钮：轴复位
        private void Button_HomeAxes_Click(object sender, EventArgs e)
        {
            bool res = false;
            bool result = true;

            //检查急停开关状态
            if (GlobalFunction.IOControlTools.CheckEmergencySwitchOn() == true)
            {
                MessageBox.Show("请释放急停按钮并且给机台上电", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //如果Lens夹爪还在产品Box内部
            if (GlobalParameters.flagassembly.lensgripperinboxflag == true)
            {
                MessageBox.Show("Lens夹爪未退出产品Box，需要人工处理", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //如果点胶针还在产品Box内部
            if (GlobalParameters.flagassembly.epoxypininboxflag == true)
            {
                MessageBox.Show("点胶针未退出产品Box，需要人工处理", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //强制将所有气缸安全上抬，防止轴运动时运动干涉而发生撞机事件
            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return;
            }

            //提醒Box夹爪和胶针气缸正下方是否有机械部件会在龙门Z轴复位过程中发生撞机事件 
            if (MessageBox.Show("请确认Box夹爪和胶针气缸处在安全位置", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            //所有运动轴执行复位
            res = GlobalFunction.MotionTools.Motion_HomeAll();
            if (res == false)
            {
                result = false;
            }

            //产品Box夹爪复位
            if (GlobalParameters.systemconfig.InstrumentConfig.IAIBoxGripper_Valid == true)
            {
                res = GlobalFunction.MotionTools.Motion_BoxGripperHome();
                if (res == false)
                {
                    result = false;
                }
                else
                {
                    GlobalParameters.flagassembly.boxongripperflag = false;
                }
            }

            //产品Lens夹爪复位
            if (GlobalParameters.systemconfig.InstrumentConfig.IAILensGripper_Valid == true)
            {
                res = GlobalFunction.MotionTools.Motion_LensGripperHome();
                if (res == false)
                {
                    result = false;
                }
                else
                {
                    GlobalParameters.flagassembly.lensongripperflag = false;
                }
            }

            //SMC卡片电机轴复位
            if (GlobalParameters.systemconfig.InstrumentConfig.SMCCardMotion_Valid == true)
            {
                res = GlobalFunction.MotionTools.Motion_SMCCardMotionHome();
                if (res == false)
                {
                    result = false;
                }
            }

            if (result == true)
            {
                GlobalParameters.flagassembly.homeflag = true;
                LockMainForm(false);
                lab_LedHome.ForeColor = Color.Green;
                MessageBox.Show("运动轴系统和夹爪复位成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                GlobalParameters.flagassembly.homeflag = false;
                LockMainForm(true);
                lab_LedHome.ForeColor = Color.Gray;
                MessageBox.Show("运动轴系统和夹爪复位失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Button_HomeAxes.Enabled = true;
            }
        }
        #endregion

        #region 主界面按钮：点胶测试
        private void Button_EpoxyTest_Click(object sender, EventArgs e)
        {
            EpoxyTestForm epoxyTest = new EpoxyTestForm();
            epoxyTest.TopLevel = true;
            epoxyTest.StartPosition = FormStartPosition.CenterScreen;
            epoxyTest.ShowDialog();
        }
        #endregion

        public void FormResult(string str)
        {
            formresult = null;
            formresult = str;
        }

        #region 主界面控件：员工号
        private void tb_EmployeeID_KeyUp(object sender, KeyEventArgs e)
        {
            bool res = false;

            if (GlobalParameters.productconfig.processConfig.mesEnable == true)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string errmsg = "";
                    PasswordPanel passpnl = new PasswordPanel();
                    passpnl.StartPosition = FormStartPosition.CenterScreen;
                    passpnl.TopLevel = true;
                    passpnl.SetPasswordEvent += new PasswordPanel.DelegateSetPassword(FormResult);
                    passpnl.ShowDialog();
                    if (formresult != "")
                    {
                        GlobalParameters.systemOperationInfo.useraccount = tb_EmployeeID.Text.Trim();
                        GlobalParameters.systemOperationInfo.userpassword = formresult;
                        res = GlobalFunction.MESTools.MES_VerifyEmployee(GlobalParameters.systemOperationInfo.useraccount, GlobalParameters.systemOperationInfo.userpassword, ref errmsg);
                        if (res == false)
                        {
                            MessageBox.Show(errmsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            GlobalFunction.updateStatusDelegate("Camstar验证用户名密码成功");
                        }
                    }

                }
            }
            else
            {
                GlobalParameters.systemOperationInfo.useraccount = tb_EmployeeID.Text.Trim();
            }
        }
        #endregion

        #region 主界面控件：DCLot物料信息输入框
        private void tb_DCLot_KeyUp(object sender, KeyEventArgs e)
        {
            bool res = false;
            int count = 0;

            if (GlobalParameters.productconfig.processConfig.mesEnable == false)
            {
                MessageBox.Show("MES功能未配置启用", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (GlobalParameters.HardwareInitialStatus.MESFunctionTools_InitialStatus == false)
            {
                MessageBox.Show("MES功能未初始化", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                string dcstr = tb_DCLot.Text.Trim().ToUpper();
                if (dcstr == "")
                {
                    MessageBox.Show("DC物料号不能为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tb_DCLot.Clear();
                    return;
                }
                else
                {
                    count = lv_DCInfo.Items.Count;
                    if (GlobalParameters.productconfig.processConfig.dcCount <= 0)
                    {
                        MessageBox.Show("DC物料类型总数量不正确", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tb_DCLot.Clear();
                        return;
                    }
                    else
                    {
                        if (count == GlobalParameters.productconfig.processConfig.dcCount)
                        {
                            MessageBox.Show("目前的DC物料类型已全部占用", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tb_DCLot.Clear();
                            return;
                        }
                    }
                    List<string> dcList = new List<string>();
                    for (int i = 0; i < count; i++)
                    {
                        dcList.Add(lv_DCInfo.Items[i].SubItems[0].Text.ToString());
                        string str = dcList[0].ToUpper();
                        if (dcstr[0] == str[0])
                        {
                            MessageBox.Show("DC物料已存在", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tb_DCLot.Clear();
                            return;
                        }
                    }
                    dcList.Add(dcstr);

                    //清除DC List
                    lv_DCInfo.Items.Clear();
                    dcCollection.dcinfolist.Clear();
                    for (int i = 0; i < count + 1; i++)
                    {
                        DCInfo dcinfo = new DCInfo();
                        string pn = "";
                        string validtime = "";
                        string errmsg = "";
                        res = GlobalFunction.MESTools.MES_GetPNByDC(dcList[i], ref pn, ref errmsg);
                        if (res == false)
                        {
                            MessageBox.Show(errmsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tb_DCLot.Clear();
                            return;
                        }
                        res = GlobalFunction.MESTools.MES_GetValidTimeByDC(dcList[i], ref validtime, ref errmsg);
                        if (res == false)
                        {
                            MessageBox.Show(errmsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            tb_DCLot.Clear();
                            return;
                        }
                        dcinfo.dcNum = dcList[i];
                        dcinfo.dcPN = pn;
                        dcinfo.validTime = validtime;
                        dcCollection.dcinfolist.Add(dcinfo);
                        ListViewItem listviewitem = new ListViewItem();
                        listviewitem.UseItemStyleForSubItems = false;
                        listviewitem.Text = dcList[i];
                        listviewitem.SubItems.Add(pn);
                        if (int.Parse(validtime) <= 0)
                        {
                            validtime = "0";
                        }
                        listviewitem.SubItems.Add(validtime);
                        lv_DCInfo.Items.Add(listviewitem);
                        int validTime = int.Parse(validtime);
                        if (validTime <= 0)
                        {
                            lv_DCInfo.Items[i].SubItems[2].BackColor = Color.Red;
                        }
                        else if (validTime > 0 && validTime < GlobalParameters.productconfig.processConfig.remindTime)
                        {
                            lv_DCInfo.Items[i].SubItems[2].BackColor = Color.Yellow;
                        }
                        else
                        {
                            lv_DCInfo.Items[i].SubItems[2].BackColor = Color.White;
                        }
                    }
                    tb_DCLot.Clear();

                    //保存DC信息
                    res = GlobalFunction.MESTools.MES_SaveDCList(dcCollection);
                    if (res == false)
                    {
                        MessageBox.Show("保存DC物料信息失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        #region 主界面按钮：删除DC物料
        private void Button_RemoveDC_Click(object sender, EventArgs e)
        {
            int index = 0;
            bool res = false;

            DialogResult diaRes = MessageBox.Show("请确认是否删除当前DC物料信息项?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (diaRes == DialogResult.Yes)
            {
                int count = lv_DCInfo.SelectedItems.Count;
                if (count > 0)
                {
                    index = lv_DCInfo.SelectedItems[0].Index;
                    lv_DCInfo.Items[index].Remove();
                    dcCollection.dcinfolist.RemoveAt(index);
                    res = GlobalFunction.MESTools.MES_SaveDCList(dcCollection);
                    if (res == false)
                    {
                        MessageBox.Show("保存DC物料信息失败", "错误");
                    }
                }
            }
        }
        #endregion

        #region 主界面控件：DCList信息刷新定时器
        private void DCTimer_Tick(object sender, EventArgs e)
        {
            int validTime = 0;
            string timeStr = string.Empty;
            string validTimeStr = string.Empty;
            int count = lv_DCInfo.Items.Count;

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    timeStr = lv_DCInfo.Items[i].SubItems[2].Text.Trim();
                    if (timeStr != "" && timeStr != "NA")
                    {
                        validTime = int.Parse(timeStr);
                        validTime = validTime - 1;
                        if (validTime <= 0)
                        {
                            validTimeStr = "0";
                            lv_DCInfo.Items[i].UseItemStyleForSubItems = false;
                            lv_DCInfo.Items[i].SubItems[2].Text = validTimeStr;
                            lv_DCInfo.Items[i].SubItems[2].BackColor = Color.Red;

                        }
                        else if (validTime > 0 && validTime < GlobalParameters.productconfig.processConfig.remindTime)
                        {
                            validTimeStr = validTime.ToString();
                            lv_DCInfo.Items[i].UseItemStyleForSubItems = false;
                            lv_DCInfo.Items[i].SubItems[2].Text = validTimeStr;
                            lv_DCInfo.Items[i].SubItems[2].BackColor = Color.Yellow;
                        }
                        else
                        {
                            validTimeStr = validTime.ToString();
                            lv_DCInfo.Items[i].UseItemStyleForSubItems = false;
                            lv_DCInfo.Items[i].SubItems[2].Text = validTimeStr;
                            lv_DCInfo.Items[i].SubItems[2].BackColor = Color.White;
                        }
                    }
                    else
                    {
                        validTimeStr = "NA";
                    }
                }
            }
        }
        #endregion

        #region 主界面按钮：开始
        private void Button_Start_Click(object sender, EventArgs e)
        {
            //强制关闭上下光源
            GlobalFunction.LightSourcesTools.LightSource_CloseUpRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseUpSpot();
            GlobalFunction.LightSourcesTools.LightSource_CloseDnRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseDnSpot();

            //强制关闭上下相机实时显示模式
            if (GlobalParameters.flagassembly.upcameraliveonflag == true)
            {
                GlobalFunction.CameraTools.Camera_StopGrab("UpCamera");
                GlobalParameters.flagassembly.upcameraliveonflag = false;
            }

            if (GlobalParameters.flagassembly.downcameraliveonflag == true)
            {
                GlobalFunction.CameraTools.Camera_StopGrab("DnCamera");
                GlobalParameters.flagassembly.downcameraliveonflag = false;
            }

            //所有轴恢复全速
            GlobalFunction.MotionTools.Motion_SetAllAxisSpeedPercent(100);

            //检查轴系统是否执行过Home操作
            if (GlobalParameters.flagassembly.homeflag == false)
            {
                MessageBox.Show("运动轴系统没有复位成功，请先执行轴系统复位", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GlobalFunction.updateRunStatusLampDelegate(Enum_RunStatus.RUN_STOP);
                LockMainForm(true);
                Button_HomeAxes.Enabled = true;
                return;
            }

            //检查当前Box夹爪上是否还有残留的产品Box，或者Box夹爪处于闭合状态
            if (GlobalParameters.flagassembly.boxongripperflag == true)
            {
                MessageBox.Show("请注意Box夹爪上有产品Box或者Box夹爪处于闭合状态，请人工处理后再运行", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GlobalFunction.updateRunStatusLampDelegate(Enum_RunStatus.RUN_STOP);
                return;
            }

            //检查当前Lens夹爪上是否还有残留的Lens，或者Lens夹爪处于闭合状态
            if (GlobalParameters.flagassembly.lensongripperflag == true)
            {
                MessageBox.Show("请注意Lens夹爪上有Lens或者Lens夹爪处于闭合状态，请人工处理后再运行", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                GlobalFunction.updateRunStatusLampDelegate(Enum_RunStatus.RUN_STOP);
                return;
            }

            //检查UV安全门是否已关闭
            if (GlobalParameters.systemconfig.SystemOperationConfig.doorsafemode == true)
            {
                if (GlobalFunction.IOControlTools.CheckUVSafetyDoorOn() == false)
                {
                    MessageBox.Show("UV安全防护门未关闭", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    GlobalFunction.updateRunStatusLampDelegate(Enum_RunStatus.RUN_STOP);
                    LockMainForm(true);
                    return;
                }
            }

            //核查DC物料数量信息
            if (GlobalParameters.productconfig.processConfig.mesEnable == true)
            {
                if (lv_DCInfo.Items.Count != GlobalParameters.productconfig.processConfig.dcCount)
                {
                    MessageBox.Show("DC物料数量信息不满足", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            //核查员工信息
            if (GlobalParameters.productconfig.processConfig.mesEnable == true)
            {
                string errmsg = string.Empty;

                //以Camstar登录则不需重复验证用户名密码
                if (cFrameWork.UserAdmin.loginMode != STD_IUserManagement.LoginModeEnum.ONLINE)
                {
                    if (GlobalFunction.MESTools.MES_VerifyEmployee(GlobalParameters.systemOperationInfo.useraccount, GlobalParameters.systemOperationInfo.userpassword, ref errmsg) == false)
                    {
                        MessageBox.Show("帐号和密码验证失败", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            else
            {
                string useraccount = tb_EmployeeID.Text.Trim();
                if (useraccount.Length < 5)
                {
                    MessageBox.Show("员工号不正确，请确认", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            //清除产品Box物料盘收料区状态
            MessageBox.Show("请确认物料盘收料区中产品已全部被取走", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ResetAllBoxUnloadStatus();

            //清除产品Box耦合组装最终结果
            ResetAllBoxFinalResult();

            //锁定主界面相关按钮和控件
            LockMainForm(true);
            Button_Stop.Enabled = true;

            //开启全自动流程主线程
            GlobalParameters.mainThread = new Thread(Thread_AutoRunWorkFlow);
            GlobalParameters.mainThread.Start();
        }
        #endregion

        #region 主界面按钮：停止
        private void Button_Stop_Click(object sender, EventArgs e)
        {
            GlobalParameters.flagassembly.stopflag = true;

            //当前机台不处于全自动耦合贴装流程状态
            if (GlobalParameters.flagassembly.mainthreadaliveflag == false)
            {
                //状态指示灯切换
                GlobalFunction.updateRunStatusLampDelegate(Enum_RunStatus.RUN_STOP);

                //解锁主界面相关控件
                LockMainForm(false);

                //复位相关标志位
                GlobalParameters.flagassembly.singlestepflag = true;
                GlobalParameters.flagassembly.continueflag = false;

                //强制关闭上下光源
                GlobalFunction.LightSourcesTools.LightSource_CloseUpRing();
                GlobalFunction.LightSourcesTools.LightSource_CloseUpSpot();
                GlobalFunction.LightSourcesTools.LightSource_CloseDnRing();
                GlobalFunction.LightSourcesTools.LightSource_CloseDnSpot();

                //强制关闭上下相机实时显示模式
                if (GlobalParameters.flagassembly.upcameraliveonflag == true)
                {
                    GlobalFunction.CameraTools.Camera_StopGrab("UpCamera");
                    GlobalParameters.flagassembly.upcameraliveonflag = false;
                }
                if (GlobalParameters.flagassembly.downcameraliveonflag == true)
                {
                    GlobalFunction.CameraTools.Camera_StopGrab("DnCamera");
                    GlobalParameters.flagassembly.downcameraliveonflag = false;
                }
            }
        }
        #endregion

        //----功能函数

        #region 主界面信息显示窗口字体颜色变化
        private void lb_SystemInfo_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index < 0)
                return;
            if (lb_SystemInfo.Items[e.Index].ToString().Contains("*"))
            {
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString().Replace("*", ""), e.Font, new SolidBrush(Color.Black), e.Bounds);
            }
            else if (lb_SystemInfo.Items[e.Index].ToString().Contains("#"))
            {
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString().Replace("#", ""), e.Font, new SolidBrush(Color.Red), e.Bounds);
            }
            else if (lb_SystemInfo.Items[e.Index].ToString().Contains("$"))
            {
                e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString().Replace("$", ""), e.Font, new SolidBrush(Color.Black), e.Bounds);
            }
            e.DrawFocusRectangle();
        }
        #endregion

        #region 锁住或者解锁主界面上一些重要按钮
        private void LockMainForm(bool lockctrl)
        {
            bool status = false;

            if (lockctrl == true)
            {
                status = false;
            }
            else
            {
                status = true;
            }
            Button_Stop.Enabled = status;
            Button_Start.Enabled = status;
            Button_AxesGoSafetyPosition.Enabled = status;
            Button_HomeAxes.Enabled = status;
            Button_EmptySystemInfo.Enabled = status;
            Button_EpoxyTest.Enabled = status;
            ComboBox_SelectedBoxIndex.Enabled = status;
            ComboBox_SelectedLensIndex.Enabled = status;            
            ListBox_Stepflow.Enabled = status;
            GroupBox_Materials.Enabled = status;
            GroupBox_ChannelLaserOnOff.Enabled = status;
            GroupBox_ShortcutButton.Enabled = status;
        }
        #endregion

        #region 主界面后台状态监控定时器时间响应
        private void timer_MainForm_Tick(object sender, EventArgs e)
        {
            //检查急停开关状态
            GlobalParameters.flagassembly.estopflag = GlobalFunction.IOControlTools.CheckEmergencySwitchOn();
            if (GlobalParameters.flagassembly.estopflag == true)
            {
                lab_LedEStop.ForeColor = Color.Red;

                //紧急停止可能正在运行的全自动流程
                GlobalParameters.flagassembly.continueflag = false;

                //设置标志指明轴需要重新Home后再执行后续操作
                GlobalParameters.flagassembly.homeflag = false;

                //锁住主界面一些重要控件和按钮
                LockMainForm(true);

                //暂时只开放主界面"轴复位"按钮
                Button_HomeAxes.Enabled = true;
            }
            else
            {
                lab_LedEStop.ForeColor = Color.Gray;

                //如果自动耦合贴装主线程运行结束并且机台处于停止
                if ((GlobalParameters.flagassembly.mainthreadaliveflag == false) && (GlobalParameters.flagassembly.stopflag == true))
                {
                    //解锁主界面一些重要控件和按钮
                    LockMainForm(false);
                }
            }

            //刷新主界面“轴复位”状态指示Led
            if (GlobalParameters.flagassembly.homeflag)
            {
                lab_LedHome.ForeColor = Color.Green;
            }
            else
            {
                lab_LedHome.ForeColor = Color.Gray;
            }

            //非全自动运行状态下刷新主界面“产品加电/下电”快捷按钮状态
            if (GlobalParameters.flagassembly.mainthreadaliveflag == false)
            {
                if (GlobalParameters.flagassembly.poweronflag)
                {
                    Button_PogoPinControl.Text = "产品下电";
                    Button_PogoPinControl.ForeColor = SystemColors.Highlight;
                    Button_PogoPinControl.BackColor = SystemColors.ButtonHighlight;
                }
                else
                {
                    Button_PogoPinControl.Text = "产品加电";
                    Button_PogoPinControl.ForeColor = SystemColors.ControlText;
                    Button_PogoPinControl.BackColor = SystemColors.Control;
                }
            }

            //根据主界面显示压力值勾选项状态刷新实时压力传感器反馈值
            GlobalParameters.flagassembly.showpressureforceflag = CheckBox_ShowPressureForce.Checked;
            if (GlobalParameters.flagassembly.showpressureforceflag == true)
            {
                //如果机台当前不处于下压Lens状态，需要实时刷新主界面压力传感器反馈值
                //注：带下压Lens动作的Lens耦合和贴装Lens步骤中，将通过委托事件（updatePressureForceReading）实时刷新主界面压力传感器反馈值
                if (GlobalParameters.flagassembly.pressdownlensflag == false)
                {
                    //获取压力计读数
                    int retErrorCode = 0;
                    double pressureforce = 0;
                    bool res = GlobalFunction.ForceSensorTools.ForceSensor_GetPressureValue("GripperPressureSensor", ref pressureforce, ref retErrorCode);
                    if (res == true)
                    {
                        GlobalParameters.processdata.realtimeLensPressureForce = Math.Abs(pressureforce);
                        tb_pressureforce.Text = GlobalParameters.processdata.realtimeLensPressureForce.ToString("F1");
                        tb_pressureforce.Refresh();
                    }
                }
            }
        }
        #endregion

        #region Box收料盘清除状态
        private void ResetAllBoxUnloadStatus()
        {
            for (int i = 0; i < GlobalParameters.productconfig.boxlensConfig.boxTray.rowcount; i++)
            {
                for (int j = 0; j < GlobalParameters.productconfig.boxlensConfig.boxTray.colcount; j++)
                {
                    boxunloadtray[i, j].Checked = false;
                    boxunloadtray[i, j].BackColor = SystemColors.Control;
                    GlobalParameters.processdata.boxunload[i, j].exists = false;
                }
            }
        }
        #endregion

        #region 清除产品Box耦合组装最终结果
        private void ResetAllBoxFinalResult()
        {
            for (int i = 0; i < GlobalParameters.productconfig.boxlensConfig.boxTray.rowcount; i++)
            {
                for (int j = 0; j < GlobalParameters.productconfig.boxlensConfig.boxTray.colcount; j++)
                {
                    GlobalParameters.processdata.boxload[i, j].finalresult = Enum_FinalResult.FAIL;
                }
            }
        }
        #endregion

        #region 运行指定的单步骤流程
        private bool AutoRunSingleStep(Enum_WorkFlowList stepflow)
        {
            bool res = false;
            int row = 0;
            int col = 0;
            DialogResult dialogResult = DialogResult.No;

            GlobalParameters.processdata.currentStepFlowName = GeneralFunction.GetWorkflowName(stepflow.ToString());
            GlobalFunction.updateStatusDelegate(GlobalParameters.processdata.currentStepFlowName + "...", Enum_MachineStatus.NORMAL);
            cFrameWork.SetTitleInfo(GlobalParameters.processdata.currentStepFlowName, eTitleType.NoError);

            switch (stepflow)
            {
                case Enum_WorkFlowList.UPCAMVIEW_BOX:
                    //上相机识别产品盒子
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        //单步骤模式
                        GlobalFunction.ProcessFlow.GetBoxLocationInTray(GlobalParameters.processdata.currentBoxIndex, ref row, ref col);
                        if (GlobalParameters.processdata.boxload[row, col].exists == false)
                        {
                            GlobalFunction.updateStatusDelegate("料盘中此产品Box未勾选", Enum_MachineStatus.ERROR);
                            res = false;
                            break;
                        }
                        res = GlobalFunction.ProcessFlow.StepFlow_UpCameraViewSingleBoxInTray(row, col);
                    }
                    else
                    {
                        //全自动模式                        
                        if (GlobalParameters.productconfig.boxlensConfig.UpCameraViewAllBox == true)
                        {
                            res = true;
                            if (GlobalParameters.flagassembly.befirstproductflag == true)
                            {
                                //只有在第一个产品组装时会根据产品Box物料盘勾选状态自动识别所有产品Box
                                res = GlobalFunction.ProcessFlow.StepFlow_UpCameraViewBoxInTray(true);
                            }
                            if (res == true)
                            {
                                //获取即将被夹取的产品Box索引号
                                res = GlobalFunction.ProcessFlow.SearchRecognizedBoxInTray(ref GlobalParameters.processdata.currentBoxIndex);
                            }
                        }
                        else
                        {
                            res = GlobalFunction.ProcessFlow.StepFlow_UpCameraViewBoxInTray(false);
                        }
                    }
                    break;

                case Enum_WorkFlowList.UPCAMVIEW_LENS:
                    //上相机识别Lens
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        //单步骤模式
                        GlobalFunction.ProcessFlow.GetLensLocationInTray(GlobalParameters.processdata.currentLensIndex, ref row);
                        if (GlobalParameters.processdata.lensload[row].exists == false)
                        {
                            GlobalFunction.updateStatusDelegate("料盘中此Lens未勾选", Enum_MachineStatus.ERROR);
                            res = false;
                            break;
                        }
                        res = GlobalFunction.ProcessFlow.StepFlow_UpCameraViewSingleLensInTray(row);
                    }
                    else
                    {
                        //全自动模式
                        if (GlobalParameters.productconfig.boxlensConfig.UpCameraViewAllLens == true)
                        {
                            res = true;
                            if (GlobalParameters.flagassembly.befirstproductflag == true)
                            {
                                //只有在第一个产品组装时会根据Lens物料盘勾选状态自动识别所有Lens
                                res = GlobalFunction.ProcessFlow.StepFlow_UpCameraViewLensInTray(true);
                            }
                            if (res == true)
                            {
                                //获取即将被夹取的Lens索引号
                                res = GlobalFunction.ProcessFlow.SearchRecognizedLensInTray(ref GlobalParameters.processdata.currentLensIndex);
                            }
                        }
                        else
                        {
                            res = GlobalFunction.ProcessFlow.StepFlow_UpCameraViewLensInTray(false);
                        }
                    }
                    break;

                case Enum_WorkFlowList.PICK_BOX:
                    //从料盘夹取产品盒子
                    GlobalFunction.ProcessFlow.GetBoxLocationInTray(GlobalParameters.processdata.currentBoxIndex, ref row, ref col);
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        if (GlobalParameters.processdata.boxload[row, col].bechecked == false)
                        {
                            GlobalFunction.updateStatusDelegate("上相机未识别过此产品Box", Enum_MachineStatus.ERROR);
                            res = false;
                            break;
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_PickBoxFromBoxTray(row, col);
                    break;

                case Enum_WorkFlowList.BOX_TOSCANNER:
                    //产品盒子移动到扫码器
                    res = GlobalFunction.ProcessFlow.StepFlow_BoxToQRCodeScanPosition();
                    break;

                case Enum_WorkFlowList.GET_BOXSN:
                    //获取产品SN信息
                    res = GlobalFunction.ProcessFlow.StepFlow_GetProductSN(ref GlobalParameters.processdata.currentProductSN);
                    break;

                case Enum_WorkFlowList.BOX_TONEST:
                    //产品盒子放入Nest
                    res = GlobalFunction.ProcessFlow.StepFlow_PlaceBoxIntoNest();
                    break;

                case Enum_WorkFlowList.POWER_ON:
                    //产品加电
                    res = GlobalFunction.ProcessFlow.StepFlow_PowerTurnOnOff(true);
                    break;

                case Enum_WorkFlowList.DNCAMVIEW_WINDOW:
                    //下相机识别盒子窗口
                    res = GlobalFunction.ProcessFlow.StepFlow_DownCameraRecognizeBoxWindow();
                    break;

                case Enum_WorkFlowList.GET_LASERSPOTPOS:
                    //下相机识别光斑
                    for (int i = 0; i < 3; i++)
                    {
                        res = GlobalFunction.ProcessFlow.StepFlow_DownCameraRecognizeLaserSpot();
                        if (res == true)
                        {
                            break;
                        }
                    }
                    if (res == true)
                    {
                        GlobalParameters.processdata.beforelenslaserSpotRecognize = GlobalParameters.processdata.laserSpotRecognize;
                    }
                    break;

                case Enum_WorkFlowList.CHECKSPOT_BEFOREALIGN:
                    //耦合前光斑确认
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        switch (GlobalParameters.productconfig.processConfig.alignmentWay)
                        {
                            case Enum_AlignmentWay.WINDOW_LENS:
                                //Lens中心和产品Box窗口中心对位
                                dialogResult = MessageBox.Show("当前产品Box窗口中心是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                break;

                            case Enum_AlignmentWay.LASER_LENS:
                                //Lens中心和多通道光斑平衡中心对位
                                dialogResult = MessageBox.Show("当前产品Box窗口中心和光斑中心是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                break;

                            case Enum_AlignmentWay.LASER_LASER:
                                //Lens中心和光斑中心对位
                                dialogResult = MessageBox.Show("当前产品光斑中心是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                break;
                        }
                        if (dialogResult == DialogResult.No)
                        {
                            res = false;
                            break;
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_CheckSpecBeforeAlign();
                    break;

                case Enum_WorkFlowList.PICK_LENS:
                    //从料盘夹取Lens
                    if (GlobalParameters.flagassembly.lensongripperflag == true)
                    {
                        //如果由于某种原因夹爪上已有Lens，则不需要再夹取Lens了
                        GlobalFunction.updateStatusDelegate("夹爪上已有Lens", Enum_MachineStatus.NORMAL);
                        res = true;
                        break;
                    }
                    GlobalFunction.ProcessFlow.GetLensLocationInTray(GlobalParameters.processdata.currentLensIndex, ref row);
                    if ((GlobalParameters.systemconfig.SystemOperationConfig.paralellprocess == true) && (GlobalParameters.flagassembly.continueflag == true))
                    {
                        //全自动模式下多线程方式（建议将此步骤配置在产品加电步骤前，可以和产品加电及后续Box窗口识别和识别光斑等步骤同时执行，避免撞机风险并且可以降低HPU）
                        //建立带参数的线程
                        GlobalFunction.updateStatusDelegate("开启自动夹取Lens线程", Enum_MachineStatus.NORMAL);
                        GlobalParameters.picklensThread = new Thread(new ParameterizedThreadStart(Thread_AutoPickLens));
                        GlobalParameters.picklensThread.Start(row);
                        return true;
                    }
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        if (GlobalParameters.processdata.lensload[row].bechecked == false)
                        {
                            GlobalFunction.updateStatusDelegate("上相机未识别过此Lens", Enum_MachineStatus.ERROR);
                            res = false;
                            break;
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_PickLensFromLensTray(row);
                    break;

                case Enum_WorkFlowList.LENS_TOBOX:
                    //Lens放入产品盒子
                    if ((GlobalParameters.systemconfig.SystemOperationConfig.paralellprocess == true) && (GlobalParameters.flagassembly.continueflag == true))
                    {
                        if (GlobalParameters.flagassembly.lensongripperflag == false)
                        {
                            //全自动模式下多线程方式需要等待夹取Lens步骤运行结束
                            while (GlobalParameters.flagassembly.picklensthreadaliveflag == true)
                            {
                                Thread.Sleep(10);
                            }
                            res = GlobalParameters.processdata.pickLensThreadResult;
                            if (res == false)
                            {
                                GlobalFunction.updateStatusDelegate("夹爪上没有Lens", Enum_MachineStatus.ERROR);
                                break;
                            }
                        }
                    }
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        dialogResult = MessageBox.Show("当前产品Box窗口中心是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            res = false;
                            break;
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_PlaceLensIntoBox();
                    if (res == false)
                    {
                        //恢复X2_Y2_Z2轴速度百分比
                        GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", 100);
                        GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y2", 100);
                        GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", 100);
                    }
                    break;

                case Enum_WorkFlowList.ALIGN_LENS:
                    //Lens耦合
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        switch (GlobalParameters.productconfig.processConfig.alignmentWay)
                        {
                            case Enum_AlignmentWay.WINDOW_LENS:
                                //Lens中心和产品Box窗口中心对位
                                dialogResult = MessageBox.Show("当前产品Box窗口中心是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                break;

                            case Enum_AlignmentWay.LASER_LENS:
                                //Lens中心和多通道光斑平衡中心对位
                                dialogResult = MessageBox.Show("当前产品Box窗口中心和光斑中心是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                break;

                            case Enum_AlignmentWay.LASER_LASER:
                                //Lens中心和光斑中心对位
                                dialogResult = MessageBox.Show("当前产品光斑中心是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                break;
                        }
                        if (dialogResult == DialogResult.No)
                        {
                            res = false;
                            break;
                        }
                    }
                    GlobalParameters.flagassembly.pressdownlensflag = true;
                    res = GlobalFunction.ProcessFlow.StepFlow_AlignLens();
                    if (res == false)
                    {
                        //恢复X2_Y2_Z2轴速度百分比
                        GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", 100);
                        GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y2", 100);
                        GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", 100);
                    }
                    GlobalParameters.flagassembly.pressdownlensflag = false;
                    break;

                case Enum_WorkFlowList.DIP_EPOXY:
                    //蘸胶
                    if ((GlobalParameters.systemconfig.SystemOperationConfig.paralellprocess == true) && (GlobalParameters.flagassembly.continueflag == true))
                    {
                        //全自动模式下多线程方式（建议将此步骤配置在Lens放入产品盒子步骤前，可以和Lens放入产品盒子及Lens耦合同时执行，避免撞机风险并且可以降低HPU）
                        GlobalFunction.updateStatusDelegate("开启自动蘸胶线程", Enum_MachineStatus.NORMAL);
                        GlobalParameters.epoxydipThread = new Thread(Thread_AutoDipEpoxy);
                        GlobalParameters.epoxydipThread.Start();
                        return true;
                    }
                    else
                    {
                        //非多线程方式
                        res = GlobalFunction.ProcessFlow.StepFlow_DispenserEpoxyDip();
                    }
                    break;

                case Enum_WorkFlowList.EPOXY_BOX:
                    //点胶到产品盒子
                    if ((GlobalParameters.systemconfig.SystemOperationConfig.paralellprocess == true) && (GlobalParameters.flagassembly.continueflag == true))
                    {
                        //全自动模式下多线程方式需要等待蘸胶步骤运行结束
                        while (GlobalParameters.flagassembly.epoxydipthreadaliveflag == true)
                        {
                            Thread.Sleep(10);
                        }
                        res = GlobalParameters.processdata.epoxyDipThreadResult;
                        if (res == false)
                        {
                            GlobalFunction.updateStatusDelegate("蘸胶步骤没有运行成功", Enum_MachineStatus.ERROR);
                            break;
                        }
                    }

                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        dialogResult = MessageBox.Show("当前产品Box窗口中心是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            res = false;
                            break;
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_DispenserEpoxyOnBox();
                    if (res == false)
                    {
                        //恢复X1_Y1_Z1轴速度百分比
                        res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", 100);
                        res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y1", 100);
                        res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z1", 100);
                    }
                    break;

                case Enum_WorkFlowList.EPOXY_WIPE:
                    //擦胶
                    if ((GlobalParameters.systemconfig.SystemOperationConfig.paralellprocess == true) && (GlobalParameters.flagassembly.continueflag == true))
                    {
                        //全自动模式下多线程方式（建议将此步骤配置在贴装Lens步骤前，可以和Lens贴装及后续UV步骤同时执行，避免撞机风险并且可以降低HPU）
                        GlobalFunction.updateStatusDelegate("开启自动擦胶线程", Enum_MachineStatus.NORMAL);
                        GlobalParameters.epoxywipeThread = new Thread(Thread_AutoEpoxyWipe);
                        GlobalParameters.epoxywipeThread.Start();
                        return true;
                    }
                    else
                    {
                        //非多线程方式
                        res = GlobalFunction.ProcessFlow.StepFlow_DispenserEpoxyWipe();
                    }
                    break;

                case Enum_WorkFlowList.ATTACH_LENS:
                    //贴装Lens
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        dialogResult = MessageBox.Show("之前是否已成功执行过Lens耦合步骤", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            res = false;
                            break;
                        }
                    }
                    GlobalParameters.flagassembly.pressdownlensflag = true;
                    res = GlobalFunction.ProcessFlow.StepFlow_AttachLens();
                    if (res == false)
                    {
                        //恢复X2_Y2_Z2轴速度百分比
                        GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", 100);
                        GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y2", 100);
                        GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", 100);
                    }
                    GlobalParameters.flagassembly.pressdownlensflag = false;
                    break;

                case Enum_WorkFlowList.CHECKLENS_BEFOREUVCURE:
                    //UV固化前Lens中心确认
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        dialogResult = MessageBox.Show("当前产品Box窗口中心是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            res = false;
                            break;
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_CheckLensCenterBeforeUVCure();
                    break;

                case Enum_WorkFlowList.CHECKSPOT_BEFOREUVCURE:
                    //UV固化前光斑确认
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        dialogResult = MessageBox.Show("当前产品Box窗口中心是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            res = false;
                            break;
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_CheckSpotBeforeUVCure();
                    break;

                case Enum_WorkFlowList.UV_LENS:
                    //UV固化Lens
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        dialogResult = MessageBox.Show("当前产品Lens是否已成功贴装", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            res = false;
                            break;
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_UVLens();
                    break;

                case Enum_WorkFlowList.RELEASE_LENS:
                    //释放Lens
                    res = GlobalFunction.ProcessFlow.StepFlow_ReleaseLens();
                    if ((res == true) && (GlobalParameters.flagassembly.singlestepflag == false))
                    {
                        //全自动模式下装配成功
                        GlobalFunction.ProcessFlow.GetBoxLocationInTray(GlobalParameters.processdata.currentBoxIndex, ref row, ref col);
                        GlobalParameters.processdata.boxload[row, col].finalresult = Enum_FinalResult.PASS;
                    }
                    break;

                case Enum_WorkFlowList.CHECKLENS_AFTERUVCURE:
                    //UV固化后Lens中心确认
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        dialogResult = MessageBox.Show("当前产品Box窗口是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            res = false;
                            break;
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_CheckLensCenterAfterUVCure();
                    break;

                case Enum_WorkFlowList.CHECKSPOT_AFTERUVCURE:
                    //UV固化后光斑确认
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        dialogResult = MessageBox.Show("当前产品Box窗口是否已成功识别过", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            res = false;
                            break;
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_CheckSpotAfterUVCure();
                    break;

                case Enum_WorkFlowList.POWER_OFF:
                    //产品下电
                    res = GlobalFunction.ProcessFlow.StepFlow_PowerTurnOnOff(false);
                    break;

                case Enum_WorkFlowList.PICK_BOXFROMNEST:
                    //从Nest中取出产品盒子
                    if ((GlobalParameters.systemconfig.SystemOperationConfig.paralellprocess == true) && (GlobalParameters.flagassembly.continueflag == true))
                    {
                        //全自动模式下多线程方式，如果不是人工预点胶工艺则需要等待擦胶步骤运行结束
                        if (GlobalParameters.productconfig.processConfig.manualPreDispensing == false)
                        {
                            while (GlobalParameters.flagassembly.epoxywipethreadaliveflag == true)
                            {
                                Thread.Sleep(10);
                            }
                            res = GlobalParameters.processdata.epoxyWipeThreadResult;
                            if (res == false)
                            {
                                GlobalFunction.updateStatusDelegate("擦胶步骤没有运行成功", Enum_MachineStatus.ERROR);
                                break;
                            }
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_PickBoxFromNest();
                    break;

                case Enum_WorkFlowList.BOX_TOTRAY:
                    //产品盒子放回料盘
                    GlobalFunction.ProcessFlow.GetBoxLocationInTray(GlobalParameters.processdata.currentBoxIndex, ref row, ref col);
                    res = GlobalFunction.ProcessFlow.StepFlow_PlaceBoxIntoUnloadTray(row, col);
                    break;

                case Enum_WorkFlowList.DISCARD_LENS:
                    //Lens抛料
                    if (GlobalParameters.flagassembly.singlestepflag == true)
                    {
                        dialogResult = MessageBox.Show("是否抛弃当前夹爪上的Lens", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            res = false;
                            break;
                        }
                    }
                    res = GlobalFunction.ProcessFlow.StepFlow_DiscardLens();
                    break;

                case Enum_WorkFlowList.CHECK_BOMLIST:
                    //检查产品SN和DC物料捆绑关系
                    if (GlobalParameters.productconfig.processConfig.mesEnable == true)
                    {
                        if (GlobalParameters.HardwareInitialStatus.MESFunctionTools_InitialStatus == true)
                        {
                            res = GlobalFunction.ProcessFlow.StepFlow_mesCheckBomList(dcCollection);
                        }
                        else
                        {
                            GlobalFunction.updateStatusDelegate("MES未初始化，核查DC物料操作无法执行", Enum_MachineStatus.REMINDER);
                            GlobalFunction.ProcessFlow.GetBoxLocationInTray(GlobalParameters.processdata.currentBoxIndex, ref row, ref col);
                            GlobalParameters.processdata.boxunload[row, col].finalresult = Enum_FinalResult.MESFAIL;
                            GlobalParameters.processdata.mesHoldReason = "MES Check BOM List Fail";
                        }
                    }
                    else
                    {
                        GlobalFunction.updateStatusDelegate("MES功能未开启，DC物料核查操作未执行", Enum_MachineStatus.REMINDER);
                        return true;
                    }
                    break;

                case Enum_WorkFlowList.AUTO_REMOVE:
                    //自动拆料
                    if (GlobalParameters.productconfig.processConfig.mesEnable == true)
                    {
                        if (GlobalParameters.HardwareInitialStatus.MESFunctionTools_InitialStatus == true)
                        {
                            res = GlobalFunction.ProcessFlow.StepFlow_mesAutoRemove();
                        }
                        else
                        {
                            GlobalFunction.updateStatusDelegate("MES未初始化，拆料操作无法执行", Enum_MachineStatus.REMINDER);
                            GlobalFunction.ProcessFlow.GetBoxLocationInTray(GlobalParameters.processdata.currentBoxIndex, ref row, ref col);
                            GlobalParameters.processdata.boxunload[row, col].finalresult = Enum_FinalResult.MESFAIL;
                            GlobalParameters.processdata.mesHoldReason = "MES Auto Remove Fail";
                        }
                    }
                    else
                    {
                        GlobalFunction.updateStatusDelegate("MES功能未开启，自动拆料操作未执行", Enum_MachineStatus.REMINDER);
                        return true;
                    }
                    break;

                case Enum_WorkFlowList.COMPONENT_ISSUE:
                    //自动发料
                    if (GlobalParameters.productconfig.processConfig.mesEnable == true)
                    {
                        //根据Box索引号返回它在Box物料盘中所在的行列号
                        GlobalFunction.ProcessFlow.GetBoxLocationInTray(GlobalParameters.processdata.currentBoxIndex, ref row, ref col);
                        if (GlobalParameters.HardwareInitialStatus.MESFunctionTools_InitialStatus == true)
                        {
                            res = GlobalFunction.ProcessFlow.StepFlow_mesComponentIssue(dcCollection);
                            if (res == false)
                            {
                                GlobalParameters.processdata.boxunload[row, col].finalresult = Enum_FinalResult.MESFAIL;
                                GlobalParameters.processdata.mesHoldReason = "MES Component Issue Fail";
                            }
                        }
                        else
                        {                            
                            GlobalParameters.processdata.boxunload[row, col].finalresult = Enum_FinalResult.MESFAIL;
                            GlobalParameters.processdata.mesHoldReason = "MES Component Issue Fail";
                            GlobalFunction.updateStatusDelegate("MES未初始化，发料操作无法执行", Enum_MachineStatus.REMINDER);
                        }
                    }
                    else
                    {
                        GlobalFunction.updateStatusDelegate("MES功能未开启，自动发料操作未执行", Enum_MachineStatus.REMINDER);
                        return true;
                    }
                    break;

                case Enum_WorkFlowList.AUTO_MOVEOUT:
                    //自动过站
                    if ((GlobalParameters.productconfig.processConfig.mesEnable == true) && (GlobalParameters.productconfig.processConfig.moveoutEnable == true))
                    {
                        //根据Box索引号返回它在Box物料盘中所在的行列号
                        GlobalFunction.ProcessFlow.GetBoxLocationInTray(GlobalParameters.processdata.currentBoxIndex, ref row, ref col);
                        if (GlobalParameters.HardwareInitialStatus.MESFunctionTools_InitialStatus == true)
                        {
                            //全自动模式下检测耦合组装是否成功
                            if ((GlobalParameters.flagassembly.continueflag == true) && (GlobalParameters.processdata.boxload[row, col].finalresult == Enum_FinalResult.FAIL))
                            {
                                //耦合组装不成功则直接跳过不执行
                                GlobalFunction.updateStatusDelegate("产品耦合组装失败，不执行过站操作", Enum_MachineStatus.REMINDER);
                                return true;
                            }
                            res = GlobalFunction.ProcessFlow.StepFlow_mesMoveOut();
                            if (res == false)
                            {
                                GlobalParameters.processdata.boxunload[row, col].finalresult = Enum_FinalResult.MESFAIL;
                                GlobalParameters.processdata.mesHoldReason = "MES Move Out Fail";
                            }
                        }
                        else
                        {
                            GlobalParameters.processdata.boxunload[row, col].finalresult = Enum_FinalResult.MESFAIL;
                            GlobalParameters.processdata.mesHoldReason = "MES Move Out Fail";
                            GlobalFunction.updateStatusDelegate("MES未初始化，过站操作无法执行", Enum_MachineStatus.REMINDER);
                            return true;
                        }
                    }
                    else
                    {
                        GlobalFunction.updateStatusDelegate("MES功能未开启，自动过站操作未执行", Enum_MachineStatus.REMINDER);
                        return true;
                    }
                    break;

                case Enum_WorkFlowList.AUTO_HOLD:
                    //自动Hold
                    if (GlobalParameters.productconfig.processConfig.mesEnable == true)
                    {
                        if (GlobalParameters.productconfig.processConfig.autoHold == true)
                        {
                            GlobalFunction.ProcessFlow.GetBoxLocationInTray(GlobalParameters.processdata.currentBoxIndex, ref row, ref col);
                            if (GlobalParameters.processdata.boxunload[row, col].finalresult == Enum_FinalResult.MESFAIL)
                            {
                                res = GlobalFunction.ProcessFlow.StepFlow_mesAutoHold();
                            }
                            else
                            {
                                GlobalParameters.processdata.mesHoldReason = string.Empty;
                                res = true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        GlobalFunction.updateStatusDelegate("MES功能未开启，自动Hold操作未执行", Enum_MachineStatus.REMINDER);
                        return true;
                    }
                    break;
            }

            if (res == true)
            {
                string str = GlobalParameters.processdata.currentStepFlowName + "成功";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                cFrameWork.SetTitleInfo(str, eTitleType.NoError);
            }
            else
            {
                string str = GlobalParameters.processdata.currentStepFlowName + "失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                cFrameWork.SetTitleInfo(str, eTitleType.AlrmError);
            }

            return res;
        }
        #endregion

        #region 单个产品全自动流程
        private bool AutoRunFocusingLensAttachProcess()
        {
            bool res = false;
            string str = string.Empty;

            for (int stepNum = 0; stepNum < GlobalParameters.productconfig.processConfig.workflowStepNum; stepNum++)
            {
                if (GlobalParameters.productconfig.processConfig.workflowArray[stepNum] != -1)
                {
                    Enum_WorkFlowList stepflow = (Enum_WorkFlowList)GlobalParameters.productconfig.processConfig.workflowArray[stepNum];
                    res = AutoRunSingleStep(stepflow);                    
                    if (GlobalParameters.flagassembly.estopflag == true)
                    {
                        //急停开关已按下
                        GlobalParameters.flagassembly.continueflag = false;
                        return false;
                    }
                    else if (GlobalParameters.flagassembly.stopflag == true)
                    {
                        //人工点击了主界面停止按钮
                        GlobalParameters.flagassembly.continueflag = false;
                        
                        //强制执行异常处理
                        GlobalFunction.ProcessFlow.AutoRunSingleStepAbnormalHandling(stepflow);
                        return false;
                    }
                    if (res == true)
                    {
                        //单步骤运动成功
                        GlobalParameters.flagassembly.continueflag = true;
                    }
                    else
                    {
                        //单步骤运行结果异常处理
                        res = GlobalFunction.ProcessFlow.AutoRunSingleStepAbnormalHandling(stepflow);
                        if (res == true)
                        {
                            //异常处理成功，继续执行自动流程
                            GlobalParameters.flagassembly.continueflag = true;
                            break;
                        }
                        else
                        {
                            //异常处理失败，强制终止自动流程
                            GlobalParameters.flagassembly.continueflag = false;
                            break;
                        }
                    }
                }
            }

            return true;
        }
        #endregion

        //----任务线程

        #region 全自动运行线程
        private void Thread_AutoRunWorkFlow()
        {
            bool res = false;

            //初始化运行标志
            GlobalParameters.flagassembly.mainthreadaliveflag = true;
            GlobalParameters.flagassembly.singlestepflag = false;
            GlobalParameters.flagassembly.continueflag = true;
            GlobalParameters.flagassembly.befirstproductflag = true;
            GlobalParameters.flagassembly.checkepoxyonboxflag = true;
            GlobalParameters.flagassembly.lensongripperflag = false;

            //状态指示灯切换
            GlobalFunction.updateRunStatusLampDelegate(Enum_RunStatus.RUN_ING);

            //全自动流程
            for (int row = 0; row < GlobalParameters.productconfig.boxlensConfig.boxTray.rowcount; row++)
            {
                if (GlobalParameters.flagassembly.continueflag == false)
                {
                    break;
                }
                for (int col = 0; col < GlobalParameters.productconfig.boxlensConfig.boxTray.colcount; col++)
                {
                    if (GlobalParameters.flagassembly.continueflag == false)
                    {
                        break;
                    }

                    //检查DC物料实时状态
                    if (GlobalParameters.productconfig.processConfig.mesEnable == true)
                    {
                        for (int DC_Index = 0; DC_Index < GlobalParameters.productconfig.processConfig.dcCount; DC_Index++)
                        {
                            if (GlobalFunction.getDCTreeStatusDelegate(DC_Index) == Color.Red)
                            {
                                GlobalFunction.updateStatusDelegate("胶水或者Lens已超出有效期", Enum_MachineStatus.ERROR);
                                MessageBox.Show("胶水或者Lens已超出有效期，自动流程终止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                GlobalParameters.flagassembly.continueflag = false;
                            }
                             else if (GlobalFunction.getDCTreeStatusDelegate(DC_Index) == Color.Yellow)
                            {
                                GlobalFunction.updateStatusDelegate("请注意胶水或者Lens即将超出有效期", Enum_MachineStatus.REMINDER);
                            }
                        }
                    }

                    //当前产品全自动耦合组装
                    if (GlobalParameters.processdata.boxload[row, col].exists == true)
                    {
                        //根据产品Box在物料盘中所在的行列号获取其对应的索引号
                        GlobalFunction.ProcessFlow.GetBoxIndexInTray(row, col, ref GlobalParameters.processdata.currentBoxIndex);

                        //初始化当前产品工艺数据
                        GlobalFunction.ProcessFlow.StepFlow_InitializeFinalProcessData();

                        //复位工作流程各运行中间标志
                        GlobalFunction.ProcessFlow.ResetWorkFlowFlag();

                        //防呆措施：恢复各轴运动速度百分比，避免全自动流程中可能出现的异常情况导致的某些轴运动速度慢现象
                        GlobalFunction.MotionTools.Motion_SetAllAxisSpeedPercent(100);

                        //开始计时
                        GlobalParameters.processdata.startTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        //运行当前产品的全自动组装流程
                        res = AutoRunFocusingLensAttachProcess();

                        //停止计时
                        GlobalParameters.processdata.endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        //清除全自动模式下组装料盘中第一个产品标记
                        GlobalParameters.flagassembly.befirstproductflag = false;

                        //检测急停开关是否已按下或者人工点击了主界面停止按钮
                        if ((GlobalParameters.flagassembly.estopflag == true) || (GlobalParameters.flagassembly.stopflag == true))
                        {
                            GlobalParameters.flagassembly.continueflag = false;
                            break;
                        }

                        //获取当前产品最终组装结果
                        GlobalParameters.processdata.finalResult = Enum.GetName(typeof(Enum_FinalResult), GlobalParameters.processdata.boxload[row, col].finalresult);
                        if ((GlobalParameters.processdata.boxload[row, col].finalresult != Enum_FinalResult.FAIL) && (GlobalParameters.flagassembly.checkepoxyonboxflag == true))
                        {
                            //只要有一次耦合组装成功，产品Box料盘中后续每个产品都不再需要弹窗人工检查确认点胶状态
                            GlobalParameters.flagassembly.checkepoxyonboxflag = false;
                        }

                        //产品工艺数据上传数据库
                        if (GlobalParameters.productconfig.processConfig.processDataUploadIntoDatabase == true)
                        {
                            //将组装成功的产品工艺数据上传数据库
                            if ((GlobalParameters.flagassembly.continueflag == true) && (GlobalParameters.processdata.boxload[row, col].finalresult != Enum_FinalResult.FAIL))
                            {
                                GlobalFunction.ProcessFlow.StepFlow_UploadProcessDataIntoDatabase();
                                //if (GlobalParameters.productconfig.processConfig.recordAllLaserOffsetAfterLens2 == true)
                                //{

                                //}
                            }
                        }

                        //产品工艺数据本地记录保存
                        GlobalFunction.ProcessFlow.StepFlow_SaveLocalProcessData();
                    }
                }
            }

            //防呆措施：恢复各轴运动速度百分比，避免全自动流程中可能出现的异常情况导致的某些轴运动速度变慢现象
            GlobalFunction.MotionTools.Motion_SetAllAxisSpeedPercent(100);

            if (GlobalParameters.flagassembly.estopflag == true)
            {
                GlobalFunction.updateStatusDelegate("急停开关已按下，全自动流程紧急中止", Enum_MachineStatus.ERROR);
                GlobalFunction.updateRunStatusLampDelegate(Enum_RunStatus.RUN_STOP);
            }
            else
            {
                if (GlobalParameters.flagassembly.stopflag == true)
                {
                    GlobalFunction.updateStatusDelegate("人工点击停止按钮，全自动流程被迫中止", Enum_MachineStatus.ERROR);
                }
                else
                {
                    GlobalFunction.updateStatusDelegate("全自动流程运行结束", Enum_MachineStatus.REMINDER);
                }

                //防呆措施：将产品Box夹爪气缸和胶针气缸安全上抬，防止撞机
                res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
                if (res == true)
                {
                    //各轴回安全位
                    GlobalFunction.MotionTools.Motion_AllMoveToSafe();
                }

                //状态指示灯切换
                if (GlobalParameters.flagassembly.stopflag == true)
                {
                    GlobalFunction.updateRunStatusLampDelegate(Enum_RunStatus.RUN_STOP);
                }
                else
                {
                    GlobalFunction.updateRunStatusLampDelegate(Enum_RunStatus.RUN_IDLE);
                }
            }

            //复位运行标志
            GlobalParameters.flagassembly.continueflag = false;
            GlobalParameters.flagassembly.singlestepflag = true;
            GlobalParameters.flagassembly.stopflag = true;
            GlobalParameters.flagassembly.mainthreadaliveflag = false;

            //回收线程占用的资源
            GC.Collect();
        }
        #endregion

        #region 自动夹取Lens线程
        private void Thread_AutoPickLens(object row)
        {
            GlobalParameters.flagassembly.picklensthreadaliveflag = true;
            GlobalParameters.processdata.pickLensThreadResult = GlobalFunction.ProcessFlow.StepFlow_PickLensFromLensTray((int)row);
            GlobalParameters.flagassembly.picklensthreadaliveflag = false;
            if (GlobalParameters.processdata.pickLensThreadResult == true)
            {
                GlobalFunction.updateStatusDelegate("自动夹取Lens线程运行成功", Enum_MachineStatus.NORMAL);
            }
            else
            {
                GlobalFunction.updateStatusDelegate("自动夹取Lens线程运行失败", Enum_MachineStatus.ERROR);
            }
        }
        #endregion

        #region 自动蘸胶线程
        private void Thread_AutoDipEpoxy()
        {
            GlobalParameters.flagassembly.epoxydipthreadaliveflag = true;
            GlobalParameters.processdata.epoxyDipThreadResult = GlobalFunction.ProcessFlow.StepFlow_DispenserEpoxyDip();
            GlobalParameters.flagassembly.epoxydipthreadaliveflag = false;
            if (GlobalParameters.processdata.epoxyDipThreadResult == true)
            {
                GlobalFunction.updateStatusDelegate("自动蘸胶线程运行成功", Enum_MachineStatus.NORMAL);
            }
            else
            {
                GlobalFunction.updateStatusDelegate("自动蘸胶线程运行失败", Enum_MachineStatus.ERROR);
            }
        }
        #endregion

        #region 自动擦胶线程
        private void Thread_AutoEpoxyWipe()
        {
            GlobalParameters.flagassembly.epoxywipethreadaliveflag = true;
            GlobalParameters.processdata.epoxyWipeThreadResult = GlobalFunction.ProcessFlow.StepFlow_DispenserEpoxyWipe();
            GlobalParameters.flagassembly.epoxywipethreadaliveflag = false;
            if (GlobalParameters.processdata.epoxyWipeThreadResult == true)
            {
                GlobalFunction.updateStatusDelegate("自动擦胶线程运行成功", Enum_MachineStatus.NORMAL);
            }
            else
            {
                GlobalFunction.updateStatusDelegate("自动擦胶线程运行失败", Enum_MachineStatus.ERROR);
            }
        }
        #endregion

        //----委托事件接口

        #region 委托事件:主界面机台状态指示LED刷新
        private delegate void UpdateRunStatusLampInvoke(Enum_RunStatus runstatus);
        private void updateRunStatusLamp(Enum_RunStatus runstatus)
        {
            string str = "";

            if (lab_LedTower.InvokeRequired)
            {
                UpdateRunStatusLampInvoke lab_LedTower_Invoke = new UpdateRunStatusLampInvoke(updateRunStatusLamp);
                lab_LedTower.Invoke(lab_LedTower_Invoke, new object[] { runstatus });
            }
            else
            {
                switch (runstatus)
                {
                    case Enum_RunStatus.RUN_STOP:
                        GlobalFunction.IOControlTools.SetSignalTowerLightStatus(Enum_SignalTowerLamp.YELLOW);
                        lab_Status.Text = "停运";
                        lab_LedTower.ForeColor = Color.Yellow;
                        str = "机台停止运行";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.REMINDER);
                        cFrameWork.SetInitializationPanelMessage(str);
                        cFrameWork.SetTitleInfo(str, eTitleType.NoError);
                        break;
                    case Enum_RunStatus.RUN_ING:
                        GlobalFunction.IOControlTools.SetSignalTowerLightStatus(Enum_SignalTowerLamp.GREEN);
                        lab_Status.Text = "运行";
                        lab_LedTower.ForeColor = Color.Green;
                        str = "机台运行中...";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
                        cFrameWork.SetInitializationPanelMessage(str);
                        cFrameWork.SetTitleInfo(str, eTitleType.Completed);
                        break;
                    case Enum_RunStatus.RUN_DOWN:
                        GlobalFunction.IOControlTools.SetSignalTowerLightStatus(Enum_SignalTowerLamp.RED);
                        lab_Status.Text = "停机";
                        lab_LedTower.ForeColor = Color.Red;
                        str = "机台已停机";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                        cFrameWork.SetInitializationPanelMessage(str);
                        cFrameWork.SetTitleInfo(str, eTitleType.AlrmError);
                        break;
                    case Enum_RunStatus.RUN_ERR:
                        GlobalFunction.IOControlTools.SetSignalTowerLightStatus(Enum_SignalTowerLamp.RED);
                        lab_Status.Text = "出错";
                        lab_LedTower.ForeColor = Color.Red;
                        str = "机台运行出错";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                        cFrameWork.SetInitializationPanelMessage(str);
                        cFrameWork.SetTitleInfo(str, eTitleType.AlrmError);
                        break;
                    case Enum_RunStatus.RUN_IDLE:
                        GlobalFunction.IOControlTools.SetSignalTowerLightStatus(Enum_SignalTowerLamp.YELLOW);
                        lab_Status.Text = "待机";
                        lab_LedTower.ForeColor = Color.Yellow;
                        str = "机台待机中...";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.REMINDER);
                        cFrameWork.SetInitializationPanelMessage(str);
                        cFrameWork.SetTitleInfo(str, eTitleType.NoError);
                        break;
                    case Enum_RunStatus.RUN_WARNING:
                        GlobalFunction.IOControlTools.SetSignalTowerLightStatus(Enum_SignalTowerLamp.RED);
                        lab_Status.Text = "报警";
                        lab_LedTower.ForeColor = Color.Red;
                        str = "机台异常报警";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.REMINDER);
                        cFrameWork.SetInitializationPanelMessage(str);
                        cFrameWork.SetTitleInfo(str, eTitleType.NoError);
                        break;
                    case Enum_RunStatus.RUN_SUSPEND:
                        GlobalFunction.IOControlTools.SetSignalTowerLightStatus(Enum_SignalTowerLamp.YELLOW);
                        lab_Status.Text = "暂停";
                        lab_LedTower.ForeColor = Color.Yellow;
                        str = "机台运行暂停";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.REMINDER);
                        cFrameWork.SetInitializationPanelMessage(str);
                        cFrameWork.SetTitleInfo(str, eTitleType.NoError);
                        break;
                    case Enum_RunStatus.RUN_PROCESS:
                        GlobalFunction.IOControlTools.SetSignalTowerLightStatus(Enum_SignalTowerLamp.YELLOW);
                        lab_Status.Text = "Process";
                        lab_LedTower.ForeColor = Color.Yellow;
                        str = "Machine is processing...";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.REMINDER);
                        cFrameWork.SetInitializationPanelMessage(str);
                        cFrameWork.SetTitleInfo(str, eTitleType.NoError);
                        break;
                }
            }
        }
        #endregion

        #region 委托事件:主界面信息显示窗口记录刷新
        readonly object recordlogfile = new object();
        private delegate void SystemInfoListBoxInvoke(string str, Enum_MachineStatus status = Enum_MachineStatus.NORMAL, Enum_UPDATEMODE updatemode = Enum_UPDATEMODE.UPDATE_INFO);
        private void updateMachineStatus(string message, Enum_MachineStatus status = Enum_MachineStatus.NORMAL, Enum_UPDATEMODE updatemode = Enum_UPDATEMODE.UPDATE_INFO)
        {
            if (updatemode == Enum_UPDATEMODE.UPDATE_INFO)
            {
                if (lb_SystemInfo.InvokeRequired == true)
                {
                    //跨线程调用
                    SystemInfoListBoxInvoke lbInvoke = new SystemInfoListBoxInvoke(updateMachineStatus);
                    lb_SystemInfo.Invoke(lbInvoke, new object[] { message, status, updatemode });
                }
                else
                {
                    //非跨线程调用
                    int statusnum = (int)status;
                    string msgStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + message;
                    if (lb_SystemInfo.Items.Count >= 1000)
                    {
                        lb_SystemInfo.Items.RemoveAt(0);
                    }
                    switch (statusnum)
                    {
                        case (int)(Enum_MachineStatus.NORMAL):
                            message = "*" + message;
                            lb_SystemInfo.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + message);
                            break;
                        case (int)(Enum_MachineStatus.ERROR):
                            message = "#" + message;
                            lb_SystemInfo.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + message);
                            break;
                        case (int)(Enum_MachineStatus.REMINDER):
                            message = "$" + message;
                            lb_SystemInfo.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "->" + message);
                            break;
                    }
                    lb_SystemInfo.SelectedIndex = lb_SystemInfo.Items.Count - 1;

                    //本地Log记录
                    lock (recordlogfile)
                    {
                        //线程锁方式，防止多线程同时操作文件记录
                        if (GlobalParameters.systemconfig.SystemOperationConfig.recordlogfile == true)
                        {
                            string filePath = GeneralFunction.GetApplicationFilePath("LocalRecord\\DataRecord\\StatusInfo\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                            if (!File.Exists(filePath))
                            {
                                FileStream fs = File.Create(filePath);
                                fs.Close();
                            }
                            StreamWriter sw = new StreamWriter(filePath, true, Encoding.Default);
                            sw.WriteLine(msgStr);
                            sw.Flush();
                            sw.Close();
                        }
                    }
                }
            }
        }
        #endregion

        #region 委托事件:主界面压力传感器读值刷新
        private delegate void UpdatePressureForceReadingInvoke(double pressureforce);
        private void updatePressureForceReading(double pressureforce)
        {
            if (GlobalParameters.flagassembly.showpressureforceflag == true)
            {
                if (tb_pressureforce.InvokeRequired == true)
                {
                    //跨线程调用
                    UpdatePressureForceReadingInvoke tb_pressureforce_Invoke = new UpdatePressureForceReadingInvoke(updatePressureForceReading);
                    tb_pressureforce.Invoke(tb_pressureforce_Invoke, new object[] { pressureforce });
                }
                else
                {
                    //非跨线程调用
                    tb_pressureforce.Text = Math.Abs(pressureforce).ToString("F1");
                    tb_pressureforce.Refresh();
                }
            }
        }
        #endregion

        #region 委托事件:主界面加电源表窗口电压读数刷新
        private delegate void UpdateSourceMeterVoltageReadingInvoke();
        private void updateSourceMeterVoltageReading()
        {
            double voltage = 0;

            if (tb_SourceMeterVoltage.InvokeRequired == true)
            {
                //跨线程调用
                UpdateSourceMeterVoltageReadingInvoke tb_SourceMeterVoltage_Invoke = new UpdateSourceMeterVoltageReadingInvoke(updateSourceMeterVoltageReading);
                tb_SourceMeterVoltage.Invoke(tb_SourceMeterVoltage_Invoke, new object[] { });
            }
            else
            {
                //非跨线程调用
                GlobalFunction.SourceMetertools.Keilthly240x_QuerySourceValue("Keithley2401-1", SourceFuncType.VOLTAGE, ref voltage);
                tb_SourceMeterVoltage.Text = "+" + voltage.ToString("F3") + "V";
            }
        }
        #endregion

        #region 委托事件:主界面加电源表窗口电流读数刷新
        private delegate void UpdateSourceMeterCurrentReadingInvoke();
        private void updateSourceMeterCurrentReading()
        {
            double current = 0;

            if (tb_SourceMeterCurrent.InvokeRequired == true)
            {
                //跨线程调用
                UpdateSourceMeterCurrentReadingInvoke tb_SourceMeterCurrent_Invoke = new UpdateSourceMeterCurrentReadingInvoke(updateSourceMeterCurrentReading);
                tb_SourceMeterCurrent.Invoke(tb_SourceMeterCurrent_Invoke, new object[] { });
            }
            else
            {
                //非跨线程调用
                GlobalFunction.SourceMetertools.Keilthly240x_ReadKeithleyValue("Keithley2401-1", SourceFuncType.CURRENT, ref current);
                tb_SourceMeterCurrent.Text = current.ToString("F3") + "A";
            }
        }
        #endregion

        #region 委托事件:主界面产品Box发料盘状态刷新
        private delegate void UpdateBoxLoadTrayStatusInvoke(int row, int col, bool exist);
        private void updateBoxLoadTrayStatus(int row, int col, bool exist)
        {
            if (boxloadtray[row, col].InvokeRequired == true)
            {
                //跨线程调用
                UpdateBoxLoadTrayStatusInvoke boxloadtray_Invoke = new UpdateBoxLoadTrayStatusInvoke(updateBoxLoadTrayStatus);
                boxloadtray[row, col].Invoke(boxloadtray_Invoke, new object[] { row, col, exist });
            }
            else
            {
                //非跨线程调用
                boxloadtray[row, col].Checked = exist;
                GlobalParameters.processdata.boxload[row, col].exists = exist;
                if (exist == true)
                {
                    boxloadtray[row, col].BackColor = Color.Green;
                }
                else
                {
                    boxloadtray[row, col].BackColor = SystemColors.Control;
                }
            }
        }
        #endregion

        #region 委托事件:主界面产品Box收料盘状态刷新
        private delegate void UpdateBoxUnloadTrayStatusInvoke(int row, int col, bool exist, Enum_FinalResult finalresult);
        private void updateBoxUnloadTrayStatus(int row, int col, bool exist, Enum_FinalResult finalresult)
        {
            if (boxunloadtray[row, col].InvokeRequired == true)
            {
                //跨线程调用
                UpdateBoxUnloadTrayStatusInvoke boxunloadtray_Invoke = new UpdateBoxUnloadTrayStatusInvoke(updateBoxUnloadTrayStatus);
                boxunloadtray[row, col].Invoke(boxunloadtray_Invoke, new object[] { row, col, exist, finalresult });
            }
            else
            {
                //非跨线程调用
                boxunloadtray[row, col].Checked = exist;
                if (exist == true)
                {
                    switch (finalresult)
                    {
                        case Enum_FinalResult.PASS:
                            boxunloadtray[row, col].BackColor = Color.Green;
                            break;

                        case Enum_FinalResult.FAIL:
                            boxunloadtray[row, col].BackColor = Color.Red;
                            break;

                        case Enum_FinalResult.MESFAIL:
                            boxunloadtray[row, col].BackColor = Color.Orange;
                            break;
                    }
                }
            }
        }
        #endregion

        #region 委托事件:主界面Lens料盘状态刷新
        private delegate void UpdateLensLoadTrayStatusInvoke(int row, bool exist);
        private void updateLensLoadTrayStatus(int row, bool exist)
        {
            if (lensloadtray[row].InvokeRequired == true)
            {
                //跨线程调用
                UpdateLensLoadTrayStatusInvoke lensloadtray_Invoke = new UpdateLensLoadTrayStatusInvoke(updateLensLoadTrayStatus);
                lensloadtray[row].Invoke(lensloadtray_Invoke, new object[] { row, exist });
            }
            else
            {
                //非跨线程调用
                lensloadtray[row].Checked = exist;
                GlobalParameters.processdata.lensload[row].exists = exist;
                if (exist == true)
                {
                    lensloadtray[row].BackColor = Color.Green;
                }
                else
                {
                    lensloadtray[row].BackColor = SystemColors.Control;
                }
            }
        }
        #endregion

        #region 委托事件:主界面产品SN信息刷新
        private delegate void UpdateProdutSNInvoke(string productSN);
        private void updateProdutSN(string productSN)
        {
            if (tb_ProductSN.InvokeRequired == true)
            {
                //跨线程调用
                UpdateProdutSNInvoke tb_ProductSN_Invoke = new UpdateProdutSNInvoke(updateProdutSN);
                tb_ProductSN.Invoke(tb_ProductSN_Invoke, new object[] { productSN });
            }
            else
            {
                //非跨线程调用
                tb_ProductSN.Text = productSN;
                tb_ProductSN.Refresh();
            }
        }
        #endregion

        #region 委托事件:主界面DC物料实时状态获取委托事件接口
        private delegate Color GetDCTreeStatusInvoke(int DC_Index);
        private Color getDCTreeStatus(int DC_Index)
        {
            Color StatusIndicatorColor = new Color();
            if (lv_DCInfo.InvokeRequired == true)
            {
                //跨线程调用
                GetDCTreeStatusInvoke getDCStatus_Invoke = new GetDCTreeStatusInvoke(getDCTreeStatus);
                StatusIndicatorColor = (Color)lv_DCInfo.Invoke(getDCStatus_Invoke, new object[] { DC_Index });
            }
            else
            {
                //非跨线程调用
                if (DC_Index > lv_DCInfo.Items.Count - 1)
                {
                    StatusIndicatorColor = Color.Red;
                }
                else
                {
                    //无背景色：在有效期内；背景黄色：即将超出有效期；背景红色：超出有效期）
                    StatusIndicatorColor = lv_DCInfo.Items[DC_Index].SubItems[2].BackColor;
                }
            }
            //返回状态指示颜色
            return StatusIndicatorColor;
        }
        #endregion
    }
}
