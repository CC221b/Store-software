//מכיוון שאנו בקבוצה מספר אי זוגי של בנות 
//והמרצה של הקבוצה אינה מרשה לעשות שלישה
//קיבלתי הנחיה להגיש לבד את הפרויקט.
using DO;
using DalApi;

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
                p._id = int.Parse(Console.ReadLine());
                p._name = Console.ReadLine();
                p._price = int.Parse(Console.ReadLine());
                p._inStock = int.Parse(Console.ReadLine());
                Console.WriteLine("choose categories: percussion=0, stringed=1, keyboard=2, wind=3, electronic=4");
                int category = int.Parse(Console.ReadLine());
                p._category = (DO.Categories)category;
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
                    p1._name = Console.ReadLine();
                    p1._name = p1._name == "" ? p._name : p1._name;
                    Console.WriteLine("Write price:");
                    p1._price = int.Parse(Console.ReadLine());
                    p1._price = p1._price == null ? p._price : p1._price;
                    Console.WriteLine("Write inStock:");
                    p1._inStock = int.Parse(Console.ReadLine());
                    p1._inStock = p1._inStock == null ? p._inStock : p1._inStock;
                    Console.WriteLine("choose categories: percussion=0, stringed=1, keyboard=2, wind=3, electronic=4");
                    category = int.Parse(Console.ReadLine());
                    p1._category = (Categories)category;
                    p1._category = p1._category == null ? p._category : p1._category;
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
                o._customerName = Console.ReadLine();
                o._customerEmail = Console.ReadLine();
                o._customerAdress = Console.ReadLine();
                o._orderDate = Convert.ToDateTime(Console.ReadLine());
                o._shipDate = Convert.ToDateTime(Console.ReadLine());
                o._deliveryDate = Convert.ToDateTime(Console.ReadLine());
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
                    o1._customerName = Console.ReadLine();
                    o1._customerName = o1._customerName == null ? o._customerName : o1._customerName;
                    Console.WriteLine("Write CustomerEmail:");
                    o1._customerEmail = Console.ReadLine();
                    o1._customerEmail = o1._customerEmail == null ? o._customerEmail : o1._customerEmail;
                    Console.WriteLine("Write CustomerAdress:");
                    o1._customerAdress = Console.ReadLine();
                    o1._customerAdress = o1._customerAdress == null ? o._customerAdress : o1._customerAdress;
                    Console.WriteLine("Write OrderDate:");
                    o1._orderDate = Convert.ToDateTime(Console.ReadLine());
                    o1._orderDate = o1._orderDate == null ? o._orderDate : o1._orderDate;
                    Console.WriteLine("Write ShipDate:");
                    o1._shipDate = Convert.ToDateTime(Console.ReadLine());
                    o1._shipDate = o1._shipDate == null ? o._shipDate : o1._shipDate;
                    Console.WriteLine("Write DeliveryDate:");
                    o1._deliveryDate = Convert.ToDateTime(Console.ReadLine());
                    o1._deliveryDate = o1._deliveryDate == null ? o._deliveryDate : o1._deliveryDate;
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
                oi._productId = int.Parse(Console.ReadLine());
                oi._orderId = int.Parse(Console.ReadLine());
                oi._price = int.Parse(Console.ReadLine());
                oi._amount = int.Parse(Console.ReadLine());
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
                    oi1._productId = int.Parse(Console.ReadLine());
                    oi1._productId = oi1._productId == null ? oi._productId : oi1._productId;
                    Console.WriteLine("Write OrderId:");
                    oi1._orderId = int.Parse(Console.ReadLine());
                    oi1._orderId = oi1._orderId == null ? oi._orderId : oi1._orderId;
                    Console.WriteLine("Write Price:");
                    oi1._price = int.Parse(Console.ReadLine());
                    oi1._price = oi1._price == null ? oi._price : oi1._price;
                    Console.WriteLine("Write Amount:");
                    oi1._amount = int.Parse(Console.ReadLine());
                    oi1._amount = oi1._amount == null ? oi._amount : oi1._amount;
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
