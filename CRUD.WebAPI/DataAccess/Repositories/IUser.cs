using CRUD.WebAPI.Data;

namespace CRUD.WebAPI.DataAccess.Repositories
{
    public interface IUser
    {
        Task<List<User>> GetAll();
        Task<User> GetByUser(int id);
        Task<UserLogin> GetByUsersAuthenticate(string username, string password);
        Task<bool> SaveChangesAsync();
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
