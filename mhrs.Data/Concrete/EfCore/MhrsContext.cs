using mhrs.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace mhrs.Data.Concrete.EfCore
{
    public class MhrsContext:DbContext
    {
        public MhrsContext(DbContextOptions<MhrsContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:mhrsdb.database.windows.net;Database=MhrsDB;User ID =tahiruzunkaya; Password = 12481632aA; Trusted_Connection = False;Encrypt = True; ");
        }

        public DbSet<Doktor> Doktorlar { get; set; }
        public DbSet<Favori> Favoriler { get; set; }
        public DbSet<Hastane> Hastaneler { get; set; }
        public DbSet<Kisitlama> Kisitlamalar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Poliklinik> Poliklinikler { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<Roller> Rollers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
