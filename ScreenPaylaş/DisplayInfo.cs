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
    public partial class DisplayInfo : DevExpress.XtraEditors.XtraForm
    {
        public DisplayInfo()
        {
            InitializeComponent();
        }

        private void DisplayInfo_Load(object sender, EventArgs e)
        {
            listBoxControl1.Items.Clear();
            for (int x = 0; x < Screen.AllScreens.Count<Screen>(); x++)
            {
                Screen sc = Screen.AllScreens[x];

                string dvc = sc.DeviceName;
                string workingarea = sc.WorkingArea.ToString();
                string bounds = sc.Bounds.ToString();
                

                listBoxControl1.Items.Add(dvc);
                listBoxControl1.Items.Add(workingarea);
                listBoxControl1.Items.Add(bounds);
              

            }
            listBoxControl1.Items.Add("");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listBoxControl1.Items.Clear();
            for (int x = 0; x < Screen.AllScreens.Count<Screen>(); x++)
            {
                Screen sc = Screen.AllScreens[x];

                string dvc = sc.DeviceName;
                string workingarea = sc.WorkingArea.ToString();
                string bounds = sc.Bounds.ToString();


                listBoxControl1.Items.Add(dvc);
                listBoxControl1.Items.Add(workingarea);
                listBoxControl1.Items.Add(bounds);


            }
            listBoxControl1.Items.Add("");
        }
    }
}