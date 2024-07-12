using System;
using System.Windows.Forms;

namespace FocusingLensAligner
{
    public partial class PasswordPanel : Form
    {
        public delegate void DelegateSetPassword(string value);
        public event DelegateSetPassword SetPasswordEvent;

        public PasswordPanel()
        {
            InitializeComponent();
        }

        private void PasswordPanel_Load(object sender, EventArgs e)
        {
            tb_Password.PasswordChar = '*';
        }

        private void RaiseEvent(string value)
        {
            if (SetPasswordEvent!=null)
            {
                SetPasswordEvent(value);
            }
        }

        private void chkbox_ShowPasswordString_CheckedChanged(object sender, EventArgs e)
        {
            bool showflag = chkbox_ShowPasswordString.Checked;
            if (showflag)
            {
                tb_Password.PasswordChar = '\0';
            }
            else
            {
                tb_Password.PasswordChar = '*';
            }
        }

        private void btn_DeletePasswordString_Click(object sender, EventArgs e)
        {
            tb_Password.Clear();
        }

        private void btn_ConfirmPassword_Click(object sender, EventArgs e)
        {
            RaiseEvent(tb_Password.Text.Trim());
            this.Close();
        }
    }
}
