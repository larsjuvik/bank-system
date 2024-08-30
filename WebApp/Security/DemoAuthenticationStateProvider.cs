using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace WebApp.Security
{
    public class DemoAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage localStorage;
        private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

        public DemoAuthenticationStateProvider(ProtectedSessionStorage localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task Initialize()
        {
            // Get the authentication state from local storage
            var state = await localStorage.GetAsync<ClaimsPrincipal?>("authenticationState");
            if (state.Value != null && state.Value.Identity != null && state.Value.Identity.IsAuthenticated)
            {
                _currentUser = state.Value;
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_currentUser)));
            }
        }

        /// <summary>
        /// Get the current authentication state which means
        /// the current user and its roles.
        /// </summary>
        /// <returns></returns>
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }


        public async Task<bool> Login(string username, string password)
        {
            bool isValid = true;
            // TODO: Implement the authentication logic here
            if (!isValid)
            {
                return false;
            }

            // Create a new ClaimsIdentity with the user's name and role
            var identity = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "User")
            ], "User");

            // Create a new ClaimsPrincipal with the identity
            var user = new ClaimsPrincipal(identity);

            // Store the authentication state in local storage
            await localStorage.SetAsync("authenticationState", user);

            // Notify the authentication state has changed
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));

            return true;
        }

        public async Task Logout()
        {
            // Remove the authentication state from local storage
            await localStorage.DeleteAsync("authenticationState");

            // Notify the authentication state has changed
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }
    }
}