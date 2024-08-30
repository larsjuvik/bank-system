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
        struct User
        {
            public string Username { get; set; }
            public string Role { get; set; }
        }

        public DemoAuthenticationStateProvider(ProtectedSessionStorage localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task Initialize()
        {
            // Get the authentication state from local storage
            var state = await localStorage.GetAsync<User?>("authenticationState");
            if (state.Value != null)
            {
                var identity = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, state.Value.Value.Username),
                new Claim(ClaimTypes.Role, state.Value.Value.Role)
            ], "demo");

                _currentUser = new ClaimsPrincipal(identity);

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
            ], "demo");

            // Create a new ClaimsPrincipal with the identity
            var user = new ClaimsPrincipal(identity);

            // Store the authentication state in local storage
            await localStorage.SetAsync("authenticationState", new
            {
                Username = username,
                Role = "User"
            });

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