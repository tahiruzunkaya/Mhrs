    using System;
using System.Collections.Generic;
using System.Text;

namespace mhrs.Entity
{
    public class Kullanici
    {
        public int KullaniciId { get; set; }
        public long TcKimlikNo { get; set; }
        public string Sifre { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public bool Cinsiyet { get; set; }
        public long Telefon { get; set; }
        public string Email { get; set; }
        public DateTime DogumTarihi { get; set; }
        public DateTime SaveDate { get; set; }

        public int RollerId { get; set; }
        public Roller Roller { get; set; }

        public List<Doktor> Doktorlar { get; set; }
        public List<Randevu> Randevular { get; set; }
        public List<Favori> Favoriler { get; set; }

    }
}
