using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;

namespace WebsiteStatus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //This is the configuration that ensures that you're able to override Microsoft logger features and create a solution that logs to a specific file path.
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\Users\Public\Documents\WorkerService\AfrotadaLogfile.txt")
                .CreateLogger();
            
            //This starts the logging process
            try
            {
                Log.Information("Starting up the service");
                CreateHostBuilder(args).Build().Run();
                return;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "There was a problem starting the service");
                return;
            }
            finally
            {
                //This ensures that any message that is buffered or in transit is logged before the close of the application.
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                //This configures the service to run as a windows service so it can be deployed.
               .UseWindowsService()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHostedService<Worker>();
               })
           //We declare this so the program knows to use Serilog
           .UseSerilog();
        }
    }
}
