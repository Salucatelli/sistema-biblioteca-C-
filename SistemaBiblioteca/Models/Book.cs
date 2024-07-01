namespace SistemaBiblioteca.Models;

public class Book
{
    public Book(string title, int autorId)
    {
        this.Title = title;
        this.AutorId = autorId;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public int AutorId { get; set; }
    public virtual Autor Autor { get; set; } = null!;
}
