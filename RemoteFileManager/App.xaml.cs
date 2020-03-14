using System;
using System.ComponentModel.Design;
using Microsoft.Extensions.DependencyInjection;

namespace RemoteFileManager
{
    public partial class App
    {
        public static IServiceProvider Services { get; } = 
            ConfigureServices(new ServiceCollection())
               .BuildServiceProvider();

        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {


            return services;
        }
    }
}
