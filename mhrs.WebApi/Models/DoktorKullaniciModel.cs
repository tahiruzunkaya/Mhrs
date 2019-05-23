using mhrs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhrs.WebApi.Models
{
    public class DoktorKullaniciModel
    {
        public int doktorId { get; set; }
        public String adi { get; set; }
        public String soyadi { get; set; }
    }
}
