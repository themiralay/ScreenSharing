using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ScreenPaylaş
{
    public partial class OnTop : DevExpress.XtraEditors.XtraForm
    {
        public OnTop()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1.Show_Form();
        }

        private void OnTop_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.ExitProc();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkEdit1.Checked;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (simpleButton2.Text == "SOL AKTİF")
            {
                Form1.soltikislem = true;
                simpleButton2.Text = "SOL PASİF";
            }
            else
            {
                Form1.soltikislem = true;
                simpleButton2.Text = "SOL AKTİF";

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            if (simpleButton3.Text == "SAĞ AKTİF")
            {
                Form1.sagtikislem = true;
                simpleButton3.Text = "SAĞ PASİF";
            }
            else
            {
                Form1.sagtikislem = true;
                simpleButton3.Text = "SAĞ AKTİF";

            }
        }
    }
}