using Assignment4_LibraryManagementSystem.Interfaces;
using Assignment4_LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment4_LibraryManagementSystem.Services
{
    public class BorrowService:IBorrow
    {
        private readonly LibraryContext _context;
        public BorrowService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<(bool IsSuccess, string Message)> AddBorrow(Borrow borrow, int durationLoanBooks, int maxBookBorrowed)
        {
            var existingBorrowedBook = await _context.Borrows
                .Where(a => a.Bookid == borrow.Bookid && a.Dateofreturn == null)
                .ToListAsync();
            if (existingBorrowedBook != null)
            {
                return (false, "The book is already borrowed.");
            }
            // Validasi jumlah buku yang dipinjam oleh pengguna
            var borrowedBooksCount = await _context.Borrows
                .Where(b => b.Userid == borrow.Userid && b.Dateofreturn == null)
                .CountAsync();
            if (borrowedBooksCount >= maxBookBorrowed)
            {
                return (false, "User has already borrowed the maximum number of books.");
            }
            borrow.Deadlinereturn = borrow.Dateofborrow.Value.AddDays(durationLoanBooks);
            await _context.Borrows.AddAsync(borrow);
            await _context.SaveChangesAsync();
            return (true, "Borrow added successfully.");
        }
        public async Task<IEnumerable<Borrow>> GetAllBorrows()
        {
            return await _context.Borrows.ToListAsync();
        }
        public async Task<Borrow> GetBorrowById(int borrowId)
        {
            var existingBorrow = await _context.Borrows.FirstOrDefaultAsync(cek => cek.Borrowid == borrowId);
            if (existingBorrow == null)
            {
                return null;
            }
            return await _context.Borrows.FindAsync(borrowId);
        }
        public async Task<bool> ReturnBook(int  borrowId)
        {
            var existingBorrow = await _context.Borrows.FirstOrDefaultAsync(cek => cek.Borrowid == borrowId);
            if (existingBorrow == null)
            {
                return false;
            }
            existingBorrow.Dateofreturn = DateOnly.FromDateTime(DateTime.UtcNow);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
