using Microsoft.AspNetCore.Mvc.Razor;
using System.Security.Cryptography;
using System.Text;

namespace ALC.WebApp.MVC.Extensions;

public static class RazorHelpers
{
    public static string HashEmailForGravatar(this RazorPage page, string email)
    {
        var md5Hasher = MD5.Create();
        email = string.IsNullOrEmpty(email) ? "" : email.Trim().ToLowerInvariant();
        var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
        var sBuilder = new StringBuilder();
        foreach (var t in data)
        {
            sBuilder.Append(t.ToString("x2"));
        }
        return sBuilder.ToString();
    }

    public static string FormatCurrency(this RazorPage page, decimal valor)
    {
        return valor > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", valor) : "Free";
    }

    public static string StockMessage(this RazorPage page,  int qtd)
    {
        return qtd > 0 ? $"Last {qtd} in stock!" : "Product not available";
    }

}
