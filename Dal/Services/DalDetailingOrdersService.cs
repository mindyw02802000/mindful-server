using Dal.Api;
using Dal.Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class DalDetailingOrdersService: IDalDetailingOrdes
    {
        dbcontext dbcontext;



        public DalDetailingOrdersService(dbcontext data)
        {
            dbcontext = data;
        }
        public List<DetailingOrder> GetDetailingOrders()
        {
            return dbcontext.DetailingOrders.ToList();
        }
        public void update(DetailingOrder m)
        {
            dbcontext.DetailingOrders.Add(m);
            dbcontext.SaveChanges();
        }

        public void remove(DetailingOrder m)
        {
            var m2 = dbcontext.DetailingOrders.ToList().Find(x => x.IdOrder == m.IdOrder);
            if (m2 != null)
            {
                dbcontext.DetailingOrders.Remove(m2);
                dbcontext.SaveChanges();
            }
        }

        public void edit(DetailingOrder m)
        {
            DetailingOrder? m2 = dbcontext.DetailingOrders.ToList().Find(x => x.IdOrder == m.IdOrder);
            if (m2 != null)
            {
                m2.Id = m.Id;
                m2.IdOrder = m.IdOrder;
                m2.IdModel = m.IdOrder;
                m2.Size = m.Size;
                m2.Count = m.Count;
                dbcontext.SaveChanges();
            }
        }

       
    }
}

    

