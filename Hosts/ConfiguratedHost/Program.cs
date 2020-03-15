using System;
using System.ServiceModel;
using RemoteFileManager.Service;

namespace ConfiguratedHost
{
    class Program
    {
        static void Main(string[] args)
        {
             var host = new ServiceHost(typeof(FileManagerService));
             host.Open();

            Console.WriteLine(new string('-', Console.BufferWidth));
            Console.ReadKey();

            host.Close();
            Console.WriteLine(new string('-', Console.BufferWidth));
            Console.ReadKey();
        }
    }
}
