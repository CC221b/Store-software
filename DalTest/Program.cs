//מכיוון שאנו בקבוצה מספר אי זוגי של בנות 
//והמרצה של הקבוצה אינה מרשה לעשות שלישה
//קיבלתי הנחיה להגיש לבד את הפרויקט.
using DO;

namespace Dal;
class Program
{
    static DalList s_IDal = new DalList();
    public static void Main(string[] args)
    {
        Console.WriteLine("enter 0 to Exit\n" +
                          "enter 1 to Product\n" +
                          "enter 2 to Order\n" +
                          "enter 3 to OrderItem");
        int choose = int.Parse(Console.ReadLine());
        while (choose != 0)
        {
            string nameChoose = (choose == 1) ? "Product" : (choose == 2) ? "Order" : "OrderItem";
            Console.WriteLine("enter 0 to add " + nameChoose +
                "\nenter 1 to read " + nameChoose + " with id" +
                "\nenter 2 to read all " + nameChoose + "s" +
                "\nenter 3 to update " + nameChoose +
                "\nenter 4 to delete " + nameChoose);
            if (nameChoose == "OrderItem")
            {
                Console.WriteLine("enter 5 to read orderItem by product_id and order_id\n" +
                "enter 6 to read orderItems by order_id");
            }
            int chooseMethod = int.Parse(Console.ReadLine());
            switch (choose)
            {
                case 1:
                    _product(chooseMethod);
                    break;
                case 2:
                    _order(chooseMethod);
                    break;
                case 3:
                    _orderItem(chooseMethod);
                    break;
                default:
                    break;
            }
            Console.WriteLine("enter 0 to Exit\n" +
                         "enter 1 to Product\n" +
                         "enter 2 to Order\n" +
                         "enter 3 to OrderItem");
            choose = Convert.ToInt32(Console.ReadLine());
        }
    }
    private static void _product(int choose)
    {
        Product p = new Product();
        int ID;
        switch (choose)
        {
            case (int)Options.Add:
                Console.WriteLine("Write ID, name, price, inStock");
                p.ID = int.Parse(Console.ReadLine());
                p.Name = Console.ReadLine();
                p.Price = int.Parse(Console.ReadLine());
                p.InStock = int.Parse(Console.ReadLine());
                Console.WriteLine("choose categories: percussion=0, stringed=1, keyboard=2, wind=3, electronic=4");
                int category = int.Parse(Console.ReadLine());
                p.Category = (DO.Categories)category;
                try
                {
                    s_IDal.Product.Add(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.Get:
                Console.WriteLine("Enter ID:");
                ID = int.Parse(Console.ReadLine());
                try
                {
                    p = s_IDal.Product.Get(ID);
                    Console.WriteLine(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.GetAll:
                try
                {
                    IEnumerable<Product> products = s_IDal.Product.GetAll();
                    foreach (var item in products)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case (int)Options.Update:
                Console.WriteLine("Enter ID to update:");
                ID = int.Parse(Console.ReadLine());
                Product p1 = new Product();
                try
                {
                    p = s_IDal.Product.Get(ID);
                    Console.WriteLine(p);
                    Console.WriteLine("Write name:");
                    p1.Name = Console.ReadLine();
                    p1.Name = p1.Name == "" ? p.Name : p1.Name;
                    Console.WriteLine("Write price:");
                    p1.Price = int.Parse(Console.ReadLine());
                    p1.Price = p1.Price == null ? p.Price : p1.Price;
                    Console.WriteLine("Write inStock:");
                    p1.InStock = int.Parse(Console.ReadLine());
                    p1.InStock = p1.InStock == null ? p.InStock : p1.InStock;
                    Console.WriteLine("choose categories: percussion=0, stringed=1, keyboard=2, wind=3, electronic=4");
                    category = int.Parse(Console.ReadLine());
                    p1.Category = (Categories)category;
                    p1.Category = p1.Category == null ? p.Category : p1.Category;
                    try
                    {
                        s_IDal.Product.Update(p1);
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
            case (int)Options.Delete:
                Console.WriteLine("Enter ID to delete:");
                ID = int.Parse(Console.ReadLine());
                try
                {
                    s_IDal.Product.Delete(ID);
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
    private static void _order(int choose)
    {
        Order o = new Order();
        int ID;
        switch (choose)
        {
            case (int)Options.Add:
                Console.WriteLine("Write CustomerName, CustomerEmail, CustomerAdress, OrderDate, ShipDate, DeliveryDate");
                o.CustomerName = Console.ReadLine();
                o.CustomerEmail = Console.ReadLine();
                o.CustomerAdress = Console.ReadLine();
                o.OrderDate = Convert.ToDateTime(Console.ReadLine());
                o.ShipDate = Convert.ToDateTime(Console.ReadLine());
                o.DeliveryDate = Convert.ToDateTime(Console.ReadLine());
                try
                {
                    s_IDal.Order.Add(o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.Get:
                Console.WriteLine("Enter ID:");
                ID = int.Parse(Console.ReadLine());
                try
                {
                    o = s_IDal.Order.Get(ID);
                    Console.WriteLine(o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.GetAll:
                try
                {
                    IEnumerable<Order> orders = s_IDal.Order.GetAll();
                    foreach (var item in orders)
                    {
                        Console.WriteLine(o);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.Update:
                Console.WriteLine("Enter Order_ID to update:");
                int order_ID = int.Parse(Console.ReadLine());
                Order o1 = new Order();
                try
                {
                    o = s_IDal.Order.Get(order_ID);
                    Console.WriteLine(o);
                    Console.WriteLine("Write CustomerName:");
                    o1.CustomerName = Console.ReadLine();
                    o1.CustomerName = o1.CustomerName == null ? o.CustomerName : o1.CustomerName;
                    Console.WriteLine("Write CustomerEmail:");
                    o1.CustomerEmail = Console.ReadLine();
                    o1.CustomerEmail = o1.CustomerEmail == null ? o.CustomerEmail : o1.CustomerEmail;
                    Console.WriteLine("Write CustomerAdress:");
                    o1.CustomerAdress = Console.ReadLine();
                    o1.CustomerAdress = o1.CustomerAdress == null ? o.CustomerAdress : o1.CustomerAdress;
                    Console.WriteLine("Write OrderDate:");
                    o1.OrderDate = Convert.ToDateTime(Console.ReadLine());
                    o1.OrderDate = o1.OrderDate == null ? o.OrderDate : o1.OrderDate;
                    Console.WriteLine("Write ShipDate:");
                    o1.ShipDate = Convert.ToDateTime(Console.ReadLine());
                    o1.ShipDate = o1.ShipDate == null ? o.ShipDate : o1.ShipDate;
                    Console.WriteLine("Write DeliveryDate:");
                    o1.DeliveryDate = Convert.ToDateTime(Console.ReadLine());
                    o1.DeliveryDate = o1.DeliveryDate == null ? o.DeliveryDate : o1.DeliveryDate;
                    try
                    {
                        s_IDal.Order.Add(o);
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
            case (int)Options.Delete:
                Console.WriteLine("Enter ID to delete:");
                ID = int.Parse(Console.ReadLine());
                try
                {
                    s_IDal.Order.Delete(ID); ;
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
    private static void _orderItem(int choose)
    {
        OrderItem oi = new OrderItem();
        int ID;
        switch (choose)
        {
            case (int)Options.Add:
                Console.WriteLine("Write ProductId, OrderId, Price, Amount");
                oi.ProductId = int.Parse(Console.ReadLine());
                oi.OrderId = int.Parse(Console.ReadLine());
                oi.Price = int.Parse(Console.ReadLine());
                oi.Amount = int.Parse(Console.ReadLine());
                try
                {
                    s_IDal.OrderItem.Add(oi);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.Get:
                Console.WriteLine("Enter ID:");
                ID = int.Parse(Console.ReadLine());
                try
                {
                    oi = s_IDal.OrderItem.Get(ID);
                    Console.WriteLine(oi);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.GetAll:
                try
                {
                    IEnumerable<OrderItem> orderItems = s_IDal.OrderItem.GetAll();
                    foreach (var item in orderItems)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.Update:
                Console.WriteLine("Enter OrderItem_ID to update:");
                int orderItem_ID = int.Parse(Console.ReadLine());
                OrderItem oi1 = new OrderItem();
                try
                {
                    oi = s_IDal.OrderItem.Get(orderItem_ID);
                    Console.WriteLine(oi);
                    Console.WriteLine("Write ProductId:");
                    oi1.ProductId = int.Parse(Console.ReadLine());
                    oi1.ProductId = oi1.ProductId == null ? oi.ProductId : oi1.ProductId;
                    Console.WriteLine("Write OrderId:");
                    oi1.OrderId = int.Parse(Console.ReadLine());
                    oi1.OrderId = oi1.OrderId == null ? oi.OrderId : oi1.OrderId;
                    Console.WriteLine("Write Price:");
                    oi1.Price = int.Parse(Console.ReadLine());
                    oi1.Price = oi1.Price == null ? oi.Price : oi1.Price;
                    Console.WriteLine("Write Amount:");
                    oi1.Amount = int.Parse(Console.ReadLine());
                    oi1.Amount = oi1.Amount == null ? oi.Amount : oi1.Amount;
                    try
                    {
                        s_IDal.OrderItem.Update(oi);
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
            case (int)Options.Delete:
                Console.WriteLine("Enter ID to delete:");
                ID = int.Parse(Console.ReadLine());
                try
                {
                    s_IDal.OrderItem.Delete(ID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.GetByOrderID:
                Console.WriteLine("Enter orderID:");
                ID = int.Parse(Console.ReadLine());
                try
                {
                    List<OrderItem> orderItems = s_IDal.OrderItem.GetByOrderID(ID);
                    foreach (var item in orderItems)
                    {
                        Console.WriteLine(item);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.GetByProductIDandOrderID:
                Console.WriteLine("Enter orderID and productID:");
                int productID = int.Parse(Console.ReadLine());
                int orderID = int.Parse(Console.ReadLine());
                try
                {
                    oi = s_IDal.OrderItem.GetByProductIDAndOrderID(productID, orderID);
                    Console.WriteLine(oi);
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
