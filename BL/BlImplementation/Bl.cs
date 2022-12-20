using BlApi;
namespace BlImplementation
{
    public class Bl : IBL
    {
        public Bl() { }

        public IBoOrder Order { get; set; } = new BoOrder();

        public IBoProduct Product { get; set; } = new BoProduct();

        public IBoCart cart { get; set; } = new BoCart();
    }
}
