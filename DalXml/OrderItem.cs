namespace Dal;
using DalApi;
using DO;

internal class OrderItem : IOrderItem
{
    public int Add(DO.OrderItem orderItem)
    {
        return 0;
    }
    public void Delete(int id)
    {

    }
    public void Update(DO.OrderItem orderItem)
    {

    }
    public DO.OrderItem Get(int ID)
    {
        return new DO.OrderItem();
    }
    public DO.OrderItem Get(Predicate<DO.OrderItem> func)
    {
        return new DO.OrderItem();
    }
    public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
    {
        IEnumerable<DO.OrderItem> d = new List<DO.OrderItem>();
        return d;
    }
    public IEnumerable<DO.OrderItem> GetByOrderID(int id)
    {
        return new List<DO.OrderItem>();
    }
    public DO.OrderItem GetByProductIDAndOrderID(int productId, int orderId)
    {
        return new DO.OrderItem();
    }
}

