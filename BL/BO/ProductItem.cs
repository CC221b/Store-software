namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public Categories? Category { get; set; }
    public double Amount { get; set; }
    public bool InStock { get; set; }

    public override string ToString() => $@"
    id: {ID}
    productName: {Name}
    productPrice: {Price}
    category: {Category}
    inStock: {InStock}
    amountProductInCart: {Amount}";
}
