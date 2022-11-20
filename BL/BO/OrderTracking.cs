
namespace BO;

public class OrderTracking
{
    public int _id { get; set; }
    public double _stateOrder { get; set; }
    public List<(DateTime,string)> _dateAndStatus { get; set; }

    public override string ToString() => $@"
    ID:{_id}
    stateOrder: {_stateOrder} 
    dateAndStatus: {_dateAndStatus}";
}
