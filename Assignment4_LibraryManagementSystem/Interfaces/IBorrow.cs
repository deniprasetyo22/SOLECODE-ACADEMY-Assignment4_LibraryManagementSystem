using Assignment4_LibraryManagementSystem.Models;

namespace Assignment4_LibraryManagementSystem.Interfaces
{
    public interface IBorrow
    {
        Task<bool> AddBorrow(Borrow borrow);
        Task<IEnumerable<Borrow>> GetAllBorrows();
        Task<Borrow> GetBorrowById(int borrowId);

    }
}
