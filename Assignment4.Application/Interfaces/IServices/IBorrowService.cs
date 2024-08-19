using Assignment4_LibraryManagementSystem.Models;

namespace Assignment4.Application.Interfaces.IServices
{
    public interface IBorrowService
    {
        Task<(bool IsSuccess, string Message)> AddBorrow(Borrow borrow);
        Task<IEnumerable<Borrow>> GetAllBorrows();
        Task<Borrow> GetBorrowById(int borrowId);
        Task<bool> ReturnBook(int borrowId);
        Task<(bool IsSuccess, string Message)> UpdateBorrow(int borrowId, Borrow updatedBorrow);
        Task<(bool IsSuccess, string Message)> DeleteBorrow(int borrowId);
    }
}
