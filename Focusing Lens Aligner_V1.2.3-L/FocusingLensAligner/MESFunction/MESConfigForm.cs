using System;
using System.Windows.Forms;

namespace FocusingLensAligner
{
    public partial class MESConfigForm : Form
    {
        //MESConfig mesconfig = new MESConfig();
        public MESConfigForm()
        {
            InitializeComponent();
        }

        private void MESConfigForm_Load(object sender, EventArgs e)
        {
            bool res = false;
            
            res = LoadMESConfig();
            if (res == false)
            {
                MessageBox.Show("Load MES Config Fail", "Error");
            }
            else
            {
                if (rbtn_SingleValid.Checked == false)
                {
                    tb_StepName.Enabled = false;
                    chkbox_MoveIn.Enabled = false;
                }
                if (rbtn_MultiValid.Checked==false)
                {
                    tb_ProductName.Enabled = false;
                    tb_MultiStepName.Enabled = false;
                    chkbox_MultiMoveIn.Enabled = false;
                    btn_Add.Enabled = false;
                    btn_Delete.Enabled = false;
                    dgv_StepVal.Enabled = false;
                }
                if (rbtn_DefineEnvr.Checked==false)
                {
                    tb_ServerName.Enabled = false;
                    tb_DatabaseName.Enabled = false;
                    tb_UserName.Enabled = false;
                    tb_Password.Enabled = false;
                }
            }
        }

        private void rbtn_SingleValid_Click(object sender, EventArgs e)
        {
            bool check = rbtn_SingleValid.Checked;
            if (check)
            {
                rbtn_MultiValid.Checked = false;
                tb_StepName.Enabled = true;
                chkbox_MoveIn.Enabled = true;

                tb_ProductName.Enabled = false;
                tb_MultiStepName.Enabled = false;
                chkbox_MultiMoveIn.Enabled = false;
                btn_Add.Enabled = false;
                btn_Delete.Enabled = false;
                dgv_StepVal.Enabled = false;
            }
        }

