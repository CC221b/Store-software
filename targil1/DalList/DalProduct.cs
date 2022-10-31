
namespace Dal;

public class DalProduct
{
    public DO.Product Read(int ID)
    {
        for (int i = 0; i < DataSource.Config.index_Product; i++)
        {
            if (DataSource.Product_arr[i].ID == ID)
            {
                return DataSource.Product_arr[i];
            }
        }
        throw new Exception("Sorry, no product was found matching the product_ID number.");
    }

    public int Create(DO.Product p)
    {

        try
        {
            Read(p.ID);
        }
        catch (Exception)
        {
            if (DataSource.Config.index_Product < DataSource.Product_arr.Length)
                DataSource.Product_arr[DataSource.Config.index_Product++] = p;
            else
                throw new Exception("Sorry, there is no more room to enter a new product.");
            return p.ID;
        }
        throw new Exception("Sorry, there is already a product with this ID number.");
    }

    public DO.Product[] ReadAll()
    {
        if (DataSource.Config.index_Product == 0)
        {
            throw new Exception("Sorry, there are currently no products in the store.");
        }
        else
        {
            DO.Product[] products = new DO.Product[DataSource.Config.index_Product];
            for (int i = 0; i < DataSource.Config.index_Product; i++)
            {
                products[i] = DataSource.Product_arr[i];
            }
            return products;
        }
    }

    public void Update(DO.Product p)
    {
        for (int i = 0; i < DataSource.Config.index_Product; i++)
        {
            if (DataSource.Product_arr[i].ID == p.ID)
            {
                DataSource.Product_arr[i] = p;
                return;
            }
        }
        throw new Exception("Sorry, no product with the required ID number exists.");
    }

    public void Delete(int ID)
    {
        for (int i = 0; i < DataSource.Config.index_Product; i++)
        {
            if (DataSource.Product_arr[i].ID == ID)
            {
                DO.Product p = new DO.Product();
                DataSource.Product_arr[i] = DataSource.Product_arr[DataSource.Config.index_Product];
                DataSource.Product_arr[DataSource.Config.index_Product] = p;
                DataSource.Config.index_Product--;
                return;
            }
        }
        throw new Exception("Sorry, no product with the required ID number exists.");
    }

}

