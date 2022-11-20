using BO;
using DO;

namespace BlApi;

public interface ICart
{
    public Cart AddProduct(Cart cart, int id);
    public Cart UpdateAmountOfProduct(Cart cart, int id, int newAmount);
    public void MakeAnOrder (Cart cart, string customerName, string customerEmail, string customerAdress);


}
