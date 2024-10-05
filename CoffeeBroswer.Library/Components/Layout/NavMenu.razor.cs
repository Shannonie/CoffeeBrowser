using CoffeeBrowser.Library.Auth;

namespace CoffeeBrowser.Library.Components.Layout;

public partial class NavMenu
{
    private bool _collapseNavMenu = true;

    private string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        _collapseNavMenu = !_collapseNavMenu;
    }

    private async Task Login()
    {
        var authStateProvider = (ICustomAuthStateProvider)AuthStateProvider;
        await authStateProvider.LogInAsync();
    }

    private void Logout()
    {
        var authStateProvider = (ICustomAuthStateProvider)AuthStateProvider;
        authStateProvider.Logout();
    }
}
