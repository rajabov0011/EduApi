using EduApi.Application.Models.Requests.Teacher;
using EduApi.Application.Models.Requests.Common;
using EduApi.Application.Models.Requests.Teacher;

namespace EduApi.Application.Interfaces
{
    public interface ITeacherService
    {
        Task<Response<ListResponse<TeacherResponse>>> GetAllTeachersAsync(TeacherFilterRequest filter);
        Task<Response<List<TeacherResponse>>> GetTopTeachersByStudentPerformanceAsync(TopTeachersByStudentPerformanceRequest request);
        Task<Response<TeacherResponse>> GetTeacherByIdAsync(long id);
        Task<Response<TeacherResponse>> CreateTeacherAsync(CreateTeacherRequest createTeacherDto);
        Task<Response<TeacherResponse>> UpdateTeacherAsync(UpdateTeacherRequest updateTeacherDto);
        Task<Response> DeleteTeacherAsync(long id);
    }
}
