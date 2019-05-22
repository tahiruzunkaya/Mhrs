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

    }
}