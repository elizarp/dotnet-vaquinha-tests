using System.Globalization;

namespace Vaquinha.Domain.Extensions
{
    public static class IntExtensions
    {
        public static string ToDinheiroBrString(this int valor)
        {
            return valor.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR"));
        }
    }
}