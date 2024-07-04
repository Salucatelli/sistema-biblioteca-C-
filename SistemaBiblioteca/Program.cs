using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.DB;
using SistemaBiblioteca.EndPoints;
using SistemaBiblioteca.Models;
using SistemaBiblioteca.Routes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Builder Setings
builder.Services.AddDbContext<BibliotecaContext>(options =>
{
    options.
    UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseLazyLoadingProxies();
});
builder.Services.AddTransient<DAL<Book>>();
builder.Services.AddTransient<DAL<Autor>>();
builder.Services.AddTransient<DAL<Loan>>();

//Essa linha serve para corrigir um erro relacionado ao Json
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//APP
var app = builder.Build();

//API Routes
app.AddAutorRoutes();
app.AddBookRoutes();
app.AddLoanRoutes();

app.Run();
