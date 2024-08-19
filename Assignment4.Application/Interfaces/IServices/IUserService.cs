using Assignment4_LibraryManagementSystem.Models;

namespace Assignment4.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<bool> AddUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<bool> UpdateUser(int userId, User user);
        Task<bool> DeleteUser(int userId);
    }
}
