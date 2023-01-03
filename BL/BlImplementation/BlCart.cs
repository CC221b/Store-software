using BlApi;
using System.Security.Cryptography;

namespace BlImplementation;


internal class BlCart : ICart
{
    private static DalApi.IDal Dal = DalApi.Factory.Get();

    /// <summary>
    /// A function that adds a product when first of all it uses the GET function and checks if the product exists.
    /// Then you go through a loop and check if it already exists,
    /// add 1 more to the quantity,
    /// and if it doesn't exist,
    /// move the product to the basket,
    /// in case of an error it throws an exception.
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    /// <exception cref="BO.ExceptionOutOfStock"></exception>
    public BO.Cart AddProduct(BO.Cart cart, int id)
    { 
        DO.Product product = new DO.Product();
        try
        {
            product = Dal.Product.Get(id);
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
        bool flag = false;
        if (product.InStock > 0)
        {
            foreach (var item in cart.Items)
            {
                if (item.ProductID == id)
                {
                    flag = true;

                    item.Amount += 1;
                    item.TotalPrice += product.Price;
                    cart.TotalPrice += product.Price;
                }
            }
        }
        else
        {
            throw new BO.ExceptionOutOfStock();
        }

        if (!flag)
        {
            if (product.InStock > 0)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ID = 0;
                orderItem.ProductID = id;
                orderItem.Name = product.Name;
                orderItem.Price = product.Price;
                orderItem.Amount = 1;
                orderItem.TotalPrice = product.Price;
                cart.TotalPrice = cart.TotalPrice + product.Price;
                cart.Items.Add(orderItem);
            }
            else
            {
                throw new BO.ExceptionOutOfStock();
            }
        }
        return cart;
    }

    /// <summary>
    /// The function updates the quantity of a product in the basket.
    /// Checks whether this is possible (is there enough in stock),
    /// and updates accordingly,
    /// in case of errors
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="id"></param>
    /// <param name="newAmount"></param>
    /// <returns></returns>
    /// <exception cref="BO.ExceptionOutOfStock"></exception>
    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int id, int newAmount)
    {
        DO.Product product = new DO.Product();
        try
        {
            product = Dal.Product.Get(id);
        }
        catch (Exception ex)
        {
            throw new BO.ExceptionFromDal(ex);
        }
        foreach (var item in cart.Items)
        {
            if (item.ProductID == id)
            {
                if (item.Amount < newAmount)
                {
                    if (product.InStock >= (newAmount - item.Amount))
                    {
                        cart.TotalPrice += item.Price * (newAmount - item.Amount);
                        item.Amount = newAmount;
                        item.TotalPrice = newAmount * item.Price;
                        break;
                    }
                    else
                    {
                        throw new BO.ExceptionOutOfStock();
                    }

                }
                else if (newAmount == 0)
                {
                    cart.TotalPrice -= item.Amount * item.Price;
                    cart.Items.Remove(item);
                    break;
                }
                else
                {
                    cart.TotalPrice -= product.Price * (item.Amount - newAmount);
                    item.Amount = newAmount;
                    item.TotalPrice = newAmount * item.Price;
                    break;
                }
            }
            else
            {
                throw new BO.ExceptionNotExists();
            }
        }
        return cart;
    }

    /// <summary>
    /// Auxiliary function for checking email integrity.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith("."))
        {
            return false; // suggested by @TK-421
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// A function that executes an order.
    /// Checks data integrity.
    /// If all data is correct,
    /// creates an order object (data entity),
    /// then creates entities of order items (data entity).
    /// throws exceptions accordingly.
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="customerName"></param>
    /// <param name="customerEmail"></param>
    /// <param name="customerAdress"></param>
    /// <exception cref="BO.ExceptionFromDal"></exception>
    /// <exception cref="BO.ExceptionInvalidData"></exception>
    public void MakeAnOrder(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
    {
        if (customerName != "" && customerAdress != "" && IsValidEmail(customerEmail))
        {
            foreach (var item in cart.Items)
            {
                DO.Product product = new DO.Product();
                try
                {
                    product = Dal.Product.Get(item.ProductID);
                }
                catch (Exception ex)
                {
                    throw new BO.ExceptionFromDal(ex);
                }
                if (product.InStock >= item.Amount && item.Amount > 0 && item.Price > 0)
                {
                    DO.Order order = new DO.Order();
                    order.CustomerAdress = customerAdress;
                    order.CustomerName = customerName;
                    order.CustomerEmail = customerEmail;
                    order.OrderDate = DateTime.Now;
                    order.ShipDate = DateTime.MinValue;
                    order.DeliveryDate = DateTime.MinValue;
                    int OrderID;
                    try
                    {
                        OrderID = Dal.Order.Add(order);
                    }
                    catch (Exception ex)
                    {
                        throw new BO.ExceptionFromDal(ex);
                    }
                    DO.OrderItem orderItem = new DO.OrderItem();
                    orderItem.OrderId = OrderID;
                    orderItem.ProductId = item.ProductID;
                    orderItem.Price = item.Price;
                    orderItem.Amount = item.Amount;
                    product.InStock -= item.Amount;
                    try
                    {
                        Dal.OrderItem.Add(orderItem);
                        Dal.Product.Update(product);
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
        else
        {
            throw new BO.ExceptionInvalidData();
        }
    }
}
