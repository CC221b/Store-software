
using System.Security.Cryptography;

namespace BO;

public class Cart
{
    public string _customerName { get; set; }
    public string _customerEmail { get; set; }
    public string _customerAdress { get; set; }
    public OrderItem _Items { get; set; }
    public int _totalPrice { get; set; }

    public override string ToString() => $@"
    customerName: {_customerName}
    customerEmail: {_customerEmail}
    customerAdress: {_customerAdress}
    orderItemList: {_Items}
    price: {_totalPrice}";
}
