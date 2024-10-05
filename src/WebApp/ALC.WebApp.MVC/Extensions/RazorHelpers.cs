using System;

namespace ALC.WebApp.MVC.Extensions;

public static class RazorHelpers
{
    public static string FormatCurrency(this decimal valor)
    {
        return valor > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", valor) : "Free";
    }
}
