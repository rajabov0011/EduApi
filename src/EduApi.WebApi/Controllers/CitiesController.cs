using EduApi.Application.Interfaces;
using EduApi.Application.Models.Requests.City;
using Microsoft.AspNetCore.Mvc;

namespace EduApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController(ICityService cityService) : Controller
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCityAsync([FromBody] CreateCityRequest createCityRequest)
        {
            var result = await cityService.CreateCityAsync(createCityRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetCitiesByFilter")]
        public async Task<IActionResult> GetAllCitiesAsync([FromBody] CityFilterRequest filter)
        {
            var result = await cityService.GetAllCitiesAsync(filter);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetCityByIdAsync(long id)
        {
            var result = await cityService.GetCityByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCityAsync([FromBody] UpdateCityRequest updateCityRequest)
        {
            var result = await cityService.UpdateCityAsync(updateCityRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteCityAsync(long id)
        {
            var result = await cityService.DeleteCityAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
