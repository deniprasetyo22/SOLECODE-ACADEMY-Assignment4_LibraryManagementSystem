using Assignment4_LibraryManagementSystem.Models;

namespace Assignment4_LibraryManagementSystem.Interfaces
{
    public interface IUser
    {
        Task<bool> AddUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<bool> UpdateUser(int userId, User user);
        Task<bool> DeleteUser(int userId);
    }
}
