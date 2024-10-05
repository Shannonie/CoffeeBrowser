using CoffeeBrowser.Library.Data;
using CoffeeBrowser.Winforms.Auth;
using CoffeeBrowser.Winforms.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeBrowser.Winforms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //dependency injection
            ServiceCollection services = new ServiceCollection();
            services.AddWindowsFormsBlazorWebView();
#if DEBUG
            services.AddBlazorWebViewDeveloperTools();
#endif
            services.AddAuthorizationCore(); // configure authorization services
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

            services.AddTransient<ICoffeeService, CoffeeService>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            //dependency injection

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(serviceProvider));
        }
    }
}