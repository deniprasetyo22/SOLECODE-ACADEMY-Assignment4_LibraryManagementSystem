using Assignment4_LibraryManagementSystem.Interfaces;
using Assignment4_LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment4_LibraryManagementSystem.Services
{
    public class BookService : IBook
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<bool> AddBook(Book book)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(cek => cek.Isbn == book.Isbn || cek.Title == book.Title);
            if (existingBook != null)
            {
                return false;
            }
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetBookById(int bookId)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(cek => cek.Bookid == bookId);
            if (existingBook == null)
            {
                return null;
            }
            return await _context.Books.FindAsync(bookId);
        }

        public async Task<bool> UpdateBook(int bookId, Book book)
        {
            var existingBook = await _context.Books.FindAsync(bookId);
            if (existingBook == null)
            {
                return false;
            }

            var duplicateBook = await _context.Books.AnyAsync(b => b.Isbn == book.Isbn && b.Bookid != bookId);
            if (duplicateBook)
            {
                return false;
            }

            var duplicateTitle = await _context.Books.AnyAsync(b => b.Title == book.Title && b.Bookid != bookId);
            if (duplicateTitle)
            {
                return false;
            }

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Publicationyear = book.Publicationyear;
            existingBook.Isbn = book.Isbn;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBook(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
