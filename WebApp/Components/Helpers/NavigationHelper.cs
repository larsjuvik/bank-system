using Microsoft.AspNetCore.Components;

namespace WebApp.Components.Helpers;

public static class NavigationHelper
{
    /// <summary>
    /// Redirects to returnUrl if set, else returns to home screen.
    /// Performs force load and history stack replace.
    /// </summary>
    public static void ForceRedirectReturnUrlOrHome(NavigationManager navigation)
    {
        var uri = new Uri(navigation.Uri);
        var returnUrl = System.Web.HttpUtility.ParseQueryString(uri.Query).Get("returnUrl");
        navigation.NavigateTo(returnUrl ?? "/", true, true);
    }

    /// <summary>
    /// Redirects to home screen.
    /// Performs force load and history stack replace.
    /// </summary>
    public static void ForceRedirectHome(NavigationManager navigation)
    {
        navigation.NavigateTo("/", true, true);
    }
}