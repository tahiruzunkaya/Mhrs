using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mhrs.Data.Concrete.EfCore
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MhrsContext>
    {
        public MhrsContext CreateDbContext(string[] args)
        {
            

            var builder = new DbContextOptionsBuilder<MhrsContext>();
            return new MhrsContext(builder.Options);
        }
    }
}
