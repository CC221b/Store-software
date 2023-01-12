using DalApi;

namespace Dal;

internal sealed class DalXml : IDal
{
    //
    public static IDal Instance { get; } = new DalXml();
    //
    public IProduct Product { get; } = new Dal.Product();
    public IOrder Order { get; } = new Dal.Order();
    public IOrderItem OrderItem { get; } = new Dal.OrderItem();

}
