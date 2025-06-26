using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Do;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BL.Services
{
  
    public class BlOrdersService : IBlOrders
    {  
        IDal dal;
        IBldetailingModels bldetailing;
        IBlDetailingOrders bldetailingOrders;
        private readonly EmailService _emailService;
        public BlOrdersService(IDal dal,IBldetailingModels dmodels, IBlDetailingOrders bldetailingOrders, EmailService emailService)
        {
            this.dal = dal;
            bldetailing = dmodels;
            this.bldetailingOrders = bldetailingOrders;
            _emailService = emailService;
        }
        //האם לא אמורה ליהיות אפשרות גישה
        public List<BlOrder> GetOrders()
        {
            var oList = dal.Orders.GetOrders();
            List<BlOrder> list = new();
           
            oList.ForEach(o => {

                var oo = new BlOrder()
                { IdOrder = o.IdOrder,
                    IdSchool = o.IdSchool,
                    Contact = o.Contact,
                    PhoneContact = o.PhoneContact,
                    ProvisionAddress = o.ProvisionAddress
                , DateOfOrdder = o.DateOfOrdder, DateOfEvent = o.DateOfEvent,
                    CostPrice = o.CostPrice,
                    SchoolName = o.SchoolName,
            };
            o.DetailingOrders.ToList().ForEach(x =>
            {
                var y = new BlDetailingOrder()
                {
                    IdOrder = x.IdOrder,
                    Count = x.Count,
                    Id = x.Id,
                    IdModel = x.IdModel,
                    Size = x.Size
                };
                oo.DetailingOrders.Add(y);
            });
            list.Add(oo);
                
                }
            ) ;
               return list; 
        }
        public BlOrder GetById(int id)
        {
           var list = GetOrders();
            return list.Find(x => x.IdOrder == id);
        }

        public List<BlOrder> GetByDate(DateTime d)
        {
            var o = GetOrders();
            return o.FindAll(x => x.DateOfEvent == d);
        }

        //public int GetCountModelByDateAndSize(DateTime date,int model,int size)
        //{
        //    var o = GetOrders();

        //    return o.Find(x => x.DateOfEvent == date && x.DetailingOrders.Find(y => y.IdModel == model && y.Size == size).Count >0);

        //    //o.FindAll(x => x.DateOfEvent == date && (x.DetailingOrders.FindAll(x1 => x1.IdModel == model && x1.Size.Equals(size)).Count > 0));

        //}
        public int GetCountModelByDateAndSize(DateTime date, int model, int size)
        {
            var o = GetOrders();

            // מחפשים את כל ההזמנות בתאריך הנתון
            var ordersOnDate = o.FindAll(x => x.DateOfEvent == date);

            // סופרים כמה פריטים מהדגם והמידה הנתונים הוזמנו בתאריך זה
            int totalItemsOrdered = 0;

            foreach (var order in ordersOnDate)
            {
                // מחפשים את כל פרטי ההזמנה שמתאימים לדגם ולמידה
                var matchingItems = order.DetailingOrders.FindAll(item => item.IdModel == model && item.Size == size);

                // מוסיפים את הכמות של כל פריט מתאים
                foreach (var item in matchingItems)
                {
                    totalItemsOrdered += item.Count;
                }
            }

            return totalItemsOrdered;
        }
        public async void add(BlOrder item)
        {

            List<DetailingOrder> list = new List<DetailingOrder> ();
            foreach (var d in item.DetailingOrders)
            {
                DetailingOrder detailing = new DetailingOrder();
                detailing.IdOrder= d.IdOrder;
                detailing.IdModel = d.IdModel;
                detailing.Size = d.Size;
                detailing.Count = d.Count;
                list.Add(detailing);
              
            }
            Order o = new Order()
            {
                IdOrder = item.IdOrder,

                IdSchool = item.IdSchool,

                Contact = item.Contact,

                PhoneContact = item.PhoneContact,   

                ProvisionAddress = item.ProvisionAddress, 
                
                DateOfOrdder = item.DateOfOrdder,

                DateOfEvent = item.DateOfEvent,

                CostPrice = item.CostPrice,

                SchoolName = item.SchoolName,
                
                DetailingOrders = list,
                //BornDate = new DateTime(DateTime.Today.Year - item.Age, 1, 1)
            };

            dal.Orders.add(o);

            //email

         
        }

     
        public void delete(int id) {         
            BlOrder o=GetById(id);
            List<BlDetailingOrder> toDelete = bldetailingOrders.GetDetailingOrderById(id);
            foreach (BlDetailingOrder b in toDelete)
            {
                bldetailingOrders.delete(b.Id);
            }
            Order o2 = new Order()
            {
                IdOrder = o.IdOrder,

                IdSchool = o.IdSchool,

                Contact = o.Contact,

                PhoneContact = o.PhoneContact,

                ProvisionAddress = o.ProvisionAddress,

                DateOfOrdder = o.DateOfOrdder,

                DateOfEvent = o.DateOfEvent,

                CostPrice = o.CostPrice
            };
            dal.Orders.delete(o2);
        }
        public void edit(BlOrder? item)
        {
            Order? o = new Order()
            {
                IdOrder = item.IdOrder,

                IdSchool = item.IdSchool,

                Contact = item.Contact,

                PhoneContact = item.PhoneContact,

                ProvisionAddress = item.ProvisionAddress,

                DateOfOrdder = item.DateOfOrdder,

                DateOfEvent = item.DateOfEvent,

                CostPrice = item.CostPrice
            };
            dal.Orders.edit(o);
        }

        public BlOrder GetOrdersById(int id)
        {
            var o = GetOrders();

            return o.Find(x => x.IdOrder == id);
        }
    }
}
