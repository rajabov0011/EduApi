using EduApi.Application.Models.Requests.Common;
using EduApi.Application.Models.Requests.Department;

namespace EduApi.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<Response<ListResponse<DepartmentResponse>>> GetAllDepartmentsAsync(DepartmentFilterRequest departmentFilterRequest);
        Task<Response<DepartmentResponse>> GetDepartmentByIdAsync(long id);
        Task<Response<DepartmentResponse>> CreateDepartmentAsync(CreateDepartmentRequest createDepartmentRequest);
        Task<Response<DepartmentResponse>> UpdateDepartmentAsync(UpdateDepartmentRequest updateDepartmentRequest);
        Task<Response> DeleteDepartmentAsync(long id);
    }
}
