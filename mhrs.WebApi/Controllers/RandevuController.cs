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

        [HttpGet]
        public JsonResult KullaniciRandevular(int id)
        {

            List<KullaniciRandevularModel> lst=new List<KullaniciRandevularModel>();

            var result = uow.Randevular.GetAll().Where(i => i.KullaniciId == id).Include(i=>i.Doktor).ThenInclude(i=>i.Kullanici).Include(i=>i.Doktor).ThenInclude(i=>i.Poliklinik).ThenInclude(i=>i.Hastane).ToList();

            foreach (var item in result)
            {

                KullaniciRandevularModel m = new KullaniciRandevularModel()
                {
                    doktorad = item.Doktor.Kullanici.Ad,
                    doktorid = item.DoktorId,
                    doktorsoyad = item.Doktor.Kullanici.Soyad,
                    hastaneadi = item.Doktor.Poliklinik.Hastane.HastaneAdi,
                    poliklinikadi = item.Doktor.Poliklinik.PoliklinikAdi,
                    randevuid = item.RandevuId,
                    tarihi = item.Tarihi


                };
                lst.Add(m);

            }


            return Json(JsonConvert.SerializeObject(lst));
        }
        
        [HttpGet]
        public JsonResult DoktorRandevu(int id)
        {
            List<DoktorRandevuModel> lst = new List<DoktorRandevuModel>();

            var result = uow.Randevular.GetAll().Where(i => i.Doktor.Kullanici.KullaniciId == id).Include(i => i.Kullanici).ToList();

            foreach (var item in result)
            {
                DoktorRandevuModel m = new DoktorRandevuModel()
                {
                    kullaniciad = item.Kullanici.Ad,
                    kullanicisoyad = item.Kullanici.Soyad,
                    tarih = item.Tarihi
                };
                lst.Add(m);
            }
            return Json(JsonConvert.SerializeObject(lst));
        }

        [HttpGet]
        public bool RandevuIptal(int id)
        {
            uow.Randevular.Delete(uow.Randevular.Get(id));
            uow.SaveChanges();
            return true;
        }
    }
}