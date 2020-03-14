using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using RemoteFileManager.Interfaces.Services;
using RemoteFileManager.Service;

namespace RemoteFileManager.ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string address = "http://localhost:8080/FileServer";
            var host = new ServiceHost(typeof(FileManagerService), new Uri(address));
            host.Description.Behaviors.Add(new ServiceMetadataBehavior());
            //host.AddServiceEndpoint(typeof(IFileManagerService), new BasicHttpBinding(), address);
            host.AddDefaultEndpoints();
            host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexHttpBinding(), "mex");
            host.Open();

            Console.WriteLine("Нажмите Enter для выхода...");
            Console.ReadLine();
        }
    }
}
