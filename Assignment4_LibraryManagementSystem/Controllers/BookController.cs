using Assignment4_LibraryManagementSystem.Models;
using Assignment4_LibraryManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment4_LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;
        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Invalid input data. Please check the book details.");
            }

            var success = await _bookService.AddBook(book);
            if (!success)
            {
                return BadRequest("A book with the same Name or ISBN already exists.");
            }

            return Ok("Book added successfully.");
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        // GET api/<BookController>/5
        [HttpGet("{bookId}")]
        public async Task<ActionResult<Book>> GetBookById(int bookId)
        {
            if (bookId <= 0)
            {
                return BadRequest("Invalid ID. The ID must be greater than zero.");
            }

            var book = await _bookService.GetBookById(bookId);
            if (book == null)
            {
                return NotFound($"Book with ID {bookId} was not found.");
            }

            return Ok(book);
        }

        // PUT api/<BookController>/5
        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(int bookId, [FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Invalid input data. Please check the book details.");
            }

            var success = await _bookService.UpdateBook(bookId, book);
            if (!success)
            {
                return BadRequest("Unable to update book. Title or ISBN might already exist.");
            }

            return Ok("Book updated successfully.");
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var success = await _bookService.DeleteBook(bookId);
            if (!success)
            {
                return NotFound("Book not found.");
            }

            return Ok("Book deleted successfully.");
        }
    }
}
