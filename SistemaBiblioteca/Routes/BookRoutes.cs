using Microsoft.AspNetCore.Mvc;
using SistemaBiblioteca.DB;
using SistemaBiblioteca.Models;
using System.Runtime.CompilerServices;

namespace SistemaBiblioteca.Routes;

public static class BookRoutes
{
    public static void AddBookRoutes(this WebApplication app)
    {
        //See all books
        app.MapGet("/books", ([FromServices] DAL<Book> dal) =>
        {
            return Results.Ok(dal.ShowAll());
        });


        //Add a book
        app.MapPost("/books", ([FromServices] DAL<Autor> dalAutor, [FromServices] DAL<Book> dalBook, [FromBody] Book book) =>
        {
            var autor = dalAutor.FindOne(a => a.Id == book.AutorId);

            if (autor == null)
            {
                return Results.NotFound("Autor não encontrado!");
            }

            book.Autor = autor;

            autor.Books.Add(book);
            dalBook.Add(book);
            dalAutor.Update(autor);

            return Results.Ok("Livro Adicionado com sucesso!");
        });
    }
}
