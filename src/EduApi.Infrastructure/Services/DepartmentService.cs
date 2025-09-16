using EduApi.Application.Interfaces;
using EduApi.Application.Mappers;
using EduApi.Application.Models.Requests.Common;
using EduApi.Application.Models.Requests.Department;
using EduApi.Infrastructure.Data;
using EduApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EduApi.Infrastructure.Services
{
    public class DepartmentService(EduApiContext dbContext) : IDepartmentService
    {
        public async Task<Response<DepartmentResponse>> CreateDepartmentAsync(CreateDepartmentRequest createDepartmentRequest)
        {
            if (string.IsNullOrWhiteSpace(createDepartmentRequest.Name))
                return new ResponseError(HttpStatusCode.BadRequest, "Parameter 'Name' must be filled");

            var department = createDepartmentRequest.ToEntity();

            dbContext.Departments.Add(department);
            
            await dbContext.SaveChangesAsync();

            return department.ToDto();
        }

        public async Task<Response<ListResponse<DepartmentResponse>>> GetAllDepartmentsAsync(DepartmentFilterRequest departmentFilterRequest)
        {
            var departmentsQuery = dbContext.Departments
                .Where(d => !d.IsDeleted);

            if (!string.IsNullOrWhiteSpace(departmentFilterRequest.Name))
                departmentsQuery = departmentsQuery.Where(d => EF.Functions.ILike(d.Name, departmentFilterRequest.Name));

            var totalCount = await departmentsQuery.CountAsync();

            var pageNumber = departmentFilterRequest.PageNumber;
            var pageSize = departmentFilterRequest.PageSize;

            var departments = await departmentsQuery
                .ToPaged(pageNumber, pageSize)
                .Select(d => d.ToDto())
                .ToListAsync();

            var result = new ListResponse<DepartmentResponse>(totalCount, pageNumber, pageSize, departments);

            return result;
        }

        public async Task<Response<DepartmentResponse>> GetDepartmentByIdAsync(long id)
        {
            var department = await dbContext.Departments
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);

            if (department == null)
                return new ResponseError(HttpStatusCode.NotFound, "Department not found");

            return department.ToDto();
        }

        public async Task<Response<DepartmentResponse>> UpdateDepartmentAsync(UpdateDepartmentRequest updateDepartmentRequest)
        {
            if (string.IsNullOrWhiteSpace(updateDepartmentRequest.Name))
                return new ResponseError(HttpStatusCode.BadRequest, "Parameter 'Name' must be filled");

            var department = await dbContext.Departments
                .FirstOrDefaultAsync(d => d.Id == updateDepartmentRequest.Id && !d.IsDeleted);

            if (department == null)
                return new ResponseError(HttpStatusCode.BadRequest, "Department not found");


            department.Name = updateDepartmentRequest.Name;
            department.LastUpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return department.ToDto();
        }

        public async Task<Response> DeleteDepartmentAsync(long id)
        {
            var department = await dbContext.Departments
                .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted);

            if (department == null)
                return new ResponseError(HttpStatusCode.BadRequest, "Department not found");

            department.IsDeleted = true;
            department.LastUpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
