using Assignment4_LibraryManagementSystem.Models;

namespace Assignment4_LibraryManagementSystem.Interfaces
{
    public interface IBook
    {
        Task<bool> AddBook(Book book);
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book?> GetBookById(int bookId);
        Task<bool> UpdateBook(int bookId, Book book);
        Task<bool> DeleteBook(int bookId);
    }
}
