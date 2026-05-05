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
using Newtonsoft.Json;



namespace DevicesControllerApp.Ana_ekran_Login
{


    public partial class Form2 : Form
    {
      

        Dictionary<Button, int> buttonLefts = new Dictionary<Button, int>();

        int menuOpenWidth = 325;
        int menuClosedWidth = 85;
        bool isMenuOpen = true;
        bool isMouseInsideMenu = false;


        private Dictionary<Control, Rectangle> originalControlBounds;
        private string secilenDil = "türkçe";

        private string jsonData = @"
        {
          ""türkçe"": {
            ""kullanici_adi"": ""Kullanıcı Adı"",
            ""sifre"": ""Şifre"",
            ""giris"": ""Giriş"",
            ""cikis"": ""Çıkış"",
            ""beni_hatirla"": ""Beni Hatırla"",
            ""sifre_sifirlama2"": ""Şifremi Unuttum"",
            ""sifre_goster"": ""Şifreyi Göster"",
            ""bos_kulanici_adi"": ""Kullanıcı adınızı girmediniz."",
            ""bos_sifre"": ""Şifrenizi girmediniz."",
            ""bos_kullanici_adi_ve_sifre"": ""Kullanıcı adınızı ve şifrenizi girmediniz."",
            ""yanlis_kullanici_adi"": ""Kullanıcı adınızı yanlış girdiniz."",
            ""yanlis_sifre"": ""Şifrenizi yanlış girdiniz."",
            ""yanlis_kullanici_adi_ve_sifre"": ""Kullanıcı adınızı ve şifrenizi yanlış girdiniz."",
            ""uc_kez_hatali_giris"":""3 kez yanlış girdiniz. Lütfen 10 saniye bekleyin."",
            ""cikis_sor"": ""Uygulamadan çıkmak istiyor musunuz?"",
            ""cikis"": ""Çıkış"",
            ""sifre_sifirlama"":  ""Şifre sıfırlama işlemi burada yapılacaktır."",
            ""bilgi"": ""Bilgi"",
            ""bekleme_mesaji"": ""Lütfen {0} saniye bekleyin..."",
            ""giris_basarili"": ""Giriş başarılı"",
            ""placeholder_kullanici_adi"": ""kullanıcı adı"",
            ""placeholder_sifre"": ""şifre"",
            ""veritabani_hata"": ""Veritabanına bağlanılamadı. Lütfen daha sonra tekrar deneyin.""

          },
          ""ingilizce"": {
            ""kullanici_adi"": ""Username"",
            ""sifre"": ""Password"",
            ""giris"": ""Login"",
            ""cikis"": ""Exit"",
            ""beni_hatirla"": ""Remember Me"",
            ""sifre_sifirlama2"": ""Forgot Password"",
            ""sifre_goster"": ""Show Password"",
            ""bos_kullanici_adi"": ""You didn't enter your username."",
            ""bos_sifre"": ""You didn't enter your password."",
            ""bos_kullanici_adi_ve_sifre"": ""You didn't enter your username and password."",
            ""yanlis_kullanici_adi"": ""Wrong username."",
            ""yanlis_sifre"": ""Wrong password."",
            ""yanlis_kullanici_adi_ve_sifre"": ""Wrong username and password."",
            ""uc_kez_hatali_giris"": ""3 wrong attempts. Please wait 10 seconds."",
            ""cikis_sor"": ""Do you want to exit the application?"",
            ""cikis"": ""Exit"",
            ""sifre_sifirlama"": ""Password reset will be done here."",
            ""bilgi"": ""Information"",
            ""bekleme_mesaji"": ""Please wait {0} seconds..."",
            ""giris_basarili"": ""Login succesfull"",
            ""placeholder_kullanici_adi"": ""username"",
            ""placeholder_sifre"": ""password"",
            ""veritabani_hata"": ""Cannot connect to the database. Please try again later.""
            },
            ""arapça"": {
            ""kullanici_adi"": ""اسم المستخدم"",
            ""sifre"": ""كلمة المرور"",
            ""giris"": ""تسجيل الدخول"",
            ""cikis"": ""خروج"",
            ""beni_hatirla"": ""تذكرني"",
            ""sifre_sifirlama2"": ""نسيت كلمة المرور"",
            ""sifre_goster"": ""إظهار كلمة المرور"",
            ""bos_kullanici_adi"": ""لم تقم بإدخال اسم المستخدم."",
            ""bos_sifre"": ""لم تقم بإدخال كلمة المرور."",
            ""bos_kullanici_adi_ve_sifre"": ""لم تقم بإدخال اسم المستخدم وكلمة المرور."",
            ""yanlis_kullanici_adi"": ""اسم المستخدم غير صحيح."",
            ""yanlis_sifre"": ""كلمة المرور غير صحيحة."",
            ""yanlis_kullanici_adi_ve_sifre"": ""اسم المستخدم وكلمة المرور غير صحيحين."",
            ""uc_kez_hatali_giris"": ""ثلاث محاولات خاطئة. يرجى الانتظار 10 ثوانٍ."",
            ""cikis_sor"": ""هل تريد الخروج من التطبيق؟"",
            ""cikis"": ""خروج"",
            ""sifre_sifirlama"": ""سيتم تنفيذ إعادة تعيين كلمة المرور هنا."",
            ""bilgi"": ""معلومات"",
            ""bekleme_mesaji"": ""يرجى الانتظار {0} ثانية..."",
            ""giris_basarili"": ""تم تسجيل الدخول بنجاح"",
            ""placeholder_kullanici_adi"": ""اسم المستخدم"",
            ""placeholder_sifre"": ""كلمة المرور"",
            ""veritabani_hata"": ""تعذر الاتصال بقاعدة البيانات. يرجى المحاولة مرة أخرى لاحقًا.""
            }

         
        }";

