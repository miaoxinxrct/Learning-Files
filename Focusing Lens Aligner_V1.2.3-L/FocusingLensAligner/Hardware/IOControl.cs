using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using STD_IDAQ;

namespace FocusingLensAligner
{
    #region IO设置信息集合

    public enum IOPolarity
    {
        Input,
        Output
    }

    public class IOConfig
    {
        public string Name { get; set; }
        public IOPolarity Polarity { get; set; }
        public int CardID { get; set; }
        public int PortNum { get; set; }
        public int ChannelNum { get; set; }
        public string Description { get; set; }
    }

    [Serializable]
    [System.Xml.Serialization.XmlRoot("IO Config")]
    public class IOConfigCollection
    {

        List<IOConfig> inputconfigs = new List<IOConfig>();
        List<IOConfig> outputconfigs = new List<IOConfig>();

        public List<IOConfig> InputConfigs
        {
            get
            {
                return inputconfigs;
            }
            set
            {
                inputconfigs = value;
            }
        }

        public List<IOConfig> OutputConfigs
        {
            get
            {
                return outputconfigs;
            }
            set
            {
                outputconfigs = value;
            }
        }
    }

    #endregion

    #region IO卡功能函数

    public class cIOControlTools
    {
        #region IO卡底层功能函数

        internal STD_IDAQ.IIO IOTool = null;
        internal STD_IDAQ.IDAQ DAQTool = null;
        private DAQTool DAQCard = new DAQTool();
        private IOConfigCollection ioconfig = new IOConfigCollection();
        
        private void Delay(int milliSecond)
        {
            System.Threading.Thread.Sleep(milliSecond);
        }

