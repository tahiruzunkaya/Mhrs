using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mhrs.Data.Abstract;
using mhrs.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}