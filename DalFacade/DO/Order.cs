
namespace DO;
public struct Order
{
    public int _id { get; set; }
    public string _customerName { get; set; }
    public string _customerEmail { get; set; }
    public string _customerAdress { get; set; }
    public DateTime _orderDate { get; set; }
    public DateTime _shipDate { get; set; }
    public DateTime _deliveryDate { get; set; }

    public override string ToString() => $@"
    Order ID: {_id}
    CustomerName: {_customerName}
    CustomerEmail: {_customerEmail}
    CustomerAdress: {_customerAdress}
    OrderDate: {_orderDate}
    ShipDate: {_shipDate}
    DeliveryDate: {_deliveryDate}";
}