namespace BO;

public class Order
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime ShipDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public OrderItem Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    Order ID: {ID}
    CustomerName: {CustomerName}
    CustomerEmail: {CustomerEmail}
    CustomerAdress: {CustomerAdress}
    OrderDate: {OrderDate}
    Status: {Status}
    ShipDate: {ShipDate}
    DeliveryDate: {DeliveryDate}
    TotalPrice: {TotalPrice}";
}
