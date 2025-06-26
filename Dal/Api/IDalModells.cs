using Dal.Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IDalModells
    {
        List<Modell> GetModels();
        void add(Modell modell);
        void edit(Modell modell);
        void delete(Modell modell);  
    }
}
