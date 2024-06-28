using Microsoft.AspNetCore.Mvc;
using SistemaBiblioteca.DB;
using SistemaBiblioteca.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BibliotecaContext>();
builder.Services.AddTransient<DAL<Book>>();


var app = builder.Build();



app.MapGet("/books", ([FromServices] DAL<Book> dal) =>
{
    return Results.Ok(dal.ShowAll());
});

app.Run();
