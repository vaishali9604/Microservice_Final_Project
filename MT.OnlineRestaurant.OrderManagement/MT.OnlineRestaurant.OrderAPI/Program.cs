using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MT.OnlineRestaurant.MessageManagement;

namespace MT.OnlineRestaurant.OrderAPI
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //IMessagePublisher messagePublisher = null;
            //messagePublisher.RegisterOnMessageHandlerAndReceiveMessages();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .UseDefaultServiceProvider(options =>
            options.ValidateScopes = false)
                .Build();
    }
}
