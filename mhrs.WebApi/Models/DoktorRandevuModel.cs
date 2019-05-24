using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhrs.WebApi.Models
{
    public class DoktorRandevuModel
    {
        public DateTime tarih { get; set; }
        public string kullaniciad { get; set; }
        public string kullanicisoyad { get; set; }
        
    }
}
