using BL.Api;
using BL.Models;
using Dal.Do;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [Route ("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IBlOrders Orders;
        public OrdersController(Ibl maneger)
        {
            Orders = maneger.Orders; 
        }
        [HttpGet("GetOrders")]
        public List<BlOrder> GetOrders()
        {
            return Orders.GetOrders();
        }
        [HttpGet("GetOrdersByDate")]
        public List<BlOrder> GetOrdersByDate(DateTime d)
        {
            return Orders.GetByDate(d);
        }
        [HttpGet("GetOrdersById/{id}")]
        public BlOrder GetOrdersById(int id)
        {
            return Orders.GetOrdersById(id);
        }
        [HttpGet("GetCountModelByDateAndSize")]
        public int GetCountModelByDateAndSize(DateTime date, int model, int size)
        {
            return Orders.GetCountModelByDateAndSize(date,model,size);
        }

         [HttpPost("Add")]
        public void add(BlOrder order)
        {
            // return  Orders.CreateOrderAsync(order);
             Orders.add(order);
        }
        [HttpDelete("delete/{id}")]
        public void delete(int id) { 
           Orders.delete(id);
        }
        [HttpPut("Edit")]
        public void edit(BlOrder order)
        {
            Orders.edit(order);

        }

    }
}
