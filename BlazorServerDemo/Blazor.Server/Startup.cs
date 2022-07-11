using Blazor.Shared.Interfaces;
using Blazor.Server.Data;
using Blazor.Server.Interfaces;
using Blazor.Server.Services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Blazor.Server;

public class Startup
{
    bool withAuthOption = false;

    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration) 
    {
        this.Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        if (withAuthOption)
        {
            // Add Windows Authentication
            services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();

            services.AddAuthorization(options =>
            {
                // By default, all incoming requests will be authorized according to the default policy.
                options.FallbackPolicy = options.DefaultPolicy;
            });
        }
        else
        {
            // [Authorize]
            // Controller 

            // No Windows Authentication
            // Ok lets map to a user for testing docker stuff purpose only
            // TODO
        }

        // Add services to the container.
        services.AddHttpContextAccessor();
        services.AddControllersWithViews();
        services.AddRazorPages();
        services.AddServerSideBlazor();

        // Swagger
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        //
        // Add scoped services to the container.
        //

        services.AddScoped<IRepository>(sp =>
        {
            string connectionString = this.Configuration.GetConnectionString("bob");
            return new Repository(new Database(connectionString));
        });

        services.AddScoped<IUserService,   UserService>();
        services.AddScoped<IValuesService, ValuesService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        // Swagger
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapRazorPages();
            endpoints.MapControllers();
            endpoints.MapFallbackToFile("index.html");
            //endpoints.MapFallbackToPage("/Error");
        });
    }
}
