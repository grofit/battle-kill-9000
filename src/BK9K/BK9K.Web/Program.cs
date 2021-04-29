using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using BK9K.Web.Infrastructure.DI;
using BK9K.Web.Modules;
using DryIoc.Microsoft.DependencyInjection;

namespace BK9K.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.ConfigureContainer(new DryIocServiceProviderFactory());
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddModule(new GameModule());
            builder.Services.AddModule(new OpenRpgModule());

            await builder.Build().RunAsync();
        }
    }
}
