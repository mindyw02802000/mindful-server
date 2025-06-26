using Dal.Do;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IDalDetailingModels
    {
        public List<DetailingModel> GetDetailingModel();
        public void update(DetailingModel m);
        public void remove(DetailingModel m);
        public void edit(DetailingModel m);

    }
}
