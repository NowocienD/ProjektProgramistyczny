using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GradebookBackend.Model;
using Microsoft.EntityFrameworkCore;
using GradebookBackend.Repositories;

namespace GradebookBackend
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

            DevelopmentSettings devSettings = new DevelopmentSettings();
            Configuration.GetSection("DevelopmentSettings").Bind(devSettings);
            if (devSettings.IsDevelopment)
            {
                services.AddMvc(option =>
                {
                    option.EnableEndpointRouting = false;
                    option.Filters.Add(new AllowAnonymousFilter());
                });
            }
            else
            {
                services.AddMvc(option =>
                {
                    option.EnableEndpointRouting = false;
                });
            }

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

            TokenSettings tokenSettings = new TokenSettings();
            Configuration.GetSection("TokenSettings").Bind(tokenSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.SecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5),
                };
            });

            services.AddDbContextPool<GradebookDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("GradebookDBConnection")));
            services.AddScoped<IRepository<Lesson>, LessonRepository>();
            services.AddScoped<IRepository<Subject>, SubjectRepository>();
            services.AddScoped<IRepository<Class>, ClassRepository>();
            services.AddScoped<IRepository<Grade>, GradeRepository>();
            services.AddScoped<IRepository<Attendance>, AttendanceRepository>();
            services.AddScoped<IRepository<Note>, NoteRepository>();
            services.AddScoped<IRepository<Student>, StudentRepository>();
            services.AddScoped<IRepository<Teacher>, TeacherRepository>();
            services.AddScoped<IRepository<Admin>, AdminRepository>();

            services.AddTransient<ITokenGeneratorService, TokenGeneratorService>();
            services.AddTransient<IUserDataService, UserDataService>();
            services.AddSingleton<ITokenSettings>(tokenSettings);
            services.AddSingleton<IDevelopmentSettings>(devSettings);

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseCors("default");
            app.UseRouting();
            app.UseMvc();
        }
    }
}
