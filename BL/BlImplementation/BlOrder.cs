using BlApi;


namespace BlImplementation;

internal class BlOrder : IOrder
{
    private static DalApi.IDal? Dal = DalApi.Factory.Get();

    /// <summary>
    /// The function returns a list of orderForList objects.
    /// It fetches all the orders from the data layer and creates a list of orderForList objects and returns the list.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    public IEnumerable<BO.OrderForList> GetAll(Func<DO.Order, bool>? func = null)
    {
        try
        {
            return (from item in Dal?.Order.GetAll(func)
                    let orderItems = Dal?.OrderItem.GetByOrderID(item.ID)
                    let resultCompareShipDate = DateTime.Compare(item.ShipDate ?? throw new BO.ExceptionNull(), DateTime.Now)
                    let resultCompareDeliveryDate = DateTime.Compare(item.DeliveryDate ?? throw new BO.ExceptionNull(), DateTime.Now)
                    select new BO.OrderForList()
                    {
                        ID = item.ID,
                        CustomerName = item.CustomerName,
                        Status =
                         resultCompareShipDate <= 0 ? BO.OrderStatus.SendOrder :
                         resultCompareDeliveryDate <= 0 ? BO.OrderStatus.ProvidedCustomerOrder :
                         BO.OrderStatus.ConfirmedOrder,
                        AmountOfItems = orderItems != null ? orderItems.Count() : throw new BO.ExceptionNull(),
                        TotalPrice = orderItems != null ? orderItems.Sum(item => item.Price) : throw new BO.ExceptionNull()
                    });
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
    }

    /// <summary>
    /// The function returns an order.
    /// Checking if the ID is correct.
    /// If so fetches the order from the data entity and converts it to a logical entity and returns the logical entity.
    /// throws errors accordingly.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    /// <exception cref="BO.ExceptionInvalidID"></exception>
    public BO.Order Get(int id)
    {
        if (id > 0)
        {
            try
            {
                DO.Order orderTypeDO = new DO.Order();
                BO.Order orderTypeBO = new BO.Order();
                orderTypeDO = Dal?.Order.Get(id) ?? throw new BO.ExceptionNull();
                IEnumerable<DO.OrderItem> orderItem = new List<DO.OrderItem>();
                orderItem = Dal.OrderItem.GetByOrderID(orderTypeDO.ID);
                orderTypeBO.ID = orderTypeDO.ID;
                orderTypeBO.CustomerName = orderTypeDO.CustomerName;
                orderTypeBO.CustomerAdress = orderTypeDO.CustomerAdress;
                orderTypeBO.CustomerEmail = orderTypeDO.CustomerEmail;
                orderTypeBO.ShipDate = orderTypeDO.ShipDate ?? throw new BO.ExceptionNull();
                orderTypeBO.OrderDate = orderTypeDO.OrderDate ?? throw new BO.ExceptionNull();
                orderTypeBO.DeliveryDate = orderTypeDO.DeliveryDate ?? throw new BO.ExceptionNull();
                if (DateTime.Compare(orderTypeDO.ShipDate ?? throw new BO.ExceptionNull(), DateTime.Now) <= 0)
                    orderTypeBO.Status = BO.OrderStatus.SendOrder;
                else if (DateTime.Compare(orderTypeDO.DeliveryDate ?? throw new BO.ExceptionNull(), DateTime.Now) <= 0)
                    orderTypeBO.Status = BO.OrderStatus.ProvidedCustomerOrder;
                else
                    orderTypeBO.Status = BO.OrderStatus.ConfirmedOrder;
                orderTypeBO.Items = orderItem.ToList();
                double sum = 0;
                sum = orderItem.Sum(item => item.Price);
                orderTypeBO.TotalPrice = sum;
                return orderTypeBO;
            }
            catch (Exception ex)
            {
                throw new BO.ExceptionFromDal(ex);
            }
        }
        throw new BO.ExceptionInvalidID();
    }

    /// <summary>
    /// Updating order shipment.
    /// Receives an order number and updates that the order has been sent,
    /// throws errors accordingly.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    /// <exception cref="BO.ExceptionNotExists"></exception>
    public BO.Order UpdateOrderShipping(int id)
    {
        try
        {
            DO.Order orderTypeDO = new DO.Order();
            orderTypeDO = Dal?.Order.Get(id) ?? throw new BO.ExceptionNull();
            if (DateTime.Compare(orderTypeDO.ShipDate ?? throw new BO.ExceptionNull(), DateTime.Now) > 0)
            {
                BO.Order orderTypeBO = new BO.Order();
                orderTypeDO.ShipDate = DateTime.Now;
                Dal.Order.Update(orderTypeDO);
                orderTypeBO = Get(id);
                return orderTypeBO;
            }
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
        throw new BO.ExceptionNotExists();
    }

    /// <summary>
    /// Updates receipt of an order.
    /// Receives an order number and updates that the order has been received,
    /// throws errors accordingly.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    /// <exception cref="BO.ExceptionNotExists"></exception>
    public BO.Order UpdateOrderDelivery(int id)
    {
        try
        {
            DO.Order orderTypeDO = new DO.Order();
            orderTypeDO = Dal?.Order.Get(id) ?? throw new BO.ExceptionNull();
            if (DateTime.Compare(orderTypeDO.DeliveryDate ?? throw new BO.ExceptionNull(), DateTime.Now) < 0)
            {
                BO.Order orderTypeBO = new BO.Order();
                orderTypeDO.DeliveryDate = DateTime.Now;
                Dal.Order.Update(orderTypeDO);
                orderTypeBO = Get(id);
                return orderTypeBO;
            }
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
        throw new BO.ExceptionNotExists();
    }

    public BO.OrderTracking GetOrderTracking(int id)
    {
        BO.Order order = new BO.Order();
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        orderTracking.DateAndStatus = new();
        try
        {
            order = Get(id);
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
        orderTracking.ID = id;
        orderTracking.Status = order.Status;
        if (order.Status >= BO.OrderStatus.ConfirmedOrder) orderTracking?.DateAndStatus?.Add(order.OrderDate, "ConfirmedOrder");
        if (order.Status >= BO.OrderStatus.SendOrder) orderTracking?.DateAndStatus?.Add(order.ShipDate, "SendOrder");
        if (order.Status >= BO.OrderStatus.ProvidedCustomerOrder) orderTracking?.DateAndStatus?.Add(order.DeliveryDate, "ProvidedCustomerOrder");
        return orderTracking ?? throw new BO.ExceptionNull();
    }

    /// <summary>
    /// Bonus!!!
    ///The function gives the administrator several options:
    ///1. Add a product(the limitation here is whether a product exists and is available in quantity.)
    ///2. Delete a product - the restriction An order has already been sent...
    ///3. Update quantity in stock (the limitation - there is enough quantity in stock..)
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    /// <exception cref="BO.ExceptionNotExists"></exception>
    /// <exception cref="Exception"></exception>
    public void Update(BO.Order order, string action, DO.OrderItem? orderItem = null, int newAmount = 0)
    {
        BO.Order order1 = Get(order.ID);
        if (action == "Add")
        {
            try
            {
                DO.Product product = new DO.Product();
                product = Dal?.Product.Get(orderItem?.ProductId ?? throw new BO.ExceptionNull()) ?? throw new BO.ExceptionNull();
                if (product.InStock <= orderItem?.Amount)
                {
                    throw new BO.ExceptionOutOfStock();
                }
            }
            catch (Exception ex)
            {
                throw new BO.ExceptionFromDal(ex);
            }
            if (orderItem != null)
                order1?.Items?.Add((DO.OrderItem)orderItem);
            else
                throw new BO.ExceptionNull();
        }
        else if (action == "Delete")
        {
            try
            {
                DO.OrderItem orderItem1 = new DO.OrderItem();
                orderItem1 = Dal?.OrderItem.GetByProductIDAndOrderID(Convert.ToInt32(orderItem?.ProductId), order1.ID) ?? throw new BO.ExceptionNull();
                if (order1.Status == BO.OrderStatus.ProvidedCustomerOrder)
                {
                    order1?.Items?.Remove(orderItem1);
                }
                else
                {
                    throw new BO.ExceptionOrderSent();
                }
            }
            catch (Exception ex)
            {
                throw new BO.ExceptionFromDal(ex);
            }
        }
        else
        {
            try
            {
                DO.Product product = new DO.Product();
                product = Dal?.Product.Get(Convert.ToInt32(orderItem?.ProductId)) ?? throw new BO.ExceptionNull();
                DO.OrderItem orderItem1 = Dal.OrderItem.GetByProductIDAndOrderID(Convert.ToInt32(orderItem?.ProductId), order1.ID);
                if (product.InStock >= (newAmount > orderItem1.Amount ? newAmount - orderItem1.Amount : orderItem1.Amount - newAmount))
                {
                    DO.OrderItem updateOrderItem = order1.Items != null ? order1.Items.Find(item => item.ID == orderItem1.ID) : throw new BO.ExceptionNull();
                    order1.Items.Remove(orderItem1);
                    updateOrderItem.Amount = newAmount;
                    updateOrderItem.Price = product.Price * newAmount;
                }
                else
                {
                    throw new BO.ExceptionOutOfStock();
                }
            }
            catch (Exception ex)
            {
                throw new BO.ExceptionFromDal(ex);
            }
        }
    }
    
    /// <summary>
    /// The function will sort all orders by status and return the oldest order ID,
    /// if it doesn't exist it will return NULL.
    /// </summary>
    /// <returns></returns>
    public int? GetOrderToSimulator()
    {
        IEnumerable<DO.Order>? orders = Dal?.Order.GetAll()
            .Where(order => order.DeliveryDate == DateTime.MinValue)
            .OrderBy(order => order.ShipDate != DateTime.MinValue ? order.ShipDate : order.OrderDate);
        return orders != null ? orders.First().ID : null;
    }
}
