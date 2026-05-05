using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevicesControllerApp.Database;

namespace DevicesControllerApp
{
    public partial class sifremiunuttum : Form
    {
        private string secilenDil;
        private bool cikisSorulacak = true;


        private Dictionary<string, Dictionary<string, string>> diller;

        private string jsonData = @"
{
  ""türkçe"": {
    ""cikis_sor"": ""Uygulamadan çıkmak istiyor musunuz?"",
    ""cikis"": ""ÇIKIŞ""
  },
  ""english"": {
    ""cikis_sor"": ""Do you want to exit the application?"",
    ""cikis"": ""EXIT""
  },
  ""arapça"": {
    ""cikis_sor"": ""هل تريد الخروج من التطبيق؟"",
    ""cikis"": ""خروج""
  }
}";


        public sifremiunuttum()
        {
            InitializeComponent();
            this.FormClosing += sifremiunuttum_FormClosing;
            this.Shown += sifremiunuttum_Shown; // 👈 BURASI ÖNEMLİ

        }

        private void sifremiunuttum_Shown(object sender, EventArgs e)
        {
            this.ActiveControl = null;   // hiçbir textbox odak almaz
            this.Focus();               // formun kendisi odak alır
        }


        private void sifremiunuttum_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cikisSorulacak) return;

            DialogResult cevap = MessageBox.Show(
                "Çıkmak istiyor musunuz?",
                "Çıkış",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (cevap == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnSifreGuncelle_Click(object sender, EventArgs e)
        {
            if (txtKullaniciAdi.Text == "" ||
                txtYeniSifre.Text == "" ||
                txtYeniSifreTekrar.Text == "")
            {
                MessageBox.Show("Tum alanlari doldurunuz");
                return;
            }

            if (txtYeniSifre.Text != txtYeniSifreTekrar.Text)
            {
                MessageBox.Show("Yeni sifreler uyusmuyor");
                return;
            }

            bool sonuc = DatabaseManager.Instance.ChangePasswordByUsername(
                txtKullaniciAdi.Text,
                txtYeniSifre.Text
            );

            if (sonuc)
            {
                MessageBox.Show("Sifre basariyla guncellendi");
                this.Close();
            }
            else
            {
                MessageBox.Show("Kullanici adi veya eski sifre hatali");
            }
        }
        private void SetPlaceholder(System.Windows.Forms.TextBox textBox, string placeholder)
        {
            textBox.Text = placeholder;
            textBox.ForeColor = Color.Gray;

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }



        private void sifremi_unuttum_Load(object sender, EventArgs e)
        {
            this.AcceptButton = btnSifreGuncelle;

            txtYeniSifre.UseSystemPasswordChar = true;
            txtYeniSifreTekrar.UseSystemPasswordChar = true;

            panelCard.Location = new Point(100, 90);

            SetPlaceholder(txtKullaniciAdi, "Kullanıcı Adı");
            SetPlaceholder(txtYeniSifre, "Yeni Şifre");
            SetPlaceholder(txtYeniSifreTekrar, "Yeni Şifre Tekrar");

            this.ActiveControl = null; // 👈 İMLEÇ HİÇBİR TEXTBOX'TA OLMAZ
        }

        private void panelCard_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, panelCard.Width, panelCard.Height);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int radius = 25; // Köşe yumuşaklığı
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            panelCard.Region = new Region(path);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            txtYeniSifre.UseSystemPasswordChar = !txtYeniSifre.UseSystemPasswordChar;

        }


        private void button3_Click_1(object sender, EventArgs e)
        {
            txtYeniSifreTekrar.UseSystemPasswordChar = !txtYeniSifreTekrar.UseSystemPasswordChar;

        }



        private void btnSifreGuncelle_Click_1(object sender, EventArgs e)
        {

            if (txtKullaniciAdi.Text == "" ||
                txtYeniSifre.Text == "" ||
                txtYeniSifreTekrar.Text == "")
            {
                MessageBox.Show("Tum alanlari doldurunuz");
                return;
            }

            if (txtYeniSifre.Text != txtYeniSifreTekrar.Text)
            {
                MessageBox.Show("Yeni sifreler uyusmuyor");
                return;
            }

            bool sonuc = DatabaseManager.Instance.ChangePasswordByUsername(
                txtKullaniciAdi.Text,
                txtYeniSifre.Text
            );

            if (sonuc)
            {
                MessageBox.Show("Sifre basariyla guncellendi");
                this.Close();
            }
            else
            {
                MessageBox.Show("Kullanici adi veya eski sifre hatali");
            }
        }
    }
}
