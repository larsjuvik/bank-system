using Microsoft.AspNetCore.Components;

namespace WebApp.Components.Helpers;

public static class NavigationHelper
{
    /// <summary>
    /// Redirects to returnUrl if set, else returns to home screen. Performs force load and replace.
    /// </summary>
    public static void ForceRedirectReturnUrlOrHome(NavigationManager navigation)
    {
        var uri = new Uri(navigation.Uri);
        var returnUrl = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("returnUrl");
        navigation.NavigateTo(returnUrl ?? "/", true, true);
    }
}