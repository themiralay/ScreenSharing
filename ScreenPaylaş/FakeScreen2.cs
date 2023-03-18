using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScreenPaylaş
{
    public partial class FakeScreen2 : Form
    {
        public static PictureBox picture2 = new PictureBox();

        public FakeScreen2()
        {
            InitializeComponent();
        }

        private void FakeScreen2_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = picture2.Image;
        }
    }
}
