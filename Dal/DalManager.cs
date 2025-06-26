using Dal.Api;
using Dal.Do;
using Dal.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class DalManager : IDal
    {
        dbcontext data = new dbcontext();

        //public DalDetailingOrdersServise DetailingOrder { get; }

        //public IDalModells Modell{get;}

        public IDalOrders Orders { get; }

        public IDalModells Modells { get; }

        public IDalDetailingModels DetailsModels { get; }

        public IDalDetailingOrdes DetailingOrdes {get;}

        public IDalSchool School { get; }

        public DalManager() {
            Orders = new DalOrdersServise(data);
            //DetailingOrder = new DalDetailingOrdersServise();   
            Modells = new DalModelService(data);
            DetailsModels = new DalDetailingModelsService(data);
            DetailingOrdes=new DalDetailingOrdersService(data);
            School=new DalSchoolService(data);
        }
    }
}
