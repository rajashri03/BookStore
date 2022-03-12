using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    public class CartController : Controller
    {
        ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;

        }
        [HttpPost("Add")]
        public IActionResult AddBooks(CartModel books)
        {
            try
            {
                var reg = this.cartBL.Cartc(books);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Cart Details Sucessfull", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to add" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpDelete("Delete")]
        public IActionResult DeleteCart(long cartid)
        {
            try
            {
                var reg = this.cartBL.DeleteCart(cartid);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Cart Details deleted Sucessfull", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to delete" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpPut("Update")]
        public IActionResult UpdateCart(long cartid,CartModel cart)
        {
            try
            {
                var reg = this.cartBL.UpdateCart(cartid,cart);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Cart Details Updated Sucessfull", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to Update" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpGet("Get")]
        public IActionResult GetCartDetails(long userid)
        {
            try
            {
                var reg = this.cartBL.RetriveCartDetails(userid);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Cart Details ", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to Fetch" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
