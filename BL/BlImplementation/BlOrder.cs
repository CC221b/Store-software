using BlApi;

namespace BlImplementation;

internal class BlOrder : IOrder
{
    DalApi.IDal Dal = new Dal.DalList();

    public IEnumerable<BO.OrderForList> GetListOrders()
    {
        IEnumerable<DO.Order> ListOrders = Dal.Order.GetAll();
        List<BO.OrderForList> ListOrderForList = new List<BO.OrderForList>();
        BO.OrderForList OrderForList = new BO.OrderForList();
        foreach (var item in ListOrders)
        {
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
