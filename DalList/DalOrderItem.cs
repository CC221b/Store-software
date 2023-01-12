using DalApi;
using DO;
using System.Linq;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    public OrderItem Get(int id)
    {
        OrderItem orderItem1 = (from orderItem in DataSource.s_orderItemList
                                where orderItem.ID == id
                                select orderItem).FirstOrDefault();
        if (orderItem1.ID != 0)
        {
            return orderItem1;
        }
        throw new ExceptionNotExists();
    }

    public OrderItem Get(Predicate<OrderItem> func)
    {
        return DataSource.s_orderItemList.Find(func);
    }

    public OrderItem GetByProductIDAndOrderID(int productId, int orderId)
    {
        IEnumerable<OrderItem> orderItem1 = from orderItem in DataSource.s_orderItemList
                                            where orderItem.ProductId == productId && orderItem.OrderId == orderId
                                            select orderItem;
        if (orderItem1 != null && orderItem1.Any())
        {
            return orderItem1.First();
        }
        throw new ExceptionNotExists();
    }

    public IEnumerable<OrderItem> GetByOrderID(int id)
    {
        IEnumerable<OrderItem> orderItem1 = from orderItem in DataSource.s_orderItemList
                                            where orderItem.OrderId == id
                                            select orderItem;
        if (orderItem1 != null && orderItem1.Any())
        {
            return orderItem1;
        }
        throw new ExceptionNotExists();
    }

    public int Add(OrderItem oi)
    {
        if (DataSource.s_orderItemList.Count < 200)
        {
            oi.ID = DataSource.Config.OrderItemId;
            DataSource.s_orderItemList.Add(oi);
        }
        else
            throw new ExceptionNoRoom();
        return oi.ID;
    }

    public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? func = null)
    {
        if (DataSource.s_orderItemList.Count == 0)
        {
            throw new ExceptionEmpty();
        }
        return (func == null) ? DataSource.s_orderItemList.OrderBy(item => item.OrderId) : DataSource.s_orderItemList.Where(func).OrderBy(item => item.OrderId);
    }

    public void Update(OrderItem oi)
    {
        try
        {
            DO.OrderItem orderItem = Get(oi.ID);
            int index = DataSource.s_orderItemList.IndexOf(orderItem);
            DataSource.s_orderItemList[index] = oi;
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
            DO.OrderItem orderItem = Get(id);
            DataSource.s_orderItemList.Remove(orderItem);
        }
        catch (Exception ex)
        {
            throw ex == null ? new ExceptionNullEx() : ex;
        }
    }
}