        public bool InitialDAQ()
        {
            bool res = false;
            res = STD_IGeneralTool.GeneralTool.ToTryLoad<IOConfigCollection>(ref ioconfig, GeneralFunction.GetConfigFilePath("IOConfig.xml"));
            if (!res)
            {
                MessageBox.Show("Load IO Config Fail!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return res;
            }
            res = DAQCard.IOInitial(ref IOTool);
            if (!res)
            {
                IOTool = null;
            }

            return res;
        }

        public bool CloseDAQ()
        {
            bool res = false;
            if (IOTool != null)
            {
                res = IOTool.DAQ_CloseDevice(new int[] {0, 1, 2});
            }

            return res;
        }

        public bool OpenDAQSetupPanel()
        {
            bool res = false;
            res = DAQCard.ShowDAQConfigPanel(true);
            return res;
        }

        public bool OpenIOSetupPanel()
        {
            bool res = true;

            IOConfigForm IOConfigFrm = new IOConfigForm();
            IOConfigFrm.TopLevel = true;
            IOConfigFrm.StartPosition = FormStartPosition.CenterScreen;
            IOConfigFrm.ShowDialog();

            return res;
        }

        public bool WriteDOLine(string linename, bool onoroff, bool wait)
        {
            bool res = false;
            IOConfig obj = ioconfig.OutputConfigs.Find(X => X.Name == linename);
            if (obj == null)
            {
                return false;
            }
            if (IOTool != null)
            {
                bool ret = IOTool.DAQ_IO_DO_SetBitStatus(obj.CardID, obj.PortNum, obj.ChannelNum, onoroff);

                if (ret == false)
                {
                    return false;
                }
                else if (wait)
                {
                    Task task = Task.Factory.StartNew(new Action(() =>
                    {
                        bool status = false;
                        while (true)
                        {
                            ret = IOTool.DAQ_IO_DO_GetBitStatus(obj.CardID, obj.PortNum, obj.ChannelNum, ref status);
                            if (onoroff == status)
                            {
                                break;
                            }
                        }
                    }));
                    Task.WaitAny(task);
                }
                res = true;

            }
            return res;
        }

        public bool ReadDOLine(string linename, ref bool onoroff)
        {
            bool res = false;
            IOConfig obj = ioconfig.OutputConfigs.Find(X => X.Name == linename);
            if (obj == null)
            {
                return false;
            }
            if (IOTool != null)
            {
                bool status = false;
                bool ret = IOTool.DAQ_IO_DO_GetBitStatus(obj.CardID, obj.PortNum, obj.ChannelNum, ref status);
                if (ret == false)
                {
                    return false;
                }
                else
                {
                    onoroff = status;
                }
                res = true;
            }
            return res;
        }

        public bool ReadDILine(string linename, ref bool onoroff)
        {
            bool res = false;
            IOConfig obj = ioconfig.InputConfigs.Find(X => X.Name == linename);
            if (obj == null)
            {
                return false;
            }
            if (IOTool != null)
            {
                bool status = false;
                bool ret = IOTool.DAQ_IO_DI_GetBitStatus(obj.CardID, obj.PortNum, obj.ChannelNum, ref status);
                if (ret == false)
                {
                    return false;
                }
                else
                {
                    onoroff = status;
                }
                res = true;
            }
            return res;
        }

        public bool ReadDILineHold(string linename, bool onoroff, ref bool success, int onofftime)
        {
            /// <summary>
            /// 判断某个IO信号是否一直处于某种状态
            /// </summary>
            /// <param name="linename">信号名称</param>
            /// <param name="onoroff">保持的状态</param>
            /// <param name="success">是否保持了onofftime的时间，true保持，false未保持 </param>
            /// <param name="onofftime">保持的时间</param>
            /// <returns>返回是否成功，保持了true，未保持false</returns>
            /// 
            bool res = false;

            IOConfig obj = ioconfig.InputConfigs.Find(X => X.Name == linename);
            if (obj == null)
            {
                return false;
            }
            if (IOTool != null)
            {
                bool status = false;
                Stopwatch sw = new Stopwatch();
                sw.Reset();
                sw.Start();
                res = true;

                while (true)
                {
                    bool ret = IOTool.DAQ_IO_DI_GetBitStatus(obj.CardID, obj.PortNum, obj.ChannelNum, ref status);
                    if (ret == false)
                    {
                        sw.Stop();
                        res = false;
                        success = false;
                        break;
                    }
                    else if (status != onoroff)
                    {
                        sw.Stop();
                        res = true;
                        success = false;
                        break;
                    }
                    else if (sw.ElapsedMilliseconds / 1000 >= onofftime)
                    {
                        sw.Stop();
                        res = true;
                        success = true;
                        break;
                    }
                    Delay(300);
                }
                sw.Stop();
            }
            return res;
        }

        public bool WaitDIOnOff(string linename, bool onoff, int timeout = 10)
        {
            bool res = false;
            IOConfig obj = ioconfig.InputConfigs.Find(X => X.Name == linename);
            if (obj == null)
            {
                return false;
            }
            if (IOTool != null)
            {
                bool status = false;
                Stopwatch sw = new Stopwatch();
                sw.Reset();
                sw.Start();
                while (true)
                {
                    bool ret = IOTool.DAQ_IO_DI_GetBitStatus(obj.CardID, obj.PortNum, obj.ChannelNum, ref status);
                    if (ret == false)
                    {
                        res = false;
                        break;
                    }
                    if (status == onoff)
                    {
                        res = true;
                        break;
                    }
                    if (sw.ElapsedMilliseconds / 1000 >= timeout)
                    {
                        res = false;
                        break;
                    }
                    Delay(100);
                }
                sw.Stop();
            }

            return res;
        }

        #endregion

        #region IO卡底层功能应用函数

        public bool BoxGripperUpDown(bool Down, bool Wait)
        {
            //Box夹爪气缸控制
            bool res = false;
            string sensorName = Down ? "BoxGripperDownSensor" : "BoxGripperUpSensor";

            res = WriteDOLine("BoxGripperUp", !Down, Wait);
            Delay(100);
            if (res == true)
            { 
                res = WriteDOLine("BoxGripperDown", Down, Wait);
                Delay(100);
            }
            if ((res == true) && (Wait == true))
            {
                res = GlobalFunction.IOControlTools.WaitDIOnOff(sensorName, true, 20);
            }
            return res;
        }

        public bool EpoxyDipUpDown(bool Down, bool Wait)
        {
            //胶针气缸控制
            bool res = false;
            string sensorName = Down ? "EpoxyDipDownSensor" : "EpoxyDipUpSensor";

            res = WriteDOLine("EpoxyDipUp", !Down, Wait);
            Delay(100);
            if (res == true)
            {
                res = WriteDOLine("EpoxyDipDown", Down, Wait);
                Delay(100);
            }
            if ((res == true) && (Wait == true))
            {
                res = GlobalFunction.IOControlTools.WaitDIOnOff(sensorName, true, 20);
            }
            return res;
        }
        
        public bool UVInOut(bool Out, bool Wait)
        {
            //UV气缸控制
            bool res = false;
            string sensorName = Out ? "UVOutSensor" : "UVInSensor";

            res = WriteDOLine("UVIn", !Out, Wait);
            Delay(100);
            if (res == true)
            {
                res = WriteDOLine("UVOut", Out, Wait);
                Delay(100);
            }
            if ((res == true) && (Wait == true))
            {
                res = GlobalFunction.IOControlTools.WaitDIOnOff(sensorName, true, 20);
            }
            return res;
        }
        
        public bool NestPinOnOff(bool On, bool Wait)
        {
            //Nest侧顶气缸控制
            bool res = false;

            res = WriteDOLine("NestPinClampOff", !On, Wait);
            Delay(100);
            if (res == true)
            {
                res = WriteDOLine("NestPinClampOn", On, Wait);
                Delay(100);
            }
            return res;
        }

        public bool NestPullOnOff(bool On, bool Wait)
        {
            //Nest抱爪气缸控制
            bool res = false;
            string sensorName = On ? "NestPullOnSensor" : "NestPullOffSensor";

            res = WriteDOLine("NestPullOff", !On, Wait);
            Delay(100);
            if (res == true)
            {
                res = WriteDOLine("NestPullOn", On, Wait);
                Delay(100);
            }
            if ((res == true) && (Wait == true))
            {
                res = GlobalFunction.IOControlTools.WaitDIOnOff(sensorName, true, 10);
            }
            return res;
        }
                
        public bool PogoPinUpDown(bool Down, bool Wait)
        {
            //PogoPin向下运动气缸控制
            bool res = false;
            string sensorName = Down ? "PogoPinDownSensor" : "PogoPinUpSensor";

            res = WriteDOLine("PogoPinUp", !Down, Wait);
            Delay(100);
            if (res == true)
            {
                res = WriteDOLine("PogoPinDown", Down, Wait);
                Delay(100);
            }
            if ((res == true) && (Wait == true))
            {
                res = GlobalFunction.IOControlTools.WaitDIOnOff(sensorName, true, 10);
            }
            return res;
        }

        public bool PogoPinInOut(bool Out, bool Wait)
        {
            //PogoPin前后运动气缸控制
            bool res = false;
            string sensorName = Out ? "PogoPinOutSensor" : "PogoPinInSensor";

            res = WriteDOLine("PogoPinIn", !Out, Wait);
            Delay(100);
            if (res == true)
            {
                res = WriteDOLine("PogoPinOut", Out, Wait);
                Delay(100);
            }
            if ((res == true) && (Wait == true))
            {
                res = GlobalFunction.IOControlTools.WaitDIOnOff(sensorName, true, 10);
            }
            return res;
        }

        public bool PogoPinClampOnOff(bool On, bool Wait)
        {
            //PogoPin合紧气缸控制
            bool res = false;

            res = WriteDOLine("PogoPinClamp", On, Wait);
            Delay(100);
            return res;
        }

        public bool NestVacuumOnOff(bool On, bool Wait)
        {
            //Nest真空发生器控制
            bool res = false;

            res = WriteDOLine("NestVacuum", On, Wait);
            Delay(200);
            if ((res == true) && (Wait == true))
            {
                res = GlobalFunction.IOControlTools.WaitDIOnOff("NestVacuumSensor", On, 10);              
            }
            return res;
        }

        public bool EpoxyWipeOpenClose(bool Close, bool Wait)
        {
            //擦胶气缸控制
            bool res = false;

            res = WriteDOLine("WipeEpoxy", Close, Wait);
            Delay(100);
            return res;
        }

        public bool EpoxyOnOff(bool On, bool Wait)
        {
            //点胶机IO控制
            bool res = false;

            res = WriteDOLine("EpoxyOn", On, Wait);
            Delay(100);
            return res;
        }

        public bool SafetyDoorUpDown(bool Up, bool Wait)
        {
            //防护门上下运动控制
            bool res = false;

            res = WriteDOLine("SafetyDoor", Up, Wait);
            Delay(100);
            return res;
        }

        public bool NestClampBox(bool OnOff)
        {
            //锁紧或者释放Nest中金属壳体类产品盒子
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.IOCard_InitialStatus == true)
            {
                if (OnOff == true)
                {
                    //抱爪第一次抱紧
                    res = GlobalFunction.IOControlTools.NestPullOnOff(true, true);
                    Delay(200);
                    if (res == true)
                    {
                        //抱爪松开
                        res = GlobalFunction.IOControlTools.NestPullOnOff(false, true);
                        Delay(200);
                        if (res == true)
                        {
                            //侧PIN顶紧
                            res = GlobalFunction.IOControlTools.NestPinOnOff(true, true);
                            Delay(200);
                            if (res == true)
                            {
                                //抱爪第二次抱紧
                                res = GlobalFunction.IOControlTools.NestPullOnOff(true, true);
                                Delay(200);
                                if (res == true)
                                {
                                    //打开Nest真空发生器
                                    res = GlobalFunction.IOControlTools.NestVacuumOnOff(true, true);
                                    Delay(200);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //侧PIN松开
                    res = GlobalFunction.IOControlTools.NestPinOnOff(false, true);
                    Delay(200);
                    if (res == true)
                    {
                        //抱爪松开
                        res = GlobalFunction.IOControlTools.NestPullOnOff(false, true);
                        Delay(200);
                        if (res == true)
                        {
                            //关闭Nest真空
                            res = GlobalFunction.IOControlTools.NestVacuumOnOff(false, true);
                            Delay(200);
                        }
                    }
                }
            }
            else
            {
                if (OnOff == true)
                {
                    MessageBox.Show("IO卡初始化未成功!", "锁紧Nest中产品盒子失败");
                }
                else
                {
                    MessageBox.Show("IO卡初始化未成功!", "释放Nest中产品盒子失败");
                }
                return false;
            }

            return res;
        }

        public bool NestClampPCBA(bool OnOff)
        {
            //锁紧或者释放Nest中PCBA类产品
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.IOCard_InitialStatus == true)
            {
                if (OnOff == true)
                {
                    //抱爪第一次抱紧
                    res = GlobalFunction.IOControlTools.NestPullOnOff(true, true);
                    Delay(200);
                    if (res == true)
                    {
                        //抱爪松开
                        res = GlobalFunction.IOControlTools.NestPullOnOff(false, true);
                        Delay(200);
                        if (res == true)
                        {
                            //侧PIN顶紧
                            res = GlobalFunction.IOControlTools.NestPinOnOff(true, true);
                            Delay(200);
                            if (res == true)
                            {
                                //抱爪第二次抱紧
                                res = GlobalFunction.IOControlTools.NestPullOnOff(true, true);
                                Delay(200);
                                if (res == true)
                                {
                                    //打开Nest真空发生器
                                    res = GlobalFunction.IOControlTools.NestVacuumOnOff(true, true);
                                    Delay(200);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //侧PIN松开
                    res = GlobalFunction.IOControlTools.NestPinOnOff(false, true);
                    Delay(200);
                    if (res == true)
                    {
                        //抱爪松开
                        res = GlobalFunction.IOControlTools.NestPullOnOff(false, true);
                        Delay(200);
                        if (res == true)
                        {
                            //关闭Nest真空
                            res = GlobalFunction.IOControlTools.NestVacuumOnOff(false, true);
                            Delay(200);
                        }
                    }
                }
            }
            else
            {
                if (OnOff == true)
                {
                    MessageBox.Show("IO卡初始化未成功!", "锁紧Nest中产品盒子失败");
                }
                else
                {
                    MessageBox.Show("IO卡初始化未成功!", "释放Nest中产品盒子失败");
                }
                return false;
            }

            return res;
        }

        public bool NestClampPogoPin(bool On)
        {
            //加电Pin针合上或者释放
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.IOCard_InitialStatus == true)
            {
                //获取当前产品加电Pin针作用方式
                string PogoPinPosition = GlobalParameters.productconfig.processConfig.pogopinPosition.ToString();

                if (On == true)
                {
                    switch (PogoPinPosition)
                    {
                        case "SIDE_PIN":
                            //侧面加电Pin针合上
                            res = GlobalFunction.IOControlTools.PogoPinClampOnOff(true, true);
                            Delay(200);
                            break;

                        case "TOP_PIN":
                            //顶部加电Pin针座伸出
                            res = GlobalFunction.IOControlTools.PogoPinInOut(true, true);
                            Delay(200);
                            if (res == true)
                            {
                                //顶部加电Pin针座向下运动
                                res = GlobalFunction.IOControlTools.PogoPinUpDown(true, true);
                                Delay(200);
                                if (res == true)
                                {
                                    //顶部加电Pin针合上
                                    res = GlobalFunction.IOControlTools.PogoPinClampOnOff(true, true);  
                                    Delay(200);
                                }
                            }
                            break;

                        case "SIDETOP_PIN":
                            //----此处需要加入功能代码

                            break;
                    }
                }
                else
                {
                    switch (PogoPinPosition)
                    {
                        case "SIDE_PIN":
                            //侧面加电Pin针松开
                            res = GlobalFunction.IOControlTools.PogoPinClampOnOff(false, true);
                            Delay(200);
                            break;

                        case "TOP_PIN":
                            //顶部加电Pin针松开
                            res = GlobalFunction.IOControlTools.PogoPinClampOnOff(false, true); 
                            Delay(200);
                            if (res == true)
                            {
                                //顶部加电Pin针座向上运动
                                res = GlobalFunction.IOControlTools.PogoPinUpDown(false, true);
                                Delay(200);
                                if (res == true)
                                {
                                    //顶部加电Pin针座后退
                                    res = GlobalFunction.IOControlTools.PogoPinInOut(false, true);
                                    Delay(200);
                                }
                            }

                            break;

                        case "SIDETOP_PIN":
                            //----此处需要加入功能代码

                            break;
                    } 
                }
            }
            else
            {
                if (On == true)
                {
                    MessageBox.Show("IO卡初始化未成功!", "加电Pin针合上产品盒子失败");
                }
                else
                {
                    MessageBox.Show("IO卡初始化未成功!", "加电Pin针松开产品盒子失败");
                }
            }

            return res;
        }

        public bool AllSliderSafelyMoveUp()
        {
            //强制将产品Box夹爪气缸和胶针气缸安全上抬，防止撞机事件发生
            bool res = false;
            bool sensorOn = false;

            if (GlobalParameters.HardwareInitialStatus.IOCard_InitialStatus == false)
            {
                MessageBox.Show("IO卡初始化未成功!", "产品Box夹爪和胶针安全上抬失败");
                return false;
            }
            else
            { 
                //获取产品Box夹爪气缸上位传感器当前状态
                res = GlobalFunction.IOControlTools.ReadDILine("BoxGripperUpSensor", ref sensorOn);
                if (res == true)
                {
                    //如果产品Box夹爪气缸上位传感器未感应到，将产品Box夹爪上抬
                    if (sensorOn == false)
                    {
                        res = GlobalFunction.IOControlTools.BoxGripperUpDown(false, true);
                        Delay(100);
                        if (res == false)
                        {
                            MessageBox.Show("产品Box夹爪上抬未成功!", "气缸运动出错");
                            return false;
                        }
                    }
                }
                else
                {
                    return false;
                }

                //获取胶针气缸上位传感器当前状态
                res = GlobalFunction.IOControlTools.ReadDILine("EpoxyDipUpSensor", ref sensorOn);
                if (res == true)
                {
                    //如果胶针气缸上位传感器未感应到，将胶针上抬
                    if (sensorOn == false)
                    {
                        res = GlobalFunction.IOControlTools.EpoxyDipUpDown(false, true);
                        Delay(100);
                        if (res == false)
                        {
                            MessageBox.Show("胶针上抬未成功!", "气缸运动出错");
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public bool SetSignalTowerLightStatus(Enum_SignalTowerLamp Enum_SignalTowerLamp)
        {
            //信号塔三色灯驱动
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.IOCard_InitialStatus == true)
            {
                switch (Enum_SignalTowerLamp)
                {
                    case Enum_SignalTowerLamp.GREEN:
                        res = WriteDOLine("GreenSignalLamp", true, false);
                        Delay(100);
                        res = WriteDOLine("YellowSignalLamp", false, false);
                        Delay(100);
                        res = WriteDOLine("RedSignalLamp", false, false);
                        Delay(100);
                        break;

                    case Enum_SignalTowerLamp.YELLOW:
                        res = WriteDOLine("GreenSignalLamp", false, false);
                        Delay(100);
                        res = WriteDOLine("YellowSignalLamp", true, false);
                        Delay(100);
                        res = WriteDOLine("RedSignalLamp", false, false);
                        Delay(100);
                        break;

                    case Enum_SignalTowerLamp.RED:
                        res = WriteDOLine("GreenSignalLamp", false, false);
                        Delay(100);
                        res = WriteDOLine("YellowSignalLamp", false, false);
                        Delay(100);
                        res = WriteDOLine("RedSignalLamp", true, false);
                        Delay(100);
                        break;

                    case Enum_SignalTowerLamp.AllTurnOff:
                        res = WriteDOLine("GreenSignalLamp", false, false);
                        Delay(100);
                        res = WriteDOLine("YellowSignalLamp", false, false);
                        Delay(100);
                        res = WriteDOLine("RedSignalLamp", false, false);
                        Delay(100);
                        break;
                }
            }
            else
            {
                MessageBox.Show("IO卡初始化未成功!", "信号塔指示灯驱动失败");
            }
            return res;
        }

        public bool CheckEmergencySwitchOn()
        {
            //检查急停开关是否处于闭合状态
            bool status = false;

            ReadDILine("EStopSensor", ref status);
            return status;
        }

        public bool CheckUVSafetyDoorOn()
        {
            //检查UV安全防抖门是否已关闭
            bool status = false;

            ReadDILine("SafetyDoorUpSensor", ref status);
            return status;
        }

        #endregion
    }

    #endregion
}