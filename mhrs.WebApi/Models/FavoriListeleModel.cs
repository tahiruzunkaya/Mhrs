using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mhrs.WebApi.Models
{
    public class FavoriListeleModel
    {
        public int favoriid { get; set; }
        public int doktorid  { get; set; }
        public string doktorad { get; set; }
        public string doktorsoyad { get; set; }
        public string hastaneadi { get; set; }
        public string poliklinikadi { get; set; }
        public DateTime savedate { get; set; }
    }
}
