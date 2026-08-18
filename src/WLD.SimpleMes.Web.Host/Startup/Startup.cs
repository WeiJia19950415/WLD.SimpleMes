using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Castle.Facilities.Logging;
using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.Extensions;
using WLD.SimpleMes.Configuration;
using WLD.SimpleMes.Identity;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Dependency;
using Abp.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.ExceptionHandling;
using Abp.AspNetCore.Mvc.Authorization;
using WLD.SimpleMes.Startup;
using WLD.SimpleMes.AttachFile;
using Abp.Castle.Logging.NLog;
using JHT.AspNetCore.Quartz;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using WLD.SimpleMes.QuaterJob;

namespace WLD.SimpleMes.Web.Host.Startup
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        private const string _apiVersion = "v1";

        private readonly IConfigurationRoot _appConfiguration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IWebHostEnvironment env)
        {
            _hostingEnvironment = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //MVC
            services.AddControllersWithViews(
                options =>
                {
                    // 屏蔽AbpValitToken
                    // options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
                }
            ).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ContractResolver = new AbpMvcContractResolver(IocManager.Instance)
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });


            // 自定义异常包装处理
            services.PostConfigure<MvcOptions>(options =>
            {
                // 去除Abp自带的过滤器
                options.Filters.Remove(new ServiceFilterAttribute(typeof(AbpExceptionFilter)));
                options.Filters.Remove(new ServiceFilterAttribute(typeof(AbpAuthorizationFilter)));
                // 添加自带的过滤器
                options.Filters.AddService(typeof(JHTExceptionFilter));
                options.Filters.AddService(typeof(JHTAbpAuthorizationFilter));
            });

            // Newtonsoft.json 空值的默认值处理
            services.PostConfigure<MvcNewtonsoftJsonOptions>(jsonOptions =>
            {
                jsonOptions.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                jsonOptions.SerializerSettings.ContractResolver = new NullToEmptyStringResolver(IocManager.Instance)
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                };
            });

            // 文件保存路径
            services.Configure<FileSaveOptions>(_appConfiguration.GetSection("FileSaveConfig"));
            services.AddOptions<FileSaveOptions>().Bind(_appConfiguration.GetSection("FileSaveConfig"));

            services.AddAspCoreQuartz(_appConfiguration, configuration =>
            {
                configuration.AddJobListener<JobListener>();
                configuration.UseMicrosoftDependencyInjectionJobFactory();
            });

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            services.PostConfigure<IdentityOptions>((options) =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            });
            // services.AddSignalR();

            // Configure CORS for angular2 UI
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(
                            // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            _appConfiguration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );

            //services.AddAspCoreQuartz(_appConfiguration);

            // Swagger - Enable this line and the related lines in Configure method to enable swagger UI

            if (_hostingEnvironment.IsProduction() == false)
            {
                ConfigureSwagger(services);
            }

            // Configure Abp and Dependency Injection
            return services.AddAbp<SimpleMesWebHostModule>(
                // Configure Nlog logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpNLog().WithConfig(_hostingEnvironment.IsDevelopment()
                        ? "NLog.config"
                        : "NLog.Production.config"
                    )
                )
            );
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseAbp(options => { options.UseAbpRequestLocalization = false; }); // Initializes ABP framework.

            app.UseCors(_defaultCorsPolicyName); // Enable CORS!

            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider()
                {
                    Mappings = { [".exe"] = "application/octect-stream" }
                }
            });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseAbpRequestLocalization();


            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapHub<AbpCommonHub>("/signalr");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
            });

            if (_hostingEnvironment.IsProduction() == false)
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint
                app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

                // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
                app.UseSwaggerUI(options =>
                {
                    // specifying the Swagger JSON endpoint.
                    options.SwaggerEndpoint($"/swagger/{_apiVersion}/swagger.json", $"SimpleMes API {_apiVersion}");
                    options.IndexStream = () => Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("WLD.SimpleMes.Web.Host.wwwroot.swagger.ui.index.html");
                    options.DisplayRequestDuration(); // Controls the display of the request duration (in milliseconds) for "Try it out" requests.  
                }); // URL: /swagger
            }
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(_apiVersion, new OpenApiInfo
                {
                    Version = _apiVersion,
                    Title = "SimpleMes API",
                    Description = "SimpleMes",
                    // uncomment if needed TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "SimpleMes",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/aspboilerplate"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/aspnetboilerplate/aspnetboilerplate/blob/dev/LICENSE"),
                    }
                });
                options.DocInclusionPredicate((docName, description) => true);

                // Define the BearerAuth scheme that's in use
                options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                //add summaries to swagger
                bool canShowSummaries = _appConfiguration.GetValue<bool>("Swagger:ShowSummaries");
                if (canShowSummaries)
                {
                    var hostXmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var hostXmlPath = Path.Combine(AppContext.BaseDirectory, hostXmlFile);
                    options.IncludeXmlComments(hostXmlPath, true);

                    var applicationXml = $"WLD.SimpleMes.Application.xml";
                    var applicationXmlPath = Path.Combine(AppContext.BaseDirectory, applicationXml);
                    options.IncludeXmlComments(applicationXmlPath, true);

                    var webCoreXmlFile = $"WLD.SimpleMes.Web.Core.xml";
                    var webCoreXmlPath = Path.Combine(AppContext.BaseDirectory, webCoreXmlFile);
                    options.IncludeXmlComments(webCoreXmlPath, true);
                }
            });
        }
    }
}

