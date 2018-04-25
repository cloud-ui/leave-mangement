﻿using LeaveMangementAPI.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace LeaveMangementAPI
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
            services.AddMvc();
            #region Swagger配置

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info
            //    {
            //        Version = "v1",

            //        Title = "LeaveMangementAPI接口文档",

            //        Description = "RESTful API for LeaveMangementAPI",

            //        TermsOfService = "None",

            //        Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Alvin_Su", Email = "asdasdasd@outlook.com", Url = "" }
            //    });
            //    //Set the comments path for the swagger json and ui.
            //    var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            //    var xmlPath = Path.Combine(basePath, "LeaveMangementAPI.xml");
            //    c.IncludeXmlComments(xmlPath);
            //    c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            //});
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "LeaveMangementAPI接口文档",
                    Version = "v1",
                    Description = "RESTful API for LeaveMangementAPI",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Joy",
                        Email = "1298520866@qq.com"
                    },
                });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "LeaveMangementAPI.xml");
                c.IncludeXmlComments(filePath);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //配置Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeaveMangementAPI API V1");
            });
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeaveMangementAPI API V1");
            //    c.SupportedSubmitMethods();
            //});


            app.UseMvc();
        }
    }
}