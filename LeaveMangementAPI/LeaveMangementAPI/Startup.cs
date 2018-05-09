using LeaveMangement_Application.DangAn;
using LeaveMangementAPI.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
            //services.AddDbContext<KaoQinContext>(options =>
            //   options.UseSqlServer(@"Server=DESKTOP-BD1U6I5;Database=KaoQin;Integrated Security=True;"));
            services.AddMvc();
            #region Swagger配置
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
                c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });
            #endregion
            //依赖注入
            services.AddTransient<IDangAnAppService, DangAnAppService>();
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
