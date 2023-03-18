using DevExpress.Skins;
using DevExpress.Utils;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Gallery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DevExpress.XtraEditors;

namespace ScreenPaylaş
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);
        [DllImport("user32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        int g1posx, g1posy;
        int g2posx, g2posy;


        public static void ExitProc()
        {
            TerminateProcess(new IntPtr(-1), 0);
        }

        int[] MonitorIndex = new int[2];
        bool sagsoldegis = false;
        bool tekmonitor = true;
        bool fiziii = false;

        public static bool soltikislem = false;
        public static bool sagtikislem = false;


        public static void Hide_Form()
        {
            IntPtr f = FindWindowByCaption(IntPtr.Zero, "V2RA v2.0");
            ShowWindowAsync(f, 0);
        }

        public static void Show_Form()
        {
            IntPtr f = FindWindowByCaption(IntPtr.Zero, "V2RA v2.0");
            ShowWindowAsync(f, 3); // tam ekran :D
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FakeScreen fake = new FakeScreen();
            fake.StartPosition = FormStartPosition.Manual;
            int posx = Screen.AllScreens[1].Bounds.X;
            int posy = Screen.AllScreens[1].Bounds.Y;
            fake.Location = Screen.AllScreens[1].Bounds.Location; 
            fake.Show();
        }

        string[] solresimler  = new string[255];
        string[] sagresimler = new string[255];
        int solsay = 0;
        int sagsay = 0;

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Screen.AllScreens.Count<Screen>().ToString());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int posx = Screen.AllScreens[1].Bounds.X;
            int posy = Screen.AllScreens[1].Bounds.Y;
            MessageBox.Show("2. monitor location" + " " + posx.ToString() + "," + posy.ToString());
            this.Location = new Point(posx, posy);
        }


        static Size GetThumbnailSize(Image original){
            const int maxPixels = 40;
            int originalWidth = original.Width;
            int originalHeight = original.Height;
            double factor;
            if (originalWidth > originalHeight){
            factor = (double)maxPixels / originalWidth;
            }else{
            factor = (double)maxPixels / originalHeight;
            }
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }

        public void SimdiGoster()
        {
        }

        public void setImg(Image img)
        {
            FakeScreen.picture1.Visible = true;
            FakeScreen.picture1.Image = img;
        }
        

        public void HemenSikert()
        {
            Image img;
            string[] fileEntries = Directory.GetFiles("Monitor1");
            foreach (string fileName in fileEntries)
            {
                solresimler[solsay] = fileName;
                int ind = fileName.IndexOf("\\");
                string gercekdosya = fileName.Substring(ind, fileName.Length - ind);
                gercekdosya = gercekdosya.Remove(0, 1);
                img = Image.FromFile(fileName);
                Size thumbnailSize = GetThumbnailSize(img);
                Image thumbnail = img.GetThumbnailImage(150, 90, null, IntPtr.Zero);
                int ayikla2 = gercekdosya.IndexOf(".");
                string ayiklanangif = gercekdosya.Substring(0, ayikla2);
                galleryControl1.Gallery.Groups[0].Items.Add(new GalleryItem(thumbnail, ayiklanangif, ""));
                solsay++;
            }

            Image img2;
            string[] fileEntries2 = Directory.GetFiles("Monitor2");
            foreach (string fileName2 in fileEntries2)
            {
                sagresimler[sagsay] = fileName2;
                int ind = fileName2.IndexOf("\\");
                string gercekdosya = fileName2.Substring(ind, fileName2.Length - ind);
                gercekdosya = gercekdosya.Remove(0, 1);
                img2 = Image.FromFile(fileName2);
                Size thumbnailSize = GetThumbnailSize(img2);
                Image thumbnail = img2.GetThumbnailImage(150, 90, null, IntPtr.Zero);
                int ayikla2 = gercekdosya.IndexOf(".");
                string ayiklanangif = gercekdosya.Substring(0, ayikla2);
                galleryControl2.Gallery.Groups[0].Items.Add(new GalleryItem(thumbnail, ayiklanangif, ""));
                sagsay++;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            if (Screen.AllScreens.Count<Screen>() < 2){
                barCheckItem1.Checked = true;
            }else{
                barCheckItem1.Checked = false;
            }

            //pictureEdit1.Image = Image.FromFile(@"Settings\\default2.jpg");
            //pictureEdit2.Image = Image.FromFile(@"Settings\\default2.jpg");

            g1posx = galleryControl1.Location.X;
            g1posy = galleryControl1.Location.Y;
            g2posx = galleryControl2.Location.X;
            g2posy = galleryControl2.Location.Y;

            notifyIcon1.BalloonTipTitle = "V2RA 2.0";
            notifyIcon1.BalloonTipText = "V2RA 2.0 Başarıyla Başlatıldı...";  
            notifyIcon1.ShowBalloonTip(200);
           
            MonitorIndex[0] = 1;
            MonitorIndex[1] = 2;
            panel1.Text.ToUpper();
            panel2.Text.ToUpper();

            SkinHelper.InitSkinPopupMenu(skinBarSubItem1);
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            panel1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            panel2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.galleryControl1.Gallery.ShowScrollBar = ShowScrollBar.Auto;
            this.galleryControl1.Gallery.ContentHorzAlignment = HorzAlignment.Center;
            this.galleryControl1.Gallery.ItemCheckMode = ItemCheckMode.SingleRadio;
            galleryControl1.Gallery.Groups[0].Items.Clear();
            galleryControl1.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            this.galleryControl1.Gallery.Appearance.ItemDescriptionAppearance.Normal.Font = new Font("Segoe UI Light", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.galleryControl1.Gallery.Appearance.ItemDescriptionAppearance.Normal.Options.UseFont = true;
          
            this.galleryControl2.Gallery.ShowScrollBar = ShowScrollBar.Auto;
            this.galleryControl2.Gallery.ContentHorzAlignment = HorzAlignment.Center;
            this.galleryControl2.Gallery.ItemCheckMode = ItemCheckMode.SingleRadio;
            galleryControl2.Gallery.Groups[0].Items.Clear();
            galleryControl2.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            this.galleryControl2.Gallery.Appearance.ItemDescriptionAppearance.Normal.Font = new Font("Segoe UI Light", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.galleryControl2.Gallery.Appearance.ItemDescriptionAppearance.Normal.Options.UseFont = true;

            HemenSikert();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(skinBarSubItem1.Id.ToString());
        }

        bool herseyok = false;
        int imgindex;
        bool yenidenolustur = true;
        FakeScreen fake;// = new FakeScreen();
        bool ikinciekran = true;
        FakeScreen2 fake2;
        int imgindex2;
        bool birinciekran = true;

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           
            if (simpleButton2.Text == "Göster")
            {
                simpleButton2.Text = "Gizle";

                if (tekmonitor)
                {
                    try
                    {
                        List<GalleryItem> checkedItems = this.galleryControl1.Gallery.Groups[0].GetCheckedItems();
                        imgindex = this.galleryControl1.Gallery.Groups[0].Items.IndexOf(checkedItems[0]);
                        pictureEdit1.Image = Image.FromFile(solresimler[imgindex]);
                        herseyok = true;
                    }
                    catch
                    {
                        XtraMessageBox.Show("Lütfen Görsel Seçin!", "Birşey Oldu", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        simpleButton2.Text = "Göster";
                        herseyok = false;
                    }

                }
                else
                {
                    try
                    {
                        List<GalleryItem> checkedItems = this.galleryControl2.Gallery.Groups[0].GetCheckedItems();
                        imgindex2 = this.galleryControl2.Gallery.Groups[0].Items.IndexOf(checkedItems[0]);
                        pictureEdit1.Image = Image.FromFile(sagresimler[imgindex2]);
                    }
                    catch
                    {
                        XtraMessageBox.Show("Lütfen Görsel Seçin!", "Birşey Oldu", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        simpleButton2.Text = "Göster";
                        herseyok = false;
                    }

                }
                
                if (herseyok && tekmonitor )
                {
                    if (yenidenolustur)
                    {
                        fake = new FakeScreen();
                        yenidenolustur = false;
                    }
                    fake.StartPosition = FormStartPosition.Manual;
                    FakeScreen.picture1.Image = Image.FromFile(solresimler[imgindex]);
                    fake.Location = Screen.AllScreens[1].Bounds.Location;
                    fake.Show();
                    
                }

                if (!tekmonitor)
                {
                    if (MonitorIndex[0] == 2)
                    {
                        if (ikinciekran)
                        {
                            fake2 = new FakeScreen2();
                            ikinciekran = false;
                        }

                        fake2.StartPosition = FormStartPosition.Manual;
                        FakeScreen2.picture2.Image = Image.FromFile(sagresimler[imgindex2]);
                        fake2.Location = Screen.AllScreens[MonitorIndex[2]].Bounds.Location;
                        fake2.Show();
                       
                    }

                    if (MonitorIndex[0] == 1)
                    {
                        if (birinciekran)
                        {
                            fake = new FakeScreen();
                            birinciekran = false;
                        }

                        fake.StartPosition = FormStartPosition.Manual;
                        FakeScreen.picture1.Image = Image.FromFile(solresimler[imgindex]);
                        fake.Location = Screen.AllScreens[MonitorIndex[1]].Bounds.Location;
                        fake.Show();
                        
                    }


                }

            }
            else
            {
                pictureEdit1.Image = Image.FromFile(@"Settings\\default.png");
                simpleButton2.Text = "Göster";

                if (herseyok &&  tekmonitor )
                {
                    fake.Close();
                    yenidenolustur = true;
                }

                if (!tekmonitor && MonitorIndex[0] == 2)
                {
                    fake2.Close();
                    ikinciekran = true;
                }

                if (!tekmonitor && MonitorIndex[0] == 1)
                {
                    fake.Close();
                    birinciekran = true;
                }




            }
        }

        

        private void barSubItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            HemenSikert();
            Thread.Sleep(1500);
            this.Show();
        }

        private void barToolbarsListItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void barSubItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExitProc();
            
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.PageUp)
            {
                simpleButton2_Click(null, null);
                return;
            }else
            if (e.KeyCode == Keys.Next)
            {
                simpleButton3_Click(null, null);
                return;
            }

            e.Handled = true;
        }

        private void windowsUIButtonPanel1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

        }

        
        private void simpleButton4_Click(object sender, EventArgs e)
        {

            Guid deviceID = new Guid("{36fc9e60-c465-11cf-8056-444553540000}");  // sürücü anahtarı
            string instancePath = @"USB\VID_0781&PID_557D\4C530101250919121551";  // aygıt örneği yolu.
            DeviceHelper.SetDeviceEnabled(deviceID, instancePath, true);

          //  simpleButton4.ImageOptions.Image = Image.FromFile("");
        }

        private void barSubItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            /*
            for (int x = 0; x < Screen.AllScreens.Count<Screen>(); x++)
            {
                Screen sc = Screen.AllScreens[x];

               string dvc = sc.DeviceName;
               string workingarea = sc.WorkingArea.ToString();
               string bounds = sc.Bounds.ToString();

               XtraMessageBox.Show(dvc + "\n" + workingarea + "\n" + bounds);

            }*/

            DisplayInfo dp = new DisplayInfo();
            dp.Show();
        }

        bool isdebugmod = true;

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (isdebugmod)
            {
                if (Screen.AllScreens.Count<Screen>() < 2)
                {
                    XtraMessageBox.Show("Monitör Değişimi Yapabilmeniz için birden fazla monitör bağlı olmalıdır!!", "Hata!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            tekmonitor = false;
            barCheckItem1.Checked = false;

            if (!sagsoldegis)
            {
              
                    MonitorIndex[0] = 2;
                    MonitorIndex[1] = 1;
                    sagsoldegis = true;
                    panel1.Text = "Sağ Monitör";
                    panel1.BackColor = Color.Red;
                    panel2.Text = "Sol Monitör";
                    panel2.BackColor = Color.Blue;
                    int gx = 1106;
                    int gy = 40;
                    int cx = 5;
                    int cy = 40;
                    galleryControl2.Location = new Point(cx, cy);
                    galleryControl1.Location = new Point(gx, gy);

            }
            else
            {
                    MonitorIndex[0] = 1;
                    MonitorIndex[1] = 2;
                    sagsoldegis = false;
                    panel1.Text = "Sol Monitör";
                    panel1.BackColor = Color.Blue;
                    panel2.Text = "Sağ Monitör";
                    panel2.BackColor = Color.Red;
                    int gx = 1106;
                    int gy = 40;
                    int cx = 5;
                    int cy = 40;
                    galleryControl1.Location = new Point(g1posx, g1posy);
                    galleryControl2.Location = new Point(g2posx, g2posy);

            }

        }

        private void barCheckItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (barCheckItem1.Checked)
            {
               
                tekmonitor = true;
                MonitorIndex[0] = 1;
                MonitorIndex[1] = 2;
                sagsoldegis = false;
                panel1.Text = "Sol Monitör";
                panel1.BackColor = Color.Blue;
                panel2.Text = "Sağ Monitör";
                panel2.BackColor = Color.Red;
               // int gx = 1079;
                //int gy = 39;
                //int cx = 5;
                //int cy = 37;
                galleryControl1.Location = new Point(g1posx, g1posy);
                galleryControl2.Location = new Point(g2posx, g2posy);
            }
            else
            {
                tekmonitor = false;
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            // on top
            OnTop tp = new OnTop();
            tp.Show();

        }

        int findex;

        private void simpleButton9_Click(object sender, EventArgs e)
        {

            if (tekmonitor == true)
            {

                try
                {
                    List<GalleryItem> checkedItems = this.galleryControl1.Gallery.Groups[0].GetCheckedItems();
                    if (checkedItems.Count != 0)
                    {
                        findex = this.galleryControl1.Gallery.Groups[0].Items.IndexOf(checkedItems[0]);
                        this.galleryControl1.Gallery.Groups[0].Items[++findex].Checked = true;

                        FakeScreen.picture1.Visible = true;
                        FakeScreen.picture1.Image = this.galleryControl1.Gallery.Groups[0].Items[findex].Image;
                        FakeScreen.degisimZaman = true;
                        simpleButton2_Click(null, null);
                        simpleButton2_Click(null, null);

                    }

                }
                catch
                {
                }

            }

          
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {

            if (tekmonitor == true)
            {
                try
                {
                    List<GalleryItem> checkedItems = this.galleryControl1.Gallery.Groups[0].GetCheckedItems();
                    if (checkedItems.Count != 0)
                    {
                        int index = this.galleryControl1.Gallery.Groups[0].Items.IndexOf(checkedItems[0]);
                        this.galleryControl1.Gallery.Groups[0].Items[--index].Checked = true;
                        FakeScreen.picture1.Visible = true;
                        FakeScreen.picture1.Image = this.galleryControl1.Gallery.Groups[0].Items[index].Image;
                        FakeScreen.degisimZaman = true;
                        simpleButton2_Click(null, null);
                        simpleButton2_Click(null, null);
                    }

                }
                catch
                {
                }

            }


        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            Show_Form();
        }


        int sagimgindx;
        bool solicinkontrol = true;
        FakeScreen ff;
        int solimgindx;
        bool sagicinkontrol = true;
        FakeScreen2 cc;

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (isdebugmod)
            {
                if (Screen.AllScreens.Count<Screen>() < 2)
                {
                    sagtikislem = false;
                    XtraMessageBox.Show("2. Monitör Bulunamadı..!\nLütfen 2. Monitör Bağlayınız..", "Hata!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                    return;
                }
            }

            if (simpleButton3.Text == "Göster")
            {

                if (sagsoldegis)  // yön değiştirilmiş 1. 2  - 2. 1
                {
                    try
                    {
                        List<GalleryItem> checkedItems = this.galleryControl1.Gallery.Groups[0].GetCheckedItems();
                        sagimgindx = this.galleryControl1.Gallery.Groups[0].Items.IndexOf(checkedItems[0]);
                        pictureEdit2.Image = Image.FromFile(solresimler[sagimgindx]);

                    }
                    catch
                    {
                        XtraMessageBox.Show("Lütfen Görsel Seçin!", "Birşey Oldu", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        simpleButton3.Text = "Göster";
                    }

                    if (solicinkontrol)
                    {
                        ff = new FakeScreen();
                        solicinkontrol = false;
                    }

                    ff.StartPosition = FormStartPosition.Manual;
                    FakeScreen.picture1.Image = Image.FromFile(solresimler[imgindex]);
                    ff.Location = Screen.AllScreens[1].Bounds.Location;
                    ff.Show();
                    


                }
                else
                {
                    try
                    {
                        List<GalleryItem> checkedItems = this.galleryControl2.Gallery.Groups[0].GetCheckedItems();
                        solimgindx = this.galleryControl2.Gallery.Groups[0].Items.IndexOf(checkedItems[0]);
                        pictureEdit2.Image = Image.FromFile(sagresimler[solimgindx]);

                    }
                    catch
                    {
                        XtraMessageBox.Show("Lütfen Görsel Seçin!", "Birşey Oldu", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        simpleButton3.Text = "Göster";
                    }


                    if (sagicinkontrol)
                    {
                        cc = new FakeScreen2();
                        sagicinkontrol = false;
                    }

                    cc.StartPosition = FormStartPosition.Manual;
                    FakeScreen2.picture2.Image = Image.FromFile(solresimler[imgindex]);
                    cc.Location = Screen.AllScreens[2].Bounds.Location;
                    cc.Show();
                   

                }

                simpleButton3.Text = "Gizle";
            }
            else
            {

                if (!solicinkontrol && sagsoldegis)
                {
                    ff.Close();
                    solicinkontrol = true;
                }

                if (sagicinkontrol && !sagsoldegis)
                {
                    cc.Close();
                    sagicinkontrol = true;
                }
                
                pictureEdit1.Image = Image.FromFile(@"Settings\\default.png");
                simpleButton3.Text = "Göster";
            }


        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {

        
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (soltikislem)
            {
                simpleButton2_Click(null, null);
                soltikislem = false;
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (sagtikislem)
            {
                simpleButton3_Click(null, null);
                sagtikislem = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void programKaptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitProc();
        }

        private void onTopModuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnTop tp  =new OnTop();
            this.Hide();
            tp.Show();
        }







    }
}
