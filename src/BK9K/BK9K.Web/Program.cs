using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using DryIoc;
using DryIoc.Microsoft.DependencyInjection;

namespace BK9K.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // await Task.Delay(5000);
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var container = new Container();
            builder.ConfigureContainer(new DryIocServiceProviderFactory(container));
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(container);

            await builder.Build().RunAsync();
        }
    }
}
