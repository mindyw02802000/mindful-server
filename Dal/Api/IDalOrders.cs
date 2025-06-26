using Dal.Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IDalOrders
    {
        List<Order> GetOrders();    
        void add(Order order);   

        void delete(Order order);

        void edit(Order order);

    }
}
