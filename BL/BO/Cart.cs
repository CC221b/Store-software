namespace BO;

public class Cart
{
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public List<OrderItem?>? Items { get; set; }
    public double TotalPrice { get; set; }

    public override string ToString()
    {
        string toString = "customerName:" + CustomerName + "\ncustomerEmail:" + CustomerEmail + "\ncustomerAdress:" + CustomerAdress + "\nlistOrderItems:";
        for (int i = 0; i < Items?.Count; i++)
        {
            toString += "\nItem" + (i + 1) + ": " + Items[i] + "\n";
        }
        toString += "cartPrice:" + TotalPrice;
        return toString;
    }
}
