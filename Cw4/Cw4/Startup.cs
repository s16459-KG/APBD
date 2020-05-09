using Cw4.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEnrollmentDbService enrollmentDbService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            /* KOLEJNOŒÆ MIDDLEWARE MA ZNACZENIE!
   * UserMiddleware bêdzie pomocne w zadaniu 2, jako generic nale¿y wykorzystaæ utworzon¹ klasê
   * Chcemy, ¿eby middleware z 2 zadania zapisywa³ WSZYSTKIE przychodz¹ce ¿¹dania, dlatego dodajemy go przez middlewarem z 1. zadania
   */

            //app.UseMiddleware<T>();

            app.Use(async (context, next) =>
            {

                if (!context.Request.Headers.ContainsKey("Index"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Nie podano indeksu w nag³ówku");
                    return;
                }


                var index = context.Request.Headers["Index"].ToString();

                // ³¹czenie z baz¹ danych i sprawdzenie czy istnieje student
                if (!enrollmentDbService.CheckIndex(index))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Nie znaleziono studenta w bazie danych");
                    return;
                }

                // jak wszystko ok to przekazujemy ¿¹danie do kolejnego middleware'a
                await next();
            });


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
