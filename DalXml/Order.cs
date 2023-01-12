namespace Dal;
using DalApi;
using DO;

internal class Order : IOrder
{
    public int Add(DO.Order order)
    {
        return 0;
    }
    public void Delete(int id)
    {

    }
    public void Update(DO.Order order)
    {

    }
    public DO.Order Get(int ID)
    {
        return new DO.Order();
    }
    public DO.Order Get(Predicate<DO.Order> func)
    {
        return new DO.Order();
    }
    public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
    {
        IEnumerable<DO.Order> d= new List<DO.Order>();
        return d;
    }
}

