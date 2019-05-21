using mhrs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhrs.WebApi.Models
{
    public class DoktorEkleModel
    {
        public Kullanici kullanici { get; set; }
        public Doktor doktor { get; set; }
    }
}
