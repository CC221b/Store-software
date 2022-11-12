
namespace DO;

public struct OrderItem
{
    public int _id { get; set; }
    public int _productId { get; set; }
    public int _orderId { get; set; }
    public double _price { get; set; }
    public int _amount { get; set; }

    public override string ToString() => $@"
    ID:{_id}
    Product ID: {_productId} 
    OrderId: {_orderId}
    Price: {_price}
    Amount: {_amount}";
}

