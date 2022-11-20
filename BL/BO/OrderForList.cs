
namespace BO;
public class OrderForList
{
    public int _id { get; set; }
    public string _customerName { get; set; }
    public double _stateOrder { get; set; }
    public int _amountItem { get; set; }
    public int _totalPrice { get; set; }
    public override string ToString() => $@"
    ID:{_id}
    customerName: {_customerName} 
    stateOrder: {_stateOrder}
    amountItem: {_amountItem}
    totalPrice: {_totalPrice}";
}
