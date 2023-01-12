//מכיוון שאנו בקבוצה מספר אי זוגי של בנות 
//והמרצה של הקבוצה אינה מרשה לעשות שלישה
//קיבלתי הנחיה להגיש לבד את הפרויקט.
using DO;
using DalApi;

namespace Dal;
class Program
{
    static DalApi.IDal? s_IDal = DalApi.Factory.Get();
    public static void Main(string[] args)
    {
        Console.WriteLine("enter 0 to Exit\n" +
                          "enter 1 to Product\n" +
                          "enter 2 to Order\n" +
                          "enter 3 to OrderItem");
        string? chooseBeforeParse = Console.ReadLine();
        int.TryParse(chooseBeforeParse, out int choose);
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
            string? chooseMethodBeforeParse = Console.ReadLine();
            int.TryParse(chooseMethodBeforeParse, out int chooseMethod);
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
            chooseBeforeParse = Console.ReadLine();
            int.TryParse(chooseBeforeParse, out choose);
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
                string? idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out ID);
                string? name = Console.ReadLine();
                string? priceBeforeParse = Console.ReadLine();
                double.TryParse(priceBeforeParse, out double price);
                string? inStockBeforeParse = Console.ReadLine();
                int.TryParse(inStockBeforeParse, out int inStock);
                Console.WriteLine("choose categories: percussion=0, stringed=1, keyboard=2, wind=3, electronic=4");
                string? categoriesBeforeParse = Console.ReadLine();
                Categories.TryParse(categoriesBeforeParse, out Categories category);
                p.ID = ID;
                p.Name = name;
                p.Price = price;
                p.InStock = inStock;
                p.Category = category;
                try
                {
                    s_IDal?.Product.Add(p);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.Get:
                Console.WriteLine("Enter ID:");
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out ID);
                try
                {
                    Product? product = s_IDal?.Product.Get(ID);
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
                    IEnumerable<Product>? products = s_IDal?.Product.GetAll();
                    if (products != null)
                    {
                        foreach (var item in products)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;
            case (int)Options.Update:
                Console.WriteLine("Enter ID to update:");
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out ID);
                Product p1 = new Product();
                try
                {
                    p = s_IDal?.Product.Get(ID) != null ? p : throw new ExceptionNull();
                    Console.WriteLine(p);
                    p1.ID = ID;
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
                            Console.WriteLine("choose categories: percussion=0, stringed=1, keyboard=2, wind=3, electronic=4");
                            categoriesBeforeParse = Console.ReadLine();
                            Categories.TryParse(categoriesBeforeParse, out category);
                            p1.Category = category;
                            break;
                        case 4:
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
                        s_IDal?.Product.Update(p1);
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
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out ID);
                try
                {
                    s_IDal?.Product.Delete(ID);
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
                string? CustomerName = Console.ReadLine();
                string? CustomerAddress = Console.ReadLine();
                string? CustomerEmail = Console.ReadLine();
                string? orderDateBeforeParse = Console.ReadLine();
                DateTime.TryParse(orderDateBeforeParse, out DateTime orderDate);
                string? shipDateBeforeParse = Console.ReadLine();
                DateTime.TryParse(shipDateBeforeParse, out DateTime shipDate);
                string? deliveryDateBeforeParse = Console.ReadLine();
                DateTime.TryParse(deliveryDateBeforeParse, out DateTime deliveryDate);
                o.CustomerName = CustomerName;
                o.CustomerEmail = CustomerAddress;
                o.CustomerAdress = CustomerEmail;
                o.OrderDate = orderDate;
                o.ShipDate = shipDate;
                o.DeliveryDate = deliveryDate;
                try
                {
                    s_IDal?.Order.Add(o);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.Get:
                Console.WriteLine("Enter ID:");
                string? idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out ID);
                try
                {
                    o = s_IDal?.Order.Get(ID) != null ? o : throw new ExceptionNull();
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
                    IEnumerable<Order>? orders = s_IDal?.Order.GetAll();
                    if (orders != null)
                    {
                        foreach (var item in orders)
                        {
                            Console.WriteLine(o);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.Update:
                Console.WriteLine("Enter Order_ID to update:");
                string? orderIDBeforeParse = Console.ReadLine();
                int.TryParse(orderIDBeforeParse, out ID);
                Order o1 = new Order();
                try
                {
                    o = s_IDal?.Order.Get(ID) != null ? o : throw new ExceptionNull();
                    Console.WriteLine(o);
                    o1.ID = ID;
                    Console.WriteLine("enter 0 to update CustomerName\n" +
                         "enter 1 to update CustomerEmail\n" +
                         "enter 2 to update CustomerAdress\n" +
                         "enter 3 to update OrderDate\n" +
                         "enter 4 to update ShipDate\n" +
                         "enter 5 to update DeliveryDate\n" +
                         "enter 6 to updare all");
                    int chooseUpdate = Convert.ToInt32(Console.ReadLine());
                    switch (chooseUpdate)
                    {
                        case 0:
                            CustomerName = Console.ReadLine();
                            o1.CustomerName = CustomerName;
                            break;
                        case 1:
                            CustomerEmail = Console.ReadLine();
                            o1.CustomerEmail = CustomerEmail;
                            break;
                        case 2:
                            CustomerAddress = Console.ReadLine();
                            o1.CustomerAdress = CustomerAddress;
                            break;
                        case 3:
                            orderDateBeforeParse = Console.ReadLine();
                            DateTime.TryParse(orderDateBeforeParse, out orderDate);
                            o1.OrderDate = orderDate;
                            break;
                        case 4:
                            shipDateBeforeParse = Console.ReadLine();
                            DateTime.TryParse(shipDateBeforeParse, out shipDate);
                            o1.ShipDate = shipDate;
                            break;
                        case 5:
                            deliveryDateBeforeParse = Console.ReadLine();
                            DateTime.TryParse(deliveryDateBeforeParse, out deliveryDate);
                            o1.DeliveryDate = deliveryDate;
                            break;
                        case 6:
                            CustomerName = Console.ReadLine();
                            CustomerAddress = Console.ReadLine();
                            CustomerEmail = Console.ReadLine();
                            orderDateBeforeParse = Console.ReadLine();
                            DateTime.TryParse(orderDateBeforeParse, out orderDate);
                            shipDateBeforeParse = Console.ReadLine();
                            DateTime.TryParse(shipDateBeforeParse, out shipDate);
                            deliveryDateBeforeParse = Console.ReadLine();
                            DateTime.TryParse(deliveryDateBeforeParse, out deliveryDate);
                            o1.CustomerName = CustomerName;
                            o1.CustomerEmail = CustomerAddress;
                            o1.CustomerAdress = CustomerEmail;
                            o1.OrderDate = orderDate;
                            o1.ShipDate = shipDate;
                            o1.DeliveryDate = deliveryDate;
                            break;
                        default:
                            break;
                    }
                    try
                    {
                        s_IDal?.Order.Add(o);
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
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out ID);
                try
                {
                    s_IDal?.Order.Delete(ID); ;
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
                Product p = new Product();
                Console.WriteLine("Write ProductId, OrderId, Amount");
                string? productIDBeforeParse = Console.ReadLine();
                int.TryParse(productIDBeforeParse, out int productID);
                string? orderIDBeforeParse = Console.ReadLine();
                int.TryParse(orderIDBeforeParse, out int orderID);
                string? amountBeforeParse = Console.ReadLine();
                int.TryParse(amountBeforeParse, out int amount);
                oi.ProductId = productID;
                oi.OrderId = orderID;
                oi.Amount = amount;
                try
                {
                    Product? product = new Product();
                    product = s_IDal?.Product.Get(oi.ProductId);
                    if (product != null) { p = (Product)product; }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                oi.Price = p.Price * oi.Amount;
                try
                {
                    s_IDal?.OrderItem.Add(oi);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.Get:
                Console.WriteLine("Enter ID:");
                string? idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out ID);
                try
                {
                    oi = s_IDal?.OrderItem.Get(ID) != null ? oi : throw new ExceptionNull();
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
                    IEnumerable<OrderItem>? orderItems = s_IDal?.OrderItem.GetAll();
                    if (orderItems != null)
                    {
                        foreach (var item in orderItems)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.Update:
                Console.WriteLine("Enter OrderItem_ID to update:");
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out ID);
                OrderItem oi1 = new OrderItem();
                try
                {

                    oi = s_IDal?.OrderItem.Get(ID) != null ? oi : throw new ExceptionNull();
                    double price = oi.Price / oi.Amount;
                    Console.WriteLine(oi);
                    oi1.ID = ID;
                    Console.WriteLine("enter 0 to update ProductId\n" +
                         "enter 1 to update OrderId\n" +
                         "enter 2 to update Amount\n" +
                         "enter 3 to update all");
                    int chooseUpdate = Convert.ToInt32(Console.ReadLine());
                    switch (chooseUpdate)
                    {
                        case 0:
                            productIDBeforeParse = Console.ReadLine();
                            int.TryParse(productIDBeforeParse, out productID);
                            oi1.ProductId = productID;
                            break;
                        case 1:
                            orderIDBeforeParse = Console.ReadLine();
                            int.TryParse(orderIDBeforeParse, out orderID);
                            oi1.OrderId = orderID;
                            break;
                        case 2:
                            amountBeforeParse = Console.ReadLine();
                            int.TryParse(amountBeforeParse, out amount);
                            oi1.Amount = amount;
                            break;
                        case 3:
                            productIDBeforeParse = Console.ReadLine();
                            int.TryParse(productIDBeforeParse, out productID);
                            orderIDBeforeParse = Console.ReadLine();
                            int.TryParse(orderIDBeforeParse, out orderID);
                            amountBeforeParse = Console.ReadLine();
                            int.TryParse(amountBeforeParse, out amount);
                            oi1.ProductId = productID;
                            oi1.OrderId = orderID;
                            oi1.Price = price;
                            oi1.Amount = amount;
                            break;
                        default:
                            break;
                    }
                    try
                    {
                        s_IDal?.OrderItem.Update(oi);
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
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out ID);
                try
                {
                    s_IDal?.OrderItem.Delete(ID);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.GetByOrderID:
                Console.WriteLine("Enter orderID:");
                idBeforeParse = Console.ReadLine();
                int.TryParse(idBeforeParse, out ID);
                try
                {
                    List<OrderItem>? orderItems = s_IDal?.OrderItem.GetByOrderID(ID).ToList();
                    if (orderItems != null)
                    {
                        foreach (var item in orderItems)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            case (int)Options.GetByProductIDandOrderID:
                Console.WriteLine("Enter orderID and productID:");
                productIDBeforeParse = Console.ReadLine();
                int.TryParse(productIDBeforeParse, out productID);
                orderIDBeforeParse = Console.ReadLine();
                int.TryParse(orderIDBeforeParse, out orderID);
                try
                {
                    oi = s_IDal?.OrderItem.GetByProductIDAndOrderID(productID, orderID) != null ? oi : throw new ExceptionNull();
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
