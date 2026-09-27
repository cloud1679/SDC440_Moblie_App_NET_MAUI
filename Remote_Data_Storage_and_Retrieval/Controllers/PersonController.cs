using Remote_Data_Storage_and_Retrieval.Models;
using Remote_Data_Storage_and_Retrieval.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Remote_Data_Storage_and_Retrieval.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        // GET: api/Person
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return new PersonData().GetPeople();
        }

        // POST: api/Person
        [HttpPost]
        public void Post([FromBody] Person person)
        {
            var pd = new PersonData();
            pd.SavePerson(person);
        }
    }
}