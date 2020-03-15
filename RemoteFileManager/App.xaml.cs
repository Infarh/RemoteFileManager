using System;
using Microsoft.Extensions.DependencyInjection;
using RemoteFileManager.Services;
using RemoteFileManager.Services.Interfaces;
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
            services.AddSingleton<IFileServer, FileServerWCF>();

            return services;
        }
    }
}
