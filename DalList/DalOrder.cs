using DalApi;
using DO;

namespace Dal;


internal class DalOrder: IOrder
{
    public Order Get(int ID)
    {
        for (int i = 0; i < DataSource.s_orderList.Count; i++)
        {
            if (DataSource.s_orderList[i]._id == ID)
            {
                return DataSource.s_orderList[i];
            }
        }
        throw new ExceptionNotExists();
    }

    public int Add(Order o)
    {
        if (DataSource.s_orderList.Count < 100)
        {
            o._id = DataSource.Config.OrderId;
            DataSource.s_orderList.Add(o);
        }
        else
            throw new ExceptionNoRoom();
        return o._id;
    }

    public IEnumerable<Order> GetAll()
    {
        if (DataSource.s_orderList.Count == 0)
        {
            throw new ExceptionEmpty();
        }
        else
        {
            Order[] orders = new Order[DataSource.s_orderList.Count];
            for (int i = 0; i < DataSource.s_orderList.Count; i++)
            {
                orders[i] = DataSource.s_orderList[i];
            }
            return orders;
        }
    }

    public void Update(Order o)
    {
        for (int i = 0; i < DataSource.s_orderList.Count; i++)
        {
            if (DataSource.s_orderList[i]._id == o._id)
            {
                DataSource.s_orderList[i] = o;
                return;
            }
        }
        throw new ExceptionNotExists();
    }

    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.s_orderList.Count; i++)
        {
            if (DataSource.s_orderList[i]._id == id)
            {
                Order o = new Order();
                DataSource.s_orderList[i] = DataSource.s_orderList[DataSource.s_orderList.Count];
                DataSource.s_orderList[DataSource.s_orderList.Count] = o;
                return;
            }
        }
        throw new ExceptionNotExists();
    }
}

