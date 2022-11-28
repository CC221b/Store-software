using BlApi;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;

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
    public void UpdateOrder(BO.Order order)
    {
        try
        {
            BO.Order order1 = GetOrder(order.ID);
            Console.WriteLine("enter 0 to addProduct" +
                "\nenter 1 to deleteProduct" +
                "\nenter 2 to UpdateAmountOfProduct");
            int choose = Convert.ToInt32(Console.ReadLine());
            int productID, orderID;
            switch (choose)
            {
                case 0:
                    DO.OrderItem orderItem = new DO.OrderItem();
                    Console.WriteLine("Write ProductId, OrderId, Price, Amount");
                    orderItem.ProductId = Convert.ToInt32(Console.ReadLine());
                    orderItem.OrderId = Convert.ToInt32(Console.ReadLine());
                    orderItem.Price = Convert.ToInt32(Console.ReadLine());
                    orderItem.Amount = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        DO.Product product = Dal.Product.Get(orderItem.ProductId);
                        if (product.InStock <= orderItem.Amount)
                        {
                            throw new BO.ExceptionOutOfStock();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new BO.ExceptionFromDal(ex);
                    }
                    order1.Items.Add(orderItem);
                    break;
                case 1:
                    Console.WriteLine("enter productID and orderID to delete:");
                    productID = Convert.ToInt32(Console.ReadLine());
                    orderID = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        DO.OrderItem orderItem1 = Dal.OrderItem.GetByProductIDAndOrderID(productID, orderID);
                        BO.Order checkOrderStatus = GetOrder(orderID);
                        if (checkOrderStatus.Status == 0)
                        {
                            order1.Items.Remove(orderItem1);
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
                    break;
                case 2:
                    Console.WriteLine("enter productID and orderID and newAmount to update:");
                    productID = Convert.ToInt32(Console.ReadLine());
                    orderID = Convert.ToInt32(Console.ReadLine());
                    int newAmount = Convert.ToInt32(Console.ReadLine());
                    try
                    {
                        DO.Product product = Dal.Product.Get(productID);
                        DO.OrderItem orderItem1 = Dal.OrderItem.GetByProductIDAndOrderID(productID, orderID);
                        if (product.InStock >= (newAmount > orderItem1.Amount ? newAmount - orderItem1.Amount : orderItem1.Amount - newAmount))
                        {
                            DO.OrderItem updateOrderItem = order1.Items.Find(item => item.ID == orderItem1.ID);
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
                    break;
                default:
                    break;
            }
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }
}
