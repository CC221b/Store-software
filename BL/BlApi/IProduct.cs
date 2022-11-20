using BO;
namespace BlApi;

public interface IProduct
{
    public IEnumerable<ProductForList> GetListProducts();
    public IEnumerable<ProductItem> GetCatalog();
    public Product GetProduct(int id);
    public void AddProduct(Product product);
    public void UpdateProduct(Product product);
    public void DeleteProduct(int id);
}
