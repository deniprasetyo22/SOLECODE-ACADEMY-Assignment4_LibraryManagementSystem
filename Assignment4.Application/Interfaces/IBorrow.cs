using Assignment4_LibraryManagementSystem.Models;

namespace Assignment4_LibraryManagementSystem.Interfaces
{
    public interface IBorrow
    {
        Task<(bool IsSuccess, string Message)> AddBorrow(Borrow borrow);
        Task<IEnumerable<Borrow>> GetAllBorrows();
        Task<Borrow> GetBorrowById(int borrowId);
        Task<bool> ReturnBook(int borrowId);
        Task<(bool IsSuccess, string Message)> UpdateBorrow(int borrowId, Borrow updatedBorrow);
        Task<(bool IsSuccess, string Message)> DeleteBorrow(int borrowId);
    }
}
