using CoffeeBrowser.Library.Components;
using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeeBrowser.Winforms
{
    public partial class MainForm : Form
    {
        public MainForm(ServiceProvider serviceProvider)
        {
            InitializeComponent();
            blazorWebView.HostPage = "wwwroot/index.html";
            blazorWebView.Services = serviceProvider;
            blazorWebView.RootComponents.Add(
                new RootComponent("#app", typeof(Routes), null));
        }
    }
}
