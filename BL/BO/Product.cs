
namespace BO;
public class Product
{
    public int _id { get; set; }
    public string _name { get; set; }
    public double _price { get; set; }
    public BO.Categories _category { get; set; }
    public int _inStock { get; set; }

    public override string ToString() => $@"
    ID: {_id}
    Product Name: {_name} 
    category: {_category}
    Price: {_price}
    Amount in stock: {_inStock}";
}
