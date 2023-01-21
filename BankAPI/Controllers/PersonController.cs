using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace BankAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<Person> _logger;

        public PersonController(ILogger<Person> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Person? Get(int? id)
        {
            List<Person>? people;
            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
            {
                people = JsonSerializer.Deserialize<List<Person>>(fs) ?? new List<Person>();
            }
            return people.FirstOrDefault(p => p.Id == id);
        }

        [HttpGet("GetAll")]
        public List<Person> GetAll()
        {
            List<Person>? people;
            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
            {
                people = JsonSerializer.Deserialize<List<Person>>(fs) ?? new List<Person>();
            }
            return people;
        }

        [HttpPost]
        public Person Post(string? firstname, string? lastname)
        {
            List<Person>? people;
            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
            {
                people = JsonSerializer.Deserialize<List<Person>>(fs) ?? new List<Person>();
            }
            people.Add(new Person { Id = Utils.GetRandomId(people), Firstname = firstname, Lastname = lastname });
            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, people);
            }
            return people.Last();
        }
    }
}
