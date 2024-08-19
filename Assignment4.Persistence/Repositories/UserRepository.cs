using Assignment4.Application.Interfaces.IRepositories;
using Assignment4_LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.Persistence.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly LibraryContext _context;
        public UserRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<bool> AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUserById(int userId)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(cek => cek.Userid == userId);
            if (existingUser == null)
            {
                return null;
            }
            return await _context.Users.FindAsync(userId);
        }
        public async Task<bool> UpdateUser(int userId, User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(cek => cek.Userid == userId);
            if (existingUser == null)
            {
                return false;
            }
            existingUser.Fname = user.Fname;
            existingUser.Lname = user.Lname;
            existingUser.Address = user.Address;
            existingUser.Dob = user.Dob;
            existingUser.Sex = user.Sex;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteUser(int userId)
        {
            var deleteUser = await _context.Users.FirstOrDefaultAsync(cek => cek.Userid == userId);
            if (deleteUser == null)
            {
                return false;
            }
            _context.Users.Remove(deleteUser);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
