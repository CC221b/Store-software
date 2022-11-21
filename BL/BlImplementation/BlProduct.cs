using BlApi;
using BO;
namespace BlImplementation;

 
internal class BlProduct: IProduct
{
    DalApi.IDal IDal = new Dal.DalList();
    public IEnumerable<ProductForList> GetListProducts()
    {
        IEnumerable<DO.Product> ListProducts = IDal.Product.GetAll();
        IEnumerable<BO.ProductForList> ListProductsForList = new List<BO.ProductForList>();
        foreach (var item in collection)
        {

        }
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
