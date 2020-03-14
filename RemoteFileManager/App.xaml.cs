using System;
using Microsoft.Extensions.DependencyInjection;
using RemoteFileManager.ViewModels;

namespace RemoteFileManager
{
    public partial class App
    {
        public static IServiceProvider Services { get; } = 
            ConfigureServices(new ServiceCollection())
               .BuildServiceProvider();

        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();

            return services;
        }
    }
}
