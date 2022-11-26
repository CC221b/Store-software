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
        int choose = int.Parse(Console.ReadLine());
        while (choose != 0)
        {
            switch (choose)
            {
                case 1:
                    _product();
                    break;
                case 2:
                    _order();
                    break;
                case 3:
                    _cart(cart);
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter 0 to Exit\n" +
                         "enter 1 to Product\n" +
                         "enter 2 to Order\n" +
                         "enter 3 to Cart");
            choose = Convert.ToInt32(Console.ReadLine());
        }
    }

    private static void _product()
    {
        Product p = new Product();
        int id;
        Console.WriteLine("enter 0 to GetListProducts" +
                "\nenter 1 to GetCatalog" +
                "\nenter 2 to GetProduct" +
                "\nenter 3 to AddProduct" +
                "\nenter 4 to UpdateProduct" +
                "\nenter 5 to DeleteProduct");
        int choose = Convert.ToInt32(Console.ReadLine());
        switch (choose)
        {
            case 0://GetListProducts
                try
                {
                    IEnumerable<ProductForList> ListProductForList = s_IBl.Product.GetListProducts();
                    foreach (var item in ListProductForList)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
                break;
            case 1://GetCatalog
                try
                {
                    IEnumerable<ProductItem> ListProducts = s_IBl.Product.GetCatalog();
                    foreach (var item in ListProducts)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
                break;
            case 2://GetProduct
                Console.WriteLine("Enter productID:");
                id = Convert.ToInt32(Console.ReadLine());
                try
                {
                    p = s_IBl.Product.GetProduct(id);
                    Console.WriteLine(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
                break;
            case 3://AddProduct
                Console.WriteLine("Write ID, name, price, inStock");
                p.ID = Convert.ToInt32(Console.ReadLine());
                p.Name = Console.ReadLine();
                p.Price = Convert.ToInt32(Console.ReadLine());
                p.InStock = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("choose categories: percussion=0, stringed=1, keyboard=2, wind=3, electronic=4");
                int category = Convert.ToInt32(Console.ReadLine());
                p.Category = (BO.Categories)category;
                try
                {
                    s_IBl.Product.AddProduct(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 4://UpdateProduct
                Console.WriteLine("Enter ID to update:");
                id = Convert.ToInt32(Console.ReadLine());
                Product p1 = new Product();
                try
                {
                    p = s_IBl.Product.GetProduct(id);
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
                            p1.Name = Console.ReadLine();
                            break;
                        case 1:
                            p1.Price = Convert.ToInt32(Console.ReadLine());
                            break;
                        case 2:
                            p1.InStock = Convert.ToInt32(Console.ReadLine());
                            break;
                        case 3:
                            Console.WriteLine("choose categories: percussion=0, stringed=1, keyboard=2, wind=3, electronic=4");
                            category = Convert.ToInt32(Console.ReadLine());
                            p1.Category = (Categories)category;
                            break;
                        case 4:
                            p1.Name = Console.ReadLine();
                            p1.Price = Convert.ToInt32(Console.ReadLine());
                            p1.InStock = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("choose categories: percussion=0, stringed=1, keyboard=2, wind=3, electronic=4");
                            category = Convert.ToInt32(Console.ReadLine());
                            p1.Category = (Categories)category;
                            break;
                        default:
                            break;
                    }
                    try
                    {
                        s_IBl.Product.UpdateProduct(p1);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 5://DeleteProduct
                Console.WriteLine("Enter ID to delete:");
                id = int.Parse(Console.ReadLine());
                try
                {
                    s_IBl.Product.DeleteProduct(id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
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
        int choose = Convert.ToInt32(Console.ReadLine());
        int id;
        switch (choose)
        {

            case 0://GetListOrders
                try
                {
                    IEnumerable<OrderForList> ListOrderForList = s_IBl.Order.GetListOrders();
                    foreach (var item in ListOrderForList)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case 1://GetOrder
                Console.WriteLine("Enter orderID:");
                id = Convert.ToInt32(Console.ReadLine());
                try
                {
                    o = s_IBl.Order.GetOrder(id);
                    Console.WriteLine(o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
                break;
            case 2://UpdateOrderShipping
                Console.WriteLine("Enter orderID:");
                id = Convert.ToInt32(Console.ReadLine());
                try
                {
                    o = s_IBl.Order.UpdateOrderShipping(id);
                    Console.WriteLine(o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
                break;
            case 3://UpdateOrderDelivery
                Console.WriteLine("Enter orderID:");
                id = Convert.ToInt32(Console.ReadLine());
                try
                {
                    o = s_IBl.Order.UpdateOrderDelivery(id);
                    Console.WriteLine(o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            default:
                break;
        }
    }

    private static void _cart(Cart cart)
    {
        Product p = new Product();
        Cart c1 = new Cart();
        int id;
        Console.WriteLine("enter 0 to AddProduct" +
                "\nenter 1 to UpdateAmountOfProduct" +
                "\nenter 2 to MakeAnOrder");
        int choose = Convert.ToInt32(Console.ReadLine());
        switch (choose)
        {
            case 0://AddProduct
                Console.WriteLine("enter productID:");
                id = Convert.ToInt32(Console.ReadLine());
                try
                {
                    c1 = s_IBl.Cart.AddProduct(cart, id);
                    Console.WriteLine(c1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
                break;
            case 1://UpdateAmountOfProduct
                Console.WriteLine("enter productID:");
                id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("enter newAmount:");
                int newAmount = Convert.ToInt32(Console.ReadLine());
                try
                {
                    c1 = s_IBl.Cart.UpdateAmountOfProduct(cart, id, newAmount);
                    Console.WriteLine(c1);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException.Message);
                }
                break;
            case 2://MakeAnOrder
                Console.WriteLine("Enter customerName:");
                string customerName = Console.ReadLine();
                Console.WriteLine("Enter customerEmail:");
                string customerEmail = Console.ReadLine();
                Console.WriteLine("Enter customerAdress:");
                string customerAdress = Console.ReadLine();
                try
                {
                    s_IBl.Cart.MakeAnOrder(cart, customerName, customerEmail, customerAdress);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            default:
                break;
        }
    }
}
