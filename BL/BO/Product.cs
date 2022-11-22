
namespace BO;
public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public Categories Category { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
    ID: {ID}
    Product Name: {Name} 
    Price: {Price}
    category: {Category}
    Amount in stock: {InStock}";
}
