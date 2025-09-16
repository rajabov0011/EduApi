using EduApi.Application.Models.Requests.Department;
using EduApi.Domain.Entities;

namespace EduApi.Application.Mappers
{
    public static class DepartmentMapper
    {
        public static DepartmentResponse ToDto(this Department department)
        {
            return new DepartmentResponse
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public static Department ToEntity(this CreateDepartmentRequest createDepartmentRequest)
        {
            return new Department
            {
                Name = createDepartmentRequest.Name ?? string.Empty
            };
        }
    }
}
