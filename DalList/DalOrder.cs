using DalApi;
using DO;
using System.Linq;
using System.Security.Cryptography;

namespace Dal;

internal class DalOrder : IOrder
{
    public Order Get(int id)
    {
        IEnumerable<DO.Order> order1 = from order in DataSource.s_orderList
                                       where order.ID == id
                                       select order;
        if (order1 != null && order1.Any())
        {
            return order1.First();
        }
        throw new ExceptionNotExists();
    }

    public Order Get(Predicate<Order> func)
    {
        return DataSource.s_orderList.Find(func);
    }

    public int Add(Order o)
    {
        try
        {
            Get(o.ID);
        }
        catch (Exception)
        {
            if (DataSource.s_orderList.Count < 100)
            {
                o.ID = DataSource.Config.OrderId;
                DataSource.s_orderList.Add(o);
            }
            else
                throw new ExceptionNoRoom();
            return o.ID;
        }
        throw new ExceptionExists();
    }

    public IEnumerable<Order> GetAll(Func<Order, bool>? func = null)
    {
        if (DataSource.s_orderList.Count == 0)
        {
            throw new ExceptionEmpty();
        }
        return (func == null) ? DataSource.s_orderList : DataSource.s_orderList.Where(func);
    }
    private List<string> getAll(Func<string, bool>? func = null)
    {
        List<string> result = new List<string>();
        return (func == null) ? result : result.Where(func).ToList();
    }

    public void Update(Order o)
    {
        try
        {
            DO.Order order = Get(o.ID);
            int index = DataSource.s_orderList.IndexOf(order);
            DataSource.s_orderList[index] = o;
        }
        catch (Exception ex)
        {
            throw ex == null ? new ExceptionNullEx() : ex;
        }
    }

    public void Delete(int id)
    {
        try
        {
            DO.Order order = Get(id);
            DataSource.s_orderList.Remove(order);
        }
        catch (Exception ex)
        {
            throw ex == null ? new ExceptionNullEx() : ex;
        }
    }
}

