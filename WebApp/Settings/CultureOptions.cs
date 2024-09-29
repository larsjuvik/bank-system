namespace WebApp.Settings;
using System.Globalization;

public class CultureOptions
{
    public const string SectionKey = "Culture";
    public string Culture { get; set; } = "en-GB";
    public string CurrencySymbol { get; set; } = "$";

    public CultureInfo Info
    {
        get
        {
            var info = new CultureInfo(Culture)
            {
                NumberFormat =
                {
                    CurrencySymbol = CurrencySymbol
                }
            };
            return info;
        }
    }
}