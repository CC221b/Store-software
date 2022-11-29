
namespace DO;

public struct Product
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public DO.Categories? Category { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
    ID: {ID}
    Product Name: {Name} 
    category: {Category}
    Price: {Price}
    Amount in stock: {InStock}";
}
