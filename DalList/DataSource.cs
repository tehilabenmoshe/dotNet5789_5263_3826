

using DalApi;
using DO;
using System.Reflection.Metadata;

namespace Dal;

 internal class DataSource

{
    
    internal static class Config
    {
        internal const int startOrderNumber = 1000;

        private static int SnextOrderNumber = startOrderNumber;
        internal static int NextOrderNbumber { get => ++SnextOrderNumber; } 
    }

    internal static DataSource s_instance { get; } = new DataSource(); //ניגשים אליו בכל פעם שרוצים לגשת לנתונים כך: DataSource _ds = DataSource.s_instance;

    private DataSource() //defult ctor
    {
        s_Initialize();
    }
    public readonly Random rand = new Random();
    private void s_Initialize() {
        CreateOrder();
        CreateProduct();
        CreateOrderItem();
   }


   

    string[] names = {"Tehila", "Maayan","Shira","Avi","Dani","Nurit","Miryam","Shirel","Tamar","Avraham","Yitzchak","Ivy","Shulamit","Moriya","Yael","Moshe","Yakov","Bibi","Yossi","Mendi", };
    string[] cities = { "Jerusalem", "BeerSheva", "Lod", "Rehovot", "Eilat", "TelAviv", "Hevron", "Tzfat", "Netivot", "Naharia", "Netanya", "Ashkelon", "Ashdod", "Ramla", "KfarSaba", "Efrat", "Elazar", "BetShemesh", "Hulon", "Gadera" };

    private void CreateOrder()//סיימתי את זה- צריך לבדוק את הזמנים של התאריכים  
    {
        for (int i = 0,z=1; i < 20; i++,z++)
        {
            Order o = new Order();
            o.ID = Config.NextOrderNbumber; //create an id
            o.CustomerName = " " + names[i];
            o.CustomerEmail = " " + o.CustomerName+"@gmail.com";//check if works!!!
            o.CustomerAddress = " "+cities[i]+" " + i+z + "/" + z+3;//random adress+city
            o.DeliveryDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 100L));
            o.ShipDate = o.DeliveryDate - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 50L));
            o.OrderDate = o.ShipDate - new TimeSpan(rand.NextInt64(10L * 1000L * 1000L * 3600L * 24L * 20L));



            if (i >= 12) //60% from the shipDate has a Delivery Date
                o.DeliveryDate = DateTime.MinValue;

            if (i >= 16) //80% from the ShipOrders after the creation of the OrderDate
                o.ShipDate = DateTime.MinValue; //initilize to min value

            ListOrder.Add(o); //adding the new order to the list
        }
    }
    private void CreateProduct()
    {
        //string[] garden = { "Oak rocking chair", "rectangular wooden bench", "Round outdoor table", "hammock", "patio umbrella"};
        //string[] livingRoom = { "Leather sofa", "Corduroy sofa", "Linen sofa", "Square coffee table", "Glass side table", "Rectangular TV stand" };
        //string[] bedRoom = { "Double oak bed", "Single oak bed", "Sliding door glass closhet", "Walnut wood Vanity table", "Cedar wood desk" };
        //string[] kitchen = { "Marble bar table", "Transparent plastic bar stool", "Mahogany wood pantry", "wood wine racks" };
        //string[] diningRoom = { "Round cherry wood dining table", "Walnut square dining table", "Leather dining chair", "Arc Floor Lamp" };
        //להגריל קטגוריה ואז להגריל מיקום במערך. את התוצאה להכניס 
        Product p1 = new Product();
        p1.Category = Category.garden;
        p1.Name = "Oak rocking chair";
        p1.Price = rand.Next(100, 300);
        p1.ID = Config.NextOrderNbumber*100;
        p1.InStock = 20;

        Product p2 = new Product();
        p2.Category = Category.bedRoom;
        p2.Name = "Double oak bed";
        p2.Price = rand.Next(7999, 10000);
        p2.ID = Config.NextOrderNbumber*100;
        p2.InStock = 15;

        Product p3 = new Product();
        p3.Category = Category.livingRoom;
        p3.Name = "Leather sofa";
        p3.Price = rand.Next(2500, 5000);
        p3.ID = Config.NextOrderNbumber*100;
        p3.InStock = 12;

        Product p4 = new Product();
        p4.Category = Category.kitchen;
        p4.Name = "Marble bar table";
        p4.Price = rand.Next(7500, 10250);
        p4.ID = Config.NextOrderNbumber*100;
        p4.InStock = 8;

        Product p5 = new Product();
        p5.Category = Category.diningRoom;
        p5.Name = "Arc Floor Lamp";
        p5.Price = rand.Next(289, 460);
        p5.ID = Config.NextOrderNbumber*100;
        p5.InStock = 2;

        Product p6 = new Product();
        p6.Category = Category.garden;
        p6.Name = "rectangular wooden bench";
        p6.Price = rand.Next(490, 799);
        p6.ID = Config.NextOrderNbumber*100;
        p6.InStock = 6;

        Product p7 = new Product();
        p7.Category = Category.livingRoom;
        p7.Name = "Corduroy sofa";
        p7.Price = rand.Next(3300, 7590);
        p7.ID = Config.NextOrderNbumber*100;
        p7.InStock = 3;

        Product p8 = new Product();
        p8.Category = Category.bedRoom;
        p8.Name = "Single oak bed";
        p8.Price = rand.Next(7800, 9999);
        p8.ID = Config.NextOrderNbumber*100;
        p8.InStock = 19;

        Product p9 = new Product();
        p9.Category = Category.kitchen;
        p9.Name = "Transparent plastic bar stool";
        p9.Price = rand.Next(489, 835);
        p9.ID = Config.NextOrderNbumber*100;
        p9.InStock = 50;

        Product p10 = new Product();
        p10.Category = Category.diningRoom;
        p10.Name = "Round cherry wood dining table";
        p10.Price = rand.Next(1500, 4329);
        p10.ID = Config.NextOrderNbumber*100;
        p10.InStock = 0;
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

    private void CreateOrderItem() { //  לבדוק שוב-את הקוד הזה העתקנו מהמצגת של נורית
        for (int i = 0; i < 10; i++)
        {
            Product? product = ListProduct[rand.Next(ListProduct.Count)];
            ListOrderItem.Add(
              new OrderItem
              {
                  ID= Config.NextOrderNbumber * 100,
                  OrderID = rand.Next(Config.startOrderNumber, Config.startOrderNumber + ListOrder.Count),
                  ProductID = product?.ID ?? 0,
                  Price = product?.Price ?? 0,
                  Amount = rand.Next(5),
              });
        }

    }
   
    internal  List<Product?> ListProduct { get; } = new List<Product?>() { };

    internal  List<Order?> ListOrder { get; } = new List<Order?>() { };

    internal  List<OrderItem?> ListOrderItem { get; } = new List<OrderItem?>() { };

    //לפחות 10 מוצרים
  //  לפחות  20 הזמנות
  //  לפחות 40 פריטי הזמנה(1 עד 4 להזמנה בודדת)



    


}
