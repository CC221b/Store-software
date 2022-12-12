
using DO;

namespace DalApi;

public interface ICrud<T>
{
    public int Add(T t);
    public void Delete(int id);
    public void Update(T t);
    public T Get(int ID);
    public T Get(Predicate<T> func);
    public IEnumerable<T> GetAll(Func<T, bool>? func = null);
}
