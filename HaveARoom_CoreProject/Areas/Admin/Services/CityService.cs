using HaveARoom_CoreProject.Areas.Admin.Interfaces;
using HaveARoom_CoreProject.Data;
using HaveARoom_CoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HaveARoom_CoreProject.Areas.Admin.Services
{
    public class CityService : ICityService
    {
        ApplicationDbContext db;

        public CityService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Task<bool> AddCityAsync(City city)
        {
            bool result = false;
            if (city != null)
            {
                db.Cities.AddAsync(city);
                db.SaveChanges();
                result = true;
            }
            return Task.FromResult(result);
        }

        public async Task<List<City>> GetAllCitiesAsync()
        {
            var list = await db.Cities.ToListAsync();
            return list;
        }

        public Task<City> GetCityByIdAsync(int id)
        {
            var city = db.Cities.FirstOrDefaultAsync(x => x.Id == id );
            return city;
        }

    }
}
