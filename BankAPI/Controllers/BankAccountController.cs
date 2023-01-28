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
            using (FileStream fs = new FileStream("bankAccount.json", FileMode.Create))
            {
                JsonSerializer.Serialize<List<BankAccount>>(fs, bankAccounts);
            }
            return bankAccounts.Last();
            //test
        }
        [HttpPost("PutMoney")]
        public BankAccount? PutMoney(int id, decimal sum)
        {
            List<BankAccount>? bank;
            using (FileStream fs = new FileStream("bankAccount.json", FileMode.Open))
            {
                bank = JsonSerializer.Deserialize<List<BankAccount>>(fs);
            }
            BankAccount? bankAcc = bank.FirstOrDefault(ba => ba.Id == id);

            if (bankAcc != null)
            {
                bankAcc.Sum += sum;
            }
            using (FileStream fs = new FileStream("bankAccount.json", FileMode.Create))
            {
                JsonSerializer.Serialize<List<BankAccount>>(fs, bank);
            }
            return bankAcc;
        }
        [HttpGet("GetAll")]
        public List<BankAccount>? GetAll()
        {
            List<BankAccount>? bankAccounts = null;
            using (FileStream fs = new FileStream("bankAccount.json", FileMode.Open))
            {
                bankAccounts = JsonSerializer.Deserialize<List<BankAccount>>(fs);
            }
            return bankAccounts;
        }
        [HttpPost("GetMoney")]
        public bool GetMoney(int id, decimal sum)
        {
            List<BankAccount>? bankAccounts = null;
            using (FileStream fs = new FileStream("bankAccount.json", FileMode.Open))
            {
                bankAccounts = JsonSerializer.Deserialize<List<BankAccount>>(fs);
            }
            BankAccount? tempaccount;
            tempaccount = bankAccounts.FirstOrDefault(ba => ba.Id == id);
            if (tempaccount != null)
            {
                if (tempaccount.Sum >= sum)
                {
                    tempaccount.Sum -= sum;
                    using (FileStream fs = new FileStream("bankAccount.json", FileMode.Create))
                    {
                        JsonSerializer.Serialize<List<BankAccount>>(fs, bankAccounts);
                    }
                    return true;
                }
            }
            return false;
        }
    }

}
