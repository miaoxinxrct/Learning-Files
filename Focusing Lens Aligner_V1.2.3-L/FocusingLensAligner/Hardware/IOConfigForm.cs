using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FocusingLensAligner
{
    public partial class IOConfigForm : Form
    {
        IOConfigCollection config = new IOConfigCollection();
        List<IOConfig> iolist = new List<IOConfig>();
        int CurrentIndex = -1;

        public IOConfigForm()
        {
            InitializeComponent();
        }

        private void IOConfigFrm_Load(object sender, EventArgs e)
        {
            string filepath = GeneralFunction.GetConfigFilePath("IOConfig.xml");
            bool res = STD_IGeneralTool.GeneralTool.ToTryLoad<IOConfigCollection>(ref config,filepath);

            if (res)
            {
                iolist = config.InputConfigs.Concat(config.OutputConfigs).ToList<IOConfig>();
                dgv_IOConfigDisplay.DataSource = iolist.ToArray();
            }
            cmb_CardID.Items.Clear();
            cmb_CardID.Items.AddRange(new object[] { 0,1,2,3});
            cmb_CardID.SelectedIndex = 0;
            cmb_PortNum.Items.Clear();
            cmb_PortNum.Items.AddRange(new object[] { 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15});
            cmb_PortNum.SelectedIndex = 0;
            cmb_Channel.Items.Clear();
            cmb_Channel.Items.AddRange(new object[] { 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15});
            cmb_Channel.SelectedIndex = 0;
            cmb_InOut.Items.Clear();
            cmb_InOut.Items.AddRange(Enum.GetNames(typeof(IOPolarity)));
            cmb_InOut.SelectedIndex = 0;    
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string filepath = GeneralFunction.GetConfigFilePath("IOConfig.xml");
            bool res = STD_IGeneralTool.GeneralTool.TryToSave<IOConfigCollection>(config,filepath);

            if (res)
            {
                MessageBox.Show("保存参数成功！", "保存");
            }
            else
            {
                MessageBox.Show("保存参数失败！", "保存", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_IOConfigDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv_IOConfigDisplay.SelectedCells[0].RowIndex>=0)
            {
                CurrentIndex = dgv_IOConfigDisplay.SelectedCells[0].RowIndex;
                txb_IOName.Text = iolist[CurrentIndex].Name;
                cmb_CardID.SelectedItem = iolist[CurrentIndex].CardID;
                cmb_InOut.SelectedItem = iolist[CurrentIndex].Polarity.ToString();
                cmb_PortNum.SelectedItem = iolist[CurrentIndex].PortNum;
                cmb_Channel.SelectedItem = iolist[CurrentIndex].ChannelNum;
                txb_Describe.Text = iolist[CurrentIndex].Description;
            }
            else
            {
                CurrentIndex = -1;
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (txb_IOName.Text==""||txb_IOName.Text==string.Empty)
            {
                MessageBox.Show("IO名称不能为空，请输入IO名称！","错误",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            IOPolarity polarity = (IOPolarity)Enum.Parse(typeof(IOPolarity),cmb_InOut.Text);
            IOConfig obj = null;
            switch (polarity)
            {
                case IOPolarity.Input:
                    obj = config.InputConfigs.Find(X=>X.Name == txb_IOName.Text.Trim());
                    break;
                case IOPolarity.Output:
                    obj = config.OutputConfigs.Find(X=>X.Name == txb_IOName.Text.Trim());
                    break;
            }
            if (obj == null)
            {
                IOConfig tmp = new IOConfig();
                tmp.Name = txb_IOName.Text.Trim();
                tmp.CardID = Convert.ToInt32(cmb_CardID.Text);
                tmp.PortNum = Convert.ToInt32(cmb_PortNum.Text);
                tmp.ChannelNum = Convert.ToInt32(cmb_Channel.Text);
                tmp.Polarity = polarity;
                tmp.Description = txb_Describe.Text;
                bool exists = false;
                switch (polarity)
                {
                    case IOPolarity.Input:
                        exists = config.InputConfigs.Exists(X=>X.CardID == tmp.CardID && X.PortNum==tmp.PortNum && X.ChannelNum==tmp.ChannelNum && X.Polarity==tmp.Polarity);
                        if (exists)
                        {
                            MessageBox.Show("该IO点位已经存在，请修改IO点位信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        config.InputConfigs.Add(tmp);
                        iolist = config.InputConfigs.Concat(config.OutputConfigs).ToList<IOConfig>();
                        dgv_IOConfigDisplay.DataSource = null;
                        dgv_IOConfigDisplay.DataSource = iolist.ToArray();
                        break;
                    case IOPolarity.Output:
                        exists = config.OutputConfigs.Exists(X =>X.Name==tmp.Name || (X.CardID == tmp.CardID && X.PortNum == tmp.PortNum && X.ChannelNum == tmp.ChannelNum && X.Polarity == tmp.Polarity));
                        if (exists)
                        {
                            MessageBox.Show("IO名称或点位已经存在，请修改对应IO名称或点位！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        config.OutputConfigs.Add(tmp);
                        iolist = config.InputConfigs.Concat(config.OutputConfigs).ToList<IOConfig>();
                        dgv_IOConfigDisplay.DataSource = null;
                        dgv_IOConfigDisplay.DataSource = iolist.ToArray();
                        break;
                }
            }
            else
            {
                MessageBox.Show("该IO名称已存在，请修改对应的名称！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btn_Modify_Click(object sender, EventArgs e)
        {
            if (CurrentIndex == -1)
            {
                return;
            }
            IOPolarity polarity = (IOPolarity)Enum.Parse(typeof(IOPolarity), cmb_InOut.Text);
            bool exists;
            IOConfig tmp = new IOConfig();
            tmp.Name = txb_IOName.Text;
            tmp.CardID = Convert.ToInt32(cmb_CardID.Text);
            tmp.Polarity = (IOPolarity)Enum.Parse(typeof(IOPolarity),cmb_InOut.Text);
            tmp.PortNum = Convert.ToInt32(cmb_PortNum.Text);
            tmp.ChannelNum = Convert.ToInt32(cmb_Channel.Text);
            tmp.Description = txb_Describe.Text;
            List<IOConfig> tmpiolist = new List<IOConfig>(iolist);
            tmpiolist.RemoveAt(CurrentIndex);
            exists = tmpiolist.Exists(X => X.Name == tmp.Name);
            if (exists)
            {
                MessageBox.Show("IO名称已经存在，请修改对应IO名称！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            exists = tmpiolist.Exists(X=>X.CardID==tmp.CardID && X.Polarity==tmp.Polarity && X.PortNum==tmp.PortNum && X.ChannelNum==tmp.ChannelNum);
            if (exists)
            {
                MessageBox.Show("IO点位已经存在，请修改对应IO点位！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            switch (polarity)
            {
                case IOPolarity.Input:
                    config.InputConfigs[CurrentIndex].Name = tmp.Name;
                    config.InputConfigs[CurrentIndex].CardID = tmp.CardID;
                    config.InputConfigs[CurrentIndex].Polarity = tmp.Polarity;
                    config.InputConfigs[CurrentIndex].PortNum = tmp.PortNum;
                    config.InputConfigs[CurrentIndex].ChannelNum = tmp.ChannelNum;
                    config.InputConfigs[CurrentIndex].Description = tmp.Description;
                    iolist = config.InputConfigs.Concat(config.OutputConfigs).ToList<IOConfig>();
                    dgv_IOConfigDisplay.DataSource = null;
                    dgv_IOConfigDisplay.DataSource = iolist;
                    break;
                case IOPolarity.Output:
                    config.OutputConfigs[CurrentIndex - config.InputConfigs.Count].Name = tmp.Name;
                    config.OutputConfigs[CurrentIndex - config.InputConfigs.Count].CardID = tmp.CardID;
                    config.OutputConfigs[CurrentIndex - config.InputConfigs.Count].Polarity = tmp.Polarity;
                    config.OutputConfigs[CurrentIndex - config.InputConfigs.Count].PortNum = tmp.PortNum;
                    config.OutputConfigs[CurrentIndex - config.InputConfigs.Count].ChannelNum = tmp.ChannelNum;
                    config.OutputConfigs[CurrentIndex - config.InputConfigs.Count].Description = tmp.Description;
                    iolist = config.InputConfigs.Concat(config.OutputConfigs).ToList<IOConfig>();
                    dgv_IOConfigDisplay.DataSource = null;
                    dgv_IOConfigDisplay.DataSource = iolist;
                    break;
            }
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
                IOPolarity polarity = (IOPolarity)Enum.Parse(typeof(IOPolarity), cmb_InOut.Text);
                switch (polarity)
                {
                    case IOPolarity.Input:
                        config.InputConfigs.RemoveAt(CurrentIndex);
                        iolist.RemoveAt(CurrentIndex);
                        break;
                    case IOPolarity.Output:
                        config.OutputConfigs.RemoveAt(CurrentIndex-config.InputConfigs.Count());
                        iolist.RemoveAt(CurrentIndex);
                        break;
                }
                dgv_IOConfigDisplay.DataSource = null;
                dgv_IOConfigDisplay.DataSource = iolist;
                CurrentIndex = -1;
            }
        }
    }
}
