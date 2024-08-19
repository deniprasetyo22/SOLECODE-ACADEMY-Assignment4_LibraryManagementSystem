using Assignment4.Application.Interfaces.IRepositories;
using Assignment4.Application.Interfaces.IServices;
using Assignment4_LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Assignment4_LibraryManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> AddUser(User user)
        {
            return await _userRepository.AddUser(user);
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }
        public async Task<User> GetUserById(int userId)
        {
            return await _userRepository.GetUserById(userId);
        }
        public async Task<bool> UpdateUser(int userId, User user)
        {
            return await _userRepository.UpdateUser(userId, user);
        }
        public async Task<bool> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUser(userId);
        }
    }
}
