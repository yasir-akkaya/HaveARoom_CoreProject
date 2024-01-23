using HaveARoom_CoreProject.Areas.Admin.Interfaces;
using HaveARoom_CoreProject.Data;
using HaveARoom_CoreProject.Models;
using Microsoft.EntityFrameworkCore;

namespace HaveARoom_CoreProject.Areas.Admin.Services
{
    public class UserService : IUserService
    {
        ApplicationDbContext db;
        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public Task<bool> AddUserAsync(HaveARoom_CoreProject.Models.User user)
        {
            bool result = false;
            if (user != null)
            {
                db.Users.AddAsync(user);
                db.SaveChanges();
                result = true;
            }
            return Task.FromResult(result);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(c => c.Id == id);
            bool result = false;
            if (user != null)
            {
                db.Remove(user);
                db.SaveChanges();
                result = true;
            }
            return result;
        }

        public async Task<List<HaveARoom_CoreProject.Models.User>> GetAllUsersAsync()
        {
            var list = await db.Users.ToListAsync();
            return list;
        }

        public Task<HaveARoom_CoreProject.Models.User> GetUserByIdAsync(int id)
        {
            var user = db.Users.FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }
    }
}
