using Assignment4_LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.Application.Interfaces.IRepositories
{
    public interface IBorrowRepository
    {
        Task<(bool IsSuccess, string Message)> AddBorrow(Borrow borrow);
        Task<IEnumerable<Borrow>> GetAllBorrows();
        Task<Borrow> GetBorrowById(int borrowId);
        Task<bool> ReturnBook(int borrowId);
        Task<(bool IsSuccess, string Message)> UpdateBorrow(int borrowId, Borrow updatedBorrow);
        Task<(bool IsSuccess, string Message)> DeleteBorrow(int borrowId);
    }
}
