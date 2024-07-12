using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;

namespace FocusingLensAligner
{
    //二维码扫码器底层功能

    public class QRCodeScannerConfig
    {
        public string CodeScannerName { get; set; }
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
    [System.Xml.Serialization.XmlRoot("QR Code Scanner Config")]

    public class QRCodeScannerConfigCollection
    {
        List<QRCodeScannerConfig> QRCodeScannerConfig = new List<QRCodeScannerConfig>();

        public List<QRCodeScannerConfig> Parameter
        {
            get
            {
                return QRCodeScannerConfig;
            }
            set
            {
                QRCodeScannerConfig = value;
            }
        }
    }

    public class cQRCodeScannerTools
    {
        private const uint SetCommDelayTime = 500;  //单位（毫秒mS）

        [DllImport("kernel32.dll")]
        static extern uint GetTickCount();

        enum Enum_ErrorCode : int
        {
            NoError = 0,
            ErrorComPortOpenFailed = -1,
            ErrorComPortCloseFailed = -2,
            ErrorComPortNotOpened = -3,
            ErrorComPortWriteFailed = -4,
            ErrorComPortResponse = -5
        }

        //建立串口实例化集合
        private static Dictionary<String, SerialPort> QRCodeScanner_ComDevice = new Dictionary<String, SerialPort>();

        //建立串口打开标志数组
        public static bool[] QRCodeScanner_ComPortOpenFlag = new bool[32];

        private QRCodeScannerConfigCollection QRCodeScannerConfig = new QRCodeScannerConfigCollection();

