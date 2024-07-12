using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SMC_LATCA_Controller;

namespace FocusingLensAligner
{
    //SMC_LATCA控制器（CardMotion卡片电机轴）底层功能
    public class SMCCardMotionConfig
    {
        public string AxisName { get; set; }
        public string Description { get; set; }
        public string SerialPort { get; set; }
        internal int ComPort
        {
            get
            {
                int Port = Convert.ToInt32(SerialPort.ToUpper().Replace("COM", ""));
                return Port;
            }
        }
    }

    [Serializable]
    [System.Xml.Serialization.XmlRoot("SMC LATCA Controller Config")]

    public class SMCCardMotionConfigCollection
    {
        List<SMCCardMotionConfig> SMCCardMotionConfig = new List<SMCCardMotionConfig>();

        public List<SMCCardMotionConfig> Parameter
        {
            get
            {
                return SMCCardMotionConfig;
            }
            set
            {
                SMCCardMotionConfig = value;
            }
        }
    }

    class cSMCCardMotionTools
    {
        private SMCCardMotionConfigCollection SMCCardMotionConfig = new SMCCardMotionConfigCollection();

        private static object LockHandle_SMC_LATCA_Controller = new object();

        public bool SMCCardMotion_Initial(string AxisName, ref int retErrorCode)
        {
            bool res = false;

            //调取配置文件
            res = STD_IGeneralTool.GeneralTool.ToTryLoad<SMCCardMotionConfigCollection>(ref SMCCardMotionConfig, GeneralFunction.GetConfigFilePath("SMCCardMotionConfig.xml"));
            if (!res)
            {
                MessageBox.Show("Load SMC Card Motion Config Fail!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //轴信息匹配核查
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                //打开轴控制器通信端口
                if (cSMC_LATCA_CardMotion.ComPortOpen(obj.ComPort, ref retErrorCode) == true)
                {
                    //设置在线通信工作模式
                    if (cSMC_LATCA_CardMotion.SetOnlineCommunicationMode(obj.ComPort, ref retErrorCode) == true)
                    {
                        //清除控制器内部报警
                        if (cSMC_LATCA_CardMotion.ClearAlarmSignal(obj.ComPort, ref retErrorCode) == true)
                        {
                            //轴ServoOn
                            res = cSMC_LATCA_CardMotion.ServoOn(obj.ComPort, true, ref retErrorCode);
                            return res;
                        }
                    }
                }

                return false;
            }
        }

        public bool SMCCardMotion_UnInitial(string AxisName, ref int retErrorCode)
        {
            bool res = false;

            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                //轴ServoOff
                if (cSMC_LATCA_CardMotion.ServoOn(obj.ComPort, false, ref retErrorCode) == true)
                {
                    //关闭轴控制器通信端口
                    res = cSMC_LATCA_CardMotion.ComPortClose(obj.ComPort, ref retErrorCode);
                }
                return res;
            }
        }

        public bool SMCCardMotion_OpenSetupPanel()
        {
            SMCCardMotionConfigForm SMCCardMotionConfigFrm = new SMCCardMotionConfigForm();
            SMCCardMotionConfigFrm.TopLevel = true;
            SMCCardMotionConfigFrm.StartPosition = FormStartPosition.CenterScreen;
            SMCCardMotionConfigFrm.ShowDialog();
            return true;
        }

        public bool SMCCardMotion_ComPortOpen(string AxisName, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.ComPortOpen(obj.ComPort, ref retErrorCode);
                return res;
            }
        }

