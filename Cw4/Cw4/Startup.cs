using Cw4.DAL;
using Cw4.Handlers;
using Cw4.Middlerwares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Cw4
{
    public class Startup
    {
        private int licznik = 0;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Przed Dodaniem Kontroler�w");

            //Uwierzytelnienie HTTP BASIC
           // services.AddAuthentication("AuthenticationBasic")
             //   .AddScheme<AuthenticationSchemeOptions, BasicAuthHandler>("AuthenticationBasic", null);
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                     {
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,
                             ValidateAudience = true,
                             ValidateLifetime = true,
                             ValidIssuer = "Gakko",
                             ValidAudience = "Students",
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))

                         };
                     });

            services.AddSingleton<IStudentDbService, StudentDbService>();
            services.AddScoped<IEnrollmentDbService, EnrollmentDbService>();
            services.AddControllers();

            Console.WriteLine("Kontrolery Dodane");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEnrollmentDbService enrollmentDbService)
        {
            Console.WriteLine("Pocz�tek konfiguracji");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /* KOLEJNO�� MIDDLEWARE MA ZNACZENIE!
   * UseMiddleware b�dzie pomocne w zadaniu 2, jako generic nale�y wykorzysta� utworzon� klas�
   * Chcemy, �eby middleware z 2 zadania zapisywa� WSZYSTKIE przychodz�ce ��dania, dlatego dodajemy go przez middlewarem z 1. zadania
   */

            //app.UseMiddleware<T>();

            app.UseMiddleware<LoggingMiddleware>();

            app.UseAuthentication();

            /*
            app.Use(async (context, next) =>
            {
                if (!context.Request.Headers.ContainsKey("Index"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Nie podano indeksu w nag��wku");
                    return;
                }
                var index = context.Request.Headers["Index"].ToString();

                // ��czenie z baz� danych i sprawdzenie czy istnieje student
                if (!enrollmentDbService.CheckIndex(index))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Nie znaleziono studenta w bazie danych");
                    return;
                }
                // jak wszystko ok to przekazujemy ��danie do kolejnego middleware'a
                await next();
            });
            */

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
