using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.DB;
using SistemaBiblioteca.EndPoints;
using SistemaBiblioteca.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BibliotecaContext>(options =>
{
    options.
    UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseLazyLoadingProxies();
});
builder.Services.AddTransient<DAL<Book>>();
builder.Services.AddTransient<DAL<Autor>>();


var app = builder.Build();

//API Routes
app.AddAutorRoutes();


app.MapGet("/books", ([FromServices] DAL<Book> dal) =>
{
    return Results.Ok(dal.ShowAll());
});

app.Run();
