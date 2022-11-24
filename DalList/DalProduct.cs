﻿using DalApi;
using DO;
namespace Dal;

internal class DalProduct: IProduct
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

    public IEnumerable<Product> GetAll()
    {
        if (DataSource.s_productList.Count == 0)
        {
            throw new ExceptionEmpty();
        }
        else
        {
            DO.Product[] products = new DO.Product[DataSource.s_productList.Count];
            for (int i = 0; i < DataSource.s_productList.Count; i++)
            {
                products[i] = DataSource.s_productList[i];
            }
            return products;
        }
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

