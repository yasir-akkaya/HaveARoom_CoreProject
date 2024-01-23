using HaveARoom_CoreProject.Models;

namespace HaveARoom_CoreProject.Areas.Admin.Interfaces
{
    public interface IUserService
    {
        public Task<bool> AddUserAsync(HaveARoom_CoreProject.Models.User user);
        public Task<List<HaveARoom_CoreProject.Models.User>> GetAllUsersAsync();
        public Task<HaveARoom_CoreProject.Models.User> GetUserByIdAsync(int id);
        public Task<bool> DeleteUserAsync(int id);
    }
}
