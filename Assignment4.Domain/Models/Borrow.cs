using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Assignment4_LibraryManagementSystem.Models;

[Table("borrow")]
public partial class Borrow
{
    [Key]
    [Column("borrowid")]
    public int Borrowid { get; set; }

    [Column("userid")]
    public int? Userid { get; set; }

    [Column("bookid")]
    public int? Bookid { get; set; }

    [Column("dateofborrow")]
    public DateOnly? Dateofborrow { get; set; }

    [Column("deadlinereturn")]
    public DateOnly? Deadlinereturn { get; set; }

    [Column("dateofreturn")]
    public DateOnly? Dateofreturn { get; set; } = null;

    [NotMapped]
    [ForeignKey("Bookid")]
    [InverseProperty("Borrows")]
    public virtual Book? Book { get; set; }

    [NotMapped]
    [ForeignKey("Userid")]
    [InverseProperty("Borrows")]
    public virtual User? User { get; set; }

    [NotMapped]
    public DateDto? DateOfBorrowObject { get; set; }
    [NotMapped]
    public DateDto? DeadlinereturnObject { get; set; }
    [NotMapped]
    public DateDto? DateOfReturnObject { get; set; }

    public void ConvertDateOfBorrowToDateOnly()
    {
        if (DateOfBorrowObject != null)
        {
            Dateofborrow = new DateOnly(DateOfBorrowObject.Year, DateOfBorrowObject.Month, DateOfBorrowObject.Day);
        }
    }
    public void ConvertDeadliniReturnToDateOnly()
    {
        if (DeadlinereturnObject != null)
        {
            Deadlinereturn = new DateOnly(DeadlinereturnObject.Year, DeadlinereturnObject.Month, DeadlinereturnObject.Day);
        }
    }
    public void ConvertDateOfReturnToDateOnly()
    {
        if (DateOfReturnObject != null)
        {
            Dateofreturn = new DateOnly(DateOfReturnObject.Year, DateOfReturnObject.Month, DateOfReturnObject.Day);
        }

    }
}
