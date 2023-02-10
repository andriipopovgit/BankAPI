using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private ApplicationContext db = new ApplicationContext();
        [HttpGet]
        public List<Currency>? Get()
        {
            return db.Currency?.ToList();
        }

        [HttpGet("{id}")]
        public Currency? Get(int id)
        {
            return db.Currency?.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public void Post(char? symbol, decimal? exchangeRate, string? name, string? code)
        {
            db.Currency?.Add(new Currency { Symbol = symbol, ExchangeRate = exchangeRate, Name = name, Code = code });
            db.SaveChanges();
        }
    }
}
