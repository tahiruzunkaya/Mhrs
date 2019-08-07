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

        [HttpGet]
        public JsonResult EnPopuler(int id)
        {
            var result = uow.Favoriler.GetAll().Include(i => i.Doktor).ThenInclude(i => i.Poliklinik).ThenInclude(i => i.Hastane).Include(i => i.Doktor).ThenInclude(i => i.Kullanici).Where(i => i.Doktor.Poliklinik.Hastane.HastaneId == id).ToList();

            var list = result.GroupBy(j => j.DoktorId).Select(grp => grp.ToList()).ToList();

            List<EnPopulerModel> lst = new List<EnPopulerModel>();


            foreach (var item in list)
            {
                for (int i = 0; i < item.Count; i++)
                {
                    if (i == 0) { 
                        EnPopulerModel m = new EnPopulerModel()
                        {
                            doktoradi = item[i].Doktor.Kullanici.Ad,
                            doktorsoyadi = item[i].Doktor.Kullanici.Soyad,
                            hastaneadi = item[i].Doktor.Poliklinik.Hastane.HastaneAdi,
                            poliklinikadi = item[i].Doktor.Poliklinik.PoliklinikAdi
                        };

                        lst.Add(m);
                    }
                }
            }


            return Json(JsonConvert.SerializeObject(lst));

        }


    }
}
