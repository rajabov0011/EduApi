using EduApi.Application.Models.Requests.City;
using EduApi.Application.Models.Requests.City;
using EduApi.Application.Models.Requests.Common;

namespace EduApi.Application.Interfaces
{
    public interface ICityService
    {
        Task<Response<ListResponse<CityResponse>>> GetAllCitiesAsync(CityFilterRequest request);
        Task<Response<CityResponse>> GetCityByIdAsync(long id);
        Task<Response<CityResponse>> CreateCityAsync(CreateCityRequest cityDto);
        Task<Response<CityResponse>> UpdateCityAsync(UpdateCityRequest cityDto);
        Task<Response> DeleteCityAsync(long id);
    }
}
