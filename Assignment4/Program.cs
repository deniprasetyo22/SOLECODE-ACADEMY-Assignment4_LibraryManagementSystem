//using Assignment4_LibraryManagementSystem.Models;
using Assignment4.Persistence;
using Assignment4_LibraryManagementSystem.Models;
using Assignment4_LibraryManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Using PostgreSQL
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<LibraryContext>(options => options.UseNpgsql(connectionString));
//builder.Services.AddScoped<BookService>();
//builder.Services.AddScoped<UserService>();
//builder.Services.AddScoped<BorrowService>();

builder.Services.ConfigurePersistence(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
