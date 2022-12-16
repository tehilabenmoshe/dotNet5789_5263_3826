using BlApi;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static BlApi.Factory;
namespace BlImplementation;


//namespace BlImplementation;

//sealed public class BL :IBL
//{
//    public IBoCart BoCart { get; } = new BlImplementation.BoCart();
//    public IBoOrder BoOrder { get; } = new BlImplementation.BoOrder();
//    public IBoProduct BoProduct { get; } = new BlImplementation.BoProduct();


//}

public class Bl : IBL
{

    public Bl() { }

    public IBoOrder Order { get; set; } = new BoOrder();

    public IBoProduct Product { get; set; } = new BoProduct();

    public IBoCart Cart { get; set; } = new BoCart();

}