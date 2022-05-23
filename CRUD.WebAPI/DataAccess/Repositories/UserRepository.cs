using CRUD.WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace CRUD.WebAPI.DataAccess.Repositories
{
    public class UserRepository : IUser
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User> GetByUser(int id)
        {
            return await _context.Users.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserLogin> GetByUsersAuthenticate(string username, string password)
        {
            return await _context.UsersAuthenticate.AsNoTracking()
                    .Where(x => x.UserName.ToLower() == username.ToLower() && x.Password == password).FirstOrDefaultAsync();  
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
