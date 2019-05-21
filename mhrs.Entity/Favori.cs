
using System;
using System.Collections.Generic;
using System.Text;

namespace mhrs.Entity
{
    public class Favori
    {
        public int FavoriId { get; set; }
        public DateTime SaveDate { get; set; }

        public int KullaniciId { get; set; }
        public Kullanici Kullanici { get; set; }

        public int DoktorId { get; set; }
        public Doktor Doktor { get; set; }
    }
}
