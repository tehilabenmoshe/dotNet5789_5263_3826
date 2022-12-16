using BlImplementation;
namespace BlApi;

public static class Factory
{
    // public static IBL GetBL()
    public static class BL
    {
        //IBL bL = new Bl();
        //return bL;

        public static IBL Get() => new BlImplementation.Bl();

    }
}
