using HaveARoom_CoreProject.Models;

namespace HaveARoom_CoreProject.Areas.Admin.Interfaces
{
    public interface ICityService
    {
        public Task<bool> AddCityAsync(City city);
        public Task<List<City>> GetAllCitiesAsync();
        public Task<City> GetCityByIdAsync(int id);
    }
}
