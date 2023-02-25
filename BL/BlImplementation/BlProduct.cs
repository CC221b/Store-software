using BlApi;
using BO;
namespace BlImplementation;

internal class BlProduct : IProduct
{
    private static DalApi.IDal? Dal = DalApi.Factory.Get();

    /// <summary>
    /// The function returns a list of type productForList.
    /// fetches the list of products from the data layer and creates a list of type productForList.
    /// throws errors accordingly.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    public IEnumerable<BO.ProductForList> GetAll(Func<DO.Product, bool>? func = null)
    {
        lock (Dal ?? throw new BO.ExceptionNull())
        {
            try
            {
                return (from item in Dal?.Product.GetAll(func)
                        select new BO.ProductForList()
                        {
                            ID = item.ID,
                            Name = item.Name,
                            Price = item.Price,
                            Category = item.Category != null ? (BO.Categories)item.Category : null
                        });
            }
            catch (Exception ex)
            {
                throw new BO.ExceptionFromDal(ex);
            }
        }
    }

    /// <summary>
    /// The function returns a list of productItem type.
    /// Brings the list of products from the data layer and creates a list of type productItem.
    /// throws errors accordingly.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    public IEnumerable<BO.ProductItem> GetCatalog(Func<DO.Product, bool>? func = null)
    {
        lock (Dal ?? throw new BO.ExceptionNull())
        {
            try
            {
                return (from item in Dal?.Product.GetAll(func)
                        select new BO.ProductItem()
                        {
                            ID = item.ID,
                            Name = item.Name,
                            Price = item.Price,
                            Category = item.Category != null ? (BO.Categories)item.Category : null,
                            Amount = 0,
                            InStock = item.InStock > 0 ? true : false
                        });
            }
            catch (Exception ex)
            {
                throw new BO.ExceptionFromDal(ex);
            }
        }
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
    public BO.Product Get(int id)
    {
        lock (Dal ?? throw new BO.ExceptionNull())
        {
            BO.Product productTypeBO = new BO.Product();
            if (id > 0)
            {
                try
                {
                    DO.Product productTypeDO = new DO.Product();
                    productTypeDO = Dal?.Product.Get(id) ?? throw new BO.ExceptionNull();
                    productTypeBO.ID = productTypeDO.ID;
                    productTypeBO.Name = productTypeDO.Name;
                    productTypeBO.Price = productTypeDO.Price;
                    productTypeBO.Category = productTypeDO.Category != null ? (BO.Categories)productTypeDO.Category : null;
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
    }

    public ProductItem Get(int id, Cart cart)
    {
        lock (Dal ?? throw new BO.ExceptionNull())
        {
            ProductItem productItem = new ProductItem();
            if (id > 0)
            {
                try
                {
                    DO.Product product = Dal?.Product.Get(id) ?? throw new ExceptionNull();
                    productItem.ID = product.ID;
                    productItem.Name = product.Name;
                    productItem.Price = product.Price;
                    productItem.Category = product.Category != null ? (BO.Categories)product.Category : null;
                    cart?.Items?.Where(item => item?.ID == product.ID)
                        .Select(item => productItem.Amount = item?.Amount ?? throw new ExceptionNull());
                    productItem.InStock = product.InStock > 0 ? true : false;
                    return productItem;
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
    }

    /// <summary>
    /// The function receives a product of the logical entity type,
    /// converts it to an entity of the data layer type and tries to insert the product into the list.
    /// throws exceptions accordingly.
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    /// <exception cref="BO.ExceptionInvalidID"></exception>
    public void Add(BO.Product product)
    {
        lock (Dal ?? throw new BO.ExceptionNull())
        {
            if (product.ID > 0 && product.Name != "" && product.Price > 0 && product.InStock > 0)
            {
                DO.Product product1 = new DO.Product();
                product1.ID = product.ID;
                product1.Name = product.Name;
                product1.Price = product.Price;
                product1.InStock = product.InStock;
                product1.Category = product.Category != null ? (DO.Categories)product.Category : null;
                try
                {
                    Dal?.Product.Add(product1);
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
    public void Delete(int id)
    {
        lock (Dal ?? throw new BO.ExceptionNull())
        {
            IEnumerable<DO.OrderItem> orderItems = Dal?.OrderItem.GetAll() ?? throw new ExceptionNull();
            var orderItems1 = from orderItem in orderItems
                              let ProductId = orderItem.ProductId
                              where ProductId == id
                              select orderItem;
            if (orderItems1 != null)
            {
                throw new BO.ExceptionExists();
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
    public void Update(BO.Product product)
    {
        lock (Dal ?? throw new BO.ExceptionNull())
        {
            try
            {
                if (product.ID > 0 && product.Name != "" && product.Price >= 0 && product.InStock >= 0)
                {
                    try
                    {
                        DO.Product product1 = new DO.Product();
                        product1 = Dal?.Product.Get(product.ID) ?? throw new ExceptionNull();
                        product1.Name = product.Name;
                        product1.Price = product.Price;
                        product1.InStock = product.InStock;
                        product1.Category = product.Category != null ? (DO.Categories)product.Category : null;
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
}
