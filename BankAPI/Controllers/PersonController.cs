using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<Person> _logger;
        private List<Person> People { get; set; }

        public PersonController(ILogger<Person> logger)
        {
            _logger = logger;
            People = new List<Person>();
        }

        [HttpGet]
        public Person? Get(int? id)
        {
            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
            {
                People = JsonSerializer.Deserialize<List<Person>>(fs) ?? People;
            }
            _logger.LogInformation(People.Count.ToString());
            return People.FirstOrDefault(p => p.Id == id);
        }

        [HttpGet("GetAll")]
        public List<Person> GetAll()
        {
            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
            {
                People = JsonSerializer.Deserialize<List<Person>>(fs) ?? People;
            }
            _logger.LogInformation(People.Count.ToString());
            return People;
        }

        [HttpPost]
        public Person Post(string? firstname, string? lastname)
        {
            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
            {
                People = JsonSerializer.Deserialize<List<Person>>(fs) ?? People;
            }
            People.Add(new Person { Id = People.Count + 1, Firstname = firstname, Lastname = lastname });
            _logger.LogInformation(People.Count.ToString());
            using (FileStream fs = new FileStream("people.json", FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize<List<Person>>(fs, People);
            }
            return People.Last();
        }
    }
}
