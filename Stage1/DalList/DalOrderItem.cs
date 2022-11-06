namespace Dal;

public class DalOrderItem
{
    public DO.OrderItem Read(int id)
    {
        for (int i = 0; i < DataSource.Config.s_indexOrderItem; i++)
        {
            if (DataSource.s_orderItemArr[i]._id == id)
            {
                return DataSource.s_orderItemArr[i];
            }
        }
        throw new Exception("Sorry, no orderItem was found matching the orderItem_ID number.");
    }


    public DO.OrderItem ReadByProductIDAndOrderID(int productId, int orderId)
    {
        for (int i = 0; i < DataSource.Config.s_indexOrderItem; i++)
        {
            if (DataSource.s_orderItemArr[i]._productId == productId && DataSource.s_orderItemArr[i]._orderId == orderId)
            {
                return DataSource.s_orderItemArr[i];
            }
        }
        throw new Exception("Sorry, no orderItem was found matching the order_ID and product_ID numbers.");
    }

    public List<DO.OrderItem> ReadByOrderID(int id)
    {
        List<DO.OrderItem> orderItems = new List<DO.OrderItem>();
        for (int i = 0; i < DataSource.Config.s_indexOrderItem; i++)
        {
            if (DataSource.s_orderItemArr[i]._id == id)
            {
                orderItems.Add(DataSource.s_orderItemArr[i]);
            }
        }
        if (orderItems == null)
        {
            throw new Exception("Sorry, no orderItems was found matching the orderItem_ID number.");
        }
        return orderItems;
    }

    public int Create(DO.OrderItem oi)
    {
        if (DataSource.Config.s_indexOrderItem < DataSource.s_orderItemArr.Length)
        {
            oi._id = DataSource.Config.OrderItemID;
            DataSource.s_orderItemArr[DataSource.Config.s_indexOrderItem++] = oi;
        }
        else
            throw new Exception("Sorry, there is no more room to enter a new orderItem.");
        return oi._id;
    }

    public DO.OrderItem[] ReadAll()
    {
        if (DataSource.Config.s_indexOrderItem == 0)
        {
            throw new Exception("Sorry, there are currently no orderItems in the store.");
        }
        else
        {
            DO.OrderItem[] orderItems = new DO.OrderItem[DataSource.Config.s_indexOrderItem];
            for (int i = 0; i < DataSource.Config.s_indexOrderItem; i++)
            {
                orderItems[i] = DataSource.s_orderItemArr[i];
            }
            return orderItems;
        }
    }

    public void Update(DO.OrderItem oi)
    {
        for (int i = 0; i < DataSource.Config.s_indexOrderItem; i++)
        {
            if (DataSource.s_orderItemArr[i]._id == oi._id)
            {
                DataSource.s_orderItemArr[i] = oi;
                return;
            }
        }
        throw new Exception("Sorry, no order with the required ID number exists.");
    }

    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.s_indexOrderItem; i++)
        {
            if (DataSource.s_orderItemArr[i]._id == id)
            {
                DO.OrderItem oi = new DO.OrderItem();
                DataSource.s_orderItemArr[i] = DataSource.s_orderItemArr[DataSource.Config.s_indexOrderItem];
                DataSource.s_orderItemArr[DataSource.Config.s_indexOrderItem] = oi;
                DataSource.Config.s_indexOrderItem--;
                return;
            }
        }
        throw new Exception("Sorry, no orderItem with the required ID number exists.");
    }
}