        private Dictionary<string, Dictionary<string, string>> diller;

        public Form2()
        {
            InitializeComponent();
            this.DoubleBuffered = true;



      

        }


        private void Form2_Load(object sender, EventArgs e)
        {
            /*
            panelMenu.Width = menuOpenWidth;

            panelMenu.MouseEnter += PanelMenu_MouseEnter;
            panelMenu.MouseLeave += PanelMenu_MouseLeave;

            foreach (Control ctrl in panelMenu.Controls)
            {
                ctrl.MouseEnter += PanelMenu_MouseEnter;
                ctrl.MouseLeave += PanelMenu_MouseLeave;
            }*/

        


            buttonLefts.Clear();

            foreach (Control c in panelMenu.Controls)
            {
                if (c is Button btn)
                {
                    buttonLefts[btn] = btn.Left; // ilk konumu KAYDET
                }
            }


            DisableAllPictureBoxes(panelMenu);





            panelMenu.Width = menuClosedWidth;
            isMenuOpen = false;
            MenuButtonLayout(false);
            // sadece ikonlar

            // TÜM BUTONLAR (tek tek, net)
            terapi.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            hasta_kayit.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            kullanici_kayit.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            rehabilitasyon_izleme.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            raporlama.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            servis.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            hesabim.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            ayarlar.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            cikis.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            // Yazı kesilmesin
            terapi.AutoEllipsis = true;
            hasta_kayit.AutoEllipsis = true;
            kullanici_kayit.AutoEllipsis = true;
            rehabilitasyon_izleme.AutoEllipsis = true;
            raporlama.AutoEllipsis = true;
            servis.AutoEllipsis = true;
            hesabim.AutoEllipsis = true;
            ayarlar.AutoEllipsis = true;
            cikis.AutoEllipsis = true;

            terapi.UseCompatibleTextRendering = true;
            hasta_kayit.UseCompatibleTextRendering = true;
            kullanici_kayit.UseCompatibleTextRendering = true;
            rehabilitasyon_izleme.UseCompatibleTextRendering = true;
            raporlama.UseCompatibleTextRendering = true;
            servis.UseCompatibleTextRendering = true;
            hesabim.UseCompatibleTextRendering = true;
            ayarlar.UseCompatibleTextRendering = true;
            cikis.UseCompatibleTextRendering = true;


            // Mouse ile aç-kapa çalışsın
            panelMenu.MouseEnter += PanelMenu_MouseEnter;
            panelMenu.MouseLeave += PanelMenu_MouseLeave;


            foreach (Control ctrl in panelMenu.Controls)
            {
                ctrl.MouseEnter += PanelMenu_MouseEnter;
                ctrl.MouseLeave += PanelMenu_MouseLeave;
            }

        }

        
     void MenuButtonLayout(bool menuOpen)
     {
         foreach (Control c in panelMenu.Controls)
         {
             if (c is Button btn) { 
             btn.Left = buttonLefts[btn];
             btn.TextImageRelation = TextImageRelation.ImageBeforeText;

                 if (menuOpen)
             {
                 btn.TextAlign = ContentAlignment.MiddleLeft;
                 btn.ImageAlign = ContentAlignment.MiddleLeft;
                 btn.Padding = new Padding(15, 0, 0, 0);
             }
             else
             {
                 btn.TextAlign = ContentAlignment.MiddleCenter;
                 btn.ImageAlign = ContentAlignment.MiddleCenter;
                 btn.Padding = new Padding(0);
             }

             }
         }
     }

              
          
