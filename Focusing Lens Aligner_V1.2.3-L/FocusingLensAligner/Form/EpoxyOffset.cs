using System;
using System.Windows.Forms;

namespace FocusingLensAligner
{
    public partial class EpoxyOffset : Form
    {
        public EpoxyOffset()
        {
            InitializeComponent();
        }

        private void btn_StopEpoxy_Click(object sender, EventArgs e)
        {
            if (GlobalParameters.flagassembly.continueflag == true)
            {
                GlobalParameters.flagassembly.stopflag = true;
                GlobalParameters.flagassembly.continueflag = false;
                GlobalFunction.updateStatusDelegate("产品点胶高度确认被人为中止", Enum_MachineStatus.NORMAL);
            }
            this.Close();
        }

        private void Process_UpdateProductPara(string[] elementStr, string newVal, ref string msg)
        {
            string filepath = GeneralFunction.GetProductConfigFilePath(GlobalParameters.systemconfig.ManageProductConfig.currentproduct + ".xml");
            GeneralFunction.UpdateXmlFileElement(filepath, elementStr, newVal, ref msg);
        }

        private void btn_EpoxyOK_Click(object sender, EventArgs e)
        {
            //获取当前点胶Z1轴高度
            double dispenserBoxZ1Height = GlobalFunction.MotionTools.Motion_GetZ1Pos();

            //如果当前产品Box点胶Z1轴高度和产品参数配置文档中数值不一致
            if (dispenserBoxZ1Height != GlobalParameters.productconfig.positionConfig.dispenserBoxZ1Height)
            {
                //更新产品参数配置文件中产品Box点胶Z1轴高度值项
                GlobalParameters.productconfig.positionConfig.dispenserBoxZ1Height = dispenserBoxZ1Height;
                string msgStr = "";
                string[] elementStr = new string[3];
                elementStr[0] = "positionConfig";
                elementStr[1] = "dispenserBoxZ1Height";
                elementStr[2] = "";
                Process_UpdateProductPara(elementStr, dispenserBoxZ1Height.ToString("f3"), ref msgStr);
                if (msgStr != "")
                {
                    if (GlobalParameters.flagassembly.continueflag == true)
                    {
                        GlobalParameters.flagassembly.continueflag = false;
                    }
                    MessageBox.Show(msgStr, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Close();
        }

        private void btn_Z1Up_Click(object sender, EventArgs e)
        {
            btn_Z1Up.Enabled = false;
            btn_EpoxyOK.Enabled = false;
            double dz = double.Parse(cmb_AdjustStepZ1.Text);
            GlobalFunction.MotionTools.Motion_MoveDistance("Z1", -dz, true);
            GeneralFunction.Delay(1000);
            tb_EpoxyDipPosZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
            tb_EpoxyDipPosZ1.Refresh();
            btn_Z1Up.Enabled = true;
            btn_EpoxyOK.Enabled = true;
            this.ActiveControl = this.btn_Z1Up;
        }

        private void btn_Z1Down_Click(object sender, EventArgs e)
        {
            btn_Z1Down.Enabled = false;
            btn_EpoxyOK.Enabled = false;
            double dz = double.Parse(cmb_AdjustStepZ1.Text);
            GlobalFunction.MotionTools.Motion_MoveDistance("Z1", dz, true);
            GeneralFunction.Delay(1000);
            tb_EpoxyDipPosZ1.Text = GlobalFunction.MotionTools.Motion_GetZ1Pos().ToString("f3");
            tb_EpoxyDipPosZ1.Refresh();
            btn_Z1Down.Enabled = true;
            btn_EpoxyOK.Enabled = true;
            this.ActiveControl = this.btn_Z1Down;
        }

        private void EpoxyOffset_Load(object sender, EventArgs e)
        {
            tb_EpoxyDipPosZ1.Text = GlobalParameters.productconfig.positionConfig.dispenserBoxZ1Height.ToString("f3");
            cmb_AdjustStepZ1.SelectedIndex = 3;     //默认"0.01";
            if (GlobalParameters.flagassembly.singlestepflag == true)
            {
                btn_StopEpoxy.Enabled = false;
                btn_StopEpoxy.Visible = false;
            }
            else
            {
                btn_StopEpoxy.Enabled = true;
                btn_StopEpoxy.Visible = true;
            }
            this.ActiveControl = this.btn_EpoxyOK;
        }
    }
}
