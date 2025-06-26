using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IDal
    {
        public IDalOrders Orders { get; }
        public IDalModells Modells { get; } 
        public IDalDetailingModels DetailsModels { get; }
        public  IDalDetailingOrdes DetailingOrdes { get; }
        public IDalSchool School { get; }
    }
}
