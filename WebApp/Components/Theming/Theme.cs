using MudBlazor;

namespace WebApp.Components.Theming;

public class Theme
{
    public static MudTheme GetApplicationTheme()
    {
        return new MudTheme
        {
            PaletteLight = new PaletteLight
            {
                Background = "#f3f3f3",
                AppbarBackground = Colors.Blue.Darken1,
            },
            PaletteDark = new PaletteDark
            {
                Primary = Colors.Blue.Lighten1
            },
        };
    }
}