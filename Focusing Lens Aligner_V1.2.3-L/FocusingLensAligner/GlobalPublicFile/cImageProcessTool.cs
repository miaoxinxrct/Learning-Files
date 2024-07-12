using System;
using System.Drawing;
using System.IO;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;
using static FocusingLensAligner.VisionResult;
using System.Windows.Forms;

namespace FocusingLensAligner
{
    class cImageProcessTool
    {
        public CogToolBlock cogtoolblock = new CogToolBlock();
        public CogToolBlock cogtoolblocktmp = new CogToolBlock();

        public bool ImageProcessInitial()
        {
            bool res = true;
            string productVppName = @"Config\CognexVisionPro\" + GlobalParameters.systemconfig.ManageProductConfig.currentproduct + ".vpp";
            string filepath = GeneralFunction.GetApplicationFilePath(productVppName);

            //检查视觉工具文件是否存在
            if (File.Exists(filepath) == false)
            {
                return false;
            }

            //加载视觉工具
            cogtoolblock = (CogToolBlock)CogSerializer.LoadObjectFromFile(filepath) as CogToolBlock;

            //加载国惠宽波段红外相机图像预处理视觉工具
            if (GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild == true)
            {
                //国惠宽波段红外相机分辨率为640x512
                //SV2000相机采集卡捕获的图像会将原始图像强制压缩为包含无效像素区域在内的640x480分辨率图像
                //需要将被压缩图像中有效像素区域还原为640x480分辨率图像
                filepath = GeneralFunction.GetApplicationFilePath(@"Config\CognexVisionPro\GhoptoIRCamera_ImageProcess.vpp");
                GlobalParameters.cogtoolblock_GhoptoIRCamera = (CogToolBlock)CogSerializer.LoadObjectFromFile(filepath) as CogToolBlock;
            }
            return res;
        }

        public bool Image_CameraCalibration(double gridstep, bool IsUpCamera, ref double pixelsize)
        {
            //校准相机像素分辨率
            bool res = true;

            CogImage8Grey grey8image = new CogImage8Grey(GlobalParameters.globitmap);
            cogtoolblocktmp = cogtoolblock.Tools["Calibration"] as CogToolBlock;
            if ((IsUpCamera == false) && (GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild == true))
            {
                //国惠宽波段红外相机图像预处理
                GlobalParameters.cogtoolblock_GhoptoIRCamera.Inputs[0].Value = grey8image;
                GlobalParameters.cogtoolblock_GhoptoIRCamera.Run();
                GlobalFunction.CogRecordDisplay.Image = GlobalParameters.cogtoolblock_GhoptoIRCamera.Outputs[0].Value as Cognex.VisionPro.ICogImage;
                Bitmap GhoptoIRCameraImage = new Bitmap(GlobalFunction.CogRecordDisplay.Image.ToBitmap());
                cogtoolblocktmp.Inputs["InputImage"].Value = new CogImage8Grey(GhoptoIRCameraImage);
                GhoptoIRCameraImage.Dispose();
            }
            else
            {
                cogtoolblocktmp.Inputs["InputImage"].Value = grey8image;
            }
            cogtoolblocktmp.Inputs["IsUpCamera"].Value = Convert.ToBoolean(IsUpCamera);
            cogtoolblocktmp.Run();
            GlobalFunction.CogRecordDisplay.Record = cogtoolblocktmp.CreateLastRunRecord().SubRecords[0];
            res = (bool)cogtoolblocktmp.Outputs["Res"].Value;
            if (res)
            {
                pixelsize = gridstep / (double)cogtoolblocktmp.Outputs["PixelDistance"].Value;
            }            
            grey8image.Dispose();
            GlobalParameters.globitmap.Dispose();
            GlobalParameters.globitmap = null;

            return res;
        }

