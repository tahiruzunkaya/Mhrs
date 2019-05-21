using System;
using System.Collections.Generic;
using System.Text;

namespace mhrs.Entity
{
    public class Kisitlama
    {
        public int KisitlamaId { get; set; }
        public DateTime KisitTarihi { get; set; }
        public DateTime SaveTime { get; set; }

        public int DoktorId { get; set; }
        public Doktor Doktor { get; set; }
    }
}
