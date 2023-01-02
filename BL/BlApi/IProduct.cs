using BO;
namespace BlApi;

public interface IProduct
{
    /// <summary>
    /// Go to the data layer and ask for the list of products,
    /// and turn it into the structure you want to see in the ui.
    /// </summary>
    public IEnumerable<ProductForList> GetAll(Func<DO.Product, bool>? func = null);
    /// <summary>
    /// Catalog request(product list) (for buyer's catalog screen).
    /// </summary>
    public IEnumerable<ProductItem> GetCatalog();
    ///// <summary>
    ///// Product details request.
    ///// </summary>
    public Product Get(int id);
    /// <summary>
    /// Adding a product (for admin screen).
    /// </summary>
    public ProductItem Get(int id, Cart cart);
    /// <summary>
    /// Adding a product (for admin screen).
    /// </summary>
    public void Add(Product product);
    /// <summary>
    /// Product data update (for admin screen).
    /// </summary>
    public void Update(Product product);
    /// <summary>
    /// Product deletion(for admin screen).
    /// </summary>
    public void Delete(int id);
    /// <summary>
}
