using Assignment4_LibraryManagementSystem.Models;

namespace Assignment4_LibraryManagementSystem.Interfaces
{
    public interface IBorrow
    {
        Task<(bool IsSuccess, string Message)> AddBorrow(Borrow borrow, int durationLoanBooks, int maxBookBorrowed);
        Task<IEnumerable<Borrow>> GetAllBorrows();
        Task<Borrow> GetBorrowById(int borrowId);
        Task<bool> ReturnBook(int borrowId);

    }
}
