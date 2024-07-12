using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Threading;

namespace FocusingLensAligner
{
    //国惠宽波段红外相机底层功能
    //相机型号：GH-SWU2-VS15
    //相机工作波段：400-1700nm

    public static class GhoptoIRCameraAPI
    {
        //动态链接库API功能函数
        [DllImport("CS_IRAPIs.dll")]
        public extern static void Init();

        [DllImport("CS_IRAPIs.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool GetUsbDevice(ref int handle);

        [DllImport("CS_IRAPIs.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool CloseIR(int handle);

        [DllImport("CS_IRAPIs.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SetBias(int handle, uint biasValue);

        [DllImport("CS_IRAPIs.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SetGain(int handle, uint gainValue);

        [DllImport("CS_IRAPIs.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SetIntegration(int handle, uint integrationValue);

        [DllImport("CS_IRAPIs.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool ControlShutter(int handle);

        [DllImport("IRSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool IR_IsOpen(int handle);

        [DllImport("IRSDK.dll")]
        public extern static int IR_FreeDevices();
    }

    public class cGhoptoIRCameraTools
    {
        public void InitSDK()
        {
            //初始化SDK环境
            GhoptoIRCameraAPI.Init();
        }

        public void UnInitSDK()
        {
            //释放SDK占用资源
            GhoptoIRCameraAPI.IR_FreeDevices();
        }

        public bool OpenCamera(ref int handle)
        {
            //打开相机
            bool res = GhoptoIRCameraAPI.GetUsbDevice(ref handle);
            return res;
        }

        public bool CloseCamera(int handle)
        {
            //关闭相机
            bool res = GhoptoIRCameraAPI.CloseIR(handle);
            return res;
        }

        public bool IsCameraOpen(int handle)
        {
            //查询相机是否打开
            bool res = false;

            if (handle > 0)
            {
                res = GhoptoIRCameraAPI.IR_IsOpen(handle);
            }
            return res;
        }

        public bool SetIntegration(int handle, uint Value)
        {
            //设置积分时间
            bool res = false;

            if (IsCameraOpen(handle) == true)
            {
                if ((Value == 100000) || (Value == 200000))
                {
                    Value = Value + 1;
                }
                res = GhoptoIRCameraAPI.SetIntegration(handle, Value);
            }
            return res;
        }

        public bool SetGain(int handle, uint Value)
        {
            //设置增益
            bool res = false;

            if (IsCameraOpen(handle) == true)
            {
                res = GhoptoIRCameraAPI.SetGain(handle, Value);
            }
            return res;
        }

        public bool SetBias(int handle, uint Value)
        {
            //设置硬件Bias
            bool res = false;

            if (IsCameraOpen(handle) == true)
            {
                res = GhoptoIRCameraAPI.SetBias(handle, Value);
            }
            return res;
        }

        public bool ControlShutter(int handle)
        {
            //快门矫正
            bool res = false;

            if (IsCameraOpen(handle) == true)
            {
                res = GhoptoIRCameraAPI.ControlShutter(handle);
            }
            return res;
        }

        public bool AdjustParameter(int handle, Enum_DnCameraViewObject objectID, bool shutterCalibration)
        {
            //调整相机内部相关参数
            //提醒：执行此函数成功后，可以运行用供应商提供的标准Demo软件，通过控制对话框中的“GET DEVICE CONFIG PARAM”按钮查看数值
            bool res = false;
            uint integrationValue = 0;
            uint gainValue = 0;
            uint biasValue = 0;

            if (GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild == false)
            {
                return true;
            }

            if (GlobalParameters.HardwareInitialStatus.GhoptoIRCamera_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("The Ghopto IR camera does not be initialized", Enum_MachineStatus.ERROR);
                return false;
            }

            switch (objectID)
            {
                case Enum_DnCameraViewObject.Window:
                    //产品盒子窗口
                    biasValue = GlobalParameters.systemconfig.GhoptoIRCameraConfig.ViewBoxWindowBias;
                    gainValue = GlobalParameters.systemconfig.GhoptoIRCameraConfig.ViewBoxWindowGain;
                    integrationValue = GlobalParameters.systemconfig.GhoptoIRCameraConfig.ViewBoxWindowIntegration;
                    break;

                case Enum_DnCameraViewObject.LaserSpot:
                    //光斑
                    biasValue = GlobalParameters.systemconfig.GhoptoIRCameraConfig.ViewLaserSpotBias;
                    gainValue = GlobalParameters.systemconfig.GhoptoIRCameraConfig.ViewLaserSpotGain;
                    integrationValue = GlobalParameters.systemconfig.GhoptoIRCameraConfig.ViewLaserSpotIntegration;
                    break;

                case Enum_DnCameraViewObject.Lens:
                    //产品盒子中的Lens
                    biasValue = GlobalParameters.systemconfig.GhoptoIRCameraConfig.ViewLensInsideBoxBias;
                    gainValue = GlobalParameters.systemconfig.GhoptoIRCameraConfig.ViewLensInsideBoxGain;
                    integrationValue = GlobalParameters.systemconfig.GhoptoIRCameraConfig.ViewLensInsideBoxIntegration;
                    break;
            }

            //设置通道1的DriveHardwareBias
            res = SetBias(handle, biasValue);
            //Thread.Sleep(200);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Failed to adjust Ghopto IR camera parameter", Enum_MachineStatus.ERROR);
                return false;
            }

            //设置积分时间
            //积分时间必需大于1000，每个单位0.1us，默认10000即为1ms，最大可以设置到200ms
            res = SetIntegration(handle, integrationValue);
            //Thread.Sleep(200);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Failed to adjust Ghopto IR camera parameter", Enum_MachineStatus.ERROR);
                return false;
            }

            //设置增益 
            //增益默认为1，场景光强时采用Gain 2,3
            res = SetGain(handle, gainValue);
            //Thread.Sleep(200);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Failed to adjust Ghopto IR camera parameter", Enum_MachineStatus.ERROR);
                return false;
            }

            //决定是否需要执行一次快门矫正
            //设置完新的积分或增益，需要重新做红外矫正，简单的方法是做一次快门矫正
            //注意：一般只在相机成功初始化并打开后做一次快门矫正操作，反复做快门矫正会大影响相机快门寿命
            //修改积分后，新的图像帧一般要等待2张出图时间，可以sleep延时等待一下
            if (shutterCalibration == true)
            {
                res = ControlShutter(handle);
                //Thread.Sleep(1000);
                if (res == false)
                {
                    return false;
                }
            }

            return true; 
        }
    }
}