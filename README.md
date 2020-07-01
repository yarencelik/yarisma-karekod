Proje hakkında: Bir yarışma için oluşturmuş karekod sistemidir ve uygulamanın iki kısmı bulunmaktadır:
1) Robot kaydı: Bu formda robotun adı ve kategorisi seçilir ve bir karekodu oluşturulur. Karekodu kaydediyoruz ve sonraki aşama için ya çıktısını ya da fotoğrafını çekiyoruz. 
Karekod = robotun adı + kayıtlı olunan kategorinin kodu 
Kategoriler:
MS: Mini Sumo
RC: Robo Cross
YS: Yıldız Savaşları
Cİ: Çizgi İzleyen
LC: Labirent Çözen
SK: Serbest Kategori
2) Kayıt masası: Bu robotun kontrol aşamasında kullanılan kısım. 
Öncelikle comboboxtan kamera seçimi yapıp “çalıştır” butonu ile kamerayı başlatıyoruz.
Sonra tarama başlıyor ve belli bir süre sonra tespit edip “robotun kodu” labelının altındaki textboxa yazıyor ve sorgula butonuna basınca kayıt veritabanında varsa onaylıyor ve işlem tamamlanıyor.
Bilgisayar kameram çok iyi olmadığı için bazen okuyup bazen okumadı. Bu yüzden ek çözüm olarak elle girme kısmı ekledim. Telefondan taratınca üretilen kodu oraya yazıp tekrar sorgulayabilir ve onaylatabiliriz.
Kod tarayamaması duruma karşı default olarak pictureboxa bir karekod ekledim.
 
