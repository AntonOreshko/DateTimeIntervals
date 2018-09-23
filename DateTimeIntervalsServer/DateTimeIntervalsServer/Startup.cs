using System;
using System.Text;
using AutoMapper;
using DateTimeIntervals.Api.Middleware;
using DateTimeIntervals.DomainLayer.Data;
using DateTimeIntervals.DomainLayer.Repositories;
using DateTimeIntervals.Logger.Data;
using DateTimeIntervals.Logger.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DateTimeIntervals.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DateTimeIntervalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("DateTimeIntervals.DomainLayer");
                }));
            services.AddDbContext<LoggerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LoggerConnection"),
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly("DateTimeIntervals.Logger");
                }));

            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IDateTimeIntervalsRepository, DateTimeIntervalsRepository>();
            services.AddScoped<ILoggerRepository, LoggerRepository>();

            ConfigureAuth(services);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerRepository logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(RequestLogMiddleware));
            app.UseAuthentication();
            app.UseMvc();
        }

        protected virtual void ConfigureAuth(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Secret").Value)
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
        }
    }
}