        private void rbtn_MultiValid_Click(object sender, EventArgs e)
        {
            bool check = rbtn_MultiValid.Checked;
            if (check)
            {
                rbtn_SingleValid.Checked = false;
                tb_ProductName.Enabled = true;
                tb_MultiStepName.Enabled = true;
                chkbox_MultiMoveIn.Enabled = true;
                btn_Add.Enabled = true;
                btn_Delete.Enabled = true;
                dgv_StepVal.Enabled = true;

                tb_StepName.Enabled = false;
                chkbox_MoveIn.Enabled = false;

            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (tb_ProductName.Text==""||tb_MultiStepName.Text=="")
            {
                MessageBox.Show("Product name and step name can't be null.", "Warning");
                return;
            }
            
            MultiStep multistep = GlobalParameters.MESConfig.multistep.Find(X => X.productname == tb_ProductName.Text);
            if (multistep==null)
            {
                multistep = new MultiStep();
                multistep.productname = tb_ProductName.Text;
                multistep.stepname = tb_MultiStepName.Text.ToString().Trim();
                multistep.CheckMoveIn = chkbox_MultiMoveIn.Checked;
                GlobalParameters.MESConfig.multistep.Add(multistep);
                dgv_StepVal.DataSource = GlobalParameters.MESConfig.multistep.ToArray();
              //  dgv_StepVal.Rows.Add(multistep);
            }
            else
            {
                MessageBox.Show("Product name already exists","Warning");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int index = dgv_StepVal.CurrentRow.Index;
            if (index == -1)
            {
                return;
            }
            else
            {
                GlobalParameters.MESConfig.multistep.RemoveAt(index);
                dgv_StepVal.DataSource = GlobalParameters.MESConfig.multistep.ToArray();
            }           
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            bool res = false;
            res = SaveMESConfig();
            if (res)
            {
                MessageBox.Show("Save MES Config Success","Success");
            }
            else
            {
                MessageBox.Show("Save MES Config Fail", "Error");
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void rbtn_ProductEnvr_Click(object sender, EventArgs e)
        {
            bool check = rbtn_ProductEnvr.Checked;
            if (check)
            {
                rbtn_TestEnvr.Enabled = false;
                rbtn_DefineEnvr.Enabled = false;
                tb_ServerName.Enabled = false;
                tb_DatabaseName.Enabled = false;
                tb_UserName.Enabled = false;
                tb_Password.Enabled = false;
            }
        }

        private void rbtn_TestEnvr_Click(object sender, EventArgs e)
        {
            bool check = rbtn_TestEnvr.Checked;
            if (check)
            {
                rbtn_ProductEnvr.Enabled = false;
                rbtn_DefineEnvr.Enabled = false;
                tb_ServerName.Enabled = false;
                tb_DatabaseName.Enabled = false;
                tb_UserName.Enabled = false;
                tb_Password.Enabled = false;
            }
        }

        private void tb_DefineEnvr_Click(object sender, EventArgs e)
        {
            bool check = rbtn_DefineEnvr.Checked;
            if (check)
            {
                rbtn_ProductEnvr.Checked = false;
                rbtn_TestEnvr.Checked = false;
                tb_ServerName.Enabled = true;
                tb_DatabaseName.Enabled = true;
                tb_UserName.Enabled = true;
                tb_Password.Enabled = true;
            }
            else
            {
                tb_ServerName.Enabled = false;
                tb_DatabaseName.Enabled = false;
                tb_UserName.Enabled = false;
                tb_Password.Enabled = false;
            }
        }

        public void LoadMESConfigtoForm(MESConfig config)
        {
            rbtn_SingleValid.Checked = config.singleStepFlag;
            tb_StepName.Text = config.singlestep.stepname;
            chkbox_MoveIn.Checked = config.singlestep.CheckMoveIn;

            rbtn_MultiValid.Checked = config.multiStepFlag;
            dgv_StepVal.DataSource = config.multistep.ToArray();

            rbtn_ProductEnvr.Checked = config.envrinfo.productEnvrFlag;
            rbtn_TestEnvr.Checked = config.envrinfo.testEnvrFlag;
            rbtn_DefineEnvr.Checked = config.envrinfo.defineEnvrFlag;
            tb_ServerName.Text = config.envrinfo.serverName;
            tb_DatabaseName.Text = config.envrinfo.databaseName;
            tb_UserName.Text = config.envrinfo.userName;
            tb_UserName.Text = config.envrinfo.userPassword;

            tb_ServerPath.Text = config.downloadinfo.serverPath;
            chkbox_AutoLoadConfig.Checked = config.downloadinfo.autoDownload;

            chkbox_InitialMES.Checked = config.InitialMES;
        }

        public void SaveMESConfigfromForm(ref MESConfig config)
        {
            config.singleStepFlag = rbtn_SingleValid.Checked;
            config.singlestep.stepname = tb_StepName.Text;
            config.singlestep.CheckMoveIn = chkbox_MoveIn.Checked;

            MultiStep multistep = new MultiStep();
            config.multiStepFlag = rbtn_MultiValid.Checked;
            for (int i=0;i<dgv_StepVal.RowCount;i++)
            {
                multistep.productname = dgv_StepVal.Rows[i].Cells[0].Value.ToString();
                multistep.stepname = dgv_StepVal.Rows[i].Cells[1].Value.ToString();
               
                if (dgv_StepVal.Rows[i].Cells[2].Value.ToString().ToLower()=="true")
                {
                    multistep.CheckMoveIn = true;
                }
                else if (dgv_StepVal.Rows[i].Cells[2].Value.ToString().ToLower() == "false")
                {
                    multistep.CheckMoveIn = false;
                }
            }

            config.envrinfo.productEnvrFlag = rbtn_ProductEnvr.Checked;
            config.envrinfo.testEnvrFlag = rbtn_TestEnvr.Checked;
            config.envrinfo.defineEnvrFlag = rbtn_DefineEnvr.Checked;
            config.envrinfo.serverName = tb_ServerName.Text;
            config.envrinfo.databaseName = tb_DatabaseName.Text;
            config.envrinfo.userName = tb_UserName.Text;
            config.envrinfo.userPassword = tb_UserName.Text;

            config.downloadinfo.serverPath = tb_ServerPath.Text;
            config.downloadinfo.autoDownload = chkbox_AutoLoadConfig.Checked;

            config.InitialMES = chkbox_InitialMES.Checked;
        }

        public bool LoadMESConfig()
        {
            bool res = false;
            string filepath = GeneralFunction.GetConfigFilePath("Camstar\\" + "CamstarConfig.xml");
            res = STD_IGeneralTool.GeneralTool.ToTryLoad<MESConfig>(ref GlobalParameters.MESConfig, filepath);

            //Load Config to Form
            if (res)
            {
                LoadMESConfigtoForm(GlobalParameters.MESConfig);
            }
            return res;
        }

        public bool SaveMESConfig()
        {
            bool res = false;
            string filepath = GeneralFunction.GetConfigFilePath("Camstar\\" + "CamstarConfig.xml");
            SaveMESConfigfromForm(ref GlobalParameters.MESConfig);
            res = STD_IGeneralTool.GeneralTool.TryToSave<MESConfig>(GlobalParameters.MESConfig, filepath);
            return res;
        }
    }
}
