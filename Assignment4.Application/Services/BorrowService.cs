using Assignment4.Application.Interfaces.IRepositories;
using Assignment4.Application.Interfaces.IServices;
using Assignment4_LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Assignment4_LibraryManagementSystem.Services
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _borrowRepository;
        public BorrowService(IBorrowRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }
        public async Task<(bool IsSuccess, string Message)> AddBorrow(Borrow borrow)
        {
            return await _borrowRepository.AddBorrow(borrow);
        }
        public async Task<IEnumerable<Borrow>> GetAllBorrows()
        {
            return await _borrowRepository.GetAllBorrows();
        }
        public async Task<Borrow> GetBorrowById(int borrowId)
        {
            return await _borrowRepository.GetBorrowById(borrowId);
        }
        public async Task<bool> ReturnBook(int borrowId)
        {
            return await _borrowRepository.ReturnBook(borrowId);
        }
        public async Task<(bool IsSuccess, string Message)> UpdateBorrow(int borrowId, Borrow updatedBorrow)
        {
            return await _borrowRepository.UpdateBorrow(borrowId, updatedBorrow);
        }
        public async Task<(bool IsSuccess, string Message)> DeleteBorrow(int borrowId)
        {
            return await _borrowRepository.DeleteBorrow(borrowId);
        }
    }
}
