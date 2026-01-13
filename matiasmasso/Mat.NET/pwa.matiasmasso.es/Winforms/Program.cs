using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.Design;

namespace Winforms
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddTransient<Form1>();
            }).Build();

            Application.Run(host.Services.GetRequiredService<Form1>());
        }
    }
}