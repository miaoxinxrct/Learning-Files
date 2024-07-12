using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;

namespace FocusingLensAligner
{
    public partial class ForceSensorConfigForm : Form
    {
        ForceSensorConfigCollection config = new ForceSensorConfigCollection();
        List<ForceSensorConfig> ParameterList = new List<ForceSensorConfig>();
        int CurrentIndex = -1;

        public ForceSensorConfigForm()
        {
            InitializeComponent();
        }
        
        private void ForceSensorConfigForm_Load(object sender, EventArgs e)
        {
            string filepath = GeneralFunction.GetConfigFilePath("ForceSensorConfig.xml");
            var res = STD_IGeneralTool.GeneralTool.ToTryLoad<ForceSensorConfigCollection>(ref config, filepath);
            if (res)
            {
                ParameterList = config.Parameter.ToList<ForceSensorConfig>();
                dgv_ForceSensorConfigDisplay.DataSource = ParameterList.ToArray();
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
            //加载通信波特率（默认9600）
            cmb_BaudRate.Items.AddRange(new object[] { 9600, 19200, 38400, 57600, 115200 });
            cmb_BaudRate.SelectedIndex = 0;
            
            //加载压力单位（默认克g）
            cmb_ForceUnit.Items.AddRange(new object[] { "none", "g", "kg", "lb", "t" });
            cmb_ForceUnit.SelectedIndex = 1;

            //加载数值精度，即小数点后面保留位数（默认1位）
            cmb_Accuracy.Items.AddRange(new object[] { 1, 2, 3, 4, 5 });
            cmb_Accuracy.SelectedIndex = 0;
        }

        private void dgv_ForceSensorConfigDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_ForceSensorConfigDisplay.SelectedCells[0].RowIndex >= 0)
            {
                CurrentIndex = dgv_ForceSensorConfigDisplay.SelectedCells[0].RowIndex;
                txb_ForceSensorName.Text = ParameterList[CurrentIndex].ForceSensorName;
                txb_ForceSensorDescription.Text = ParameterList[CurrentIndex].Description;
                cmb_SerialPort.Text = ParameterList[CurrentIndex].SerialPort;
                cmb_BaudRate.Text = ParameterList[CurrentIndex].BaudRate.ToString();
                cmb_ForceUnit.Text = ParameterList[CurrentIndex].ForceUnit;
                cmb_Accuracy.Text = ParameterList[CurrentIndex].Accuracy.ToString();                
            }
            else
            {
                CurrentIndex = -1;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (txb_ForceSensorName.Text == "" || txb_ForceSensorName.Text == string.Empty)
            {
                MessageBox.Show("ForceSensorName信息不能为空，请输入ForceSensorName信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ForceSensorConfig obj = null;
            obj = config.Parameter.Find(X => X.ForceSensorName == txb_ForceSensorName.Text.Trim());
            if (obj == null)
            {
                ForceSensorConfig tmp = new ForceSensorConfig();
                tmp.ForceSensorName = txb_ForceSensorName.Text.Trim();
                tmp.Description = txb_ForceSensorDescription.Text;
                tmp.SerialPort = cmb_SerialPort.Text.Trim();
                tmp.BaudRate = Convert.ToInt32(cmb_BaudRate.Text.Trim());
                tmp.ForceUnit = cmb_ForceUnit.Text.Trim();
                tmp.Accuracy = Convert.ToInt32(cmb_Accuracy.Text.Trim());

                bool exists = false;
                exists = config.Parameter.Exists(X => X.ForceSensorName == tmp.ForceSensorName);
                if (exists)
                {
                    MessageBox.Show("该ForceSensorName信息已经存在，请修改ForceSensorName信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                config.Parameter.Add(tmp);
                ParameterList = config.Parameter.ToList<ForceSensorConfig>();
                dgv_ForceSensorConfigDisplay.DataSource = null;
                dgv_ForceSensorConfigDisplay.DataSource = ParameterList.ToArray();
            }
            else
            {
                MessageBox.Show("该ForceSensorName信息已经存在，请修改对应的名称！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
  
        private void btn_Modify_Click(object sender, EventArgs e)
        {
            if (CurrentIndex == -1)
            {
                return;
            }

            ForceSensorConfig tmp = new ForceSensorConfig();
            tmp.ForceSensorName = txb_ForceSensorName.Text.Trim();
            tmp.Description = txb_ForceSensorDescription.Text;
            tmp.SerialPort = cmb_SerialPort.Text.Trim();
            tmp.BaudRate = Convert.ToInt32(cmb_BaudRate.Text.Trim());
            tmp.ForceUnit = cmb_ForceUnit.Text.Trim();
            tmp.Accuracy = Convert.ToInt32(cmb_Accuracy.Text.Trim());
            List<ForceSensorConfig> tmpForceSensorList = new List<ForceSensorConfig>(ParameterList);
            tmpForceSensorList.RemoveAt(CurrentIndex);

            bool exists = false;
            exists = tmpForceSensorList.Exists(X => X.ForceSensorName == tmp.ForceSensorName);
            if (exists)
            {
                MessageBox.Show("该ForceSensorName信息已经存在，请修改ForceSensorName信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            config.Parameter[CurrentIndex].ForceSensorName = tmp.ForceSensorName;
            config.Parameter[CurrentIndex].Description = tmp.Description;
            config.Parameter[CurrentIndex].SerialPort = tmp.SerialPort;
            config.Parameter[CurrentIndex].BaudRate = tmp.BaudRate;
            config.Parameter[CurrentIndex].ForceUnit = tmp.ForceUnit;
            config.Parameter[CurrentIndex].Accuracy = tmp.Accuracy;
            dgv_ForceSensorConfigDisplay.DataSource = null;
            dgv_ForceSensorConfigDisplay.DataSource = ParameterList;
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
                dgv_ForceSensorConfigDisplay.DataSource = null;
                dgv_ForceSensorConfigDisplay.DataSource = ParameterList;
                CurrentIndex = -1;
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string filepath = GeneralFunction.GetConfigFilePath("ForceSensorConfig.xml");
            bool res = STD_IGeneralTool.GeneralTool.TryToSave<ForceSensorConfigCollection>(config, filepath);
            if (res)
            {
                MessageBox.Show("保存参数成功！", "保存");
            }
            else
            {
                MessageBox.Show("保存参数失败！", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}