using DO;

namespace DalApi;

public interface IOrderItem: ICrud<OrderItem>
{
    public List<OrderItem> GetByOrderID(int id);
    public OrderItem GetByProductIDAndOrderID(int productId, int orderId);
}
