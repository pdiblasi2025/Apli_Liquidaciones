using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Api.Core.Infrastructure;
using Api.Core.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Api.Core.Admin;
using Microsoft.OpenApi.Models;
using Api.Core.Services.Interfaces;
using Api.Core.Services.Implementations;
using FluentValidation.AspNetCore;
using Api.Core.Services;
using System;
using Quartz;
using Api.Core.Jobs;
using AutoMapper;
using Newtonsoft.Json;
using DocumentFormat.OpenXml.InkML;
using System.Globalization;

namespace Api.Core
{
    public class Startup
    {
        private readonly IHostEnvironment _environment;
        private AppSettings _appSettings;
        public IConfiguration Configuration { get; }
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));


        public Startup(IConfiguration configuration, Microsoft.Extensions.Hosting.IHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
            _appSettings = new AppSettings();
            Configuration.Bind("App", _appSettings);
        }

 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson().AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
                s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true; // false by default
            })
            .AddNewtonsoftJson(options => {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "IFlow.Liquidacion API.Core Service",
                    Description = "API for IFlow.Liquidacion",
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            services.AddAutoMapper(typeof(MapperConfiguration));
            services.Configure<AppSettings>(Configuration.GetSection("App"));
            services.AddSingleton<AppSettings>();
            services.AddScoped(typeof(AuthAdmin));
            services.AddScoped<ILocalidadService, LocalidadService>();
            services.AddScoped<IProvinciaService, ProvinciaService>();
            services.AddScoped<ICondicionPagoService, CondicionPagoService>();
            services.AddScoped<IImpuestoService, ImpuestoService>();
            services.AddScoped<IOmsEnvioService, OmsEnvioService>();
            services.AddScoped<IOmsOrdenService, OmsOrdenService>();
            services.AddScoped<IErpMilongaMasterService, ErpMilongaMasterService>(); 
            services.AddScoped<IErpMilongaPaymentMethodService, ErpMilongaPaymentMethodService>();
            services.AddScoped<IErpMilongaTaxTypeService, ErpMilongaTaxTypeService>();
            services.AddScoped<IErpMilongaIdentificationTypeService, ErpMilongaIdentificationTypeService>();
            services.AddScoped<IErpMilongaTaxCodeService, ErpMilongaTaxCodeService>();
            services.AddScoped<IErpMilongaProductTypeService, ErpMilongaProductTypeService>();
            services.AddScoped<IErpMilongaInvoiceService, ErpMilongaInvoiceService>();
            services.AddScoped<IErpMilongaUnitOfMeasureService, ErpMilongaUnitOfMeasureService>();
            services.AddScoped<IErpMilongaProductCodeService, ErpMilongaProductCodeService>();
            services.AddScoped<ITipoImpuestoService, TipoImpuestoService>();
            services.AddScoped<ITipoDocumentoService, TipoDocumentoService>();
            services.AddScoped<ICodigoProductoService, CodigoProductoService>();
            services.AddScoped<IOmsSyncLogService, OmsSyncLogService>();

            services.AddHttpClient<IOmsService, OmsService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(Configuration["Oms:ApiBaseAddress"]);
                httpClient.DefaultRequestHeaders.Add("apikey", Configuration["Oms:ApiKey"]);
            });

            services.AddHttpClient<IErpMilongaService, ErpMilongaService>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(Configuration["Erp:ApiBaseAddress"]);
            });


            services.AddCors(options =>
            {
                options.AddPolicy("Core",
                builder =>
                {
                    builder.WithOrigins(_appSettings.Cors.Split(new char[] { ',' }))
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = _appSettings.Authority;
                o.Audience = _appSettings.Audience;
                o.SaveToken = true;
            });

            System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };

            services.AddDbContext<MyContext>(options => options.UseMySql(_appSettings.ConnectionStrings["DefaultConnection"]));

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var clientJobKey = new JobKey("ClientJob");

                q.AddJob<ClientJob>(opts => opts.WithIdentity(clientJobKey));

                q.AddTrigger(opts => opts
                    .ForJob(clientJobKey)
                    .WithIdentity("ClientJob-trigger")
                    .WithCronSchedule(Configuration["Oms:ClientJobCron"]));

                var startAt = DateTimeOffset.ParseExact("02/05/2023 00:00:00 -03:00",
                                 "dd/MM/yyyy HH:mm:ss zzz",
                                 CultureInfo.InvariantCulture);

                var orderJobKey = new JobKey("OrderJob");

                q.AddJob<OrderJob>(opts => opts.WithIdentity(orderJobKey));

                q.AddTrigger(opts => opts
                    .ForJob(orderJobKey)
                    .WithIdentity("OrderJob-trigger")
                    .StartAt(startAt)
                    .WithSchedule(CronScheduleBuilder.CronSchedule(Configuration["Oms:OrderJobCron"])));

                var shippingJobKey = new JobKey("ShippingJob");

                q.AddJob<ShippingJob>(opts => opts.WithIdentity(shippingJobKey));

                q.AddTrigger(opts => opts
                    .ForJob(shippingJobKey)
                    .WithIdentity("ShippingJob-trigger")
                    .StartAt(startAt)
                    .WithSchedule(CronScheduleBuilder.CronSchedule(Configuration["Oms:ShippingJobCron"])));

                var erpMasterobKey = new JobKey("ErpMilongaMasterJob");

                q.AddJob<ErpMilongaMasterJob>(opts => opts.WithIdentity(erpMasterobKey));

                q.AddTrigger(opts => opts
                    .ForJob(erpMasterobKey)
                    .WithIdentity("ErpMilongaMasterJob-trigger")
                   .WithCronSchedule(Configuration["Erp:MasterJobCron"]));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        }
 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<MyContext>();

                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (!env.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "IFlow Liquidacion API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseGlobalExceptionHandler();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("Core");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            BootStrapper.BootStrap();
        }
    }
}
