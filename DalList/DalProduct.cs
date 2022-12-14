using DalApi;
using DO;
using System;
using System.Linq;

namespace Dal;

internal class DalProduct : IProduct
{
    public Product Get(int id)
    {
        for (int i = 0; i < DataSource.s_productList.Count; i++)
        {
            if (DataSource.s_productList[i].ID == id)
            {
                return DataSource.s_productList[i];
            }
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
        return (func == null) ? DataSource.s_productList : DataSource.s_productList.Where(func);
    }

    public void Update(DO.Product p)
    {
        for (int i = 0; i < DataSource.s_productList.Count; i++)
        {
            if (DataSource.s_productList[i].ID == p.ID)
            {
                DataSource.s_productList[i] = p;
                return;
            }
        }
        throw new ExceptionNotExists();
    }

    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.s_productList.Count; i++)
        {
            if (DataSource.s_productList[i].ID == id)
            {
                DO.Product p = new DO.Product();
                DataSource.s_productList.Remove(DataSource.s_productList[i]);
                return;
            }
        }
        throw new ExceptionNotExists();
    }

}

