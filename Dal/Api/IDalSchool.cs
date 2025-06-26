using Dal.Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IDalSchool
    {
        List<School> GetSchool();
        void update(School school);

        void delete(School school);

        void edit(School school);
    }
}
