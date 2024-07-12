using System;
using System.Drawing;
using STD_ICamera;

namespace FocusingLensAligner
{
    //相机底层功能
    class cCameraTools
    {
        public cCameraTool CameraTool = new cCameraTool();
        public cCameraSettingCollection cameracollection = new cCameraSettingCollection();
        public ICamera icamera = null;

        public cSingleCameraSettingInformation GetCameraInfo(string cameraname)
        {
            bool res = false;

            cSingleCameraSettingInformation tmp = new cSingleCameraSettingInformation();
            res = STD_IGeneralTool.GeneralTool.ToTryLoad<cCameraSettingCollection>(ref cameracollection, GeneralFunction.GetConfigFilePath("STD_CameraSetting.xml"));
            if (res)
            {
                tmp = cameracollection.CameraInfor.Find(X => X.CameraName == cameraname);
            }
            return tmp;
        }

        public bool Camera_Initial()
        {
            bool res = false;

            int resnum = CameraTool.CameraInital();
            if (resnum == 1)
            {
                res = true;
            }
            return res;
        }

        public void Camera_UnInitial()
        {
            CameraTool.DisposedTools();
        }

        public bool Camera_OpenSetupPanel()
        {
            bool res = true;

            CameraTool.Camera_SystemSettingForm();
            return res;
        }

        public bool Camera_Open(string CameraName)
        {
            bool res = false;

            res = CameraTool.CameraHandle[CameraName].Camera_OpenDevice(CameraTool.CameraHandle[CameraName].CameraSN);
            return res;
        }

        public void Camera_SetExposure(string CameraName, double ExposureTimeValue)
        {
            CameraTool.CameraHandle[CameraName].Camera_ExposureTime = ExposureTimeValue;
        }

        public double Camera_GetExposure(string CameraName)
        {
            return CameraTool.CameraHandle[CameraName].Camera_ExposureTime;
        }

        public void Camera_SetGain(string CameraName, double GainValue)
        {
            CameraTool.CameraHandle[CameraName].Camera_Gain = GainValue;
        }

        public double Camera_GetGain(string CameraName)
        {
            return CameraTool.CameraHandle[CameraName].Camera_Gain;
        }

        public bool Camera_IsCameraOpen(string CameraName)
        {
            bool res = false;

            res = CameraTool.CameraHandle[CameraName].CameraIsOpen;
            return res;
        }

        public bool Camera_Close(string CameraName)
        {
            bool res = false;
            res = CameraTool.CameraHandle[CameraName].Camera_CloseDevice();
            return res;
        }

        public bool Camera_CloseAll()
        {
            bool res = true ;
            CameraTool.DisposedTools();
            return res;
        }

        public bool Camera_StartGrab(string CameraName)
        {
            bool res = false;

            res = CameraTool.CameraHandle[CameraName].Camera_StartGrab();
            return res;
        }

        public bool Camera_IsCameraGrabing(string CameraName)
        {
            bool res = false;

            res = CameraTool.CameraHandle[CameraName].CameraIsGrabing;
            return res;
        }

        public bool Camera_StopGrab(string CameraName)
        {
            bool res = false;

            res = CameraTool.CameraHandle[CameraName].Camera_StopGrab();
            return res;
        }

        public bool Camera_Snap(string CameraName, ref Bitmap FrameImage)
        {
            bool res = false;

            res = CameraTool.CameraHandle[CameraName].Camera_Snap(ref FrameImage);
            return res;
        }

        public bool Camera_UpCameraSnap(ref Bitmap bitmap)
        {
            bool res = false;
            string cameraname = "UpCamera";
            for (int i = 0; i < 3; i++)
            {
                GeneralFunction.Delay(200);
                try
                {
                    res = Camera_Snap(cameraname, ref bitmap);
                    if (bitmap.Width > 0 && bitmap.Height > 0)
                    {
                        break;
                    }
                }
                catch (Exception)
                {


                }

            }


            return res;
        }

        public bool Camera_DnCameraSnap(ref Bitmap bitmap)
        {
            bool res = false;
            string cameraname = "DnCamera";
            res = Camera_Snap(cameraname, ref bitmap);
            return res;
        }

    }
}
