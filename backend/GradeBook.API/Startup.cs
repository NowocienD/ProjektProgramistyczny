using System;
using GradeBook.API;
using GradeBook.API.Controllers;
using GradeBook.API.Core;
using GradeBook.API.Core.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GradeBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //DevelopmentSettings devSettings = new DevelopmentSettings();
            //Configuration.GetSection("DevelopmentSettings").Bind(devSettings);
            //if (devSettings.IsDevelopment)
            //{
            //    services.AddMvc(option =>
            //    {
            //        option.EnableEndpointRouting = false;
            //        option.Filters.Add(new AllowAnonymousFilter());
            //    });
            //}
            //else
            //{
            //services.AddMvc(option =>
            //{
            //    option.EnableEndpointRouting = false;
            //});
            //}
            
            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddCors(options =>
            {            
                CorsSettings corsSettings = new CorsSettings();
                Configuration.GetSection("CorsSettings").Bind(corsSettings);
                
                options.AddPolicy(
                    "default",
                    builder =>
                    {
                        builder
                        .WithOrigins(corsSettings.Origins)
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = SigningController.SigningKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5),
                };
            });

            services.AddTransient<ITokenGeneratorService, TokenGeneratorService>();
            //services.AddSingleton<IDevelopmentSettings>(devSettings);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            app.UseCors("default");
            
            app.UseRouting();
            app.UseMvc();
        }
    }
}
