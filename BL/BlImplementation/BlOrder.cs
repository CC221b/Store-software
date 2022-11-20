using BlApi;
using BO;

namespace BlImplementation;

internal class BlOrder: IOrder
{
    public IEnumerable<OrderForList> GetListOrders()
    {
        LinkedList<OrderForList> listOrders = new LinkedList<OrderForList>();
        return listOrders;
    }
    public DO.Order GetListOrders(int id)
    {
        DO.Order order = new DO.Order();
        return order;
    }
    public Order UpdateOrderShipping(int id)
    {
        Order order = new Order();
        return order;
    }
    public Order UpdateOrderDelivery(int id)
    {
        Order order = new Order();
        return order;
    }
}
