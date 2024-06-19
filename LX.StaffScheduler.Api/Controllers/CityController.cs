using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LX.StaffScheduler.BLL;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.BLL.DTO;
using LX.StaffScheduler.Api.DependencyInjection;
using LX.StaffScheduler.Api.Models;

namespace LX.StaffScheduler.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _svc;

        public CityController(ICityService service)
        {
            _svc = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityModel>>> Get()
        {
            var result = await _svc.GetAll();
            var cities = result.FromDTO();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityModel>> GetCity(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
