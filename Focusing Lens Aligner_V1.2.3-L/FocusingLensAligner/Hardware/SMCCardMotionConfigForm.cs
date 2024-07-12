using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;

namespace FocusingLensAligner
{
    public partial class SMCCardMotionConfigForm : Form
    {
        SMCCardMotionConfigCollection config = new SMCCardMotionConfigCollection();
        List<SMCCardMotionConfig> ParameterList = new List<SMCCardMotionConfig>();
        int CurrentIndex = -1;

        public SMCCardMotionConfigForm()
        {
            InitializeComponent();
        }

        private void SMCCardMotionConfigForm_Load(object sender, EventArgs e)
        {
            string filepath = GeneralFunction.GetConfigFilePath("SMCCardMotionConfig.xml");
            var res = STD_IGeneralTool.GeneralTool.ToTryLoad<SMCCardMotionConfigCollection>(ref config, filepath);
            if (res)
            {
                ParameterList = config.Parameter.ToList<SMCCardMotionConfig>();
                dgv_SMCCardMotionConfigDisplay.DataSource = ParameterList.ToArray();
            }
            //搜索并加载本机串口号
            cmb_SerialPort.Items.AddRange(SerialPort.GetPortNames());
            if (cmb_SerialPort.Items.Count > 0)
            {
                cmb_SerialPort.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("未发现可用串口，请检查硬件线路");
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string filepath = GeneralFunction.GetConfigFilePath("SMCCardMotionConfig.xml");
            bool res = STD_IGeneralTool.GeneralTool.TryToSave<SMCCardMotionConfigCollection>(config, filepath);
            if (res)
            {
                MessageBox.Show("保存参数成功！", "保存");
            }
            else
            {
                MessageBox.Show("保存参数失败！", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_SMCCardMotionConfigDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_SMCCardMotionConfigDisplay.SelectedCells[0].RowIndex >= 0)
            {
                CurrentIndex = dgv_SMCCardMotionConfigDisplay.SelectedCells[0].RowIndex;
                txb_AxisName.Text = ParameterList[CurrentIndex].AxisName;
                txb_Description.Text = ParameterList[CurrentIndex].Description;
                cmb_SerialPort.Text = ParameterList[CurrentIndex].SerialPort;
            }
            else
            {
                CurrentIndex = -1;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (txb_AxisName.Text == "" || txb_AxisName.Text == string.Empty)
            {
                MessageBox.Show("AxisName信息不能为空，请输入AxisName信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SMCCardMotionConfig obj = null;
            obj = config.Parameter.Find(X => X.AxisName == txb_AxisName.Text.Trim());
            if (obj == null)
            {
                SMCCardMotionConfig tmp = new SMCCardMotionConfig();
                tmp.AxisName = txb_AxisName.Text.Trim();
                tmp.Description = txb_Description.Text;
                tmp.SerialPort = cmb_SerialPort.Text.Trim();

                bool exists = false;
                exists = config.Parameter.Exists(X => X.AxisName == tmp.AxisName);
                if (exists)
                {
                    MessageBox.Show("该AxisName信息已经存在，请修改AxisName信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                config.Parameter.Add(tmp);
                ParameterList = config.Parameter.ToList<SMCCardMotionConfig>();
                dgv_SMCCardMotionConfigDisplay.DataSource = null;
                dgv_SMCCardMotionConfigDisplay.DataSource = ParameterList.ToArray();
            }
            else
            {
                MessageBox.Show("该AxisName信息已经存在，请修改对应的名称！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Modify_Click(object sender, EventArgs e)
        {
            if (CurrentIndex == -1)
            {
                return;
            }

            SMCCardMotionConfig tmp = new SMCCardMotionConfig();
            tmp.AxisName = txb_AxisName.Text.Trim();
            tmp.Description = txb_Description.Text;
            tmp.SerialPort = cmb_SerialPort.Text.Trim();
            List<SMCCardMotionConfig> tmpParameterList = new List<SMCCardMotionConfig>(ParameterList);
            tmpParameterList.RemoveAt(CurrentIndex);

            bool exists = false;
            exists = tmpParameterList.Exists(X => X.AxisName == tmp.AxisName);
            if (exists)
            {
                MessageBox.Show("该AxisName信息已经存在，请修改AxisName信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            config.Parameter[CurrentIndex].AxisName = tmp.AxisName;
            config.Parameter[CurrentIndex].Description = tmp.Description;
            config.Parameter[CurrentIndex].SerialPort = tmp.SerialPort;
            dgv_SMCCardMotionConfigDisplay.DataSource = null;
            dgv_SMCCardMotionConfigDisplay.DataSource = ParameterList;
            CurrentIndex = -1;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (CurrentIndex == -1)
            {
                return;
            }
            else
            {
                config.Parameter.RemoveAt(CurrentIndex);
                ParameterList.RemoveAt(CurrentIndex);
                dgv_SMCCardMotionConfigDisplay.DataSource = null;
                dgv_SMCCardMotionConfigDisplay.DataSource = ParameterList;
                CurrentIndex = -1;
            }
        }
    }
}