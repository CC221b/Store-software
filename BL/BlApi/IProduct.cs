using BO;
namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// Go to the data layer and ask for the list of products,
    /// and turn it into the structure you want to see in the ui.
    /// </summary>
    public IEnumerable<ProductForList> GetListProducts();
    /// <summary>
    /// Catalog request (product list) (for buyer's catalog screen).
    /// </summary>
    public IEnumerable<ProductItem> GetCatalog();
    /// <summary>
    /// Product details request.
    /// </summary>
    public Product GetProduct(int id);
    /// <summary>
    /// Adding a product (for admin screen).
    /// </summary>
    public void AddProduct(Product product);
    /// <summary>
    /// Product data update (for admin screen).
    /// </summary>
    public void UpdateProduct(Product product);
    /// <summary>
    /// Product deletion(for admin screen).
    /// </summary>
    public void DeleteProduct(int id);
}
