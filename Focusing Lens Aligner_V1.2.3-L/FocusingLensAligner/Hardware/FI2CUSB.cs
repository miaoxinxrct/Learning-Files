using System.Linq;
using STD_IFI2CFunction;

namespace FocusingLensAligner
{
    //FI2CUSB设备底层功能
    class cFI2CUSBTools
    {
        public cFI2CTool cfi2ctool = new cFI2CTool();
        byte fi2cAddr = 0xA0;   //FI2CUSB设备访问地址

        public bool FI2CUSB_Initial()
        {
            //初始化FIC2USB设备
            bool res = false;

            int resnum = cfi2ctool.FI2CInitial();
            if (resnum == 0)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }

        public bool FI2CUSB_Close()
        {
            //关闭FIC2USB设备
            bool res = true;

            cfi2ctool.DisposedTools();
            return res;
        }

        public bool FI2CUSB_OpenSetForm()
        {
            //打开FIC2USB设备配置面板
            bool res = true;

            cfi2ctool.FI2CSettingForm();
            return res;
        }

        public bool FI2CUSB_ReadProductSN(string ProductType, ref string SN)
        {
            //读取产品SN信息
            bool res = false;

            switch (ProductType)
            {
                case "GIII_QSFP28_TOSA":
                    res = FI2CUSB_QSFP28G_Gen3ReadSN(ref SN);
                    break;

                case "QSFP56_TOSA":
                    res = FI2CUSB_QSFP56ReadSN(ref SN);
                    break;

                case "OSFP2X200G_TOSA":
                    res = FI2CUSB_OSFP2X200GReadSN(ref SN);
                    break;
            }
            return res;
        }

        public bool FI2CUSB_SetLaserDAC(string ProductType, int Channel, int DAC)
        {
            //设置产品指定通道加载的电流强度DAC值（注：如果DAC=0，则LaserOff）
            bool res = false;

            switch (ProductType)
            {
                case "QSFP28G_TOSA":
                    res = FI2CUSB_QSFP28GSetDAC(Channel, DAC);
                    break;

                case "CFP4_TOSA":
                    res = FI2CUSB_CFP4SetDAC(Channel, DAC);
                    break;

                case "GIII_QSFP28_TOSA":
                    if (DAC != 0)
                    {
                        res = FI2CUSB_QSFP28G_Gen3SetLaserOn(Channel, true);
                    }
                    else
                    {
                        res = FI2CUSB_QSFP28G_Gen3SetLaserOn(Channel, false);
                    }
                    break;

                case "QSFP56_TOSA":
                    res = FI2CUSB_QSFP56SetDAC(Channel, DAC);
                    break;

                case "OSFP2X200G_TOSA":
                    res = FI2CUSB_OSFP2X200GSetDAC(Channel, DAC);
                    break;
            }
            return res;
        }

        public bool FI2CUSB_AllChannelLaserOff(string ProductType)
        {
            bool res = false;

            switch (ProductType)
            {
                case "QSFP28G_TOSA":
                    res = FI2CUSB_QSFP28GClearInternal();
                    break;

                case "CFP4_TOSA":
                    res = FI2CUSB_CFP4ClearInternal();
                    break;

                case "GIII_QSFP28_TOSA":
                    res = FI2CUSB_QSFP28G_Gen3ClearInternal();
                    break;

                case "QSFP56_TOSA":
                    res = FI2CUSB_QSFP56ClearInternal();
                    break;

                case "OSFP2X200G_TOSA":
                    res = FI2CUSB_OSFP2X200GClearInternal();
                    break;
            }
            return res;
        }

        #region QSFP28G
        private bool FI2CUSB_QSFP28GSetDAC(int Channel, int DAC)
        {
            //设置QSFP28G产品指定通道加载的电流强度DAC值（注：如果DAC=0，则LaserOff）
            bool res = false;
            string fi2cName = "FI2CUSB";
            int delay = 200;

            res = cfi2ctool[fi2cName].QSFP28_SetDAC((byte)Channel, (byte)DAC);
            GeneralFunction.Delay(delay);
            return res;
        }

        private bool FI2CUSB_QSFP28GClearInternal()
        {
            //控制QSFP28G产品所有通道LaserOff
            bool res = false;

            for (byte i = 0; i < 4; i++)
            {
                res = FI2CUSB_QSFP28GSetDAC(i, 0);
            }
            return res;
        }
        #endregion

        #region CFP4
        private bool FI2CUSB_CFP4SetDAC(int Channel, int DAC)
        {
            //设置CFP4产品指定通道加载的电流强度DAC值（注：如果DAC=0，则LaserOff）
            bool res = false;
            string fi2cName = "FI2CUSB";
            int delay = 200;

            res = cfi2ctool[fi2cName].CFP4_SetDAC((byte)Channel, (byte)DAC);
            GeneralFunction.Delay(delay);
            return res;

        }

        private bool FI2CUSB_CFP4ClearInternal()
        {
            //控制100G_CFP4产品所有通道LaserOff
            bool res = false;

            for (byte i = 0; i < 4; i++)
            {
                res = FI2CUSB_CFP4SetDAC(i, 0);
            }
            return res;
        }
        #endregion

