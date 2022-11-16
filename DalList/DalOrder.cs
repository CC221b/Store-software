namespace Dal;

public class DalOrder
{
    public DO.Order Read(int ID)
    {
        for (int i = 0; i < DataSource.Config.s_indexOrder; i++)
        {
            if (DataSource.s_orderArr[i]._id == ID)
            {
                return DataSource.s_orderArr[i];
            }
        }
        throw new Exception("Sorry, no order was found matching the order_ID number.");
    }

    public int Create(DO.Order o)
    {
        if (DataSource.Config.s_indexOrder < DataSource.s_orderArr.Length)
        {
            o._id = DataSource.Config.OrderId;
            DataSource.s_orderArr[DataSource.Config.s_indexOrder++] = o;
        }  
        else
            throw new Exception("Sorry, there is no more room to enter a new order.");
        return o._id;
    }

    public DO.Order[] ReadAll()
    {
        if (DataSource.Config.s_indexOrder == 0)
        {
            throw new Exception("Sorry, there are currently no orders in the store.");
        }
        else
        {
            DO.Order[] orders = new DO.Order[DataSource.Config.s_indexOrder];
            for (int i = 0; i < DataSource.Config.s_indexOrder; i++)
            {
                orders[i] = DataSource.s_orderArr[i];
            }
            return orders;
        }
    }

    public void Update(DO.Order o)
    {
        for (int i = 0; i < DataSource.Config.s_indexOrder; i++)
        {
            if (DataSource.s_orderArr[i]._id == o._id)
            {
                DataSource.s_orderArr[i] = o;
                return;
            }
        }
        throw new Exception("Sorry, no order with the required ID number exists.");
    }

    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.s_indexOrder; i++)
        {
            if (DataSource.s_orderArr[i]._id == id)
            {
                DO.Order o = new DO.Order();
                DataSource.s_orderArr[i] = DataSource.s_orderArr[DataSource.Config.s_indexOrder];
                DataSource.s_orderArr[DataSource.Config.s_indexOrder] = o;
                DataSource.Config.s_indexOrder--;
                return;
            }
        }
        throw new Exception("Sorry, no order with the required ID number exists.");
    }
}

