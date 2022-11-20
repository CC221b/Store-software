
namespace BO;

public class OrderItem
{
    public int _id { get; set; }
    public int _productId { get; set; }
    public string _productName { get; set; }
    public double _price { get; set; }
    public double _productPrice { get; set; }
    public int _amountItemInCart { get; set; }

    public override string ToString() => $@"
    ID:{_id}
    Product ID: {_productId} 
    ProductName: {_productName}
    Price: {_price}
    ProductPrice: {_productPrice}
    AmountItemInCart: {_amountItemInCart}";
}
