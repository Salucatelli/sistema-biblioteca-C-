using Microsoft.AspNetCore.Mvc;
using SistemaBiblioteca.DB;
using SistemaBiblioteca.DTOs;
using SistemaBiblioteca.Models;
using System.Runtime.CompilerServices;

namespace SistemaBiblioteca.Routes;

public static class BookRoutes
{
    public static void AddBookRoutes(this WebApplication app)
    {
        //Find all books
        app.MapGet("/books", ([FromServices] DAL<Book> dal) =>
        {
            return Results.Ok(dal.ShowAll());
        });

        ////Find a Book by Id ----------------((NOT WORKING))-------------------
        //app.MapGet("/books/{id}", ([FromServices] DAL<Book> dal, int id) =>
        //{
        //    var book = dal.FindOne(a => a.Id == id);

        //    if(book is null)
        //    {
        //        return Results.NotFound("Livro não encontrado");
        //    }

        //    return Results.Ok(book);
        //});

        //Add a book
        app.MapPost("/books", ([FromServices] DAL<Autor> dalAutor, [FromServices] DAL<Book> dalBook, [FromBody] BookDTO bookdto) =>
        {
            var autor = dalAutor.FindOne(a => a.Id == bookdto.AutorId);

            if (autor == null)
            {
                return Results.NotFound("Autor não encontrado!");
            }

            var book = new Book(bookdto.Title, bookdto.AutorId)
            {
                ReleaseYear = bookdto.ReleaseYear,
            };

            book.Autor = autor;

            autor.Books.Add(book);
            dalBook.Add(book);
            dalAutor.Update(autor);

            return Results.Ok("Livro Adicionado com sucesso!");
        });

        //Update a Book
        app.MapPut("/books", ([FromServices] DAL<Book> dal, [FromBody] Book book) =>
        {

        });
    }
}
