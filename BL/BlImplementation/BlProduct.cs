using BlApi;
using BO;
namespace BlImplementation;


internal class BlProduct : IProduct
{
    DalApi.IDal IDal = new Dal.DalList();
    public IEnumerable<ProductForList> GetListProducts()
    {
        IEnumerable<DO.Product> ListProducts = IDal.Product.GetAll();
        List<ProductForList> ListProductsForList = new List<ProductForList>();
        ProductForList productForList = new ProductForList();
        foreach (var item in ListProducts)
        {
            productForList.ID = item.ID;
            productForList.Name = item.Name;
            productForList.Price = item.Price;
            productForList.Category = (BO.Categories)item.Category;
            ListProductsForList.Add(productForList);
        }
        return ListProductsForList;
    }
    public IEnumerable<ProductItem> GetCatalog()
    {
        IEnumerable<DO.Product> ListProducts = IDal.Product.GetAll();
        List<ProductItem> ListProductItem = new List<ProductItem>();
        ProductItem listProductItem = new ProductItem();
        foreach (var item in ListProducts)
        {
            listProductItem.ID = item.ID;
            listProductItem.Name = item.Name;
            listProductItem.Price = item.Price;
            listProductItem.Category = (BO.Categories)item.Category;
            listProductItem.Amount = 0;
            listProductItem.InStock = item.InStock > 0 ? true : false ;
            ListProductItem.Add(listProductItem);
        }
        return ListProductItem;
    }
    public Product GetProduct(int id)
    { 
        try
        {
            Product productTypeBO = new Product();
            if (id > 0)
            {
                DO.Product productTypeDO = new DO.Product();
                productTypeDO = IDal.Product.Get(id);
                productTypeBO.ID = productTypeDO.ID;
                productTypeBO.Name = productTypeDO.Name;
                productTypeBO.Price = productTypeDO.Price;
                productTypeBO.Category = (BO.Categories)productTypeDO.Category;
                productTypeBO.InStock = productTypeDO.InStock;
            }
            return productTypeBO;
        }
        catch (Exception)
        {
            throw new Exception();
        }
    }
    public void AddProduct(Product product)
    {
        if (true)
        {

        }
    }
    public void UpdateProduct(Product product)
    {

    }
    public void DeleteProduct(int id)
    {

    }
}
