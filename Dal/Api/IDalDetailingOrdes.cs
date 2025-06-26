using Dal.Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IDalDetailingOrdes
    {
        public List<DetailingOrder> GetDetailingOrders();
        public void update(DetailingOrder m);
        public void remove(DetailingOrder m);
        public void edit(DetailingOrder m);
      
    }
}
