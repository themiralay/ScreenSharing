using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenPaylaş
{
    public partial class FakeScreen : Form
    {
        public static PictureBox picture1 = new PictureBox();
        public static bool degisimZaman = false;

        public FakeScreen()
        {
            InitializeComponent();

           // pictureBox1.Image = Image.FromFile(@"forforik.gif");
           // this.Location = new Point(Screen.AllScreens[0].Bounds.Left + 10, 10);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void FakeScreen_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = picture1.Image;
        }

        private void FakeScreen_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (degisimZaman)
            {
                pictureBox1.Image = picture1.Image;
                pictureBox1.Refresh();
                degisimZaman = false;
            }
        }
    }
}
