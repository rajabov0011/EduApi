using EduApi.Application.Models.Requests.City;
using EduApi.Domain.Entities;

namespace EduApi.Application.Mappers
{
    public static class CityMapper
    {
        public static CityResponse ToDto(this City city)
        {
            return new CityResponse
            {
                Id = city.Id,
                Name = city.Name
            };
        }

        public static City ToEntity(this CreateCityRequest dto)
        {
            return new City
            {
                Name = dto.Name ?? string.Empty
            };
        }
    }
}
