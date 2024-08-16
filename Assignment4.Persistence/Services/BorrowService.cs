using Assignment4_LibraryManagementSystem.Interfaces;
using Assignment4_LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Assignment4_LibraryManagementSystem.Services
{
    public class BorrowService : IBorrow
    {
        private readonly LibraryContext _context;
        private readonly IConfiguration _configuration;

        public BorrowService(LibraryContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<(bool IsSuccess, string Message)> AddBorrow(Borrow borrow)
        {
            // Maximum books allowed to be borrowed by a user
            var maxBookBorrowed = int.Parse(_configuration["MySetting:MaxBookBorrowed"]);

            // Check if the borrow object is null
            if (borrow == null)
            {
                return (false, "Invalid borrow data.");
            }

            // Check if the book is already borrowed and not returned
            var existingBorrowedBook = await _context.Borrows
                .Where(b => b.Bookid == borrow.Bookid && b.Dateofreturn == null)
                .FirstOrDefaultAsync();

            if (existingBorrowedBook != null)
            {
                return (false, "The book is already borrowed and not returned yet.");
            }

            // Validate the number of books currently borrowed by the user
            var borrowedBooksCount = await _context.Borrows
                .Where(b => b.Userid == borrow.Userid && b.Dateofreturn == null)
                .CountAsync();

            if (borrowedBooksCount >= maxBookBorrowed)
            {
                return (false, $"User has already borrowed the maximum number of books ({maxBookBorrowed}).");
            }

            // Ensure the DateOfBorrow is provided
            if (!borrow.Dateofborrow.HasValue)
            {
                return (false, "The borrow date must be provided.");
            }

            // Calculate and set the deadline for returning the book
            int durationLoanBooks = int.Parse(_configuration["MySetting:DurationBookLoans"]);
            borrow.Deadlinereturn = borrow.Dateofborrow.Value.AddDays(durationLoanBooks);

            // Add the borrow record to the database
            await _context.Borrows.AddAsync(borrow);
            await _context.SaveChangesAsync();

            return (true, "Borrow added successfully.");
        }

        public async Task<IEnumerable<Borrow>> GetAllBorrows()
        {
            return await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task<Borrow> GetBorrowById(int borrowId)
        {
            var borrow = await _context.Borrows
                .Include(b => b.Book)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Borrowid == borrowId);

            if (borrow == null)
            {
                return null;
            }

            return borrow;
        }

        public async Task<bool> ReturnBook(int borrowId)
        {
            var existingBorrow = await _context.Borrows.FirstOrDefaultAsync(b => b.Borrowid == borrowId);
            if (existingBorrow == null)
            {
                return false;
            }

            existingBorrow.Dateofreturn = DateOnly.FromDateTime(DateTime.UtcNow);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(bool IsSuccess, string Message)> UpdateBorrow(int borrowId, Borrow updatedBorrow)
        {
            var existingBorrow = await _context.Borrows.FirstOrDefaultAsync(b => b.Borrowid == borrowId);
            if (existingBorrow == null)
            {
                return (false, "Borrow record not found.");
            }

            // Validate that the updated borrow record meets the requirements
            var maxBookBorrowed = int.Parse(_configuration["MySetting:MaxBookBorrowed"]);
            var borrowedBooksCount = await _context.Borrows
                .Where(b => b.Userid == updatedBorrow.Userid && b.Dateofreturn == null && b.Borrowid != borrowId)
                .CountAsync();

            if (borrowedBooksCount >= maxBookBorrowed)
            {
                return (false, "User has already borrowed the maximum number of books.");
            }

            // Update borrow details
            existingBorrow.Userid = updatedBorrow.Userid;
            existingBorrow.Bookid = updatedBorrow.Bookid;
            existingBorrow.Dateofborrow = updatedBorrow.Dateofborrow;
            existingBorrow.Deadlinereturn = updatedBorrow.Dateofborrow?.AddDays(int.Parse(_configuration["MySetting:DurationBookLoans"]));
            existingBorrow.Dateofreturn = updatedBorrow.Dateofreturn;

            await _context.SaveChangesAsync();
            return (true, "Borrow record updated successfully.");
        }

        public async Task<(bool IsSuccess, string Message)> DeleteBorrow(int borrowId)
        {
            var existingBorrow = await _context.Borrows.FirstOrDefaultAsync(b => b.Borrowid == borrowId);
            if (existingBorrow == null)
            {
                return (false, "Borrow record not found.");
            }

            _context.Borrows.Remove(existingBorrow);
            await _context.SaveChangesAsync();
            return (true, "Borrow record deleted successfully.");
        }
    }
}
