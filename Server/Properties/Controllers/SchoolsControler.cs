using BL.Api;
using BL.Models;
using Dal.Api;
using Dal.Do;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsControler : ControllerBase
    {

        IBlSchool schools;
        public SchoolsControler(Ibl maneger)
        {
            schools = maneger.School;
        }

        [HttpGet("GetSchools")]
        public List<BlSchool> GetSchools()
        {
            return schools.GetSchool();
        }
        [HttpGet("GetSchoolsByName/{name}")]
        public BlSchool GetSchoolsByName(string name)
        {
            return schools.GetSchoolByName(name);
        }

        [HttpPost("Add")]
        public void update(BlSchool school)
        {
            schools.update(school);
        }
        [HttpDelete("delete")]
        public void delete(string name)
        {
            schools.delete(name);
        }
        [HttpPut("Edit")]
        public void edit(BlSchool school)
        {
            schools.edit(school);

        }
       

    }
}
