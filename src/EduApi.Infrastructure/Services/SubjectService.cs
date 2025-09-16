using EduApi.Application.Interfaces;
using EduApi.Application.Mappers;
using EduApi.Application.Models.Requests.Common;
using EduApi.Application.Models.Requests.Subject;
using EduApi.Infrastructure.Data;
using EduApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EduApi.Infrastructure.Services
{
    public class SubjectService(EduApiContext dbContext) : ISubjectService
    {
        public async Task<Response<SubjectResponse>> CreateSubjectAsync(CreateSubjectRequest createSubjectRequest)
        {
            if (string.IsNullOrWhiteSpace(createSubjectRequest.Name))
                return new ResponseError(HttpStatusCode.BadRequest, "Parameter 'Name' must be filled");

            var subject = createSubjectRequest.ToEntity();

            dbContext.Subjects.Add(subject);
            
            await dbContext.SaveChangesAsync();

            return subject.ToDto();
        }

        public async Task<Response<ListResponse<SubjectResponse>>> GetAllSubjectsAsync(SubjectFilterRequest subjectFilterRequest)
        {
            var subjectsQuery = dbContext.Subjects
            .Where(s => !s.IsDeleted);

            if (!string.IsNullOrWhiteSpace(subjectFilterRequest.Name))
                subjectsQuery = subjectsQuery.Where(c => EF.Functions.ILike(c.Name, subjectFilterRequest.Name));

            var totalCount = await subjectsQuery.CountAsync();

            var pageNumber = subjectFilterRequest.PageNumber;
            var pageSize = subjectFilterRequest.PageSize;

            var subjects = await subjectsQuery
                .ToPaged(pageNumber,pageSize)
                .Select(s => s.ToDto())
                .ToListAsync();

            var result = new ListResponse<SubjectResponse>(totalCount, pageNumber, pageSize, subjects);

            return result;
        }

        public async Task<Response<SubjectResponse>> GetSubjectByIdAsync(long id)
        {
            var subject = await dbContext.Subjects
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (subject == null)
                return new ResponseError(HttpStatusCode.NotFound, "Subject not found");

            return subject.ToDto();
        }

        public async Task<Response<SubjectResponse>> UpdateSubjectAsync(UpdateSubjectRequest updateSubjectRequest)
        {
            if (string.IsNullOrWhiteSpace(updateSubjectRequest.Name))
                return new ResponseError(HttpStatusCode.BadRequest, "Parameter 'Name' must be filled");

            var subject = await dbContext.Subjects
                .FirstOrDefaultAsync(s => s.Id == updateSubjectRequest.Id && !s.IsDeleted);

            if (subject == null)
                return new ResponseError(HttpStatusCode.NotFound, "Subject not found");

            subject.Name = updateSubjectRequest.Name;
            subject.LastUpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return subject.ToDto();
        }

        public async Task<Response> DeleteSubjectAsync(long id)
        {
            var subject = await dbContext.Subjects
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);

            if (subject == null)
                return new ResponseError(HttpStatusCode.NotFound, "Subject not found");

            subject.IsDeleted = true;
            subject.LastUpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
