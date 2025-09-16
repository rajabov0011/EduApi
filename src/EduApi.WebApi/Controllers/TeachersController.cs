using EduApi.Application.Interfaces;
using EduApi.Application.Models.Requests.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace EduApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController(ITeacherService teacherService) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateTeacherAsync([FromBody] CreateTeacherRequest createTeacherRequest)
        {
            var result = await teacherService.CreateTeacherAsync(createTeacherRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetTeachersByFilter")]
        public async Task<IActionResult> GetTeachersByFilter([FromBody] TeacherFilterRequest filter)
        {
            var result = await teacherService.GetAllTeachersAsync(filter);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetTopTeachersByStudentPerformance")]
        public async Task<IActionResult> GetTopTeachersByStudentPerformance([FromBody] TopTeachersByStudentPerformanceRequest topTeachersByStudentPerformanceRequest)
        {
            var result = await teacherService.GetTopTeachersByStudentPerformanceAsync(topTeachersByStudentPerformanceRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetTeacherByIdAsync(long id)
        {
            var result = await teacherService.GetTeacherByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeacherAsync([FromBody] UpdateTeacherRequest updateTeacherRequest)
        {
            var result = await teacherService.UpdateTeacherAsync(updateTeacherRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteTeacherAsync(long id)
        {
            var result = await teacherService.DeleteTeacherAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
