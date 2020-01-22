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
using GradebookBackend.ServicesCore;
using GradebookBackend.Services;
using GradebookBackend.Settings;

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
            services.AddMvc();

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
                    ClockSkew = TimeSpan.FromHours(5),
                };
            });
            services.AddTransient<IClassService, ClassService>();
            services.AddTransient<IGradeService, GradeService>();
            services.AddTransient<ILessonService, LessonService>();
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IAttendanceService, AttendanceService>();
            services.AddTransient<IAttendanceStatusService, AttendanceStatusService>();
            services.AddTransient<ITokenGeneratorService, TokenGeneratorService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITeacherSubjectService, TeacherSubjectService>();
            services.AddTransient<IUserProviderService, UserProviderService>();
            services.AddHttpContextAccessor();

            services.AddSingleton<ITokenSettings>(tokenSettings);
            services.AddSingleton<IDevelopmentSettings>(devSettings);

            services.AddDbContextPool<GradebookDbContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("GradebookDBConnection")));
            services.AddScoped<IRepository<LessonDAO>, LessonRepository>();
            services.AddScoped<IRepository<SubjectDAO>, SubjectRepository>();
            services.AddScoped<IRepository<ClassDAO>, ClassRepository>();
            services.AddScoped<IRepository<GradeDAO>, GradeRepository>();
            services.AddScoped<IRepository<AttendanceDAO>, AttendanceRepository>();
            services.AddScoped<IRepository<AttendanceStatusDAO>, AttendanceStatusRepository>();
            services.AddScoped<IRepository<NoteDAO>, NoteRepository>();
            services.AddScoped<IRepository<StudentDAO>, StudentRepository>();
            services.AddScoped<IRepository<TeacherDAO>, TeacherRepository>();
            services.AddScoped<IRepository<AdminDAO>, AdminRepository>();
            services.AddScoped<IRepository<RoleDAO>, RoleRepository>();
            services.AddScoped<IRepository<UserDAO>, UserRepository>();
            services.AddScoped<IRepository<ClassSubjectDAO>, ClassSubjectRepository>();
            services.AddScoped<IRepository<TeacherSubjectDAO>, TeacherSubjectRepository>();

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
