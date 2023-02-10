using System.Text;
using System.Text.Json;

namespace BankAPI
{
    public class Utils
    {
        public static string GetRandomCardNumber()
        {
            Random random = new Random();
            StringBuilder number = new StringBuilder("5");
            for (int i = 0; i < 15; i++)
            {
                number.Append(random.Next(0, 10));
            }
            return number.ToString();
        }
    }
}
