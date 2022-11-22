// See https://aka.ms/new-console-template for more information

namespace Dal;
using System;
using DalApi;
using DO;
class Program
{
    static void OrderCheck(DalOrder order)
    {
        try
        {
            Console.WriteLine(@"checking order:
                Type your choice:
                a - ADD A NEW ORDER
                b - GET BY ID
                c - GET ALL ORDERS IN THE LIST
                d - UPDATE THE ORDER
                e - DELETE EXSIST ORDER");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "a":
                    Order o=new Order();
                    int id;
                    Console.WriteLine(@"Enter Order ID:");
                    int.TryParse(Console.ReadLine(), out id);
                    o.ID = id;

                    Console.WriteLine(@"Enter costumer name:");
                    string name;
                    name = Console.ReadLine();
                    o.CustomerName = name;


                    Console.WriteLine(@"Enter costumer email:");
                    string email;
                    email = Console.ReadLine();
                    o.CustomerEmail= email;

                    Console.WriteLine(@"Enter costumer adress:");
                   o.CustomerAdress = Console.ReadLine();

                    order.Add(o);
                    break;

                case "b":
                    Console.WriteLine(@"Enter Order ID:");
                    int.TryParse(Console.ReadLine(), out id);
                    int getId = id;
                    Console.WriteLine(order.GetById(getId));
                    break;

                case "c":
                    foreach (Order? item in order.GetAll())
                    {
                        Console.WriteLine(item);
                    }
                    break;


                case "d":
                    Order orderTemp = new Order();
                    Console.WriteLine(@"Enter Order ID:");
                    int.TryParse(Console.ReadLine(), out id);
                    orderTemp.ID = id;
                    Console.WriteLine(order.GetById(id));
                    Console.WriteLine(@"Enter new costumer name");
                    orderTemp.CustomerName = Console.ReadLine();
                    Console.WriteLine(@"Enter new costumer email:");
                    orderTemp.CustomerEmail = Console.ReadLine();
                    Console.WriteLine(@"Enter new costumer adress:");
                    orderTemp.CustomerAdress = Console.ReadLine();
                    order.Update(orderTemp);
                    break;


                case "e":
                    Console.WriteLine("Enter product ID");
                    int.TryParse(Console.ReadLine(), out id);
                    int i = id;
                    order.Delete(i);
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void OrderItemCheck(DalOrderItem item)
    {
        try
        {
            Console.WriteLine(@"checking order-Item:
                Type your choice:
                a - ADD A NEW ORDER-ITEM
                b - GET BY ID
                c - GET ALL ORDER-ITEMS IN THE LIST
                d - UPDATE THE ORDER-ITEM
                e - DELETE EXSIST ORDER-ITEM");
            string option = Console.ReadLine();
            switch (option)
            {
                case "a":
                    OrderItem myItem = new OrderItem();
                    Console.WriteLine("Enter order-item ID");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    myItem.ID = id;
                    Console.WriteLine("Enter product ID");
                    int.TryParse(Console.ReadLine(), out id);
                    myItem.ProductID = id;
                    Console.WriteLine("Enter Order ID");
                    int.TryParse(Console.ReadLine(), out id);
                    myItem.OrderID = id;
                    Console.WriteLine("Enter order-item price");
                    double myPrice;
                    double.TryParse(Console.ReadLine(), out myPrice);
                    myItem.Price = myPrice;
                    Console.WriteLine("Enter order-item amount");
                    int.TryParse(Console.ReadLine(), out id);
                    myItem.Amount = id;
                    item.Add(myItem);
                    break;


                case "b":
                    Console.WriteLine("Enter order-item ID");
                    int tmpId;
                    int.TryParse(Console.ReadLine(), out tmpId);
                    Console.WriteLine(item.GetById(tmpId));
                    break;

                case "c":
                    foreach (OrderItem myOrderItem in item.GetAll())
                    {
                        Console.WriteLine(myOrderItem);
                    }
                    break;


                case "d":
                    OrderItem tmpOrderItem = new OrderItem();
                    Console.WriteLine("Enter item ID");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpOrderItem.ID = id;
                    Console.WriteLine(item.GetById(id));
                    Console.WriteLine("Enter product ID");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpOrderItem.ProductID = id;
                    Console.WriteLine("Enter Order ID");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpOrderItem.OrderID = id;
                    Console.WriteLine("Enter order-item price");
                    double tmpPrice;
                    double.TryParse(Console.ReadLine(), out tmpPrice);
                    tmpOrderItem.Price = tmpPrice;

                    Console.WriteLine("Enter order-item amount");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpOrderItem.Amount = id;
                    item.Update(tmpOrderItem);
                    break;

                case "e":
                    Console.WriteLine("enter the product ID");
                    int.TryParse(Console.ReadLine(), out id);
                    item.Delete(id);
                    break;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void ProductCheck(DalProduct product)
    {
        try
        {
            Console.WriteLine(@"checking order-Item:
                Type your choice:
                a - ADD A NEW PRODUCT
                b - GET BY ID
                c - GET ALL PRODUCT IN THE LIST
                d - UPDATE THE PRODUCT
                e - DELETE EXSIST PRODUCT");
            string option = Console.ReadLine();
            switch (option)
            {
                case "a":
                    Product tmpProduct = new Product();
                    Console.WriteLine("Enter product ID");
                    int id;
                    int.TryParse(Console.ReadLine(), out id);
                    tmpProduct.ID = id;
                    Console.WriteLine("Enter product name:");
                    tmpProduct.Name = Console.ReadLine();
                    Console.WriteLine(@"Enter product catgory:
                                        garden-0,
                                        kitchen-1,
                                        livingRoom-2,
                                        bedRoom-3,
                                        diningRoom-4");
                    int.TryParse(Console.ReadLine(), out id);
                    int myCategory = id;
                    switch (myCategory)
                    {
                        case 0:
                            tmpProduct.Category = Category.garden;
                            break;
                        case 1:
                            tmpProduct.Category = Category.kitchen;
                            break;
                        case 2:
                            tmpProduct.Category = Category.livingRoom;
                            break;
                        case 3:
                            tmpProduct.Category = Category.bedRoom;
                            break;
                        case 4:
                            tmpProduct.Category = Category.diningRoom;
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }
                    Console.WriteLine("Enter product price:");
                    double myPrice;
                    double.TryParse(Console.ReadLine(), out myPrice);
                    tmpProduct.Price = myPrice;
                    Console.WriteLine("Enter product amount:");
                    int.TryParse(Console.ReadLine(), out id);
                    tmpProduct.InStock = id;
                    product.Add(tmpProduct);
                    break;

                case "b":
                    Console.WriteLine("Enter product ID:");
                    int myId;
                    int.TryParse(Console.ReadLine(), out myId);
                    Console.WriteLine(product.GetById(myId));
                    break;

                case "c":
                    foreach (Product item in product.GetAll())
                    {
                        Console.WriteLine(item);
                    };
                    break;

                case "d":
                    Product myProduct = new Product();
                    Console.WriteLine("Enter product ID:");
                    int.TryParse(Console.ReadLine(), out id);
                    myProduct.ID = id;
                    Console.WriteLine(product.GetById(id));
                    Console.WriteLine("Enter product name:");
                    myProduct.Name = Console.ReadLine();
                    Console.WriteLine(@"enter the catgory:
                                         garden-0,
                                        kitchen-1,
                                        livingRoom-2,
                                        bedRoom-3,
                                        diningRoom-4");
                    int.TryParse(Console.ReadLine(), out myCategory);
                    switch (myCategory)
                    {
                        case 0:
                            myProduct.Category = Category.garden;
                            break;
                        case 1:
                            myProduct.Category = Category.kitchen;
                            break;
                        case 2:
                            myProduct.Category = Category.livingRoom;
                            break;
                        case 3:
                            myProduct.Category = Category.bedRoom;
                            break;
                        case 4:
                            myProduct.Category = Category.diningRoom;
                            break;
                        default:
                            Console.WriteLine("ERROR");
                            break;
                    }

                    double tempPrice;
                    Console.WriteLine("Enter product price");
                    double.TryParse(Console.ReadLine(), out tempPrice);
                    myProduct.Price = tempPrice;
                    Console.WriteLine("Enter product amount");
                    int.TryParse(Console.ReadLine(), out id);
                    myProduct.InStock = id;
                    product.Update(myProduct);
                    break;

                case "e":
                    Console.WriteLine("Enter product ID");
                    int.TryParse(Console.ReadLine(), out myId);
                    product.Delete(myId);
                    break;
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void Main(string[] args)
    {
        DalProduct product = new DalProduct();
        DalOrder order = new DalOrder();
        DalOrderItem item = new DalOrderItem();
        int num = 1;
        while (num != 0)
        {
            Console.WriteLine(@"
                Type your choice:
                0-exit
                1-check Order
                2-check OrderItem
                3-check Product");
            string choice = Console.ReadLine();
            bool b = int.TryParse(choice, out num);
            if (!b)
            {
                Console.WriteLine("ERROR");
                break;
            }
            switch (num)
            {
                case 1:
                    OrderCheck(order);
                    break;
                case 2:
                    OrderItemCheck(item);
                    break;
                case 3:
                    ProductCheck(product);
                    break;
                default:
                    break;
            }

        }
    }
}
