using DalApi;
using DO;
using System;
using System.Linq;

namespace Dal;

internal class DalProduct : IProduct
{
    public Product Get(int id)
    {
        Product? product1 = (from product in DataSource.s_productList
                             where product.ID == id
                             select new Product
                             {
                                 ID = product.ID,
                                 Name = product.Name,
                                 InStock = product.InStock,
                                 Price = product.Price,
                                 Category = product.Category
                             }).FirstOrDefault();
        if (product1 != null)
        {
            return (Product)product1;
        }
        throw new ExceptionNotExists();
    }

    public Product Get(Predicate<Product> func)
    {
        return DataSource.s_productList.Find(func);
    }

    public int Add(DO.Product p)
    {
        try
        {
            Get(p.ID);
        }
        catch (Exception)
        {
            if (DataSource.s_productList.Count < 50)
                DataSource.s_productList.Add(p);
            else
                throw new ExceptionNoRoom();
            return p.ID;
        }
        throw new ExceptionExists();
    }

    public IEnumerable<Product> GetAll(Func<Product, bool>? func = null)
    {
        if (DataSource.s_orderList.Count == 0)
        {
            throw new ExceptionEmpty();
        }
        return (func == null) ? DataSource.s_productList.OrderBy(item => item.Name) : DataSource.s_productList.Where(func).OrderBy(item => item.Name);
    }

    public void Update(DO.Product p)
    {
        try
        {
            DO.Product product = Get(p.ID);
            int index = DataSource.s_productList.IndexOf(product);
            DataSource.s_productList[index] = p;
        }
        catch (Exception ex)
        {
            throw ex == null ? new ExceptionNullEx() : ex;
        }
    }

    public void Delete(int id)
    {
        try
        {
            DO.Product product = Get(id);
            DataSource.s_productList.Remove(product);
        }
        catch (Exception ex)
        {
            throw ex == null ? new ExceptionNullEx() : ex;
        }
    }
}

