using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mhrs.Data.Abstract;
using mhrs.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace mhrs.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PoliklinikController : Controller
    {

        private IUnitOfWork uow;

        public PoliklinikController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public bool PoliklinikEkle(Poliklinik entity)
        {

            entity.Hastane = uow.Hastaneler.Get(entity.HastaneId);

            uow.Poliklinikler.Add(entity);
            uow.SaveChanges();

            return true;
        }

        public JsonResult PoliklinikListele()
        {
            return Json(JsonConvert.SerializeObject(uow.Poliklinikler.GetAll().ToList()));
        }

        [HttpGet]
        public JsonResult PoliklinikListeleId(int id)
        {
            return Json(JsonConvert.SerializeObject(uow.Poliklinikler.GetAll().Where(i=>i.HastaneId==id).ToList()));
        }

        [HttpGet]
        public bool PoliklinikSil(int id)
        {
            var entity = uow.Poliklinikler.Get(id);
            if (entity == null)
            {
                return false;
            }
            uow.Poliklinikler.Delete(entity);
            uow.SaveChanges();
            return true;
        }

    }
}