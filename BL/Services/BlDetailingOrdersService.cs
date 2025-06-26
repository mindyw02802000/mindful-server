using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BlDetailingOrdersService:IBlDetailingOrders
    {
        IDal dal;
        public BlDetailingOrdersService(IDal dal)
        {

            this.dal = dal;
        }

        public void delete(int id)
        {
            List<BlDetailingOrder> list = GetDetailingOrderById(id);

            list.ForEach(x =>
            {
               DetailingOrder o2 = new DetailingOrder()
                {    
                    IdOrder = x.IdOrder,
                    IdModel = x.IdModel,
                    Size = x.Size,
                    Count = x.Count,
                    Id = x.Id
                };
                dal.DetailingOrdes.remove(o2);
            });
        }
        //עריכה
        public void edit(BlDetailingOrder item)
        {
            DetailingOrder? d = new DetailingOrder()
            {
                IdOrder = item.IdOrder,
                IdModel = item.IdModel,
                Size = item.Size,
                Count = item.Count,
                Id = item.Id
            };

            dal.DetailingOrdes.edit(d);
        }
        public List<BlDetailingOrder> GetDetailingOrderById(int Id)
        {
            var list = GetDetailingOrder();
            return list.FindAll(x => x.IdOrder == Id);
        }

        public List<BlDetailingOrder> GetDetailingOrder()
        {
            var oList = dal.DetailingOrdes.GetDetailingOrders();
            List<BlDetailingOrder> list = new();

            oList.ForEach(o =>
            {
                var y = new BlDetailingOrder()
                {
                    Id = o.Id,
                    IdOrder = o.IdOrder,
                    IdModel = o.IdModel,    
                    Size = o.Size,  
                    Count = o.Count,
                };
                list.Add(y);
            }
          );
            return list;
        }
        public void update(BlDetailingOrder item)
        {
            DetailingOrder d = new DetailingOrder()
            {
                Id = item.Id,
                IdOrder = item.IdOrder,
                IdModel = item.IdModel,
                Size = item.Size,
                Count = item.Count,
            };
            dal.DetailingOrdes.update(d);
        }
    }
}


