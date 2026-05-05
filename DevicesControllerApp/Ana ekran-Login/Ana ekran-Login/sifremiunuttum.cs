using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void sifremi_unuttum_Load(object sender, EventArgs e)
        {

        }
    }
}
