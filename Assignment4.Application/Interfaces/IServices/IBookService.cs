using Assignment4_LibraryManagementSystem.Models;

namespace Assignment4.Application.Interfaces.IServices
{
    public interface IBookService
    {
        Task<bool> AddBook(Book book);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book?> GetBookById(int bookId);
        Task<bool> UpdateBook(int bookId, Book book);
        Task<bool> DeleteBook(int bookId);
    }
}
