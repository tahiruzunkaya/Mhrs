using mhrs.Data.Abstract;
using mhrs.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace mhrs.Data.Concrete.EfCore
{
    public class EfRollerRepository : EfGenericRepository<Roller>, IRollerRepository
    {
        public EfRollerRepository(MhrsContext context) : base(context)
        {

        }
        public MhrsContext EContext
        {
            get { return context as MhrsContext; }
        }
    }
}
