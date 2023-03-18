using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Resources;
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.Threading;
using Utility.ModifyRegistry;

using System.Runtime.InteropServices; 


namespace ScreenPaylaş
{
    public partial class XtraForm1 : DevExpress.XtraEditors.XtraForm
    {
        public XtraForm1()
        {
            InitializeComponent();
        }


        ModifyRegistry reg = new ModifyRegistry();


        public void WriteReg(string keys, string val)
        {
            Microsoft.Win32.RegistryKey key;
            key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("V2RA");
            key.SetValue(keys, val);
            key.Close();
        }

        public string ReadReg(string keys)
        {
            try
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("V2RA");
                return key.GetValue(keys).ToString();
            }
            catch
            {
                return "";
            }
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            string remember = ReadReg("Remember");
            if (remember == "1")
            {
                string id = ReadReg("V2RA_UID");
                string pw = ReadReg("V2RA_UPW");
                textEdit5.Text = id;
                textEdit4.Text = pw;
                checkEdit3.Checked = true;
            }

            uyarpanel.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            textEdit4.Properties.PasswordChar = '*';
            textEdit2.Properties.PasswordChar = '*';
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string id = ReadReg("V2RA_UID");
            string pw = ReadReg("V2RA_UPW");

            if (!textEdit5.Text.Equals(id) || !textEdit4.Text.Equals(pw))
            {
                XtraMessageBox.Show("Kullanıcı adınız veya Şifreniz Hatalı!", "Hata!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (checkEdit3.Checked){
                WriteReg("Remember", "1");
            }else{
                WriteReg("Remember", "0");
            }

            if (textEdit5.Text.Equals(id) && textEdit4.Text.Equals(pw))
            {
                this.Hide();
                Form1 frm = new Form1();
                frm.Show();
            }
           // string s = defaultLookAndFeel1.LookAndFeel.SkinName;
           // MessageBox.Show(s);
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked)
            {
                textEdit2.Properties.PasswordChar = '\0';
            }
            else
            {

                textEdit2.Properties.PasswordChar = '*';
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                textEdit4.Properties.PasswordChar = '\0';
            }
            else
            {

                textEdit4.Properties.PasswordChar = '*';
            }
        }




        bool bulundulisans = false;

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            bulundulisans = false;
            uyarpanel.Visible = false;

            if (textEdit1.Text.Length < 5)
            {
                uyarpanel.Text = " Lütfen geçerli bir kullanıcı oluşturunuz";
                uyarpanel.Visible = true;
                return;
            }

            if (textEdit2.Text.Length < 5)
            {
                uyarpanel.Text = " Şifreniz minimum 5 karakter olmalıdır.";
                uyarpanel.Visible = true;
                return;
            }

            for (int x = 0; x < listBox1.Items.Count; x++)
            {
                if (listBox1.Items[x].Equals(textEdit3.Text))
                {
                    bulundulisans = true;
                    break;
                }
            }

            if (!bulundulisans)
            {
                XtraMessageBox.Show("Lisans Kodunuz Hatalı...\nLisans kodunuzu düzeltip tekrar deneyin...\nSorun devam ediyorsa yönetici ile irtibata geçin.", "Hata!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (bulundulisans == true)
            {
                XtraMessageBox.Show("Kayıt Başarıyla Tamamlandı..\nGiriş yapabilirsiniz.", "Başarılı",MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteReg("V2RA_UID", textEdit1.Text);
                WriteReg("V2RA_UPW", textEdit2.Text);
            }


            /*
            var culture = System.Globalization.CultureInfo.CurrentCulture;
            ResourceManager rm = new ResourceManager("LKeys.resx", Assembly.GetExecutingAssembly());
            ResourceSet resourceSet = rm.GetResourceSet(new CultureInfo("tr"), true, true);
            IDictionaryEnumerator enumerator = resourceSet.GetEnumerator();
            while (enumerator.MoveNext())
            {
                MessageBox.Show(enumerator.Value.ToString());
            }
            */


            /*
           

            ResourceSet resources = ScreenPaylaş.Properties.Resources.ResourceManager.GetResourceSet(new System.Globalization.CultureInfo("en"), false, true);

            IDictionaryEnumerator resourceList = resources.GetEnumerator();

            MessageBox.Show(ScreenPaylaş.Properties.Resources.ResourceManager.GetString("String1"));
             */



          
        }

        private void XtraForm1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1.ExitProc();
        }
        
    }
}