using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Assignment4_LibraryManagementSystem.Models;

[Table("User")]
public partial class User
{
    [Key]
    [Column("userid")]
    public int Userid { get; set; }

    [Column("fname")]
    [StringLength(255)]
    public string Fname { get; set; } = null!;

    [Column("lname")]
    [StringLength(255)]
    public string Lname { get; set; } = null!;

    [Column("address")]
    [StringLength(255)]
    public string Address { get; set; } = null!;

    [Column("dob")]
    public DateOnly Dob { get; set; }

    [Column("sex")]
    [StringLength(255)]
    public string Sex { get; set; } = null!;

    [NotMapped]
    [InverseProperty("User")]
    public virtual ICollection<Borrow> Borrows { get; set; } = new List<Borrow>();

    [NotMapped]
    public DateDto? DobObject { get; set; }

    public void ConvertDobObjectToDateOnly()
    {
        if (DobObject != null)
        {
            Dob = new DateOnly(DobObject.Year, DobObject.Month, DobObject.Day);
        }
    }
}
