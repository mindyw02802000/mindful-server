using BL.Api;
using BL.Services;
using Dal;
using Dal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class BlManager : Ibl
    {
        public IBlOrders Orders { get; }
        public IBlModells Modells { get; }
        public IBldetailingModels DetailingModels { get; }
        public IBlDetailingOrders DetailingOrders { get; }
        public IBlSchool School { get; }
        public EmailService EmailService { get; }

        public BlManager()
        {
            IDal dal = new DalManager();
           
        
            DetailingOrders = new BlDetailingOrdersService(dal);
            Orders = new BlOrdersService(dal, DetailingModels, DetailingOrders, EmailService);
            School = new BlSchoolService(dal);
            DetailingModels = new BlDetailingModelService(dal, Orders);
            Modells = new BlModelService(dal, Orders, DetailingModels);
        }
    }
}
