using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodPortal.API.Core.Context;
using FoodPortal.API.Core.Repository.Concrete;
using FoodPortal.API.Core.Repository.Contract;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FoodPortal.API
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
            /*services.AddDbContext<FPDbContext>(options=>{
                options.UseSqlServer(Configuration.GetConnectionString("FoodPortalConStr"));
            });*/
            DbContextOptionsBuilder<FPDbContext> dbo = new DbContextOptionsBuilder<FPDbContext>().UseSqlServer(Configuration.GetConnectionString("FoodPortalConStr"));
            services.AddSingleton<IUnitOfWork,UnitOfWork>(s => new UnitOfWork(new FPDbContext(dbo.Options)));
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
