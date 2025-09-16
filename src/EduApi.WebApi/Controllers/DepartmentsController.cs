using EduApi.Application.Interfaces;
using EduApi.Application.Models.Requests.Department;
using Microsoft.AspNetCore.Mvc;

namespace EduApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController(IDepartmentService departmentService) : Controller
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateDepartmentAsync([FromBody] CreateDepartmentRequest createDepartmentRequest)
        {
            var result = await departmentService.CreateDepartmentAsync(createDepartmentRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetDepartmentsByFilter")]
        public async Task<IActionResult> GetAllDepartmentsAsync([FromBody]DepartmentFilterRequest filter)
        {
            var result = await departmentService.GetAllDepartmentsAsync(filter);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetDepartmentByIdAsync(long id)
        {
            var result = await departmentService.GetDepartmentByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDepartmentAsync([FromBody] UpdateDepartmentRequest updateDepartmentRequest)
        {
            var result = await departmentService.UpdateDepartmentAsync(updateDepartmentRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteDepartmentAsync(long id)
        {
            var result = await departmentService.DeleteDepartmentAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
