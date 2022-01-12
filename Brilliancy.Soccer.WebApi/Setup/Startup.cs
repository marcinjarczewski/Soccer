using Brilliancy.Soccer.DbAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Brilliancy.Soccer.Common.Contracts.Modules;
using Brilliancy.Soccer.Core.Modules;
using Brilliancy.Soccer.WebApi.Setup;
using Brilliancy.Soccer.DbModels.Interfaces;
using Brilliancy.Soccer.DbAccess.Repositories;
using Brilliancy.Soccer.WebApi.Providers;

namespace Brilliancy.Soccer.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc();
            services.AddRazorPages().AddRazorRuntimeCompilation();

           services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));

            ConfigureInjection(services);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication("MTCookieScheme")
              .AddCookie("MTCookieScheme", options =>
              {
                  options.LoginPath = "/login";
                  options.AccessDeniedPath = new PathString("/login?unauth");
              });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    await ErrorHandlingMiddleware.HandleExceptionAsync(context);
                });
            });
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
            app.UseStaticFiles();

        }

        private void ConfigureInjection(IServiceCollection services)
        {
            services.AddDbContext<SoccerDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            var mapper = AutomapperBootstrapper.Init();

            services.AddSingleton(mapper);
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IApplicationUserManager, ApplicationUserManager>();
            services.AddTransient<ILoginModule, LoginModule>();
           
        }
    }
}
