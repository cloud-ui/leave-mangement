using LeaveMangement_Application.Approval;
using LeaveMangement_Application.Attendance;
using LeaveMangement_Application.Common;
using LeaveMangement_Application.DangAn;
using LeaveMangement_Application.Notice;
using LeaveMangement_Application.Permission;
using LeaveMangement_Application.User;
using LeaveMangementAPI.Authorization;
using LeaveMangementAPI.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Text;

namespace LeaveMangementAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        #region jwt
        public void ConfigureJwtAuthService(IServiceCollection services)
        {
            var audienceConfig = Configuration.GetSection("TokenAuthentication:Audience").Value;
            var symmetricKeyAsBase64 = Configuration.GetSection("TokenAuthentication:SecretKey").Value;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);

            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!  
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim  
                ValidateIssuer = true,
                ValidIssuer = audienceConfig,

                // Validate the JWT Audience (aud) claim  
                ValidateAudience = true,
                ValidAudience = audienceConfig,

                // Validate the token expiry  
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = tokenValidationParameters;
            });
        }
        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //session
            services.AddSession();

            //configure the jwt   
            ConfigureJwtAuthService(services);

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


            //配置signalr
            services.AddSignalR();
            
            //配置跨域处理
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });

           
            
            //依赖注入
            services.AddTransient<IDangAnAppService, DangAnAppService>();
            services.AddTransient<IUserAppService, UserAppService>();
            services.AddTransient<ICommonAppService, CommonAppService>();
            services.AddTransient<IApprovalAppService, ApprovalAppService>();
            services.AddTransient<IPermissionAppService, PermissionAppServer>();
            services.AddTransient<INoticeAppService, NoticeAppService>();
            services.AddTransient<IAttendanceAppService, AttendanceAppService>();

            services.AddSingleton<IServiceProvider, ServiceProvider>();
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

            //session
            app.UseSession();

            //use the authentication  
            app.UseAuthentication();

            //signalr
            app.UseSignalR(routes =>
            {
               routes.MapHub<SignalrHubs>("/signalrHubs");
            });


            app.UseWebSockets();
            app.UseMvc();
        }
    }
}
