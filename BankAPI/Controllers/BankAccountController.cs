using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {
        private ApplicationContext db = new ApplicationContext();
        [HttpGet]
        public BankAccount? Get(int? id)
        {
            return db.BankAccounts?.FirstOrDefault(p => p.Id == id);
        }
        [HttpPost]
        public BankAccount? Post(int personId)
        {

            List<Person>? people;
            people = db.People?.ToList();
            if (!people.Any(ba => ba.Id == personId))
            {
                return null;
            }
            List<BankAccount>? banks;
            banks = db.BankAccounts?.ToList();
            BankAccount bank = new BankAccount { Person = db.People.FirstOrDefault(p => personId == p.Id), Sum = 0, Number = Utils.GetRandomCardNumber() };
            db.BankAccounts.Add(bank);
            db.SaveChanges();
            return bank;
        }
        [HttpPost("PutMoney")]
        public BankAccount? PutMoney(int id, decimal sum)
        {
            List<BankAccount>? bank;
            bank = db.BankAccounts?.ToList();
            BankAccount? bankAcc = bank.FirstOrDefault(ba => ba.Id == id);

            if (bankAcc != null)
            {
                bankAcc.Sum += sum;
            }
            db.SaveChanges();
            return bankAcc;
        }
        [HttpGet("GetAll")]
        public List<BankAccount>? GetAll()
        {
            List<BankAccount>? bank;
            bank = db.BankAccounts?.ToList();
            return bank;
        }
        [HttpPost("GetMoney")]
        public bool GetMoney(int id, decimal sum)
        {
            List<BankAccount>? bank;
            bank = db.BankAccounts?.ToList();
            BankAccount? tempaccount;
            tempaccount = bank?.FirstOrDefault(ba => ba.Id == id);
            if (tempaccount != null)
            {
                if (tempaccount.Sum >= sum)
                {
                    tempaccount.Sum -= sum;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        [HttpGet("GetAllAccountsByPersonId")]
        public List<BankAccount>? GetAllAccountsByPersonId(int personId)
        {
            List<BankAccount>? bank;
            bank = db.BankAccounts?.ToList();
            return bank?.Where(ba => ba.Person.Id == personId).ToList();
        }
        [HttpPost("Delete")]

        public bool Delete(int bankId)
        {
            List<BankAccount>? bank;
            bank = db.BankAccounts?.ToList();
            BankAccount? todelete = bank?.FirstOrDefault(ba => ba.Id == bankId);
            if (todelete != null)
            {
                db.BankAccounts?.Remove(todelete);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
