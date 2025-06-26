using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface Ibl
    {
        public IBlOrders Orders { get; }
        public IBlModells Modells { get; }
        public IBldetailingModels DetailingModels { get; }
        public IBlDetailingOrders DetailingOrders { get; }
        public IBlSchool School { get; }
    }
}
