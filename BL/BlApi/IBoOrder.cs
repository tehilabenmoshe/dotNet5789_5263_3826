using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BO;
namespace BlApi;
public interface IBoOrder
{
    public IEnumerable<BO.OrderForList> getOrderForList();//
    public BO.Order GetOrder(int ID);//
    public BO.Order UpdateShipOrder(int ID);//
    public BO.Order UpdateProvisionOrder(int ID);//
    public BO.OrderTracking TrackOrder(int ID);
    public BO.Order UpdateOrder(BO.Product product, int amount);
    public BO.OrderStatus getStatus(BO.Order o);
    public BO.Order? DoOrderToBo(DO.Order order);
}
