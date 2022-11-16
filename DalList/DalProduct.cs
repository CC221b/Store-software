namespace Dal;

public class DalProduct
{
    public DO.Product Read(int id)
    {
        for (int i = 0; i < DataSource.Config.s_indexProduct; i++)
        {
            if (DataSource.s_productArr[i]._id == id)
            {
                return DataSource.s_productArr[i];
            }
        }
        throw new Exception("Sorry, no product was found matching the product_ID number.");
    }

    public int Create(DO.Product p)
    {
        try
        {
            Read(p._id);
        }
        catch (Exception)
        {
            if (DataSource.Config.s_indexProduct < DataSource.s_productArr.Length)
                DataSource.s_productArr[DataSource.Config.s_indexProduct++] = p;
            else
                throw new Exception("Sorry, there is no more room to enter a new product.");
            return p._id;
        }
        throw new Exception("Sorry, there is already a product with this ID number.");
    }

    public DO.Product[] ReadAll()
    {
        if (DataSource.Config.s_indexProduct == 0)
        {
            throw new Exception("Sorry, there are currently no products in the store.");
        }
        else
        {
            DO.Product[] products = new DO.Product[DataSource.Config.s_indexProduct];
            for (int i = 0; i < DataSource.Config.s_indexProduct; i++)
            {
                products[i] = DataSource.s_productArr[i];
            }
            return products;
        }
    }

    public void Update(DO.Product p)
    {
        for (int i = 0; i < DataSource.Config.s_indexProduct; i++)
        {
            if (DataSource.s_productArr[i]._id == p._id)
            {
                DataSource.s_productArr[i] = p;
                return;
            }
        }
        throw new Exception("Sorry, no product with the required ID number exists.");
    }

    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.Config.s_indexProduct; i++)
        {
            if (DataSource.s_productArr[i]._id == id)
            {
                DO.Product p = new DO.Product();
                DataSource.s_productArr[i] = DataSource.s_productArr[DataSource.Config.s_indexProduct];
                DataSource.s_productArr[DataSource.Config.s_indexProduct] = p;
                DataSource.Config.s_indexProduct--;
                return;
            }
        }
        throw new Exception("Sorry, no product with the required ID number exists.");
    }

}

