namespace WebApp.Settings;
using System.Globalization;

public class CultureOptions
{
    public const string SectionKey = "Culture";
    public string Culture { get; set; } = "en-GB";
    public CultureInfo Info => CultureInfo.GetCultureInfo(Culture);
}