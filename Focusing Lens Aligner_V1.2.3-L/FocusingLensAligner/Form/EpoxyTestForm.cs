using System;
using System.Windows.Forms;

namespace FocusingLensAligner
{
    public partial class EpoxyTestForm : Form
    {
        public EpoxyTestForm()
        {
            InitializeComponent();
        }

        private void EpoxyTestForm_Load(object sender, EventArgs e)
        {
            tb_EpoxyDipPosX1.Text = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.X1.ToString("f3");
            tb_EpoxyDipPosY1.Text = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.Y1.ToString("f3");
            tb_EpoxyDipPosZ1.Text = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.Z1.ToString("f3");
            tb_EpoxyDipPosU1.Text = GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.U1.ToString("f3");
            tb_EpoxySpotMaxDiameter.Text = GlobalParameters.productconfig.processConfig.maxEpoxyDia.ToString("f3");
            tb_EpoxySpotMinDiameter.Text = GlobalParameters.productconfig.processConfig.minEpoxyDia.ToString("f3");
            this.ActiveControl = this.btn_EpoxyTest;
            if (MessageBox.Show("请确保胶池防尘盖已取走", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }
        }

        private void Process_UpdateSystemPara(string[] elementStr, string newVal, ref string msg)
        {
            string filepath = GeneralFunction.GetConfigFilePath("SystemConfig.xml");
            GeneralFunction.UpdateXmlFileElement(filepath, elementStr, newVal, ref msg);
        }

        private void btn_SaveEpoxyDipPosZ1_Click(object sender, EventArgs e)
        {
            //实时蘸胶Z1轴高度值刷新
            GlobalParameters.processdata.realtimeEpoxyDipHeight = double.Parse(tb_EpoxyDipPosZ1.Text);

            //实时蘸胶累计次数清零
            GlobalParameters.processdata.realtimeEpoxyDipCount = 0;

            //更新系统配置文件中蘸胶Z1轴高度值项
            string msgStr = "";
            string[] elementStr = new string[3];
            GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.Z1 = double.Parse(tb_EpoxyDipPosZ1.Text);
            elementStr[0] = "DispenserConfig";
            elementStr[1] = "epoxyDipPos";
            elementStr[2] = "Z1";
            Process_UpdateSystemPara(elementStr, GlobalParameters.systemconfig.DispenserConfig.epoxyDipPos.Z1.ToString("f3"), ref msgStr);
            if (msgStr != "")
            {
                MessageBox.Show(msgStr, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void btn_EpoxyWipe_Click(object sender, EventArgs e)
        {
            //擦胶
            bool res = false;

            res = GlobalFunction.ProcessFlow.StepFlow_DispenserEpoxyWipe();
            if (res == false)
            {
                GlobalFunction.updateStatusDelegate("擦胶失败", Enum_MachineStatus.ERROR);
            }
            else
            {
                GlobalFunction.updateStatusDelegate("擦胶成功", Enum_MachineStatus.NORMAL);
            }
        }

        private void btn_EpoxyTest_Click(object sender, EventArgs e)
        {
            //暂时禁用窗口中相关控件，防止重复点击误触发事件
            btn_SaveEpoxyDipPosZ1.Enabled = false;
            btn_EpoxyTest.Enabled = false;
            btn_EpoxyWipe.Enabled = false;
            btn_Z1Up.Enabled = false;
            btn_Z1Down.Enabled = false;
            cmb_AdjustStepZ1.Enabled = false;

            bool res = false;
            double epoxySpotDiameter = 0;

            res = GlobalFunction.ProcessFlow.DispenserEpoxyTest(ref epoxySpotDiameter);            
            if (res == false)
            {
                tb_EpoxySpotSize.Text = string.Empty;
                GeneralFunction.SetStatusIndicateLedPictureBoxImage(led_EpoxyTestResult, false, 0);
                GlobalFunction.updateStatusDelegate("点胶测试失败", Enum_MachineStatus.ERROR);
                MessageBox.Show("点胶测试失败", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
            else
            {
                tb_EpoxySpotSize.Text = epoxySpotDiameter.ToString("f3");
                GlobalFunction.updateStatusDelegate("胶斑直径: " + tb_EpoxySpotSize.Text + "mm", Enum_MachineStatus.NORMAL);
                double diameterSpecMax = GlobalParameters.productconfig.processConfig.maxEpoxyDia;
                double diameterSpecMin = GlobalParameters.productconfig.processConfig.minEpoxyDia;
                if ((epoxySpotDiameter < diameterSpecMin) || (epoxySpotDiameter > diameterSpecMax))
                {
                    GeneralFunction.SetStatusIndicateLedPictureBoxImage(led_EpoxyTestResult, false, 0);
                    GlobalFunction.updateStatusDelegate("胶斑直径不达标", Enum_MachineStatus.NORMAL);
                    MessageBox.Show("胶斑直径不达标", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }
                else
                {
                    GeneralFunction.SetStatusIndicateLedPictureBoxImage(led_EpoxyTestResult, true, 0);
                    GlobalFunction.updateStatusDelegate("点胶测试合格", Enum_MachineStatus.NORMAL);
                    MessageBox.Show("点胶测试合格", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            }

            //恢复相关控件响应事件
            btn_SaveEpoxyDipPosZ1.Enabled = true;
            btn_EpoxyTest.Enabled = true;
            btn_EpoxyWipe.Enabled = true;
            btn_Z1Up.Enabled = true;
            btn_Z1Down.Enabled = true;
            cmb_AdjustStepZ1.Enabled = true;
        }

        private void btn_Z1Up_Click(object sender, EventArgs e)
        {
            double epoxyz =  double.Parse(tb_EpoxyDipPosZ1.Text);
            double dz = double.Parse(cmb_AdjustStepZ1.Text);
            tb_EpoxyDipPosZ1.Text = (epoxyz - dz).ToString("f3");
        }

        private void btn_Z1Down_Click(object sender, EventArgs e)
        {
            double epoxyz = double.Parse(tb_EpoxyDipPosZ1.Text);
            double dz = double.Parse(cmb_AdjustStepZ1.Text);
            tb_EpoxyDipPosZ1.Text = (epoxyz + dz).ToString("f3");
        }
    }
}
