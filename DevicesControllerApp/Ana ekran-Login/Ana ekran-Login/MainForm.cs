using DevicesControllerApp.Ana_ekran_Login;
using DevicesControllerApp.Ayarlar;
using DevicesControllerApp.Hasta_kayit;
using DevicesControllerApp.Kullanici;
using DevicesControllerApp.Raporlama;
using DevicesControllerApp.Servis;
using DevicesControllerApp.Terapi;
using DevicesControllerApp.Veri_izleme;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevicesControllerApp
{
    public partial class MainForm : Form
    {



        int menuAcikGenislik = 220;
        int menuKapaliGenislik = 50;
        bool menuAcik = false;
        Timer menuTimer;


        // Kontrol + Orijinal Konum
        private Dictionary<Control, Point> sabitKonumlar =
            new Dictionary<Control, Point>();


        // MainForm sınıfının içinde (private alanlar bölümünde)
        private Dictionary<Button, Color> orijinalButonRenkleri = new Dictionary<Button, Color>();
        private Dictionary<Control, Rectangle> orijinalKonumlar = new Dictionary<Control, Rectangle>();
      

        private string _rol;

        private string secilenDil;

        private Dictionary<string, Dictionary<string, string>> diller;

        private string jsonData = @"
{
  ""türkçe"": {
    ""terapi"": ""TERAPİ"",
    ""hasta_kayit"": ""HASTA KAYIT"",
    ""kullanici_kayit"": ""KULLANICI KAYIT"",
    ""rehabilitasyon_izleme"": ""REHABİLİTASYON İZLEME"",
    ""raporlama"": ""RAPORLAMA"",
    ""servis"": ""SERVİS"",
    ""hesabim"": ""HESABIM"",
    ""ayarlar"": ""AYARLAR"",
    ""cikis_sor"": ""Uygulamadan çıkmak istiyor musunuz?"",
    ""cikis"": ""ÇIKIŞ""
  },
  ""ingilizce"": {
    ""terapi"": ""THERAPY"",
    ""hasta_kayit"": ""PATIENT REGISTRATION"",
    ""kullanici_kayit"": ""USER REGISTRATION"",
    ""rehabilitasyon_izleme"": ""REHABILITATION MONITORING"",
    ""raporlama"": ""REPORTING"",
    ""servis"": ""SERVICE"",
    ""hesabim"": ""MY ACCOUNT"",
    ""ayarlar"": ""SETTINGS"",
    ""cikis_sor"": ""Do you want to exit the application?"",
    ""cikis"": ""EXIT""
  },
  ""arapça"": {
    ""terapi"": ""العلاج"",
    ""hasta_kayit"": ""تسجيل المرضى"",
    ""kullanici_kayit"": ""تسجيل المستخدم"",
    ""rehabilitasyon_izleme"": ""متابعة إعادة التأهيل"",
    ""raporlama"": ""التقارير"",
    ""servis"": ""الخدمة"",
    ""hesabim"": ""حسابي"",
    ""ayarlar"": ""الإعدادات"",
    ""cikis_sor"": ""هل تريد الخروج من التطبيق؟"",
    ""cikis"": ""خروج""
  }
}";

        public MainForm(string rol, string dil)
        {
            InitializeComponent();


            this.FormClosing += MainForm_FormClosing;
            this.Load += MainForm_Load;
           


            _rol = rol?.Trim();     // 🔒 NULL + boşluk koruması
            secilenDil = dil;

            

            diller = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonData);
            MetinleriGuncelle(secilenDil);


        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult cevap = MessageBox.Show(
                diller[secilenDil]["cikis_sor"],
                diller[secilenDil]["cikis"],
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (cevap == DialogResult.No)
            {
                e.Cancel = true; // ❌ KAPANMA
            }
        }

       



        private void MetinleriGuncelle(string secilenDil)
        {
            btnTerapi.Text = diller[secilenDil]["terapi"];
            btnHastaKayit.Text = diller[secilenDil]["hasta_kayit"];
            btnKullaniciKayit.Text = diller[secilenDil]["kullanici_kayit"];
            btnRehabilitasyonİzleme.Text = diller[secilenDil]["rehabilitasyon_izleme"];
            btnRaporlama.Text = diller[secilenDil]["raporlama"];
            btnServis.Text = diller[secilenDil]["servis"];
            btnHesabim.Text = diller[secilenDil]["hesabim"];
            btnAyarlar.Text = diller[secilenDil]["ayarlar"];
            btnCikis.Text = diller[secilenDil]["cikis"];
        }





        /*
        private void MenuAc()
        {
            splitContainer1.SplitterDistance = menuAcikGenislik;
            menuAcik = true;
          
        }
     

        private void MenuKapat()
        {
            splitContainer1.SplitterDistance = menuKapaliGenislik;
            menuAcik = false;
          
        }*/

   


        /*
        private void Menu_MouseEnter(object sender, EventArgs e)
        {
            MenuAc();
        }

        private void Menu_MouseLeave(object sender, EventArgs e)
        {
            // Fare hâlâ panelin içindeyse kapatma
            Point mousePos = splitContainer1.Panel1.PointToClient(Cursor.Position);
            if (!splitContainer1.Panel1.ClientRectangle.Contains(mousePos))
            {
                MenuKapat();
            }
        }*/


        private void Menu_MouseEnter(object sender, EventArgs e)
        {
            if (!menuAcik && !menuTimer.Enabled)
                menuTimer.Start();
        }

        private void Menu_MouseLeave(object sender, EventArgs e)
        {
            Point mousePos = splitContainer1.Panel1.PointToClient(Cursor.Position);
            if (splitContainer1.Panel1.ClientRectangle.Contains(mousePos))
                return;

            if (menuAcik && !menuTimer.Enabled)
                menuTimer.Start();
        }

        private void MenuTimer_Tick(object sender, EventArgs e)
        {

            //pictureBox14.Size = splitContainer1.Panel2.ClientSize;


            if (!menuAcik)
            {
                splitContainer1.SplitterDistance += 25;
                if (splitContainer1.SplitterDistance >= menuAcikGenislik)
                {
                    splitContainer1.SplitterDistance = menuAcikGenislik;
                    menuAcik = true;
                    menuTimer.Stop();
                    MenuTextGoster(true);
                }
            }
            else
            {
                splitContainer1.SplitterDistance -= 25;
                if (splitContainer1.SplitterDistance <= menuKapaliGenislik)
                {
                    splitContainer1.SplitterDistance = menuKapaliGenislik;
                    menuAcik = false;
                    menuTimer.Stop();
                    MenuTextGoster(false);
                }
            }
        }



        private void MenuTextGoster(bool goster)
        {
            if (diller == null) return;
            if (!diller.ContainsKey(secilenDil)) return;

            foreach (Control c in splitContainer1.Panel1.Controls)
            {
                if (c is Button btn)
                {
                    if (btn.Tag == null) continue;

                    string key = btn.Tag.ToString();

                    if (!diller[secilenDil].ContainsKey(key)) continue;

                    btn.Text = goster
                        ? diller[secilenDil][key]
                        : "";
                }
            }
        }




        // ✅ Recursive metod: Panel + altındaki tüm kontroller
        private void AddMouseEvents(Control parent)
        {
            parent.MouseEnter += Menu_MouseEnter;
            parent.MouseLeave += Menu_MouseLeave;

            foreach (Control c in parent.Controls)
                AddMouseEvents(c);
        }


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void SetKullaniciAdi(string kullaniciAdi)
        {
            labelKullaniciAdi.Text = kullaniciAdi;
        }

     


        private void MainForm_Load(object sender, EventArgs e)
        {

            //this.Resize += MainForm_Resize;

            //KonumlariKaydet(splitContainer2.Panel1);
            //KonumlariKaydet(splitContainer2.Panel2);

            if (secilenDil == "türkçe")
            {
                labelDil.Text = "TR";
                pictureBox3.Visible = false;
                pictureBox12.Visible = false;
            }
            if (secilenDil == "ingilizce")
            {
                labelDil.Text = "EN";
                pictureBox3.Visible = false;
                pictureBox13.Visible = false;
            }
            if (secilenDil == "arapça")
            {
                labelDil.Text = "AR";
                pictureBox12.Visible = false;
                pictureBox13.Visible = false;
            }

            lblTarihSaat.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            timerSaat.Start();

            statusStrip1.Font = new Font("Segoe UI", 12F);

            YetkiyeGoreButonlar();

            foreach (Control c in splitContainer1.Panel1.Controls)
            {
                if (c is Button btn)
                {
                    orijinalButonRenkleri[btn] = btn.BackColor; // Orijinal rengi sakla
                    btn.MouseEnter += MenuButon_MouseEnter;
                    btn.MouseLeave += MenuButon_MouseLeave;
                }
            }


            this.DoubleBuffered = true;


            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer2.Dock = DockStyle.Fill;

            if (!splitContainer1.Panel2.Controls.Contains(splitContainer2))
                splitContainer1.Panel2.Controls.Add(splitContainer2);

            splitContainer2.Dock = DockStyle.Fill;


            splitContainer1.SplitterDistance = menuKapaliGenislik;

          //  pictureBox14.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;


            AddMouseEvents(splitContainer1.Panel1);

      

            menuTimer = new Timer();
            menuTimer.Interval = 10;
            menuTimer.Tick += MenuTimer_Tick;

            MenuTextGoster(false);

        }

        /*
        private void MainForm_Resize(object sender, EventArgs e)
        {  
            pictureBox14.Size = splitContainer1.Panel2.ClientSize; // Panel2'yi doldur
            pictureBox14.Location = new Point(0, 0); // sol üst köşe
            
        }*/





        /*
        private void AnaEkran_Resize(object sender, EventArgs e)
        {
            btnRehabilitasyonİzleme.Font = new Font(btnTerapi.Font.FontFamily, 13, btnTerapi.Font.Style);
            btnRehabilitasyonİzleme.Text = diller[secilenDil]["rehabilitasyon_izleme"];


            if (orijinalFormBoyutu.Width == 0 || orijinalFormBoyutu.Height == 0)
                return;

            float oranX = (float)this.Width / orijinalFormBoyutu.Width;
            float oranY = (float)this.Height / orijinalFormBoyutu.Height;

            foreach (var kvp in orijinalKonumlar)
            {
                Control c = kvp.Key;
                Rectangle r = kvp.Value;

                int yeniX = (int)(r.X * oranX);
                int yeniY = (int)(r.Y * oranY);
                int yeniWidth = (int)(r.Width * oranX);
                int yeniHeight = (int)(r.Height * oranY);

                c.Bounds = new Rectangle(yeniX, yeniY, yeniWidth, yeniHeight);

                if (c is TextBox txt)
                {
                    float yeniFontSize = txt.Font.Size * Math.Min(oranX, oranY);
                    txt.Font = new Font(txt.Font.FontFamily, yeniFontSize, txt.Font.Style);
                }
            }
        }*/


        /*
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (orijinalFormBoyutu.Width == 0 || orijinalFormBoyutu.Height == 0) return;

            float oranX = (float)this.Width / orijinalFormBoyutu.Width;
            float oranY = (float)this.Height / orijinalFormBoyutu.Height;

            foreach (var kvp in orijinalKontrolBoyutlari)
            {
                Control c = kvp.Key;
                Rectangle r = kvp.Value;

                c.Bounds = new Rectangle(
                    (int)(r.X * oranX),
                    (int)(r.Y * oranY),
                    (int)(r.Width * oranX),
                    (int)(r.Height * oranY)
                );

                // Fontları da ölçeklendir
                if (c is Label lbl)
                {
                    lbl.Font = new Font(lbl.Font.FontFamily, r.Height * 0.5f, lbl.Font.Style);
                }
                else if (c is TextBox txt)
                {
                    txt.Font = new Font(txt.Font.FontFamily, r.Height * 0.5f, txt.Font.Style);
                }
            }
        }
        */



        private void YetkiyeGoreButonlar()
        {
    

            btnKullaniciKayit.Visible = false;
            btnAyarlar.Visible = false;
            btnServis.Visible = false;
            pictureBox4.Visible = false;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;

            if (_rol == "Admin")
            {
                btnKullaniciKayit.Visible = true;
                btnAyarlar.Visible = true;
                pictureBox4.Visible= true;
                pictureBox8.Visible = true;

           
            }
            else if (_rol == "Servis")
            {
                btnServis.Visible = true;
                pictureBox7.Visible = true;

         
            }

            if(_rol != "Servis" && _rol != "Admin")
            {
               
            }
        }


        
        private void MenuButon_MouseEnter(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.FromArgb(245, 245, 245);
        }

        private void MenuButon_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (orijinalButonRenkleri.ContainsKey(btn))
                btn.BackColor = orijinalButonRenkleri[btn]; // Orijinal rengi geri yükle
        }
        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelKullaniciAdi_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTarihSaat.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lblTarihSaat_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {



        }

        private void btnRehabilitasyonİzleme_Click(object sender, EventArgs e)
        {

        }

        private void btnRaporlama_Click(object sender, EventArgs e)
        {

        }

        private void btnKullaniciKayit_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click_2(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }
    }
}
