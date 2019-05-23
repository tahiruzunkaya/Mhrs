using mhrs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhrs.WebApi.Models
{
    public class DoktorKullaniciModel
    {
        public Doktor doktor { get; set; }
        public Kullanici kullanici { get; set; }
    }
}
