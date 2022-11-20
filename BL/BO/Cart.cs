
using System.Security.Cryptography;

namespace BO;

public class Cart
{
    public string _customerName { get; set; }
    public string _customerEmail { get; set; }
    public string _customerAdress { get; set; }
    public OrderItem _orderItemList { get; set; }
    public int _price { get; set; }

    public override string ToString() => $@"
    customerName: {_customerName}
    customerEmail: {_customerEmail}
    customerAdress: {_customerAdress}
    orderItemList: {_orderItemList}
    price: {_price}";
}
