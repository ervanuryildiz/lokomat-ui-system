using DevicesControllerApp.Ana_ekran_Login;
using DevicesControllerApp.Ayarlar;
using DevicesControllerApp.Hasta_kayit;
using DevicesControllerApp.Kullanici;
using DevicesControllerApp.Raporlama;
using DevicesControllerApp.Servis;
using DevicesControllerApp.Terapi;
using DevicesControllerApp.Veri_izleme;
using Newtonsoft.Json;
using RehabilitationSystem.Communication;
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
        private Dictionary<Button, string> orijinalButonTextleri = new Dictionary<Button, string>();

        /* private void TLPSatirGizle(TableLayoutPanel tlp, int rowIndex)
         {
             if (tlp == null) return;
             if (rowIndex < 0 || rowIndex >= tlp.RowCount) return;

             tlp.RowStyles[rowIndex].SizeType = SizeType.Absolute;
             tlp.RowStyles[rowIndex].Height = 0;

             foreach (Control c in tlp.Controls)
                 if (tlp.GetRow(c) == rowIndex)
                     c.Visible = false;
         }
        */
        /*
         private void TLPSatirGoster(TableLayoutPanel tlp, int rowIndex)
         {
             if (tlp == null) return;
             if (rowIndex < 0 || rowIndex >= tlp.RowCount) return;

             tlp.RowStyles[rowIndex].SizeType = SizeType.Absolute;
             tlp.RowStyles[rowIndex].Height = 55; // 🔹 EŞİT BOŞLUK (50–60 ideal)

             foreach (Control c in tlp.Controls)
                 if (tlp.GetRow(c) == rowIndex)
                     c.Visible = true;
         }
        */





        /*

          private void YetkiyeGoreButonlar()
          {
              // önce TÜM menüyü gizle (2–10)
              for (int i = 2; i <= 10; i++)
                  TLPSatirGizle(tlpMenu, i);

              if (_rol == "Admin")
              {
                  TLPSatirGoster(tlpMenu, 2);  // Terapi
                  TLPSatirGoster(tlpMenu, 3);  // Hasta Kayıt
                  TLPSatirGoster(tlpMenu, 4);  // Kullanıcı Kayıt
                  TLPSatirGoster(tlpMenu, 5);  // Rehabilitasyon
                  TLPSatirGoster(tlpMenu, 6);  // Raporlama
                  TLPSatirGoster(tlpMenu, 8);  // Hesabım
                  TLPSatirGoster(tlpMenu, 9);  // Ayarlar
                  TLPSatirGoster(tlpMenu, 10); // Çıkış
                                               // Servis (7) YOK
              }
              else if (_rol == "Servis")
              {
                  TLPSatirGoster(tlpMenu, 2);  // Terapi
                  TLPSatirGoster(tlpMenu, 5);  // Hasta Kayıt
                  TLPSatirGoster(tlpMenu, 6);  // Rehabilitasyon
                  TLPSatirGoster(tlpMenu, 7);  // Raporlama
                  TLPSatirGoster(tlpMenu, 8);  // Hesabım
                  TLPSatirGoster(tlpMenu, 9);
                  TLPSatirGoster(tlpMenu, 10);// Çıkış
              }
              else // DİĞER KULLANICILAR
              {
                  TLPSatirGoster(tlpMenu, 2);  // Terapi
                  TLPSatirGoster(tlpMenu, 5);  // Hasta Kayıt
                  TLPSatirGoster(tlpMenu, 6);  // Rehabilitasyon
                  TLPSatirGoster(tlpMenu, 8);  // Raporlama
                  TLPSatirGoster(tlpMenu, 9);  // Hesabım
                  TLPSatirGoster(tlpMenu, 10); // Çıkış
              }

          }

        */

        private void SetDoubleBuffered(Control c)
        {
            if (c is Panel || c is TableLayoutPanel)
            {
                c.GetType().GetProperty("DoubleBuffered",
                    System.Reflection.BindingFlags.Instance |
                    System.Reflection.BindingFlags.NonPublic)
                    .SetValue(c, true, null);
            }

            foreach (Control child in c.Controls)
                SetDoubleBuffered(child);
        }

        private Dictionary<Control, RectangleF> oranlar = new Dictionary<Control, RectangleF>();

        private void OranlariGuncelle()
        {
            foreach (Control c in panelContent.Controls)
            {
                if (!oranlar.ContainsKey(c))
                {
                    float xRatio = (float)c.Left / panelContent.Width;
                    float yRatio = (float)c.Top / panelContent.Height;
                    float wRatio = (float)c.Width / panelContent.Width;
                    float hRatio = (float)c.Height / panelContent.Height;

                    oranlar[c] = new RectangleF(xRatio, yRatio, wRatio, hRatio);
                }
            }
        }

        private void SagPaneliBoyutlandir()
        {
            panelContent.SuspendLayout(); // toplu çizimi başlat
            panelContent.ResumeLayout(false);

            foreach (var kvp in oranlar)
            {
                Control c = kvp.Key;
                RectangleF r = kvp.Value;

                c.Left = (int)(panelContent.Width * r.X);
                c.Top = (int)(panelContent.Height * r.Y);
                c.Width = (int)(panelContent.Width * r.Width);
                c.Height = (int)(panelContent.Height * r.Height);
            }

            panelContent.ResumeLayout(); // toplu çizimi tamamla
        }



        private void MenuButonlariGoster(bool goster)
        {
            foreach (var kvp in orijinalButonTextleri)
                kvp.Key.Text = goster ? kvp.Value : "";
        }

        private void MenuyuYenidenDuzenle()
        {
            var visibleControls = tlpMenu.Controls
                .Cast<Control>()
                .Where(c => c.Visible)
                .OrderBy(c => tlpMenu.GetRow(c))
                .ToList();

            if (visibleControls.Count == 0) return;

            float percent = 100f / visibleControls.Count;

            for (int i = 0; i < tlpMenu.RowCount; i++)
            {
                tlpMenu.RowStyles[i].SizeType = SizeType.Percent;
                tlpMenu.RowStyles[i].Height = 0;
            }

            foreach (var ctrl in visibleControls)
            {
                int row = tlpMenu.GetRow(ctrl);

                tlpMenu.RowStyles[row].SizeType = SizeType.Percent;
                tlpMenu.RowStyles[row].Height = percent;

                ctrl.Dock = DockStyle.Fill;
            }

            tlpMenu.PerformLayout();
        }



        private void MainForm_Resize(object sender, EventArgs e)
        {
            MenuyuYenidenDuzenle();
            OranlariGuncelle();
            SagPaneliBoyutlandir();
        }

        private void YetkiyeGoreMenu()
        {
            foreach (Control c in tlpMenu.Controls)
                c.Visible = false;

            if (_rol == "Admin")
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox8.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                btnTerapi.Visible = true;
                btnHastaKayit.Visible = true;
                btnKullaniciKayit.Visible = true;
                btnRehabilitasyonİzleme.Visible = true;
                btnRaporlama.Visible = true;
                btnHesabim.Visible = true;
                btnAyarlar.Visible = true;
                btnCikis.Visible = true;
            }
            else if (_rol == "Servis")
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                btnTerapi.Visible = true;
                btnHastaKayit.Visible = true;
                btnRehabilitasyonİzleme.Visible = true;
                btnRaporlama.Visible = true;
                btnServis.Visible = true;
                btnHesabim.Visible = true;
                btnCikis.Visible = true;
            }
            else
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox9.Visible = true;
                pictureBox10.Visible = true;
                btnTerapi.Visible = true;
                btnHastaKayit.Visible = true;
                btnRehabilitasyonİzleme.Visible = true;
                btnRaporlama.Visible = true;
                btnHesabim.Visible = true;
                btnCikis.Visible = true;
            }

            MenuyuYenidenDuzenle(); // 🔥 EN KRİTİK SATIR
        }



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
          private Size orijinalFormBoyutu;
          private bool cikisOnaylandi = false;
        
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
      ""cikis"": ""ÇIKIŞ"",
      ""device_connected"": ""Bağlı"",
      ""device_disconnected"": ""Bağlı Değil""    

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
      ""cikis"": ""EXIT"",
      ""device_connected"": ""Connected"",
      ""device_disconnected"": ""Disconnected""
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
      ""cikis"": ""خروج"",
      ""device_connected"": ""متصل"",
      ""device_disconnected"": ""غير متصل""
    }
  }";

          public MainForm(string rol, string dil)
          {
              InitializeComponent();

            this.DoubleBuffered = true;
            panelContent.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(panelContent, true, null);
            this.Resize += MainForm_Resize;

            // DeviceCommunication event abone ol
          

              this.FormClosing += MainForm_FormClosing;
              this.Load += MainForm_Load;


              _rol = rol?.Trim();     // 🔒 NULL + boşluk koruması
              secilenDil = dil;



              diller = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonData);
              MetinleriGuncelle(secilenDil);


          }

        /*  private void KaydetOrijinalKonumlar(Control parent)
          {
              foreach (Control c in parent.Controls)
              {
                  orijinalKonumlar[c] = new Rectangle(c.Location, c.Size);
                  if (c.Controls.Count > 0)
                      KaydetOrijinalKonumlar(c);
              }
          }
        */

          private void Instance_ConnectionStatusChanged(object sender, RehabilitationSystem.Communication.ConnectionEventArgs e)
          {
              // Thread güvenliği için Invoke kullan
              if (this.InvokeRequired)
              {
                  this.Invoke(new Action(() => UpdateConnectionStatus(e.IsConnected)));
              }
              else
              {
                  UpdateConnectionStatus(e.IsConnected);
              }
          }

          private void UpdateConnectionStatus(bool isConnected)
          {
              if (isConnected)
              {
                lblDeviceStatus.Text = diller[secilenDil]["device_connected"];
                lblDeviceStatus.ForeColor = Color.Green;
                  kirmizi.Visible = false;
              }
              else
              {
                lblDeviceStatus.Text = diller[secilenDil]["device_disconnected"];
                lblDeviceStatus.ForeColor = Color.Red;
                  yesil.Visible = false;
              }
          }

          private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
          {

            if (cikisOnaylandi)
                return;

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




        private void Menu_MouseEnter(object sender, EventArgs e)
        {
            if (!menuAcik && !menuTimer.Enabled)
                menuTimer.Start();
        }

        private void Menu_MouseLeave(object sender, EventArgs e)
        {
            Point mousePos = panelMenu.PointToClient(Cursor.Position);
            if (panelMenu.ClientRectangle.Contains(mousePos))
                return;

            if (menuAcik && !menuTimer.Enabled)
                menuTimer.Start();
        }

        private void MenuTimer_Tick(object sender, EventArgs e)
        {
            if (!menuAcik)
            {
                // 🔥 DAHA AÇILIRKEN TEXT GELSİN
                MenuButonlariGoster(true);

                panelMenu.Width += 15;
                if (panelMenu.Width >= menuAcikGenislik)
                {
                    panelMenu.Width = menuAcikGenislik;
                    menuAcik = true;
                    menuTimer.Stop();
                }
            }
            else
            {
                panelMenu.Width -= 15;
                if (panelMenu.Width <= menuKapaliGenislik)
                {
                    panelMenu.Width = menuKapaliGenislik;
                    menuAcik = false;
                    menuTimer.Stop();

                    // 🔽 KAPANINCA GİTSİN
                    MenuButonlariGoster(false);
                }
            }

            SagPaneliBoyutlandir();  // Menü değiştiğinde kontrolleri yeniden boyutlandır
            OranlariGuncelle();
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


            YetkiyeGoreMenu();

            SetDoubleBuffered(panelContent);

            //this.Resize += MainForm_Resize;

            //KonumlariKaydet(splitContainer2.Panel1);
            //KonumlariKaydet(splitContainer2.Panel2);

            DeviceCommunication.Instance.ConnectionStatusChanged += Instance_ConnectionStatusChanged;
            UpdateConnectionStatus(DeviceCommunication.Instance.IsConnected);



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

            this.DoubleBuffered = true;

            AddMouseEvents(panelMenu);

            foreach (Control c in tlpMenu.Controls)
            {
                if (c is Button btn && !orijinalButonTextleri.ContainsKey(btn))
                {
                    orijinalButonTextleri[btn] = btn.Text;
                }
            }

            menuTimer = new Timer();
            menuTimer.Interval = 10;
            menuTimer.Tick += MenuTimer_Tick;

            MenuButonlariGoster(false);
            YetkiyeGoreMenu();
            panelMenu.Width = menuKapaliGenislik;

            foreach (Control c in tlpMenu.Controls)
            {
                if (c is Button btn)
                {
                    btn.Dock = DockStyle.Fill;
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    btn.Padding = new Padding(10, 0, 0, 0);
                }
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

        private void btnCikis_Click(object sender, EventArgs e)
        {
            CikisSor();
            
        }

        private void CikisSor()
        {
            DialogResult cevap = MessageBox.Show(
                diller[secilenDil]["cikis_sor"],
                diller[secilenDil]["cikis"],
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                cikisOnaylandi = true;
                Application.Exit();
            }
        }
    }
}
