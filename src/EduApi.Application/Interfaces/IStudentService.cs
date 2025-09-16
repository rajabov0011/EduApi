using EduApi.Application.Models.Requests.Student;
using EduApi.Application.Models.Requests.Common;
using EduApi.Application.Models.Requests.Student;

namespace EduApi.Application.Interfaces
{
    public interface IStudentService
    {
        Task<Response<ListResponse<StudentResponse>>> GetAllStudentsAsync(StudentFilterRequest studentFilterRequest);
        Task<Response<List<StudentResponse>>> GetTopStudentsAsync(TopStudentsFilterRequest topStudentsFilterRequest);
        Task<Response<StudentResponse>> GetStudentByIdAsync(long id);
        Task<Response<StudentResponse>> CreateStudentAsync(CreateStudentRequest createStudentRequest);
        Task<Response<StudentResponse>> UpdateStudentAsync(UpdateStudentRequest updateStudentRequest);
        Task<Response> DeleteStudentAsync(long id);
    }
}
