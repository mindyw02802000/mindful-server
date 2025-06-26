using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Api
{
    public interface IBlSchool
    {
        //get
        List<BlSchool> GetSchool();

        BlSchool GetSchoolByName(string name);
        ////update
        void update(BlSchool item);
        ////delete
        void delete(string name);
        ///edit
        void edit(BlSchool item);
    }
}
