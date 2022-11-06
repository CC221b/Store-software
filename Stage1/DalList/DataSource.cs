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
        static internal int s_indexOrder = 0, s_indexProduct = 0, s_indexOrderItem;
        private static int s_orderId = 222222, s_orderItemId = 123456;

        public static int OrderId
        {
            get { return s_orderId++; }
        }
        public static int OrderItemId
        {
            get { return s_orderItemId++; }
        }
    }
    //Defining a random variable.
    public static readonly Random Rand = new Random(0);

    //Creating entity arrays.
    internal static DO.Order[] s_orderArr = new DO.Order[100];
    internal static DO.Product[] s_productArr = new DO.Product[50];
    internal static DO.OrderItem[] s_orderItemArr = new DO.OrderItem[200];

    //Adding functions to entity arrays.
    public static void AddProduct(DO.Product product) { s_productArr[Config.s_indexProduct++] = product; }
    public static void AddOrder(DO.Order order) { s_orderArr[Config.s_indexOrder++] = order; }
    public static void AddOrderItem(DO.OrderItem orderItem) { s_orderItemArr[Config.s_indexOrderItem++] = orderItem; }

    /// <summary>
    /// A function that initializes 10 products.
    /// </summary>
    private static void s_Initialize()
    {
        // An array of 10 items entered manually.
        (string, int, DO.Categories)[] productArr = new[] {
            ("Acoustic_Guitar", 600, DO.Categories.Stringed),
            ("Electro_acoustic_Guitar", 900, DO.Categories.Stringed),
            ("Piccolo_Trumpet", 8700,DO.Categories.Wind),
            ("Pocket_Trumpet", 3700, DO.Categories.Wind),
            ("Bass_Clarinet", 22000, DO.Categories.Wind),
            ("Contra_alto_Clarinet", 620, DO.Categories.Wind),
            ("Bass_Drum", 600,DO.Categories.Percussion),
            ("Grand_Piano", 1450,DO.Categories.Keyboard),
            ("Upright_Piano", 11000,DO.Categories.Keyboard),
            ("Semi_Acoustic_Violin",3000,DO.Categories.Stringed),
            ("Five_String_Violin", 400,DO.Categories.Stringed)};

        (string, string, string)[] customersArr = new[] {
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
        int randProductId = Rand.Next(333333, 666666);

        //add 10 product to product_arr.
        for (int i = 0; i < productArr.Length; i++)
        {
            int randNumInStock = Rand.Next(100);
            DO.Product p = new Product();
            //I added the index to make sure that a different number will be created each time.
            p._id = randProductId + i;
            p._name = productArr[i].Item1;
            p._price = productArr[i].Item2;
            p._category = productArr[i].Item3;
            p._inStock = randNumInStock;
            AddProduct(p);
        }

        int randomIndex = (int)Rand.NextInt64(0, 19);
        //add 20 order to order_arr.
        for (int i = 0; i < 20; i++)
        {
            DO.Order o = new DO.Order();
            //Automatic ID defined in config.
            o._id = Config.OrderId;
            o._customerName = customersArr[i].Item1;
            o._customerEmail = customersArr[i].Item2;
            o._customerAdress = customersArr[i].Item3;
            o._orderDate = DateTime.Now;
            TimeSpan t = new TimeSpan((int)Rand.NextInt64(1, 3), 0, 0, 0);
            o._shipDate = (randomIndex % 20) % 5 != 0 ? o._orderDate.Add(t) : DateTime.MinValue;
            t = new TimeSpan((int)Rand.NextInt64(3, 7), 0, 0, 0);
            o._deliveryDate = (randomIndex % 20) % 3 != 0 ? o._shipDate.Add(t) : DateTime.MinValue;
            AddOrder(o);
        }

        //add 40 orderItem to orderItem_arr.

        //The first one was created to make sure that all 20 orders will surely be a private order, not randomly.
        for (int i = 0; i < 20; i++)
        {
            DO.OrderItem oi = new DO.OrderItem();
            int randIndexProduct = Rand.Next(10);
            //Since this is a musical instrument store, the maximum amount you can order from one instrument is 3 (this is also quite excessive.)
            int randAmount = Rand.Next(1, 3);
            oi._id = Config.OrderItemId;
            oi._productId = s_productArr[randIndexProduct]._id;
            oi._orderId = s_orderArr[i]._id;
            oi._amount = randAmount;
            oi._price = s_productArr[randIndexProduct]._price * randAmount;

            AddOrderItem(oi);
        }

        //The second loop randomly adds order details to the drawn order.
        //A number between 0 and 3 has been drawn and according to this we will advance the loop index,
        //as stated in the requirements for maximum items on one order - 4.
        int indexOrder = 0;
        int randAmountProductInOrder = Rand.Next(0, 3);
        for (int i = 0; i < 20; i += randAmountProductInOrder)
        {
            for (int j = 0; j < randAmountProductInOrder; j++)
            {
                DO.OrderItem oi = new DO.OrderItem();
                int randIndexProduct = Rand.Next(10);
                //Since this is a musical instrument store, the maximum amount you can order from one instrument is 3 (this is also quite excessive.)
                int randAmount = Rand.Next(1, 3);
                oi._id = Config.OrderItemId;
                oi._productId = s_productArr[randIndexProduct]._id;
                oi._orderId = s_orderArr[indexOrder]._id;
                oi._amount = randAmount;
                oi._price = s_productArr[randIndexProduct]._price * randAmount;
                AddOrderItem(oi);
            }
            indexOrder++;
            randAmountProductInOrder = Rand.Next(0, 3);
        }
    }

}

