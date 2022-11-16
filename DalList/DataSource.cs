

using DO;

namespace Dal;

 internal class DataSource
{

    internal static class Config
    {
        internal const int s_tartOrderNumber = 1000;

        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNbumber { get => ++s_nextOrderNumber; } 
    }

    private void s_Initialize() {
        CreateOrder();
        CreateProduct();
        CreateOrderItem();
   }


    private DataSource() //defult ctor
    {
        s_Initialize();
    }

    private void CreateOrder()
    {
        string[] garden = { "Oak rocking chair", "rectangular wooden bench", "Round outdoor table", "hammock", "patio umbrella", "Grilanda", "wooden chair", "Rattan garden sofa" };
        string[] livingRoom = { "Leather sofa", "Corduroy sofa", "Linen sofa", "Square coffee table", "Glass side table", "Rectangular TV stand", "Long linen curtain", "Solid wood library" };
        string[] bedRoom = { };



    }
    private void CreateProduct() {

       

        string[] garden = { "Oak rocking chair", "rectangular wooden bench", "Round outdoor table", "hammock", "patio umbrella"};
        string[] livingRoom = { "Leather sofa", "Corduroy sofa", "Linen sofa", "Square coffee table", "Glass side table", "Rectangular TV stand" };
        string[] bedRoom = { "Double oak bed", "Single oak bed", "Sliding door glass closhet", "Walnut wood Vanity table", "Cedar wood desk" };
        string[] kitchen = { "Marble bar table", "Transparent plastic bar stool", "Mahogany wood pantry", "wood wine racks" };
        string[] diningRoom = { "Round cherry wood dining table", "Walnut square dining table", "Leather dining chair", "Arc Floor Lamp" };
         //להגריל קטגוריה ואז להגריל מיקום במערך. את התוצאה להכניס 
        for(int i=0; i < 10; i++)
        {
            Product p = new Product();
            p.ID = Random();
            p.Category =( )
                p.Name =
                p.GetType();
             



        }

    }

    private void CreateOrderItem() {
        for (int i = 0; i < 10; i++)
        {
            Product? product = ListProduct[s_rand.Next(ListProduct.Count)];
            ListOrderItem.Add(
              new OrderItem
              {
                  OrderID = s_rand.Next(Config.s_startOrderNumber, Config.s_starrOrderNumber + ListOrder.Count),
                  ProductID = product?.ID ?? 0,
                  Price = product?.Price ?? 0,
                  Amount = s_rand.Next(5),
              });
        }

    }
    internal static DataSource s_instance { get; } = new DataSource(); //ניגשים אליו בכל פעם שרוצים לגשת לנתונים כך: DataSource _ds = DataSource.s_instance;

    private static readonly Random s_rand= new Random();

    private DataSource() => s_Initialize();





    internal  List<Product?> ListProduct { get; } = new List<Product?>() { };

    internal  List<Order?> ListOrder { get; } = new List<Order?>() { };


    internal  List<OrderItem?> ListOrderItem { get; } = new List<OrderItem?>() { };

    //לפחות 10 מוצרים
  //  לפחות  20 הזמנות
  //  לפחות 40 פריטי הזמנה(1 עד 4 להזמנה בודדת)



    


}
