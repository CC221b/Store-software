using BlApi;

namespace BlImplementation;

internal class BlOrder : IOrder
{
    DalApi.IDal Dal = new Dal.DalList();

    /// <summary>
    /// The function returns a list of orderForList objects.
    /// It fetches all the orders from the data layer and creates a list of orderForList objects and returns the list.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    public IEnumerable<BO.OrderForList> GetListOrders()
    {
        IEnumerable<DO.Order> ListOrders = new List<DO.Order>();
        try
        {
            ListOrders = Dal.Order.GetAll();
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
        List<BO.OrderForList> ListOrderForList = new List<BO.OrderForList>();
        foreach (var item in ListOrders)
        {
            BO.OrderForList OrderForList = new BO.OrderForList();
            OrderForList.ID = item.ID;
            OrderForList.CustomerName = item.CustomerName;
            OrderForList.Status = 0;
            IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetByOrderID(item.ID);
            OrderForList.AmountOfItems = orderItems.Count();
            double price = 0;
            foreach (var item1 in orderItems)
            {
                price += item1.Price;
            }
            OrderForList.TotalPrice = price;
            ListOrderForList.Add(OrderForList);
        }
        return ListOrderForList;
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
    public BO.Order GetOrder(int id)
    {
        if (id > 0)
        {
            try
            {
                DO.Order orderTypeDO = new DO.Order();
                BO.Order orderTypeBO = new BO.Order();
                orderTypeDO = Dal.Order.Get(id);
                List<DO.OrderItem> orderItem = new List<DO.OrderItem>();
                orderItem = Dal.OrderItem.GetByOrderID(orderTypeDO.ID);
                orderTypeBO.ID = orderTypeDO.ID;
                orderTypeBO.CustomerName = orderTypeDO.CustomerName;
                orderTypeBO.CustomerAdress = orderTypeDO.CustomerAdress;
                orderTypeBO.CustomerEmail = orderTypeDO.CustomerEmail;
                orderTypeBO.ShipDate = orderTypeDO.ShipDate;
                orderTypeBO.OrderDate = orderTypeDO.OrderDate;
                orderTypeBO.DeliveryDate = orderTypeDO.DeliveryDate;
                orderTypeBO.Status = 0;
                orderTypeBO.Items = orderItem;
                double sum = 0;
                foreach (var item in orderItem)
                {
                    sum += item.Price;
                }
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
            orderTypeDO = Dal.Order.Get(id);
            if (DateTime.Compare(orderTypeDO.ShipDate, DateTime.Now) > 0)
            {
                BO.Order orderTypeBO = new BO.Order();
                orderTypeDO.ShipDate = DateTime.Now;
                Dal.Order.Update(orderTypeDO);
                orderTypeBO = GetOrder(id);
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
            orderTypeDO = Dal.Order.Get(id);
            if (DateTime.Compare(orderTypeDO.DeliveryDate, DateTime.Now) > 0)
            {
                BO.Order orderTypeBO = new BO.Order();
                orderTypeDO.DeliveryDate = DateTime.Now;
                Dal.Order.Update(orderTypeDO);
                orderTypeBO = GetOrder(id);
                return orderTypeBO;
            }
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
        throw new BO.ExceptionNotExists();
    }
}
