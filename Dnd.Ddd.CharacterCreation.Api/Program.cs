using System;
using System.Reflection;

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using NLog;
using NLog.Web;

using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Dnd.Ddd.CharacterCreation.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rootLogger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                rootLogger.Debug("Startiung application...");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                rootLogger.Fatal(e, "Unhandled exception occured; shutting down...");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
                .ConfigureLogging(
                    logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                .ConfigureServices(
                    services =>
                    {
                        services.AddSwaggerGen(
                            c =>
                            {
                                c.SwaggerDoc(
                                    "v1",
                                    new OpenApiInfo
                                    {
                                        Title = "D&D Character Creation WebApi",
                                        Version = "v1",
                                        Description =
                                            $"<p><strong>Build version: </strong>{typeof(Program).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>()!.Version}</p>"
                                    });
                            });
                    })
                .UseNLog();
    }
}