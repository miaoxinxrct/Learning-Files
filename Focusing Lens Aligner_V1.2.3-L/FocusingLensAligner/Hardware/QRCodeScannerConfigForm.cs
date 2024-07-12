using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO.Ports;

namespace FocusingLensAligner
{
    public partial class QRCodeScannerConfigForm : Form
    {
        QRCodeScannerConfigCollection config = new QRCodeScannerConfigCollection();
        List<QRCodeScannerConfig> ParameterList = new List<QRCodeScannerConfig>();
        int CurrentIndex = -1;

        public QRCodeScannerConfigForm()
        {
            InitializeComponent();
        }

        private void QRCodeScannerConfigForm_Load(object sender, EventArgs e)
        {
            string filepath = GeneralFunction.GetConfigFilePath("QRCodeScannerConfig.xml");
            var res = STD_IGeneralTool.GeneralTool.ToTryLoad<QRCodeScannerConfigCollection>(ref config, filepath);
            if (res)
            {
                ParameterList = config.Parameter.ToList<QRCodeScannerConfig>();
                dgv_QRCodeScannerConfigDisplay.DataSource = ParameterList.ToArray();
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
            string filepath = GeneralFunction.GetConfigFilePath("QRCodeScannerConfig.xml");
            bool res = STD_IGeneralTool.GeneralTool.TryToSave<QRCodeScannerConfigCollection>(config, filepath);
            if (res)
            {
                MessageBox.Show("保存参数成功！", "保存");
            }
            else
            {
                MessageBox.Show("保存参数失败！", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_QRCodeScannerConfigDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_QRCodeScannerConfigDisplay.SelectedCells[0].RowIndex >= 0)
            {
                CurrentIndex = dgv_QRCodeScannerConfigDisplay.SelectedCells[0].RowIndex;
                txb_CodeScannerName.Text = ParameterList[CurrentIndex].CodeScannerName;
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
            if (txb_CodeScannerName.Text == "" || txb_CodeScannerName.Text == string.Empty)
            {
                MessageBox.Show("CodeScannerName信息不能为空，请输入CodeScannerName信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            QRCodeScannerConfig obj = null;
            obj = config.Parameter.Find(X => X.CodeScannerName == txb_CodeScannerName.Text.Trim());
            if (obj == null)
            {
                QRCodeScannerConfig tmp = new QRCodeScannerConfig();
                tmp.CodeScannerName = txb_CodeScannerName.Text.Trim();
                tmp.Description = txb_Description.Text;
                tmp.SerialPort = cmb_SerialPort.Text.Trim();

                bool exists = false;
                exists = config.Parameter.Exists(X => X.CodeScannerName == tmp.CodeScannerName);
                if (exists)
                {
                    MessageBox.Show("该CodeScannerName信息已经存在，请修改CodeScannerName信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                config.Parameter.Add(tmp);
                ParameterList = config.Parameter.ToList<QRCodeScannerConfig>();
                dgv_QRCodeScannerConfigDisplay.DataSource = null;
                dgv_QRCodeScannerConfigDisplay.DataSource = ParameterList.ToArray();
            }
            else
            {
                MessageBox.Show("该CodeScannerName信息已经存在，请修改对应的名称！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_Modify_Click(object sender, EventArgs e)
        {
            if (CurrentIndex == -1)
            {
                return;
            }

            QRCodeScannerConfig tmp = new QRCodeScannerConfig();
            tmp.CodeScannerName = txb_CodeScannerName.Text.Trim();
            tmp.Description = txb_Description.Text;
            tmp.SerialPort = cmb_SerialPort.Text.Trim();
            List<QRCodeScannerConfig> tmpParameterList = new List<QRCodeScannerConfig>(ParameterList);
            tmpParameterList.RemoveAt(CurrentIndex);

            bool exists = false;
            exists = tmpParameterList.Exists(X => X.CodeScannerName == tmp.CodeScannerName);
            if (exists)
            {
                MessageBox.Show("该CodeScannerName信息已经存在，请修改CodeScannerName信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            config.Parameter[CurrentIndex].CodeScannerName = tmp.CodeScannerName;
            config.Parameter[CurrentIndex].Description = tmp.Description;
            config.Parameter[CurrentIndex].SerialPort = tmp.SerialPort;
            dgv_QRCodeScannerConfigDisplay.DataSource = null;
            dgv_QRCodeScannerConfigDisplay.DataSource = ParameterList;
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
                dgv_QRCodeScannerConfigDisplay.DataSource = null;
                dgv_QRCodeScannerConfigDisplay.DataSource = ParameterList;
                CurrentIndex = -1;
            }
        }
    }
}