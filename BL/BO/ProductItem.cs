
namespace BO;
public class ProductItem
{
    public int _id { get; set; }
    public string _productName { get; set; }
    public double _productPrice { get; set; }
    public BO.Categories _category { get; set; }
    public bool _inStock { get; set; }
    public double _amountProductInCart { get; set; }

    public override string ToString() => $@"
    id: {_id}
    productName: {_productName}
    productPrice: {_productPrice}
    category: {_category}
    inStock: {_inStock}
    amountProductInCart: {_amountProductInCart}";
}
