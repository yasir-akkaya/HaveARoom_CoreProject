using HaveARoom_CoreProject.Areas.Admin.Interfaces;
using HaveARoom_CoreProject.Data;
using HaveARoom_CoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HaveARoom_CoreProject.Areas.Admin.Services
{
    public class HotelService : IHotelService
    {
        ApplicationDbContext db;
        public HotelService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Task<bool> AddHotelAsync(Hotel hotel)
        {
            bool result = false;
            if (hotel != null)
            {
                db.Hotels.AddAsync(hotel);
                db.SaveChanges();
                result = true;
            }
            return Task.FromResult(result);
        }

        public async Task<bool> DeleteHotelAsync(int id)
        {
            var hotel = await db.Hotels.FirstOrDefaultAsync(c => c.Id == id);
            bool result = false;
            if (hotel != null)
            {
                db.Remove(hotel);
                db.SaveChanges();
                result = true;
            }
            return result;
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            var list = await db.Hotels.ToListAsync();
            return list;
        }

        public Task<Hotel> GetHotelByIdAsync(int id)
        {
            var hotel = db.Hotels.FirstOrDefaultAsync(x => x.Id == id );
            
            return hotel;
        }

        public async Task<bool> UpdateHotelAsync(Hotel hotel)
        {
            var editedHotel = await db.Hotels.FirstOrDefaultAsync(x => x.Id == hotel.Id);
            bool result = false;
            if (editedHotel != null)
            {
                editedHotel.Name = hotel.Name;
                if (!String.IsNullOrEmpty(editedHotel.Image))
                {
                    editedHotel.Image = editedHotel.Image;
                }
                db.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}
