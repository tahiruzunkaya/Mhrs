using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mhrs.Data.Abstract;
using mhrs.Entity;
using mhrs.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace mhrs.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RandevuController : Controller
    {

        private IUnitOfWork uow;

        public RandevuController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        [HttpPost]
        public bool RandevuEkle(Randevu entity)
        {

            entity.Kullanici = uow.Kullanicilar.Get(entity.KullaniciId);
            entity.Doktor = uow.Doktorlar.Get(entity.DoktorId);

            uow.Randevular.Add(entity);
            uow.SaveChanges();
            return true;

        }

        [HttpGet]
        public JsonResult TarihControl(string tarih1,int doktorid)
        {
            DateTime tarih = DateTime.Parse(tarih1);
            var result = uow.Randevular.Find(i => (i.Tarihi < (tarih.AddDays(1)) || i.Tarihi > tarih.AddDays(-1)) && i.DoktorId==doktorid).ToList();
            List<RandevuControlModel> lst = new List<RandevuControlModel>();
            if (result.Count > 0)
            {

                foreach (var item in result)
                {
                    RandevuControlModel m = new RandevuControlModel()
                    {
                        saat = item.Tarihi.Hour.ToString() + ":" + item.Tarihi.Minute
                    };

                    lst.Add(m);

                }

                

            }

            return Json(JsonConvert.SerializeObject(lst));
        }


    }
}