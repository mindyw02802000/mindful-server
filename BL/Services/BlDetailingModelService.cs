using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Do;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BlDetailingModelService : IBldetailingModels
    {
        IDal dal;
        IBlOrders orders;

        public BlDetailingModelService(IDal dal, IBlOrders orders)
        {
            this.dal = dal;
            this.orders = orders;
            
        }

        public void delete(int id)
        {
            List<BlDetailingModel> list = GetDetailingModel();

            BlDetailingModel o = list.Find(x => x.Id == id);
            
                DetailingModel o2 = new DetailingModel()
                {
                    IdModel = o.IdModel,
                    Size = o.Size,
                    Count = o.Count,
                    Id = o.Id,
                };
                dal.DetailsModels.remove(o2);
    }
        //עריכה
        public void edit(BlDetailingModel item)
        {

            DetailingModel? d = new DetailingModel()
            {
                IdModel = item.IdModel,
                Size = item.Size,
                Count = item.Count,
                Id = item.Id,
            };
            dal.DetailsModels.edit(d);
        }

        public int getCountBySize(int id, int size)
        {
            var list = GetDetailingModelId(id);
            return list.Find(x =>x.Size == size).Count;
        }

     
        public int CurrentCount(DateTime date, int model, int size)
        {
            // מקבלים את מספר הפריטים שכבר הוזמנו
            var itemsOrdered = orders.GetCountModelByDateAndSize(date, model, size);

            // מקבלים את המלאי הכולל של פריטים מסוג זה
            var totalInventory = getCountBySize(model, size);

            // מחזירים את מספר הפריטים הפנויים
            return totalInventory - itemsOrdered;
        }
        public int getCount(int id)
        {
             var list = GetDetailingModelId(id);
             int count= 0;
             list.ForEach(x=>count+=x.Count);
           return count;
        }

       
        public List<BlDetailingModel> GetDetailingModelId(int Id)
        {
            var list = GetDetailingModel();
            return list.FindAll(x => x.IdModel == Id);
        }

        public List<BlDetailingModel> GetDetailingModelByIdAndDate(int Id,DateTime date)
        {
            var list = GetDetailingModel(date);

            return list.FindAll(x => x.IdModel == Id);
        }

        public List<BlDetailingModel> GetDetailingModel()
        {
            var oList = dal.DetailsModels.GetDetailingModel();
            List<BlDetailingModel> list = new();
        
            oList.ForEach(o =>
            {
                var y = new BlDetailingModel()
                {
                    IdModel = o.IdModel,
                    Count = o.Count,
                    Size = o.Size,
                    Id = o.Id,
                   
            };
                list.Add(y);
            }
          );
            return list;

        }
        // מחזירה את כל הדגמים לפי תאריך כרגע לא מושלם!!!!!!!!!!!!!!
        public List<BlDetailingModel> GetDetailingModel(DateTime date)
        {
            var oList = dal.DetailsModels.GetDetailingModel();
            List<BlDetailingModel> list = new();
           
            oList.ForEach(o =>
            {
                var y = new BlDetailingModel()
                {
                    IdModel = o.IdModel,
                    Count = o.Count,
                    Size = o.Size,
                    Id = o.Id,
                    CountByDate = CurrentCount(date, o.IdModel, o.Size)
                };
                list.Add(y);
            }
          );
            return list;

        }
        public void update(BlDetailingModel item)
        {
            DetailingModel d = new DetailingModel()
            {
                IdModel = item.IdModel,
                Size = item.Size,
                Count = item.Count,
                Id = item.Id,
            };
            dal.DetailsModels.update(d);
        }
    }
}
