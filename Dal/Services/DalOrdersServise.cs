using Dal.Api;
using Dal.Do;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class DalOrdersServise : IDalOrders
    {
        dbcontext dbcontext;
        public DalOrdersServise(dbcontext data)
        {
            dbcontext = data;
        }

        //public void delete(Order order)
        //{
        //    var orderr= dbcontext.Orders.ToList().Find(x=> x.IdOrder==order.IdOrder);
        //    if (orderr != null) { 

        //    dbcontext.Orders.Remove(orderr);
        //    dbcontext.SaveChanges();
        //    }
        //}

        public void delete(Order order)
        {
            // חפש את המודל הקיים בקונטקסט
            var orderToDelete = dbcontext.Orders.Include(m => m.DetailingOrders)
                                                  .FirstOrDefault(x => x.IdOrder == order.IdOrder);

            if (orderToDelete != null)
            {
                // נקה את פרטי המודל הקשורים
                orderToDelete.DetailingOrders.Clear();

                // מחק את המודל
                dbcontext.Orders.Remove(orderToDelete);
                dbcontext.SaveChanges();
            }
        }
        public List<Order> GetOrders()
        {
           return dbcontext.Orders.Include(x => x.DetailingOrders).ToList();
          
        }
        public void add(Order item)
        {
            dbcontext.Orders.Add(item);
            dbcontext.SaveChanges();
        }
        public void edit(Order order)
        {
            Order? o = dbcontext.Orders.ToList().Find(x =>x.IdOrder==order.IdOrder );
            if (o != null)
            {
                o.IdOrder = order.IdOrder;
                o.IdSchool = order.IdSchool;
                o.Contact = order.Contact;
                o.PhoneContact = order.PhoneContact;
                o.ProvisionAddress  = order.ProvisionAddress;
                o.DateOfEvent = order.DateOfEvent;
                o.DateOfOrdder = order.DateOfOrdder;
                o.CostPrice = order.CostPrice;
                dbcontext.SaveChanges();

            }


        }

    }
}
