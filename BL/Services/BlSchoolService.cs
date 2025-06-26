using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Do;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class BlSchoolService:IBlSchool
    {
        IDal dal;

        public BlSchoolService(IDal dal)
        {
            this.dal = dal;
        }

        public void delete(string name)
        {
            BlSchool s = GetSchoolByName(name);

            School s2 = new School()
            {
                IdSchool = s.IdSchool,
                AddressSchool = s.AddressSchool,
                Name = s.Name,
                Phone = s.Phone,
            };
            dal.School.delete(s2);
        }

        public void edit(BlSchool item)
        {
            School? s = new School()
            {
                IdSchool = item.IdSchool,
                Name = item.Name,
                Phone = item.Phone,
                AddressSchool = item.AddressSchool,
            };
            dal.School.edit(s);
        }

        public List<BlSchool> GetSchool()
        {
            var oList = dal.School.GetSchool();
            List<BlSchool> list = new();
            oList.ForEach(o =>
            {
                var oo = new BlSchool()
                {
                    IdSchool = o.IdSchool,
                    AddressSchool = o.AddressSchool,
                    Name = o.Name,
                    Phone = o.Phone,
                };
                list.Add(oo);
            }
            );
            return list;
        }
        public BlSchool GetSchoolByName(string name)
        {
            var o = GetSchool();
            BlSchool s = o.Find(x => x.Name.Equals(name));
            return s;
        }

        public void update(BlSchool item)
        {
           if (GetSchoolByName(item.Name) !=null)return;
            School o = new School()
            {
                IdSchool = item.IdSchool,
                Name = item.Name,
                Phone = item.Phone,
                AddressSchool = item.AddressSchool,
            };
            dal.School.update(o);
        }
    }
}
