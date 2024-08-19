using Assignment4_LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.Application.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<bool> AddUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int userId);
        Task<bool> UpdateUser(int userId, User user);
        Task<bool> DeleteUser(int userId);
    }
}
