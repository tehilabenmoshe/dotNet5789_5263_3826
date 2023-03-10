

using DalApi;
using DO;
using System.Collections.ObjectModel;
using System.Reflection.Metadata;

namespace Dal;

 public class DataSource
 {
    internal static DataSource s_instance { get; } = new DataSource();
    private DataSource() { s_Initialize(); }
    public readonly Random rand = new Random();

    //initialize:
    private void s_Initialize()
    {
        CreateOrder();
        CreateProduct();
        CreateOrderItem();
    }

    //list of the entities:
    internal List<Product?> ListProduct { get; } = new List<Product?>() { };
    internal List<Order?> ListOrder { get; } = new List<Order?>() { };
    internal List<OrderItem?> ListOrderItem { get; } = new List<OrderItem?>() { };


    
    //random number for each entity:
    public static class ConfigOrder
    {
        internal const int s_startOrderNumber = 999;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => ++s_nextOrderNumber; }
    }
    internal static class ConfigOrderItem
    {
        internal const int s_startOrderItemNumber = 999;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => ++s_nextOrderItemNumber; }

    }
    internal static class ConfigProduct
    {
        internal const int s_startProductNumber = 99999;
        private static int s_nextProductNumber = s_startProductNumber;
        internal static int NextProductNumber { get => ++s_nextProductNumber; }
    }

    
    //the name and address - just in order to show them in clear
    string[] names = {"Tehila", "Maayan","Shira","Avi","Dani","Nurit","Miryam","Shirel","Tamar","Avraham","Yitzchak","Ivy","Shulamit","Moriya","Yael","Moshe","Yakov","Bibi","Yossi","Mendi", };
    string[] cities = { "Jerusalem", "BeerSheva", "Lod", "Rehovot", "Eilat", "TelAviv", "Hevron", "Tzfat", "Netivot", "Naharia", "Netanya", "Ashkelon", "Ashdod", "Ramla", "KfarSaba", "Efrat", "Elazar", "BetShemesh", "Hulon", "Gadera" };

    
    //initialize order list:
    private void CreateOrder()  
    {
        for (int i = 0,z=1; i < 20; i++,z++)
        {
            Order o = new Order();
            o.ID = ConfigOrder.NextOrderNumber; //create an id
            o.CustomerName = " " + names[i];
            o.CustomerEmail = " " + o.CustomerName + "@gmail.com";
            o.CustomerAddress = " " + cities[i] + " " + i + z + "/" + z + 3;//random adress+city
            //o.OrderDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)); //order befor ship
            //o.ShipDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//ship befor deliverd
            //o.DeliveryDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));

            o.DeliveryDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));
            o.ShipDate = o.DeliveryDate - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L));//ship befor deliverd
            o.OrderDate = o.ShipDate - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 10L)); //order befor ship

            if (i<=16 && i >= 8) //certen amount of order status define different:
            {
                o.DeliveryDate =null;
                o.ShipDate = null;
            }

            if (i <= 8)
                o.DeliveryDate = null;

            ///ListOrder.(o);
            ListOrder.Add(o); //adding the new order to the list
        }
    }


    //initialize product list: setting 10 defults product 
    private void CreateProduct()
    {
        Product p1 = new Product();
        p1.Category = Category.garden;
        p1.Name = "Oak rocking chair";
        p1.Price = rand.Next(100, 300); //random number for the price
        p1.ID = ConfigProduct.NextProductNumber; //random number wirh 6 digit for id
        p1.InStock = 20;

        Product p2 = new Product();
        p2.Category = Category.bedRoom;
        p2.Name = "Double oak bed";
        p2.Price = rand.Next(7999, 10000);
        p2.ID = ConfigProduct.NextProductNumber;
        p2.InStock = 15;

        Product p3 = new Product();
        p3.Category = Category.livingRoom;
        p3.Name = "Leather sofa";
        p3.Price = rand.Next(2500, 5000);
        p3.ID = ConfigProduct.NextProductNumber;
        p3.InStock = 12;

        Product p4 = new Product();
        p4.Category = Category.kitchen;
        p4.Name = "Marble bar table";
        p4.Price = rand.Next(7500, 10250);
        p4.ID = ConfigProduct.NextProductNumber;
        p4.InStock = 8;

        Product p5 = new Product();
        p5.Category = Category.diningRoom;
        p5.Name = "Arc Floor Lamp";
        p5.Price = rand.Next(289, 460);
        p5.ID = ConfigProduct.NextProductNumber;
        p5.InStock = 2;

        Product p6 = new Product();
        p6.Category = Category.garden;
        p6.Name = "rectangular wooden bench";
        p6.Price = rand.Next(490, 799);
        p6.ID = ConfigProduct.NextProductNumber;
        p6.InStock = 6;

        Product p7 = new Product();
        p7.Category = Category.livingRoom;
        p7.Name = "Corduroy sofa";
        p7.Price = rand.Next(3300, 7590);
        p7.ID = ConfigProduct.NextProductNumber;
        p7.InStock = 3;

        Product p8 = new Product();
        p8.Category = Category.bedRoom;
        p8.Name = "Single oak bed";
        p8.Price = rand.Next(7800, 9999);
        p8.ID = ConfigProduct.NextProductNumber;
        p8.InStock = 19;

        Product p9 = new Product();
        p9.Category = Category.kitchen;
        p9.Name = "Transparent plastic bar stool";
        p9.Price = rand.Next(489, 835);
        p9.ID = ConfigProduct.NextProductNumber;
        p9.InStock = 50;

        Product p10 = new Product();
        p10.Category = Category.diningRoom;
        p10.Name = "Round cherry wood dining table";
        p10.Price = rand.Next(1500, 4329);
        p10.ID = ConfigProduct.NextProductNumber;
        p10.InStock = 0;

        //push the products into the listproduct
        ListProduct.Add(p1);
        ListProduct.Add(p2);
        ListProduct.Add(p3);
        ListProduct.Add(p4);
        ListProduct.Add(p5);
        ListProduct.Add(p6);
        ListProduct.Add(p7);
        ListProduct.Add(p8);
        ListProduct.Add(p9);
        ListProduct.Add(p10);
    }


    //initialize orderItem List:
    private void CreateOrderItem()
    { 
        for (int i = 0; i < 20; i++)
        {
            Product? product = ListProduct[rand.Next(10)];// choose randomly product
            int itemsInOrder = rand.Next(1, 5);
            Order? order = ListOrder[i]; //creat list order 
            for (int j = 0; j < itemsInOrder; j++) //for each time- create a new order and fiil it with details
            {
                OrderItem orderItem = new OrderItem()
                {
                    ID = ConfigOrderItem.NextOrderItemNumber,
                    ProductID = product?.ID ?? 0,
                    OrderID = order?.ID ?? 0,
                    Price = product?.Price ?? 0,
                    Amount = rand.Next(2, 5), //rand num for the amount

                };
                ListOrderItem.Add(orderItem); //add to the list
                product = ListProduct[rand.Next(10)];
            }



        }







    }

 }
