using BlApi;

namespace BlImplementation;


internal class BlCart : ICart
{
    DalApi.IDal Dal = new Dal.DalList();

    public BO.Cart AddProduct(BO.Cart cart, int id)
    {
        DO.Product product = new DO.Product();
        product = Dal.Product.Get(id);
        bool flag = false;
        foreach (var item in cart.Items)
        {
            if (item.ProductID == id)
            {
                flag = true;
                if (product.InStock > 0)
                {
                    item.Amount += 1;
                    item.TotalPrice += product.Price;
                    cart.TotalPrice += product.Price;
                }
                else
                {
                    throw new BO.ExceptionOutOfStock();
                }
            }
        }
        if (flag)
        {
            if (product.InStock > 0)
            {
                BO.OrderItem orderItem = new BO.OrderItem();
                orderItem.ID = 0;
                orderItem.ProductID = id;
                orderItem.Price = product.Price;
                orderItem.Amount = 1;
                orderItem.TotalPrice += product.Price;
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

    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int id, int newAmount)
    {
        DO.Product product = new DO.Product();
        product = Dal.Product.Get(id);
        foreach (var item in cart.Items)
        {
            if (item.ProductID == id)
            {
                if (item.Amount < newAmount)
                {
                    if (product.InStock > (newAmount - item.Amount))
                    {
                        item.Amount = newAmount;
                        item.TotalPrice = newAmount * item.Price;
                        cart.TotalPrice += item.Price * (newAmount - item.Amount);
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
                }
                else
                {
                    cart.TotalPrice -= product.Price * (item.Amount - newAmount);
                    item.Amount = newAmount;
                    item.TotalPrice = newAmount * item.Price;
                }
            }
        }
        return cart;
    }

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

    public void MakeAnOrder(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
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
            if (product.InStock >= item.Amount || item.Amount > 0 || item.Price > 0 || customerName != "" || customerAdress != "" || IsValidEmail(customerEmail))
            {
                DO.Order order = new DO.Order();
                order.CustomerAdress = customerAdress;
                order.CustomerName = customerName;
                order.CustomerEmail = customerEmail;
                order.OrderDate = DateTime.Now;
                order.ShipDate = new DateTime(0, 0, 0);
                order.DeliveryDate = new DateTime(0, 0, 0);
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
}
