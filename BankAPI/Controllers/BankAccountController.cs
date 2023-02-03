using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private readonly ILogger<Person> _logger;
        private ApplicationContext db = new ApplicationContext();
        [HttpGet]
        public BankAccount? Get(int? id)
        {
            List<BankAccount>? banks;
            banks = db.BankAccounts?.ToList();
            //List<BankAccount>? bankAccounts;
            //using (FileStream fs = new FileStream("bankAccount.json", FileMode.OpenOrCreate))
            //{
            //    bankAccounts = JsonSerializer.Deserialize<List<BankAccount>>(fs);
            //}
            return banks.FirstOrDefault(p => p.Id == id);
        }
        [HttpPost]
        public BankAccount? Post(int personId)
        {
            
            List<Person>? people;
            people = db.People?.ToList();
            //using (FileStream fs = new FileStream("people.json", FileMode.Open))
            //{
            //    people = JsonSerializer.Deserialize<List<Person>>(fs);
            //}
            if (!people.Any(ba => ba.Id == personId))
            {
                return null;
            }
            List<BankAccount>? banks;
            banks = db.BankAccounts?.ToList();
            //using (FileStream fs = new FileStream("bankAccount.json", FileMode.OpenOrCreate))
            //{
            //    bankAccounts = JsonSerializer.Deserialize<List<BankAccount>>(fs);
            //}
            BankAccount bank = new BankAccount { Id = Utils.GetRandomId(banks), PersonId = personId, Sum = 0 };
            //using (FileStream fs = new FileStream("bankAccount.json", FileMode.Create))
            //{
            //    JsonSerializer.Serialize<List<BankAccount>>(fs, bankAccounts);
            //}
            db.BankAccounts.Add(bank);
            db.SaveChanges();
            return bank;
            //test
        }
        [HttpPost("PutMoney")]
        public BankAccount? PutMoney(int id, decimal sum)
        {
            List<BankAccount>? bank;
            bank = db.BankAccounts?.ToList();
            //using (FileStream fs = new FileStream("bankAccount.json", FileMode.Open))
            //{
            //    bank = JsonSerializer.Deserialize<List<BankAccount>>(fs);
            //}
            BankAccount? bankAcc = bank.FirstOrDefault(ba => ba.Id == id);

            if (bankAcc != null)
            {
                bankAcc.Sum += sum;
            }
            //using (FileStream fs = new FileStream("bankAccount.json", FileMode.Create))
            //{
            //    JsonSerializer.Serialize<List<BankAccount>>(fs, bank);
            //}
            db.SaveChanges();
            return bankAcc;
        }
        [HttpGet("GetAll")]
        public List<BankAccount>? GetAll()
        {
            List<BankAccount>? bank;
            bank = db.BankAccounts?.ToList();
            //using (FileStream fs = new FileStream("bankAccount.json", FileMode.Open))
            //{
            //    bankAccounts = JsonSerializer.Deserialize<List<BankAccount>>(fs);
            //}
            return bank;
        }
        [HttpPost("GetMoney")]
        public bool GetMoney(int id, decimal sum)
        {
            List<BankAccount>? bank;
            bank = db.BankAccounts?.ToList();
            //using (FileStream fs = new FileStream("bankAccount.json", FileMode.Open))
            //{
            //    bankAccounts = JsonSerializer.Deserialize<List<BankAccount>>(fs);
            //}
            BankAccount? tempaccount;
            tempaccount = bank.FirstOrDefault(ba => ba.Id == id);
            if (tempaccount != null)
            {
                if (tempaccount.Sum >= sum)
                {
                    tempaccount.Sum -= sum;
                    //using (FileStream fs = new FileStream("bankAccount.json", FileMode.Create))
                    //{
                    //    JsonSerializer.Serialize<List<BankAccount>>(fs, bankAccounts);
                    //}
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }

}
