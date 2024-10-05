using System.Security.Claims;
using CoffeeBrowser.Library.Auth;
using Microsoft.AspNetCore.Components.Authorization;

namespace CoffeeBrowser.Maui.Auth;

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
        /*
            Provide OpenID/MSAL code to authenticate the user. See your identity 
            provider's documentation for details.

            Return a new ClaimsPrincipal based on a new ClaimsIdentity.
        */

        Claim[] claims = [
            new Claim(ClaimTypes.Name, "Julia"),
            new Claim(ClaimTypes.Role, "Admin"),
        ];

        ClaimsIdentity identity = new ClaimsIdentity(claims, "Custom"); // set it to "custom"

        ClaimsPrincipal authenticatedUser = new ClaimsPrincipal(identity);

        return Task.FromResult(authenticatedUser);
    }

    public void Logout()
    {
        currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(currentUser)));
    }
}