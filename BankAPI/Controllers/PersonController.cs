using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<Person> _logger;
        private ApplicationContext db = new ApplicationContext();
        public PersonController(ILogger<Person> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Person? Get(int? id)
        {
            List<Person>? people;
            people = db.People?.ToList();
            return people.FirstOrDefault(p => p.Id == id);
        }

        [HttpGet("GetAll")]
        public List<Person> GetAll()
        {
            List<Person>? people;
            people = db.People?.ToList();
            return people;
        }

        [HttpPost]
        public Person Post(string? firstname, string? lastname)
        {
            List<Person>? people = db.People?.ToList();
            Person person = new Person{Firstname = firstname, Lastname = lastname };
            db.People?.Add(person);
            db.SaveChanges();
            return person;
        }

    }
}
