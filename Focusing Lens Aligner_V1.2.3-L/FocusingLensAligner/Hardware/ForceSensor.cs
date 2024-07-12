using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;

namespace FocusingLensAligner
{
    //压力传感器底层功能
    public class ForceSensorConfig
    {
        public string ForceSensorName { get; set; }
        public string Description { get; set; }
        public string SerialPort { get; set; }
        public int BaudRate { get; set; }
        public string ForceUnit { get; set; }
        public int Accuracy { get; set; }

        internal int ComPort
        {
            get
            {
                return Convert.ToInt32(SerialPort.ToUpper().Replace("COM", ""));
            }
        }

        internal int ForceUnitValue
        {
            get
            {
                return Convert.ToInt32(Enum.Parse(typeof(cForceSensorTools.Enum_ForceReadingUnit), ForceUnit));
            }
        }
    }

    [Serializable]
    [System.Xml.Serialization.XmlRoot("Force Sensor Config")]

    public class ForceSensorConfigCollection
    {
        List<ForceSensorConfig> ForceSensorController = new List<ForceSensorConfig>();

        public List<ForceSensorConfig> Parameter
        {
            get
            {
                return ForceSensorController;
            }
            set
            {
                ForceSensorController = value;
            }
        }
    }

    class cForceSensorTools
    {
        enum Enum_ErrorCode : int
        {
            NoError = 0,
            ErrorComPortOpenFailed = -1,
            ErrorComPortCloseFailed = -2,
            ErrorComPortNotOpened = -3,
            ErrorComPortWriteFailed = -4,
            ErrorComPortResponse = -5,
            ErrorDeviceIsOffline = -6
        }

        public enum Enum_R320Property :int
        {
            Unit = 1,
            Accuracy = 2,
            Capacity = 3,
            LiteralGrossWeight = 4,
            FinalGrossWeight = 5,
            LiteralNetWeight = 6,
            FinalNetWeight = 7,
            Tare = 8,
            Zero = 9
        }

        public enum Enum_ForceReadingUnit : int
        {
            none = 0,
            g = 1,
            kg = 2,
            lb = 3,
            t = 4
        }

        //建立串口实例化集合
        private static Dictionary<String, SerialPort> ForceSensor_ComDevice = new Dictionary<String, SerialPort>();

        //建立串口打开标志数组
        public static bool[] ForceSensor_ComPortOpenFlag = new bool[32];

        private ForceSensorConfigCollection ForceSensorConfig = new ForceSensorConfigCollection();

