
using System;
using System.Collections.Generic;
using System.Text;

namespace mhrs.Entity
{
    public class Poliklinik
    {
        public int PoliklinikId { get; set; }
        public string PoliklinikAdi { get; set; }
        public DateTime SaveDate { get; set; }

        public int HastaneId { get; set; }
        public Hastane Hastane { get; set; }

        public List<Doktor> Doktorlar { get; set; }

    }
}
