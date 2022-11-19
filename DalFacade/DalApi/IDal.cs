
namespace DalApi;

public interface IDal
{
    public IOrder Order { get; }
    public IProduct Product { get; }
    public IOrderItem OrderItem { get; }
}
