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
    public class DoktorController : Controller
    {
        private IUnitOfWork uow;

        public DoktorController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        [HttpPost]
        public bool DoktorEkle(DoktorEkleModel entity)
        {
            var result = uow.Kullanicilar.Find(i => i.TcKimlikNo == entity.kullanici.TcKimlikNo).FirstOrDefault();
            if (result == null)
            {

                entity.doktor.SaveDate = DateTime.Now;
                entity.kullanici.SaveDate = DateTime.Now;
                entity.kullanici.Roller = uow.Rollers.Find(i => i.RollerId == 3).FirstOrDefault();

                entity.doktor.Poliklinik = uow.Poliklinikler.Find(i => i.PoliklinikId == entity.doktor.PoliklinikId).FirstOrDefault();
                entity.doktor.Kullanici = entity.kullanici;
                uow.Kullanicilar.Add(entity.kullanici);
                uow.Doktorlar.Add(entity.doktor);
                uow.SaveChanges();


                return true;

            }
            else
            {
                return false;
            }
            
        }

        [HttpGet]
        public bool DoktorSil(int id)
        {
            var doktor = uow.Doktorlar.Get(id);
            var kullanici = uow.Kullanicilar.Get(doktor.KullaniciId);
            uow.Kullanicilar.Delete(kullanici);
            uow.Doktorlar.Delete(doktor);
            uow.SaveChanges();
            return true;
        }

        [HttpGet]
        public JsonResult DoktorListele(int id)
        {

            List<DoktorKullaniciModel> lst = new List<DoktorKullaniciModel>();

            var liste= uow.Doktorlar.GetAll().Where(i => i.PoliklinikId == id).ToList();

            foreach (var item in liste)
            {
                var ad = uow.Kullanicilar.Get(item.KullaniciId).Ad;
                var soyad = uow.Kullanicilar.Get(item.KullaniciId).Soyad;
                DoktorKullaniciModel m = new DoktorKullaniciModel()
                {
                    doktorId = item.DoktorId,
                    adi = ad,
                    soyadi=soyad
                };
                lst.Add(m);
            }


            return Json(JsonConvert.SerializeObject(lst));
        }

        public JsonResult GetDoktor(int id){

            var entity=uow.Doktorlar.Get(id);

            
            return Json(JsonConvert.SerializeObject(entity));
        }

    }
}