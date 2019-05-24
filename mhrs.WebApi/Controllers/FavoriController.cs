using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mhrs.Data.Abstract;
using mhrs.Entity;
using mhrs.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace mhrs.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FavoriController : Controller
    {
        private IUnitOfWork uow;
        public FavoriController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public bool FavoriEkle(Favori entity)
        {
            var result = uow.Favoriler.Find(i => i.KullaniciId == entity.KullaniciId && i.DoktorId == entity.DoktorId).FirstOrDefault();

            if (result != null)
            {
                return false;
            }

            entity.Doktor = uow.Doktorlar.Get(entity.DoktorId);
            entity.Kullanici = uow.Kullanicilar.Get(entity.KullaniciId);
            uow.Favoriler.Add(entity);
            
            uow.SaveChanges();
            return true;

        }

        public JsonResult FavoriListele(int id)
        {
            List<FavoriListeleModel> lst = new List<FavoriListeleModel>();

            var result = uow.Favoriler.GetAll().Where(i => i.KullaniciId == id).Include(i => i.Doktor).ThenInclude(i => i.Kullanici).Include(i => i.Doktor).ThenInclude(i => i.Poliklinik).ThenInclude(i => i.Hastane).ToList();

            foreach (var item in result)
            {
                FavoriListeleModel m = new FavoriListeleModel()
                {
                    doktorad = item.Doktor.Kullanici.Ad,
                    doktorsoyad = item.Doktor.Kullanici.Soyad,
                    doktorid = item.DoktorId,
                    favoriid = item.FavoriId,
                    hastaneadi = item.Doktor.Poliklinik.Hastane.HastaneAdi,
                    poliklinikadi = item.Doktor.Poliklinik.PoliklinikAdi,
                    savedate = item.SaveDate
                };
                lst.Add(m);
            }

            return Json(JsonConvert.SerializeObject(lst));
        }

        [HttpGet]
        public bool FavoriSil(int id)
        {
            uow.Favoriler.Delete(uow.Favoriler.Get(id));
            uow.SaveChanges();
            return true;
        }
    }
}