        public bool QRCodeScanner_ComPortOpen(string CodeScannerName, ref int retErrorCode)
        {
            QRCodeScannerConfig obj = QRCodeScannerConfig.Parameter.Find(X => X.CodeScannerName == CodeScannerName);
            if (obj == null)
            {
                MessageBox.Show(CodeScannerName + " can't be found in QRCodeScannerConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            retErrorCode = (int)Enum_ErrorCode.NoError;
            if (QRCodeScanner_ComPortOpenFlag[obj.ComPort] == true)
            {
                return true;
            }
            SerialPort tmp = new SerialPort();
            if (tmp.IsOpen == false)
            {
                tmp.PortName = obj.SerialPort;
                tmp.BaudRate = 115200;                          //波特率
                tmp.Parity = System.IO.Ports.Parity.None;       //无校验
                tmp.DataBits = 8;                               //数据位
                tmp.StopBits = System.IO.Ports.StopBits.One;    //停止位
                tmp.Handshake = Handshake.None;                 //无流控
                tmp.ReadTimeout = 5000;                         //通讯超时
                try
                {
                    tmp.Open();
                }
                catch (Exception ex)
                {
                    retErrorCode = (int)Enum_ErrorCode.ErrorComPortOpenFailed;
                    MessageBox.Show(ex.Message, "未能成功打开串口COM" + obj.ComPort.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //添加串口实例
                QRCodeScanner_ComDevice.Add(obj.SerialPort, tmp);
                QRCodeScanner_ComPortOpenFlag[obj.ComPort] = true;
            }
            return true;
        }

        public bool QRCodeScanner_ComPortClose(string CodeScannerName, ref int retErrorCode)
        {
            QRCodeScannerConfig obj = QRCodeScannerConfig.Parameter.Find(X => X.CodeScannerName == CodeScannerName);
            if (obj == null)
            {
                MessageBox.Show(CodeScannerName + " can't be found in QRCodeScannerConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            retErrorCode = (int)Enum_ErrorCode.NoError;
            if (QRCodeScanner_ComPortOpenFlag[obj.ComPort] == true)
            {
                try
                {
                    QRCodeScanner_ComDevice[obj.SerialPort].Close();
                }
                catch (Exception ex)
                {
                    retErrorCode = (int)Enum_ErrorCode.ErrorComPortCloseFailed;
                    MessageBox.Show(ex.Message, "无法关闭串口COM" + obj.ComPort.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                //清空串口实例
                QRCodeScanner_ComDevice.Remove(obj.SerialPort);
                QRCodeScanner_ComPortOpenFlag[obj.ComPort] = false;
                return true;
            }
            retErrorCode = (int)Enum_ErrorCode.ErrorComPortNotOpened;
            return false;
        }

        public bool QRCodeScanner_Initial(string CodeScannerName, ref int retErrorCode)
        {
            bool res = false;

            res = STD_IGeneralTool.GeneralTool.ToTryLoad<QRCodeScannerConfigCollection>(ref QRCodeScannerConfig, GeneralFunction.GetConfigFilePath("QRCodeScannerConfig.xml"));
            if (!res)
            {
                MessageBox.Show("Load QR Code Scanner Config Fail!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            QRCodeScannerConfig obj = QRCodeScannerConfig.Parameter.Find(X => X.CodeScannerName == CodeScannerName);
            if (obj == null)
            {
                MessageBox.Show(CodeScannerName + " can't be found in QRCodeScannerConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            retErrorCode = (int)Enum_ErrorCode.NoError;
            res = QRCodeScanner_ComPortOpen(obj.CodeScannerName, ref retErrorCode);
            return res;
        }

        public bool QRCodeScanner_UnInitial(string CodeScannerName, ref int retErrorCode)
        {
            QRCodeScannerConfig obj = QRCodeScannerConfig.Parameter.Find(X => X.CodeScannerName == CodeScannerName);
            if (obj == null)
            {
                MessageBox.Show(CodeScannerName + " can't be found in QRCodeScannerConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            retErrorCode = (int)Enum_ErrorCode.NoError;
            bool res = QRCodeScanner_ComPortClose(obj.CodeScannerName, ref retErrorCode);
            return res;
        }

        public bool QRCodeScanner_OpenSetupPanel()
        {
            bool res = true;

            QRCodeScannerConfigForm QRCodeScannerConfigFrm = new QRCodeScannerConfigForm();
            QRCodeScannerConfigFrm.TopLevel = true;
            QRCodeScannerConfigFrm.StartPosition = FormStartPosition.CenterScreen;
            QRCodeScannerConfigFrm.ShowDialog();
            return res;
        }

        private void QRCodeScanner_ComPortClearReceiveBuffer(int ComPort)
        {
            if (QRCodeScanner_ComPortOpenFlag[ComPort] == true)
            {
                QRCodeScanner_ComDevice["COM" + ComPort.ToString()].DiscardInBuffer();
            }
        }

        private void QRCodeScanner_ComPortClearSendBuffer(int ComPort)
        {
            if (QRCodeScanner_ComPortOpenFlag[ComPort] == true)
            {
                QRCodeScanner_ComDevice["COM" + ComPort.ToString()].DiscardOutBuffer();
            }
        }

        private int QRCodeScanner_GetNumberOfBytesReceived(int ComPort)
        {
            if (QRCodeScanner_ComPortOpenFlag[ComPort] == true)
            {
                return QRCodeScanner_ComDevice["COM" + ComPort.ToString()].BytesToRead;
            }
            return 0;
        }

        private bool QRCodeScanner_ComPortReadReceiveBuffer(int ComPort, ref byte[] ReceivedBytes, ref int retErrorCode)
        {
            if (QRCodeScanner_ComPortOpenFlag[ComPort] == true)
            {
                retErrorCode = (int)Enum_ErrorCode.NoError;
                QRCodeScanner_ComDevice["COM" + ComPort.ToString()].Read(ReceivedBytes, 0, ReceivedBytes.Length);
                return true;
            }
            retErrorCode = (int)Enum_ErrorCode.ErrorComPortNotOpened;
            return false;
        }

        private bool QRCodeScanner_ComPortSendCommand(int ComPort, byte[] CommandBytes, ref int retErrorCode)
        {
            if (QRCodeScanner_ComPortOpenFlag[ComPort] == true)
            {
                retErrorCode = (int)Enum_ErrorCode.NoError;
                QRCodeScanner_ComDevice["COM" + ComPort.ToString()].Write(CommandBytes, 0, CommandBytes.Length);
                return true;
            }
            retErrorCode = (int)Enum_ErrorCode.ErrorComPortNotOpened;
            return false;
        }

        private void QRCodeScanner_ComPortTimeDelay(uint DelayTime)
        {
            //----串口通信精确延时（ms毫秒）

            uint starttime = GetTickCount();

            while ((GetTickCount() - starttime) < DelayTime) ;
        }

        public bool QRCodeScanner_ScanCode(string CodeScannerName, ref string CodeString, ref int retErrorCode)
        {
            CodeString = string.Empty;
            QRCodeScannerConfig obj = QRCodeScannerConfig.Parameter.Find(X => X.CodeScannerName == CodeScannerName);
            if (obj == null)
            {
                MessageBox.Show(CodeScannerName + " can't be found in QRCodeScannerConfig.xml File!", "Load Config Fail", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (QRCodeScanner_ComPortOpenFlag[obj.ComPort] == false)
            {
                retErrorCode = (int)Enum_ErrorCode.ErrorComPortNotOpened;
                return false;
            }

            //清空串口发送和接收缓冲区
            QRCodeScanner_ComPortClearReceiveBuffer(obj.ComPort);
            QRCodeScanner_ComPortClearSendBuffer(obj.ComPort);

            //扫码命令码字符串
            byte[] CommandBytes = new byte[1];
            CommandBytes[0] = 0x2B;     //字符‘+’

            bool res = false;
            retErrorCode = (int)Enum_ErrorCode.NoError;

            //发送命令
            res = QRCodeScanner_ComPortSendCommand(obj.ComPort, CommandBytes, ref retErrorCode);
            if (res == false)
            {
                return false;
            }

            //延时等待扫描器扫码结束
            QRCodeScanner_ComPortTimeDelay(SetCommDelayTime);

            //查询接收缓冲区中的字符数量
            int length = QRCodeScanner_GetNumberOfBytesReceived(obj.ComPort);

            if (length == 0)
            {
                //未扫到码
                CodeString = string.Empty;
                return true;
            }

            //读取接收缓冲区中的响应信息
            byte[] ResponseBytes = new byte[length];
            res = QRCodeScanner_ComPortReadReceiveBuffer(obj.ComPort, ref ResponseBytes, ref retErrorCode);
            if (res == false)
            {
                return false;
            }

            //清空串口发送和接收缓冲区
            QRCodeScanner_ComPortClearReceiveBuffer(obj.ComPort);
            QRCodeScanner_ComPortClearSendBuffer(obj.ComPort);

            //检查串口返回信息是否正确
            if ((ResponseBytes[length - 2] == 0x0D) && (ResponseBytes[length - 1] == 0x0A))
            {
                //返回信息完整正确，提取扫到的码信息
                string ResponseString = System.Text.Encoding.ASCII.GetString(ResponseBytes);
                CodeString = ResponseString.Substring(0, length - 2);
                return true;
            }

            retErrorCode = (int)Enum_ErrorCode.ErrorComPortResponse;
            return false;
        }

        public void QRCodeScanner_ReturnErrorMessage(int retErrorCode, ref string ErrorMessage)
        {
            ErrorMessage = Enum.GetName(typeof(cQRCodeScannerTools.Enum_ErrorCode), retErrorCode);
        }
    }
}