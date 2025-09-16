using EduApi.Application.Interfaces;
using EduApi.Application.Mappers;
using EduApi.Application.Models.Requests.City;
using EduApi.Application.Models.Requests.Common;
using EduApi.Infrastructure.Data;
using EduApi.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EduApi.Infrastructure.Services
{
    public class CityService(EduApiContext dbContext) : ICityService
    {
        public async Task<Response<CityResponse>> CreateCityAsync(CreateCityRequest createCityRequest)
        {
            if (string.IsNullOrWhiteSpace(createCityRequest.Name))
                return new ResponseError(HttpStatusCode.BadRequest, "Parameter 'Name' must be filled");

            var city = createCityRequest.ToEntity();

            dbContext.Cities.Add(city);
            
            await dbContext.SaveChangesAsync();

            return city.ToDto();
        }

        public async Task<Response<ListResponse<CityResponse>>> GetAllCitiesAsync(CityFilterRequest cityFilterRequest)
        {
            var citiesQuery = dbContext.Cities
                .Where(c => !c.IsDeleted);

            if (!string.IsNullOrWhiteSpace(cityFilterRequest.Name))
                citiesQuery = citiesQuery.Where(c => EF.Functions.ILike(c.Name, cityFilterRequest.Name));

            var totalCount = await citiesQuery.CountAsync();

            var pageNumber = cityFilterRequest.PageNumber;
            var pageSize = cityFilterRequest.PageSize;

            var cities = await citiesQuery
                .ToPaged(pageNumber, pageSize)
                .Select(c => c.ToDto())
                .ToListAsync();

            var result = new ListResponse<CityResponse>(totalCount, pageNumber, pageSize, cities);

            return result;
        }

        public async Task<Response<CityResponse>> GetCityByIdAsync(long id)
        {
            var city = await dbContext.Cities
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (city == null)
                return new ResponseError(HttpStatusCode.NotFound, "City not found");

            return city.ToDto();
        }

        public async Task<Response<CityResponse>> UpdateCityAsync(UpdateCityRequest updateCityRequest)
        {
            if (string.IsNullOrWhiteSpace(updateCityRequest.Name))
                return new ResponseError(HttpStatusCode.BadRequest, "Parameter 'Name' must be filled");

            var city = await dbContext.Cities
                .FirstOrDefaultAsync(c => c.Id == updateCityRequest.Id && !c.IsDeleted);

            if (city == null)
                return new ResponseError(HttpStatusCode.NotFound, "City not found");

            city.Name = updateCityRequest.Name;
            city.LastUpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return city.ToDto();
        }

        public async Task<Response> DeleteCityAsync(long id)
        {
            var city = await dbContext.Cities
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (city == null)
                return new ResponseError(HttpStatusCode.NotFound, "City not found");

            city.IsDeleted = true;
            city.LastUpdatedDate = DateTime.UtcNow;

            await dbContext.SaveChangesAsync();

            return new Response();
        }
    }
}
