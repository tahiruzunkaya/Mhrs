using System;
using System.Collections.Generic;
using System.Text;

namespace mhrs.Data.Abstract
{
    public interface IUnitOfWork
    {

        IDoktorRepository Doktorlar { get; }
        IFavoriRepository Favoriler { get; }
        IHastaneRepository Hastaneler { get; }
        IKisitlamaRepository Kisitlamalar { get; }
        IKullaniciRepository Kullanicilar { get; }
        IPoliklinikRepository Poliklinikler { get; }
        IRandevuRepository Randevular { get; }
        IRollerRepository Rollers { get; }

        int SaveChanges();

    }
}
