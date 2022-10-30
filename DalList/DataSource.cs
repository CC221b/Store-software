using DO;

namespace Dal;
static internal class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }

    internal static class Config
    {
        static internal int index_Order = 0, index_Product = 0, index_OrderItem;
        public static int order_ID = 222222, orderItem_ID = 123456;

        public static int Order_ID
        {
            get { return order_ID++; }
        }
        public static int OrderItem_ID
        {
            get { return orderItem_ID++; }
        }
        //static internal int index_ID_product = 0;
    }
    //Defining a random variable.
    public static readonly Random rand = new Random(0);

    //Creating entity arrays.
    internal static DO.Order[] Order_arr = new DO.Order[100];

    internal static DO.Product[] Product_arr = new DO.Product[50];
    internal static DO.OrderItem[] OrderItem_arr = new DO.OrderItem[200];

    //Adding functions to entity arrays.
    public static void AddProduct(DO.Product product) { Product_arr[Config.index_Product++] = product; }
    public static void AddOrder(DO.Order order) { Order_arr[Config.index_Order++] = order; }
    public static void AddOrderItem(DO.OrderItem orderItem) { OrderItem_arr[Config.index_OrderItem++] = orderItem; }

    /// <summary>
    /// A function that initializes 10 products.
    /// </summary>
    private static void s_Initialize()
    {
        // An array of 10 items entered manually.
        (string, int, DO.Categories)[] product_arr = new[] {
            ("Acoustic_Guitar", 600, DO.Cate
            gories.stringed),
            ("Electro_acoustic_Guitar", 900, DO.Categories.stringed),
            ("Piccolo_Trumpet", 8700,DO.Categories.wind),
            ("Pocket_Trumpet", 3700, DO.Categories.wind),
            ("Bass_Clarinet", 22000, DO.Categories.wind),
            ("Contra_alto_Clarinet", 620, DO.Categories.wind),
            ("Bass_Drum", 600,DO.Categories.percussion),
            ("Grand_Piano", 1450,DO.Categories.keyboard),
            ("Upright_Piano", 11000,DO.Categories.keyboard),
            ("Semi_Acoustic_Violin",3000,DO.Categories.stringed),
            ("Five_String_Violin", 400,DO.Categories.stringed)};

        (string, string, string)[] customers_arr = new[] {
            ("chani","chani@gmail.com","A"),("eli","eli@gmail.com","B"),
            ("efrat","efrat@gmail.com","C"),("moti","moti@gmail.com","D"),
            ("yael","yael@gmail.com","E"),("sari","sari@gmail.com","F"),
            ("devora","devora@gmail.com","G"),("milca","milca@gmail.com","H"),
            ("siri","siri@gmail.com","I"),("zvi","zvi@gmail.com","J"),
            ("meir","meir@gmail.com","K"),("or","or@gmail.com","L"),
            ("racheli","racheli@gmail.com","M"),("tzzipy","tzzipy@gmail.com","N"),
            ("roth","roth@gmail.com","O"),("daived","daived@gmail.com","P"),
            ("batchen","batchen@gmail.com","W"),("mira","mira@gmail.com","X"),
            ("menochi","menochi@gmail.com","Y"),("mali","mali@gmail.com","Z") };

        //Draw a number that will start the product ID.
        int rand_productId = rand.Next(333333, 666666);

        //add 10 product to product_arr.
        for (int i = 0; i < product_arr.Length; i++)
        {
            int rand_num_InStock = rand.Next(100);
            DO.Product p = new Product();
            //I added the index to make sure that a different number will be created each time.
            p.ID = rand_productId + i;
            p.Name = product_arr[i].Item1;
            p.Price = product_arr[i].Item2;
            p.Category = product_arr[i].Item3;
            p.InStock = rand_num_InStock;
            AddProduct(p);
        }

        //add 20 order to order_arr.
        for (int i = 0; i < 20; i++)
        {
            DO.Order o = new DO.Order();
            //Automatic ID defined in config.
            o.ID = Config.Order_ID;
            o.CustomerName = customers_arr[i].Item1;
            o.CustomerEmail = customers_arr[i].Item2;
            o.CustomerAdress = customers_arr[i].Item3;
            o.OrderDate = DateTime.MinValue;
            //איך משתמשים ב
            //DATASPAN
            //להשלים את 2 התכונות החסרות.
            AddOrder(o);
        }

        //add 40 orderItem to orderItem_arr.

        //The first one was created to make sure that all 20 orders will surely be a private order, not randomly.
        for (int i = 0; i < 20; i++)
        {
            DO.OrderItem oi = new DO.OrderItem();
            int rand_index_product = rand.Next(10);
            //Since this is a musical instrument store, the maximum amount you can order from one instrument is 3 (this is also quite excessive.)
            int rand_amount = rand.Next(1, 3);
            oi.ID = Config.orderItem_ID;
            oi.ProductId = Product_arr[rand_index_product].ID;
            oi.OrderId = Order_arr[i].ID;
            oi.Price = Product_arr[rand_index_product].Price;
            oi.Amount = rand_amount;
            AddOrderItem(oi);
        }

        //The second loop randomly adds order details to the drawn order.
        //A number between 0 and 3 has been drawn and according to this we will advance the loop index,
        //as stated in the requirements for maximum items on one order - 4.
        int rand_amount_product_in_order = rand.Next(0, 3);
        for (int i = 0; i < 20; i += rand_amount_product_in_order)
        {
            int rand_index_order = rand.Next(20);
            for (int j = 0; j < rand_amount_product_in_order; j++)
            {
                DO.OrderItem oi = new DO.OrderItem();
                int rand_index_product = rand.Next(10);
                //Since this is a musical instrument store, the maximum amount you can order from one instrument is 3 (this is also quite excessive.)
                int rand_amount = rand.Next(1, 3);
                oi.ID = Config.orderItem_ID;
                oi.ProductId = Product_arr[rand_index_product].ID;
                oi.OrderId = Order_arr[rand_index_order].ID;
                oi.Price = Product_arr[rand_index_product].Price;
                oi.Amount = rand_amount;
                AddOrderItem(oi);
            }
        }
    }

}

