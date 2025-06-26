using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlDetailingOrders
    {
        //get
        List<BlDetailingOrder> GetDetailingOrder();

        List<BlDetailingOrder> GetDetailingOrderById(int Id);

        ////update הוספה
        void update(BlDetailingOrder item);
        ////delete
        void delete(int id);
        /// editשינוי  
        void edit(BlDetailingOrder item);
        //int getCountBySize(int id, int size);
        //int getCount(int id);
    }
}
