using BlApi;
namespace BlImplementation;


internal class BlProduct : IProduct
{
    DalApi.IDal Dal = new Dal.DalList();
    public IEnumerable<BO.ProductForList> GetListProducts()
    {
        IEnumerable<DO.Product> ListProducts = Dal.Product.GetAll();
        List<BO.ProductForList> ListProductsForList = new List<BO.ProductForList>();
        BO.ProductForList productForList = new BO.ProductForList();
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
    public IEnumerable<BO.ProductItem> GetCatalog()
    {
        IEnumerable<DO.Product> ListProducts = Dal.Product.GetAll();
        List<BO.ProductItem> ListProductItem = new List<BO.ProductItem>();
        BO.ProductItem listProductItem = new BO.ProductItem();
        foreach (var item in ListProducts)
        {
            listProductItem.ID = item.ID;
            listProductItem.Name = item.Name;
            listProductItem.Price = item.Price;
            listProductItem.Category = (BO.Categories)item.Category;
            listProductItem.Amount = 0;
            listProductItem.InStock = item.InStock > 0 ? true : false;
            ListProductItem.Add(listProductItem);
        }
        return ListProductItem;
    }
    public BO.Product GetProduct(int id)
    {
        try
        {
            BO.Product productTypeBO = new BO.Product();
            if (id > 0)
            {
                DO.Product productTypeDO = new DO.Product();
                productTypeDO = Dal.Product.Get(id);
                productTypeBO.ID = productTypeDO.ID;
                productTypeBO.Name = productTypeDO.Name;
                productTypeBO.Price = productTypeDO.Price;
                productTypeBO.Category = (BO.Categories)productTypeDO.Category;
                productTypeBO.InStock = productTypeDO.InStock;
            }
            return productTypeBO;
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
    }
    public void AddProduct(BO.Product product)
    {
        try
        {
            if (product.ID > 0 && product.Name != "" && product.Price > 0 && product.InStock > 0)
            {
                DO.Product product1 = new DO.Product();
                product1.ID = product.ID;
                product1.Name = product.Name;
                product1.Price = product.Price;
                product1.InStock = product.InStock;
                product1.Category = (DO.Categories)product.Category;
                try
                {
                    Dal.Product.Add(product1);
                }
                catch (Exception ex)
                {
                    throw new BO.ExceptionFromDal(ex);
                }
            }
        }
        catch (Exception)
        {
            throw new BO.ExceptionInvalidData();
        }
    }
    public void DeleteProduct(int id)
    {
        IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetAll();
        foreach (DO.OrderItem orderItem in orderItems)
        {
            if (orderItem.ProductId == id)
            {
                throw new BO.ExceptionExistsInOrder();
            }
        }
        try
        {
            Dal.Product.Delete(id);
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
    }
    public void UpdateProduct(BO.Product product)
    {
        try
        {
            if (product.ID > 0 && product.Name != "" && product.Price > 0 && product.InStock > 0)
            {
                try
                {
                    DO.Product product1= new DO.Product();
                    product1 = Dal.Product.Get(product.ID);
                    product1.Name = product.Name;
                    product1.Price = product.Price;
                    product1.InStock = product.InStock;
                    product1.Category =(DO.Categories)product.Category;
                    Dal.Product.Update(product1);
                }
                catch (Exception ex)
                {
                    throw new BO.ExceptionFromDal(ex);
                }  
            }        
        }
        catch (Exception)
        {
            throw new BO.ExceptionInvalidData();
        }
    }
}
