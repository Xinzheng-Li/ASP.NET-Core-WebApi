using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Data;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace WebApi.WebApi
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
            //Set SQLTYPE and DBConnection
            ConnectionFactory.SQLTYPE = Configuration.GetConnectionString("SQLTYPE");
            ConnectionFactory.CONN_STRING = Configuration.GetConnectionString("DBConnection");

            services.AddSwaggerGen(service =>
            {
                service.SwaggerDoc("ManagementV1", new Info { Version = "v1", Title = "ManagementAPI", Description = "Description of ManagementAPI", TermsOfService = "TermsOfService", Contact = new Contact { Name = "Grom", Email = "841789021@qq.com" } });
                service.SwaggerDoc("ServiceV1", new Info { Version = "v1", Title = "ServerAPI", Description = "Description of ServerAPI", TermsOfService = "TermsOfService", Contact = new Contact { Name = "Grom", Email = "841789021@qq.com" } });
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "WebApi.WebApi.xml");
                service.IncludeXmlComments(xmlPath);
            });
                      
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger(s =>
            {
                s.RouteTemplate = "api-docs/{documentName}/swagger.json";
            }
            );
            app.UseSwaggerUI(s=>
            {
                s.RoutePrefix = "api-docs";
                s.SwaggerEndpoint("/api-docs/ServiceV1/swagger.json", "Service API");
                s.SwaggerEndpoint("/api-docs/ManagementV1/swagger.json", "Management API");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
