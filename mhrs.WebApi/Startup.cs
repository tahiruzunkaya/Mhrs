using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mhrs.Data.Abstract;
using mhrs.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace mhrs.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<MhrsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IDoktorRepository, EfDoktorRepository>();
            services.AddTransient<IFavoriRepository, EfFavoriRepository>();
            services.AddTransient<IHastaneRepository, EfHastaneRepository>();
            services.AddTransient<IKisitlamaRepository, EfKisitlamaRepository>();
            services.AddTransient<IKullaniciRepository, EfKullaniciRepository>();
            services.AddTransient<IPoliklinikRepository, EfPoliklinikRepository>();
            services.AddTransient<IRandevuRepository, EfRandevuRepository>();
            services.AddTransient<IRollerRepository, EfRollerRepository>();
            services.AddTransient<IUnitOfWork, EfUnitOfWork>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin", builder => builder.AllowAnyOrigin());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowMyOrigin");
            app.UseMvc();
            
        }
    }
}
