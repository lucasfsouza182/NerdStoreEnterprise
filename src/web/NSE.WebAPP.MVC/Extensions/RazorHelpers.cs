using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Razor;

namespace NSE.WebAPP.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static string HashEmailForGravatar(this RazorPage page, string email)
        {
            var md5Hasher = MD5.Create();
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string MessageStock(this RazorPage page, int quantity)
        {
            return quantity > 0 ? $"Only {quantity} in stock!" : "Product out of stock!";
        }

        public static string FormatCurrency(this RazorPage page, decimal price)
        {
            return price > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", price) : "Free";
        }

        public static string UnitsForProduct(this RazorPage page, int units)
        {
            return units > 1 ? $"{units} units" : $"{units} unit";
        }

        public static string SelectOptionsByAmount(this RazorPage page, int unit, int selectedValue = 0)
        {
            var sb = new StringBuilder();
            for (var i = 1; i <= unit; i++)
            {
                var selected = "";
                if (i == selectedValue) selected = "selected";
                sb.Append($"<option {selected} value='{i}'>{i}</option>");
            }

            return sb.ToString();
        }
    }
}
