//מכיוון שאנו בקבוצה מספר אי זוגי של בנות 
//והמרצה של הקבוצה אינה מרשה לעשות שלישה
//קיבלתי הנחיה להגיש לבד את הפרויקט.
using BlApi;
using BO;
using BlImplementation;

namespace BlTest;
class Program
{
    static IBl s_IBl = new Bl();

    public static void Main(string[] args)
    {
        Cart cart = new Cart();
        cart.Items = new List<OrderItem>();
        Console.WriteLine("enter 0 to Exit\n" +
                          "enter 1 to Product\n" +
                          "enter 2 to Order\n" +
                          "enter 3 to Cart");
        string? chooseBeforeParse = Console.ReadLine();
        int.TryParse(chooseBeforeParse, out int choose);
        while (choose != 0)
        {
            switch (choose)
            {
                case 1:
                    _product(cart);
                    break;
                case 2:
                    _order();
                    break;
                case 3:
                    cart = _cart(cart);
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter 0 to Exit\n" +
                         "enter 1 to Product\n" +
                         "enter 2 to Order\n" +
                         "enter 3 to Cart");
            chooseBeforeParse = Console.ReadLine();
            int.TryParse(chooseBeforeParse, out choose);
        }
    }

    private static void _product(Cart cart)
    {
        Product p = new Product();
        int id;
        Console.WriteLine("enter 0 to GetListProducts" +
                "\nenter 1 to GetProductItem" +
                "\nenter 2 to GetProduct" +
                "\nenter 3 to AddProduct" +
                "\nenter 4 to UpdateProduct" +
                "\nenter 5 to DeleteProduct");
        string? chooseBeforeParse = Console.ReadLine();
        int.TryParse(chooseBeforeParse, out int choose);
        string? idBeforeParse;
        switch (choose)
        {
            case 0://GetListProducts
                try
                {
                    IEnumerable<ProductForList> ListProductForList = s_IBl.Product.GetAll();
                    foreach (var item in ListProductForList)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            case 1://GetProductItem
                try
                {
                    Console.WriteLine("Enter productID:");
                    idBeforeParse = Console.ReadLine();
                    int.TryParse(idBeforeParse, out id);
                    ProductItem productItem = s_IBl.Product.Get(id, cart);
                    Console.WriteLine(productItem);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            case 2://GetProduct
                Console.WriteLine("Enter productID:");
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out id);
                try
                {
                    p = s_IBl.Product.Get(id);
                    Console.WriteLine(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            case 3://AddProduct
                Console.WriteLine("Write ID, name, price, inStock");
                string? name;
                Categories category;
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out id);
                name = Console.ReadLine();
                string? priceBeforeParse = Console.ReadLine();
                double.TryParse(priceBeforeParse, out double price);
                string? inStockBeforeParse = Console.ReadLine();
                int.TryParse(inStockBeforeParse, out int inStock);
                Console.WriteLine("choose categories: percussion=0, stringed=1, keyboard=2, wind=3, electronic=4");
                string? categoriesBeforeParse = Console.ReadLine();
                Categories.TryParse(categoriesBeforeParse, out category);
                p.ID = id;
                p.Name = name;
                p.Price = price;
                p.InStock = inStock;
                p.Category = category;
                try
                {
                    s_IBl.Product.Add(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            case 4://UpdateProduct
                Console.WriteLine("Enter ID to update:");
                id = Convert.ToInt32(Console.ReadLine());
                Product p1 = new Product();
                try
                {
                    p = s_IBl.Product.Get(id);
                    p1.ID = p.ID;
                    Console.WriteLine("enter 0 to update name\n" +
                         "enter 1 to update price\n" +
                         "enter 2 to update inStock\n" +
                         "enter 3 to update categories\n" +
                         "enter 4 to update all");
                    int chooseUpdate = Convert.ToInt32(Console.ReadLine());
                    switch (chooseUpdate)
                    {
                        case 0:
                            name = Console.ReadLine();
                            p1.Name = name;
                            break;
                        case 1:
                            priceBeforeParse = Console.ReadLine();
                            double.TryParse(priceBeforeParse, out price);
                            p1.Price = price;
                            break;
                        case 2:
                            inStockBeforeParse = Console.ReadLine();
                            int.TryParse(inStockBeforeParse, out inStock);
                            p1.InStock = inStock;
                            break;
                        case 3:
                            categoriesBeforeParse = Console.ReadLine();
                            Categories.TryParse(categoriesBeforeParse, out category);
                            p1.Category = category;
                            break;
                        case 4:
                            Console.WriteLine("Write ID, name, price, inStock");
                            name = Console.ReadLine();
                            priceBeforeParse = Console.ReadLine();
                            double.TryParse(priceBeforeParse, out price);
                            inStockBeforeParse = Console.ReadLine();
                            int.TryParse(inStockBeforeParse, out inStock);
                            Console.WriteLine("choose categories: percussion=0, stringed=1, keyboard=2, wind=3, electronic=4");
                            categoriesBeforeParse = Console.ReadLine();
                            Categories.TryParse(categoriesBeforeParse, out category);
                            p1.Name = name;
                            p1.Price = price;
                            p1.InStock = inStock;
                            p1.Category = category;
                            break;
                        default:
                            break;
                    }
                    try
                    {
                        s_IBl.Product.Update(p1);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        if (ex.InnerException != null)
                        {
                            Console.WriteLine(ex.InnerException.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            case 5://DeleteProduct
                Console.WriteLine("Enter ID to delete:");
                Console.WriteLine("Enter ID to update:");
                id = Convert.ToInt32(Console.ReadLine());
                try
                {
                    s_IBl.Product.Delete(id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            default:
                break;
        }
    }

    private static void _order()
    {
        Order o = new Order();
        Console.WriteLine("enter 0 to GetListOrders" +
                "\nenter 1 to GetOrder" +
                "\nenter 2 to UpdateOrderShipping" +
                "\nenter 3 to UpdateOrderDelivery");
        string? chooseBeforeParse = Console.ReadLine();
        int.TryParse(chooseBeforeParse, out int choose);
        int id;
        switch (choose)
        {

            case 0://GetListOrders
                try
                {
                    IEnumerable<OrderForList> ListOrderForList = s_IBl.Order.GetAll();
                    foreach (var item in ListOrderForList)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            case 1://GetOrder
                Console.WriteLine("Enter orderID:");
                string? idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out id);
                try
                {
                    o = s_IBl.Order.Get(id);
                    Console.WriteLine(o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            case 2://UpdateOrderShipping
                Console.WriteLine("Enter orderID:");
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out id);
                try
                {
                    o = s_IBl.Order.UpdateOrderShipping(id);
                    Console.WriteLine(o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            case 3://UpdateOrderDelivery
                Console.WriteLine("Enter orderID:");
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out id);
                try
                {
                    o = s_IBl.Order.UpdateOrderDelivery(id);
                    Console.WriteLine(o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            default:
                break;
        }
    }

    private static Cart _cart(Cart cart)
    {
        Product p = new Product();
        int id;
        Console.WriteLine("enter 0 to AddProduct" +
                "\nenter 1 to UpdateAmountOfProduct" +
                "\nenter 2 to MakeAnOrder");
        string? chooseBeforeParse = Console.ReadLine();
        int.TryParse(chooseBeforeParse, out int choose);
        switch (choose)
        {
            case 0://AddProduct
                Console.WriteLine("enter productID:");
                string? idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out id);
                try
                {
                    cart = s_IBl.Cart.AddProduct(cart, id);
                    Console.WriteLine(cart);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            case 1://UpdateAmountOfProduct
                Console.WriteLine("enter productID:");
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out id);
                Console.WriteLine("enter newAmount:");
                string? idNewAmount = Console.ReadLine();
                int.TryParse(idNewAmount, out int newAmount);
                try
                {
                    cart = s_IBl.Cart.UpdateAmountOfProduct(cart, id, newAmount);
                    Console.WriteLine(cart);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            case 2://MakeAnOrder
                Console.WriteLine("Enter customerName:");
                string? customerName = Console.ReadLine();
                Console.WriteLine("Enter customerEmail:");
                string? customerEmail = Console.ReadLine();
                Console.WriteLine("Enter customerAdress:");
                string? customerAdress = Console.ReadLine();
                try
                {
                    s_IBl.Cart.MakeAnOrder(cart, customerName, customerEmail, customerAdress);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(ex.InnerException.Message);
                    }
                }
                break;
            default:
                break;
        }
        return cart;
    }
}
