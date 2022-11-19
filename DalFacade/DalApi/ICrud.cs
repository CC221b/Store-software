
namespace DalApi;

public interface ICrud<T>
{
    public int Add(T t);
    public void Delete(int id);
    public void Update(T t);
    public T Get(int ID);
    public IEnumerable<T> GetAll();
}
