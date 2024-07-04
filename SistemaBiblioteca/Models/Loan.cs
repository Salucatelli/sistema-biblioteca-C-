namespace SistemaBiblioteca.Models;

public class Loan
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.Now;
    public DateTime DevolutionDate {  get; set; } = DateTime.Now.AddDays(7);
}
