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
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrow _borrowService;

        public BorrowController(IBorrow borrowService)
        {
            _borrowService = borrowService;
        }

        // POST: api/Borrow
        [HttpPost]
        public async Task<ActionResult> AddBorrow([FromBody] Borrow borrow)
        {
            if (borrow == null)
            {
                return BadRequest("Invalid input data. Please check the borrow data.");
            }

            var (isSuccess, message) = await _borrowService.AddBorrow(borrow);

            if (isSuccess)
            {
                return Ok(new
                {
                    Message = message,
                    BorrowDetails = borrow
                });
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
                return BadRequest("Invalid borrow ID. The ID must be greater than zero.");
            }

            var borrow = await _borrowService.GetBorrowById(borrowId);
            if (borrow == null)
            {
                return NotFound($"Borrow record with ID {borrowId} not found.");
            }

            return Ok(borrow);
        }

        // PUT: api/Borrow/return/5
        [HttpPut("return/{borrowId}")]
        public async Task<ActionResult> ReturnBook(int borrowId)
        {
            if (borrowId <= 0)
            {
                return BadRequest("Invalid borrow ID.");
            }

            bool result = await _borrowService.ReturnBook(borrowId);
            if (result)
            {
                return Ok("Book returned successfully.");
            }
            else
            {
                return NotFound("Borrow record not found.");
            }
        }

        [HttpPut("{borrowId}")]
        public async Task<ActionResult> UpdateBorrow(int borrowId, [FromBody] Borrow updatedBorrow)
        {
            if (borrowId <= 0)
            {
                return BadRequest("Invalid borrow ID.");
            }

            if (updatedBorrow == null)
            {
                return BadRequest("Invalid borrow data.");
            }

            var (isSuccess, message) = await _borrowService.UpdateBorrow(borrowId, updatedBorrow);

            if (isSuccess)
            {
                return Ok(new
                {
                    Message = message,
                    BorrowDetails = updatedBorrow
                });
            }

            return BadRequest(message);
        }

        [HttpDelete("{borrowId}")]
        public async Task<ActionResult> DeleteBorrow(int borrowId)
        {
            if (borrowId <= 0)
            {
                return BadRequest("Invalid borrow ID.");
            }

            var (isSuccess, message) = await _borrowService.DeleteBorrow(borrowId);

            if (isSuccess)
            {
                return Ok(message);
            }

            return NotFound(message);
        }
    }
}
