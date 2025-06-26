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
    public class DalDetailingModelsService : IDalDetailingModels
    {

        dbcontext dbcontext;



        public DalDetailingModelsService(dbcontext data)
        {
            dbcontext = data;
        }


        public void update(DetailingModel m)
        {
            dbcontext.DetailingModels.Add(m);   
            dbcontext.SaveChanges();
        }

      
        public List<DetailingModel> GetDetailingModel()
        {
            return dbcontext.DetailingModels.ToList();
        }
        public void remove(DetailingModel m)
        {
            var m2 = dbcontext.DetailingModels.ToList().Find(x => x.Id == m.Id);
            if (m2 != null)
            {
                dbcontext.DetailingModels.Remove(m2);
                dbcontext.SaveChanges();
            }
        }

        public void edit(DetailingModel m)
        {
            DetailingModel? m2 = dbcontext.DetailingModels.ToList().Find(x => x.Id == m.Id);
            if (m2 != null)
            {
                m2.Id = m.Id;
                m2.IdModel = m.IdModel;
                m2.Size = m.Size;
                m2.Count = m.Count;
                dbcontext.SaveChanges();
            }
        }
    }
}
