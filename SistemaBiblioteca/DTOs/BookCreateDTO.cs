using SistemaBiblioteca.Models;

namespace SistemaBiblioteca.DTOs;

public record BookCreateDTO(string Title, int ReleaseYear, int AutorId, string autorName)
{
}