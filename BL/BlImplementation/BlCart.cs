using BlApi;

namespace BlImplementation;


//להעתיק מהסמינר...כל מה שעשיתי
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
        //לבדוק את הפונקציה שוב, מלאי וענינים....
        DO.Product product = new DO.Product();
        product = Dal.Product.Get(id);
        foreach (var item in cart.Items)
        {
            if (item.ProductID == id)
            {
                if (item.Amount < newAmount)
                {
                    if (product.InStock > newAmount - item.Amount)
                    {
                        item.Amount = newAmount;
                        cart.TotalPrice += item.Price * (newAmount - item.Amount);
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
                    product.InStock += (item.Amount - newAmount);
                }
            }
        }
        return cart;
    }
    public void MakeAnOrder(BO.Cart cart, string customerName, string customerEmail, string customerAdress)
    {
    }
}
