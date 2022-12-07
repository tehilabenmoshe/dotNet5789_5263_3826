using BlImplementation;
namespace BlApi;

public static class Factory
{
    public static IBL GetBL()
    {
        IBL bL = new Bl();
        return bL;

    }
}
