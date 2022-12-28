using DalApi;
using DO;
using System.Linq;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    public OrderItem Get(int id)
    {
        IEnumerable<OrderItem> orderItem1 = from orderItem in DataSource.s_orderItemList
                                            where orderItem.ID == id
                                            select orderItem;
        if (orderItem1 != null && orderItem1.Any())
        {
            return orderItem1.First();
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

    public List<OrderItem> GetByOrderID(int id)
    {
        IEnumerable<OrderItem> orderItem1 = from orderItem in DataSource.s_orderItemList
                                            where orderItem.OrderId == id
                                            select orderItem;
        if (orderItem1 != null && orderItem1.Any())
        {
            return orderItem1.ToList();
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
        return (func == null) ? DataSource.s_orderItemList : DataSource.s_orderItemList.Where(func);
    }

    public void Update(OrderItem oi)
    {
        for (int i = 0; i < DataSource.s_orderItemList.Count; i++)
        {
            if (DataSource.s_orderItemList[i].ID == oi.ID)
            {
                DataSource.s_orderItemList[i] = oi;
                return;
            }
        }
        throw new ExceptionNotExists();
    }

    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.s_orderItemList.Count; i++)
        {
            if (DataSource.s_orderItemList[i].ID == id)
            {
                OrderItem oi = new OrderItem();
                DataSource.s_orderItemList[i] = DataSource.s_orderItemList[DataSource.s_orderItemList.Count];
                DataSource.s_orderItemList[DataSource.s_orderItemList.Count] = oi;
                return;
            }
        }
        throw new ExceptionNotExists();
    }
}

