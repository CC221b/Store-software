
namespace BO;
public class ProductForList
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Categories Category { get; set; }

    public override string ToString() => $@"
    ID:{ID}
    productName: {Name}
    productPrice: {Price}
    category: {Category}";
}
