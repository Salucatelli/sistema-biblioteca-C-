namespace SistemaBiblioteca.Models;

public class Autor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BirthYear { get; set; }
    public virtual ICollection<Book> Books { get; set; }
}
