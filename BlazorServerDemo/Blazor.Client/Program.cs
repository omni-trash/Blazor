using Blazor.Client;
using Blazor.Client.Services;
using Blazor.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Blazor.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddScoped(sp =>
        {
            return new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            };
        });

        builder.Services.AddScoped<IValuesService, ValuesService>();

        await builder.Build().RunAsync();
        //https://www.youtube.com/watch?v=AYXx5vPFzFo
    }
}
