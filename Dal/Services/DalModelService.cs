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
    internal class DalModelService : IDalModells
    {
        dbcontext dbcontext;

      

        public DalModelService(dbcontext data)
        {
            dbcontext =data;
        }




        public void delete(Modell modell)
        {
            // חפש את המודל הקיים בקונטקסט
            var modellToDelete = dbcontext.Modells.Include(m => m.DetailingModels)
                                                  .FirstOrDefault(x => x.IdModel == modell.IdModel);

            if (modellToDelete != null)
            {
                // נקה את פרטי המודל הקשורים
                modellToDelete.DetailingModels.Clear();

                // מחק את המודל
                dbcontext.Modells.Remove(modellToDelete);
                dbcontext.SaveChanges();
            }
        }

        public void edit(Modell modell)
        {
        Modell? m = dbcontext.Modells.ToList().Find(x => x.IdModel == modell.IdModel);
            if (m != null)
            {
                m.IdModel = modell.IdModel;
                m.Price = modell.Price;
                m.Picture = modell.Picture;
                m.Kategory = modell.Kategory;
                dbcontext.SaveChanges();
             
            }


        }
        public List<Modell> GetModels()
        {
            return dbcontext.Modells.Include(x => x.DetailingModels).ToList();
        }
        public void add(Modell item)
        {
            dbcontext.Modells.Add(item);
            dbcontext.SaveChanges();
        }
    }

}
