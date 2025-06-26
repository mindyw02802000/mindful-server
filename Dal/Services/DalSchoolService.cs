using Dal.Api;
using Dal.Do;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    public class DalSchoolService : IDalSchool
    {
        dbcontext dbcontext;
        public DalSchoolService(dbcontext data)
        {
            dbcontext = data;
        }
        public void delete(School school)
        {
            var school2 = dbcontext.Schools.ToList().Find(x => x.IdSchool ==school.IdSchool);
            if (school2 != null)
            {
                dbcontext.Schools.Remove(school2);
                dbcontext.SaveChanges();
            }
        }
        public void edit(School school)
        {
            School? s = dbcontext.Schools.ToList().Find(x => x.IdSchool == school.IdSchool);
            if (s != null)
            {
                s.IdSchool = school.IdSchool;
                s.Phone = school.Phone;
                s.AddressSchool = school.AddressSchool;
                s.Name = school.Name;
                dbcontext.SaveChanges();

            }
        }
        public List<School> GetSchool()
        {
            return dbcontext.Schools.ToList();
        }
        public void update(School school)
        {
            dbcontext.Schools.Add(school);
            dbcontext.SaveChanges();
        }
    }
}
