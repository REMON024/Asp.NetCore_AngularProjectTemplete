using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using NybSys.API.ServiceCollection;
using NybSys.AuditLog.DAL;
using NybSys.DAL;
using NybSys.Session.DAL;
using NybSys.UnitOfWork;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using NybSys.Mqtt;
using NybSys.MassTransit;
using NybSys.RedisSession.DAL;
using NybSys.API.DBInitializer;
using System;

namespace NybSys.API
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
            services.AddMvc(options =>
            {
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver
                    = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });



            //AddMassTransit(services);

            //AddMqtt(services);

            AddDatabase(services);

            AddJWTToken(services);

            services.AddDBInitializer();

            services.AddUnitOfWork<DatabaseContext>();

            services.AddBLL();
            services.AddManager();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AuditLogContext auditLogContext, DatabaseContext databaseContext, IDBInitializer dBInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });


            //IDBInitializer dBInitializer = app.ApplicationServices.GetService<IDBInitializer>();

            bool isAuditDbCreate = auditLogContext.Database.EnsureCreated();
            bool isDbCreate =  databaseContext.Database.EnsureCreated();
            

            dBInitializer.StartDbInitialize(isAuditDbCreate, isDbCreate).GetAwaiter().GetResult();

            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NybSys API V1");
            });

            app.UseAuthentication();
            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private IServiceCollection AddDatabase(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Defualt"));
            });

            services.AddDbContext<AuditLogContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AuditLog"));
            });

            //services.AddDbContext<SessionContext>(options =>
            //{
            //    options.UseSqlite(Configuration.GetConnectionString("Session"));
            //    //options.UseInMemoryDatabase("SessionContext");
            //});

            // Add Redis Cache DB for session storage
            //services.AddRedisSession(options =>
            //{
            //    options.Configuration = "127.0.0.1";
            //    options.InstanceName = "SessionCache";
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "NybSys API", Version = "v1" });
            });

            return services;
        }

        private IServiceCollection AddJWTToken(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:TokenKey"]))
                };
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });

            // CORS Setup
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                .Build());
            });

            return services;
        }

        //private IServiceCollection AddMassTransit(IServiceCollection services)
        //{
        //    services.AddAspNetCoreMassTransit(config =>
        //    {
        //        config.SetupConnection(connection =>
        //        {
        //            connection.RabbitMQConnectionString = Configuration["MassTransit:Host"];
        //            connection.Port = Configuration["MassTransit:Port"];
        //            connection.Timeout = Convert.ToInt32(Configuration["MassTransit:Timeout"]);
        //            connection.Username = Configuration["MassTransit:Username"];
        //            connection.Password = Configuration["MassTransit:Password"];
        //        });

        //        config.AddRequestResponse<Models.ViewModels.Request, Models.ViewModels.Response>("Worker");
        //    });

        //    return services;
        //}

        //private IServiceCollection AddMqtt(IServiceCollection services)
        //{
        //    services.AddMqttServer(config =>
        //    {
        //        config.AddConnection(connection =>
        //        {
        //            connection.MQBrokerHost = Configuration["Mqtt:Host"];
        //            connection.MQbrokerPort = Convert.ToInt32(Configuration["Mqtt:Port"]);
        //            connection.AlivePeriod = Convert.ToInt32(Configuration["Mqtt:AlivePeriod"]);
        //            connection.ConnectionTimeout = Convert.ToInt32(Configuration["Mqtt:ConnectionTimeout"]);
        //        });
        //    });

        //    return services;
        //}
    }
}
