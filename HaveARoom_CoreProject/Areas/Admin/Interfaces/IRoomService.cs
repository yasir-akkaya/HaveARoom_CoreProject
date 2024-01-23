using HaveARoom_CoreProject.Models;

namespace HaveARoom_CoreProject.Areas.Admin.Interfaces
{
    public interface IRoomService
    {
        public Task<bool> AddRoomAsync(Room room);
        public Task<List<Room>> GetAllRoomsAsync();
        public Task<Room> GetRoomByIdAsync(int id);
        public Task<bool> DeleteRoomAsync(int id);
        public Task<bool> UpdateRoomAsync(Room room);

    }
}
