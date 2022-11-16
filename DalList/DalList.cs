using DalApi;


namespace Dal;

sealed internal class DalList:IDal
{
    public static IDal Instance { get; }=new DalList();
    private DalList() { }

    public IOrder Order =>new DalOrder(); //לבדוק
    // להמשיך





}
