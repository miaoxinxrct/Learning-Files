using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using STD_ISourceMeter;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace FocusingLensAligner
{
    class ProcessFlow
    {
        //----视觉相关功能函数

        #region 相机校准分辨率
        public bool VisionCameraCalibrate(double UpRingValue, double UpSpotValue, double DnRingValue, double DnSpotValue, double GridStep, bool IsUpCamera, ref double PixelSize)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            GlobalFunction.LightSourcesTools.LightSource_OpenUpRing(UpRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenUpSpot(UpSpotValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenDnRing(DnRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenDnSpot(DnSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);

            if (GlobalParameters.globitmap != null)
            {
                GlobalParameters.globitmap.Dispose();
            }

            if (IsUpCamera == true)
            {
                GlobalFunction.CameraTools.Camera_UpCameraSnap(ref GlobalParameters.globitmap);
                if (GlobalParameters.globitmap == null)
                {
                    GlobalFunction.updateStatusDelegate("上相机取像失败", Enum_MachineStatus.ERROR);
                    res = false;
                }
                else
                {
                    res = GlobalFunction.ImageProcessTools.Image_CameraCalibration(GridStep, true, ref PixelSize);
                }
            }
            else
            {
                GlobalFunction.CameraTools.Camera_DnCameraSnap(ref GlobalParameters.globitmap);
                if (GlobalParameters.globitmap == null)
                {
                    GlobalFunction.updateStatusDelegate("下相机取像失败", Enum_MachineStatus.ERROR);
                    res = false;
                }
                else
                {
                    res = GlobalFunction.ImageProcessTools.Image_CameraCalibration(GridStep, false, ref PixelSize);
                }
            }
            GlobalFunction.LightSourcesTools.LightSource_CloseUpRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseDnRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseUpSpot();
            GlobalFunction.LightSourcesTools.LightSource_CloseDnSpot();

            return res;
        }
        #endregion

        #region 上相机识别Box夹爪标定块中心孔位置(像素坐标)和直径(像素尺寸)
        public bool VisionRecognizeBoxGripper(double UpRingValue, double UpSpotValue, ref VisionResult.EpoxyResult gripperBlockResult)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            GlobalFunction.LightSourcesTools.LightSource_OpenUpRing(UpRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenUpSpot(UpSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);
            if (GlobalParameters.globitmap != null)
            {
                GlobalParameters.globitmap.Dispose();
            }
            GlobalFunction.CameraTools.Camera_UpCameraSnap(ref GlobalParameters.globitmap);
            if (GlobalParameters.globitmap == null)
            {
                GlobalFunction.updateStatusDelegate("上相机取像失败", Enum_MachineStatus.ERROR);
                res = false;
            }
            else
            {
                res = GlobalFunction.ImageProcessTools.Image_DnwardViewEpoxySpot(ref gripperBlockResult);
            }
            GlobalFunction.LightSourcesTools.LightSource_CloseUpRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseUpSpot();

            return res;
        }
        #endregion

        #region 上相机识别Lens夹爪中心位置(像素坐标)
        public bool VisionRecognizeLensGripper(double UpRingValue, double UpSpotValue, ref VisionResult.GripperResult gripperResult)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            GlobalFunction.LightSourcesTools.LightSource_OpenUpRing(UpRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenUpSpot(UpSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);
            if (GlobalParameters.globitmap != null)
            {
                GlobalParameters.globitmap.Dispose();
            }
            GlobalFunction.CameraTools.Camera_UpCameraSnap(ref GlobalParameters.globitmap);
            if (GlobalParameters.globitmap == null)
            {
                GlobalFunction.updateStatusDelegate("上相机取像失败", Enum_MachineStatus.ERROR);
                res = false;
            }
            else
            {
                res = GlobalFunction.ImageProcessTools.Image_DnwardViewGripper(ref gripperResult);
            }
            GlobalFunction.LightSourcesTools.LightSource_CloseUpRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseUpSpot();

            return res;
        }
        #endregion

        #region 上相机识别胶斑中心位置(像素坐标)和直径(像素尺寸)
        public bool VisionRecognizeEpoxySpot(double UpRingValue, double UpSpotValue, ref VisionResult.EpoxyResult epoxySpotResult)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            GlobalFunction.LightSourcesTools.LightSource_OpenUpRing(UpRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenUpSpot(UpSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);
            if (GlobalParameters.globitmap != null)
            {
                GlobalParameters.globitmap.Dispose();
            }
            GlobalFunction.CameraTools.Camera_UpCameraSnap(ref GlobalParameters.globitmap);
            if (GlobalParameters.globitmap == null)
            {
                GlobalFunction.updateStatusDelegate("上相机取像失败", Enum_MachineStatus.ERROR);
                res = false;
            }
            else
            {
                res = GlobalFunction.ImageProcessTools.Image_DnwardViewEpoxySpot(ref epoxySpotResult);
            }
            GlobalFunction.LightSourcesTools.LightSource_CloseUpRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseUpSpot();

            return res;
        }
        #endregion

        #region 上相机识别物料盘中Lens中心位置(像素坐标)
        public bool VisionRecognizeLensInTray(double UpRingValue, double UpSpotValue, ref VisionResult.LensResult lensResult)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            GlobalFunction.LightSourcesTools.LightSource_OpenUpRing(UpRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenUpSpot(UpSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);
            if (GlobalParameters.globitmap != null)
            {
                GlobalParameters.globitmap.Dispose();
            }
            GlobalFunction.CameraTools.Camera_UpCameraSnap(ref GlobalParameters.globitmap);
            if (GlobalParameters.globitmap == null)
            {
                GlobalFunction.updateStatusDelegate("上相机取像失败", Enum_MachineStatus.ERROR);
                res = false;
            }
            else
            {
                res = GlobalFunction.ImageProcessTools.Image_DnwardViewLens(ref lensResult);
            }
            GlobalFunction.LightSourcesTools.LightSource_CloseUpRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseUpSpot();

            return res;
        }
        #endregion

        #region 上相机识别物料盘中产品Box左上角位置(像素坐标)
        public bool VisionRecognizeBoxInTray(double UpRingValue, double UpSpotValue, ref VisionResult.BoxResult boxResult)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            GlobalFunction.LightSourcesTools.LightSource_OpenUpRing(UpRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenUpSpot(UpSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);
            if (GlobalParameters.globitmap != null)
            {
                GlobalParameters.globitmap.Dispose();
            }
            GlobalFunction.CameraTools.Camera_UpCameraSnap(ref GlobalParameters.globitmap);
            if (GlobalParameters.globitmap == null)
            {
                GlobalFunction.updateStatusDelegate("上相机取像失败", Enum_MachineStatus.ERROR);
                res = false;
            }
            else
            {
                res = GlobalFunction.ImageProcessTools.Image_DnwardViewBox(ref boxResult);
            }
            GlobalFunction.LightSourcesTools.LightSource_CloseUpRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseUpSpot();

            return res;
        }
        #endregion

        #region 下相机识别产品Box窗口中心位置(像素坐标)和窗口直径(像素尺寸)
        public bool VisionRecognizeBoxWindow(double DnRingValue, double DnSpotValue, ref VisionResult.BoxResult windowResult)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            //打开光源
            GlobalFunction.LightSourcesTools.LightSource_OpenDnRing(DnRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenDnSpot(DnSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);

            //延时等待相机成像稳定
            GeneralFunction.Delay((int)GlobalParameters.systemconfig.IRCameraCaptureCardConfig.ViewBoxWindowDelay * 1000);

            for (int i = 0; i < 3; i++)
            {
                //清除图像缓存
                if (GlobalParameters.globitmap != null)
                {
                    GlobalParameters.globitmap.Dispose();
                }

                //取像识别
                GlobalFunction.CameraTools.Camera_DnCameraSnap(ref GlobalParameters.globitmap);
                if (GlobalParameters.globitmap == null)
                {
                    GlobalFunction.updateStatusDelegate("下相机取像失败", Enum_MachineStatus.ERROR);
                    res = false;
                    break;
                }
                res = GlobalFunction.ImageProcessTools.Image_UpwardViewWindow(ref windowResult);
                if (res == true)
                {
                    break;
                }
                GeneralFunction.Delay(200);
            }

            //关闭光源
            GlobalFunction.LightSourcesTools.LightSource_CloseDnRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseDnSpot();

            //如果识别成功，根据配置检查窗口中心与下相机中心的偏移量是否超标
            if (res == true)
            {
                if (GlobalParameters.productconfig.boxlensConfig.CheckBoxWindowCentreOffset == true)
                {
                    double Offset_x = Math.Abs(windowResult.point.X - windowResult.imagecenter.X) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.xscale;
                    double Offset_y = Math.Abs(windowResult.point.Y - windowResult.imagecenter.Y) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.yscale;
                    double Offset_distance = Math.Sqrt(Math.Pow(Offset_x, 2) + Math.Pow(Offset_y, 2));
                    GlobalFunction.updateStatusDelegate("产品Box窗口中心偏离下相机中心: " + Offset_distance.ToString("f3") + "mm", Enum_MachineStatus.NORMAL);
                    if (Offset_distance >= GlobalParameters.productconfig.boxlensConfig.BoxWindowCentreOffsetLimit)
                    {
                        GlobalFunction.updateStatusDelegate("产品Box窗口中心与下相机中心定位偏移量超标异常", Enum_MachineStatus.ERROR);
                        return false;
                    }
                }
            }

            return res;
        }
        #endregion

        #region 下相机识别光斑中心位置(像素坐标)
        public bool VisionRecognizeLaserSpot(double MinThreshold, double MaxThreshold, ref VisionResult.LaserSpotResult laserSpotResult)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            //延时等待相机成像稳定
            GeneralFunction.Delay((int)GlobalParameters.systemconfig.IRCameraCaptureCardConfig.ViewLaserSpotDelay * 1000);

            for (int i = 0; i < 3; i++)
            {
                //清除图像缓存
                if (GlobalParameters.globitmap != null)
                {
                    GlobalParameters.globitmap.Dispose();
                }

                //取像识别
                GlobalFunction.CameraTools.Camera_DnCameraSnap(ref GlobalParameters.globitmap);
                if (GlobalParameters.globitmap == null)
                {
                    GlobalFunction.updateStatusDelegate("下相机取像失败", Enum_MachineStatus.ERROR);
                    res = false;
                    break;
                }
                res = GlobalFunction.ImageProcessTools.Image_UpwardViewLaserSpot(MinThreshold, MaxThreshold, false, ref laserSpotResult);
                if (res == true)
                {
                    break;
                }
                GeneralFunction.Delay(200);
            }

            return res;
        }
        #endregion

        #region 下相机识别指定通道光斑中心位置(机械坐标)
        public bool VisionRecognizeChannelLaserSpot(int channel)
        {
            bool res = false;
            string str = string.Empty;

            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            VisionResult.LaserSpotResult laserSpotResult = new VisionResult.LaserSpotResult();
            double MaxThreshold = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLaserSpotMaxThreshold;
            double MinThreshold = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLaserSpotMinThreshold;

            //下相机识别光斑中心（最多尝试3次）
            for (int i = 0; i < 3; i++)
            {
                res = GlobalFunction.ProcessFlow.VisionRecognizeLaserSpot(MinThreshold, MaxThreshold, ref laserSpotResult);
                if (res == true)
                {
                    break;
                }
            }
            if (res == false)
            {
                str = "下相机识别CH" + channel.ToString() + "通道光斑中心失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                return false;
            }

            //计算光斑中心与下相机中心的偏移距离
            double centeroffsetx = (laserSpotResult.center.X - laserSpotResult.imagecenter.X) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.xscale;
            double centeroffsety = (laserSpotResult.center.Y - laserSpotResult.imagecenter.Y) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.yscale;

            //换算出光斑中心在步进轴坐标系中X2_Y2方向定位坐标
            GlobalParameters.processdata.laserSpotRecognize[channel].posx2 = GlobalParameters.systemconfig.LensGripperConfig.offsetx - (GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.X1 - centeroffsetx);
            GlobalParameters.processdata.laserSpotRecognize[channel].posy2 = GlobalParameters.systemconfig.LensGripperConfig.offsety - (GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.Y1 + centeroffsety);

            str = "下相机识别CH" + channel.ToString() + "通道光斑中心成功";
            GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
            double posx = GlobalParameters.processdata.laserSpotRecognize[channel].posx2;
            double posy = GlobalParameters.processdata.laserSpotRecognize[channel].posy2;
            str = "CH" + channel.ToString() + "通道光斑位置: X2 = " + posx.ToString("f3") + " Y2 = " + posy.ToString("f3");
            GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.NORMAL);
            return true;
        }
        #endregion

        #region 下相机识别产品Box中Lens中心位置(像素坐标)
        public bool VisionRecognizeLensInBox(double DnRingValue, double DnSpotValue, ref VisionResult.LensResult lensResult)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            //打开光源
            GlobalFunction.LightSourcesTools.LightSource_OpenDnRing(DnRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenDnSpot(DnSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);

            //延时等待相机成像稳定
            GeneralFunction.Delay((int)GlobalParameters.systemconfig.IRCameraCaptureCardConfig.ViewLensInsideBoxDelay * 1000);

            for (int i = 0; i < 3; i++)
            {
                //清除图像缓存
                if (GlobalParameters.globitmap != null)
                {
                    GlobalParameters.globitmap.Dispose();
                }

                //取像识别
                GlobalFunction.CameraTools.Camera_DnCameraSnap(ref GlobalParameters.globitmap);
                if (GlobalParameters.globitmap == null)
                {
                    GlobalFunction.updateStatusDelegate("下相机取像失败", Enum_MachineStatus.ERROR);
                    res = false;
                    break;
                }
                bool IsPatMatch = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensPatMatch;
                res = GlobalFunction.ImageProcessTools.Image_UpwardViewLens(IsPatMatch, ref lensResult);
                if (res == true)
                {
                    break;
                }
                GeneralFunction.Delay(200);
            }

            //关闭光源
            GlobalFunction.LightSourcesTools.LightSource_CloseDnRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseDnSpot();

            return res;
        }
        #endregion

        #region 根据视野中需要观察的目标动态调整下相机工作条件
        public bool VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject ObjectID)
        {
            bool res = true;

            //动态调整图像采集卡工作参数
            double CaptureCardBrightness = 0;
            double CaptureCardContrast = 0;
            switch (ObjectID)
            {
                case Enum_DnCameraViewObject.Window:
                    CaptureCardBrightness = GlobalParameters.systemconfig.IRCameraCaptureCardConfig.ViewBoxWindowBrightness;
                    CaptureCardContrast = GlobalParameters.systemconfig.IRCameraCaptureCardConfig.ViewBoxWindowContrast;
                    break;

                case Enum_DnCameraViewObject.LaserSpot:
                    CaptureCardBrightness = GlobalParameters.systemconfig.IRCameraCaptureCardConfig.ViewLaserSpotBrightness;
                    CaptureCardContrast = GlobalParameters.systemconfig.IRCameraCaptureCardConfig.ViewLaserSpotContrast;
                    break;

                case Enum_DnCameraViewObject.Lens:
                    CaptureCardBrightness = GlobalParameters.systemconfig.IRCameraCaptureCardConfig.ViewLensInsideBoxBrightness;
                    CaptureCardContrast = GlobalParameters.systemconfig.IRCameraCaptureCardConfig.ViewLensInsideBoxContrast;
                    break;
            }
            GlobalFunction.CameraTools.Camera_SetExposure("DnCamera", CaptureCardBrightness);
            GlobalFunction.CameraTools.Camera_SetGain("DnCamera", CaptureCardContrast);
            GeneralFunction.Delay(100);

            //动态调整国惠宽波段红外相机内部工作参数
            if ((GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild == true) && (GlobalParameters.HardwareInitialStatus.GhoptoIRCamera_InitialStatus == true))
            {
                res = GlobalFunction.GhoptoIRCameraTools.AdjustParameter(GlobalParameters.GhoptoIRCameraHandle, ObjectID, false);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("动态调整国惠宽波段红外相机参数失败", Enum_MachineStatus.ERROR);
                }
            }

            return res;
        }
        #endregion

        //----点胶相关功能函数

        #region 各轴移动到点胶安全位
        public bool DispenserMoveToSafePosition()
        {
            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;
            double posu = 0;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Box夹爪气缸或胶针气缸上抬失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //X1_Y1双轴联动到点胶安全位
            posx = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.X1;
            posy = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Y1;
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1ToLocation(posx, posy, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X1_Y1双轴联动到点胶安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //U1轴旋转至点胶角度
            posu = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.U1;
            res = GlobalFunction.MotionTools.Motion_MoveU1ToLocation(posu, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("U1轴旋转至点胶角度失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //X2_Y2_Z2三轴联动到点胶安全位
            posx = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.X2;
            posy = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Y2;
            posz = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Z2;
            res = GlobalFunction.MotionTools.Motion_MoveX2Y2Z2ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到点胶安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Z1轴运动到点胶安全位
            posz = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Z1轴运动到点胶安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            return true;
        }
        #endregion

        #region 在试点胶位置上点胶
        public bool DispenserEpoxyOnBoard()
        {
            bool res = false;

            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Box夹爪气缸或胶针气缸上抬失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //胶针移动到试点胶位置
            double posx = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.X1;
            double posy = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.Y1;
            double posu = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.U1;
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1U1ToLocation(posx, posy, posu, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针移动到试点胶位置失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //胶针气缸下降
            res = GlobalFunction.IOControlTools.EpoxyDipUpDown(true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针气缸下降失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //胶针减速下降到点胶位置
            GlobalFunction.MotionTools.Motion_SetZ1SpeedPercent(30);
            double posz = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            GlobalFunction.MotionTools.Motion_SetZ1SpeedPercent(100);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针下降到点胶位置失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GeneralFunction.Delay(500);

            //胶针气缸上抬
            res = GlobalFunction.IOControlTools.EpoxyDipUpDown(false, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针气缸上抬失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //胶针上抬到点胶安全位置
            posz = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针上抬到点胶安全位置失败", Enum_MachineStatus.ERROR);
            }

            return res;
        }
        #endregion

        #region 自动试点胶测试
        public bool DispenserEpoxyTest(ref double EpoxySpotDiameter)
        {
            bool res = false;

            //X2_Y2_Z2三轴联动到试点胶避让位（避免撞机）
            double posx = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.X2;
            double posy = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.Y2;
            double posz = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.Z2;
            res = GlobalFunction.ProcessFlow.MotionAxisX2Y2Z2MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到点胶安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //蘸胶
            res = GlobalFunction.ProcessFlow.StepFlow_DispenserEpoxyDip();
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("蘸胶失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //点胶(一般点在胶池板靠近左上角或者试胶板上的预先标定位置)
            res = GlobalFunction.ProcessFlow.DispenserEpoxyOnBoard();
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("在胶池上点胶失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //上相机移动到测胶斑位置
            posx = GlobalParameters.systemconfig.DispenserConfig.UpCameraViewEpoxyPos.X1;
            posy = GlobalParameters.systemconfig.DispenserConfig.UpCameraViewEpoxyPos.Y1;
            posz = GlobalParameters.systemconfig.DispenserConfig.UpCameraViewEpoxyPos.Z1;
            res = GlobalFunction.ProcessFlow.MotionAxisX1Y1Z1MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("上相机移动到测胶斑位置失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //上相机识别胶斑
            VisionResult.EpoxyResult epoxyResult = new VisionResult.EpoxyResult();
            double UpRingValue = GlobalParameters.systemconfig.DispenserConfig.upringlightval;
            double UpSpotValue = GlobalParameters.systemconfig.DispenserConfig.upspotlightval;
            res = GlobalFunction.ProcessFlow.VisionRecognizeEpoxySpot(UpRingValue, UpSpotValue, ref epoxyResult);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("上相机识别胶斑失败", Enum_MachineStatus.ERROR);
                return false;
            }
            EpoxySpotDiameter = GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale * epoxyResult.diameter;

            //擦胶
            res = GlobalFunction.ProcessFlow.StepFlow_DispenserEpoxyWipe();
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("擦胶失败", Enum_MachineStatus.ERROR);
            }

            return res;
        }
        #endregion

        #region 胶针气缸运动控制
        public bool DispenserEpoxyCylinderControl(bool Down)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.IOCard_InitialStatus == true)
            {
                if (Down == true)
                {
                    res = GlobalFunction.IOControlTools.EpoxyDipUpDown(true, true);
                    if (res == false)
                    {
                        GlobalFunction.updateStatusDelegate("胶针下降失败", Enum_MachineStatus.ERROR);
                        return false;
                    }
                    return true;
                }
                else
                {
                    res = GlobalFunction.IOControlTools.EpoxyDipUpDown(false, true);
                    if (res == false)
                    {
                        GlobalFunction.updateStatusDelegate("胶针上抬失败", Enum_MachineStatus.ERROR);
                        return false;
                    }
                    return true;
                }
            }

            GlobalFunction.updateStatusDelegate("IO卡未初始化，胶针气缸运动失败", Enum_MachineStatus.ERROR);
            return false;
        }
        #endregion

        #region 胶针移出产品Box返回到安全位
        public bool DispenserEpoxyPinOutOfBox()
        {
            bool res = false;

            //强制恢复X1/Y1/Z1轴速度百分比
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", 100);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y1", 100);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z1", 100);

            //X2_Y2_Z2三轴联动到点胶安全位（防呆措施：避让防止撞机）
            double posx = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.X2;
            double posy = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Y2;
            double posz = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Z2;
            res = GlobalFunction.MotionTools.Motion_MoveX2Y2Z2ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到点胶安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //点胶针回抬到进入产品Box安全高度
            posz = GlobalParameters.productconfig.positionConfig.dispenserBoxZ1Height - Math.Abs(GlobalParameters.productconfig.positionConfig.dispenserBoxZ1Up);
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("Z1", posz, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("点胶针回抬到进入产品Box安全高度失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //点胶针X方向安全移出产品Box
            posx = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.X1;
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("X1", posx, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("点胶针X方向安全移出产品Box失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //标记点胶针已从产品Box内部移出
            GlobalParameters.flagassembly.epoxypininboxflag = false;

            //胶针气缸上抬
            res = GlobalFunction.ProcessFlow.DispenserEpoxyCylinderControl(false);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针气缸上抬失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //X1_Y1_Z1三轴联动到点胶安全位
            posx = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.X1;
            posy = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Y1;
            posz = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X1_Y1_Z1三轴联动到点胶安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            return true;
        }
        #endregion

        //----运动轴相关功能函数

        #region 伺服Z1轴减速运动回安全位
            public bool MotionAxisZ1MoveToSafetyPosition()
        {

            bool res = false;
            string str = string.Empty;

            if (GlobalParameters.HardwareInitialStatus.MotionCard_InitialStatus == true)
            {
                res = GlobalFunction.MotionTools.Motion_SetZ1SpeedPercent(GlobalParameters.systemconfig.AxisSafetyPosConfig.AxisSpeedPercent);
                if (res == true)
                {
                    res = GlobalFunction.MotionTools.Motion_Z1UpToSafe();
                    if (res == false)
                    {
                        str = "Z1轴回安全位失败";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    }
                    res = GlobalFunction.MotionTools.Motion_SetZ1SpeedPercent(100);
                }
            }
            else
            {
                str = "运动轴卡未初始化，Z1轴回安全位失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
            }

            return res;
        }
        #endregion

        #region 步进Z2轴减速运动回安全位
        public bool MotionAxisZ2MoveToSafetyPosition()
        {
            bool res = false;
            string str = string.Empty;

            if (GlobalParameters.HardwareInitialStatus.MotionCard_InitialStatus == true)
            {
                res = GlobalFunction.MotionTools.Motion_SetZ2SpeedPercent(GlobalParameters.systemconfig.AxisSafetyPosConfig.AxisSpeedPercent);
                if (res == true)
                {
                    res = GlobalFunction.MotionTools.Motion_Z2UpToSafe();
                    if (res == false)
                    {
                        str = "Z2轴回安全位失败";
                        GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    }
                    res = GlobalFunction.MotionTools.Motion_SetZ2SpeedPercent(100);
                }
            }
            else
            {
                str = "运动轴卡未初始化，Z2轴回安全位失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
            }

            return res;
        }
        #endregion

        #region X2_Y2_Z2三轴联动到指定位置
        public bool MotionAxisX2Y2Z2MoveToLocation(double posx, double posy, double posz, bool sliderup, bool Z2up, bool wait)
        {
            string str = string.Empty;
            bool res = false;

            if (sliderup == true)
            {
                //Box夹爪气缸和胶针气缸上抬
                res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
                if (res == false)
                {
                    str = "Box夹爪气缸或胶针气缸上抬失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            res = GlobalFunction.MotionTools.Motion_MoveX2Y2Z2ToLocation(posx, posy, posz, Z2up, true);
            if (res == false)
            {
                str = "X2_Y2_Z2三轴联动失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                return false;
            }
            return true;
        }
        #endregion

        #region Y2_Z2双轴联动到指定位置
        public bool MotionAxisY2Z2MoveToLocation(double posy, double posz, bool sliderup, bool Z2up, bool wait)
        {
            string str = string.Empty;
            bool res = false;

            if (sliderup == true)
            {
                //Box夹爪气缸和胶针气缸上抬
                res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
                if (res == false)
                {
                    str = "Box夹爪气缸或胶针气缸上抬失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            res = GlobalFunction.MotionTools.Motion_MoveY2Z2ToLocation(posy, posz, Z2up, true);
            if (res == false)
            {
                str = "Y2_Z2双轴联动失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                return false;
            }
            return true;
        }
        #endregion

        #region X1_Y1_Z1三轴联动到指定位置
        public bool MotionAxisX1Y1Z1MoveToLocation(double posx, double posy, double posz, bool sliderup, bool Z1up, bool wait)
        {
            string str = string.Empty;
            bool res = false;

            if (sliderup == true)
            {
                //Box夹爪气缸和胶针气缸上抬
                res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
                if (res == false)
                {
                    str = "Box夹爪气缸或胶针气缸上抬失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            res = GlobalFunction.MotionTools.Motion_MoveX1Y1Z1ToLocation(posx, posy, posz, Z1up, true);
            if (res == false)
            {
                str = "X1_Y1_Z1三轴联动失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                return false;
            }
            return true;
        }
        #endregion

        #region X1_Y1_U1三轴联动到指定位置
        public bool MotionAxisX1Y1U1MoveToLocation(double posx, double posy, double posu, bool sliderup, bool Z1up, bool wait)
        {
            string str = string.Empty;
            bool res = false;

            if (sliderup == true)
            {
                //Box夹爪气缸和胶针气缸上抬
                res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
                if (res == false)
                {
                    str = "Box夹爪气缸或胶针气缸上抬失败";
                    GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            res = GlobalFunction.MotionTools.Motion_MoveX1Y1U1ToLocation(posx, posy, posu, Z1up, true);
            if (res == false)
            {
                str = "X1_Y1_U1三轴联动失败";
                GlobalFunction.updateStatusDelegate(str, Enum_MachineStatus.ERROR);
                return false;
            }
            return true;
        }
        #endregion

        //----加电相关功能函数

        #region 打开指定通道激光器
        public bool LaserTurnOnOff(int Channel, bool On)
        {
            bool res = false;
            string str = string.Empty;
            int CurrentDAC = 0;

            //获取当前产品的加电方式
            string PowerWay = GlobalParameters.productconfig.processConfig.powerway.ToString();
            switch (PowerWay)
            {
                case "VOLTAGE_I2C":         //I2C通信模块
                    //----此处需要加入功能代码
                    res = true;
                    break;

                case "VOLTAGE_FI2CUSB":     //FI2CUSB模块

                    if (GlobalParameters.HardwareInitialStatus.FI2CUSB_InitialStatus == false)
                    {
                        GlobalFunction.updateStatusDelegate("FI2CUSB通信设备未初始化，当前通道加电失败", Enum_MachineStatus.ERROR);
                        res = false;
                    }
                    else
                    {
                        CurrentDAC = GlobalParameters.productconfig.processConfig.laserIntensity[Channel].currentDAC;
                        if (On == false)
                        {
                            CurrentDAC = 0;
                        }
                        res = GlobalFunction.ProcessFlow.VOLTAGE_FI2CUSB_SetLaserIntensityDAC(Channel, CurrentDAC);
                        GeneralFunction.Delay(200);
                        if ((res == true) && (On == true))
                        {
                            //更新主界面加电源表读数显示
                            GlobalFunction.updateSourceMeterVoltageReadingDelegate();
                            GlobalFunction.updateSourceMeterCurrentReadingDelegate();
                        }
                    }
                    break;

                case "CURRENT_IO":          //源表电流源方式经过IO点切换加电通道
                    //----此处需要加入功能代码
                    res = true;
                    break;

                case "CURRENT_NO":          //源表电流源方式加电
                    if (GlobalParameters.HardwareInitialStatus.Keithley2401_1_InitialStatus == false)
                    {
                        GlobalFunction.updateStatusDelegate("Keithley2401_1 源表未初始化", Enum_MachineStatus.ERROR);
                        res = false;
                    }
                    else
                    {
                        //控制源表输出或关闭
                        res = GlobalFunction.SourceMetertools.Keilthly240x_TurnOnKeithley("Keithley2401-1", On);
                        GeneralFunction.Delay(200);
                        if ((res == true) && (On == true))
                        {
                            //更新主界面加电源表读数显示
                            GlobalFunction.updateSourceMeterVoltageReadingDelegate();
                            GlobalFunction.updateSourceMeterCurrentReadingDelegate();
                        }
                    }
                    break;

                case "DRIVERBOARD":         //S4500驱动板
                    //----此处需要加入功能代码
                    res = true;
                    break;

                case "EUIBOARD":            //EUI模块加电
                    //----此处需要加入功能代码
                    res = true;
                    break;
            }

            return res;
        }
        #endregion

        #region FI2CUSB方式LaserOn/Off
        private bool VOLTAGE_FI2CUSB_SetLaserIntensityDAC(int Channel, int CurrentDAC)
        {
            bool res = GlobalFunction.FI2CUSBTools.FI2CUSB_SetLaserDAC(GlobalParameters.systemconfig.ManageProductConfig.currentproduct, Channel, CurrentDAC);
            return res;
        }
        #endregion

        #region FI2CUSB方式所有通道LaserOn/Off
        private bool VOLTAGE_FI2CUSB_AllLaserOff()
        {
            bool res = GlobalFunction.FI2CUSBTools.FI2CUSB_AllChannelLaserOff(GlobalParameters.systemconfig.ManageProductConfig.currentproduct);
            return res;
        }
        #endregion

        //----夹爪相关功能函数

        #region Box夹爪控制
        public bool BoxGripperControl(bool Open)
        {
            bool res = false;
            int errorCode = 0;
            bool pend = false;

            if (GlobalParameters.HardwareInitialStatus.BoxGripper_InitialStatus == true)
            {
                if (Open == true)
                {
                    res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("BoxGripper", "BoxGripperOpen", ref errorCode);
                    if (GlobalParameters.systemconfig.SystemOperationConfig.grippersecuritydetection == true)
                    {
                        if (res == true)
                        {
                            //等待夹爪张开动作完成
                            while (pend == false)
                            {
                                GlobalFunction.IAIGripperTools.ElectricGripper_IsPositionMoveCompleted("BoxGripper", ref pend, ref errorCode);
                            }
                            return true;
                        }
                    }
                    else
                    {
                        Thread.Sleep(GlobalParameters.systemconfig.InstrumentConfig.GripperMotionDelay);
                        if (res == true)
                        {
                            return true;
                        }
                    }
                    GlobalFunction.updateStatusDelegate("Box夹爪张开失败", Enum_MachineStatus.ERROR);
                    return false;
                }
                else
                {
                    if (GlobalParameters.systemconfig.SystemOperationConfig.grippersecuritydetection == true)
                    {
                        uint OriginalMotorCurrent = 0;
                        uint MotorCurrent = 0;

                        //读取夹爪未闭合时夹爪驱动轴伺服电机电流值
                        res = GlobalFunction.IAIGripperTools.ElectricGripper_GetMotorCurrentAmpere("BoxGripper", ref OriginalMotorCurrent, ref errorCode);
                        if (res == true)
                        {
                            res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("BoxGripper", "BoxGripperClose", ref errorCode);
                            if (res == true)
                            {
                                //等待夹爪闭合动作完成
                                while (pend == false)
                                {
                                    GlobalFunction.IAIGripperTools.ElectricGripper_IsPositionMoveCompleted("BoxGripper", ref pend, ref errorCode);
                                }

                                //读取夹爪闭合后夹爪驱动轴伺服电机电流值
                                GlobalFunction.IAIGripperTools.ElectricGripper_GetMotorCurrentAmpere("BoxGripper", ref MotorCurrent, ref errorCode);
                                if (Math.Abs(MotorCurrent - OriginalMotorCurrent) < 0.1)
                                {
                                    //如果电流无突变，说明夹爪未夹紧物料或者无物料，需要检查夹取位置参数项中是否配置有推压设置
                                    //推压设置后夹爪控制器会自动检测夹爪驱动轴伺服电机电流并输出PEND信号
                                    GlobalFunction.updateStatusDelegate("产品Box夹取失败", Enum_MachineStatus.ERROR);
                                    return false;
                                }
                                return true;
                            }
                        }
                    }
                    else
                    {
                        res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("BoxGripper", "BoxGripperClose", ref errorCode);
                        //延时等待夹爪闭合动作完成
                        Thread.Sleep(GlobalParameters.systemconfig.InstrumentConfig.GripperMotionDelay);
                        if (res == true)
                        {
                            return true;
                        }
                    }
                    GlobalFunction.updateStatusDelegate("Box夹爪闭合失败", Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            GlobalFunction.updateStatusDelegate("Box夹爪控制器未初始化，Box夹爪运动失败", Enum_MachineStatus.ERROR);
            return false;
        }
        #endregion

        #region Lens夹爪控制
        public bool LensGripperControl(bool Open)
        {
            bool res = false;
            int errorCode = 0;
            bool pend = false;

            if (GlobalParameters.HardwareInitialStatus.LensGripper_InitialStatus == true)
            {
                if (Open == true)
                {
                    res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("LensGripper", "LensGripperOpen", ref errorCode);

                    if (GlobalParameters.systemconfig.SystemOperationConfig.grippersecuritydetection == true)
                    {
                        if (res == true)
                        {
                            //等待夹爪张开动作完成
                            while (pend == false)
                            {
                                GlobalFunction.IAIGripperTools.ElectricGripper_IsPositionMoveCompleted("LensGripper", ref pend, ref errorCode);
                            }
                            return true;
                        }
                    }
                    else
                    {
                        Thread.Sleep(GlobalParameters.systemconfig.InstrumentConfig.GripperMotionDelay);
                        if (res == true)
                        {
                            return true;
                        }
                    }
                    GlobalFunction.updateStatusDelegate("Lens夹爪张开失败", Enum_MachineStatus.ERROR);
                    return false;
                }
                else
                {
                    if (GlobalParameters.systemconfig.SystemOperationConfig.grippersecuritydetection == true)
                    {
                        uint OriginalMotorCurrent = 0;
                        uint MotorCurrent = 0;

                        //读取夹爪未闭合时夹爪驱动轴伺服电机电流值
                        res = GlobalFunction.IAIGripperTools.ElectricGripper_GetMotorCurrentAmpere("LensGripper", ref OriginalMotorCurrent, ref errorCode);
                        if (res == true)
                        {
                            res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("LensGripper", "LensGripperClose", ref errorCode);
                            if (res == true)
                            {
                                //等待夹爪闭合动作完成
                                while (pend == false)
                                {
                                    GlobalFunction.IAIGripperTools.ElectricGripper_IsPositionMoveCompleted("LensGripper", ref pend, ref errorCode);
                                }

                                //读取夹爪闭合后夹爪驱动轴伺服电机电流值
                                GlobalFunction.IAIGripperTools.ElectricGripper_GetMotorCurrentAmpere("LensGripper", ref MotorCurrent, ref errorCode);
                                if (Math.Abs(MotorCurrent - OriginalMotorCurrent) < 0.1)
                                {
                                    //如果电流无突变，说明夹爪未夹紧物料或者无物料，需要检查夹取位置参数项中是否配置有推压设置
                                    //推压设置后夹爪控制器会自动检测夹爪驱动轴伺服电机电流并输出PEND信号
                                    GlobalFunction.updateStatusDelegate("Lens夹取失败", Enum_MachineStatus.ERROR);
                                    return false;
                                }
                                return true;
                            }
                        }
                    }
                    else
                    {
                        res = GlobalFunction.IAIGripperTools.ElectricalGripper_PositionMove("LensGripper", "LensGripperClose", ref errorCode);
                        //延时等待夹爪闭合动作完成
                        Thread.Sleep(GlobalParameters.systemconfig.InstrumentConfig.GripperMotionDelay);
                        if (res == true)
                        {
                            return true;
                        }
                    }
                    GlobalFunction.updateStatusDelegate("Lens夹爪闭合失败", Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            GlobalFunction.updateStatusDelegate("Lens夹爪控制器未初始化，Lens夹爪运动失败", Enum_MachineStatus.ERROR);
            return false;
        }
        #endregion

        #region Box夹爪气缸上下运动控制
        public bool BoxGripperCylinderControl(bool Down)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.IOCard_InitialStatus == true)
            {
                if (Down == true)
                {
                    res = GlobalFunction.IOControlTools.BoxGripperUpDown(true, true);
                    if (res == false)
                    {
                        GlobalFunction.updateStatusDelegate("Box夹爪下降失败", Enum_MachineStatus.ERROR);
                        return false;
                    }
                    return true;
                }
                else
                {
                    res = GlobalFunction.IOControlTools.BoxGripperUpDown(false, true);
                    if (res == false)
                    {
                        GlobalFunction.updateStatusDelegate("Box夹爪上抬失败", Enum_MachineStatus.ERROR);
                        return false;
                    }
                    return true;
                }
            }

            GlobalFunction.updateStatusDelegate("IO卡未初始化，Box夹爪气缸运动失败", Enum_MachineStatus.ERROR);
            return false;
        }
        #endregion

        #region Lens墩料
        private bool PickLensReliably()
        {
            bool res = false;

            //墩料_Z2轴上抬1.1mm
            res = GlobalFunction.MotionTools.Motion_MoveZ2Distance(1.1, true);
            if (res == false)
            {
                return false;
            }

            //墩料_X2轴左移1.5mm
            res = GlobalFunction.MotionTools.Motion_MoveX2Distance(1.5, false, false, true);
            if (res == false)
            {
                return false;
            }

            //墩料_Z2轴下降1mm
            res = GlobalFunction.MotionTools.Motion_MoveZ2Distance(-1, true);
            if (res == false)
            {
                return false;
            }

            //墩料_Lens夹爪张开
            res = GlobalFunction.ProcessFlow.LensGripperControl(true);
            if (res == false)
            {
                return false;
            }
            GeneralFunction.Delay(200);

            //墩料_Lens夹爪闭合
            res = GlobalFunction.ProcessFlow.LensGripperControl(false);
            if (res == false)
            {
                return false;
            }

            //墩料_Z2轴下压0.1mm
            res = GlobalFunction.MotionTools.Motion_MoveZ2Distance(-0.1, true);
            if (res == false)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Lens夹爪移出产品Box返回到安全位
        public bool LensGripperOutOfBox()
        {
            bool res = false;
            double posx = GlobalParameters.productconfig.positionConfig.lensSafePosition.X2;
            double posy = GlobalParameters.productconfig.positionConfig.lensSafePosition.Y2;
            double posz = GlobalParameters.productconfig.positionConfig.lensSafePosition.Z2;

            //设置Z2轴速度百分比(一般放慢速度以保证安全，取夹爪贴装Lens时的速度)
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", GlobalParameters.productconfig.processConfig.pressLensSpeedPercent);

            //Lens夹爪上抬到移入产品Box时的安全高度            
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("Z2", posz, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪上抬到移入产品Box时的安全高度失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //恢复X2_Y2_Z2轴速度百分比
            GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", 100);
            GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y2", 100);
            GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", 100);

            //Lens夹爪X方向移动到安全位            
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("X2", posx, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪X方向移动到安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.flagassembly.lensgripperinboxflag = false;

            //Lens夹爪Y方向移动到安全位            
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("Y2", posy, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪Y方向移动到安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalFunction.updateStatusDelegate("Lens夹爪移出产品Box回安全位成功", Enum_MachineStatus.NORMAL);
            return res;
        }
        #endregion

        //----物料盘相关功能函数

        #region 根据Box索引号返回它在Box物料盘中所在的行列号
        public bool GetBoxLocationInTray(int index, ref int row, ref int col)
        {
            for (int i = 0; i < GlobalParameters.productconfig.boxlensConfig.boxTray.rowcount; i++)
            {
                for (int j = 0; j < GlobalParameters.productconfig.boxlensConfig.boxTray.colcount; j++)
                {
                    if (GlobalParameters.processdata.boxload[i, j].index == index)
                    {
                        row = i;
                        col = j;
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 根据Box物料盘中所在的行列号返回它的索引号
        public bool GetBoxIndexInTray(int row, int col, ref int index)
        {
            for (int i = 0; i < GlobalParameters.productconfig.boxlensConfig.boxTray.rowcount; i++)
            {
                for (int j = 0; j < GlobalParameters.productconfig.boxlensConfig.boxTray.colcount; j++)
                {
                    if ((i == row) && (j == col))
                    {
                        index = GlobalParameters.processdata.boxload[i, j].index;
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        #region 搜索一个当前Box料盘中已识别过符合要求的产品Box并返回它的索引号
        public bool SearchRecognizedBoxInTray(ref int currentBoxIndex)
        {
            currentBoxIndex = 0;
            for (int row = 0; row < GlobalParameters.productconfig.boxlensConfig.boxTray.rowcount; row++)
            {
                for (int col = 0; col < GlobalParameters.productconfig.boxlensConfig.boxTray.colcount; col++)
                {
                    if ((GlobalParameters.processdata.boxload[row, col].exists == true) && (GlobalParameters.processdata.boxload[row, col].bechecked = true))
                    {
                        GlobalFunction.ProcessFlow.GetBoxIndexInTray(row, col, ref currentBoxIndex);
                        GlobalFunction.updateStatusDelegate("产品Box物料盘中#" + currentBoxIndex.ToString() + "位置Box已识别过", Enum_MachineStatus.NORMAL);
                        return true;
                    }
                }
            }
            GlobalFunction.updateStatusDelegate("产品Box物料盘中已无合适的产品Box", Enum_MachineStatus.ERROR);
            return false;
        }
        #endregion

        #region 根据Lens索引号返回它在Lens物料盘中所在的行号
        public bool GetLensLocationInTray(int index, ref int row)
        {
            for (int i = 0; i < GlobalParameters.productconfig.boxlensConfig.lensTray.rowcount; i++)
            {
                if (GlobalParameters.processdata.lensload[i].index == index)
                {
                    row = i;
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 根据Lens在物料盘中所在的行号返回它的索引号
        public bool GetLensIndexInTray(int row, ref int index)
        {
            for (int i = 0; i < GlobalParameters.productconfig.boxlensConfig.lensTray.rowcount; i++)
            {
                if (i == row)
                {
                    index = GlobalParameters.processdata.lensload[i].index;
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 搜索一个当前Lens料盘中已识别过符合要求的Lens并返回它的索引号
        public bool SearchRecognizedLensInTray(ref int currentLensIndex)
        {
            currentLensIndex = 0;
            for (int row = 0; row < GlobalParameters.productconfig.boxlensConfig.lensTray.rowcount; row++)
            {
                if ((GlobalParameters.processdata.lensload[row].exists == true) && (GlobalParameters.processdata.lensload[row].bechecked = true))
                {
                    GlobalFunction.ProcessFlow.GetLensIndexInTray(row, ref currentLensIndex);
                    GlobalFunction.updateStatusDelegate("产品Lens物料盘中#" + currentLensIndex.ToString() + "位置Lens已识别过", Enum_MachineStatus.NORMAL);
                    return true;
                }
            }
            GlobalFunction.updateStatusDelegate("产品Lens物料盘中已无合适的Lens", Enum_MachineStatus.ERROR);
            return false;
        }
        #endregion

        //----UV相关功能函数

        #region UV枪气缸控制
        public bool UVCylinderControl(bool Out)
        {
            bool res = false;

            if (GlobalParameters.HardwareInitialStatus.IOCard_InitialStatus == true)
            {
                if (Out == true)
                {
                    res = GlobalFunction.IOControlTools.UVInOut(true, true);
                    if (res == false)
                    {
                        GlobalFunction.updateStatusDelegate("UV枪伸出失败", Enum_MachineStatus.ERROR);
                        return false;
                    }
                    return true;
                }
                else
                {
                    res = GlobalFunction.IOControlTools.UVInOut(false, true);
                    if (res == false)
                    {
                        GlobalFunction.updateStatusDelegate("UV枪收回失败", Enum_MachineStatus.ERROR);
                        return false;
                    }
                    return true;
                }
            }

            GlobalFunction.updateStatusDelegate("IO卡未初始化，UV气缸运动失败", Enum_MachineStatus.ERROR);
            return false;
        }
        #endregion  

        //----流程控制功能函数

        #region 获取指定单步骤的工作流程索引号
        public bool GetStepFlowIndexID(Enum_WorkFlowList stepflow, ref int IndexID)
        {
            for (int i = 0; i < GlobalParameters.productconfig.processConfig.workflowStepNum; i++)
            {
                IndexID = GlobalParameters.productconfig.processConfig.workflowArray[i];
                if ((IndexID != -1) && (IndexID == Convert.ToInt16(stepflow)))
                {
                    IndexID = i;
                    return true;
                }
            }

            IndexID = -1;
            return false;
        }
        #endregion

        #region 复位工作流程各运行中间标志
        public void ResetWorkFlowFlag()
        {
            GlobalParameters.flagassembly.poweronflag = false;
            GlobalParameters.flagassembly.epoxypininboxflag = false;
            GlobalParameters.flagassembly.lenstouchedepoxyflag = false;
            //GlobalParameters.flagassembly.lensongripperflag = false;
            GlobalParameters.flagassembly.boxongripperflag = false;
            GlobalParameters.flagassembly.lensgripperinboxflag = false;
            GlobalParameters.flagassembly.upcameraliveonflag = false;
            GlobalParameters.flagassembly.downcameraliveonflag = false;
            GlobalParameters.flagassembly.pressdownlensflag = false;
            GlobalParameters.flagassembly.stopflag = false;

            //初始化多线程运行状态标记及其执行结果
            GlobalParameters.flagassembly.epoxywipethreadaliveflag = false;
            GlobalParameters.flagassembly.epoxydipthreadaliveflag = false;
            GlobalParameters.flagassembly.picklensthreadaliveflag = false;
            GlobalParameters.processdata.epoxyWipeThreadResult = false;
            GlobalParameters.processdata.epoxyDipThreadResult = false;
            GlobalParameters.processdata.pickLensThreadResult = false;
        }
        #endregion

        #region 全自动模式下单个流程步骤运行异常处理
        public bool AutoRunSingleStepAbnormalHandling(Enum_WorkFlowList stepflow)
        {
            bool res = false;
            int row = 0;
            int col = 0;

            //根据Box索引号返回它在Box物料盘中所在的行列号
            GlobalFunction.ProcessFlow.GetBoxLocationInTray(GlobalParameters.processdata.currentBoxIndex, ref row, ref col);

            //MES拆料_发料_出站_HOLD功能调用只报错不强制中止流程
            if ((stepflow == Enum_WorkFlowList.AUTO_HOLD) || (stepflow == Enum_WorkFlowList.AUTO_MOVEOUT) || (stepflow == Enum_WorkFlowList.AUTO_REMOVE) || (stepflow == Enum_WorkFlowList.COMPONENT_ISSUE))
            {
                GlobalParameters.processdata.boxload[row, col].finalresult = Enum_FinalResult.MESFAIL;
                return true;
            }

            //标记产品最终耦合组装失败
            GlobalParameters.processdata.boxload[row, col].finalresult = Enum_FinalResult.FAIL;

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

            //如果Lens夹爪还在产品Box内部
            if (GlobalParameters.flagassembly.lensgripperinboxflag == true)
            {
                //Lens夹爪移出产品Box返回安全位
                res = GlobalFunction.ProcessFlow.LensGripperOutOfBox();
                if (res == false)
                {
                    return false;
                }
            }

            //如果Lens还在Lens夹爪上
            if (GlobalParameters.flagassembly.lensongripperflag == true)
            {
                //已粘胶的Lens需要执行抛料动作（注：没粘胶的Lens可以继续给下一个产品耦合组装使用）
                if (GlobalParameters.flagassembly.lenstouchedepoxyflag == true)
                {
                    res = GlobalFunction.ProcessFlow.StepFlow_DiscardLens();
                    if (res == false)
                    {
                        GlobalFunction.updateStatusDelegate("Lens抛料失败", Enum_MachineStatus.NORMAL);
                        return false;
                    }
                    GlobalFunction.updateStatusDelegate("Lens抛料成功", Enum_MachineStatus.NORMAL);
                }
            }

            //如果点胶针还在产品Box内部
            if (GlobalParameters.flagassembly.epoxypininboxflag == true)
            {
                //点胶针移出产品Box返回安全位
                res = GlobalFunction.ProcessFlow.DispenserEpoxyPinOutOfBox();
                if (res == false)
                {                    
                    return false;
                }
            }

            //如果产品Box还在Nest中
            if (GlobalParameters.flagassembly.boxinnestflag == true)
            {
                //产品下电
                res = GlobalFunction.ProcessFlow.StepFlow_PowerTurnOnOff(false);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("产品下电失败", Enum_MachineStatus.NORMAL);
                    return false;
                }
                GlobalFunction.updateStatusDelegate("产品下电成功", Enum_MachineStatus.NORMAL);

                //从Nest中取出产品Box
                res = GlobalFunction.ProcessFlow.StepFlow_PickBoxFromNest();
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("从Nest中取出产品Box失败", Enum_MachineStatus.NORMAL);
                    return false;
                }
                GlobalFunction.updateStatusDelegate("从Nest中取出产品Box成功", Enum_MachineStatus.NORMAL);

                //产品Box放回料盘
                res = GlobalFunction.ProcessFlow.StepFlow_PlaceBoxIntoUnloadTray(row, col);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("产品Box放回料盘失败", Enum_MachineStatus.NORMAL);
                    return false;
                }
                GlobalFunction.updateStatusDelegate("产品Box放回料盘成功", Enum_MachineStatus.NORMAL);
            }

            //如果产品Box还残留在产品Box夹爪上
            if (GlobalParameters.flagassembly.boxongripperflag == true)
            {
                res = GlobalFunction.ProcessFlow.StepFlow_PlaceBoxIntoUnloadTray(row, col);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("产品Box放回料盘失败", Enum_MachineStatus.NORMAL);
                    return false;
                }
                GlobalFunction.updateStatusDelegate("产品Box放回料盘成功", Enum_MachineStatus.NORMAL);
                GlobalParameters.flagassembly.boxongripperflag = false;
            }

            //对于耦合贴装前光斑Spec检查不合格的产品
            if ((stepflow == Enum_WorkFlowList.CHECKSPOT_BEFOREALIGN) || (GlobalParameters.processdata.checkSpotBeforeAlignResult == false))
            {
                //工艺参数上传数据库（注：供ME工程师查看分析）                
                if ((GlobalParameters.flagassembly.continueflag == true) && (GlobalParameters.productconfig.processConfig.processDataUploadIntoDatabase == true))
                {
                    GlobalParameters.processdata.endTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    GlobalFunction.ProcessFlow.StepFlow_UploadProcessDataIntoDatabase();
                }
            }

            return true;
        }
        #endregion

        //----单步骤工艺流程功能函数

        #region 上相机识别产品盒子

        //上相机识别Box料盘中指定位置的单个Box并返回结果
        public bool StepFlow_UpCameraViewSingleBoxInTray(int row, int col)
        {
            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;
            double offsetx = 0;
            double offsety = 0;
            string str = string.Empty;

            GlobalParameters.processdata.boxload[row, col].bechecked = false;

            //Z1轴上抬到安全位
            res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
            if (res == false)
            {
                return false;
            }

            //X2_Y2_Z2三轴联动到识别Box时的安全位
            posx = GlobalParameters.productconfig.positionConfig.boxSafePosition.X2;
            posy = GlobalParameters.productconfig.positionConfig.boxSafePosition.Y2;
            posz = GlobalParameters.productconfig.positionConfig.boxSafePosition.Z2;
            res = GlobalFunction.ProcessFlow.MotionAxisX2Y2Z2MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X2_Y2_Z2Box", Enum_MachineStatus.ERROR);
                return false;
            }

            //上相机移动至识别物料盘中Box位置
            posx = GlobalParameters.productconfig.positionConfig.UpCameraViewBoxInTrayPosition.X1 - GlobalParameters.productconfig.boxlensConfig.boxTray.colspace * col;
            posy = GlobalParameters.productconfig.positionConfig.UpCameraViewBoxInTrayPosition.Y1 - GlobalParameters.productconfig.boxlensConfig.boxTray.rowspace * row;
            posz = GlobalParameters.productconfig.positionConfig.UpCameraViewBoxInTrayPosition.Z1;
            res = GlobalFunction.ProcessFlow.MotionAxisX1Y1Z1MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("上相机移动至识别物料盘中产品Box位置失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //上相机识别物料盘中产品Box左上角位置(像素坐标)
            VisionResult.BoxResult BoxResult = new VisionResult.BoxResult();
            double UpRingValue = GlobalParameters.productconfig.boxlensConfig.UpCameraViewBoxUpRing;
            double UpSpotValue = GlobalParameters.productconfig.boxlensConfig.UpCameraViewBoxUpSpot;
            res = GlobalFunction.ProcessFlow.VisionRecognizeBoxInTray(UpRingValue, UpSpotValue, ref BoxResult);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("上相机识别物料盘中产品Box失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //计算产品Box左上角位置与上相机中心的偏移距离
            offsetx = (BoxResult.point.X - BoxResult.imagecenter.X) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
            offsety = (BoxResult.point.Y - BoxResult.imagecenter.Y) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.yscale;

            //计算出产品Box中心在龙门轴坐标系中X1_Y1轴定位坐标
            GlobalParameters.processdata.boxload[row, col].posx = posx - offsetx + GlobalParameters.productconfig.boxlensConfig.pickBoxOffsetX;
            GlobalParameters.processdata.boxload[row, col].posy = posy + offsety + GlobalParameters.productconfig.boxlensConfig.pickBoxOffsetY;

            GlobalParameters.processdata.boxload[row, col].bechecked = true;
            return true;
        }

        //上相机自动识别Box料盘中符合要求的Box并获取相关信息
        public bool StepFlow_UpCameraViewBoxInTray(bool recognizeAllBox)
        {
            bool res = false;
            bool boxReady = false;  //料盘中有符合识别要求的产品Box存在
            double posx = 0;
            double posy = 0;
            double posz = 0;
            double offsetx = 0;
            double offsety = 0;
            VisionResult.BoxResult BoxResult = new VisionResult.BoxResult();
            double UpRingValue = GlobalParameters.productconfig.boxlensConfig.UpCameraViewBoxUpRing;
            double UpSpotValue = GlobalParameters.productconfig.boxlensConfig.UpCameraViewBoxUpSpot;

            //Z1轴上抬到安全位
            res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
            if (res == false)
            {
                return false;
            }

            //X2_Y2_Z2三轴联动到识别Box时的安全位
            posx = GlobalParameters.productconfig.positionConfig.boxSafePosition.X2;
            posy = GlobalParameters.productconfig.positionConfig.boxSafePosition.Y2;
            posz = GlobalParameters.productconfig.positionConfig.boxSafePosition.Z2;
            res = GlobalFunction.ProcessFlow.MotionAxisX2Y2Z2MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到识别产品Box时的安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            for (int row = 0; row < GlobalParameters.productconfig.boxlensConfig.boxTray.rowcount; row++)
            {
                for (int col = 0; col < GlobalParameters.productconfig.boxlensConfig.boxTray.colcount; col++)
                {
                    if (GlobalParameters.processdata.boxload[row, col].exists == true)
                    {
                        GlobalParameters.processdata.boxload[row, col].bechecked = false;

                        //上相机移动至识别物料盘中Box位置
                        posx = GlobalParameters.productconfig.positionConfig.UpCameraViewBoxInTrayPosition.X1 - GlobalParameters.productconfig.boxlensConfig.boxTray.colspace * col;
                        posy = GlobalParameters.productconfig.positionConfig.UpCameraViewBoxInTrayPosition.Y1 - GlobalParameters.productconfig.boxlensConfig.boxTray.rowspace * row;
                        posz = GlobalParameters.productconfig.positionConfig.UpCameraViewBoxInTrayPosition.Z1;
                        res = GlobalFunction.ProcessFlow.MotionAxisX1Y1Z1MoveToLocation(posx, posy, posz, false, false, true);
                        if (res == false)
                        {
                            GlobalFunction.updateStatusDelegate("上相机移动至识别物料盘中产品Box位置失败", Enum_MachineStatus.ERROR);
                            return false;
                        }

                        //上相机识别物料盘中产品Box左上角位置(像素坐标)
                        res = GlobalFunction.ProcessFlow.VisionRecognizeBoxInTray(UpRingValue, UpSpotValue, ref BoxResult);
                        if (res == true)
                        {
                            //如果识别成功
                            GlobalFunction.ProcessFlow.GetBoxIndexInTray(row, col, ref GlobalParameters.processdata.currentBoxIndex);
                            GlobalParameters.processdata.boxload[row, col].bechecked = true;

                            //计算产品Box左上角位置与上相机中心的偏移距离
                            offsetx = (BoxResult.point.X - BoxResult.imagecenter.X) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
                            offsety = (BoxResult.point.Y - BoxResult.imagecenter.Y) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.yscale;

                            //计算出产品Box中心在龙门轴坐标系中X1_Y1轴定位坐标
                            GlobalParameters.processdata.boxload[row, col].posx = posx - offsetx + GlobalParameters.productconfig.boxlensConfig.pickBoxOffsetX;
                            GlobalParameters.processdata.boxload[row, col].posy = posy + offsety + GlobalParameters.productconfig.boxlensConfig.pickBoxOffsetY;

                            //标识料盘中已有符合要求的产品Box存在
                            boxReady = true;

                            //检查产品Box识别模式
                            //if (GlobalParameters.productconfig.boxlensConfig.UpCameraViewAllBox == false)
                            if (recognizeAllBox == false)
                            {
                                //直接返回当前首个识别成功的产品Box信息，不再继续识别其他产品Box
                                return true;
                            }
                        }
                        else
                        {
                            //如果识别失败，则主动放弃当前Box并刷新Box料盘中对应穴位物料状态
                            GlobalFunction.updateBoxLoadTrayStatusDelegate(row, col, false);
                        }
                    }
                }
            }

            if (boxReady == false)
            {
                GlobalFunction.updateStatusDelegate("上相机无法识别到物料盘中合适的产品Box", Enum_MachineStatus.ERROR);
            }
            return boxReady;
        }

        #endregion

        #region 上相机识别Lens

        //上相机识别Lens料盘中指定位置的单个Lens并返回结果
        public bool StepFlow_UpCameraViewSingleLensInTray(int row)
        {
            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;
            double offsetx = 0;
            double offsety = 0;
            string str = string.Empty;

            GlobalParameters.processdata.lensload[row].bechecked = false;

            //Z1轴上抬到安全位
            res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
            if (res == false)
            {
                return false;
            }

            //X2_Y2_Z2三轴联动到识别Box时的安全位以避免碰撞上相机
            posx = GlobalParameters.productconfig.positionConfig.boxSafePosition.X2;
            posy = GlobalParameters.productconfig.positionConfig.boxSafePosition.Y2;
            posz = GlobalParameters.productconfig.positionConfig.boxSafePosition.Z2;
            res = GlobalFunction.ProcessFlow.MotionAxisX2Y2Z2MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到识别Lens时的安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //上相机移动至识别物料盘中Lens位置
            posx = GlobalParameters.productconfig.positionConfig.UpCameraViewLensInTrayPosition.X1;
            posy = GlobalParameters.productconfig.positionConfig.UpCameraViewLensInTrayPosition.Y1 - GlobalParameters.productconfig.boxlensConfig.lensTray.rowspace * row;
            posz = GlobalParameters.productconfig.positionConfig.UpCameraViewLensInTrayPosition.Z1;
            res = GlobalFunction.ProcessFlow.MotionAxisX1Y1Z1MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("上相机移动至识别物料盘中Lens位置失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //上相机识别物料盘中Lens中心位置(像素坐标)
            VisionResult.LensResult LensResult = new VisionResult.LensResult();
            double UpRingValue = GlobalParameters.productconfig.boxlensConfig.UpCameraViewLensUpRing;
            double UpSpotValue = GlobalParameters.productconfig.boxlensConfig.UpCameraViewLensUpSpot;
            res = GlobalFunction.ProcessFlow.VisionRecognizeLensInTray(UpRingValue, UpSpotValue, ref LensResult);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("上相机识别物料盘中Lens失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //计算Lens中心与上相机中心的偏移距离
            offsetx = (LensResult.center.X - LensResult.imagecenter.X) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
            offsety = (LensResult.center.Y - LensResult.imagecenter.Y) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.yscale;

            //计算出Lens中心在龙门轴坐标系中X1_Y1轴定位坐标
            GlobalParameters.processdata.lensload[row].posx = posx - offsetx;
            GlobalParameters.processdata.lensload[row].posy = posy + offsety;

            GlobalParameters.processdata.lensload[row].bechecked = true;
            return true;
        }

        //上相机自动识别Lens料盘中符合要求的lens并获取相关信息
        public bool StepFlow_UpCameraViewLensInTray(bool recognizeAllLens)
        {
            bool res = false;
            bool lensReady = false;  //料盘中有识别符合要求的Lens存在
            double posx = 0;
            double posy = 0;
            double posz = 0;
            double offsetx = 0;
            double offsety = 0;
            VisionResult.LensResult LensResult = new VisionResult.LensResult();
            double UpRingValue = GlobalParameters.productconfig.boxlensConfig.UpCameraViewLensUpRing;
            double UpSpotValue = GlobalParameters.productconfig.boxlensConfig.UpCameraViewLensUpSpot;

            //Z1轴上抬到安全位
            res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
            if (res == false)
            {
                return false;
            }

            //X2_Y2_Z2三轴联动到识别Box时的安全位以避免碰撞上相机
            posx = GlobalParameters.productconfig.positionConfig.boxSafePosition.X2;
            posy = GlobalParameters.productconfig.positionConfig.boxSafePosition.Y2;
            posz = GlobalParameters.productconfig.positionConfig.boxSafePosition.Z2;
            res = GlobalFunction.ProcessFlow.MotionAxisX2Y2Z2MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到识别Lens时的安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            for (int row = 0; row < GlobalParameters.productconfig.boxlensConfig.lensTray.rowcount; row++)
            {
                if (GlobalParameters.processdata.lensload[row].exists == true)
                {
                    GlobalParameters.processdata.lensload[row].bechecked = false;

                    //上相机移动至识别物料盘中Lens位置
                    posx = GlobalParameters.productconfig.positionConfig.UpCameraViewLensInTrayPosition.X1;
                    posy = GlobalParameters.productconfig.positionConfig.UpCameraViewLensInTrayPosition.Y1 - GlobalParameters.productconfig.boxlensConfig.lensTray.rowspace * row;
                    posz = GlobalParameters.productconfig.positionConfig.UpCameraViewLensInTrayPosition.Z1;
                    res = GlobalFunction.ProcessFlow.MotionAxisX1Y1Z1MoveToLocation(posx, posy, posz, false, false, true);
                    if (res == false)
                    {
                        GlobalFunction.updateStatusDelegate("上相机移动至识别物料盘中Lens位置失败", Enum_MachineStatus.ERROR);
                        return false;
                    }

                    //上相机识别物料盘中Lens中心位置(像素坐标)
                    res = GlobalFunction.ProcessFlow.VisionRecognizeLensInTray(UpRingValue, UpSpotValue, ref LensResult);
                    if (res == true)
                    {
                        //如果识别成功
                        GlobalParameters.processdata.lensload[row].bechecked = true;
                        GlobalFunction.ProcessFlow.GetLensIndexInTray(row, ref GlobalParameters.processdata.currentLensIndex);

                        //计算Lens中心与上相机中心的偏移距离
                        offsetx = (LensResult.center.X - LensResult.imagecenter.X) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
                        offsety = (LensResult.center.Y - LensResult.imagecenter.Y) * GlobalParameters.systemconfig.UpCameraCalibrateConfig.yscale;

                        //计算出Lens中心在龙门轴坐标系中X1_Y1轴定位坐标
                        GlobalParameters.processdata.lensload[row].posx = posx - offsetx;
                        GlobalParameters.processdata.lensload[row].posy = posy + offsety;

                        //料盘中有符合要求的Lens存在
                        lensReady = true;

                        //检查Lens识别模式
                        if (recognizeAllLens == false)
                        {
                            //直接返回当前首个识别成功的Lens信息，不再继续识别其他Lens
                            return true;
                        }
                    }
                    else
                    {
                        //如果识别失败，则主动放弃当前Lens并刷新Lens料盘中对应穴位物料状态
                        GlobalFunction.updateLensLoadTrayStatusDelegate(row, false);
                    }
                }
            }

            if (lensReady == false)
            {
                GlobalFunction.updateStatusDelegate("上相机无法识别到物料盘中合适的Lens", Enum_MachineStatus.ERROR);
            }
            return lensReady;
        }

        #endregion

        #region 从料盘夹取产品盒子
        public bool StepFlow_PickBoxFromBoxTray(int row, int col)
        {
            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;

            GlobalParameters.flagassembly.boxongripperflag = false;

            //单步骤模式下Z1轴上抬到安全位（防止撞机事件发生）
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
                if (res == false)
                {
                    return false;
                }
            }

            //单步骤模式下X2_Y2_Z2三轴联动到夹取Box时的安全位（防止撞机事件发生）
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                posx = GlobalParameters.productconfig.positionConfig.boxSafePosition.X2;
                posy = GlobalParameters.productconfig.positionConfig.boxSafePosition.Y2;
                posz = GlobalParameters.productconfig.positionConfig.boxSafePosition.Z2;
                res = GlobalFunction.ProcessFlow.MotionAxisX2Y2Z2MoveToLocation(posx, posy, posz, true, true, true);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到夹取产品Box时的安全位失败", Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            //Box夹爪张开
            res = GlobalFunction.ProcessFlow.BoxGripperControl(true);
            if (res == false)
            {
                return false;
            }

            //产品Box夹爪运动至产品Box夹取位
            posx = GlobalParameters.processdata.boxload[row, col].posx + GlobalParameters.systemconfig.BoxGripperConfig.offsetx;
            posy = GlobalParameters.processdata.boxload[row, col].posy + GlobalParameters.systemconfig.BoxGripperConfig.offsety;
            posz = GlobalParameters.productconfig.positionConfig.pickBoxZ1Height;
            res = GlobalFunction.ProcessFlow.MotionAxisX1Y1Z1MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Box夹爪运动至产品Box夹取位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Box夹爪气缸下降
            res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(true);
            if (res == false)
            {
                return false;
            }
            GeneralFunction.Delay(1000);

            //Box夹爪闭合
            res = GlobalFunction.ProcessFlow.BoxGripperControl(false);
            if (res == false)
            {
                return false;
            }
            GlobalParameters.flagassembly.boxongripperflag = true;

            //Box夹爪气缸上抬
            res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(false);
            if (res == false)
            {
                return false;
            }

            //Z1轴上抬到安全位
            res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
            if (res == false)
            {
                return false;
            }

            //刷新产品Box发料盘中对应穴位物料状态
            GlobalFunction.updateBoxLoadTrayStatusDelegate(row, col, false);
            GlobalFunction.ProcessFlow.GetBoxIndexInTray(row, col, ref GlobalParameters.processdata.currentBoxIndex);

            return true;
        }
        #endregion

        #region 产品盒子搬移到扫码器
        public bool StepFlow_BoxToQRCodeScanPosition()
        {
            //强制将产品Box夹爪气缸和胶针气缸安全上抬，防止撞机事件发生
            bool res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();

            //产品Box移动至扫码位置（附带将Z1轴上抬到安全位）
            if (res == true)
            {
                double posx1 = GlobalParameters.productconfig.positionConfig.boxScanPosition.X1;
                double posy1 = GlobalParameters.productconfig.positionConfig.boxScanPosition.Y1;
                res = GlobalFunction.MotionTools.Motion_MoveX1Y1ToLocation(posx1, posy1, true, true);
            }
            return res;
        }
        #endregion

        #region 获取产品SN信息
        public bool StepFlow_GetProductSN(ref string ProductSN)
        {
            bool res = false;
            int retErrorCode = 0;
            ProductSN = string.Empty;

            //清空主界面产品SN信息框
            GlobalFunction.updateProdutSNDelegate(ProductSN);

            if (GlobalParameters.productconfig.processConfig.readSN == true)
            {
                //----通信方式读取产品内部寄存器获取SN信息
                if ((GlobalParameters.systemconfig.InstrumentConfig.FI2CUSB_Valid == false) || (GlobalParameters.HardwareInitialStatus.FI2CUSB_InitialStatus == false))
                {
                    GlobalFunction.updateStatusDelegate("FI2CUSB加电模块未配置或未初始化成功", Enum_MachineStatus.ERROR);
                    return false;
                }
                res = GlobalFunction.FI2CUSBTools.FI2CUSB_ReadProductSN(GlobalParameters.systemconfig.ManageProductConfig.currentproduct, ref ProductSN);
            }
            else
            {
                //----扫码器获取SN信息
                if ((GlobalParameters.systemconfig.InstrumentConfig.QRCodeScanner_Valid == false) || (GlobalParameters.HardwareInitialStatus.QRCodeScanner_InitialStatus == false))
                {
                    GlobalFunction.updateStatusDelegate("扫码器未配置或未初始化成功", Enum_MachineStatus.ERROR);
                    return false;
                }

                //Z1轴下降到扫码高度
                double posz1 = GlobalParameters.productconfig.positionConfig.boxScanPosition.Z1;
                res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz1, true);
                if (res == false)
                {
                    return false;
                }

                //夹爪气缸下降
                res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(true);
                if (res == false)
                {
                    return false;
                }

                //扫码器扫码获取SN信息
                bool scanResult = false;
                for (int i = 0; i < 3; i++)
                {
                    res = GlobalFunction.QRCodeScannerTools.QRCodeScanner_ScanCode("DM70S", ref ProductSN, ref retErrorCode);
                    if (res == true)
                    {
                        if ((ProductSN != "") && (ProductSN != "ErrorCode") && (ProductSN != "ERRORCODE"))
                        {
                            scanResult = true;
                            break;
                        }
                    }
                    GeneralFunction.Delay(200);
                }
                if (scanResult == false)
                {
                    ProductSN = "";
                }

                //夹爪气缸上抬
                res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(false);
                if (res == false)
                {
                    return false;
                }

                //Z1轴上抬到安全位
                res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
                if (res == false)
                {
                    return false;
                }
            }

            if (ProductSN == "")
            {
                res = false;
            }
            else
            {
                //刷新主界面产品SN信息框
                ProductSN = ProductSN.ToUpper().Trim();
                GlobalFunction.updateProdutSNDelegate(ProductSN);
                GlobalFunction.updateStatusDelegate("产品SN:" + ProductSN, Enum_MachineStatus.NORMAL);

                //检查产品SN是否在当前站点
                if ((GlobalParameters.productconfig.processConfig.mesEnable == true) && (GlobalParameters.productconfig.processConfig.checkSN == true))
                {
                    res = GlobalFunction.ProcessFlow.StepFlow_mesCheckSN(ProductSN);
                }
            }

            return res;
        }
        #endregion

        #region 产品盒子放入Nest
        public bool StepFlow_PlaceBoxIntoNest()
        {
            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;

            //单步骤模式下Z1轴上抬到安全位（防止撞机事件发生）
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
                if (res == false)
                {
                    return false;
                }
            }

            //单步骤模式下X2_Y2_Z2三轴联动到夹取Box时的安全位（防止撞机事件发生）
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                posx = GlobalParameters.productconfig.positionConfig.boxSafePosition.X2;
                posy = GlobalParameters.productconfig.positionConfig.boxSafePosition.Y2;
                posz = GlobalParameters.productconfig.positionConfig.boxSafePosition.Z2;
                res = GlobalFunction.ProcessFlow.MotionAxisX2Y2Z2MoveToLocation(posx, posy, posz, true, true, true);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到夹取Box时的安全位失败", Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            //产品Box搬移到Nest安全位
            posx = GlobalParameters.productconfig.positionConfig.boxInNestPosition.X1;
            posy = GlobalParameters.productconfig.positionConfig.boxInNestPosition.Y1;
            posz = GlobalParameters.productconfig.positionConfig.boxInNestPosition.Z1;
            res = GlobalFunction.ProcessFlow.MotionAxisX1Y1Z1MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("产品Box搬移到Nest安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Box夹爪气缸下降
            res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(true);
            if (res == false)
            {
                return false;
            }
            GeneralFunction.Delay(1000);

            //临时改变X1轴运动速度
            double speedPercent = GlobalParameters.productconfig.positionConfig.boxIntoNestSpeedPercent;
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", speedPercent);
            if (res == false)
            {
                return false;
            }

            //X1轴减速移动到产品Box在Nest中最终位置前方0.1mm位置（防止产品直接碰到Nest壁产生倾斜）
            posx = GlobalParameters.productconfig.positionConfig.pickBoxFromNestPosition.X1 + 0.1;
            res = GlobalFunction.MotionTools.Motion_MoveX1ToLocation(posx, true);
            if (res == false)
            {
                //恢复X1轴运动速度
                GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", 100);
                return false;
            }

            //恢复X1轴运动速度
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", 100);
            if (res == false)
            {
                return false;
            }

            //松开Box夹爪
            res = GlobalFunction.ProcessFlow.BoxGripperControl(true);
            if (res == false)
            {
                return false;
            }
            GlobalParameters.flagassembly.boxongripperflag = false;
            GlobalParameters.flagassembly.boxinnestflag = true;

            //Box夹爪气缸上抬
            res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(false);
            if (res == false)
            {
                return false;
            }

            //Z1轴上抬到安全位
            res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();

            return res;
        }
        #endregion

        #region 从料盘夹取Lens
        public bool StepFlow_PickLensFromLensTray(int row)
        {
            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;

            GlobalParameters.flagassembly.lensongripperflag = false;

            //单步骤模式下Z1轴上抬到安全位（防止撞机事件发生）
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
                if (res == false)
                {
                    return false;
                }
            }

            //Lens夹爪张开
            res = GlobalFunction.ProcessFlow.LensGripperControl(true);
            if (res == false)
            {
                return false;
            }

            //Lens夹爪运动至Lens夹取位
            posx = GlobalParameters.systemconfig.LensGripperConfig.offsetx - GlobalParameters.processdata.lensload[row].posx;
            posy = GlobalParameters.systemconfig.LensGripperConfig.offsety - GlobalParameters.processdata.lensload[row].posy;
            posz = GlobalParameters.productconfig.positionConfig.pickLensZ2Height;
            res = GlobalFunction.ProcessFlow.MotionAxisY2Z2MoveToLocation(posy, posz, true, false, true);
            if (res == true)
            {
                res = GlobalFunction.MotionTools.Motion_MoveX2ToLocation(posx, true);
            }
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪运动至Lens夹取位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Lens夹爪闭合
            res = GlobalFunction.ProcessFlow.LensGripperControl(false);
            if (res == false)
            {
                return false;
            }

            //标记夹爪上已有未沾胶的干净Lens存在
            GlobalParameters.flagassembly.lensongripperflag = true;
            GlobalParameters.flagassembly.lenstouchedepoxyflag = false;

            //刷新Lens料盘中对应穴位物料状态
            GlobalFunction.updateLensLoadTrayStatusDelegate(row, false);
            GlobalFunction.ProcessFlow.GetLensIndexInTray(row, ref GlobalParameters.processdata.currentLensIndex);

            //Lens墩料
            res = PickLensReliably();
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("夹取Lens后墩料操作失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //X2_Y2_Z2三轴联动到夹取Lens时的安全位
            posx = GlobalParameters.productconfig.positionConfig.lensSafePosition.X2;
            posy = GlobalParameters.productconfig.positionConfig.lensSafePosition.Y2;
            posz = GlobalParameters.productconfig.positionConfig.lensSafePosition.Z2;
            res = GlobalFunction.ProcessFlow.MotionAxisX2Y2Z2MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到夹取Lens时的安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            return true;
        }
        #endregion

        #region 产品加电_下电
        public bool StepFlow_PowerTurnOnOff(bool On)
        {
            bool res = false;
            string str = string.Empty;

            //获取当前产品加电Pin针作用方式
            Enum_PogoPinPosition PogoPinPosition = GlobalParameters.productconfig.processConfig.pogopinPosition;

            //获取当前产品加电方式
            string PowerWay = GlobalParameters.productconfig.processConfig.powerway.ToString();

            if (On == true)
            {                
                if (GlobalParameters.HardwareInitialStatus.Keithley2401_1_InitialStatus == true)
                {
                    //合上Pin针前先强制关闭源表输出
                    res = GlobalFunction.SourceMetertools.Keilthly240x_TurnOnKeithley("Keithley2401-1", false);
                    GeneralFunction.Delay(100);
                }

                if (PogoPinPosition == Enum_PogoPinPosition.PCBA_PIN)
                {
                    //PCBA类产品盒子锁紧
                    res = GlobalFunction.IOControlTools.NestClampPCBA(true);
                }
                else
                {
                    //金属壳体类产品盒子锁紧
                    res = GlobalFunction.IOControlTools.NestClampBox(true);
                }
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("产品Box锁紧失败", Enum_MachineStatus.ERROR);
                    return false;
                }

                res = GlobalFunction.IOControlTools.NestClampPogoPin(true);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("加电Pin针抱合失败", Enum_MachineStatus.ERROR);
                    return false;
                }
                else
                {
                    //记录加电Pin针累计使用次数
                    GlobalParameters.productconfig.processConfig.pogopinUsedCount++;
                    string msgStr = "";
                    string filepath = GeneralFunction.GetProductConfigFilePath(GlobalParameters.systemconfig.ManageProductConfig.currentproduct + ".xml");
                    string[] elementStr = new string[3];
                    elementStr[0] = "processConfig";
                    elementStr[1] = "pogopinUsedCount";
                    elementStr[2] = "";                    
                    GeneralFunction.UpdateXmlFileElement(filepath, elementStr, GlobalParameters.productconfig.processConfig.pogopinUsedCount.ToString(), ref msgStr);
                    if (GlobalParameters.productconfig.processConfig.pogopinUsedCount > GlobalParameters.productconfig.processConfig.pogopinLifetime)
                    {
                        GlobalFunction.updateStatusDelegate("加电Pin针使用次数：" + GlobalParameters.productconfig.processConfig.pogopinUsedCount.ToString() +
                            "，已超过使用寿命次数：" + GlobalParameters.productconfig.processConfig.pogopinLifetime.ToString() + "，请及时更换", Enum_MachineStatus.ERROR);
                    }
                    else
                    {
                        GlobalFunction.updateStatusDelegate("加电Pin针使用次数：" + GlobalParameters.productconfig.processConfig.pogopinUsedCount.ToString() +
                                                    "，使用寿命次数：" + GlobalParameters.productconfig.processConfig.pogopinLifetime.ToString(), Enum_MachineStatus.NORMAL);
                    }
                }

                if (GlobalParameters.productconfig.processConfig.alignmentWay != Enum_AlignmentWay.WINDOW_LENS)
                {
                    if (GlobalParameters.HardwareInitialStatus.Keithley2401_1_InitialStatus == true)
                    {
                        if (GlobalParameters.productconfig.processConfig.rampPowerOnEnable == true)
                        {
                            RampPara ramppara = new RampPara();
                            ramppara.startval = 0;
                            ramppara.endval = GlobalParameters.productconfig.processConfig.voltageval;
                            ramppara.interval = GlobalParameters.productconfig.processConfig.rampDelay;
                            ramppara.step = GlobalParameters.productconfig.processConfig.voltageval / GlobalParameters.productconfig.processConfig.rampSteps;
                            res = GlobalFunction.SourceMetertools.Keilthly240x_TurnOnKeithleyRamp("Keithley2401-1", SourceFuncType.VOLTAGE, true, ramppara);
                        }
                        else
                        {
                            //设置好输出电压值
                            res = GlobalFunction.SourceMetertools.Keilthly240x_SetVoltageValue("Keithley2401-1", GlobalParameters.productconfig.processConfig.voltageval);

                            if (PowerWay == "CURRENT_NO")
                            {
                                //针对电流源加电方式：设置好输出电流值后不立即打开源表输出，等到后续执行LaserOn操作时再打开源表输出
                                res = GlobalFunction.SourceMetertools.Keilthly240x_SetCurrentValue("Keithley2401-1", GlobalParameters.productconfig.processConfig.currentval);
                            }
                            else
                            {
                                //打开源表输出
                                res = GlobalFunction.SourceMetertools.Keilthly240x_TurnOnKeithley("Keithley2401-1", true);
                            }
                        }
                    }
                    else
                    {
                        GlobalFunction.updateStatusDelegate("Keithley2401_1 源表未初始化", Enum_MachineStatus.ERROR);
                        return false;
                    }
                }                
            }
            else
            {
                if (GlobalParameters.productconfig.processConfig.alignmentWay != Enum_AlignmentWay.WINDOW_LENS)
                {
                    if (GlobalParameters.HardwareInitialStatus.Keithley2401_1_InitialStatus == true)
                    {
                        //关闭源表输出
                        GlobalFunction.SourceMetertools.Keilthly240x_TurnOnKeithley("Keithley2401-1", false);
                        GeneralFunction.Delay(100);
                    }
                    else
                    {
                        GlobalFunction.updateStatusDelegate("Keithley2401_1 源表未初始化", Enum_MachineStatus.ERROR);
                        return false;
                    }
                }

                res = GlobalFunction.IOControlTools.NestClampPogoPin(false);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("加电Pin针松开失败", Enum_MachineStatus.ERROR);
                    return false;
                }

                if (PogoPinPosition == Enum_PogoPinPosition.PCBA_PIN)
                {
                    //PCBA类产品盒子松开
                    res = GlobalFunction.IOControlTools.NestClampPCBA(false);
                }
                else
                {
                    //金属壳体类产品盒子松开
                    res = GlobalFunction.IOControlTools.NestClampBox(false);
                }
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("产品Box松开失败", Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            if (GlobalParameters.productconfig.processConfig.alignmentWay != Enum_AlignmentWay.WINDOW_LENS)
            {
                //更新主界面加电源表读数显示
                if (PowerWay != "CURRENT_NO")
                {
                    GlobalFunction.updateSourceMeterVoltageReadingDelegate();
                    GlobalFunction.updateSourceMeterCurrentReadingDelegate();
                }
            }

            GlobalParameters.flagassembly.poweronflag = On;
            return true;
        }
        #endregion

        #region 下相机识别盒子窗口
        public bool StepFlow_DownCameraRecognizeBoxWindow()
        {
            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            //动态调整下相机工作条件
            bool res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.Window);
            if (res == false)
            {
                return false;
            }

            VisionResult.BoxResult WindowResult = new VisionResult.BoxResult();
            double DnSpotValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewBoxWindowDnSpot;
            double DnRingValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewBoxWindowDnRing;
            res = GlobalFunction.ProcessFlow.VisionRecognizeBoxWindow(DnRingValue, DnSpotValue, ref WindowResult);
            if (res == false)
            {
                return false;
            }

            //计算出Box窗口直径
            GlobalParameters.processdata.boxWindowRecognize.diameter = WindowResult.diameter * GlobalParameters.systemconfig.DownCameraCalibrateConfig.xscale;
            GlobalFunction.updateStatusDelegate("产品Box窗口直径: " + GlobalParameters.processdata.boxWindowRecognize.diameter.ToString("f3") + "mm", Enum_MachineStatus.NORMAL);

            //计算Box窗口中心与下相机中心的偏移距离
            double centeroffsetx = (WindowResult.point.X - WindowResult.imagecenter.X) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.xscale;
            double centeroffsety = (WindowResult.point.Y - WindowResult.imagecenter.Y) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.yscale;

            //计算Box窗口中心在龙门轴系统中的坐标
            GlobalParameters.processdata.boxWindowRecognize.windowcenterposx1 = GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.X1 + centeroffsetx;
            GlobalParameters.processdata.boxWindowRecognize.windowcenterposy1 = GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.Y1 + centeroffsety;

            //换算Box窗口中心在步进轴系统中的坐标
            GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2 = GlobalParameters.systemconfig.LensGripperConfig.offsetx - (GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.X1 - centeroffsetx);
            GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2 = GlobalParameters.systemconfig.LensGripperConfig.offsety - (GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.Y1 + centeroffsety);
            GlobalFunction.updateStatusDelegate("产品Box窗口中心位置: X2 = " + GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2.ToString("f3") + " Y2 = " + GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2.ToString("f3"), Enum_MachineStatus.NORMAL);

            //换算出点胶针进入产品Box时的定位坐标：Box窗口中心坐标 + 胶针与上相机中心偏差距离 + Offset设置
            GlobalParameters.processdata.boxWindowRecognize.epoxydipboxwindowposx1 = GlobalParameters.processdata.boxWindowRecognize.windowcenterposx1
                                                                                    + GlobalParameters.systemconfig.DispenserConfig.offsetx
                                                                                    + GlobalParameters.productconfig.positionConfig.dispenserToBoxOffsetX;
            GlobalParameters.processdata.boxWindowRecognize.epoxydipboxwindowposy1 = GlobalParameters.processdata.boxWindowRecognize.windowcenterposy1
                                                                                    + GlobalParameters.systemconfig.DispenserConfig.offsety
                                                                                    + GlobalParameters.productconfig.positionConfig.dispenserToBoxOffsetY;
            GlobalFunction.updateStatusDelegate("产品Box窗口点胶位置: X1 = " + GlobalParameters.processdata.boxWindowRecognize.epoxydipboxwindowposx1.ToString("f3") + " Y1 = " + GlobalParameters.processdata.boxWindowRecognize.epoxydipboxwindowposy1.ToString("f3"), Enum_MachineStatus.NORMAL);

            //换算出Lens夹爪第一次进入产品Box内部时的初始坐标：
            GlobalParameters.processdata.boxWindowRecognize.lensgripperintoboxposx2 = GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2
                                                                                    + GlobalParameters.productconfig.positionConfig.step1LensPlaceOffsetX;
            GlobalParameters.processdata.boxWindowRecognize.lensgripperintoboxposy2 = GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2
                                                                                    + GlobalParameters.productconfig.positionConfig.step1LensPlaceOffsetY;
            GlobalFunction.updateStatusDelegate("Lens移入盒子初始位置: X2 = " + GlobalParameters.processdata.boxWindowRecognize.lensgripperintoboxposx2.ToString("f3") + " Y2 = " + GlobalParameters.processdata.boxWindowRecognize.lensgripperintoboxposy2.ToString("f3"), Enum_MachineStatus.NORMAL);

            return true;
        }
        #endregion

        #region 获取光斑位置
        public bool StepFlow_DownCameraRecognizeLaserSpot()
        {
            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            //动态调整下相机工作条件
            bool res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.LaserSpot);
            if (res == false)
            {
                return false;
            }

            for (int i = 0; i < GlobalParameters.productconfig.processConfig.channelCount; i++)
            {
                //打开当前通道激光器
                res = GlobalFunction.ProcessFlow.LaserTurnOnOff(i, true);
                if (res == true)
                {
                    //识别当前通道光斑中心坐标
                    res = GlobalFunction.ProcessFlow.VisionRecognizeChannelLaserSpot(i);
                    if (res == true)
                    {
                        //关闭当前通道激光器
                        res = GlobalFunction.ProcessFlow.LaserTurnOnOff(i, false);
                        GeneralFunction.Delay(200);
                        if (res == false)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        //关闭当前通道激光器
                        res = GlobalFunction.ProcessFlow.LaserTurnOnOff(i, false);
                        GeneralFunction.Delay(200);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region 耦合前光斑确认
        public bool StepFlow_CheckSpecBeforeAlign()
        {
            double posx2 = 0;
            double posy2 = 0;
            double posx2_offset = 0;
            double posy2_offset = 0;

            switch (GlobalParameters.productconfig.processConfig.alignmentWay)
            {
                case Enum_AlignmentWay.WINDOW_LENS:
                    //这种方式下直接将Lens中心和产品Box窗口中心对齐(适用于ROSA类产品)
                    GlobalParameters.processdata.lensCenterAttachPosition.posx2 = GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2;
                    GlobalParameters.processdata.lensCenterAttachPosition.posy2 = GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2;
                    GlobalParameters.processdata.lensCenterAttachOffsetx = 0;
                    GlobalParameters.processdata.lensCenterAttachOffsety = 0;
                    break;

                case Enum_AlignmentWay.LASER_LENS:
                    //这种方式下Lens中心将对准多通道光斑平衡并做产品窗口中心对位补偿后的位置(适用于多通道TOSA类产品)

                    switch (GlobalParameters.productconfig.processConfig.laserSpotPosBalanceMode)
                    {
                        case Enum_LaserSpotPosBalanceMode.MEAN: //选用多通道光斑位置的平均值

                            for (int i = 0; i < GlobalParameters.productconfig.processConfig.channelCount; i++)
                            {
                                posx2 = posx2 + GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posx2;
                                posy2 = posy2 + GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posy2;
                            }
                            //计算坐标平均值作为多通道光斑平衡位置
                            posx2 = posx2 / GlobalParameters.productconfig.processConfig.channelCount;
                            posy2 = posy2 / GlobalParameters.productconfig.processConfig.channelCount;
                            break;

                        case Enum_LaserSpotPosBalanceMode.MIDDLE:   //选用多通道光斑位置的中间值
                            //确定多通道光斑X方向最大值和最小值
                            double posx_max = GlobalParameters.processdata.beforelenslaserSpotRecognize[0].posx2;
                            double posx_min = GlobalParameters.processdata.beforelenslaserSpotRecognize[0].posx2;
                            for (int i = 0; i < GlobalParameters.productconfig.processConfig.channelCount; i++)
                            {
                                if (GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posx2 >= posx_max)
                                {
                                    posx_max = GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posx2;
                                }
                                if (GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posx2 <= posx_min)
                                {
                                    posx_min = GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posx2;
                                }
                            }
                            //确定多通道光斑Y方向最大值和最小值
                            double posy_max = GlobalParameters.processdata.beforelenslaserSpotRecognize[0].posy2;
                            double posy_min = GlobalParameters.processdata.beforelenslaserSpotRecognize[0].posy2;
                            for (int i = 0; i < GlobalParameters.productconfig.processConfig.channelCount; i++)
                            {
                                if (GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posy2 >= posy_max)
                                {
                                    posy_max = GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posy2;
                                }
                                if (GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posy2 <= posy_min)
                                {
                                    posy_min = GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posy2;
                                }
                            }
                            //计算坐标中间值作为多通道光斑平衡位置
                            posx2 = (posx_max + posx_min) / 2;
                            posy2 = (posy_max + posy_min) / 2;
                            break;
                    }

                    //计算多通道光斑平衡位置与产品Box窗口之间X方向及Y方向各自的偏移量
                    posx2_offset = posx2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2;
                    posy2_offset = posy2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2;
                    GlobalFunction.updateStatusDelegate("光斑平衡位置与产品Box窗口中心X方向偏移：" + posx2_offset.ToString("f3"), Enum_MachineStatus.NORMAL);
                    GlobalFunction.updateStatusDelegate("光斑平衡位置与产品Box窗口中心Y方向偏移：" + posy2_offset.ToString("f3"), Enum_MachineStatus.NORMAL);

                    //获取LaserSpec
                    double spec1 = Math.Abs(GlobalParameters.productconfig.processConfig.laserSpec1);
                    double spec2 = Math.Abs(GlobalParameters.productconfig.processConfig.laserSpec2);

                    //执行CheckLaserPositionBeforeLens2检查
                    if (GlobalParameters.productconfig.processConfig.checkLaserPositionBeforeLens2 == true)
                    {
                        //如果偏移量超出Spec2规格，则放弃当前产品的后续组装操作
                        if (Math.Abs(posx2_offset) > spec2)
                        {
                            GlobalFunction.updateStatusDelegate("光斑平衡位置与产品Box窗口中心X方向偏移超标Spec2", Enum_MachineStatus.ERROR);
                            GlobalParameters.processdata.checkSpotBeforeAlignResult = false;
                            GlobalParameters.processdata.failReason = "Spot center OffsetX exceed Spec2";
                            return false;
                        }
                        if (Math.Abs(posy2_offset) > spec2)
                        {
                            GlobalFunction.updateStatusDelegate("光斑平衡位置与产品Box窗口中心Y方向偏移超标Spec2", Enum_MachineStatus.ERROR);
                            GlobalParameters.processdata.checkSpotBeforeAlignResult = false;
                            GlobalParameters.processdata.failReason = "Spot center OffsetY exceed Spec2";
                            return false;
                        }
                    }

                    //执行CompensateLaserWindowOffset补偿
                    if (GlobalParameters.productconfig.processConfig.compensateLaserWindowOffset == true)
                    {
                        int flag = 0;

                        if ((Math.Abs(posx2_offset) <= spec1) && (Math.Abs(posy2_offset) <= spec1))
                        {
                            GlobalParameters.processdata.lensCenterAttachPosition.posx2 = posx2;
                            GlobalParameters.processdata.lensCenterAttachPosition.posy2 = posy2;
                        }
                        else if ((Math.Abs(posx2_offset) > spec1) && (Math.Abs(posx2_offset) <= spec2) && (Math.Abs(posy2_offset) <= spec1))
                        {
                            if (posx2_offset > 0)
                            {
                                flag = 1;
                            }
                            else if (posx2_offset < 0)
                            {
                                flag = -1;
                            }
                            else
                            {
                                flag = 0;
                            }
                            GlobalParameters.processdata.lensCenterAttachPosition.posx2 = GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2 + flag * spec1;
                            GlobalParameters.processdata.lensCenterAttachPosition.posy2 = posy2;
                        }
                        else if ((Math.Abs(posy2_offset) > spec1) && (Math.Abs(posy2_offset) <= spec2) && (Math.Abs(posx2_offset) <= spec1))
                        {
                            if (posy2_offset > 0)
                            {
                                flag = 1;
                            }
                            else if (posy2_offset < 0)
                            {
                                flag = -1;
                            }
                            else
                            {
                                flag = 0;
                            }
                            GlobalParameters.processdata.lensCenterAttachPosition.posx2 = posx2;
                            GlobalParameters.processdata.lensCenterAttachPosition.posy2 = GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2 + flag * spec1;
                        }
                        else if ((Math.Abs(posx2_offset) > spec1) && (Math.Abs(posx2_offset) <= spec2) && (Math.Abs(posy2_offset) > spec1) && (Math.Abs(posy2_offset) <= spec2))
                        {
                            if (posx2_offset > 0)
                            {
                                flag = 1;
                            }
                            else if (posx2_offset < 0)
                            {
                                flag = -1;
                            }
                            else
                            {
                                flag = 0;
                            }
                            GlobalParameters.processdata.lensCenterAttachPosition.posx2 = GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2 + flag * spec1;

                            if (posy2_offset > 0)
                            {
                                flag = 1;
                            }
                            else if (posy2_offset < 0)
                            {
                                flag = -1;
                            }
                            else
                            {
                                flag = 0;
                            }
                            GlobalParameters.processdata.lensCenterAttachPosition.posy2 = GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2 + flag * spec1;
                        }
                    }
                    else
                    {
                        GlobalParameters.processdata.lensCenterAttachPosition.posx2 = posx2;
                        GlobalParameters.processdata.lensCenterAttachPosition.posy2 = posy2;
                    }

                    //计算Lens中心贴装目标位置与产品Box窗口中心在X和Y方向的偏移量(即)
                    GlobalParameters.processdata.lensCenterAttachOffsetx = GlobalParameters.processdata.lensCenterAttachPosition.posx2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2;
                    GlobalParameters.processdata.lensCenterAttachOffsety = GlobalParameters.processdata.lensCenterAttachPosition.posy2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2;

                    break;

                case Enum_AlignmentWay.LASER_LASER:
                    //这种方式下Lens中心将对准光斑中心位置(适用于单通道TOSA类产品)
                    //特殊情况：下相机受硬件功能限制(只能工作在红外线波段)无法看到Lens和产品Box窗口，只可以看到光斑中心
                    GlobalParameters.processdata.lensCenterAttachPosition.posx2 = GlobalParameters.processdata.laserSpotRecognize[0].posx2;
                    GlobalParameters.processdata.lensCenterAttachPosition.posy2 = GlobalParameters.processdata.laserSpotRecognize[0].posy2;
                    GlobalParameters.processdata.lensCenterAttachOffsetx = 0;
                    GlobalParameters.processdata.lensCenterAttachOffsety = 0;

                    break;
            }

            //主界面信息窗口显示各通道光斑中心和产品Box窗口中心在X和Y方向的偏移量
            for (int i = 0; i < GlobalParameters.productconfig.processConfig.channelCount; i++)
            {
                GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsetx = GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posx2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2;
                GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsety = GlobalParameters.processdata.beforelenslaserSpotRecognize[i].posy2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2;
                GlobalFunction.updateStatusDelegate("耦合前CH" + i.ToString() + "通道光斑中心X方向偏移: " + GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsetx.ToString("f3"), Enum_MachineStatus.NORMAL);
                GlobalFunction.updateStatusDelegate("耦合前CH" + i.ToString() + "通道光斑中心Y方向偏移: " + GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsety.ToString("f3"), Enum_MachineStatus.NORMAL);
            }

            //筛选出各通道光斑中心和产品Box窗口中心在X方向的偏移量极大值与极小值
            double offsetx_max = GlobalParameters.processdata.beforelenslaserSpotRecognize[0].centeroffsetx;
            double offsetx_min = GlobalParameters.processdata.beforelenslaserSpotRecognize[0].centeroffsetx;
            for (int i = 0; i < GlobalParameters.productconfig.processConfig.channelCount; i++)
            {
                if (GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsetx >= offsetx_max)
                {
                    offsetx_max = GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsetx;
                }
                if (GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsetx <= offsetx_min)
                {
                    offsetx_min = GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsetx;
                }
            }
            GlobalParameters.processdata.spotCenterMaxOffsetx = offsetx_max - offsetx_min;

            //筛选出各通道光斑中心和产品Box窗口中心在Y方向的偏移量极大值与极小值
            double offsety_max = GlobalParameters.processdata.beforelenslaserSpotRecognize[0].centeroffsety;
            double offsety_min = GlobalParameters.processdata.beforelenslaserSpotRecognize[0].centeroffsety;
            for (int i = 0; i < GlobalParameters.productconfig.processConfig.channelCount; i++)
            {
                if (GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsety >= offsety_max)
                {
                    offsety_max = GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsety;
                }
                if (GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsety <= offsety_min)
                {
                    offsety_min = GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsety;
                }
            }
            GlobalParameters.processdata.spotCenterMaxOffsety = offsety_max - offsety_min;

            //主界面信息窗口显示Lens中心贴装目标位置
            GlobalFunction.updateStatusDelegate("Lens中心贴装目标位置: X2 = " + GlobalParameters.processdata.lensCenterAttachPosition.posx2.ToString("f3") + " Y2 = " + GlobalParameters.processdata.lensCenterAttachPosition.posy2.ToString("f3"), Enum_MachineStatus.NORMAL);

            GlobalParameters.processdata.checkSpotBeforeAlignResult = true;
            GlobalParameters.processdata.failReason = string.Empty;

            return true;
        }
        #endregion

        #region 蘸胶
        public bool StepFlow_DispenserEpoxyDip()
        {
            bool res = false;

            //胶针移动到蘸胶位置上方（附带Z1轴上抬安全位、产品Box夹爪和胶针气缸上抬）
            double posx = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.X1;
            double posy = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.Y1;
            double posu = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.U1;
            res = GlobalFunction.ProcessFlow.MotionAxisX1Y1U1MoveToLocation(posx, posy, posu, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针移动到蘸胶位置上方失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //胶针气缸下降
            res = GlobalFunction.IOControlTools.EpoxyDipUpDown(true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针气缸下降失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //确定胶针蘸胶时的Z1轴高度值
            double posz = 0;
            if (GlobalParameters.systemconfig.DispenserConfig.autoEpoxyDipHeightCompensation == true)
            {
                //处理胶针蘸胶高度自动补偿
                //由于蘸胶一定次数后胶池内胶液总量减少了，如果不向下补偿蘸胶高度的话胶针头部蘸胶量会越来越少
                if (GlobalParameters.processdata.realtimeEpoxyDipHeight < (GlobalParameters.systemconfig.DispenserConfig.autoEpoxyDipZ1HeightLimit - Math.Abs(GlobalParameters.systemconfig.DispenserConfig.autoEpoxyDipCompensationHeight)))
                {
                    //胶针蘸胶高度仍处在蘸胶极限高度上方
                    if (GlobalParameters.processdata.realtimeEpoxyDipCount == GlobalParameters.systemconfig.DispenserConfig.autoEpoxyDipCount)
                    {
                        //如果实时蘸胶累计次数已经达到预设值，则胶针蘸胶高度自动向下初偿一个微步距离                        
                        GlobalParameters.processdata.realtimeEpoxyDipHeight += Math.Abs(GlobalParameters.systemconfig.DispenserConfig.autoEpoxyDipCompensationHeight);

                        //实时蘸胶累计次数清零
                        GlobalParameters.processdata.realtimeEpoxyDipCount = 0;
                    }
                    else
                    {
                        //实时蘸胶累计次数刷新
                        GlobalParameters.processdata.realtimeEpoxyDipCount++;
                    }
                }
                else
                {
                    //强制胶针蘸胶高度停留在蘸胶极限高度，防止胶针触到胶池底部而损坏
                    GlobalParameters.processdata.realtimeEpoxyDipHeight = GlobalParameters.systemconfig.DispenserConfig.autoEpoxyDipZ1HeightLimit;
                    GlobalParameters.processdata.realtimeEpoxyDipCount = 0;
                }
                posz = GlobalParameters.processdata.realtimeEpoxyDipHeight;
            }
            else
            {
                //不使用蘸胶高度自动补偿
                posz = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.Z1;
            }

            //胶针下降到胶池中蘸胶        
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针下降到胶池中蘸胶失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GeneralFunction.Delay((int)(GlobalParameters.systemconfig.DispenserConfig.epoxytestdiptime * 1000));

            //胶针气缸上抬
            res = GlobalFunction.IOControlTools.EpoxyDipUpDown(false, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针气缸上抬失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Z1轴上抬到点胶安全位
            posz = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针上抬到点胶安全位置失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //胶针旋转到点胶角度
            posu = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.U1;
            res = GlobalFunction.MotionTools.Motion_MoveU1ToLocation(posu, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针旋转到点胶角度失败", Enum_MachineStatus.ERROR);
            }

            return res;
        }
        #endregion

        #region Lens放入产品盒子
        public bool StepFlow_PlaceLensIntoBox()
        {
            bool res = false;

            //单步骤模式下Z1轴上抬到安全位（防止撞机事件发生）
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
                if (res == false)
                {
                    return false;
                }
            }

            //动态调整下相机工作条件
            res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.Window);
            if (res == false)
            {
                return false;
            }

            //打开下光源
            double DnRingValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewBoxWindowDnRing;
            double DnSpotValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewBoxWindowDnSpot;
            GlobalFunction.LightSourcesTools.LightSource_OpenDnRing(DnRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenDnSpot(DnSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);

            //下相机实时显示模式打开
            res = GlobalFunction.CameraTools.Camera_StartGrab("DnCamera");
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("下相机实时显示模式打开失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.flagassembly.downcameraliveonflag = true;

            //Lens夹爪Y和Z方向移动到进入产品Box内部初始位
            double posy = GlobalParameters.processdata.boxWindowRecognize.lensgripperintoboxposy2;
            double posz = GlobalParameters.productconfig.positionConfig.lensSafePosition.Z2;
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                //单步骤模式下附带产品Box气缸和胶针气缸上抬动作防止撞机
                res = GlobalFunction.ProcessFlow.MotionAxisY2Z2MoveToLocation(posy, posz, true, false, true);
            }
            else
            {
                //全自动模式
                res = GlobalFunction.ProcessFlow.MotionAxisY2Z2MoveToLocation(posy, posz, false, false, true);
            }
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪Y和Z方向移动到进入产品Box内部初始位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //设置X2轴速度百分比（一般放慢速度将Lens移入产品Box内部，以保证安全）
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", GlobalParameters.productconfig.positionConfig.moveLensIntoBoxSpeedPercent);

            //Lens夹爪X方向移动到进入产品Box内部初始位
            double posx = GlobalParameters.processdata.boxWindowRecognize.lensgripperintoboxposx2;
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("X2", posx, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪X方向移动到进入产品Box内部初始位失败", Enum_MachineStatus.ERROR);

                //恢复X2轴速度百分比
                res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", 100);
                return false;
            }
            GeneralFunction.Delay(500);
            GlobalParameters.flagassembly.lensgripperinboxflag = true;

            //恢复X2轴速度百分比
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", 100);

            //下相机实时显示模式关闭
            res = GlobalFunction.CameraTools.Camera_StopGrab("DnCamera");
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("下相机实时显示模式关闭失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.flagassembly.downcameraliveonflag = false;

            //关闭下光源
            GlobalFunction.LightSourcesTools.LightSource_CloseDnRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseDnSpot();

            return true;
        }
        #endregion

        #region Lens耦合
        public bool StepFlow_AlignLens()
        {
            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            //动态调整下相机工作条件
            bool res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.Lens);
            if (res == false)
            {
                return false;
            }

            int retErrorCode = 0;
            double pressureforce = 0;
            double posx2 = 0;
            double posy2 = 0;
            double posz2 = 0;
            double posz2_delta = 0;
            double posz2_touchwindow = 0;
            bool touchwindow = false;

            //打开下光源
            double DnRingValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensDnRing;
            double DnSpotValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensDnSpot;
            GlobalFunction.LightSourcesTools.LightSource_OpenDnRing(DnRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenDnSpot(DnSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);

            //下相机实时显示模式打开
            res = GlobalFunction.CameraTools.Camera_StartGrab("DnCamera");
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("下相机实时显示模式打开失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.flagassembly.downcameraliveonflag = true;

            //设置Z2轴速度百分比(一般放慢速度以保证安全，取夹爪贴装Lens时的速度)
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", GlobalParameters.productconfig.processConfig.pressLensSpeedPercent);

            //清除压力传感器读数
            if (GlobalParameters.HardwareInitialStatus.ForceSensor_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("压力计未初始化", Enum_MachineStatus.ERROR);
                return false;
            }
            else
            {
                res = GlobalFunction.ForceSensorTools.ForceSensor_ResetZero("GripperPressureSensor", ref retErrorCode);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("压力计示数清零操作失败", Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            //获取压力计读数
            res = GlobalFunction.ForceSensorTools.ForceSensor_GetPressureValue("GripperPressureSensor", ref pressureforce, ref retErrorCode);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("获取压力计读数失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.processdata.realtimeLensPressureForce = Math.Abs(pressureforce);

            //主界面压力传感器读值刷新
            GlobalFunction.updatePressureForceReadingDelegate(GlobalParameters.processdata.realtimeLensPressureForce);

            //防呆措施：如果是人工预点胶工艺，不管耦合是否成功强制标记此时Lens已接触到胶水
            if (GlobalParameters.productconfig.processConfig.manualPreDispensing == true)
            {
                GlobalParameters.flagassembly.lenstouchedepoxyflag = true;
            }

            //Z2轴向下运动0.05mm
            res = GlobalFunction.MotionTools.Motion_MoveZ2Distance(-0.05, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Z2轴下压Lens运动失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //获取当前Z2轴位置
            posz2 = GlobalFunction.MotionTools.Motion_GetZ2Pos();

            //获取压力计读数
            res = GlobalFunction.ForceSensorTools.ForceSensor_GetPressureValue("GripperPressureSensor", ref pressureforce, ref retErrorCode);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("获取压力计读数失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.processdata.realtimeLensPressureForce = Math.Abs(pressureforce);

            //主界面压力传感器读值刷新
            GlobalFunction.updatePressureForceReadingDelegate(GlobalParameters.processdata.realtimeLensPressureForce);

            while (true)
            {
                //Z2轴向下运动一个微步(Touch Step)，即夹爪夹持Lens下压一个微步
                res = GlobalFunction.MotionTools.Motion_MoveZ2Distance(-Math.Abs(GlobalParameters.productconfig.processConfig.pressLensTouchStep), true);

                //获取当前Z2轴位置
                posz2 = GlobalFunction.MotionTools.Motion_GetZ2Pos();

                //获取压力计读数
                res = GlobalFunction.ForceSensorTools.ForceSensor_GetPressureValue("GripperPressureSensor", ref pressureforce, ref retErrorCode);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("获取压力计读数失败", Enum_MachineStatus.ERROR);
                    return false;
                }
                GlobalParameters.processdata.realtimeLensPressureForce = Math.Abs(pressureforce);

                //主界面压力传感器读值刷新
                GlobalFunction.updatePressureForceReadingDelegate(GlobalParameters.processdata.realtimeLensPressureForce);

                //检测压力计读数是否大于预设的接触压力值(Touch Force)，即检测夹爪夹持Lens是否触底产品Box光窗口表面
                if ((touchwindow == false) && (GlobalParameters.processdata.realtimeLensPressureForce >= Math.Abs(GlobalParameters.productconfig.processConfig.pressLensTouchForce)))
                {
                    //夹爪夹持Lens到达触底位置
                    touchwindow = true;
                    break;
                }

                //如果此时压力计读数已经大于预设的Lens下压压力值(Press Force)，则Z2轴停止继续向下运动
                if (GlobalParameters.processdata.realtimeLensPressureForce >= Math.Abs(GlobalParameters.productconfig.processConfig.pressLensForce))
                {
                    break;
                }

                //计算当前Z2轴高度和Lens进入产品Box时Z2轴安全高度之间的高度差
                posz2_delta = Math.Abs(posz2 - GlobalParameters.productconfig.positionConfig.lensSafePosition.Z2);

                //如果高度差大于预设的Lens下压Z2轴运动范围
                if (posz2_delta >= Math.Abs(GlobalParameters.productconfig.positionConfig.pressLensDistanceLimit))
                {
                    GlobalFunction.updateStatusDelegate("Z2轴下压Lens触底总行程已超出极限位移量", Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            //记录Z2轴下压Lens触底产品Box光窗表面的位置
            posz2_touchwindow = GlobalFunction.MotionTools.Motion_GetZ2Pos();

            //如果之前夹爪夹持Lens已触底过并且Z2轴下压时的微步距设定值(Touch Force)大于0.005mm，为防止过压损坏夹爪需要回抬一个微步
            if ((touchwindow == true) && (GlobalParameters.productconfig.processConfig.pressLensTouchStep > 0.005))
            {
                touchwindow = false;

                //Z2轴上抬一个微步(Touch Step)，让夹爪夹持Lens离开触底位置
                res = GlobalFunction.MotionTools.Motion_MoveZ2Distance(Math.Abs(GlobalParameters.productconfig.processConfig.pressLensTouchStep), true);

                //获取当前Z2轴位置
                posz2 = GlobalFunction.MotionTools.Motion_GetZ2Pos();

                //获取压力计读数
                res = GlobalFunction.ForceSensorTools.ForceSensor_GetPressureValue("GripperPressureSensor", ref pressureforce, ref retErrorCode);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("获取压力计读数失败", Enum_MachineStatus.ERROR);
                    return false;
                }
                GlobalParameters.processdata.realtimeLensPressureForce = Math.Abs(pressureforce);

                //主界面压力传感器读值刷新
                GlobalFunction.updatePressureForceReadingDelegate(GlobalParameters.processdata.realtimeLensPressureForce);

                while (true)
                {
                    //Z2轴上抬一个微步(Touch Step)，让夹爪夹持Lens离开触底位置
                    res = GlobalFunction.MotionTools.Motion_MoveZ2Distance(Math.Abs(GlobalParameters.productconfig.processConfig.pressLensTouchStep), true);

                    //获取当前Z2轴位置
                    posz2 = GlobalFunction.MotionTools.Motion_GetZ2Pos();

                    //获取压力计读数
                    res = GlobalFunction.ForceSensorTools.ForceSensor_GetPressureValue("GripperPressureSensor", ref pressureforce, ref retErrorCode);
                    if (res == false)
                    {
                        GlobalFunction.updateStatusDelegate("获取压力计读数失败", Enum_MachineStatus.ERROR);
                        return false;
                    }
                    GlobalParameters.processdata.realtimeLensPressureForce = Math.Abs(pressureforce);

                    //主界面压力传感器读值刷新
                    GlobalFunction.updatePressureForceReadingDelegate(GlobalParameters.processdata.realtimeLensPressureForce);

                    //如果之前夹爪夹持Lens已触过底，检测当前压力计读数是否小于预设的接触压力值(Touch Force)
                    if ((touchwindow == false) && (GlobalParameters.processdata.realtimeLensPressureForce <= Math.Abs(GlobalParameters.productconfig.processConfig.pressLensTouchForce)))
                    {
                        //说明夹爪夹持Lens已离开触底位置
                        touchwindow = true;
                        break;
                    }

                    //计算当前Z2轴高度和夹爪夹持Lens触底位置之间的高度差
                    posz2_delta = Math.Abs(posz2 - posz2_touchwindow);

                    //如果高度差大于5倍的(Touch Step)微步距离，则报下压Lens回抬异常并退出
                    if (posz2_delta > Math.Abs(GlobalParameters.productconfig.processConfig.pressLensTouchStep * 5))
                    {
                        GlobalFunction.updateStatusDelegate("Z2轴下压Lens触底回抬压力值异常", Enum_MachineStatus.ERROR);
                        MessageBox.Show("Z2轴下压Lens触底回抬压力值异常", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }

            //Z2轴上抬胶层厚度(Epoxy Thickness)
            res = GlobalFunction.MotionTools.Motion_MoveZ2Distance(Math.Abs(GlobalParameters.productconfig.processConfig.pressLensEpoxyThickness), true);

            //获取压力计读数
            res = GlobalFunction.ForceSensorTools.ForceSensor_GetPressureValue("GripperPressureSensor", ref pressureforce, ref retErrorCode);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("获取压力计读数失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.processdata.realtimeLensPressureForce = Math.Abs(pressureforce);

            //主界面压力传感器读值刷新
            GlobalFunction.updatePressureForceReadingDelegate(GlobalParameters.processdata.realtimeLensPressureForce);

            //如果当前压力计读数仍然大于或者等于预设的接触压力值(Touch Force)，则Z2轴继续上抬一次胶层厚度
            if (GlobalParameters.processdata.realtimeLensPressureForce >= Math.Abs(GlobalParameters.productconfig.processConfig.pressLensTouchForce))
            {
                //Z2轴上抬胶层厚度(Epoxy Thickness)
                res = GlobalFunction.MotionTools.Motion_MoveZ2Distance(Math.Abs(GlobalParameters.productconfig.processConfig.pressLensEpoxyThickness), true);
            }

            //确定Lens夹爪最终在产品Box内贴装Lens时的Z2轴位置
            GlobalParameters.processdata.lensGripperAttachPosition.posz2 = GlobalFunction.MotionTools.Motion_GetZ2Pos();

            //下相机实时显示模式关闭
            res = GlobalFunction.CameraTools.Camera_StopGrab("DnCamera");
            GlobalParameters.flagassembly.downcameraliveonflag = false;

            //设置X2_Y2轴速度百分比(一般放慢速度以保证安全，取夹爪贴装Lens时的速度)
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", GlobalParameters.productconfig.processConfig.pressLensSpeedPercent);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y2", GlobalParameters.productconfig.processConfig.pressLensSpeedPercent);

            //实时获取Lens中心位置并将其修正定位到贴装目标位置
            DnRingValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensDnRing;
            DnSpotValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensDnSpot;
            VisionResult.LensResult LensResult = new VisionResult.LensResult();
            for (int i = 0; i < 3; i++)
            {
                res = GlobalFunction.ProcessFlow.VisionRecognizeLensInBox(DnRingValue, DnSpotValue, ref LensResult);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("下相机识别产品Box内部Lens中心失败", Enum_MachineStatus.ERROR);
                    return false;
                }

                //计算下相机中心与当前Lens中心在X和Y方向上的偏移量
                double lenscenteroffsetx = (LensResult.center.X - LensResult.imagecenter.X) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.xscale;
                double lenscenteroffsety = (LensResult.center.Y - LensResult.imagecenter.Y) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.yscale;

                //换算当前Lens中心在步进轴系统中的坐标
                double lenscenterposx2 = GlobalParameters.systemconfig.LensGripperConfig.offsetx - (GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.X1 - lenscenteroffsetx);
                double lenscenterposy2 = GlobalParameters.systemconfig.LensGripperConfig.offsety - (GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.Y1 + lenscenteroffsety);

                //计算Lens中心贴装位置和当前Lens中心位置在X和Y方向上的偏移量
                double posx2_offset = GlobalParameters.processdata.lensCenterAttachPosition.posx2 - lenscenterposx2;
                double posy2_offset = GlobalParameters.processdata.lensCenterAttachPosition.posy2 - lenscenterposy2;

                //Lens中心X方向移动到贴装目标位置
                res = GlobalFunction.MotionTools.Motion_MoveX2Distance(posx2_offset, false, false, true);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("X2轴Lens耦合运动失败", Enum_MachineStatus.ERROR);
                    return false;
                }

                //Lens中心Y方向移动到贴装目标位置
                res = GlobalFunction.MotionTools.Motion_MoveY2Distance(-posy2_offset, false, false, true);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("Y2轴Lens耦合运动失败", Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            //确定Lens夹爪最终在产品Box内贴装Lens时的X2轴和Y2轴坐标
            GlobalParameters.processdata.lensGripperAttachPosition.posx2 = GlobalFunction.MotionTools.Motion_GetX2Pos();
            GlobalParameters.processdata.lensGripperAttachPosition.posy2 = GlobalFunction.MotionTools.Motion_GetY2Pos();

            //如果不是人工预点胶工艺则夹爪需要上抬到移入产品Box时的安全高度
            if (GlobalParameters.productconfig.processConfig.manualPreDispensing == false)
            {
                //放慢速度以保证安全，取夹爪夹持Lens移入产品Box时的速度
                res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", GlobalParameters.productconfig.positionConfig.moveLensIntoBoxSpeedPercent);
                res = GlobalFunction.MotionTools.Motion_MoveToLocation("Z2", GlobalParameters.productconfig.positionConfig.lensSafePosition.Z2, true);
            }

            //恢复X2_Y2_Z2轴速度百分比
            GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", 100);
            GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y2", 100);
            GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", 100);

            //如果不是人工预点胶工艺则夹爪需要平移出产品Box
            if (GlobalParameters.productconfig.processConfig.manualPreDispensing == false)
            {
                res = GlobalFunction.MotionTools.Motion_MoveToLocation("X2", GlobalParameters.productconfig.positionConfig.lensSafePosition.X2, true);
                if (res == true)
                {
                    GlobalParameters.flagassembly.lensgripperinboxflag = false;
                }
            }

            //主界面信息窗口更新信息
            posx2 = GlobalParameters.processdata.lensGripperAttachPosition.posx2;
            posy2 = GlobalParameters.processdata.lensGripperAttachPosition.posy2;
            posz2 = GlobalParameters.processdata.lensGripperAttachPosition.posz2;
            GlobalFunction.updateStatusDelegate("Lens贴装夹爪目标位置: X2 = " + posx2.ToString("f3") + " Y2 = " + posy2.ToString("f3") + " Z2 = " + posz2.ToString("f3"), Enum_MachineStatus.NORMAL);

            return true;
        }
        #endregion      

        #region 点胶到产品盒子
        public bool StepFlow_DispenserEpoxyOnBox()
        {
            //动态调整下相机工作条件
            bool res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.Window);
            if (res == false)
            {
                return false;
            }

            //打开下光源
            double DnRingValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewEpoxyDnRing;
            double DnSpotValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewEpoxyDnSpot;
            GlobalFunction.LightSourcesTools.LightSource_OpenDnRing(DnRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenDnSpot(DnSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);

            //下相机实时显示模式打开
            res = GlobalFunction.CameraTools.Camera_StartGrab("DnCamera");
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("下相机实时显示模式打开失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.flagassembly.downcameraliveonflag = true;

            //各轴移动到点胶安全位（包含产品Box夹爪和胶针气缸上抬）
            res = GlobalFunction.ProcessFlow.DispenserMoveToSafePosition();
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("各轴移动到点胶安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //确定点胶针点胶时的坐标
            double posx = GlobalParameters.processdata.boxWindowRecognize.epoxydipboxwindowposx1;
            double posy = GlobalParameters.processdata.boxWindowRecognize.epoxydipboxwindowposy1;
            double posz = GlobalParameters.productconfig.positionConfig.dispenserBoxZ1Height;

            //点胶针下降到进入产品Box安全高度
            double dispenserpos_safez = posz - Math.Abs(GlobalParameters.productconfig.positionConfig.dispenserBoxZ1Up);
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("Z1", dispenserpos_safez, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("点胶针下降到进入产品Box安全高度失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //点胶针Y方向定位
            res = GlobalFunction.MotionTools.Motion_MoveY1ToLocation(posy, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("点胶针Y方向定位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //胶针气缸下降
            res = GlobalFunction.ProcessFlow.DispenserEpoxyCylinderControl(true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针气缸下降失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GeneralFunction.Delay(500);

            //设置X1和Z1轴速度百分比(一般放慢速度以保证安全和定位精度)
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", GlobalParameters.productconfig.positionConfig.dispenserToBoxSpeedPercent);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z1", GlobalParameters.productconfig.positionConfig.dispenserToBoxSpeedPercent);

            //点胶针X方向平移进入产品Box
            res = GlobalFunction.MotionTools.Motion_MoveX1ToLocation(posx, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("点胶针X方向平移进入产品Box失败", Enum_MachineStatus.ERROR);
                return false;
            }
            else
            {
                //标记点胶针已进入产品Box内部
                GlobalParameters.flagassembly.epoxypininboxflag = true;

                //点胶针下降到点胶高度
                res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("点胶针下降到点胶高度失败", Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            //延时(让胶水均匀流淌到产品Box光窗上)
            GeneralFunction.Delay(1000);

            //弹窗人工确认点胶高度是否合适并允许微调
            if (((GlobalParameters.flagassembly.continueflag == true) && (GlobalParameters.flagassembly.checkepoxyonboxflag == true)) || (GlobalParameters.flagassembly.singlestepflag == true))
            {
                EpoxyOffset epoxyOffset = new EpoxyOffset();
                epoxyOffset.TopLevel = true;
                epoxyOffset.StartPosition = FormStartPosition.CenterScreen;
                epoxyOffset.ShowDialog();
                //GlobalParameters.flagassembly.befirstproductflag = false;
            }

            //点胶针回抬到进入产品Box安全高度
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("Z1", dispenserpos_safez, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("点胶针回抬到进入产品Box安全高度失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //点胶针X方向安全移出产品Box
            posx = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.X1;
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("X1", posx, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("点胶针X方向安全移出产品Box失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //标记点胶针已从产品Box内部移出
            GlobalParameters.flagassembly.epoxypininboxflag = false;

            //恢复X1和Z1轴速度百分比
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X1", 100);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z1", 100);

            //胶针气缸上抬
            res = GlobalFunction.ProcessFlow.DispenserEpoxyCylinderControl(false);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针气缸上抬失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Z1轴上抬到安全位
            res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
            if (res == false)
            {
                return false;
            }

            //下相机实时显示模式关闭
            res = GlobalFunction.CameraTools.Camera_StopGrab("DnCamera");
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("下相机实时显示模式关闭失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.flagassembly.downcameraliveonflag = false;

            //关闭下光源
            GlobalFunction.LightSourcesTools.LightSource_CloseDnRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseDnSpot();

            return true;
        }
        #endregion

        #region 擦胶
        public bool StepFlow_DispenserEpoxyWipe()
        {
            bool res = false;

            //胶针移动到擦胶位置
            double posx = GlobalParameters.systemconfig.DispenserConfig.epoxyWipePos.X1;
            double posy = GlobalParameters.systemconfig.DispenserConfig.epoxyWipePos.Y1;
            double posu = GlobalParameters.systemconfig.DispenserConfig.epoxyWipePos.U1;
            res = GlobalFunction.ProcessFlow.MotionAxisX1Y1U1MoveToLocation(posx, posy, posu, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针移动到擦胶位置失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Z1轴下降到擦胶位置
            double posz = GlobalParameters.systemconfig.DispenserConfig.epoxyWipePos.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Z1轴下降到擦胶位置失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //擦胶气缸张开
            res = GlobalFunction.IOControlTools.EpoxyWipeOpenClose(false, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("擦胶气缸张开失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GeneralFunction.Delay(500);

            //胶针气缸下降
            res = GlobalFunction.IOControlTools.EpoxyDipUpDown(true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针气缸下降失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GeneralFunction.Delay(1000);

            //擦胶气缸闭合
            res = GlobalFunction.IOControlTools.EpoxyWipeOpenClose(true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("擦胶气缸闭合失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GeneralFunction.Delay(500);

            //胶针右移擦胶
            GlobalFunction.MotionTools.Motion_SetX1SpeedPercent(10);
            res = GlobalFunction.MotionTools.Motion_MoveX1Distance(10, false, false, true);
            GlobalFunction.MotionTools.Motion_SetX1SpeedPercent(100);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针右移擦胶失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //擦胶气缸张开
            res = GlobalFunction.IOControlTools.EpoxyWipeOpenClose(false, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("擦胶气缸张开失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GeneralFunction.Delay(500);

            //胶针气缸上抬
            res = GlobalFunction.IOControlTools.EpoxyDipUpDown(false, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针气缸上抬失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //胶针上抬到点胶安全位置
            posz = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Z1;
            res = GlobalFunction.MotionTools.Motion_MoveZ1ToLocation(posz, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针上抬到安全位置失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //胶针移动到点胶安全位置
            posx = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.X1;
            posy = GlobalParameters.productconfig.positionConfig.dispenserSafePosition.Y1;
            posu = GlobalParameters.systemconfig.DispenserConfig.DispenserPinToEpoxyPos.U1;
            res = GlobalFunction.ProcessFlow.MotionAxisX1Y1U1MoveToLocation(posx, posy, posu, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("胶针移动到安全位置失败", Enum_MachineStatus.ERROR);
                return false;
            }

            return res;
        }
        #endregion

        #region 贴装Lens
        public bool StepFlow_AttachLens()
        {
            bool res = false;

            //单步骤模式下Z1轴上抬到安全位（防止撞机事件发生）
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
                if (res == false)
                {
                    return false;
                }
            }

            //动态调整下相机工作条件
            res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.Window);
            if (res == false)
            {
                return false;
            }

            double posx2 = 0;
            double posy2 = 0;
            double posz2 = 0;
            double pressureforce = 0;
            double pressStep = Math.Abs(GlobalParameters.productconfig.processConfig.pressLensStep);
            int retErrorCode = 0;

            //打开下光源
            double DnRingValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensDnRing;
            double DnSpotValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensDnSpot;
            GlobalFunction.LightSourcesTools.LightSource_OpenDnRing(DnRingValue);
            GlobalFunction.LightSourcesTools.LightSource_OpenDnSpot(DnSpotValue);
            GeneralFunction.Delay(GlobalParameters.systemconfig.InstrumentConfig.LightSourceDelay);

            //下相机实时显示模式打开
            res = GlobalFunction.CameraTools.Camera_StartGrab("DnCamera");
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("下相机实时显示模式打开失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.flagassembly.downcameraliveonflag = true;

            //夹爪快速移动到产品Box外部贴装等待位
            posx2 = GlobalParameters.processdata.lensGripperAttachPosition.posx2 - 5;
            posy2 = GlobalParameters.processdata.lensGripperAttachPosition.posy2;
            posz2 = GlobalParameters.processdata.lensGripperAttachPosition.posz2 + Math.Abs(GlobalParameters.productconfig.positionConfig.step2LensPlaceOffsetZ);
            if (posz2 > GlobalParameters.productconfig.positionConfig.lensSafePosition.Z2)
            {
                posz2 = GlobalParameters.productconfig.positionConfig.lensSafePosition.Z2;
            }
            res = GlobalFunction.MotionTools.Motion_MoveZ2ToLocation(posz2, true);
            res = GlobalFunction.MotionTools.Motion_MoveY2ToLocation(posy2, true);
            res = GlobalFunction.MotionTools.Motion_MoveX2ToLocation(posx2, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("夹爪快速移动到产品Box外部贴装等待位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //设置X2和Z2轴速度百分比(一般放慢速度以保证安全和定位精度)
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", GlobalParameters.productconfig.positionConfig.moveLensIntoBoxSpeedPercent);
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", GlobalParameters.productconfig.processConfig.pressLensSpeedPercent);

            //Lens平移进入产品Box内部压胶起始位置
            posx2 = GlobalParameters.processdata.lensGripperAttachPosition.posx2;
            res = GlobalFunction.MotionTools.Motion_MoveX2ToLocation(posx2, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens平移进入产品Box内部压胶起始位置失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.flagassembly.lensgripperinboxflag = true;

            //防呆措施：强制标记此时Lens已接触到胶水
            GlobalParameters.flagassembly.lenstouchedepoxyflag = true;

            //清除压力传感器读数
            if (GlobalParameters.HardwareInitialStatus.ForceSensor_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("压力计未初始化", Enum_MachineStatus.ERROR);
                return false;
            }
            else
            {
                res = GlobalFunction.ForceSensorTools.ForceSensor_ResetZero("GripperPressureSensor", ref retErrorCode);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("压力计示数清零操作失败", Enum_MachineStatus.ERROR);
                    return false;
                }
            }

            //获取压力计读数
            res = GlobalFunction.ForceSensorTools.ForceSensor_GetPressureValue("GripperPressureSensor", ref pressureforce, ref retErrorCode);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("获取压力计读数失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.processdata.realtimeLensPressureForce = Math.Abs(pressureforce);

            //主界面压力传感器读值刷新
            GlobalFunction.updatePressureForceReadingDelegate(GlobalParameters.processdata.realtimeLensPressureForce);

            //Lens下降压胶            
            do
            {
                //Lens下压一个微步
                res = GlobalFunction.MotionTools.Motion_MoveZ2Distance(-pressStep, true);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("Z2轴执行Lens贴装压胶运动失败", Enum_MachineStatus.ERROR);
                    return false;
                }

                //获取压力计读数
                res = GlobalFunction.ForceSensorTools.ForceSensor_GetPressureValue("GripperPressureSensor", ref pressureforce, ref retErrorCode);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate("获取压力计读数失败", Enum_MachineStatus.ERROR);
                    return false;
                }
                GlobalParameters.processdata.realtimeLensPressureForce = Math.Abs(pressureforce);

                //主界面压力传感器读值刷新
                GlobalFunction.updatePressureForceReadingDelegate(GlobalParameters.processdata.realtimeLensPressureForce);

                //如果当前压力计读数大于压力报警值，则中止压胶
                if (GlobalParameters.processdata.realtimeLensPressureForce >= Math.Abs(GlobalParameters.productconfig.processConfig.pressLensAlarmForce))
                {
                    GlobalFunction.updateStatusDelegate("Lens贴装压胶时压力值异常", Enum_MachineStatus.ERROR);
                    MessageBox.Show("Lens贴装压胶时压力值异常", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                //获取当前Z2轴位置
                posz2 = GlobalFunction.MotionTools.Motion_GetZ2Pos();
            }
            while (posz2 > (GlobalParameters.processdata.lensGripperAttachPosition.posz2 + pressStep));

            //压胶延时(爬胶并充分环绕包裹住Lens底部)
            GeneralFunction.Delay(GlobalParameters.productconfig.processConfig.pressLensTime * 1000);

            //下相机实时显示模式关闭
            res = GlobalFunction.CameraTools.Camera_StopGrab("DnCamera");
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("下相机实时显示模式关闭失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.flagassembly.downcameraliveonflag = false;

            //关闭下光源
            GlobalFunction.LightSourcesTools.LightSource_CloseDnRing();
            GlobalFunction.LightSourcesTools.LightSource_CloseDnSpot();

            return true;
        }
        #endregion

        #region UV固化前Lens中心确认
        public bool StepFlow_CheckLensCenterBeforeUVCure()
        {
            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            //动态调整下相机工作条件
            bool res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.Lens);
            if (res == false)
            {
                return false;
            }

            //下相机识别当前Lens中心
            double DnRingValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensDnRing;
            double DnSpotValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensDnSpot;
            VisionResult.LensResult LensResult = new VisionResult.LensResult();
            res = GlobalFunction.ProcessFlow.VisionRecognizeLensInBox(DnRingValue, DnSpotValue, ref LensResult);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("下相机识别产品Box内部Lens中心失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //计算当前Lens中心与下相机中心在X和Y方向上的偏移量
            double centeroffsetx = (LensResult.center.X - LensResult.imagecenter.X) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.xscale;
            double centeroffsety = (LensResult.center.Y - LensResult.imagecenter.Y) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.yscale;

            //换算出当前Lens中心在步进轴坐标系中X2_Y2方向定位坐标
            GlobalParameters.processdata.beforeUVlensCenterRecognize.posx2 = GlobalParameters.systemconfig.LensGripperConfig.offsetx - (GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.X1 - centeroffsetx);
            GlobalParameters.processdata.beforeUVlensCenterRecognize.posy2 = GlobalParameters.systemconfig.LensGripperConfig.offsety - (GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.Y1 + centeroffsety);
            GlobalFunction.updateStatusDelegate("UV前Lens中心: X2 = " + GlobalParameters.processdata.beforeUVlensCenterRecognize.posx2.ToString("f3") + " Y2 = " + GlobalParameters.processdata.beforeUVlensCenterRecognize.posy2.ToString("f3"), Enum_MachineStatus.NORMAL);

            //计算当前Lens中心与产品Box中心在X和Y方向上的偏移量
            GlobalParameters.processdata.beforeUVlensCenterRecognize.centeroffsetx = GlobalParameters.processdata.beforeUVlensCenterRecognize.posx2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2;
            GlobalParameters.processdata.beforeUVlensCenterRecognize.centeroffsety = GlobalParameters.processdata.beforeUVlensCenterRecognize.posy2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2;
            GlobalFunction.updateStatusDelegate("UV前Lens中心偏移: X = " + GlobalParameters.processdata.beforeUVlensCenterRecognize.centeroffsetx.ToString("f3") + " Y = " + GlobalParameters.processdata.beforeUVlensCenterRecognize.centeroffsety.ToString("f3"), Enum_MachineStatus.NORMAL);

            return true;
        }
        #endregion

        #region UV固化前光斑确认
        public bool StepFlow_CheckSpotBeforeUVCure()
        {
            //下相机识别各通道光斑中心
            bool res = GlobalFunction.ProcessFlow.StepFlow_DownCameraRecognizeLaserSpot();
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("下相机识别光斑失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.processdata.afterlenslaserSpotRecognize = GlobalParameters.processdata.laserSpotRecognize;

            //主界面信息窗口显示各通道光斑中心和产品Box窗口中心在X和Y方向的偏移量
            for (int i = 0; i < GlobalParameters.productconfig.processConfig.channelCount; i++)
            {
                GlobalParameters.processdata.afterlenslaserSpotRecognize[i].centeroffsetx = GlobalParameters.processdata.afterlenslaserSpotRecognize[i].posx2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2;
                GlobalParameters.processdata.afterlenslaserSpotRecognize[i].centeroffsety = GlobalParameters.processdata.afterlenslaserSpotRecognize[i].posy2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2;
                GlobalFunction.updateStatusDelegate("UV前CH" + i.ToString() + "通道光斑中心X方向偏移： " + GlobalParameters.processdata.afterlenslaserSpotRecognize[i].centeroffsetx.ToString("f3"), Enum_MachineStatus.NORMAL);
                GlobalFunction.updateStatusDelegate("UV前CH" + i.ToString() + "通道光斑中心Y方向偏移： " + GlobalParameters.processdata.afterlenslaserSpotRecognize[i].centeroffsety.ToString("f3"), Enum_MachineStatus.NORMAL);

                //执行CheckLaserPositionAfterLens2检查
                if (GlobalParameters.productconfig.processConfig.checkLaserPositionAfterLens2 == true)
                {
                    //如果偏移量超出Spec2规格，则放弃当前产品的后续组装操作
                    if (Math.Abs(GlobalParameters.processdata.afterlenslaserSpotRecognize[i].centeroffsetx) > GlobalParameters.productconfig.processConfig.laserSpec2)
                    {
                        GlobalFunction.updateStatusDelegate("UV前CH" + i.ToString() + "通道光斑中心X方向偏移超标Spec2", Enum_MachineStatus.ERROR);
                        return false;
                    }
                    if (Math.Abs(GlobalParameters.processdata.afterlenslaserSpotRecognize[i].centeroffsety) > GlobalParameters.productconfig.processConfig.laserSpec2)
                    {
                        GlobalFunction.updateStatusDelegate("UV前CH" + i.ToString() + "通道光斑中心Y方向偏移超标Spec2", Enum_MachineStatus.ERROR);
                        return false;
                    }
                }
            }

            return true;
        }
        #endregion

        #region UV固化Lens
        public bool StepFlow_UVLens()
        {
            bool res = false;
            string UVControllerName = "UVController";
            string channelName = "Channel1";
            double power = GlobalParameters.productconfig.processConfig.uvPower;
            int time = GlobalParameters.productconfig.processConfig.uvTime;
            int UVCount = GlobalParameters.productconfig.processConfig.uvCount;

            //设置UV固化参数
            res = GlobalFunction.UVControllerTools.UVController_SetUVParametes(UVControllerName, channelName, power, time);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("设置UV固化参数失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //UV枪伸出
            res = GlobalFunction.ProcessFlow.UVCylinderControl(true);
            if (res == false)
            {
                return false;
            }

            //启动UV固化
            res = GlobalFunction.UVControllerTools.UVController_StartSingleChannelUV(UVControllerName, channelName);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("启动UV固化失败", Enum_MachineStatus.ERROR);

                //UV枪收回
                GlobalFunction.ProcessFlow.UVCylinderControl(false);
                return false;
            }
            for (int i = 0; i < UVCount; i++)
            {
                GeneralFunction.Delay(time * 1000);
            }

            //停止UV固化
            res = GlobalFunction.UVControllerTools.UVController_StopSingleChannelUV(UVControllerName, channelName);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("停止UV固化失败", Enum_MachineStatus.ERROR);

                //UV枪收回
                GlobalFunction.ProcessFlow.UVCylinderControl(false);
                return false;
            }

            //UV枪收回
            res = GlobalFunction.ProcessFlow.UVCylinderControl(false);
            if (res == false)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region 释放Lens
        public bool StepFlow_ReleaseLens()
        {
            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;

            //单步骤模式下Z1轴上抬到安全位（防止撞机事件发生）
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
                if (res == false)
                {
                    return false;
                }
            }

            //Lens夹爪松开
            res = GlobalFunction.ProcessFlow.LensGripperControl(true);
            if (res == false)
            {
                return false;
            }
            GlobalParameters.flagassembly.lensongripperflag = false;

            //设置Z2轴速度百分比(一般放慢速度以保证安全，取夹爪贴装Lens时的速度)
            res = GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", GlobalParameters.productconfig.processConfig.pressLensSpeedPercent);

            //Lens夹爪上抬到移入产品Box时的安全高度
            posz = GlobalParameters.productconfig.positionConfig.lensSafePosition.Z2;
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("Z2", posz, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪上抬到移入产品Box时的安全高度失败", Enum_MachineStatus.ERROR);

                //恢复X2_Y2_Z2轴速度百分比
                GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", 100);
                GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y2", 100);
                GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", 100);
                return false;
            }

            //恢复X2_Y2_Z2轴速度百分比
            GlobalFunction.MotionTools.Motion_SetSpeedPercent("X2", 100);
            GlobalFunction.MotionTools.Motion_SetSpeedPercent("Y2", 100);
            GlobalFunction.MotionTools.Motion_SetSpeedPercent("Z2", 100);

            //Lens夹爪X方向移动到安全位
            posx = GlobalParameters.productconfig.positionConfig.lensSafePosition.X2;
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("X2", posx, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪X方向移动到安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }
            GlobalParameters.flagassembly.lensgripperinboxflag = false;

            //Lens夹爪Y方向移动到安全位
            posy = GlobalParameters.productconfig.positionConfig.lensSafePosition.Y2;
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("Y2", posy, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪Y方向移动到安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            return true;
        }
        #endregion

        #region UV固化后Lens中心确认
        public bool StepFlow_CheckLensCenterAfterUVCure()
        {
            if (GlobalParameters.HardwareInitialStatus.ImageProcessTools_InitialStatus == false)
            {
                GlobalFunction.updateStatusDelegate("Cognex视觉工具未初始化", Enum_MachineStatus.ERROR);
                return false;
            }

            //动态调整下相机工作条件
            bool res = GlobalFunction.ProcessFlow.VisionAdjustDownCameraWorkEnvironment(Enum_DnCameraViewObject.Lens);
            if (res == false)
            {
                return false;
            }

            //下相机识别当前Lens中心
            double DnRingValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensDnRing;
            double DnSpotValue = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensDnSpot;
            VisionResult.LensResult LensResult = new VisionResult.LensResult();
            res = GlobalFunction.ProcessFlow.VisionRecognizeLensInBox(DnRingValue, DnSpotValue, ref LensResult);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("下相机识别产品Box内部Lens中心失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //计算当前Lens中心与下相机中心在X和Y方向上的偏移量
            double centeroffsetx = (LensResult.center.X - LensResult.imagecenter.X) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.xscale;
            double centeroffsety = (LensResult.center.Y - LensResult.imagecenter.Y) * GlobalParameters.systemconfig.DownCameraCalibrateConfig.yscale;

            //换算出当前Lens中心在步进轴坐标系中X2_Y2方向定位坐标
            GlobalParameters.processdata.afterUVlensCenterRecognize.posx2 = GlobalParameters.systemconfig.LensGripperConfig.offsetx - (GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.X1 - centeroffsetx);
            GlobalParameters.processdata.afterUVlensCenterRecognize.posy2 = GlobalParameters.systemconfig.LensGripperConfig.offsety - (GlobalParameters.systemconfig.DownCameraCalibrateConfig.CameraPos.Y1 + centeroffsety);
            GlobalFunction.updateStatusDelegate("UV后Lens中心: X2 = " + GlobalParameters.processdata.afterUVlensCenterRecognize.posx2.ToString("f3") + " Y2 = " + GlobalParameters.processdata.afterUVlensCenterRecognize.posy2.ToString("f3"), Enum_MachineStatus.NORMAL);

            //计算当前Lens中心与产品Box中心在X和Y方向上的偏移量
            GlobalParameters.processdata.afterUVlensCenterRecognize.centeroffsetx = GlobalParameters.processdata.afterUVlensCenterRecognize.posx2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2;
            GlobalParameters.processdata.afterUVlensCenterRecognize.centeroffsety = GlobalParameters.processdata.afterUVlensCenterRecognize.posy2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2;
            GlobalFunction.updateStatusDelegate("UV后Lens中心偏移: X = " + GlobalParameters.processdata.afterUVlensCenterRecognize.centeroffsetx.ToString("f3") + " Y = " + GlobalParameters.processdata.afterUVlensCenterRecognize.centeroffsety.ToString("f3"), Enum_MachineStatus.NORMAL);

            return true;
        }
        #endregion

        #region UV固化后光斑确认
        public bool StepFlow_CheckSpotAfterUVCure()
        {
            //下相机识别各通道光斑中心
            bool res = GlobalFunction.ProcessFlow.StepFlow_DownCameraRecognizeLaserSpot();
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("下相机识别光斑失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //主界面信息窗口显示各通道光斑中心和产品Box窗口中心在X和Y方向的偏移量
            for (int i = 0; i < GlobalParameters.productconfig.processConfig.channelCount; i++)
            {
                GlobalParameters.processdata.laserSpotRecognize[i].centeroffsetx = GlobalParameters.processdata.laserSpotRecognize[i].posx2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposx2;
                GlobalParameters.processdata.laserSpotRecognize[i].centeroffsety = GlobalParameters.processdata.laserSpotRecognize[i].posy2 - GlobalParameters.processdata.boxWindowRecognize.windowcenterposy2;
                GlobalFunction.updateStatusDelegate("UV后CH" + i.ToString() + "通道光斑中心X方向偏移： " + GlobalParameters.processdata.laserSpotRecognize[i].centeroffsetx.ToString("f3"), Enum_MachineStatus.NORMAL);
                GlobalFunction.updateStatusDelegate("UV后CH" + i.ToString() + "通道光斑中心Y方向偏移： " + GlobalParameters.processdata.laserSpotRecognize[i].centeroffsety.ToString("f3"), Enum_MachineStatus.NORMAL);
            }

            return true;
        }
        #endregion

        #region 从Nest中取出产品盒子
        public bool StepFlow_PickBoxFromNest()
        {
            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;

            //单步骤模式下Z1轴上抬到安全位（防止撞机事件发生）
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
                if (res == false)
                {
                    return false;
                }
            }

            //X2_Y2_Z2三轴联动到夹取Box时的安全位
            posx = GlobalParameters.productconfig.positionConfig.boxSafePosition.X2;
            posy = GlobalParameters.productconfig.positionConfig.boxSafePosition.Y2;
            posz = GlobalParameters.productconfig.positionConfig.boxSafePosition.Z2;
            res = GlobalFunction.ProcessFlow.MotionAxisX2Y2Z2MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到夹取Box时的安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //夹爪移动到夹取Nest中产品Box位置
            posx = GlobalParameters.productconfig.positionConfig.pickBoxFromNestPosition.X1;
            posy = GlobalParameters.productconfig.positionConfig.pickBoxFromNestPosition.Y1;
            posz = GlobalParameters.productconfig.positionConfig.pickBoxFromNestPosition.Z1;
            res = GlobalFunction.ProcessFlow.MotionAxisX1Y1Z1MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("夹爪移动到夹取Nest中产品Box位置失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //张开Box夹爪
            res = GlobalFunction.ProcessFlow.BoxGripperControl(true);
            if (res == false)
            {
                return false;
            }

            //Box夹爪气缸下降
            res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(true);
            if (res == false)
            {
                return false;
            }
            GeneralFunction.Delay(1000);

            //Box夹爪闭合
            res = GlobalFunction.ProcessFlow.BoxGripperControl(false);
            if (res == false)
            {
                return false;
            }

            //Box夹爪气缸上抬
            res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(false);
            if (res == false)
            {
                return false;
            }
            GlobalParameters.flagassembly.boxinnestflag = false;

            //Z1轴上抬到安全位
            res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();

            return res;
        }
        #endregion

        #region 产品盒子放回料盘
        public bool StepFlow_PlaceBoxIntoUnloadTray(int row, int col)
        {
            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;

            //单步骤模式下Z1轴上抬到安全位（防止撞机事件发生）
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
                if (res == false)
                {
                    return false;
                }
            }

            //X2_Y2_Z2三轴联动到夹取Box时的安全位
            posx = GlobalParameters.productconfig.positionConfig.boxSafePosition.X2;
            posy = GlobalParameters.productconfig.positionConfig.boxSafePosition.Y2;
            posz = GlobalParameters.productconfig.positionConfig.boxSafePosition.Z2;
            res = GlobalFunction.ProcessFlow.MotionAxisX2Y2Z2MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到夹取Box时的安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Box移动到收料盘上方
            posx = GlobalParameters.processdata.boxload[row, col].posx + GlobalParameters.systemconfig.BoxGripperConfig.offsetx - GlobalParameters.productconfig.boxlensConfig.boxTrayMiddleSpace;
            posy = GlobalParameters.processdata.boxload[row, col].posy + GlobalParameters.systemconfig.BoxGripperConfig.offsety;
            posz = GlobalParameters.productconfig.positionConfig.pickBoxZ1Height - 0.5;
            res = GlobalFunction.ProcessFlow.MotionAxisX1Y1Z1MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Box移动到收料盘上方失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Box夹爪气缸下降
            res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(true);
            if (res == false)
            {
                return false;
            }
            GeneralFunction.Delay(500);

            //松开Box夹爪
            res = GlobalFunction.ProcessFlow.BoxGripperControl(true);
            if (res == false)
            {
                return false;
            }
            GeneralFunction.Delay(500);
            GlobalParameters.flagassembly.boxongripperflag = false;

            //Box夹爪气缸上抬
            res = GlobalFunction.ProcessFlow.BoxGripperCylinderControl(false);
            if (res == false)
            {
                return false;
            }

            //Z1轴上抬到安全位
            res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
            if (res == false)
            {
                return false;
            }

            //刷新主界面产品Box收料盘中对应穴位状态
            GlobalFunction.updateBoxUnloadTrayStatusDelegate(row, col, true, GlobalParameters.processdata.boxload[row, col].finalresult);

            return res;
        }
        #endregion

        #region Lens抛料
        public bool StepFlow_DiscardLens()
        {
            bool res = false;
            double posx = 0;
            double posy = 0;
            double posz = 0;

            //Z1轴上抬到安全位（防止撞机）
            res = GlobalFunction.ProcessFlow.MotionAxisZ1MoveToSafetyPosition();
            if (res == false)
            {
                return false;
            }

            //强制将产品Box夹爪气缸和胶针气缸安全上抬（防止撞机）
            res = GlobalFunction.IOControlTools.AllSliderSafelyMoveUp();
            if (res == false)
            {
                return false;
            }

            //强制将Lens夹爪上抬到移入产品Box时的安全高度(防止夹爪在产品Box内直接运行抛料造成机械摩擦或者碰撞)
            posz = GlobalParameters.productconfig.positionConfig.lensSafePosition.Z2;
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("Z2", posz, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪上抬到移入产品Box时的安全高度失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //根据Lens是否接触到胶水的情况决定最终的抛料位置
            if (GlobalParameters.flagassembly.lenstouchedepoxyflag == true)
            {
                posx = GlobalParameters.productconfig.positionConfig.dirtyLensDicardPosition.X2;
                posy = GlobalParameters.productconfig.positionConfig.dirtyLensDicardPosition.Y2;
                posz = GlobalParameters.productconfig.positionConfig.dirtyLensDicardPosition.Z2;
            }
            else
            {
                posx = GlobalParameters.productconfig.positionConfig.cleanLensDicardPosition.X2;
                posy = GlobalParameters.productconfig.positionConfig.cleanLensDicardPosition.Y2;
                posz = GlobalParameters.productconfig.positionConfig.cleanLensDicardPosition.Z2;
            }

            //Lens夹爪X方向移动到抛料位
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("X2", posx, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪X方向移动到抛料位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Lens夹爪Z方向移动到抛料位
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("Z2", posz, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪Z方向移动到抛料位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Lens夹爪Y方向移动到抛料位
            res = GlobalFunction.MotionTools.Motion_MoveToLocation("Y2", posy, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("Lens夹爪Y方向移动到抛料位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            //Lens夹爪松开
            res = GlobalFunction.ProcessFlow.LensGripperControl(true);
            if (res == false)
            {
                return false;
            }
            GlobalParameters.flagassembly.lensongripperflag = false;
            GlobalParameters.flagassembly.lenstouchedepoxyflag = false;

            //X2_Y2_Z2三轴联动到夹取Lens时的安全位
            posx = GlobalParameters.productconfig.positionConfig.lensSafePosition.X2;
            posy = GlobalParameters.productconfig.positionConfig.lensSafePosition.Y2;
            posz = GlobalParameters.productconfig.positionConfig.lensSafePosition.Z2;
            res = GlobalFunction.ProcessFlow.MotionAxisX2Y2Z2MoveToLocation(posx, posy, posz, true, true, true);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("X2_Y2_Z2三轴联动到夹取Lens时的安全位失败", Enum_MachineStatus.ERROR);
                return false;
            }

            return true;
        }
        #endregion

        //----MES功能函数

        #region 检查产品SN是否在当前站点
        public bool StepFlow_mesCheckSN(string ProductSN)
        {
            bool res = false;
            string errmsg = string.Empty;

            if (GlobalParameters.MESConfig.singleStepFlag == true)
            {
                //单站点配置
                res = GlobalFunction.MESTools.MES_CheckSN(ProductSN, ref errmsg);
            }
            else
            {
                //多站点配置
                res = GlobalFunction.MESTools.MES_CheckSNWithMultiStep(ProductSN, GlobalParameters.systemconfig.ManageProductConfig.currentproduct, ref errmsg);
            }
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate(ProductSN + "核查确认不在当前站点", Enum_MachineStatus.ERROR);
                MessageBox.Show(errmsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return res;
        }
        #endregion

        #region 检查产品BOM
        public bool StepFlow_mesCheckBomList(DCList dcCollection)
        {
            bool res = false;
            string errmsg = string.Empty;
            int currentcount = dcCollection.dcinfolist.Count;
            int dccount = GlobalParameters.productconfig.processConfig.dcCount;
            List<string> pnlist = new List<string>();

            for (int i = 0; i < dcCollection.dcinfolist.Count; i++)
            {
                pnlist.Add(dcCollection.dcinfolist[i].dcPN);
            }
            res = GlobalFunction.MESTools.MES_CheckBomList(currentcount, dccount, pnlist, GlobalParameters.processdata.currentProductSN, ref errmsg);
            if (res == false)
            {
                MessageBox.Show(errmsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return res;
        }
        #endregion

        #region 自动拆料
        public bool StepFlow_mesAutoRemove()
        {
            bool res = false;
            string errmsg = string.Empty;

            res = GlobalFunction.MESTools.MES_AutoRemove(GlobalParameters.processdata.currentProductSN, ref errmsg);

            //无论是返工Rework产品（拆料成功res = true），还是正常WIP产品（拆料失败res = false），拆料操作都强制返回True
            return true;    
        }
        #endregion

        #region 自动发料
        public bool StepFlow_mesComponentIssue(DCList dcCollection)
        {
            bool res = false;
            bool result = true;
            string errmsg = string.Empty;
            List<string> dclotepoxy = new List<string>();
            List<string> dclotlens = new List<string>();
            int dccount = 1;

            //强制拆料(针对返工品)
            res = GlobalFunction.ProcessFlow.StepFlow_mesAutoRemove();
            if (res == true)
            {
                GlobalFunction.updateStatusDelegate("自动拆料成功", Enum_MachineStatus.NORMAL);
            }

            //获取物料DC信息
            for (int i = 0; i < dcCollection.dcinfolist.Count; i++)
            {
                if (dcCollection.dcinfolist[i].dcNum[0] == 'A' || dcCollection.dcinfolist[i].dcNum[0] == 'a')
                {
                    //获取胶水的DC信息
                    dclotepoxy.Add(dcCollection.dcinfolist[i].dcNum);
                }
                else if (dcCollection.dcinfolist[i].dcNum[0] == 'R' || dcCollection.dcinfolist[i].dcNum[0] == 'r')
                {
                    //获取Lens的DC信息
                    dclotlens.Add(dcCollection.dcinfolist[i].dcNum);
                }
            }

            //胶水发料
            res = GlobalFunction.MESTools.MES_AutoComponentIssue(GlobalParameters.processdata.currentProductSN, dccount, dclotepoxy, ref errmsg);
            if (res == false)
            {
                //如果第一次操作失败，则尝试再次操作
                res = GlobalFunction.MESTools.MES_AutoComponentIssue(GlobalParameters.processdata.currentProductSN, dccount, dclotepoxy, ref errmsg);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate(errmsg, Enum_MachineStatus.ERROR);
                    GlobalFunction.updateStatusDelegate("胶水发料失败", Enum_MachineStatus.ERROR);
                    result = false;
                }
                else
                {
                    GlobalFunction.updateStatusDelegate("胶水发料成功", Enum_MachineStatus.NORMAL);
                }
            }
            else
            {
                GlobalFunction.updateStatusDelegate("胶水发料成功", Enum_MachineStatus.NORMAL);
            }

            //Lens发料
            res = GlobalFunction.MESTools.MES_AutoComponentIssue(GlobalParameters.processdata.currentProductSN, dccount, dclotlens, ref errmsg);
            if (res == false)
            {
                //如果第一次操作失败，则尝试再次操作
                res = GlobalFunction.MESTools.MES_AutoComponentIssue(GlobalParameters.processdata.currentProductSN, dccount, dclotlens, ref errmsg);
                if (res == false)
                {
                    GlobalFunction.updateStatusDelegate(errmsg, Enum_MachineStatus.ERROR);
                    GlobalFunction.updateStatusDelegate("Lens发料失败", Enum_MachineStatus.ERROR);
                    result = false;
                }
                else
                {
                    GlobalFunction.updateStatusDelegate("Lens发料成功", Enum_MachineStatus.NORMAL);
                }
            }
            else
            {
                GlobalFunction.updateStatusDelegate("Lens发料成功", Enum_MachineStatus.NORMAL);
            }

            return result;
        }
        #endregion

        #region 自动过站
        public bool StepFlow_mesMoveOut()
        {
            bool res = false;
            string errmsg = string.Empty;

            //检查产品SN是否在当前站点
            res = GlobalFunction.ProcessFlow.StepFlow_mesCheckSN(GlobalParameters.processdata.currentProductSN);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate(GlobalParameters.processdata.currentProductSN + "不在当前站点", Enum_MachineStatus.ERROR);
                return false;
            }

            res = GlobalFunction.MESTools.MES_AutoMoveOut(GlobalParameters.processdata.currentProductSN, ref errmsg);
            if (res == false)
            {
                //自动过站失败
                GlobalFunction.updateStatusDelegate(errmsg, Enum_MachineStatus.ERROR);
            }

            return res;
        }
        #endregion

        #region 自动Hold
        public bool StepFlow_mesAutoHold()
        {
            bool res = false;
            string errmsg = string.Empty;

            res = GlobalFunction.MESTools.MES_AutoHold(GlobalParameters.processdata.currentProductSN, GlobalParameters.processdata.mesHoldReason, ref errmsg);
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate(errmsg, Enum_MachineStatus.ERROR);
            }

            return res;
        }
        #endregion

        //----产品工艺数据上传和本地保存功能函数

        #region 产品工艺数据上传数据库
        public bool StepFlow_UploadProcessDataIntoDatabase()
        {
            bool res = false;
            string dataSource = GlobalParameters.productconfig.datalogConfig.database.datasource;
            string userID = GlobalParameters.productconfig.datalogConfig.database.userid;
            string password = GlobalParameters.productconfig.datalogConfig.database.password;
            string connectionString = "Data Source=" + dataSource + ";User ID=" + userID + ";PassWord=" + password;
            string tableName = "[EngrData].[SHG_OSA_Test].[QSFP_TOSA_Lens2]";

            SqlConnection DBconnect = new SqlConnection(connectionString);
            if (DBconnect.State == ConnectionState.Open)
            {
                DBconnect.Close();
            }
            else
            {
                DBconnect.Open();
                if (DBconnect.State != ConnectionState.Open)
                {
                    GlobalFunction.updateStatusDelegate("数据库连接错误", Enum_MachineStatus.ERROR);
                    DBconnect.Dispose();
                    return false;
                }
            }

            try
            {
                SqlCommand Sqlcommand = new SqlCommand("", DBconnect);
                Sqlcommand.CommandText = String.Format("INSERT INTO " + tableName +
                    " (" + "[Serial_Number]," +
                    "[Operator_ID]," +
                    "[Machine_ID]," +
                    "[product_type]," +
                    "[SW_Rev]," +
                    "[STartTime]," +
                    "[FinishTime]," +
                    "[Pass_fail]," +
                    "[Fail_reason]," +
                    "[Method]," +
                    "[SeLect_Channel]," +
                    "[SLCH_X]," +
                    "[SLCH_Y]," +
                    "[Lens2_X]," +
                    "[Lens2_Y]," +
                    "[CH0_X]," +
                    "[CH0_Y]," +
                    "[CH1_X]," +
                    "[CH1_Y]," +
                    "[CH2_X]," +
                    "[CH2_Y]," +
                    "[CH3_X]," +
                    "[CH3_Y]," +
                    "[Ch0_Area_of light]," +
                    "[Ch1_Area_of light]," +
                    "[Ch2_Area_of light]," +
                    "[Ch3_Area_of light]," +
                    "[Uniformity_X]," +
                    "[Uniformity_Y]" + ")" +
                    " values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}')",
                    GlobalParameters.processdata.currentProductSN,
                    GlobalParameters.systemOperationInfo.useraccount,
                    GlobalParameters.systemOperationInfo.machineid,
                    GlobalParameters.systemconfig.ManageProductConfig.currentproduct,
                    GlobalParameters.systemOperationInfo.swversion,
                    GlobalParameters.processdata.startTime,
                    GlobalParameters.processdata.endTime,
                    GlobalParameters.processdata.finalResult,
                    GlobalParameters.processdata.failReason,
                    GlobalParameters.processdata.laserSpotPosBalanceMode,
                    GlobalParameters.productconfig.processConfig.channelCount,
                    GlobalParameters.processdata.lensCenterAttachOffsetx,
                    GlobalParameters.processdata.lensCenterAttachOffsety,
                    GlobalParameters.processdata.beforeUVlensCenterRecognize.centeroffsetx,
                    GlobalParameters.processdata.beforeUVlensCenterRecognize.centeroffsety,
                    GlobalParameters.processdata.beforelenslaserSpotRecognize[0].centeroffsetx,
                    GlobalParameters.processdata.beforelenslaserSpotRecognize[0].centeroffsety,
                    GlobalParameters.processdata.beforelenslaserSpotRecognize[1].centeroffsetx,
                    GlobalParameters.processdata.beforelenslaserSpotRecognize[1].centeroffsety,
                    GlobalParameters.processdata.beforelenslaserSpotRecognize[2].centeroffsetx,
                    GlobalParameters.processdata.beforelenslaserSpotRecognize[2].centeroffsety,
                    GlobalParameters.processdata.beforelenslaserSpotRecognize[3].centeroffsetx,
                    GlobalParameters.processdata.beforelenslaserSpotRecognize[3].centeroffsety,
                    0, 0, 0, 0,
                    GlobalParameters.processdata.spotCenterMaxOffsetx,
                    GlobalParameters.processdata.spotCenterMaxOffsety);

                int insert = Sqlcommand.ExecuteNonQuery();
                if (insert > 0)
                {
                    res = true;
                }
                else
                {
                    res = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DBconnect.Close();
            }

            return res;
        }
        #endregion

        #region 产品工艺数据本地记录保存
        public void StepFlow_SaveLocalProcessData()
        {
            //检查文件夹是否存在
            string fileDirectory = $"D:\\Data\\{GlobalParameters.systemOperationInfo.producttype}\\";
            if (!Directory.Exists(fileDirectory))
            {
                //创建文件夹
                Directory.CreateDirectory(fileDirectory);
            }
            //检查数据文件是否存在
            string fileName = fileDirectory + "LocalProcessData.csv";
            if (!File.Exists(fileName))
            {
                //创建数据文件并写入表头
                File.AppendAllText(fileName, "[Serial_Number]," +
                                            "[Operator_ID]," +
                                            "[Machine_ID]," +
                                            "[Product_type]," +
                                            "[SW_Rev]," +
                                            "[StartTime]," +
                                            "[FinishTime]," +
                                            "[Pass_fail]," +
                                            "[Fail_reason]," +
                                            "[Method]," +
                                            "[SeLect_Channel]," +
                                            "[SLCH_X]," +
                                            "[SLCH_Y]," +
                                            "[Lens2_X]," +
                                            "[Lens2_Y]," +
                                            "[CH0_X]," +
                                            "[CH0_Y]," +
                                            "[CH1_X]," +
                                            "[CH1_Y]," +
                                            "[CH2_X]," +
                                            "[CH2_Y]," +
                                            "[CH3_X]," +
                                            "[CH3_Y]," +
                                            "[Ch0_Area_of light]," +
                                            "[Ch1_Area_of light]," +
                                            "[Ch2_Area_of light]," +
                                            "[Ch3_Area_of light]," +
                                            "[Uniformity_X]," +
                                            "[Uniformity_Y]) " + Environment.NewLine);

            }
            else
            {
                //防呆措施：如果文件存在，则检查当前文件是否被其他应用程序打开占用中
                try
                {
                    using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                    {
                        fs.Close();
                    }
                }
                catch
                {
                    DialogResult dialogResult = MessageBox.Show("数据文件被其他应用程序打开占用中，是否已关闭掉", "本地工艺数据无法保存", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            //记录产品数据
            string contents = GlobalParameters.processdata.currentProductSN + "," +
                                GlobalParameters.systemOperationInfo.useraccount + "," +
                                GlobalParameters.systemOperationInfo.machineid + "," +
                                GlobalParameters.systemconfig.ManageProductConfig.currentproduct + "," +
                                GlobalParameters.systemOperationInfo.swversion + "," +
                                GlobalParameters.processdata.startTime + "," +
                                GlobalParameters.processdata.endTime + "," +
                                GlobalParameters.processdata.finalResult + "," +
                                GlobalParameters.processdata.failReason + "," +
                                GlobalParameters.processdata.laserSpotPosBalanceMode + "," +
                                GlobalParameters.productconfig.processConfig.channelCount + "," +
                                GlobalParameters.processdata.lensCenterAttachOffsetx.ToString("f3") + "," +
                                GlobalParameters.processdata.lensCenterAttachOffsety.ToString("f3") + "," +
                                GlobalParameters.processdata.beforeUVlensCenterRecognize.centeroffsetx.ToString("f3") + "," +
                                GlobalParameters.processdata.beforeUVlensCenterRecognize.centeroffsety.ToString("f3") + "," +
                                GlobalParameters.processdata.beforelenslaserSpotRecognize[0].centeroffsetx.ToString("f3") + "," +
                                GlobalParameters.processdata.beforelenslaserSpotRecognize[0].centeroffsety.ToString("f3") + "," +
                                GlobalParameters.processdata.beforelenslaserSpotRecognize[1].centeroffsetx.ToString("f3") + "," +
                                GlobalParameters.processdata.beforelenslaserSpotRecognize[1].centeroffsety.ToString("f3") + "," +
                                GlobalParameters.processdata.beforelenslaserSpotRecognize[2].centeroffsetx.ToString("f3") + "," +
                                GlobalParameters.processdata.beforelenslaserSpotRecognize[2].centeroffsety.ToString("f3") + "," +
                                GlobalParameters.processdata.beforelenslaserSpotRecognize[3].centeroffsetx.ToString("f3") + "," +
                                GlobalParameters.processdata.beforelenslaserSpotRecognize[3].centeroffsety.ToString("f3") + "," +
                                "0, 0, 0, 0," +
                                GlobalParameters.processdata.spotCenterMaxOffsetx.ToString("f3") + "," +
                                GlobalParameters.processdata.spotCenterMaxOffsety.ToString("f3");
            File.AppendAllText(fileName, contents + Environment.NewLine);
        }
        #endregion

        #region 初始化产品工艺数据
        public void StepFlow_InitializeFinalProcessData()
        {
            GlobalParameters.processdata.finalResult = "Fail";
            GlobalParameters.processdata.failReason = string.Empty;
            GlobalParameters.processdata.laserSpotPosBalanceMode = Enum.GetName(typeof(Enum_LaserSpotPosBalanceMode), GlobalParameters.productconfig.processConfig.laserSpotPosBalanceMode);
            GlobalParameters.processdata.lensCenterAttachOffsetx = 0.0;
            GlobalParameters.processdata.lensCenterAttachOffsety = 0.0;
            GlobalParameters.processdata.beforeUVlensCenterRecognize.centeroffsetx = 0.0;
            GlobalParameters.processdata.beforeUVlensCenterRecognize.centeroffsety = 0.0;
            for (int i = 0; i < 4; i++)
            {
                GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsetx = 0.0;
                GlobalParameters.processdata.beforelenslaserSpotRecognize[i].centeroffsety = 0.0;
            }
            GlobalParameters.processdata.spotCenterMaxOffsetx = 0.0;
            GlobalParameters.processdata.spotCenterMaxOffsety = 0.0;

            //清空主界面产品SN信息框
            GlobalParameters.processdata.currentProductSN = String.Empty;
            GlobalFunction.updateProdutSNDelegate("");
        }
        #endregion
    }
}
