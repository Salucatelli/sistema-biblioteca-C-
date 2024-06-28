namespace SistemaBiblioteca.DB;

public class DAL<T> where T : class
{
    private BibliotecaContext _context;

    public DAL(BibliotecaContext context)
    {
        this._context = context;
    }

    public IEnumerable<T> ShowAll()
    {
        return _context.Set<T>();
    }
}

