namespace SistemaBiblioteca.Models;

public class Book
{
    public Book(string title, int autorId)
    {
        this.Title = title;
        this.AutorId = autorId;
    }

    public int Id { get; set; }
    public required string Title { get; set; }
    public string? ReleaseDate { get; set; }
    public required int AutorId { get; set; }
}
