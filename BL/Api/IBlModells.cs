using BL.Models;
using Dal.Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlModells
    {
        //get
        List<BlModell> GetModels();
        BlModell GetModellById(int Id);

        List<BlModell> GetModelsByKategory(string k);
        //int CurrentCount(DateTime date, int model, int size);

        ////update
        void add(BlModell item);
        ////delete
        void delete(int id);
        ///edit
        void edit (BlModell item);
    }
}
