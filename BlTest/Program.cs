//מכיוון שאנו בקבוצה מספר אי זוגי של בנות 
//והמרצה של הקבוצה אינה מרשה לעשות שלישה
//קיבלתי הנחיה להגיש לבד את הפרויקט.
using BlApi;
using BO;
using BlImplementation;
using DalApi;

namespace BlTest;
class Program
{
    static IBl s_IBl = new Bl();

    public static void Main(string[] args)
    {
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
                    _cart();
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
        int ID;
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
                }
                break;
            case 2://GetProduct
                Console.WriteLine("Enter productID:");
                int id = Convert.ToInt32(Console.ReadLine());
                try
                {
                    Product product = new Product();
                    product = s_IBl.Product.GetProduct(id);
                    Console.WriteLine(product);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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
                break;
            case 5://DeleteProduct
                break;
            default:
                break;
        }
    }

    private static void _order()
    {
        Product p = new Product();
        int ID;
        Console.WriteLine("enter 0 to GetListOrders" +
                "\nenter 1 to GetOrder" +
                "\nenter 2 to UpdateOrderShipping" +
                "\nenter 3 to UpdateOrderDelivery");
        int choose = Convert.ToInt32(Console.ReadLine());
        switch (choose)
        {
            case 0://GetListOrders
                break;
            case 1://GetOrder
                break;
            case 2://UpdateOrderShipping
                break;
            case 3://UpdateOrderDelivery
            default:
                break;
        }
    }

    private static void _cart()
    {
        Product p = new Product();
        int ID;
        Console.WriteLine("enter 0 to AddProduct" +
                "\nenter 1 to UpdateAmountOfProduct" +
                "\nenter 2 to MakeAnOrder");
        int choose = Convert.ToInt32(Console.ReadLine());
        switch (choose)
        {
            case 0://AddProduct
                break;
            case 1://UpdateAmountOfProduct
                break;
            case 2://MakeAnOrder
                break;
            default:
                break;
        }
    }
}
