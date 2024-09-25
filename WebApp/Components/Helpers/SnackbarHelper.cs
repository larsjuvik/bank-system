using MudBlazor;

namespace WebApp.Components.Helpers;

public static class SnackbarHelper
{
    public static void GenericError(ISnackbar snackbar)
    {
        snackbar.Add("Something went wrong", Severity.Error);
    }
}