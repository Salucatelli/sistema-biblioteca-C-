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

    public IEnumerable<T> ShowAllSelected()
    {
        return _context.Set<T>().AsQueryable();
    }

    public void Add(T obj)
    {
        _context.Set<T>().Add(obj);
        _context.SaveChanges();
    }

    public void Update(T obj)
    {
        _context.Set<T>().Update(obj);
        _context.SaveChanges();
    }

    public void Delete(T obj)
    {
        _context.Set<T>().Remove(obj);
        _context.SaveChanges();
    }

    public T? FindOne(Func<T, bool> condition)
    {
        return _context.Set<T>().FirstOrDefault(condition);
    }

    public T? FindOneSelect(Func<T, bool> condition)
    {
        return _context.Set<T>().AsQueryable().FirstOrDefault(condition);
    }
}

