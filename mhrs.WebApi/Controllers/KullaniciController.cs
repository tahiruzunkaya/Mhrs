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
    public class KullaniciController : Controller
    {
        private IUnitOfWork uow;

        public KullaniciController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        [HttpPost]
        public String KullaniciEkle(Kullanici entity)
        {
            var rol = uow.Rollers.Get(entity.RollerId);
            entity.Roller = rol;
            uow.Kullanicilar.Add(entity);
            uow.SaveChanges();
            return "Ok";
        }

        [HttpPost]
        public GirisYapModel GirisYap(Kullanici entity)
        {

            var result = uow.Kullanicilar.Find(i=> i.Sifre ==entity.Sifre && i.TcKimlikNo==entity.TcKimlikNo).FirstOrDefault();

            if (result!=null)
            {
                return new GirisYapModel() { KullaniciId = result.KullaniciId, Durum = 1 };
            }
            else
            {
                return new GirisYapModel() { Durum = 0 };
            }

        }

        [HttpPost]
        public GirisYapAdminModel GirisYapAdmin(Kullanici entity)
        {
            var result = uow.Kullanicilar.Find(i => i.Sifre == entity.Sifre && i.Email == entity.Email).FirstOrDefault();
            
            if (result != null && result.RollerId==1)
            {
                return new GirisYapAdminModel() { KullaniciId = result.KullaniciId, Durum = 1 };
            }
            else
            {
                return new GirisYapAdminModel() { Durum = 0 };
            }
        }

        [HttpGet]
        public Kullanici GetKullanici(int id)
        {
            return uow.Kullanicilar.Get(id);
        }

        [HttpPost]
        public bool KayitOl(Kullanici entity)
        {
            var result = uow.Kullanicilar.Find(i => i.TcKimlikNo == entity.TcKimlikNo).FirstOrDefault();

            if (result != null)
            {
                return false;
            }
            else
            {
                entity.SaveDate = DateTime.Now;
                var rol = uow.Rollers.Find(i=>i.RollerId==2).FirstOrDefault();
                entity.Roller = rol;

                uow.Kullanicilar.Add(entity);
                uow.SaveChanges();
                return true;
            }
        }

        [HttpGet]
        public JsonResult GetKull(int id)
        {
            return Json(JsonConvert.SerializeObject(uow.Kullanicilar.Get(id)));
        }

    }
}