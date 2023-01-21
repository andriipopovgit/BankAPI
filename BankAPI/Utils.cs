using System.Text.Json;

namespace BankAPI
{
    public class Utils
    {
        public static int GetRandomId<T>(List<T> items) where T : IIdentify
        {
            Random random = new Random();
            int id;
            do
            {
                id = random.Next();
                if (items.All(item => item.Id != id))
                    return id;
            } while (true);
        }
    }
}
