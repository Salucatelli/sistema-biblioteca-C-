using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SistemaBiblioteca.DB;
using SistemaBiblioteca.DTOs;
using SistemaBiblioteca.Models;
using System.Runtime.CompilerServices;

namespace SistemaBiblioteca.Routes;

public static class LoanRoutes
{
    public static void AddLoanRoutes(this WebApplication app)
    {
        //See All Loans
        app.MapGet("/loans", ([FromServices] DAL<Loan> dal) =>
        {
            return Results.Ok(dal.ShowAll());
        });

        //See a Loan by Id
        app.MapGet("/loans/{id}", ([FromServices] DAL<Loan> dal, int id) =>
        {
            var loan = dal.FindOne(a => a.Id == id);

            if(loan == null)
            {
                return Results.NotFound("Empréstimo não encontrado");
            }

            return Results.Ok(loan);
        });

        //Create Loan
        app.MapPost("/loans", ([FromServices] DAL<Loan> dalLoan, [FromServices] DAL <Book> dalBook, [FromBody] LoanCreateDTO loandto) =>
        {
            var book = dalBook.FindOne(a => a.Id == loandto.BookId);

            if(book == null)
            {
                return Results.NotFound("Livro não encontrado");
            }

            var valid = dalLoan.FindOne(a => a.BookId == loandto.BookId);
            if(valid is not null)
            {
                return Results.BadRequest("Este livro ja foi emprestado");
            }

            var loan = new Loan { BookId = loandto.BookId };

            dalLoan.Add(loan);

            return Results.Ok("Empréstimo Adicionado Com Sucesso");
        });

        //Update Loan
        app.MapPut("/loans", ([FromServices] DAL<Loan> dalLoan, [FromServices] DAL <Book> dalBook, [FromBody] LoanUpdateDTO loandto) =>
        {
            var loan = dalLoan.FindOne(a => a.Id == loandto.Id);
            var book = dalBook.FindOne(a => a.Id == loandto.BookId);

            if (loan == null || book == null)
            {
                return Results.NotFound("Empréstimo não encontrado");
            }

            loan.BookId = loandto.BookId;
            loan.Book = book;

            dalLoan.Update(loan);
            return Results.Ok("Empréstimo Atualizado com Sucesso!");
        });

        //Delete Loan
        app.MapDelete("/loans/{id}", ([FromServices] DAL<Loan> dal, int id) =>
        {
            var loan = dal.FindOne(a => a.Id == id);

            if(loan is not null)
            {
                dal.Delete(loan);
                return Results.Ok("Empréstimo deletado com sucesso");
            }

            return Results.NotFound("Empréstimo não encontrado");
        });
    }
}
