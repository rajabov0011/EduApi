using EduApi.Application.Models.Requests.Common;
using EduApi.Application.Models.Requests.Subject;

namespace EduApi.Application.Interfaces
{
    public interface ISubjectService
    {
        Task<Response<ListResponse<SubjectResponse>>> GetAllSubjectsAsync(SubjectFilterRequest subjectFilterRequest);
        Task<Response<SubjectResponse>> GetSubjectByIdAsync(long id);
        Task<Response<SubjectResponse>> CreateSubjectAsync(CreateSubjectRequest createSubjectRequest);
        Task<Response<SubjectResponse>> UpdateSubjectAsync(UpdateSubjectRequest updateSubjectRequest);
        Task<Response> DeleteSubjectAsync(long id);
    }
}
