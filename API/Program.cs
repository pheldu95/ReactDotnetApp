using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try{
                    //try to create the database, if it doesn't exist yet
                    var context = services.GetRequiredService<DataContext>();
                    context.Database.Migrate();
                    //seed our data from Seed.cs in Domain
                    Seed.SeedData(context);
                }catch(Exception ex){
                    //log out our error
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "error occured during migration");

                }
                //run our application after checking for the database
                host.Run();
            }
        }
        // public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //     WebHost.CreateDefaultBuilder(args)
        //         .UseStartup<Startup>();
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
