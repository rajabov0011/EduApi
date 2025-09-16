using EduApi.Application.Interfaces;
using EduApi.Application.Models.Requests.Student;
using Microsoft.AspNetCore.Mvc;

namespace EduApi.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(IStudentService studentService) : Controller
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateStudentAsync([FromBody] CreateStudentRequest createStudentRequest)
        {
            var result = await studentService.CreateStudentAsync(createStudentRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetStudentsByFilter")]
        public async Task<IActionResult> GetStudentsByFilter([FromBody] StudentFilterRequest filter)
        {
            var result = await studentService.GetAllStudentsAsync(filter);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetTopStudents")]
        public async Task<IActionResult> GetTopStudents([FromBody] TopStudentsFilterRequest topStudentsFilterRequest)
        {
            var result = await studentService.GetTopStudentsAsync(topStudentsFilterRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetStudentByIdAsync(long id)
        {
            var result = await studentService.GetStudentByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudentAsync([FromBody] UpdateStudentRequest updateStudentRequest)
        {
            var result = await studentService.UpdateStudentAsync(updateStudentRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudentAsync(long id)
        {
            var result = await studentService.DeleteStudentAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
