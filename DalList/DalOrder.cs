using DalApi;
using DO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Dal;

internal class DalOrder : IOrder
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(int id)
    {
        Order order1 = (from order in DataSource.s_orderList
                        where order.ID == id
                        select order).FirstOrDefault();
        if (order1.ID != 0)
        {
            return order1;
        }
        throw new ExceptionNotExists();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Order Get(Predicate<Order> func)
    {
        return DataSource.s_orderList.Find(func);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
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
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Order> GetAll(Func<Order, bool>? func = null)
    {
        if (DataSource.s_orderList.Count == 0)
        {
            throw new ExceptionEmpty();
        }
        return (func == null) ? DataSource.s_orderList.OrderBy(item => item.ID) : DataSource.s_orderList.Where(func).OrderBy(item => item.ID);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    private List<string> getAll(Func<string, bool>? func = null)
    {
        List<string> result = new List<string>();
        return (func == null) ? result : result.Where(func).ToList();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
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
            throw ex == null ? new ExceptionNull() : ex;
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        try
        {
            DO.Order order = Get(id);
            DataSource.s_orderList.Remove(order);
        }
        catch (Exception ex)
        {
            throw ex == null ? new ExceptionNull() : ex;
        }
    }
}

