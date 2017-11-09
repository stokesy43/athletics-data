using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using StapeleyDigital.AthleticsData.Data;
using Swashbuckle.AspNetCore.Swagger;

namespace StapeleyDigital.AthleticsData.Api
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AthleticsDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().AddFluentValidation(
                fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()
            );

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Stapeley Digital - Athletics Data Api", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "StapeleyDigital.AthleticsData.Api.xml");
                c.IncludeXmlComments(xmlPath);
                //c.SchemaFilter<AddFluentValidationRules>();
            });

            services.AddAutoMapper();

            services.AddScoped<IAthleteRepository, AthleteRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IPerformanceRepository, PerformanceRepository>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IStandardRepository, StandardRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AthleticsDataContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            context.EnsureSeedDataForContext();

            app.UseStatusCodePages();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stapeley Digital - Athletics Data Api");
            });

            app.UseMvc();
        }
    }
}
