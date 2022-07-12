using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

/*
Note: Client does not support Windows Authentication, but IIS who serves the client.
       Think the Browser remembers that first step of Authentication.
       Client debugging did not work, dont know why, without Authentication it works.

-----------------------------+---------------------------------------------------
 Blazor.Client (wasm)        | Blazor.Server
-----------------------------+---------------------------------------------------
 wasm                       <-- UseBlazorFrameworkFiles 
 ValuesService (http)       --> ValuesController -> ValuesService
                                                        -> UserService  -> AD
                                                        -> Repository   -> Database
-----------------------------+---------------------------------------------------
                Blazor.Shared.Interfaces
                Blazor.Shared.Models
-----------------------------+---------------------------------------------------
*/

/*
Changes when it should be a standalone App

Standalone Blazor Server App
- use Pages and wwwroot from Blazor.Client
- ValuesController not needed anymore

Standalone Blazor Client App (wasm)
- use ValuesService from Blazor.Server
- no AD
- no Backend Database Access
- no Server Infrastructure and Logic
*/

namespace Blazor.Server;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder =>
        {
            builder.UseStartup<Startup>();
        });
    }
}
