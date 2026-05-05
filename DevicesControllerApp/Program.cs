using System;
using System.Windows.Forms;
using DevicesControllerApp.Ana_ekran_Login;

namespace DevicesControllerApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var login = new form1()) // Login formu
            {
                if (login.ShowDialog() == DialogResult.OK)
                {

                    // 2. ADIM: Login kapandı, şimdi Loading Formu açıyoruz
                    using (var loading = new LoadingForm())
                    {
                        loading.Show();
                        Application.DoEvents(); // Formun düzgün çizilmesi için

                        // 3. ADIM: Burada 7.5 saniye bekliyoruz
                        // Program.cs içinde async olmadığı için Thread.Sleep veya Timer yerine 
                        // şu basit döngüyü kullanabiliriz:
                        DateTime end = DateTime.Now.AddMilliseconds(7500);
                        while (DateTime.Now < end)
                        {
                            Application.DoEvents(); // GIF'in donmaması için şart!
                        }

                        loading.Close();
                    }

                    // 4. ADIM: Bekleme bitti, MainForm'u başlatıyoruz
                    MainForm main = new MainForm(login.SecilenRol, login.SecilenDil);
                    main.SetKullaniciAdi(login.GeciciKullaniciAdi);
                    Application.Run(main);
                }

                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
