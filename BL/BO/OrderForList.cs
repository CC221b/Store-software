
namespace BO;
public class OrderForList
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public OrderStatus Status { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString() => $@"
    ID:{ID}
    customerName: {CustomerName} 
    stateOrder: {Status}
    amountItem: {AmountOfItems}
    totalPrice: {TotalPrice}";
}