        public bool SMCCardMotion_ComPortClose(string AxisName, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.ComPortClose(obj.ComPort, ref retErrorCode);
                return res;
            }
        }

        public bool SMCCardMotion_SetOnlineCommunicationMode(string AxisName, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.SetOnlineCommunicationMode(obj.ComPort, ref retErrorCode);
                return res;
            }
        }

        public bool SMCCardMotion_IsOnline(string AxisName, ref bool Online)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            Online = cSMC_LATCA_CardMotion.DeviceOnLineFlag[obj.ComPort + 1];
            return true;
        }

        public bool SMCCardMotion_IsServoOn(string AxisName, ref bool ServoOnStatus, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.CheckIsServoOn(obj.ComPort, ref ServoOnStatus, ref retErrorCode);
                return res;
            }
        }

        public bool SMCCardMotion_IsHomed(string AxisName, ref bool Homed, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.CheckIsHomed(obj.ComPort, ref Homed, ref retErrorCode);
                return res;
            }
        }

        public bool SMCCardMotion_IsBusy(string AxisName, ref bool Busy, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.CheckIsBusy(obj.ComPort, ref Busy, ref retErrorCode);
                return res;
            }
        }

        public bool SMCCardMotion_IsMoveCompleted(string AxisName, ref bool Completed, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.CheckIsMoveCompleted(obj.ComPort, ref Completed, ref retErrorCode);
                return res;
            }
        }

        public bool SMCCardMotion_ClearAlarmSignal(string AxisName, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.ClearAlarmSignal(obj.ComPort, ref retErrorCode);
                return res;
            }
        }

        public bool SMCCardMotion_ServoOn(string AxisName, bool ServoOn, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.ServoOn(obj.ComPort, ServoOn, ref retErrorCode);
                return res;
            }
        }

        public bool SMCCardMotion_GetPosition(string AxisName, ref double Position, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.GetPosition(obj.ComPort, ref Position, ref retErrorCode);
                return res;
            }
        }

        public bool SMCCardMotion_HomeMove(string AxisName, bool Wait, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.HomeMove(obj.ComPort, ref retErrorCode);
                if (Wait == true)
                {
                    bool Completed = false;
                    do
                    {
                        System.Threading.Thread.Sleep(200);
                        res = cSMC_LATCA_CardMotion.CheckIsMoveCompleted(obj.ComPort, ref Completed, ref retErrorCode);
                        if (res == false)
                        {
                            break;
                        }
                    } while (Completed == false);
                }

                return res;
            }
        }

        public bool SMCCardMotion_MoveDistance(string AxisName, double Distance, bool Wait, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.MoveDistance(obj.ComPort, Distance, ref retErrorCode);
                if (Wait == true)
                {
                    bool Completed = false;
                    do
                    {
                        System.Threading.Thread.Sleep(200);
                        res = cSMC_LATCA_CardMotion.CheckIsMoveCompleted(obj.ComPort, ref Completed, ref retErrorCode);
                        if (res == false)
                        {
                            break;
                        }
                    } while (Completed == false);
                }

                return res;
            }
        }

        public bool SMCCardMotion_MoveToLocation(string AxisName, double Location, bool Wait, ref int retErrorCode)
        {
            SMCCardMotionConfig obj = SMCCardMotionConfig.Parameter.Find(X => X.AxisName == AxisName);
            if (obj == null)
            {
                MessageBox.Show(AxisName + " can't be found in SMCCardMotionConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            lock (LockHandle_SMC_LATCA_Controller)
            {
                bool res = cSMC_LATCA_CardMotion.MoveToLocation(obj.ComPort, Location, ref retErrorCode);
                if (Wait == true)
                {
                    bool Completed = false;
                    do
                    {
                        System.Threading.Thread.Sleep(200);
                        res = cSMC_LATCA_CardMotion.CheckIsMoveCompleted(obj.ComPort, ref Completed, ref retErrorCode);
                        if (res == false)
                        {
                            break;
                        }
                    } while (Completed == false);
                }

                return res;
            }
        }

        public void SMCCardMotion_ReturnErrorMessage(int retErrorCode, ref string ErrorMessage)
        {
            ErrorMessage = Enum.GetName(typeof(cSMC_LATCA_CardMotion.eErrorCode), retErrorCode);
        }
    }
}