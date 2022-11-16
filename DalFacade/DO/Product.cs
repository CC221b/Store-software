
namespace DO;

public struct Product
{
    public int _id { get; set; }
    public string _name { get; set; }
    public double _price { get; set; }
    public DO.Categories _category { get; set; }
    public int _inStock { get; set; }

    public override string ToString() => $@"
    ID: {_id}
    Product Name: {_name} 
    category: {_category}
    Price: {_price}
    Amount in stock: {_inStock}";
}