        public bool Image_UpwardViewWindow(ref BoxResult boxresult)
        {
            //下相机识别产品Box窗口中心位置并测量窗口直径
            bool res = false;
 
            try
            {
                CogImage8Grey grey8image = new CogImage8Grey(GlobalParameters.globitmap);
                cogtoolblocktmp = cogtoolblock.Tools["UpwardViewWindow"] as CogToolBlock;
                if (GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild == true)
                {
                    //国惠宽波段红外相机图像预处理
                    GlobalParameters.cogtoolblock_GhoptoIRCamera.Inputs[0].Value = grey8image;
                    GlobalParameters.cogtoolblock_GhoptoIRCamera.Run();
                    GlobalFunction.CogRecordDisplay.Image = GlobalParameters.cogtoolblock_GhoptoIRCamera.Outputs[0].Value as Cognex.VisionPro.ICogImage;
                    Bitmap GhoptoIRCameraImage = new Bitmap(GlobalFunction.CogRecordDisplay.Image.ToBitmap());
                    cogtoolblocktmp.Inputs["InputImage"].Value = new CogImage8Grey(GhoptoIRCameraImage);
                    GhoptoIRCameraImage.Dispose();
                }
                else
                {
                    cogtoolblocktmp.Inputs["InputImage"].Value = grey8image;
                }                   
                cogtoolblocktmp.Run();

                GlobalFunction.CogRecordDisplay.Record = cogtoolblocktmp.CreateLastRunRecord().SubRecords[0];
                res = (bool)cogtoolblocktmp.Outputs["Res"].Value;
                if (res)
                {
                    boxresult.point.X = float.Parse(cogtoolblocktmp.Outputs["X"].Value.ToString());
                    boxresult.point.Y = float.Parse(cogtoolblocktmp.Outputs["Y"].Value.ToString());
                    boxresult.imagecenter.X = float.Parse(cogtoolblocktmp.Outputs["ImageCenterX"].Value.ToString());
                    boxresult.imagecenter.Y = float.Parse(cogtoolblocktmp.Outputs["ImageCenterY"].Value.ToString());
                    boxresult.diameter = float.Parse(cogtoolblocktmp.Outputs["Diameter"].Value.ToString());
                    //GlobalFunction.CogRecordDisplay.Image = cogtoolblocktmp.Outputs..grey8image;
                }
                grey8image.Dispose();
                GlobalParameters.globitmap.Dispose();
                GlobalParameters.globitmap = null;
            }
            catch
            {
                res = false;
            }
            
            return res;
        }

        public bool Image_UpwardViewLens(bool IsPatMatch, ref LensResult lensres)
        {
            //下相机识别(产品Box中的)Lens中心位置
            bool res = false;
            
            try
            {
                CogImage8Grey grey8image = new CogImage8Grey(GlobalParameters.globitmap);
                cogtoolblocktmp = cogtoolblock.Tools["UpwardViewLens"] as CogToolBlock;
                if (GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild == true)
                {
                    //国惠宽波段红外相机图像预处理
                    GlobalParameters.cogtoolblock_GhoptoIRCamera.Inputs[0].Value = grey8image;
                    GlobalParameters.cogtoolblock_GhoptoIRCamera.Run();
                    GlobalFunction.CogRecordDisplay.Image = GlobalParameters.cogtoolblock_GhoptoIRCamera.Outputs[0].Value as Cognex.VisionPro.ICogImage;
                    Bitmap GhoptoIRCameraImage = new Bitmap(GlobalFunction.CogRecordDisplay.Image.ToBitmap());
                    cogtoolblocktmp.Inputs["InputImage"].Value = new CogImage8Grey(GhoptoIRCameraImage);
                    GhoptoIRCameraImage.Dispose();
                }
                else
                {
                    cogtoolblocktmp.Inputs["InputImage"].Value = grey8image;
                }
                cogtoolblocktmp.Inputs["IsPatMatch"].Value = IsPatMatch;
                cogtoolblocktmp.Run();

                GlobalFunction.CogRecordDisplay.Record = cogtoolblocktmp.CreateLastRunRecord().SubRecords[0];
                res = (bool)cogtoolblocktmp.Outputs["Res"].Value;
                if (res)
                {
                    lensres.center.X = float.Parse(cogtoolblocktmp.Outputs["X"].Value.ToString());
                    lensres.center.Y = float.Parse(cogtoolblocktmp.Outputs["Y"].Value.ToString());
                    lensres.imagecenter.X = float.Parse(cogtoolblocktmp.Outputs["ImageCenterX"].Value.ToString());
                    lensres.imagecenter.Y = float.Parse(cogtoolblocktmp.Outputs["ImageCenterY"].Value.ToString());
                }
                grey8image.Dispose();                
                GlobalParameters.globitmap.Dispose();
                GlobalParameters.globitmap = null;
            }
            catch
            {
                res = false;
            }

            return res;
        }

