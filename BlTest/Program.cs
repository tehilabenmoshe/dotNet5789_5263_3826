using BlApi;
using BlImplementation;
using BO;
using Dal;
using DalApi;
using DO;
using System.Diagnostics;

namespace BlTset;

internal class Program
{
    static IBL bl = new Bl();
    static Cart? cart = new Cart() { CustomerAddress = "", CustomerEmail = "", CustomerName = "", Items = new List<BO.OrderItem?>(), TotalPrice = 0 };
}