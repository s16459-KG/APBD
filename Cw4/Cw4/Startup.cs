using Cw4.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Cw4
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
            //Dodanie kolejnych serwisów do naszego programu.
            //Metody np. AddSingleton, AddTransient, AddScoped dodaj¹ serwisy
            //U¿ycie konkretnego serwisu zale¿y od modelu korzystania z serwisów
            //AddScoped u¿ywa jednego obiektu serwisu w ramach sesji.
            //AddSingleton jednego ogólnie, a AddTransient dla ka¿dego wywo³ania tworzy nowy obiekt
            services.AddScoped<IStudentDbService, StudentDbService>();
            services.AddScoped<IEnrollmentDbService, EnrollmentDbService>();
            services.AddControllers();
           /* for (int i = 0 ; i < services.ToList().Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(services.ToArray()[i]);
            }*/
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
        }
    }
}
