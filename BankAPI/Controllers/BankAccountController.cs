using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        [HttpGet]
        public BankAccount? Get(int? id)
        {
            List<BankAccount>? bankAccounts;
            using (FileStream fs = new FileStream("bankAccount.json", FileMode.OpenOrCreate))
            {
                bankAccounts = JsonSerializer.Deserialize<List<BankAccount>>(fs);
            }
            return bankAccounts.FirstOrDefault(ba => ba.Id == id);
        }
        [HttpPost]
        public BankAccount? Post(int personId)
        {
            List<Person>? people;
            using (FileStream fs = new FileStream("people.json", FileMode.Open))
            {
                people = JsonSerializer.Deserialize<List<Person>>(fs);
            }
            if (!people.Any(ba => ba.Id == personId))
            {
                return null;
            }
            List<BankAccount>? bankAccounts;
            using (FileStream fs = new FileStream("bankAccount.json", FileMode.OpenOrCreate))
            {
                bankAccounts = JsonSerializer.Deserialize<List<BankAccount>>(fs);
            }
            bankAccounts.Add(new BankAccount { Id = Utils.GetRandomId(bankAccounts), PersonId = personId, Sum = 0 });
            using (FileStream fs = new FileStream("bankAccount.json", FileMode.Open))
            {
                JsonSerializer.Serialize<List<BankAccount>>(fs, bankAccounts);
            }
            return bankAccounts.Last();
        }
        [HttpPost]
        public BankAccount? MyPutSum(int id, decimal sum)
        {
            List<BankAccount>? bank;
            using (FileStream fs = new FileStream("bankaccount.json", FileMode.Open))
            {
                bank = JsonSerializer.Deserialize<List<BankAccount>>(fs);
            }
            BankAccount? bankAcc = bank.FirstOrDefault(ba => ba.Id == id);

            if (bankAcc != null)
            {
                bankAcc.Sum += sum;
            }
            using (FileStream fs = new FileStream("bankaccount.json", FileMode.Open))
            {
                JsonSerializer.Serialize<List<BankAccount>>(fs, bank);
            }
            return bankAcc;
        }
    }

}
