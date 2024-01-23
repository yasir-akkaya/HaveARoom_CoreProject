using HaveARoom_CoreProject.Models;

namespace HaveARoom_CoreProject.Areas.Admin.Interfaces
{
    public interface IHotelService
    {
        public Task<bool> AddHotelAsync(Hotel hotel);
        public Task<List<Hotel>> GetAllHotelsAsync();
        public Task<Hotel> GetHotelByIdAsync(int id);
        public Task<bool> UpdateHotelAsync(Hotel hotel);
        public Task<bool> DeleteHotelAsync(int id);
    }
}
