using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAPIPrueba
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureAppConfiguration((env, config) =>
            {
                // Las variables nombradas de la misma forma se mostrará el valor de 
                // la última configuración registrada.

                var ambiente = env.HostingEnvironment.EnvironmentName;

                config.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);

                config.AddJsonFile($"appsettings.{ambiente}.json", optional: true, reloadOnChange: true);

                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }

                var currentConfig = config.Build();
                config.AddAzureKeyVault(currentConfig["Vault"],
                currentConfig["ClientId"],
                currentConfig["ClientSecret"]);

            });
    }
}
