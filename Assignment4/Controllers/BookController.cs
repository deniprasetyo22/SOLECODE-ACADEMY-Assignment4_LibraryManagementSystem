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

        /// <summary>
        /// Adds a new book to the system.
        /// </summary>
        /// <remarks>
        /// Ensure that the book data is not null and that the book details are valid.
        /// Validate that no book with the same Name or ISBN already exists.
        ///
        /// Sample request:
        ///
        ///     POST /api/Book
        ///     {
        ///        "Title": "The Great Gatsby",
        ///        "Author": "F. Scott Fitzgerald",
        ///        "PublicationYear": 1925
        ///        "ISBN": "9780743273565",
        ///     }
        /// </remarks>
        /// <param name="book">The book to be added.</param>
        /// <returns>Success message if the book is added successfully or an error message if validation fails.</returns>
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

        /// <summary>
        /// Retrieves a list of all books in the system.
        /// </summary>
        /// <remarks>
        /// This endpoint retrieves all books available in the system.
        ///
        /// Sample request:
        ///
        ///     GET /api/Book
        /// </remarks>
        /// <returns>A list of books.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        /// <summary>
        /// Retrieves a book by its ID.
        /// </summary>
        /// <remarks>
        /// Ensure that the provided book ID is valid (greater than zero).
        ///
        /// Sample request:
        ///
        ///     GET /api/Book/5
        /// </remarks>
        /// <param name="bookId">The ID of the book to be retrieved.</param>
        /// <returns>Book details if found, otherwise an error message.</returns>
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

        /// <summary>
        /// Updates an existing book by its ID.
        /// </summary>
        /// <remarks>
        /// Ensure that the book data is not null and that all required fields are provided.
        /// Validate that the book's name and ISBN are unique.
        ///
        /// Sample request:
        ///
        ///     PUT /api/Book/5
        ///     {
        ///        "Title": "Updated Book Title",
        ///        "Author": "Updated Author",
        ///        "PublicationYear": 1925
        ///        "ISBN": "9780743273565",
        ///     }
        /// </remarks>
        /// <param name="bookId">The ID of the book to be updated.</param>
        /// <param name="book">The updated book details.</param>
        /// <returns>Success message if the book is updated successfully or an error message if validation fails.</returns>
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

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <remarks>
        /// Ensure that the book ID provided is valid.
        ///
        /// Sample request:
        ///
        ///     DELETE /api/Book/5
        /// </remarks>
        /// <param name="bookId">The ID of the book to be deleted.</param>
        /// <returns>Success message if the book is deleted successfully or an error message if the book is not found.</returns>
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
