using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebsiteStatus
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        //Using HttpClient is something that should be called once in an application, not everytime the application is in use as it can be resource intensive on your system.
        private HttpClient client;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            //This ensures that the HttpClient starts when the worker service starts running.
            client = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        //This method is meant to be called when the service is shut down
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            client.Dispose();
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //The code below prints the text below every second.
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                //Call the client (HttpCient) here.
                //The "Task.Delay" is what delays the messages for a second, 60*1000 specifies 60000 milliseconds which is 60 seconds.
                var result = await client.GetAsync("http://www.afrotada.com/");
                
                //This is where we trigger a log report if the website is up.
                if (result.IsSuccessStatusCode)
                {
                    //This is the logger and we're adding the status code so we can get as much of a feedback as possible
                    _logger.LogInformation($"Afrotada is up and running. Status code {result.StatusCode}");
                }
                //The "else" statement triggers if the website is down.
                else
                {
                    //Normally, you should also write a mechanism that sends an email to someone that the platform is down at this point.
                    //This is the logger and we're adding the status code so we can get as much of a feedback as possible
                    _logger.LogError($"Afrotada is down, don't panick. Status code {result.StatusCode}");
                }
                await Task.Delay(60*1000, stoppingToken);
            }
        }
    }
}
