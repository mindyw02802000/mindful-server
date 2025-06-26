using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBldetailingModels
    {
      
            //get
            List<BlDetailingModel>GetDetailingModel();

            List<BlDetailingModel> GetDetailingModelId(int Id);
            List<BlDetailingModel> GetDetailingModel(DateTime date);
            List<BlDetailingModel> GetDetailingModelByIdAndDate(int Id, DateTime date);

            int CurrentCount(DateTime date, int model, int size);
            //update הוספה
            void update(BlDetailingModel item);
            ////delete
            void delete(int id);
            /// editשינוי  
            void edit(BlDetailingModel item);
            int getCountBySize(int id,int size);
            int getCount(int id);
    }
    }



