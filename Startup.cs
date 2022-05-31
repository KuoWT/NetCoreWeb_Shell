using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.PlatformAbstractions;

namespace WMS_API
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
            //With webAPI
            services.AddControllers();
            //With view
            //services.AddControllersWithViews();

            //CORS
            services.AddCors(options =>
            {
                // CorsPolicy 是自訂的 Policy 名稱
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .WithMethods("GET", "POST", "PUT", "DELETE", "PATCH");
                });
            });

            //UseStaticFiles - Serve files inside of web root (wwwroot folder)
            //UseSpaStaticFiles - Serve static file like image, css, js in asset folder of angular app
            //UseSpa - let asp.net core know which directory you want to run your angular app, what dist folder when running in production mode and which command to run angular app in dev mode
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            //WebAPI Doc Generate
            services.AddSwaggerGen(c =>
            {
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Api.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // else
            // {
            //     app.UseExceptionHandler("/Home/Error");
            //     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //     app.UseHsts();
            // }

            app.UseHttpsRedirection();
            //在 web 根目錄中提供檔案
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(/*
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"*/);
            });
        }
    }
}
