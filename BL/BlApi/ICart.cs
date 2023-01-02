using BO;
using DO;

namespace BlApi;

public interface ICart
{
    static BO.Cart cart = new BO.Cart();
    /// <summary>
    /// Adding a product to the shopping cart (for catalog screen, product details screen)
    /// </summary>
    public Cart AddProduct(Cart cart, int id);
    /// <summary>
    /// Updating the amount of a product in the shopping cart (for the shopping cart screen).
    /// </summary>
    public Cart UpdateAmountOfProduct(Cart cart, int id, int newAmount);
    /// <summary>
    /// Confirm basket for order \ placing an order (for shopping basket screen or order completion screen).
    /// </summary>
    public void MakeAnOrder (Cart cart, string customerName, string customerEmail, string customerAdress);


}
