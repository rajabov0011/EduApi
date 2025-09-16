using EduApi.Application.Interfaces;
using EduApi.Application.Models.Requests.Subject;
using Microsoft.AspNetCore.Mvc;

namespace EduApi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController(ISubjectService subjectService) : Controller
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateSubjectAsync([FromBody] CreateSubjectRequest createSubjectRequest)
        {
            var result = await subjectService.CreateSubjectAsync(createSubjectRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("GetSubjectsByFilter")]
        public async Task<IActionResult> GetAllSubjectsAsync([FromBody] SubjectFilterRequest filter)
        {
            var result = await subjectService.GetAllSubjectsAsync(filter);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetSubjectByIdAsync(long id)
        {
            var result = await subjectService.GetSubjectByIdAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubjectAsync([FromBody] UpdateSubjectRequest updateSubjectRequest)
        {
            var result = await subjectService.UpdateSubjectAsync(updateSubjectRequest);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteSubjectAsync(long id)
        {
            var result = await subjectService.DeleteSubjectAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
