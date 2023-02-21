using BO;
namespace BlApi;

public interface IOrder
{
    /// <summary>
    /// Order list request (admin screen).
    /// </summary>
    public IEnumerable<OrderForList> GetAll(Func<DO.Order, bool>? func = null);
    /// <summary>
    /// Order details request (for manager screen and buyer screen).
    /// </summary>
    public Order Get(int id);
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
    public void Update(BO.Order order, string action, DO.OrderItem? orderItem = null, int newAmount = 0);
    /// <summary>
    /// This function is intended to tell the Threads which order to update
    /// in order to create a simulator of the system's behavior.
    /// </summary>
    public int? GetOrderToSimulator();
}
