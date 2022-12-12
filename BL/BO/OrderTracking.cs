namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus? Status { get; set; }
    public Dictionary<DateTime,string> DateAndStatus { get; set; }

    public override string ToString() => $@"
    ID:{ID}
    stateOrder: {Status} 
    dateAndStatus: {DateAndStatus}";
}
