
namespace BO;
public class ProductForList
{
    public int _id { get; set; }
    public string _productName { get; set; }
    public double _productPrice { get; set; }
    public BO.Categories _category { get; set; }
    public override string ToString() => $@"
    ID:{_id}
    productName: {_productName}
    productPrice: {_productPrice}
    category: {_category}";
}
