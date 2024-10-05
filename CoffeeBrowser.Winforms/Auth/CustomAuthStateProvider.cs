using System.Security.Claims;
using System.Security.Principal;
using CoffeeBrowser.Library.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace CoffeeBrowser.Winforms.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider, ICustomAuthStateProvider
{
    private ClaimsPrincipal currentUser = new ClaimsPrincipal(new ClaimsIdentity());

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        Task.FromResult(new AuthenticationState(currentUser));

    public Task LogInAsync()
    {
        var loginTask = LogInAsyncCore();
        NotifyAuthenticationStateChanged(loginTask);

        return loginTask;

        async Task<AuthenticationState> LogInAsyncCore()
        {
            var user = await LoginWithExternalProviderAsync();
            currentUser = user;

            return new AuthenticationState(currentUser);
        }
    }

    /// <summary>
    /// Return authenty user
    /// </summary>
    /// <returns></returns>
    private Task<ClaimsPrincipal> LoginWithExternalProviderAsync()
    {

        ClaimsPrincipal authenticatedUser = GetWindowsPrincipal();

        return Task.FromResult(authenticatedUser);
    }

    private ClaimsPrincipal GetWindowsPrincipal()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();

        return new WindowsPrincipal(identity);
    }

    public void Logout()
    {
        currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(currentUser)));
    }
}