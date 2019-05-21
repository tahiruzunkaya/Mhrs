using System;
using System.Collections.Generic;
using System.Text;

namespace mhrs.Entity
{
    public class Doktor
    {
        public int DoktorId { get; set; }
        public string Brans { get; set; }
        public DateTime SaveDate { get; set; }

        public int PoliklinikId { get; set; }
        public Poliklinik Poliklinik { get; set; }

        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }

        public List<Randevu> Randevular { get; set; }
        public List<Kisitlama> Kisitlamalar { get; set; }
    }
}
