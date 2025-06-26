using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModellsControllers : ControllerBase
    {
        IBlModells Modells;
        public ModellsControllers(Ibl maneger)
        {
            Modells = maneger.Modells;        
        }

        [HttpGet("GetModels")]
        public List<BlModell> GetModels()
        {
            return Modells.GetModels(); 
        }

        [HttpGet("GetModelsById/{id}")]
        public BlModell GetModellById(int id)
        {
            return Modells.GetModellById(id);       
        }
        //update  
        [HttpGet("GetModelsByKategory")]
        public  List<BlModell>  GetModelsByKategory(string k)
        {
            return Modells.GetModelsByKategory(k);
        }
        //[HttpGet("GetCurrentCount")]
        //public int GetCurrentCount(DateTime date, int model, int size)
        //{
        //    return Modells.CurrentCount(date, model, size);
        //}
        [HttpDelete("delete/{id}")]
        public void delete(int id)
        {
            Modells.delete(id);
        }
        [HttpPost("Add")]
        public void add(BlModell model)
        {
            Modells.add(model);
        }
        // PUT
        [HttpPut("Edit")]
        public void edit(BlModell model)
        {
           Modells.edit(model);
             
        }
    }
}
