using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailingModelControler : ControllerBase
    {
     
        IBldetailingModels DetailingModels;
        public DetailingModelControler(Ibl maneger)
        {
            DetailingModels = maneger.DetailingModels;
        }
        [HttpGet("GetDetailingModels")]
        public List<BlDetailingModel> GetDetailingModels()
        {
            return DetailingModels.GetDetailingModel();
        }
        [HttpGet("GetCurrentCount")]
        public int GetCurrentCount(DateTime date, int model, int size)
        {
            return DetailingModels.CurrentCount(date, model, size);
        }
        [HttpGet("GetDetailingModelByIdAndDate/{id}/{date}")]
        public List<BlDetailingModel> GetDetailingModelByIdAndDate(int id, DateTime date)
        {
            return DetailingModels.GetDetailingModelByIdAndDate(id,date);
        }
        
        [HttpDelete("delete/{id}")]
        public void delete(int id)
        {
            DetailingModels.delete(id);
        }
        [HttpPut("Edit")]
        public void edit(BlDetailingModel model)
        {
            DetailingModels.edit(model);

        }
        [HttpGet("getCountBySize")]
        public int getCountBySize(int id, int size)
        {
            return DetailingModels.getCountBySize(id,size);
        }
        [HttpGet("getCountById")]
        public int getCountById(int id)
        {
            return DetailingModels.getCount(id);
        }
        [HttpGet("GetDetailingModelsById/{id}")]
        public List<BlDetailingModel> GetDetailingModelId(int id)
        {
            return DetailingModels.GetDetailingModelId(id);
        }
      
        [HttpPost("update")]
        public void update(BlDetailingModel model)
        {
            DetailingModels.update(model);
        }
    }
}

