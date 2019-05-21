using mhrs.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace mhrs.Data.Concrete.EfCore
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly MhrsContext dbContext;

        public EfUnitOfWork(MhrsContext _dbContext)
        {
            dbContext = _dbContext ?? throw new ArgumentNullException("Db Context can not be null");

        }

        private IDoktorRepository _doktorlar;
        private IFavoriRepository _favoriler;
        private IHastaneRepository _hastaneler;
        private IKisitlamaRepository _kisitlamalar;
        private IKullaniciRepository _kullanicilar;
        private IPoliklinikRepository _poliklinikler;
        private IRandevuRepository _randevular;
        private IRollerRepository _rollers;

        public IDoktorRepository Doktorlar
        {
            get
            {
                return _doktorlar ?? (_doktorlar = new EfDoktorRepository(dbContext));
            }
        }

        public IFavoriRepository Favoriler
        {
            get
            {
                return _favoriler ?? (_favoriler = new EfFavoriRepository(dbContext));
            }
        }


        public IHastaneRepository Hastaneler
        {
            get
            {
                return _hastaneler ?? (_hastaneler = new EfHastaneRepository(dbContext));
            }
        }

        public IKisitlamaRepository Kisitlamalar
        {
            get
            {
                return _kisitlamalar ?? (_kisitlamalar = new EfKisitlamaRepository(dbContext));
            }
        }

        public IKullaniciRepository Kullanicilar
        {
            get
            {
                return _kullanicilar ?? (_kullanicilar = new EfKullaniciRepository(dbContext));
            }
        }


        public IPoliklinikRepository Poliklinikler
        {
            get
            {
                return _poliklinikler ?? (_poliklinikler = new EfPoliklinikRepository(dbContext));
            }
        }

        public IRandevuRepository Randevular
        {
            get
            {
                return _randevular ?? (_randevular = new EfRandevuRepository(dbContext));
            }
        }

        public IRollerRepository Rollers
        {
            get
            {
                return _rollers ?? (_rollers = new EfRollerRepository(dbContext));
            }
        }
        public int SaveChanges()
        {
            try
            {
                return dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
