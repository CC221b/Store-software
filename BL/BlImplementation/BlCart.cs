using BlApi;
using BO;

namespace BlImplementation;

internal class BlCart: ICart
{
    public Cart AddProduct(Cart cart, int id)
    {
        return cart;
    }
    public Cart UpdateAmountOfProduct(Cart cart, int id, int newAmount) 
    {
        return cart;
    }
    public void MakeAnOrder(Cart cart, string customerName, string customerEmail, string customerAdress) 
    {
    }
}
