using DalApi;
using DO;

namespace Dal;

internal class DalOrderItem:IOrderItem
{
    public OrderItem Get(int id)
    {
        for (int i = 0; i < DataSource.s_orderItemList.Count; i++)
        {
            if (DataSource.s_orderItemList[i]._id == id)
            {
                return DataSource.s_orderItemList[i];
            }
        }
        throw new ExceptionNotExists();
    }


    public OrderItem GetByProductIDAndOrderID(int productId, int orderId)
    {
        for (int i = 0; i < DataSource.s_orderItemList.Count; i++)
        {
            if (DataSource.s_orderItemList[i]._productId == productId && DataSource.s_orderItemList[i]._orderId == orderId)
            {
                return DataSource.s_orderItemList[i];
            }
        }
        throw new ExceptionNotExists();
    }



    public List<OrderItem> GetByOrderID(int id)
    {
        List<OrderItem> orderItems = new List<OrderItem>();
        for (int i = 0; i < DataSource.s_orderItemList.Count; i++)
        {
            if (DataSource.s_orderItemList[i]._id == id)
            {
                orderItems.Add(DataSource.s_orderItemList[i]);
            }
        }
        if (orderItems == null)
        {
            throw new ExceptionNotExists();
        }
        return orderItems;
    }

    public int Add(OrderItem oi)
    {
        if (DataSource.s_orderItemList.Count < 200)
        {
            oi._id = DataSource.Config.OrderItemId;
            DataSource.s_orderItemList.Add(oi);
        }
        else
            throw new ExceptionNoRoom();
        return oi._id;
    }

    public IEnumerable<OrderItem> GetAll()
    {
        if (DataSource.s_orderItemList.Count == 0)
        {
            throw new ExceptionEmpty();
        }
        else
        {
            OrderItem[] orderItems = new OrderItem[DataSource.s_orderItemList.Count];
            for (int i = 0; i < DataSource.s_orderItemList.Count; i++)
            {
                orderItems[i] = DataSource.s_orderItemList[i];
            }
            return orderItems;
        }
    }

    public void Update(OrderItem oi)
    {
        for (int i = 0; i < DataSource.s_orderItemList.Count; i++)
        {
            if (DataSource.s_orderItemList[i]._id == oi._id)
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
            if (DataSource.s_orderItemList[i]._id == id)
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

