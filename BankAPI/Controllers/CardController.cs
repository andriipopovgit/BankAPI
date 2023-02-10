using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private ApplicationContext db = new ApplicationContext();

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
    }
}
