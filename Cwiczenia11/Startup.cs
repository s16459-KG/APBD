using System;
using Cwiczenia11.DAL;
using Cwiczenia11.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cwiczenia11
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
            Console.WriteLine("Przed Dodaniem Kontrolerów");
            services.AddDbContext<CodeFirstContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("DbContext")));
            services.AddScoped<IDoctorDbService, DoctorDbService>();
            services.AddControllers();
            Console.WriteLine("Kontrolery Dodane");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine("Początek konfiguracji");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            Console.WriteLine("Skonfigurowano Middlewary");
        }
    }
}
