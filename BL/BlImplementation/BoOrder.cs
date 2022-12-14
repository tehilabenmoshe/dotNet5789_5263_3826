using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
//using BO;
using DalApi;

namespace BlImplementation;

internal class BoOrder : IBoOrder
{
    private IDal? Dal = DalApi.Factory.Get();

    public IEnumerable<BO.OrderForList> getOrderForList()
    {
        List<DO.Order> temp = Dal.Order.GetAll().ToList();
        List<BO.OrderForList> orders=new List<BO.OrderForList>();
        DO.Order o=new DO.Order();
        BO.OrderForList d = new BO.OrderForList();
       foreach(DO.Order? or in temp)
        {
            
        }




        return orders;
    }







}
