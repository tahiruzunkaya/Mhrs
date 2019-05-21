using System;
using System.Collections.Generic;
using System.Text;

namespace mhrs.Entity
{
    public class Roller
    {
        public int RollerId { get; set; }
        public string RolAdi { get; set; }

        public List<Kullanici> Kullanicilar { get; set; }
    }
}
