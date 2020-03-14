using Microsoft.Extensions.DependencyInjection;

namespace RemoteFileManager.ViewModels
{
    class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
