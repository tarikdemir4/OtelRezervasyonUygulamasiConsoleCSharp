
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace OtelRezervasyonu1
{
    internal class Menu
    {
        private string connectionString = "Data Source=TARDEMPC\\SQLEXPRESS;Initial Catalog=OtelRezervasyonu;Integrated Security=True";

        public void Baslat()
        {
            if (Giris())
            {
                MainMenu();
            }
        }
        private bool Giris()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("                         ╔═════════════════════════════════════════════════════════════════╗");
                Console.WriteLine("                         ║                      OTEL REZERVASYON UYGULAMASI                ║");
                Console.WriteLine("                         ╚═════════════════════════════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine();

                int consoleWidth = Console.WindowWidth;
                string kullaniciAdiMetin = "Kullanıcı Adı: ";
                string sifreMetin = "Şifre: ";

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(new string(' ', (consoleWidth - kullaniciAdiMetin.Length) / 2));
                Console.Write(kullaniciAdiMetin);
                Console.ResetColor();
                string username = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(new string(' ', (consoleWidth - sifreMetin.Length) / 2));
                Console.Write(sifreMetin);
                Console.ResetColor();
                string password = ReadPassword();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Kullanici WHERE KullaniciAdi = @username AND Sifre = @password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", password);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        DisplayCenteredMessage("Giriş başarılı! Ana menüye yönlendiriliyorsunuz...", ConsoleColor.Green);
                        Thread.Sleep(2000);
                        return true;
                    }
                    else
                    {
                        DisplayCenteredMessage("Hatalı kullanıcı adı veya şifre. Tekrar deneyin.", ConsoleColor.Red);
                        Thread.Sleep(2000);
                    }
                }
            }
        }
        private string ReadPassword()
        {
            StringBuilder password = new StringBuilder();
            ConsoleKeyInfo info;

            do
            {
                info = Console.ReadKey(true);
                if (info.Key != ConsoleKey.Enter && info.Key != ConsoleKey.Backspace)
                {
                    password.Append(info.KeyChar);
                    Console.Write("*");
                }
                else if (info.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Remove(password.Length - 1, 1);
                    Console.Write("\b \b");
                }
            } while (info.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password.ToString();
        }
        private void DisplayCenteredMessage(string message, ConsoleColor color)
        {
            Console.Clear();
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;
            int messageLength = message.Length;

            Console.ForegroundColor = color;

            for (int i = 0; i < (consoleHeight / 2) - 1; i++)
            {
                Console.WriteLine();
            }

            Console.WriteLine(new string(' ', (consoleWidth - messageLength) / 2) + message);

            Console.ResetColor();
        }



        private  void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║     Otel Rezervasyon Uygulamasına Hoşgeldiniz      ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║                      Ana Menü                      ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 1. Müşteri İşlemleri                               ║");
                Console.WriteLine("║ 2. Oda İşlemleri                                   ║");
                Console.WriteLine("║ 3. Rezervasyon İşlemleri                           ║");
                Console.WriteLine("║ 4. Faturaları Listele                              ║");
                Console.WriteLine("║ 5. Kasa Göster                                     ║");
                Console.WriteLine("║ 0. Sistemi Kapat                                   ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║ İşlem Seç:                                         ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                Console.SetCursorPosition(12, 12);
                string process = Console.ReadLine();

                Console.Clear();

                switch (process)
                {
                    case "1":
                        CustomerMenu();
                        break;
                    case "2":
                        RoomMenu();
                        break;
                    case "3":
                        ReservationMenu();
                        break;
                    case "4":
                        FaturaMenu();
                        break;
                    case "5":
                        KasaMenu();
                        break;
                    case "0":
                        Console.WriteLine("Sistem Kapatılıyor...");
                        Thread.Sleep(3000);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Tanımsız Seçim.");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }
        private void CustomerMenu()
        {
            Console.Clear();
            bool status = true;
            while (status)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║                    Otel Rezervasyon                ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║                   Müşteri İşlemleri                ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 1. Müşteri Listesi                                 ║");
                Console.WriteLine("║ 2. Müşteri Ekle                                    ║");
                Console.WriteLine("║ 3. Müşteri Güncelle                                ║");
                Console.WriteLine("║ 4. Müşteri Sil                                     ║");
                Console.WriteLine("║ 0. Ana Menüye Dön                                  ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║ İşlem Seç:                                         ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");
                Console.SetCursorPosition(14, 11);
                string process = Console.ReadLine();

                Console.Clear();

                switch (process)
                {
                    case "1":
                        GetAllCustomers();
                        break;
                    case "2":
                        AddCustomer();
                        break;
                    case "3":
                        UpdateCustomer();
                        break;
                    case "4":
                        DeleteCustomer();
                        break;
                    case "0":
                        Console.WriteLine("Ana Menüye Dönülüyor...");
                        Thread.Sleep(2000);
                        status = false;
                        break;
                    default:
                        Console.WriteLine("Tanımsız Seçim.");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }
        private void RoomMenu()
        {

            Console.Clear();
            bool status = true;
            while (status)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║                    Otel Rezervasyon                ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║                     Oda İşlemleri                  ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 1. Oda Listesi                                     ║");
                Console.WriteLine("║ 2. Oda Ekle                                        ║");
                Console.WriteLine("║ 3. Oda Güncelle                                    ║");
                Console.WriteLine("║ 4. Oda Sil                                         ║");
                Console.WriteLine("║ 0. Ana Menüye Dön                                  ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║ İşlem Seç:                                         ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                Console.SetCursorPosition(14, 11);
                string process = Console.ReadLine();

                Console.Clear();

                switch (process)
                {
                    case "1":
                        GetAllRooms();
                        break;
                    case "2":
                        AddRoom();
                        break;
                    case "3":
                        UpdateRoom();
                        break;
                    case "4":
                        DeleteRoom();
                        break;
                    case "0":
                        Console.WriteLine("Ana Menüye Dönülüyor...");
                        Thread.Sleep(2000);
                        status = false;
                        break;
                    default:
                        Console.WriteLine("Tanımsız Seçim.");
                        Thread.Sleep(1000);
                        break;

                }
            }
        }
        private void ReservationMenu()
        {
            bool status = true;
            while (status)
            {
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════════════════╗");
                Console.WriteLine("║                    Otel Rezervasyon                ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║                  Rezervasyon İşlemleri             ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 1. Rezervasyon Listesi                             ║");
                Console.WriteLine("║ 2. Rezervasyon Ekle                                ║");
                Console.WriteLine("║ 3. Rezervasyon Güncelle                            ║");
                Console.WriteLine("║ 4. Rezervasyon Sil                                 ║");
                Console.WriteLine("║ 0. Ana Menüye Dön                                  ║");
                Console.WriteLine("╠════════════════════════════════════════════════════╣");
                Console.WriteLine("║ İşlem Seç:                                         ║");
                Console.WriteLine("╚════════════════════════════════════════════════════╝");

                Console.SetCursorPosition(14, 11);
                string process = Console.ReadLine();

                Console.Clear();

                switch (process)
                {
                    case "1":
                        GetAllReservations();
                        break;
                    case "2":
                        AddReservation();
                        break;
                    case "3":
                        UpdateReservation();
                        break;
                    case "4":
                        DeleteReservation();
                        break;
                    case "0":
                        Console.WriteLine("Ana Menüye Dönülüyor...");
                        Thread.Sleep(2000);
                        status = false;
                        break;
                    default:
                        Console.WriteLine("Tanımsız Seçim.");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }
        private void FaturaMenu()
        {
            Console.Clear();
            Console.WriteLine($"                          ╔════════════════════════════════════════════════════╗");
            Console.WriteLine($"                          ║                    Fatura Listesi                  ║");
            Console.WriteLine($"                          ╚════════════════════════════════════════════════════╝");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("FaturaListele", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    bool hasData = false;
                    while (reader.Read())
                    {
                        if (!hasData)
                        {
                            hasData = true;
                        }

                        Console.WriteLine($"ID: {reader["FaturaID"],-8} | RezervasyonID: {reader["RezervasyonID"],-14} | ToplamTutar: {reader["ToplamTutar"],-11} | Ödeme Tarihi: {reader["OdemeTarihi"],-10}");
                        Console.WriteLine("---------------------------------------------------------------------------------------------------");
                    }

                    if (!hasData)
                    {
                        Console.WriteLine("Fatura listesinde kayıt bulunmamaktadır.");
                    }

                    reader.Close();
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand updateKasaCommand = new SqlCommand("KasaGuncelleveListele", connection);
                    updateKasaCommand.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    updateKasaCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }

            Console.WriteLine("Ana Menüye dönmek için bir tuşa basın...");
            Console.ReadKey();
        }
        private void KasaMenu()
        {
            Console.Clear();
            Console.WriteLine($"\t \t \t \t╔════════════════════════════════════════════════════╗");
            Console.WriteLine($"\t \t \t \t║                    Kasa Bilgileri                  ║");
            Console.WriteLine($"\t \t \t \t╚════════════════════════════════════════════════════╝");

            decimal toplamTutar = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("KasaGuncelleVeListele", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        decimal tutar = reader["Tutar"] != DBNull.Value ? (decimal)reader["Tutar"] : 0;
                        toplamTutar += tutar;
                    }

                    reader.Close();
                }

                string toplamTutarStr = "Toplam Tutar: " + toplamTutar + " TL";
                int consoleWidth = Console.WindowWidth;
                int padding = (consoleWidth - toplamTutarStr.Length) / 2;

                Console.WriteLine();
                Console.WriteLine(new string(' ', padding) + toplamTutarStr);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Ana Menüye dönmek için bir tuşa basın...");
            Console.ReadKey();
        }




        private void GetAllCustomers(bool showMenuMessage = true)
        {
            Console.Clear();
            Console.WriteLine($"                          ╔════════════════════════════════════════════════════╗");
            Console.WriteLine($"                          ║                    Müşteri Listesi                 ║");
            Console.WriteLine($"                          ╚════════════════════════════════════════════════════╝");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("MusteriListele", connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                bool hasData = false;
                while (reader.Read())
                {
                    if (!hasData)
                    {
                        hasData = true;
                    }
                    Console.WriteLine($"Id: {reader["MusteriID"],-3} Ad: {reader["Ad"],-15} Soyad: {reader["Soyad"],-15} Telefon: {reader["Telefon"],-15} Email: {reader["Email"],-30}");
                    Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════════");
                }

                if (!hasData)
                {
                    Console.WriteLine("Müşteri listesinde kayıt bulunmamaktadır.");
                }

                reader.Close();
            }

            if (showMenuMessage)
            {
                Console.WriteLine("Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
            }
        }
        private void GetAllRooms(bool showReturnMessage = true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("                          ╔════════════════════════════════════════════════════╗");
                Console.WriteLine("                          ║                    Oda Listesi                     ║");
                Console.WriteLine("                          ╚════════════════════════════════════════════════════╝");

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("OdaListele", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Id:{reader["OdaID"],-5} No:{reader["OdaNo"],-10} Yatak:{reader["YatakSayisi"],-10} Durum:{reader["Durum"],-15} Kat:{reader["Kat"],-5} Fiyat:{reader["Fiyat"] + " TL"}");
                                Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════════");

                            }
                        }
                        else
                        {
                            Console.WriteLine("Listeye ait veri bulunamadı.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }

            if (showReturnMessage)
            {
                Console.WriteLine("Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
            }
        }
        private void GetAllReservations(bool showReturnMessage = true)
        {
            Console.Clear();
            Console.WriteLine("                          ╔════════════════════════════════════════════════════╗");
            Console.WriteLine("                          ║                    Rezervasyon Listesi             ║");
            Console.WriteLine("                          ╚════════════════════════════════════════════════════╝");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("RezervasyonListele", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    bool hasRecords = false;

                    while (reader.Read())
                    {
                        hasRecords = true;
                        DateTime girisTarihi = (DateTime)reader["GirisTarihi"];
                        DateTime cikisTarihi = (DateTime)reader["CikisTarihi"];
                        decimal toplamFiyat = (decimal)reader["ToplamFiyat"];
                        string musteriAdiSoyadi = reader["MusteriAdiSoyadi"].ToString();
                        int odaNo = (int)reader["OdaNo"];

                        Console.WriteLine($"Id:{reader["RezervasyonID"],-5}| Musteri:{musteriAdiSoyadi,-20}| OdaNo:{odaNo,-5}| GirisTarihi:{girisTarihi:yyyy-MM-dd}| CikisTarihi:{cikisTarihi:yyyy-MM-dd}| ToplamFiyat:{toplamFiyat,-10}");
                        Console.WriteLine("═══════════════════════════════════════════════════════════════════════════════════════════════════════════");
                    }

                    if (!hasRecords)
                    {
                        Console.WriteLine("Hiç rezervasyon bulunamadı.");
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }

            if (showReturnMessage)
            {
                Console.WriteLine("Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
            }
        }












        private void AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("                          ╔════════════════════════════════════════════════════╗");
            Console.WriteLine("                          ║                    Müşteri Ekle                    ║");
            Console.WriteLine("                          ╚════════════════════════════════════════════════════╝");
            Console.Write("Müşteri Adı: ");
            string ad = Console.ReadLine().Trim();
            Console.Write("Müşteri Soyadı: ");
            string soyad = Console.ReadLine().Trim();
            Console.Write("Müşteri Telefon: ");
            string telefon = Console.ReadLine().Trim();
            Console.Write("Müşteri Email: ");
            string email = Console.ReadLine().Trim();

            if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad) || string.IsNullOrEmpty(telefon) || string.IsNullOrEmpty(email))
            {
                Console.WriteLine("Hiçbir değer boş bırakılamaz. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            if (telefon.Length != 11 || !telefon.All(char.IsDigit))
            {
                Console.WriteLine("Telefon numarası 11 haneli olmalı ve sadece rakamlardan oluşmalıdır. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("MusteriEkle", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Ad", ad);
                    command.Parameters.AddWithValue("@Soyad", soyad);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Telefon", telefon);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("Müşteri başarıyla eklendi. Ana Menüye dönmek için bir tuşa basın...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu: " + ex.Message);
            }

            Console.ReadKey();
        }
        private void AddRoom()
        {
            Console.Clear();
            Console.WriteLine("                          ╔════════════════════════════════════════════════════╗");
            Console.WriteLine("                          ║                    Oda Ekle                        ║");
            Console.WriteLine("                          ╚════════════════════════════════════════════════════╝");

            int odaNo;
            while (true)
            {
                Console.WriteLine("Oda Numarası: ");
                if (!int.TryParse(Console.ReadLine(), out odaNo))
                {
                    Console.WriteLine("Geçersiz Oda Numarası. Lütfen geçerli bir sayı girin.");
                }
                else
                {
                    break;
                }
            }

            int yatakSayisi;
            while (true)
            {
                Console.WriteLine("Yatak Sayısı: ");
                if (!int.TryParse(Console.ReadLine(), out yatakSayisi))
                {
                    Console.WriteLine("Geçersiz Yatak Sayısı. Lütfen geçerli bir sayı girin.");
                }
                else
                {
                    break;
                }
            }

            string durum;
            while (true)
            {
                Console.WriteLine("Oda Durumu (Tadilatta / Boş / Temizleniyor / Dolu / Rezerve): ");
                durum = Console.ReadLine();

                var validDurums = new[] { "Tadilatta", "Boş", "Temizleniyor", "Dolu", "Rezerve" };

                if (Array.Exists(validDurums, d => d.Equals(durum, StringComparison.OrdinalIgnoreCase)))
                {
                    break; 
                }
                else
                {
                    Console.WriteLine("Geçersiz durum. Lütfen geçerli bir durum girin.");
                }
            }

            int kat;
            while (true)
            {
                Console.WriteLine("Kat: ");
                if (!int.TryParse(Console.ReadLine(), out kat))
                {
                    Console.WriteLine("Geçersiz Kat. Lütfen geçerli bir sayı girin.");
                }
                else
                {
                    break;
                }
            }

            decimal fiyat;
            while (true)
            {
                Console.WriteLine("Fiyat: ");
                if (!decimal.TryParse(Console.ReadLine(), out fiyat))
                {
                    Console.WriteLine("Geçersiz Fiyat. Lütfen geçerli bir sayı girin.");
                }
                else
                {
                    break;
                }
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Oda WHERE OdaNo = @OdaNo AND IsDelete = 0";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@OdaNo", odaNo);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count > 0)
                    {
                        Console.WriteLine("Bu oda numarası zaten mevcut. Lütfen başka bir numara girin.");
                    }
                    else
                    {
                        SqlCommand command = new SqlCommand("OdaEkle", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OdaNo", odaNo);
                        command.Parameters.AddWithValue("@YatakSayisi", yatakSayisi);
                        command.Parameters.AddWithValue("@Durum", durum);
                        command.Parameters.AddWithValue("@Kat", kat);
                        command.Parameters.AddWithValue("@Fiyat", fiyat);

                        command.ExecuteNonQuery();
                        Console.WriteLine("Oda başarıyla eklendi. Ana Menüye dönmek için bir tuşa basın...");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
            }

            Console.ReadKey();
        }
        private void AddReservation()
        {
            Console.Clear();
            Console.WriteLine("                          ╔════════════════════════════════════════════════════╗");
            Console.WriteLine("                          ║                    Rezervasyon Ekle                ║");
            Console.WriteLine("                          ╚════════════════════════════════════════════════════╝");

            Console.WriteLine("Müşteri ID: ");
            int musteriId;
            if (!int.TryParse(Console.ReadLine(), out musteriId))
            {
                Console.WriteLine("Geçersiz Müşteri ID. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Oda ID: ");
            int odaId;
            if (!int.TryParse(Console.ReadLine(), out odaId))
            {
                Console.WriteLine("Geçersiz Oda ID. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Giriş Tarihi (YYYY-MM-DD): ");
            DateTime girisTarihi;
            if (!DateTime.TryParse(Console.ReadLine(), out girisTarihi))
            {
                Console.WriteLine("Geçersiz Giriş Tarihi. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Çıkış Tarihi (YYYY-MM-DD): ");
            DateTime cikisTarihi;
            if (!DateTime.TryParse(Console.ReadLine(), out cikisTarihi))
            {
                Console.WriteLine("Geçersiz Çıkış Tarihi. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            if (girisTarihi < DateTime.Now.Date || cikisTarihi < DateTime.Now.Date)
            {
                Console.WriteLine("Geçmiş tarihler için rezervasyon yapılamaz.");
                Console.WriteLine("Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            if (cikisTarihi <= girisTarihi)
            {
                Console.WriteLine("Çıkış tarihi giriş tarihinden önce veya aynı olamaz.");
                Console.WriteLine("Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("RezervasyonEkle", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@MusteriID", musteriId);
                command.Parameters.AddWithValue("@OdaID", odaId);
                command.Parameters.AddWithValue("@GirisTarihi", girisTarihi);
                command.Parameters.AddWithValue("@CikisTarihi", cikisTarihi);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    Console.WriteLine("Rezervasyon başarıyla eklendi.");
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Bir hata oluştu: " + ex.Message);
                }
            }

            Console.WriteLine("Ana Menüye dönmek için bir tuşa basın...");
            Console.ReadKey();
        }







        private void UpdateCustomer()
        {
            GetAllCustomers(false); 
            Console.WriteLine("Güncellenecek Müşteri ID: ");
            int id;

            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Geçersiz Müşteri ID. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Yeni Ad: ");
            string yeniAd = Console.ReadLine();

            Console.WriteLine("Yeni Soyad: ");
            string yeniSoyad = Console.ReadLine();

            Console.WriteLine("Yeni Telefon: ");
            string yeniTelefon = Console.ReadLine();

            Console.WriteLine("Yeni Email: ");
            string yeniEmail = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("MusteriGuncelle", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MusteriID", id);
                    command.Parameters.AddWithValue("@Ad", yeniAd);
                    command.Parameters.AddWithValue("@Soyad", yeniSoyad);
                    command.Parameters.AddWithValue("@Telefon", yeniTelefon);
                    command.Parameters.AddWithValue("@Email", yeniEmail);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Müşteri başarıyla güncellendi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bir hata oluştu: " + ex.Message);
                }
            }

            Console.WriteLine("Ana Menüye dönmek için bir tuşa basın...");
            Console.ReadKey();
        }
        private void UpdateRoom()
        {
            while (true)
            {
                GetAllRooms(false); 

                Console.WriteLine("                          ╔════════════════════════════════════════════════════╗");
                Console.WriteLine("                          ║                    Oda Güncelle                    ║");
                Console.WriteLine("                          ╚════════════════════════════════════════════════════╝");
                Console.WriteLine("Güncellenecek Oda ID: ");
                int id;

                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Geçersiz Oda ID. Ana Menüye dönmek için bir tuşa basın...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Yeni Oda Numarası: ");
                int odaNo;
                if (!int.TryParse(Console.ReadLine(), out odaNo))
                {
                    Console.WriteLine("Geçersiz Oda Numarası. Ana Menüye dönmek için bir tuşa basın...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Yeni Yatak Sayısı: ");
                int yatakSayisi;
                if (!int.TryParse(Console.ReadLine(), out yatakSayisi))
                {
                    Console.WriteLine("Geçersiz Yatak Sayısı. Ana Menüye dönmek için bir tuşa basın...");
                    Console.ReadKey();
                    return;
                }

                string durum;
                while (true)
                {
                    Console.WriteLine("Yeni Durum (Tadilatta / Boş / Temizleniyor / Dolu / Rezerve): ");
                    durum = Console.ReadLine();

                    var validDurums = new[] { "Tadilatta", "Boş", "Temizleniyor", "Dolu", "Rezerve" };

                    if (Array.Exists(validDurums, d => d.Equals(durum, StringComparison.OrdinalIgnoreCase)))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Geçersiz durum. Lütfen geçerli bir durum girin.");
                    }
                }

                Console.WriteLine("Yeni Kat: ");
                int kat;
                if (!int.TryParse(Console.ReadLine(), out kat))
                {
                    Console.WriteLine("Geçersiz Kat. Ana Menüye dönmek için bir tuşa basın...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Yeni Fiyat: ");
                decimal fiyat;
                if (!decimal.TryParse(Console.ReadLine(), out fiyat))
                {
                    Console.WriteLine("Geçersiz Fiyat. Ana Menüye dönmek için bir tuşa basın...");
                    Console.ReadKey();
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Oda WHERE OdaID = @OdaID AND IsDelete = 0";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@OdaID", id);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        Console.WriteLine("Böyle bir ID mevcut değil. Ana Menüye dönmek için bir tuşa basın...");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        string checkOdaNoQuery = "SELECT COUNT(*) FROM Oda WHERE OdaNo = @OdaNo AND OdaID != @OdaID AND IsDelete = 0";
                        SqlCommand checkOdaNoCommand = new SqlCommand(checkOdaNoQuery, connection);
                        checkOdaNoCommand.Parameters.AddWithValue("@OdaNo", odaNo);
                        checkOdaNoCommand.Parameters.AddWithValue("@OdaID", id);
                        int odaNoCount = (int)checkOdaNoCommand.ExecuteScalar();

                        if (odaNoCount > 0)
                        {
                            Console.WriteLine("Bu oda numarası zaten mevcut. Lütfen farklı bir numara girin.");
                            Console.ReadKey();
                            continue;
                        }

                        try
                        {
                            SqlCommand command = new SqlCommand("OdaGuncelle", connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@OdaID", id);
                            command.Parameters.AddWithValue("@OdaNo", odaNo);
                            command.Parameters.AddWithValue("@YatakSayisi", yatakSayisi);
                            command.Parameters.AddWithValue("@Durum", durum);
                            command.Parameters.AddWithValue("@Kat", kat);
                            command.Parameters.AddWithValue("@Fiyat", fiyat);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine("Oda başarıyla güncellendi. Ana Menüye dönmek için bir tuşa basın...");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Güncelleme işlemi başarısız oldu. Ana Menüye dönmek için bir tuşa basın...");
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine("Bir hata oluştu: " + ex.Message);
                        }
                    }
                }
            }
        }
        private void UpdateReservation()
        {
            Console.Clear();
            Console.WriteLine("                          ╔════════════════════════════════════════════════════╗");
            Console.WriteLine("                          ║                    Rezervasyon Güncelle            ║");
            Console.WriteLine("                          ╚════════════════════════════════════════════════════╝");

           
            GetAllReservations(false);

            Console.WriteLine("\nGüncellenecek Rezervasyon ID: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Geçersiz Rezervasyon ID. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Yeni Müşteri ID: ");
            int musteriId;
            if (!int.TryParse(Console.ReadLine(), out musteriId))
            {
                Console.WriteLine("Geçersiz Müşteri ID. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Yeni Oda ID: ");
            int odaId;
            if (!int.TryParse(Console.ReadLine(), out odaId))
            {
                Console.WriteLine("Geçersiz Oda ID. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Yeni Giriş Tarihi (YYYY-MM-DD): ");
            DateTime girisTarihi;
            if (!DateTime.TryParse(Console.ReadLine(), out girisTarihi))
            {
                Console.WriteLine("Geçersiz Giriş Tarihi. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Yeni Çıkış Tarihi (YYYY-MM-DD): ");
            DateTime cikisTarihi;
            if (!DateTime.TryParse(Console.ReadLine(), out cikisTarihi))
            {
                Console.WriteLine("Geçersiz Çıkış Tarihi. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            if (girisTarihi < DateTime.Now.Date || cikisTarihi < DateTime.Now.Date)
            {
                Console.WriteLine("Geçmiş tarihler için rezervasyon yapılamaz.");
                Console.WriteLine("Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            if (cikisTarihi <= girisTarihi)
            {
                Console.WriteLine("Çıkış tarihi giriş tarihinden önce veya aynı olamaz.");
                Console.WriteLine("Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand("RezervasyonGuncelle", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RezervasyonID", id);
                    command.Parameters.AddWithValue("@MusteriID", musteriId);
                    command.Parameters.AddWithValue("@OdaID", odaId);
                    command.Parameters.AddWithValue("@GirisTarihi", girisTarihi);
                    command.Parameters.AddWithValue("@CikisTarihi", cikisTarihi);

                    connection.Open();
                    command.ExecuteNonQuery();

                    Console.WriteLine("Rezervasyon başarıyla güncellendi. Ana Menüye dönmek için bir tuşa basın...");
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("50000"))
                    {
                        Console.WriteLine("Bir hata oluştu: " + ex.Message);
                    }
                    else
                    {
                        Console.WriteLine("Bir hata oluştu: " + ex.Message);
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Geçersiz giriş: " + ex.Message);
                }
            }

            Console.ReadKey();
        }




        private void DeleteCustomer()
        {
            Console.Clear();
            Console.WriteLine("                          ╔════════════════════════════════════════════════════╗");
            Console.WriteLine("                          ║                     Müşteri Sil                    ║");
            Console.WriteLine("                          ╚════════════════════════════════════════════════════╝");

            GetAllCustomers(false);

            Console.WriteLine("Silinecek Müşteri ID: ");
            int id;

            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Geçersiz Müşteri ID. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("MusteriSil", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MusteriID", id);

                    command.ExecuteNonQuery();

                    SqlCommand checkCommand = new SqlCommand("SELECT IsDelete FROM Musteri WHERE MusteriID = @MusteriID", connection);
                    checkCommand.Parameters.AddWithValue("@MusteriID", id);
                    object result = checkCommand.ExecuteScalar();

                    if (result != null && (bool)result == true)
                    {
                        Console.WriteLine("Müşteri başarıyla silindi. Ana Menüye dönmek için bir tuşa basın...");
                    }
                    else
                    {
                        Console.WriteLine("Silme işlemi başarısız oldu veya müşteri bulunamadı. Ana Menüye dönmek için bir tuşa basın...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bir hata oluştu: " + ex.Message);
                }
            }

            Console.ReadKey();
        }
        private void DeleteRoom()
        {
            GetAllRooms(false); 
            Console.WriteLine("                          ╔════════════════════════════════════════════════════╗");
            Console.WriteLine("                          ║                      Oda Sil                       ║");
            Console.WriteLine("                          ╚════════════════════════════════════════════════════╝");
            Console.Write("Silinecek Oda ID: ");
            int id;

            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Geçersiz ID. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Oda WHERE OdaID = @OdaID AND IsDelete = 0";
                    SqlCommand checkCommand = new SqlCommand(checkQuery, connection);
                    checkCommand.Parameters.AddWithValue("@OdaID", id);
                    int count = (int)checkCommand.ExecuteScalar();

                    if (count == 0)
                    {
                        Console.WriteLine("Belirtilen ID'ye sahip oda bulunamadı veya oda zaten silinmiş. Ana Menüye dönmek için bir tuşa basın...");
                    }
                    else
                    {
                        SqlCommand command = new SqlCommand("OdaSil", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@OdaID", id);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Oda başarıyla silindi. Ana Menüye dönmek için bir tuşa basın...");
                        }
                        else
                        {
                            Console.WriteLine("Oda silme işlemi başarısız oldu. Ana Menüye dönmek için bir tuşa basın...");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bir hata oluştu: " + ex.Message);
                }
            }

            Console.ReadKey();
        }
        private void DeleteReservation()
        {
            Console.Clear();
            Console.WriteLine("                          ╔════════════════════════════════════════════════════╗");
            Console.WriteLine("                          ║                    Rezervasyon Sil                ║");
            Console.WriteLine("                          ╚════════════════════════════════════════════════════╝");

            
            GetAllReservations(false);

            Console.WriteLine("\nSilinecek Rezervasyon ID: ");
            int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Geçersiz Rezervasyon ID. Ana Menüye dönmek için bir tuşa basın...");
                Console.ReadKey();
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand("RezervasyonSil", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RezervasyonID", id);

                    connection.Open();
                    command.ExecuteNonQuery();

                    Console.WriteLine("Rezervasyon başarıyla silindi. Ana Menüye dönmek için bir tuşa basın...");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Bir hata oluştu: " + ex.Message);
                }
            }
            Console.ReadKey();
        }

    }
}