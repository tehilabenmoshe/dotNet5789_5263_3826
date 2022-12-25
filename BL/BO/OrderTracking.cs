using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderTracking
{
    /// <summary>
    /// order id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// order status
    /// </summary>
    public OrderStatus? Status { get; set; }
    //public List<DateTime,string>

    /// <summary>
    /// function- print OrderTracking class
    /// </summary>
    /// <returns></returns>

    public List<Tuple<DateTime, string>>? Tracking { set; get; }

    public override string ToString() => $@"
        ID:{ID}
        Order status:{Status}
";

}
