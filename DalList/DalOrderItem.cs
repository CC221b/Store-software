namespace Dal;

public class DalOrderItem
{
    public DO.OrderItem Read(int ID)
    {
        for (int i = 0; i < DataSource.Config.index_OrderItem; i++)
        {
            if (DataSource.OrderItem_arr[i].ID == ID)
            {
                return DataSource.OrderItem_arr[i];
            }
        }
        throw new Exception("Sorry, no orderItem was found matching the orderItem_ID number.");
    }


    public DO.OrderItem ReadByProductIDAndOrderID(int product_ID, int order_ID)
    {
        for (int i = 0; i < DataSource.Config.index_OrderItem; i++)
        {
            if (DataSource.OrderItem_arr[i].ProductId == product_ID && DataSource.OrderItem_arr[i].OrderId == order_ID)
            {
                return DataSource.OrderItem_arr[i];
            }
        }
        throw new Exception("Sorry, no orderItem was found matching the order_ID and product_ID numbers.");
    }

    /// <summary>
    /// האם גם כאן בלי פרטים?
    /// </summary>
    public List<DO.OrderItem> ReadByOrderID(int ID)
    {
        List<DO.OrderItem> OrderItems = new List<DO.OrderItem>();
        for (int i = 0; i < DataSource.Config.index_OrderItem; i++)
        {
            if (DataSource.OrderItem_arr[i].ID == ID)
            {
                OrderItems.Add(DataSource.OrderItem_arr[i]);
            }
        }
        if (OrderItems == null)
        {
            throw new Exception("Sorry, no orderItems was found matching the orderItem_ID number.");
        }
        return OrderItems;
    }

    public int Create(DO.OrderItem oi)
    {
        if (DataSource.Config.index_OrderItem < DataSource.OrderItem_arr.Length)
        {
            oi.ID = DataSource.Config.OrderItem_ID;
            DataSource.OrderItem_arr[DataSource.Config.index_OrderItem++] = oi;
        }
        else
            throw new Exception("Sorry, there is no more room to enter a new orderItem.");
        return oi.ID;
    }

    public Tuple<int, int>[] ReadAll()
    {
        if (DataSource.Config.index_OrderItem == 0)
        {
            throw new Exception("Sorry, there are currently no orderItems in the store.");
        }
        else
        {
            Tuple<int, int>[] orderItems = new Tuple<int, int>[DataSource.Config.index_OrderItem];
            for (int i = 0; i < DataSource.Config.orderItem_ID; i++)
            {
                orderItems[i] = new Tuple<int, int>(DataSource.OrderItem_arr[i].ID, DataSource.OrderItem_arr[i].OrderId);
            }
            return orderItems;
        }
    }

    public void Update(DO.OrderItem oi)
    {
        for (int i = 0; i < DataSource.Config.index_OrderItem; i++)
        {
            if (DataSource.OrderItem_arr[i].ID == oi.ID)
            {
                DataSource.OrderItem_arr[i] = oi;
                return;
            }
        }
        throw new Exception("Sorry, no order with the required ID number exists.");
    }

    public void Delete(int ID)
    {
        for (int i = 0; i < DataSource.Config.index_OrderItem; i++)
        {
            if (DataSource.OrderItem_arr[i].ID == ID)
            {
                DO.OrderItem oi = new DO.OrderItem();
                DataSource.OrderItem_arr[i] = DataSource.OrderItem_arr[DataSource.Config.index_OrderItem];
                DataSource.OrderItem_arr[DataSource.Config.index_OrderItem] = oi;
                DataSource.Config.index_OrderItem--;
                return;
            }
        }
        throw new Exception("Sorry, no orderItem with the required ID number exists.");
    }
}

