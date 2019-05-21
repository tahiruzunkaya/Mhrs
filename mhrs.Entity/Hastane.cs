using System;
using System.Collections.Generic;
using System.Text;

namespace mhrs.Entity
{
    public class Hastane
    {
        public int HastaneId { get; set; }
        public string HastaneAdi { get; set; }
        public string Adres { get; set; }
        public DateTime SaveDate { get; set; }

        public List<Poliklinik> Poliklinikler { get; set; }
    }
}
