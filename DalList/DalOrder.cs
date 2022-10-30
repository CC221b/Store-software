namespace Dal;

public class DalOrder
{
    public DO.Order Read(int ID)
    {
        for (int i = 0; i < DataSource.Config.index_Order; i++)
        {
            if (DataSource.Order_arr[i].ID == ID)
            {
                return DataSource.Order_arr[i];
            }
        }
        throw new Exception("Sorry, no order was found matching the order_ID number.");
    }

    public int Create(DO.Order o)
    {
        if (DataSource.Config.index_Order < DataSource.Order_arr.Length)
        {
            o.ID = DataSource.Config.Order_ID;
            DataSource.Order_arr[DataSource.Config.index_Order++] = o;
        }  
        else
            throw new Exception("Sorry, there is no more room to enter a new order.");
        return o.ID;
    }

    public DO.Order[] ReadAll()
    {
        if (DataSource.Config.index_Order == 0)
        {
            throw new Exception("Sorry, there are currently no orders in the store.");
        }
        else
        {
            DO.Order[] orders = new DO.Order[DataSource.Config.index_Order];
            for (int i = 0; i < DataSource.Config.index_Order; i++)
            {
                orders[i] = DataSource.Order_arr[i];
            }
            return orders;
        }
    }

    public void Update(DO.Order o)
    {
        for (int i = 0; i < DataSource.Config.index_Order; i++)
        {
            if (DataSource.Order_arr[i].ID == o.ID)
            {
                DataSource.Order_arr[i] = o;
                return;
            }
        }
        throw new Exception("Sorry, no order with the required ID number exists.");
    }

    public void Delete(int ID)
    {
        for (int i = 0; i < DataSource.Config.index_Order; i++)
        {
            if (DataSource.Order_arr[i].ID == ID)
            {
                DO.Order o = new DO.Order();
                DataSource.Order_arr[i] = DataSource.Order_arr[DataSource.Config.index_Order];
                DataSource.Order_arr[DataSource.Config.index_Order] = o;
                DataSource.Config.index_Order--;
                return;
            }
        }
        throw new Exception("Sorry, no order with the required ID number exists.");
    }
}

