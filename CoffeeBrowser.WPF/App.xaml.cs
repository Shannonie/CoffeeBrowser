using CoffeeBrowser.Library.Data;
using CoffeeBrowser.WPF.Auth;
using CoffeeBrowser.WPF.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace CoffeeBrowser.WPF;

public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        //dependency injection
        ServiceCollection services = new ServiceCollection();
        services.AddWpfBlazorWebView();
#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif
        //dependency injection
        services.AddAuthorizationCore(); // configure authorization services
        services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

        services.AddTransient<ICoffeeService, CoffeeService>();
        //dependency injection

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        this.Resources.Add("ServiceProvider", serviceProvider);
        //dependency injection
    }
}

