using STD_IMotion;
using System;
using System.Windows.Forms;

namespace FocusingLensAligner
{
    public partial class AxisConfigForm : Form
    {
        int CurrentIndex = -1;
        AxisConfigCollection config = new AxisConfigCollection();

        public AxisConfigForm()
        {
            InitializeComponent();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string filepath = GeneralFunction.GetConfigFilePath("AxisConfig.xml");
            //string filepath = GetAxisConfigFilePath();
            bool res = STD_IGeneralTool.GeneralTool.TryToSave<AxisConfigCollection>(config, filepath);
            if (res)
            {
                MessageBox.Show("保存参数成功！", "轴参数配置保存");
            }
            else
            {
                MessageBox.Show("保存参数失败！", "轴参数配置保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AxisConfigFrm_Load(object sender, EventArgs e)
        {
            string filepath = GeneralFunction.GetConfigFilePath("AxisConfig.xml");
            //string filepath = GetAxisConfigFilePath();
            bool res = STD_IGeneralTool.GeneralTool.ToTryLoad<AxisConfigCollection>(ref config, filepath);
            if (res)
            {
                dgv_AxisConfigDisplay.DataSource = config.AxisConfigs.ToArray();
            }

            cmb_AxisID.Items.Clear();
            cmb_AxisID.Items.AddRange(new object[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            cmb_AxisID.SelectedIndex = 0;
            cmb_AxisType.Items.Clear();
            cmb_AxisType.Items.AddRange(new object[] { "NI", "Adlink" });
            cmb_AxisType.SelectedIndex = 0;
            cmb_HomeMode.Items.Clear();
            cmb_HomeMode.Items.AddRange(Enum.GetNames(typeof (STD_IMotion.HOME_MODE)));
            cmb_HomeMode.SelectedIndex = 0;
            cmb_HomeDirection.Items.Clear();
            cmb_HomeDirection.Items.AddRange(Enum.GetNames(typeof(STD_IMotion.HOME_DIR)));
            cmb_HomeDirection.SelectedIndex = 0; 
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (txb_AxisName.Text == "" || txb_AxisName.Text ==string.Empty)
            {
                MessageBox.Show("轴名称不能为空，请配置对应名称！", "轴参数配置错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AxisConfig obj = config.AxisConfigs.Find(X => X.AxisName == txb_AxisName.Text.Trim());
            if (obj == null)
            {
                AxisConfig tmp = new AxisConfig();
                tmp.AxisName = txb_AxisName.Text.Trim();
                tmp.AxisID = Convert.ToInt32(cmb_AxisID.Text);
                tmp.AxisType = cmb_AxisType.Text.Trim();
                tmp.HomeMode = (STD_IMotion.HOME_MODE)Enum.Parse(typeof(STD_IMotion.HOME_MODE), cmb_HomeMode.Text);
                tmp.HomeDir = (STD_IMotion.HOME_DIR)Enum.Parse(typeof(STD_IMotion.HOME_DIR), cmb_HomeDirection.Text);
                config.AxisConfigs.Add(tmp);
                dgv_AxisConfigDisplay.DataSource = null;
                dgv_AxisConfigDisplay.DataSource = config.AxisConfigs.ToArray();
            }
            else
            {
                MessageBox.Show("该轴名称已存在，请修改对应名称！", "轴参数配置错误", MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btn_Modify_Click(object sender, EventArgs e)
        {
            if (CurrentIndex == -1)
            {
                return;
            }
            else
            {
                if (txb_AxisName.Text == "" || txb_AxisName.Text == string.Empty)
                {
                    MessageBox.Show("轴名称不能为空，请配置对应名称！", "轴参数配置错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                config.AxisConfigs[CurrentIndex].AxisName = txb_AxisName.Text.Trim();
                config.AxisConfigs[CurrentIndex].AxisID = Convert.ToInt32(cmb_AxisID.Text);
                config.AxisConfigs[CurrentIndex].AxisType = cmb_AxisType.Text;
                config.AxisConfigs[CurrentIndex].HomeMode = (STD_IMotion.HOME_MODE)Enum.Parse(typeof(HOME_MODE), cmb_HomeMode.Text);
                config.AxisConfigs[CurrentIndex].HomeDir = (STD_IMotion.HOME_DIR)Enum.Parse(typeof(HOME_DIR),cmb_HomeDirection.Text);
                dgv_AxisConfigDisplay.DataSource = null;
                dgv_AxisConfigDisplay.DataSource = config.AxisConfigs.ToArray();
                CurrentIndex = -1;
            }
        }

        private void dgv_AxisConfigDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_AxisConfigDisplay.SelectedCells[0].RowIndex >= 0)
            {
                CurrentIndex = dgv_AxisConfigDisplay.SelectedCells[0].RowIndex;
                AxisConfig tmp = config.AxisConfigs[CurrentIndex];
                txb_AxisName.Text = tmp.AxisName;
                cmb_AxisID.SelectedItem = tmp.AxisID;
                cmb_AxisType.SelectedItem = tmp.AxisType.ToString();
                cmb_HomeMode.SelectedItem = tmp.HomeMode.ToString();
                cmb_HomeDirection.SelectedItem = tmp.HomeDir.ToString();
            }
            else
            {
                CurrentIndex = -1;
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (CurrentIndex == -1)
            {
                return;
            }
            else
            {
                config.AxisConfigs.RemoveAt(CurrentIndex);
                dgv_AxisConfigDisplay.DataSource = null;
                dgv_AxisConfigDisplay.DataSource = config.AxisConfigs.ToArray();
                CurrentIndex = -1;
            }
        }
    }
}
