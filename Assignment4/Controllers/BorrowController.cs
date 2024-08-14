using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment4_LibraryManagementSystem.Models;
using Assignment4_LibraryManagementSystem.Services;
using Assignment4_LibraryManagementSystem.Interfaces;

namespace Assignment4_LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly BorrowService _borrowService;
        private readonly IConfiguration _configuration;

        public BorrowController(BorrowService borrowService, IConfiguration configuration)
        {
            _borrowService = borrowService;
            _configuration = configuration;
        }

        // POST: api/Borrow
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Borrow>> AddBorrow([FromBody] Borrow borrow)
        {
            if (borrow == null)
            {
                return BadRequest("Invalid input data. Please check the borrow data");
            }
            var durationLoanBooks = _configuration.GetValue<int>("MySetting:DurationBookLoans");
            var maxBookBorrowed = _configuration.GetValue<int>("MySetting:MaxBookBorrowed");
            var result = await _borrowService.AddBorrow(borrow, durationLoanBooks, maxBookBorrowed);
            var (isSuccess, message) = await _borrowService.AddBorrow(borrow, durationLoanBooks, maxBookBorrowed);

            if (isSuccess)
            {
                return Ok(message);
            }

            return BadRequest(message);
        }

        // GET: api/Borrow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrow>>> GetAllBorrows()
        {
            var borrows = await _borrowService.GetAllBorrows();
            return Ok(borrows);
        }

        // GET: api/Borrow/5
        [HttpGet("{borrowId}")]
        public async Task<ActionResult<Borrow>> GetBorrowById(int borrowId)
        {
            if (borrowId <= 0)
            {
                return BadRequest("Invalid ID. The ID must grather than zero");
            }
            var borrow = await _borrowService.GetBorrowById(borrowId);
            if (borrow == null)
            {
                return NotFound($"User with ID {borrowId} not found.");
            }
            return Ok(borrow);
        }

        [HttpPut("return/{borrowId}")]
        public async Task<ActionResult> UpdateBorrow(int borrowId)
        {
            if (borrowId <= 0)
            {
                return BadRequest("Invalid borrow ID.");
            }
            // Memanggil service untuk menangani logika update
            bool result = await _borrowService.ReturnBook(borrowId);
            if (result)
            {
                return Ok("Borrow record updated successfully.");
            }
            else
            {
                return NotFound("Borrow record not found.");
            }
        }

        //// PUT: api/Borrow/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBorrow(int id, Borrow borrow)
        //{

        //}

        //// DELETE: api/Borrow/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBorrow(int id)
        //{

        //}
    }
}
