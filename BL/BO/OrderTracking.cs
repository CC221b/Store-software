
namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    public OrderStatus Status { get; set; }
    public List<(DateTime,string)> _dateAndStatus { get; set; }

    public override string ToString() => $@"
    ID:{ID}
    stateOrder: {Status} 
    dateAndStatus: {_dateAndStatus}";
}
