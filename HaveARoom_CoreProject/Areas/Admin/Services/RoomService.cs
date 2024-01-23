using HaveARoom_CoreProject.Areas.Admin.Interfaces;
using HaveARoom_CoreProject.Data;
using HaveARoom_CoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HaveARoom_CoreProject.Areas.Admin.Services
{
    public class RoomService : IRoomService
    {
        ApplicationDbContext db;
        public RoomService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Task<bool> AddRoomAsync(Room room)
        {
            bool result = false;
            if (room != null)
            {
                db.Rooms.AddAsync(room);
                db.SaveChanges();
                result = true;
            }
            return Task.FromResult(result);
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var Room = await db.Rooms.FirstOrDefaultAsync(c => c.Id == id);
            bool result = false;
            if (Room != null)
            {
                db.Remove(Room);
                db.SaveChanges();
                result = true;
            }
            return result;
        }

        public async Task<List<Room>> GetAllRoomsAsync()
        {
            var list = await db.Rooms.ToListAsync();
            return list;
        }

        public Task<Room> GetRoomByIdAsync(int id)
        {
            var room = db.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            return room;
        }

        public async Task<bool> UpdateRoomAsync(Room room)
        {
            var editedRoom = await db.Rooms.FirstOrDefaultAsync(x => x.Id == room.Id);
            bool result = false;
            if (editedRoom != null)
            {
                editedRoom.LuxuryLevel = room.LuxuryLevel;
                editedRoom.Price = room.Price;
                editedRoom.Status= room.Status;
                if (!String.IsNullOrEmpty(editedRoom.Image))
                {
                    editedRoom.Image = editedRoom.Image;
                }
                db.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}