        public bool Image_UpwardViewLaserSpot(double minthres, double maxthres, bool isarea, ref LaserSpotResult spotresult)
        {
            //下相机识别光斑中心位置
            bool res = false;
            
            try
            {                
                if (GlobalParameters.globitmap != null && GlobalParameters.globitmap.Width > 0 && GlobalParameters.globitmap.Height > 0)
                {
                    CogImage8Grey grey8image = new CogImage8Grey(GlobalParameters.globitmap);
                    cogtoolblocktmp = cogtoolblock.Tools["UpwardViewLaserSpot"] as CogToolBlock;
                    if (GlobalParameters.systemconfig.InstrumentConfig.GhoptoIRCamera_Vaild == true)
                    {
                        //国惠宽波段红外相机图像预处理
                        GlobalParameters.cogtoolblock_GhoptoIRCamera.Inputs[0].Value = grey8image;
                        GlobalParameters.cogtoolblock_GhoptoIRCamera.Run();
                        GlobalFunction.CogRecordDisplay.Image = GlobalParameters.cogtoolblock_GhoptoIRCamera.Outputs[0].Value as Cognex.VisionPro.ICogImage;
                        Bitmap GhoptoIRCameraImage = new Bitmap(GlobalFunction.CogRecordDisplay.Image.ToBitmap());
                        cogtoolblocktmp.Inputs["InputImage"].Value = new CogImage8Grey(GhoptoIRCameraImage);
                        GhoptoIRCameraImage.Dispose();
                    }
                    else
                    {
                        cogtoolblocktmp.Inputs["InputImage"].Value = grey8image;
                    }
                    cogtoolblocktmp.Inputs["InputMin"].Value = minthres;
                    cogtoolblocktmp.Inputs["InputMax"].Value = maxthres;
                    cogtoolblocktmp.Inputs["IsArea"].Value = isarea;
                    cogtoolblocktmp.Run();

                    res = (bool)cogtoolblocktmp.Outputs["Res"].Value;
                    if (res)
                    {
                        GlobalFunction.CogRecordDisplay.Record = cogtoolblocktmp.CreateLastRunRecord().SubRecords[0];
                        spotresult.center.X = float.Parse(cogtoolblocktmp.Outputs["CenterX"].Value.ToString());
                        spotresult.center.Y = float.Parse(cogtoolblocktmp.Outputs["CenterY"].Value.ToString());
                        spotresult.imagecenter.X = float.Parse(cogtoolblocktmp.Outputs["ImageCenterX"].Value.ToString());
                        spotresult.imagecenter.Y = float.Parse(cogtoolblocktmp.Outputs["ImageCenterY"].Value.ToString());
                    }
                    else
                    {
                        GlobalFunction.CogRecordDisplay.Image = grey8image;
                    }
                    grey8image.Dispose();
                    GlobalParameters.globitmap.Dispose();
                    GlobalParameters.globitmap = null;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                res = false;
            }

            return res;
        }

        public bool Image_DnwardViewBox(ref BoxResult boxresult)
        {
            //上相机识别产品Box左上角位置
            bool res = false;

            cogtoolblocktmp = cogtoolblock.Tools["DnwardViewBox"] as CogToolBlock;
            CogImage8Grey grey8image = new CogImage8Grey(GlobalParameters.globitmap);
            cogtoolblocktmp.Inputs["InputImage"].Value = grey8image;
            cogtoolblocktmp.Run();

            GlobalFunction.CogRecordDisplay.Record = cogtoolblocktmp.CreateLastRunRecord().SubRecords[0];
            res = (bool)cogtoolblocktmp.Outputs["Res"].Value;
            if (res)
            {
                boxresult.point.X = float.Parse(cogtoolblocktmp.Outputs["X"].Value.ToString());
                boxresult.point.Y = float.Parse(cogtoolblocktmp.Outputs["Y"].Value.ToString());
                boxresult.imagecenter.X = float.Parse(cogtoolblocktmp.Outputs["ImageCenterX"].Value.ToString());
                boxresult.imagecenter.Y = float.Parse(cogtoolblocktmp.Outputs["ImageCenterY"].Value.ToString());
            }
            grey8image.Dispose();
            GlobalParameters.globitmap.Dispose();
            GlobalParameters.globitmap = null;
            return res;
        }

        public bool Image_DnwardViewEpoxySpot(ref EpoxyResult epoxyres)
        {
            //上相机识别胶斑中心位置并测量胶斑直径
            bool res = false;

            try
            {
                cogtoolblocktmp = cogtoolblock.Tools["DnwardViewEpoxySpot"] as CogToolBlock;
                CogImage8Grey grey8image = new CogImage8Grey(GlobalParameters.globitmap);
                cogtoolblocktmp.Inputs["InputImage"].Value = grey8image;
                cogtoolblocktmp.Run();

                GlobalFunction.CogRecordDisplay.Record = cogtoolblocktmp.CreateLastRunRecord().SubRecords[0];
                res = (bool)cogtoolblocktmp.Outputs["Res"].Value;
                if (res)
                {
                    epoxyres.diameter = float.Parse(cogtoolblocktmp.Outputs["Diameter"].Value.ToString());
                    epoxyres.center.X = float.Parse(cogtoolblocktmp.Outputs["CenterX"].Value.ToString());
                    epoxyres.center.Y = float.Parse(cogtoolblocktmp.Outputs["CenterY"].Value.ToString());
                    epoxyres.imagecenter.X = float.Parse(cogtoolblocktmp.Outputs["ImageCenterX"].Value.ToString());
                    epoxyres.imagecenter.Y = float.Parse(cogtoolblocktmp.Outputs["ImageCenterY"].Value.ToString());
                }
                grey8image.Dispose();
                GlobalParameters.globitmap.Dispose();
                GlobalParameters.globitmap = null;
            }
            catch
            {
                res = false;
            }
            
            return res;
        }
        
        public bool Image_DnwardViewLens(ref LensResult lensres, bool trosalens=false)
        {
            //上相机识别Lens中心位置(识别Lens反射光斑中心位置)
            bool res = false;

            try
            {
                cogtoolblocktmp = cogtoolblock.Tools["DnwardViewLens"] as CogToolBlock;
                CogImage8Grey grey8image = new CogImage8Grey(GlobalParameters.globitmap);
                cogtoolblocktmp.Inputs["InputImage"].Value = grey8image;
                cogtoolblocktmp.Run();
                GlobalFunction.CogRecordDisplay.InteractiveGraphics.Clear();

                res = (bool)cogtoolblocktmp.Outputs["Res"].Value;
                if (res)
                {
                    GlobalFunction.CogRecordDisplay.Record = cogtoolblocktmp.CreateLastRunRecord().SubRecords[0];
                    lensres.center.X = float.Parse(cogtoolblocktmp.Outputs["X"].Value.ToString());
                    lensres.center.Y = float.Parse(cogtoolblocktmp.Outputs["Y"].Value.ToString());
                    lensres.imagecenter.X = float.Parse(cogtoolblocktmp.Outputs["ImageCenterX"].Value.ToString());
                    lensres.imagecenter.Y = float.Parse(cogtoolblocktmp.Outputs["ImageCenterY"].Value.ToString());
                }
                else
                {
                    GlobalFunction.CogRecordDisplay.Image = grey8image;
                }
                grey8image.Dispose();
                GlobalParameters.globitmap.Dispose();
                GlobalParameters.globitmap = null;
            }
            catch
            {                
                res = false;
            }
            
            return res;
        }

        public bool Image_DnwardViewGripper(ref GripperResult gripres)
        {
            //上相机识别Lens夹爪中心位置(识别Lens夹爪上所夹持的Lens反射光斑中心位置)
            bool res = false;

            try
            {
                cogtoolblocktmp = cogtoolblock.Tools["DnwardViewGripper"] as CogToolBlock;
                CogImage8Grey grey8image = new CogImage8Grey(GlobalParameters.globitmap);
                cogtoolblocktmp.Inputs["InputImage"].Value = grey8image;
                cogtoolblocktmp.Run();

                GlobalFunction.CogRecordDisplay.Record = cogtoolblocktmp.CreateLastRunRecord().SubRecords[0];
                res = (bool)cogtoolblocktmp.Outputs["Res"].Value;
                if (res)
                {
                    gripres.center.X = float.Parse(cogtoolblocktmp.Outputs["X"].Value.ToString());
                    gripres.center.Y = float.Parse(cogtoolblocktmp.Outputs["Y"].Value.ToString());
                    gripres.imagecenter.X = float.Parse(cogtoolblocktmp.Outputs["ImageCenterX"].Value.ToString());
                    gripres.imagecenter.Y = float.Parse(cogtoolblocktmp.Outputs["ImageCenterY"].Value.ToString());
                }
                grey8image.Dispose();
                GlobalParameters.globitmap.Dispose();
                GlobalParameters.globitmap = null;
            }
            catch
            {
                res = false;
            }

            return res;
        }
    }
}
