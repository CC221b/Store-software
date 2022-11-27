using BlApi;
namespace BlImplementation;


internal class BlProduct : IProduct
{
    DalApi.IDal Dal = new Dal.DalList();

    /// <summary>
    /// The function returns a list of type productForList.
    /// fetches the list of products from the data layer and creates a list of type productForList.
    /// throws errors accordingly.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    public IEnumerable<BO.ProductForList> GetListProducts()
    {
        IEnumerable<DO.Product> ListProducts = new List<DO.Product>();
        try
        {
            ListProducts = Dal.Product.GetAll();
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
        List<BO.ProductForList> ListProductsForList = new List<BO.ProductForList>();
        foreach (var item in ListProducts)
        {
            BO.ProductForList productForList = new BO.ProductForList();
            productForList.ID = item.ID;
            productForList.Name = item.Name;
            productForList.Price = item.Price;
            productForList.Category = (BO.Categories)item.Category;
            ListProductsForList.Add(productForList);
        }
        return ListProductsForList;
    }

    /// <summary>
    /// The function returns a list of productItem type.
    /// Brings the list of products from the data layer and creates a list of type productItem.
    /// throws errors accordingly.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    public IEnumerable<BO.ProductItem> GetCatalog()
    {
        IEnumerable<DO.Product> ListProducts = new List<DO.Product>();
        try
        {
            ListProducts = Dal.Product.GetAll();
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
        List<BO.ProductItem> ListProductItem = new List<BO.ProductItem>();
        foreach (var item in ListProducts)
        {
            BO.ProductItem listProductItem = new BO.ProductItem();
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

    /// <summary>
    /// The function receives an ID,
    /// fetches the product from the data layer and converts to a logical entity.
    /// Returns the logical entity.
    /// throws exceptions accordingly.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    /// <exception cref="BO.ExceptionInvalidID"></exception>
    public BO.Product GetProduct(int id)
    {
        BO.Product productTypeBO = new BO.Product();
        if (id > 0)
        {
            try
            {
                DO.Product productTypeDO = new DO.Product();
                productTypeDO = Dal.Product.Get(id);
                productTypeBO.ID = productTypeDO.ID;
                productTypeBO.Name = productTypeDO.Name;
                productTypeBO.Price = productTypeDO.Price;
                productTypeBO.Category = (BO.Categories)productTypeDO.Category;
                productTypeBO.InStock = productTypeDO.InStock;
                return productTypeBO;
            }
            catch (Exception ex)
            {
                throw new BO.ExceptionFromDal(ex);
            }
        }
        throw new BO.ExceptionInvalidID();
    }

    /// <summary>
    /// The function receives a product of the logical entity type,
    /// converts it to an entity of the data layer type and tries to insert the product into the list.
    /// throws exceptions accordingly.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    /// <exception cref="BO.ExceptionInvalidID"></exception>
    public void AddProduct(BO.Product product)
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
        else
        {
            throw new BO.ExceptionInvalidID();
        }

    }

    /// <summary>
    /// The function receives an ID,
    /// looks for the product in a list of products it pulled from the data layer,
    /// and tries to delete the product.
    /// Throws exceptions accordingly.
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="BO.ExceptionExists"></exception>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    public void DeleteProduct(int id)
    {
        IEnumerable<DO.OrderItem> orderItems = Dal.OrderItem.GetAll();
        foreach (DO.OrderItem orderItem in orderItems)
        {
            if (orderItem.ProductId == id)
            {
                throw new BO.ExceptionExists();
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

    /// <summary>
    /// The function receives an updated product,
    /// looks for the product in a list of products it pulled from the data layer according to the identifier,
    /// and tries to update the product.
    /// Throws exceptions accordingly.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    /// <exception cref="BO.ExceptionInvalidData"></exception>
    public void UpdateProduct(BO.Product product)
    {
        try
        {
            if (product.ID > 0 && product.Name != "" && product.Price >= 0 && product.InStock >= 0)
            {
                try
                {
                    DO.Product product1 = new DO.Product();
                    product1 = Dal.Product.Get(product.ID);
                    product1.Name = product.Name;
                    product1.Price = product.Price;
                    product1.InStock = product.InStock;
                    product1.Category = (DO.Categories)product.Category;
                    Dal.Product.Update(product1);
                }
                catch (Exception ex)
                {
                    throw new BO.ExceptionFromDal(ex);
                }
            }
            else
            {
                throw new BO.ExceptionInvalidData();
            }
        }
        catch (Exception)
        {
            throw new BO.ExceptionInvalidData();
        }
    }
}
