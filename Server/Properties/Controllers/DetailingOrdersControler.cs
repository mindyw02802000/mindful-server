
using BL.Api;
using BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailingOrdersControler : ControllerBase
    {

        IBlDetailingOrders DetailingOrders;
        public DetailingOrdersControler(Ibl maneger)
        {
            DetailingOrders = maneger.DetailingOrders;
        }
        [HttpGet("GetDetailingOrders")]
        public List<BlDetailingOrder> GetDetailingOrders()
        {
            return DetailingOrders.GetDetailingOrder();
        }
        [HttpDelete("delete")]
        public void delete(int id)
        {
            DetailingOrders.delete(id);
        }
        [HttpPut("Edit")]
        public void edit(BlDetailingOrder order)
        {
            DetailingOrders.edit(order);

        }
        [HttpGet("GetDetailingOrdersById/{Id}")]
        public List<BlDetailingOrder> GetDetailingOrderById(int Id)
        {
            return DetailingOrders.GetDetailingOrderById(Id);
        }
     
        [HttpPost("update")]
        public void update(BlDetailingOrder order)
        {
            DetailingOrders.update(order);
        }

    }
}
