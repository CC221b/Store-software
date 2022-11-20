using BlApi;
using BO;

namespace BlImplementation;

internal class BlProduct: IProduct
{
    public IEnumerable<ProductForList> GetListProducts()
    {
        return new List<ProductForList>();
    }
    public IEnumerable<ProductItem> GetCatalog()
    {
        return new List<ProductItem>();
    }
    public Product GetProduct(int id)
    {
        return new Product();
    }
    public void AddProduct(Product product)
    {

    }
    public void UpdateProduct(Product product)
    {

    }
    public void DeleteProduct(int id)
    {

    }
}
