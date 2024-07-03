using Microsoft.AspNetCore.Mvc;
using SistemaBiblioteca.DB;
using SistemaBiblioteca.Models;
using SistemaBiblioteca.DTOs;
using System.Runtime.CompilerServices;

namespace SistemaBiblioteca.EndPoints;

public static class AutorRoutes
{
    public static void AddAutorRoutes(this WebApplication app)
    {
        //Find all the Autors
        app.MapGet("/autors", ([FromServices] DAL<Autor> dal) =>
        {
            var query = dal.ShowAllSelected();

            var autor = query.Select(l => new AutorDTO(l.Name, l.BirthYear, l.Id)).ToList();

            return Results.Ok(autor);
        });

        //Find One Autor by Id
        app.MapGet("/autors/{id}", ([FromServices] DAL<Autor> dal, int id) =>
        {
            var autor = dal.FindOne(a => a.Id == id);

            if(autor is not null)
            {
                return Results.Ok(autor);
            }
            return Results.NotFound("Autor não encontrado!");
        });

        //Add an Autor
        app.MapPost("/autors", ([FromServices] DAL<Autor> dal, [FromBody] AutorDTO autordto) =>
        {
            var autor = new Autor()
            {
                Name = autordto.Name,
                BirthYear = autordto.BirthYear
            };
            
            dal.Add(autor);
            return Results.Ok("Autor adicionado!");
        });

        //Update an Autor
        app.MapPut("/autors", ([FromServices] DAL<Autor> dal, AutorUpdateDTO autordto) =>
        {
            if (autordto == null)
            {
                return Results.NotFound("Existem dados faltando");
            }

            var autorUpdate = dal.FindOne(a => a.Id == autordto.Id);

            if (autorUpdate == null)
            {
                return Results.NotFound("Autor não encontrado");
            }

            autorUpdate.Name = autordto.Name;
            autorUpdate.BirthYear = autordto.BirthYear;

            dal.Update(autorUpdate);
            return Results.Ok("Autor Atualizado com Sucesso!");
        });

        //Delete an Autor
        app.MapDelete("/autors/{id}", ([FromServices] DAL<Autor> dal, int id) => 
        {
            var autor = dal.FindOne(a => a.Id == id);

            if (autor == null)
            {
                return Results.NotFound("Autor não Encontrado");
            }

            dal.Delete(autor);
            return Results.Ok("Autor Deletado com Sucesso!");
        });
    }
}
