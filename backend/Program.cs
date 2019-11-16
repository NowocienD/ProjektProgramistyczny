using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;


namespace GradeBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
            {
                configurationBuilder
                    .AddJsonFile("backend/appsettings.json", optional: false, reloadOnChange: true);
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    //.UseKestrel()
                    .UseIISIntegration()
                    .UseStartup<Startup>();
                });
    }
}