        #region GIII-QSFP28
        private bool FI2CUSB_QSFP28G_Gen3SetLaserOn(int Channel, bool On)
        {
            //设置QSFP28G_Gen3产品指定通道LaserOn/Off
            bool res = false;
            string fi2cName = "FI2CUSB";
            byte[] WriteData;
            byte[] WriteTemp = new byte[10];
            int delay = 200;

            WriteTemp[0] = 0x7F;
            WriteTemp[1] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);
            if (res)
            {
                WriteTemp[0] = 0x56;
                if (On)
                {
                    if (Channel == 0)
                    {
                        WriteTemp[1] = 0x0E;
                    }
                    if (Channel == 1)
                    {
                        WriteTemp[1] = 0x0D;
                    }
                    if (Channel == 2)
                    {
                        WriteTemp[1] = 0x0B;
                    }
                    if (Channel == 3)
                    {
                        WriteTemp[1] = 0x07;
                    }
                }
                else
                {
                    WriteTemp[1] = 0x0F;
                }
                WriteData = WriteTemp.Skip(0).Take(2).ToArray();
                res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
                GeneralFunction.Delay(delay);
            }
            return res;
        }

        private bool FI2CUSB_QSFP28G_Gen3ClearInternal()
        {
            //控制QSFP28G_Gen3产品所有通道LaserOff
            bool res = false;

            for (byte i = 0; i < 4; i++)
            {
                res = FI2CUSB_QSFP28G_Gen3SetLaserOn(i, false);
                if (!res)
                {
                    break;
                }
            }
            return res;
        }

        private bool FI2CUSB_QSFP28G_Gen3ReadSN(ref string SN)
        {
            //读取28G_Gen3产品SN信息
            bool res = false;
            string fi2cName = "FI2CUSB";
            byte[] WriteData;
            byte[] WriteTemp = new byte[10];
            byte[] ReadData = new byte[16];
            int delay = 200;

            WriteTemp[0] = 0x7F;
            WriteTemp[1] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0xC4;
            WriteData = WriteTemp.Skip(0).Take(1).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            SN = "";
            res = cfi2ctool[fi2cName].ReadFI2CUSBData(fi2cAddr, 0xC4, ref ReadData);
            if (res)
            {
                SN = System.Text.Encoding.Default.GetString(ReadData);
            }
            return res;
        }
        #endregion

        #region QSFP56
        private bool FI2CUSB_QSFP56SetDAC(int Channel, int DAC)
        {
            //设置OSFP56产品指定通道加载的电流强度DAC值（注：如果DAC=0，则LaserOff）
            bool res = false;
            string fi2cName = "FI2CUSB";
            byte[] WriteData;
            byte[] WriteTemp = new byte[10];
            int delay = 200;

            //Write Password
            WriteTemp[0] = 0x7A;
            WriteTemp[1] = 0xDF;
            WriteTemp[2] = 0x5E;
            WriteTemp[3] = 0x75;
            WriteTemp[4] = 0xCD;
            WriteData = WriteTemp.Skip(0).Take(5).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            //Select Page
            WriteTemp[0] = 0x7F;
            WriteTemp[1] = 0x7F;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            if (res) res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            //Set DAC
            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xA0;
            switch (Channel)
            {
                case 0:
                    WriteTemp[5] = 0x16;
                    break;
                case 1:
                    WriteTemp[5] = 0x18;
                    break;
                case 2:
                    WriteTemp[5] = 0x1A;
                    break;
                case 3:
                    WriteTemp[5] = 0x20;
                    break;
                default:
                    WriteTemp[5] = 0x00;
                    break;
            }

            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            if (DAC == 0)
            {
                WriteTemp[8] = 0x00;
                WriteTemp[9] = 0x00;
            }
            else
            {
                WriteTemp[8] = (byte)DAC;
                WriteTemp[9] = 0x01;
            }
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            if (res) res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            if (res) res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            return res;
        }

        private bool FI2CUSB_QSFP56ClearInternal()
        {
            //控制QSFP56产品所有通道LaserOff
            bool res = false;
            string fi2cName = "FI2CUSB";
            byte[] WriteData;
            byte[] WriteTemp = new byte[10];
            int delay = 200;

            //Write Password
            WriteTemp[0] = 0x7A;
            WriteTemp[1] = 0xDF;
            WriteTemp[2] = 0x5E;
            WriteTemp[3] = 0x75;
            WriteTemp[4] = 0xCD;
            WriteData = WriteTemp.Skip(0).Take(5).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            //Select Page
            WriteTemp[0] = 0x7F;
            WriteTemp[1] = 0x7F;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xA0;
            WriteTemp[5] = 0x16;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xA0;
            WriteTemp[5] = 0x18;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xA0;
            WriteTemp[5] = 0x1A;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xA0;
            WriteTemp[5] = 0x20;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            return res;
        }

