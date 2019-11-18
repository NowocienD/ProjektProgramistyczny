using System;
using GradeBook.API;
using GradeBook.API.Controllers;
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
                    IssuerSigningKey = SigningController.signingKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5),
                };
            });

            services.AddTransient<ITokenGeneratorService, TokenGeneratorService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthorization();

            app.UseCors("default");
            
            app.UseRouting();
            app.UseMvc();
        }
    }
}
