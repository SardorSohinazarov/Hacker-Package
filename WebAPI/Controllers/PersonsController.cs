using JsonDB.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PersonsController : ControllerBase
    {
        private readonly EntityService entityService;

        public PersonsController(EntityService entityService)
        {
            this.entityService = entityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(this.entityService.Get<Person>());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            return Ok(this.entityService.Add(person));
        }
    }
}