        private bool ForceSensor_ComPortOpen(int ComPort, int BaudRate, ref int retErrorCode)
        {
            retErrorCode = (int)Enum_ErrorCode.NoError;
            if (ForceSensor_ComPortOpenFlag[ComPort] == true)
            {
                return true;
            }
            SerialPort tmp = new SerialPort();
            if (tmp.IsOpen == false)
            {
                tmp.PortName = "COM" + ComPort;
                tmp.BaudRate = BaudRate;                        //波特率
                tmp.Parity = System.IO.Ports.Parity.None;       //无校验
                tmp.DataBits = 8;                               //数据位
                tmp.StopBits = System.IO.Ports.StopBits.One;    //停止位
                tmp.ReadTimeout = 5000;                         //通讯超时
                try
                {
                    tmp.Open();
                }
                catch (Exception ex)
                {
                    retErrorCode = (int)Enum_ErrorCode.ErrorComPortOpenFailed;
                    MessageBox.Show(ex.Message, "未能成功打开串口COM" + ComPort.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //添加串口实例
                ForceSensor_ComDevice.Add(tmp.PortName, tmp);
                ForceSensor_ComPortOpenFlag[ComPort] = true;
            }
            return true;
        }

        private bool ForceSensor_ComPortClose(int ComPort, ref int retErrorCode)
        {
            retErrorCode = (int)Enum_ErrorCode.NoError;
            if (ForceSensor_ComPortOpenFlag[ComPort] == true)
            {
                try
                {
                    ForceSensor_ComDevice["COM" + ComPort.ToString()].Close();
                }
                catch (Exception ex)
                {
                    retErrorCode = (int)Enum_ErrorCode.ErrorComPortCloseFailed;
                    MessageBox.Show(ex.Message, "无法关闭串口COM" + ComPort.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //清空串口实例
                ForceSensor_ComDevice.Remove("COM" + ComPort.ToString());
                ForceSensor_ComPortOpenFlag[ComPort] = false;
                return true;
            }
            retErrorCode = (int)Enum_ErrorCode.ErrorComPortNotOpened;
            return false;
        }

        private bool ForceSensor_SetValue(string ForceSensorName, Enum_R320Property Property, object Value, ref int retErrorCode)
        {
            ForceSensorConfig obj = ForceSensorConfig.Parameter.Find(X => X.ForceSensorName == ForceSensorName);
            if (obj == null)
            {
                MessageBox.Show(ForceSensorName + " can't be found in ForceSensorConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            bool OnlineStatus = false;
            if (ForceSensor_GetOnlineStatus(obj.ForceSensorName, ref OnlineStatus))
            {
                if (OnlineStatus == false)
                {
                    retErrorCode = (int)Enum_ErrorCode.ErrorDeviceIsOffline;
                    return false;
                }
            }

            retErrorCode = (int)Enum_ErrorCode.NoError;
            string strWrite = "";
            switch (Property)
            {
                case Enum_R320Property.Unit:
                    strWrite = "20120129:" + ((int)Value).ToString("X");
                    break;
                case Enum_R320Property.Accuracy:
                    strWrite = "20120128:" + ((int)Value).ToString("X");
                    break;
                case Enum_R320Property.Capacity:
                    strWrite = "20120121:" + ((int)Value).ToString("X");
                    break;
                case Enum_R320Property.Tare:
                    strWrite = "20120008:8003:";
                    break;
                case Enum_R320Property.Zero:
                    strWrite = "20120008:8002:";
                    break;
            }
            strWrite += "\n";
            try
            {
                ForceSensor_ComDevice["COM" + obj.ComPort.ToString()].Write(strWrite);
            }
            catch (Exception ex)
            {
                retErrorCode = (int)Enum_ErrorCode.ErrorComPortWriteFailed;
                MessageBox.Show(ex.Message, "无法发送数据到串口COM" + obj.ComPort.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            Thread.Sleep(50);
            return true;
        }

        private bool ForceSensor_GetValue(string ForceSensorName, Enum_R320Property Property, ref object Value, ref int retErrorCode)
        {
            ForceSensorConfig obj = ForceSensorConfig.Parameter.Find(X => X.ForceSensorName == ForceSensorName);
            if (obj == null)
            {
                MessageBox.Show(ForceSensorName + " can't be found in ForceSensorConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
                        
            bool OnlineStatus = false;
            if (ForceSensor_GetOnlineStatus(obj.ForceSensorName, ref OnlineStatus))
            {
                if(OnlineStatus == false)
                {                    
                    retErrorCode = (int)Enum_ErrorCode.ErrorDeviceIsOffline;
                    return false;
                }
            }

            retErrorCode = (int)Enum_ErrorCode.NoError;
            string strWrite = "";
            string strRead = "";
            string strCompare = "";
            string strResult = "";
            string[] strSplit = null;

            switch (Property)
            {
                case Enum_R320Property.Unit:
                    strWrite = "20110129:";
                    break;
                case Enum_R320Property.Accuracy:
                    strWrite = "20110128:";
                    break;
                case Enum_R320Property.Capacity:
                    strWrite = "20110121:";
                    break;
                case Enum_R320Property.LiteralGrossWeight:
                    strWrite = "20050026:";
                    break;
                case Enum_R320Property.LiteralNetWeight:
                    strWrite = "20050027:";
                    break;
                //case Enum_R320Property.FinalGrossWeight:
                //    strWrite = "20110026:";
                //    break;
                //case Enum_R320Property.FinalNetWeight:
                //    strWrite = "20110027:";
                //    break;
            }
            strWrite += "\n";
            try
            {
                byte[] writeBytes = null;
                ForceSensor_ComDevice["COM" + obj.ComPort.ToString()].DiscardInBuffer();
                writeBytes = Encoding.UTF8.GetBytes(strWrite);
                ForceSensor_ComDevice["COM" + obj.ComPort.ToString()].Write(writeBytes, 0, writeBytes.Length);
                Thread.Sleep(100);
                byte[] receiveData = new byte[ForceSensor_ComDevice["COM" + obj.ComPort.ToString()].BytesToRead];
                byte[] dstData = new byte[8];
                ForceSensor_ComDevice["COM" + obj.ComPort.ToString()].Read(receiveData, 0, receiveData.Length);
                if (receiveData.Length <= 0)
                {
                    return false;
                }
                strRead = Encoding.UTF8.GetString(receiveData);
                string tempStr = "";
                tempStr = Encoding.UTF8.GetString(receiveData);
                strSplit = strRead.Replace(" ", "").Split(':');
                switch (Property)
                {
                    case Enum_R320Property.Unit:
                        strCompare = "110129:";
                        strResult = strSplit[1];
                        break;
                    case Enum_R320Property.Accuracy:
                        strCompare = "110128:";
                        strResult = strSplit[1];
                        break;
                    case Enum_R320Property.Capacity:
                        strCompare = "110121:";
                        strResult = strSplit[1];
                        break;
                    case Enum_R320Property.LiteralGrossWeight:
                        strCompare = "050026:";
                        strResult = strSplit[1];
                        break;
                    case Enum_R320Property.LiteralNetWeight:
                        strCompare = "050027:";
                        strResult = strSplit[1];
                        break;
                    //case Enum_R320Property.FinalGrossWeight:
                    //    strCompare = "110026:";
                    //    strResult = strSplit[4];
                    //    break;
                    //case Enum_R320Property.FinalNetWeight:
                    //    strCompare = "110027:";
                    //    strResult = strSplit[4];
                    //    break;
                }
                if (!strRead.Contains(strCompare))
                {
                    retErrorCode = (int)Enum_ErrorCode.ErrorComPortResponse;
                    MessageBox.Show("串口COM" + obj.ComPort.ToString() + "返回数据错误", "通信错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                try
                {
                    string str = System.Text.RegularExpressions.Regex.Replace(strResult, @"^[+-]?\d*[.]?\d*", "");
                    strResult = strResult.Replace(str, "");
                    if (Property == Enum_R320Property.Capacity)
                    {
                        Value = Int32.Parse(strResult, System.Globalization.NumberStyles.HexNumber);
                    }
                    else
                    {
                        Value = double.Parse(strResult);
                    }
                }
                catch (Exception)
                {

                    return false;

                }
            }
            catch (Exception ex)
            {
                retErrorCode = (int)Enum_ErrorCode.ErrorComPortWriteFailed;
                MessageBox.Show(ex.Message, "无法发送数据到串口COM" + obj.ComPort.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool ForceSensor_SetForceUnit(string ForceSensorName, Enum_ForceReadingUnit Unit, ref int retErrorCode)
        {
            bool res = true;

            res = ForceSensor_SetValue(ForceSensorName, Enum_R320Property.Unit, Unit, ref retErrorCode);
            return res;

        }

        private bool ForceSensor_SetAccuracy(string ForceSensorName, int Accuracy, ref int retErrorCode)
        {
            if (Accuracy < 0 || Accuracy > 5)
            {
                retErrorCode = (int)Enum_ErrorCode.ErrorComPortWriteFailed;
                MessageBox.Show("压力传感器数值精度设置不正确", "数据错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            bool res = true;
            res = ForceSensor_SetValue(ForceSensorName, Enum_R320Property.Accuracy, Accuracy, ref retErrorCode);
            return res;
        }

        private bool ForceSensor_GetGrossWeight(string ForceSensorName, ref double Weight, ref int retErrorCode)
        {
            bool res = true;
            object value = new object();
            res = ForceSensor_GetValue(ForceSensorName, Enum_R320Property.LiteralGrossWeight, ref value, ref retErrorCode);
            if (res)
            {
                Weight = Convert.ToDouble(value);
            }
            else
            {
                Weight = -99999;
            }
            return res;
        }

        private bool ForceSensor_GetNetWeight(string ForceSensorName, ref double Weight, ref int retErrorCode)
        {
            bool res = true;
            object value = new object();
            res = ForceSensor_GetValue(ForceSensorName, Enum_R320Property.LiteralNetWeight, ref value, ref retErrorCode);
            if (res)
            {
                Weight = Convert.ToDouble(value);
            }
            else
            {
                Weight = -99999;
            }
            return res;
        }

        private bool ForceSensor_Tare(string ForceSensorName, ref int retErrorCode)
        {
            bool res = true;

            res = ForceSensor_SetValue(ForceSensorName, Enum_R320Property.Tare, null, ref retErrorCode);
            return res;
        }

        public bool ForceSensor_ResetZero(string ForceSensorName, ref int retErrorCode)
        {
            bool res = true;

            res = ForceSensor_SetValue(ForceSensorName, Enum_R320Property.Zero, null, ref retErrorCode);
            return res;
        }

        public bool ForceSensor_Initial(string ForceSensorName, ref int retErrorCode)
        {
            bool res = false;

            res = STD_IGeneralTool.GeneralTool.ToTryLoad<ForceSensorConfigCollection>(ref ForceSensorConfig, GeneralFunction.GetConfigFilePath("ForceSensorConfig.xml"));
            if (!res)
            {
                MessageBox.Show("Load Force Sensor Config Fail!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            ForceSensorConfig obj = ForceSensorConfig.Parameter.Find(X => X.ForceSensorName == ForceSensorName);
            if (obj == null)
            {
                MessageBox.Show(ForceSensorName + " can't be found in ForceSensorConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            res = ForceSensor_ComPortOpen(obj.ComPort, obj.BaudRate, ref retErrorCode);
            if (res == true)
            {
                res = ForceSensor_ResetZero(ForceSensorName, ref retErrorCode);
                if (res == true)
                {
                    res = ForceSensor_SetValue(ForceSensorName, Enum_R320Property.Unit, obj.ForceUnitValue, ref retErrorCode);
                    if (res == true)
                    {
                        res = ForceSensor_SetValue(ForceSensorName, Enum_R320Property.Accuracy, obj.Accuracy, ref retErrorCode);
                    }
                }
            }
            return res;
        }

        public bool ForceSensor_UnInitial(string ForceSensorName, ref int retErrorCode)
        {
            bool res = false;

            ForceSensorConfig obj = ForceSensorConfig.Parameter.Find(X => X.ForceSensorName == ForceSensorName);
            if (obj == null)
            {
                MessageBox.Show(ForceSensorName + " can't be found in ForceSensorConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            res = ForceSensor_ComPortClose(obj.ComPort, ref retErrorCode);
            return res;
        }

        public bool ForceSensor_OpenSetupPanel()
        {
            bool res = true;

            ForceSensorConfigForm ForceSensorConfigFrm = new ForceSensorConfigForm();
            ForceSensorConfigFrm.TopLevel = true;
            ForceSensorConfigFrm.StartPosition = FormStartPosition.CenterScreen;
            ForceSensorConfigFrm.ShowDialog();
            return res;
        }

        public bool ForceSensor_GetPressureValue(string ForceSensorName, ref double PressureValue, ref int retErrorCode)
        {
            bool res = false;

            ForceSensorConfig obj = ForceSensorConfig.Parameter.Find(X => X.ForceSensorName == ForceSensorName);
            if (obj == null)
            {
                MessageBox.Show(ForceSensorName + " can't be found in ForceSensorConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            res = ForceSensor_GetNetWeight(ForceSensorName, ref PressureValue, ref retErrorCode);
            return res;
        }

        public bool ForceSensor_GetOnlineStatus(string ForceSensorName, ref bool OnlineStatus)
        { 
            ForceSensorConfig obj = ForceSensorConfig.Parameter.Find(X => X.ForceSensorName == ForceSensorName);
            if (obj == null)
            {
                MessageBox.Show(ForceSensorName + " can't be found in ForceSensorConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            OnlineStatus = ForceSensor_ComPortOpenFlag[obj.ComPort];
            return true;
        }

        public void ForceSensor_ReturnErrorMessage(int retErrorCode, ref string ErrorMessage)
        {
            ErrorMessage = Enum.GetName(typeof(Enum_ErrorCode), retErrorCode);
        }
    }
}