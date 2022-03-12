using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        IOrderBL orderBL;
        public OrdersController(IOrderBL bookBL)
        {
            this.orderBL = bookBL;

        }
        [HttpPost("Add")]
        public IActionResult AddOrder(OrderModel books)
        {
            try
            {
                var reg = this.orderBL.AddOrder(books);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Order Added Sucessfull", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Order not added" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpGet("Get")]
        public IActionResult GetOrders(long userid)
        {
            try
            {
                var reg = this.orderBL.GetOrderById(userid);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Order Details", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Order not Found" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
