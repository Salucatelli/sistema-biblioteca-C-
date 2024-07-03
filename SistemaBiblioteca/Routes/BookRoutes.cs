using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
            var query = dal.ShowAllSelected();

            var book = query.Select(a => new BookDTO(a.Title, a.ReleaseYear, a.AutorId, a.Autor.Name, a.Id));

            return Results.Ok(book);
        });

        ////Find a Book by Id 
        app.MapGet("/books/{id}", ([FromServices] DAL<Book> dal, int id) =>
        {
            var query = dal.FindOneSelect(a => a.Id == id)!;

            if (query is null)
            {
                return Results.NotFound("Livro não encontrado");
            }

            var book = new BookDTO(query.Title, query.ReleaseYear, query.AutorId, query.Autor.Name, query.Id);

            return Results.Ok(book);
        });

        //Add a book
        app.MapPost("/books", ([FromServices] DAL<Autor> dalAutor, [FromServices] DAL<Book> dalBook, [FromBody] BookCreateDTO bookdto) =>
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
        app.MapPut("/books", ([FromServices] DAL<Book> dal, [FromBody] BookUpdateDTO bookdto) =>
        {
            if (bookdto == null)
            {
                return Results.NotFound("Existem dados faltando");
            }

            var book = dal.FindOne(a => a.Id == bookdto.Id);

            if(book == null)
            {
                return Results.NotFound("Livro não econtrado");
            }

            book.Title = bookdto.Title;
            book.ReleaseYear = bookdto.ReleaseYear;

            dal.Update(book);
            return Results.Ok("Livro Atualizado com Sucesso!");
        });

        //Delete a Book
        app.MapDelete("/books/{id}", ([FromServices] DAL<Book> dal, int id) =>
        {
            var book = dal.FindOne(app => app.Id == id);

            if (book == null)
            {
                return Results.NotFound("Livro não encontrado");
            }

            dal.Delete(book);
            return Results.Ok("Livro Deletado com Sucesso!");
        });
    }
}
