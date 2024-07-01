namespace SistemaBiblioteca.Models;

public class Autor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public virtual List<Book> Books { get; set; } = new List<Book>();
}
