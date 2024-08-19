using Assignment4.Application.Interfaces.IRepositories;
using Assignment4.Application.Interfaces.IServices;
using Assignment4_LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment4_LibraryManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<bool> AddBook(Book book)
        {
            return await _bookRepository.AddBook(book);
        }
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _bookRepository.GetAllBooks();
        }
        public async Task<Book?> GetBookById(int bookId)
        {
            return await _bookRepository.GetBookById(bookId);
        }
        public async Task<bool> UpdateBook(int bookId, Book book)
        {
            return await _bookRepository.UpdateBook(bookId, book);
        }
        public async Task<bool> DeleteBook(int bookId)
        {
            return await _bookRepository.DeleteBook(bookId);
        }
    }
}
