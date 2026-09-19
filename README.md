# IdentityMail

[![.NET](https://img.shields.io/badge/.NET-10-blue)](https://dotnet.microsoft.com/)

IdentityMail; ASP.NET Core (.NET 10, Razor) ile geliştirilmiş, kullanıcı kimlik doğrulama, rol yönetimi ve e‑posta/messaging işlevleri sunan hafif, üretime yakın bir örnek uygulamadır.

## Özellikler
- Kullanıcı kayıt ve giriş (ASP.NET Core Identity)
- Rol tabanlı yetkilendirme (Admin / User)
- Mesaj gönderme / alma, gelen/giden kutusu
- Çöp kutusu ve geri yükleme
- Yıldızlama, okundu/okunmadı takibi
- Mesaj kategorileri, filtreleme ve sıralama
- Profil yönetimi ve admin dashboard

## Teknolojiler
- .NET 10
- ASP.NET Core (Razor)
- Entity Framework Core (Code First)
- SQL Server / LocalDB
- Bootstrap 5, Razor Views, JavaScript

## Kurulum
1. Depoyu klonlayın:

   git clone https://github.com/Cihaansaahin/MyAcademy_IdentityMailProject.git

2. `IdentityMail.web/appsettings.json` içindeki `DefaultConnection` değerini güncelleyin.

3. Migration uygulayın:

   dotnet ef database update --project IdentityMail.web

4. Uygulamayı çalıştırın:

   dotnet run --project IdentityMail.web

## Ekran Görüntüleri
![Admin Panel](https://raw.githubusercontent.com/Cihaansaahin/MyAcademy_IdentityMailProject/master/IdentityMail.web/Img/AdminPanel.png)
![Gelen Kutusu](https://raw.githubusercontent.com/Cihaansaahin/MyAcademy_IdentityMailProject/master/IdentityMail.web/Img/GelenKutu.png)
![Çöp Kutusu](https://raw.githubusercontent.com/Cihaansaahin/MyAcademy_IdentityMailProject/master/IdentityMail.web/Img/CopKutusu.png)
![Yeni Mesaj](https://raw.githubusercontent.com/Cihaansaahin/MyAcademy_IdentityMailProject/master/IdentityMail.web/Img/YeniMessage.png)
![Gönderilen Mesaj](https://raw.githubusercontent.com/Cihaansaahin/MyAcademy_IdentityMailProject/master/IdentityMail.web/Img/SendMessage.png)
![Kayıt Olma Ekranı](https://raw.githubusercontent.com/Cihaansaahin/MyAcademy_IdentityMailProject/master/IdentityMail.web/Img/KayitOl.png)
![Giriş Ekranı](https://raw.githubusercontent.com/Cihaansaahin/MyAcademy_IdentityMailProject/master/IdentityMail.web/Img/Login.png)
![Şifremi Unuttum](https://raw.githubusercontent.com/Cihaansaahin/MyAcademy_IdentityMailProject/master/IdentityMail.web/Img/Unuttum.png)
![Yıldızlı Mesaj](https://raw.githubusercontent.com/Cihaansaahin/MyAcademy_IdentityMailProject/master/IdentityMail.web/Img/YıldızlıMessge.png)

## Katkıda bulunma
Forklayın ve PR gönderin. Değişiklik açıklamalarında yaptıklarınızı belirtin.

## Lisans
Lisans belirtilmemiştir. Gerekirse `LICENSE` ekleyin.
