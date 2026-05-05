# 🚀 Lokomat – İleri Programlama Dersi Projesi

Lokomat, kullanıcı giriş sistemi, rol bazlı erişim kontrolü ve çoklu dil desteği içeren Windows Forms tabanlı bir masaüstü uygulamasıdır. Proje, UI/UX tasarımı ve sistem mimarisi açısından geliştirilmiştir.

---

## 🎯 Proje Amacı

Kullanıcıların sisteme giriş yapabildiği, yetkilerine göre farklı ekranlara erişebildiği ve çoklu dil desteği bulunan modüler bir masaüstü uygulaması geliştirmek.

---

## 👨‍💻 Benim Katkım (ÖNEMLİ)

- Login ekranının kullanıcı arayüz tasarımı (username/password giriş sistemi)
- “Beni Hatırla” özelliğinin UI tarafı
- “Şifremi Unuttum” butonunun tasarımı ve yönlendirme akışı
- 3 hatalı giriş sonrası geçici hesap kilitleme mekanizmasının arayüz tarafı
- Ana form (MainForm) sol menü (slide menu) tasarımı
- Rol bazlı menü görünürlüğü (Admin / Servis / Kullanıcı)
- Çoklu dil sisteminin (JSON/XML) UI entegrasyonu
- Dinamik içerik yükleme (UserControl switching) yapısı
- Üst bar tasarımı (kullanıcı bilgisi, saat, cihaz durumu)

---

## 🔐 Login Ekranı Özellikleri

- Kullanıcı adı ve şifre girişi
- Şifre göster/gizle (toggle)
- “Beni Hatırla” checkbox (username kaydetme)
- “Şifremi Unuttum” butonu
- Dil seçimi (ComboBox)
- Giriş ve çıkış butonları
- 3 hatalı giriş sonrası geçici kilitleme
- Loading animasyonu

---

## 🧭 Ana Form (MainForm) Özellikleri

### 📌 Sol Menü (Slide Menu)
- Terapi Başlat
- Hasta Kayıt
- Terapi İzle
- Raporlama
- Kullanıcı Kayıt (Admin)
- Ayarlar (Admin)
- Servis (Servis Personeli)
- Hesabım
- Çıkış

### 📌 Üst Bar
- Sol: Logo
- Orta: Cihaz bağlantı durumu (Bağlı / Bağlı Değil)
- Sağ: Aktif kullanıcı, aktif dil, saat

### 📌 Ana Panel
- Dinamik UserControl yükleme alanı
- Sayfa geçiş sistemi

---

## 🌍 Çoklu Dil Desteği

- JSON / XML tabanlı dil dosyaları kullanıldı
- Runtime (çalışma anında) dil değiştirme desteği
- Tüm UI metinleri dinamik olarak güncellenmektedir

---

## 🛠 Kullanılan Teknolojiler

- C#
- Windows Forms (.NET Framework)
- JSON / XML
- Event-driven UI architecture
- Panel / MDI tabanlı yapı

---

## 🧠 Mimari Yapı

- Role-Based Access Control (RBAC)
- Session yönetimi (kullanıcı oturumu)
- Event-driven form navigation
- Modular UserControl yapısı

---
## Ekran Görüntüleri
images klasöründe yer almaktadır
---
## 📁 Kurulum

```bash
git clone https://github.com/USERNAME/lokomat-ui-system.git
