using BlApi;

namespace BlTest;

internal class Program
{
    static void Cart(IBoCart cart)
    {
        try
        {
            Console.WriteLine(@"checking cart:
                Type your choice:
                a - ADD A NEW PRODUCT TO CART
                b - UPDATE PRODUCT IN CART
                c - MAKE CART");

            string choice = Console.ReadLine();//receiving the users choice
            switch (choice)//operating the chosen submenu according to the users input 
            {
                case "a":



                case "b":


                case "c":
                    break;



            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }


    static void Order(IBoOrder order)
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

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void Product(IBoProduct product)
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

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    static void Main(string[] args)
    {

    }




}


