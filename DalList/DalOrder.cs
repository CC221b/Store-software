using DalApi;
using DO;
using System.Linq;

namespace Dal;

internal class DalOrder: IOrder
{
    public Order Get(int id)
    {
        for (int i = 0; i < DataSource.s_orderList.Count; i++)
        {
            if (DataSource.s_orderList[i].ID == id)
            {
                return DataSource.s_orderList[i];
            }
        }
        throw new ExceptionNotExists();
    }

    public Order Get(Predicate<Order> func)
    {
       return DataSource.s_orderList.Find(func);
    }

    public int Add(Order o)
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
        for (int i = 0; i < DataSource.s_orderList.Count; i++)
        {
            if (DataSource.s_orderList[i].ID == o.ID)
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
            if (DataSource.s_orderList[i].ID == id)
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

