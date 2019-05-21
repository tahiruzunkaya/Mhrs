using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mhrs.Data.Abstract;
using mhrs.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace mhrs.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HastaneController : Controller
    {

        private IUnitOfWork uow;

        public HastaneController(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        [HttpPost]
        public bool HastaneEkle(Hastane entity)
        {
            entity.SaveDate = DateTime.Now;
            uow.Hastaneler.Add(entity);
            uow.SaveChanges();  
            return true;
        }

        [HttpGet]
        public JsonResult HastaneListele()
        {

            return Json(JsonConvert.SerializeObject(uow.Hastaneler.GetAll().ToList()));
        }

        [HttpGet]
        public bool HastaneSil(int id)
        {
            var entity = uow.Hastaneler.Get(id);
            if(entity == null)
            {
                return false;
            }
            uow.Hastaneler.Delete(entity);
            uow.SaveChanges();
            return true;
        }
    }
}