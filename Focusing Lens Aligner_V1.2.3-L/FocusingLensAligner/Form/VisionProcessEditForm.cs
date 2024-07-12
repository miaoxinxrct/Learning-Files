using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ToolBlock;

namespace FocusingLensAligner
{
    public partial class VisionProcessEditForm : Form
    {
        bool exit = false;
        Bitmap bitmaptemp = null;
        CogImage8Grey grey8image = null;

        struct VisionProcessResult
        {
            public PointF point;
            public PointF imagecenter;
            public float diameter;
        }

        VisionProcessResult OutResult = new VisionProcessResult();

        public VisionProcessEditForm()
        {
            InitializeComponent();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            if (bitmaptemp != null)
            {
                bitmaptemp.Dispose();
            }
            if (grey8image != null)
            {
                grey8image.Dispose();
            }
            exit = true;
            this.Close();
        }

        private void VisionProcessEditForm_Load(object sender, EventArgs e)
        {
            tb_ProductName.Text = GlobalParameters.systemconfig.ManageProductConfig.currentproduct;
            cmb_VisionProcessType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_VisionProcessType.Items.Clear();
            foreach (string item in Enum.GetNames(typeof(Enum_VisionProcessType)))
            {
                cmb_VisionProcessType.Items.Add(item);
            }
            cmb_VisionProcessType.Text = Enum_VisionProcessType.DownCameraRecognizeWindow.ToString();
            try
            {
                cogToolBlockEditV21.Subject = GlobalFunction.ImageProcessTools.cogtoolblock.Tools["UpwardViewWindow"] as CogToolBlock;
            }
            catch (Exception error)
            {
                MessageBox.Show("视觉处理功能模块加载失败：" + error.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VisionProcessEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void cmb_VisionProcessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch ((Enum_VisionProcessType)Enum.Parse(typeof(Enum_VisionProcessType), cmb_VisionProcessType.SelectedItem.ToString()))
                {
                    case Enum_VisionProcessType.DownCameraRecognizeWindow:
                        //下相机识别产品Box窗口中心位置并测量窗口直径
                        cogToolBlockEditV21.Subject = GlobalFunction.ImageProcessTools.cogtoolblock.Tools["UpwardViewWindow"] as CogToolBlock;
                        break;

                    case Enum_VisionProcessType.DownCameraRecognizeLens:
                        //下相机识别产品Box中的Lens中心位置
                        cogToolBlockEditV21.Subject = GlobalFunction.ImageProcessTools.cogtoolblock.Tools["UpwardViewLens"] as CogToolBlock;
                        break;

                    case Enum_VisionProcessType.DownCameraRecognizeLaserSpot:
                        //下相机识别光斑中心位置
                        cogToolBlockEditV21.Subject = GlobalFunction.ImageProcessTools.cogtoolblock.Tools["UpwardViewLaserSpot"] as CogToolBlock;
                        break;

                    case Enum_VisionProcessType.UpCameraRecognizeBoxCorner:
                        //上相机识别产品Box左上角位置
                        cogToolBlockEditV21.Subject = GlobalFunction.ImageProcessTools.cogtoolblock.Tools["DnwardViewBox"] as CogToolBlock;
                        break;

                    case Enum_VisionProcessType.UpCameraRecognizeEpoxySpot:
                        //上相机识别胶斑中心位置并测量胶斑直径
                        cogToolBlockEditV21.Subject = GlobalFunction.ImageProcessTools.cogtoolblock.Tools["DnwardViewEpoxySpot"] as CogToolBlock;
                        break;

                    case Enum_VisionProcessType.UpCameraRecognizeLens:
                        //上相机识别Lens中心位置(识别Lens反射光斑中心位置)
                        cogToolBlockEditV21.Subject = GlobalFunction.ImageProcessTools.cogtoolblock.Tools["DnwardViewLens"] as CogToolBlock;
                        break;

                    case Enum_VisionProcessType.UpCameraRecognizeGripper:
                        //上相机识别Lens夹爪中心位置(识别Lens夹爪上所夹持的Lens反射光斑中心位置)
                        cogToolBlockEditV21.Subject = GlobalFunction.ImageProcessTools.cogtoolblock.Tools["DnwardViewGripper"] as CogToolBlock;
                        break;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("视觉处理功能模块加载失败：" + error.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("确定要保存当前Cognex视觉处理文件", "提问", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                try
                {
                    CogSerializer.SaveObjectToFile(GlobalFunction.ImageProcessTools.cogtoolblock, Application.StartupPath + @"\Config\CognexVisionPro\" + GlobalParameters.systemconfig.ManageProductConfig.currentproduct + ".vpp");
                    MessageBox.Show("Cognex视觉处理文件保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception error)
                {                   
                    MessageBox.Show("Cognex视觉处理文件保存失败：" + error.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Run_Click(object sender, EventArgs e)
        {
            bool res = false;

            if (bitmaptemp != null)
            {
                btn_Run.Enabled = false;
                tb_ImageCenterX.Text = string.Empty;
                tb_ImageCenterY.Text = string.Empty;
                tb_PointX.Text = string.Empty;
                tb_PointY.Text = string.Empty;
                tb_Diameter.Text = string.Empty;

                //导入图像
                CogImage8Grey grey8image = new CogImage8Grey(bitmaptemp);
                cogToolBlockEditV21.Subject.Inputs["InputImage"].Value = grey8image;

                //导入参数
                switch ((Enum_VisionProcessType)Enum.Parse(typeof(Enum_VisionProcessType), cmb_VisionProcessType.SelectedItem.ToString()))
                {
                    case Enum_VisionProcessType.DownCameraRecognizeLens:
                        //下相机识别产品Box中的Lens中心位置
                        cogToolBlockEditV21.Subject.Inputs["IsPatMatch"].Value = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLensPatMatch;
                        break;

                    case Enum_VisionProcessType.DownCameraRecognizeLaserSpot:
                        //下相机识别光斑中心位置
                        cogToolBlockEditV21.Subject.Inputs["InputMin"].Value = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLaserSpotMinThreshold;
                        cogToolBlockEditV21.Subject.Inputs["InputMax"].Value = GlobalParameters.productconfig.boxlensConfig.DnCameraViewLaserSpotMaxThreshold;
                        cogToolBlockEditV21.Subject.Inputs["IsArea"].Value = false;
                        break;
                }

                //运行视觉处理模块
                try
                {
                    cogToolBlockEditV21.Subject.Run();
                }
                catch (Exception error)
                {
                    MessageBox.Show("当前视觉处理功能模块运行异常：" + error.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                res = (bool)cogToolBlockEditV21.Subject.Outputs["Res"].Value;
                if (res == true)
                {
                    //获取识别结果
                    double diameter = 0; 
                    switch ((Enum_VisionProcessType)Enum.Parse(typeof(Enum_VisionProcessType), cmb_VisionProcessType.SelectedItem.ToString()))
                    {
                        case Enum_VisionProcessType.DownCameraRecognizeWindow:
                            //下相机识别产品Box窗口中心位置并测量窗口直径
                            OutResult.point.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["X"].Value.ToString());
                            OutResult.point.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["Y"].Value.ToString());
                            OutResult.imagecenter.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterX"].Value.ToString());
                            OutResult.imagecenter.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterY"].Value.ToString());
                            OutResult.diameter = float.Parse(cogToolBlockEditV21.Subject.Outputs["Diameter"].Value.ToString());
                            diameter =  OutResult.diameter * GlobalParameters.systemconfig.DownCameraCalibrateConfig.xscale;
                            break;

                        case Enum_VisionProcessType.DownCameraRecognizeLens:
                            //下相机识别产品Box中的Lens中心位置
                            OutResult.point.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["X"].Value.ToString());
                            OutResult.point.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["Y"].Value.ToString());
                            OutResult.imagecenter.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterX"].Value.ToString());
                            OutResult.imagecenter.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterY"].Value.ToString());
                            break;

                        case Enum_VisionProcessType.DownCameraRecognizeLaserSpot:
                            //下相机识别光斑中心位置
                            OutResult.point.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["CenterX"].Value.ToString());
                            OutResult.point.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["CenterY"].Value.ToString());
                            OutResult.imagecenter.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterX"].Value.ToString());
                            OutResult.imagecenter.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterY"].Value.ToString());
                            break;

                        case Enum_VisionProcessType.UpCameraRecognizeBoxCorner:
                            //上相机识别产品Box左上角位置
                            OutResult.point.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["X"].Value.ToString());
                            OutResult.point.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["Y"].Value.ToString());
                            OutResult.imagecenter.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterX"].Value.ToString());
                            OutResult.imagecenter.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterY"].Value.ToString());
                            break;

                        case Enum_VisionProcessType.UpCameraRecognizeEpoxySpot:
                            //上相机识别胶斑中心位置并测量胶斑直径
                            OutResult.point.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["CenterX"].Value.ToString());
                            OutResult.point.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["CenterY"].Value.ToString());
                            OutResult.imagecenter.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterX"].Value.ToString());
                            OutResult.imagecenter.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterY"].Value.ToString());
                            OutResult.diameter = float.Parse(cogToolBlockEditV21.Subject.Outputs["Diameter"].Value.ToString());
                            diameter = OutResult.diameter * GlobalParameters.systemconfig.UpCameraCalibrateConfig.xscale;
                            break;

                        case Enum_VisionProcessType.UpCameraRecognizeLens:
                            //上相机识别Lens中心位置(识别Lens反射光斑中心位置)
                            OutResult.point.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["X"].Value.ToString());
                            OutResult.point.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["Y"].Value.ToString());
                            OutResult.imagecenter.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterX"].Value.ToString());
                            OutResult.imagecenter.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterY"].Value.ToString());
                            break;

                        case Enum_VisionProcessType.UpCameraRecognizeGripper:
                            //上相机识别Lens夹爪中心位置(识别Lens夹爪上所夹持的Lens反射光斑中心位置)
                            OutResult.point.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["X"].Value.ToString());
                            OutResult.point.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["Y"].Value.ToString());
                            OutResult.imagecenter.X = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterX"].Value.ToString());
                            OutResult.imagecenter.Y = float.Parse(cogToolBlockEditV21.Subject.Outputs["ImageCenterY"].Value.ToString());
                            break;
                    }
                    
                    //显示结果
                    tb_ImageCenterX.Text = OutResult.imagecenter.X.ToString();
                    tb_ImageCenterY.Text = OutResult.imagecenter.Y.ToString();
                    tb_PointX.Text = ((int)OutResult.point.X).ToString();
                    tb_PointY.Text = ((int)OutResult.point.Y).ToString();
                    if (diameter != 0)
                    {
                        tb_Diameter.Text = diameter.ToString("f3");
                    }
                }
                else
                {
                    MessageBox.Show("无法识别到符合要求的图像特征", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btn_Run.Enabled = true;
            }
            else
            {
                MessageBox.Show("请选择并打开图像文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_LoadImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图像文件(*.png; *.jpg; *.bmp; *.jpeg)| *.png; *.jpg; *.bmp; *.jpeg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                bitmaptemp = new Bitmap(filename);
            }
        }
    }
}