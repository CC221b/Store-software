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
    /// <summary>
    /// Order tracking (admin order management screen).
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public OrderTracking GetOrderTracking(int id);
    /// <summary>
    /// BONUS
    /// will allow adding / downloading / changing the quantity of a product ordered by the manager
    /// </summary>
    /// <param name="order"></param>
    public void UpdateOrder(Order order);
}