        void MenuTextGoster(bool goster)
        {
            foreach (Control c in panelMenu.Controls)
            {
                if (c is Button btn)
                    btn.TextAlign = goster
                        ? ContentAlignment.MiddleLeft
                        : ContentAlignment.MiddleCenter;
            }
        }

        
        private void PanelMenu_MouseEnter(object sender, EventArgs e)
        {
            isMouseInsideMenu = true;
            if (!isMenuOpen)
                menuTimer.Start();
        }

        private void PanelMenu_MouseLeave(object sender, EventArgs e)
        {
            Point mousePos = panelMenu.PointToClient(Cursor.Position);

            if (!panelMenu.ClientRectangle.Contains(mousePos))
            {
                isMouseInsideMenu = false;
                if (isMenuOpen)
                    menuTimer.Start();
            }
        }

        

        





        private void cikis_Click(object sender, EventArgs e)
        {
            DialogResult cevap = MessageBox.Show("Çıkış yapmak istiyor musunuz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        
        private void menuTimer_Tick(object sender, EventArgs e)
        {
            /*
            if (isMouseInsideMenu && panelMenu.Width < menuOpenWidth)
            {
                panelMenu.Width += 10;
                if (panelMenu.Width >= menuOpenWidth)
                {
                    panelMenu.Width = menuOpenWidth;
                    isMenuOpen = true;
                    menuTimer.Stop();
                }
            }
            else if (!isMouseInsideMenu && panelMenu.Width > menuClosedWidth)
            {
                panelMenu.Width -= 10;
                if (panelMenu.Width <= menuClosedWidth)
                {
                    panelMenu.Width = menuClosedWidth;
                    isMenuOpen = false;
                    menuTimer.Stop();
                }
            }

        }*/

        
        if (isMouseInsideMenu && panelMenu.Width < menuOpenWidth)
        {
            panelMenu.Width += 10;
            if (panelMenu.Width >= menuOpenWidth)
            {
                panelMenu.Width = menuOpenWidth;
                isMenuOpen = true;
                MenuButtonLayout(true);
                menuTimer.Stop();


            }
        }
        else if (!isMouseInsideMenu && panelMenu.Width > menuClosedWidth)
        {
            panelMenu.Width -= 10;
            if (panelMenu.Width <= menuClosedWidth)
            {
                panelMenu.Width = menuClosedWidth;
                isMenuOpen = false;
                MenuButtonLayout(false);
                menuTimer.Stop();
            }
        }

    }


      
        

        void DisableAllPictureBoxes(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is PictureBox pb)
                {
                    pb.Enabled = false;
                }

                if (c.HasChildren)
                {
                    DisableAllPictureBoxes(c);
                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }
    }

}
 


