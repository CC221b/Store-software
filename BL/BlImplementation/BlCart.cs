using BlApi;

namespace BlImplementation;

internal class BlCart : ICart
{
    DalApi.IDal Dal = new Dal.DalList();

    public BO.Cart AddProduct(BO.Cart cart, int id)
    {
        // ----------------------------
        //צריכה לגמור את הפונקציה הזו!!!!!!
        // ----------------------------
        foreach (var item in cart.Items)
        {
            if (item.ProductID == id)
            {
                throw new BO.ExceptionExists();
            }
        }
        try
        {
            DO.Product product = new DO.Product();
            product = Dal.Product.Get(id);
            if (product.InStock > 0)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ID = 0;
                orderItem.ProductID = id;
                orderItem.Price = product.Price;
                orderItem.Amount = 1;
                orderItem.TotalPrice = cart.TotalPrice + product.Price;
                cart.Items.Add(orderItem);
            }
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }

        return cart;
    }
    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int id, int newAmount)
    {
        return cart;
    }
    public void MakeAnOrder(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
    {
    }
}
