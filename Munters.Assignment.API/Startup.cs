using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Munters.Assignment.BL;
using Munters.Assignment.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace Munters.Assignment.API
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
            string allowCors = "_myOrigins";

            services.AddControllers();

            services.AddScoped<IHttpGetRequestSender, HttpGetRequestSender>();

            services.AddScoped<IImageService, ImageService>();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            services.AddHttpContextAccessor();

            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy(name: allowCors, builder =>
                {
                    builder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
                });
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string allowCors = "_myOrigins";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(allowCors);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllerRoute(
                //name: "default",
                //pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}