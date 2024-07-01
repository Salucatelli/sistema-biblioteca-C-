using Microsoft.AspNetCore.Mvc;
using SistemaBiblioteca.DB;
using SistemaBiblioteca.Models;
using System.Runtime.CompilerServices;

namespace SistemaBiblioteca.EndPoints;

public static class AutorRoutes
{
    public static void AddAutorRoutes(this WebApplication app)
    {
        //See all the Autors
        app.MapGet("/autors", ([FromServices] DAL<Autor> dal) =>
        {
            return Results.Ok(dal.ShowAll());
        });

        //Add an Autor
        app.MapPost("/autors", ([FromServices] DAL<Autor> dal, [FromBody] Autor autor) =>
        {
            autor.BirthDate = DateTime.Now;
            dal.Add(autor);
            return Results.Ok("Autor adicionado!");
        });
    }
}
