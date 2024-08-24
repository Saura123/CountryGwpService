using CountryGwpService.DataLayer;
using CountryGwpService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CountryGwpService.Controllers
{
    [Route("server/api/gwp")]
    [ApiController]
    public class CountryGwpController : ControllerBase
    {
        private readonly IGwpRepository _gwpRepository;

        public CountryGwpController(IGwpRepository gwpRepository)
        {
            _gwpRepository = gwpRepository;
        }


        [HttpPost("avg")]
        public async Task<IActionResult> GetAverageGwp([FromBody] GwpRequest request)
        {
            if(!ModelState.IsValid)
                return BadRequest("Invalid request");

            var result = await _gwpRepository.GetAverageGwpAsync(request.Country, request.Lob);
            return Ok(result);
        }
    }
}