        private bool FI2CUSB_QSFP56ReadSN(ref string SN)
        {
            //读取QSFP56产品SN信息
            bool res = false;
            string fi2cName = "FI2CUSB";
            byte[] WriteData;
            byte[] WriteTemp = new byte[10];
            byte[] ReadData = new byte[16];
            int delay = 200;

            WriteTemp[0] = 0x7F;
            WriteTemp[1] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0xA6;
            WriteData = WriteTemp.Skip(0).Take(1).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            SN = "";
            res = cfi2ctool[fi2cName].ReadFI2CUSBData(fi2cAddr, 0xA6, ref ReadData);
            if (res)
            {
                SN = System.Text.Encoding.Default.GetString(ReadData);
            }
            return res;
        }
        #endregion

        #region OSFP2X200G
        private bool FI2CUSB_OSFP2X200GSetDAC(int Channel, int DAC)
        {
            //设置OSFP2X200G产品指定通道加载的电流强度DAC值（注：如果DAC=0，则LaserOff）
            bool res = false;
            string fi2cName = "FI2CUSB";
            byte[] WriteData;
            byte[] WriteTemp = new byte[10];
            int delay = 200;

            //Write Password
            WriteTemp[0] = 0x7A;
            WriteTemp[1] = 0xDF;
            WriteTemp[2] = 0x5E;
            WriteTemp[3] = 0x75;
            WriteTemp[4] = 0xCD;
            WriteData = WriteTemp.Skip(0).Take(5).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            //Select Page
            WriteTemp[0] = 0x7F;
            WriteTemp[1] = 0x7F;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            if (res) res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            //Set DAC
            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xAD;
            switch (Channel)
            {
                case 0:
                    WriteTemp[5] = 0x10;
                    break;
                case 1:
                    WriteTemp[5] = 0x12;
                    break;
                case 2:
                    WriteTemp[5] = 0x14;
                    break;
                case 3:
                    WriteTemp[5] = 0x16;
                    break;
                case 4:
                    WriteTemp[5] = 0x18;
                    break;
                case 5:
                    WriteTemp[5] = 0x1A;
                    break;
                case 6:
                    WriteTemp[5] = 0x1C;
                    break;
                case 7:
                    WriteTemp[5] = 0x1E;
                    break;
                default:
                    WriteTemp[5] = 0x00;
                    break;
            }

            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            if (DAC == 0)
            {
                WriteTemp[8] = 0x00;
                WriteTemp[9] = 0x00;
            }
            else
            {
                WriteTemp[8] = (byte)DAC;
                WriteTemp[9] = 0x01;
            }
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            if (res) res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            if (res) res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            return res;
        }

        private bool FI2CUSB_OSFP2X200GClearInternal()
        {
            //控制OSFP2X200G产品所有通道LaserOff
            bool res = false;
            string fi2cName = "FI2CUSB";
            byte[] WriteData;
            byte[] WriteTemp = new byte[10];
            int delay = 200;

            //Write Password
            WriteTemp[0] = 0x7A;
            WriteTemp[1] = 0xDF;
            WriteTemp[2] = 0x5E;
            WriteTemp[3] = 0x75;
            WriteTemp[4] = 0xCD;
            WriteData = WriteTemp.Skip(0).Take(5).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            //Select Page
            WriteTemp[0] = 0x7F;
            WriteTemp[1] = 0x7F;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xAD;
            WriteTemp[5] = 0x10;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xAD;
            WriteTemp[5] = 0x12;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xAD;
            WriteTemp[5] = 0x14;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xAD;
            WriteTemp[5] = 0x16;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xAD;
            WriteTemp[5] = 0x18;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xAD;
            WriteTemp[5] = 0x1A;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xAD;
            WriteTemp[5] = 0x1C;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x81;
            WriteTemp[1] = 0x01;
            WriteTemp[2] = 0x00;
            WriteTemp[3] = 0x00;
            WriteTemp[4] = 0xAD;
            WriteTemp[5] = 0x1E;
            WriteTemp[6] = 0x00;
            WriteTemp[7] = 0x01;
            WriteTemp[8] = 0x00;
            WriteTemp[9] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(10).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0x80;
            WriteTemp[1] = 0x7E;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            return res;
        }

        private bool FI2CUSB_OSFP2X200GReadSN(ref string SN)
        {
            //读取OSFP2X200G产品SN信息
            bool res = false;
            string fi2cName = "FI2CUSB";
            byte[] WriteData;
            byte[] WriteTemp = new byte[10];
            byte[] ReadData = new byte[16];
            int delay = 200;

            WriteTemp[0] = 0x7F;
            WriteTemp[1] = 0x00;
            WriteData = WriteTemp.Skip(0).Take(2).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            WriteTemp[0] = 0xA6;
            WriteData = WriteTemp.Skip(0).Take(1).ToArray();
            res = cfi2ctool[fi2cName].WriteFI2CUSBData(fi2cAddr, ref WriteData);
            GeneralFunction.Delay(delay);

            SN = "";
            res = cfi2ctool[fi2cName].ReadFI2CUSBData(fi2cAddr, 0xA6, ref ReadData);
            if (res)
            {
                SN = System.Text.Encoding.Default.GetString(ReadData);
            }
            return res;
        }
        #endregion
    }
}