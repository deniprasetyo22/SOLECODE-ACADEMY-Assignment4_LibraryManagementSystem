using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assignment4_LibraryManagementSystem.Models;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Borrow> Borrows { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Bookid).HasName("book_pkey");
        });

        modelBuilder.Entity<Borrow>(entity =>
        {
            entity.HasKey(e => e.Borrowid).HasName("borrow_pkey");

            entity.HasOne(d => d.Book).WithMany(p => p.Borrows)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("borrow_bookid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Borrows)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("borrow_userid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("User_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
