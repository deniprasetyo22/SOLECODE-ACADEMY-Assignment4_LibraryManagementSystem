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

        public async Task<bool> AddBorrow(Borrow borrow)
        {
            await _context.Borrows.AddAsync(borrow);
            await _context.SaveChangesAsync();
            return true;
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
    }
}
