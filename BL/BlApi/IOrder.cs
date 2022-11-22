using BO;
namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// Order list request (admin screen).
    /// </summary>
    public IEnumerable<OrderForList> GetListOrders();
    /// <summary>
    /// Order details request (for manager screen and buyer screen).
    /// </summary>
    public Order GetOrder(int id);
    /// <summary>
    /// Order shipping update (admin order management screen).
    /// </summary>
    public Order UpdateOrderShipping(int id);
    /// <summary>
    /// Order Delivery Update (Manager Order Management Screen).
    /// </summary>
    public Order UpdateOrderDelivery(int id); 
}
