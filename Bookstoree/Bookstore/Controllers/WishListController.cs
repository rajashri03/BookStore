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
    public class WishListController : Controller
    {
        IwishlistBL wishBL;
        public WishListController(IwishlistBL wishBL)
        {
            this.wishBL = wishBL;

        }
        [HttpPost("Add")]
        public IActionResult AddWishList(WishList wishlist)
        {
            try
            {
                string result = this.wishBL.AddWishlist(wishlist);
                if (result.Equals("Book Wishlisted successfully"))
                {

                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete("Delete")]
        public IActionResult DeleteWishList(long wishlistid)
        {
            try
            {
                var reg = this.wishBL.DeleteWishlist(wishlistid);
                if (reg != null)
                {
                    return this.Ok(new { Success = true, message = "Book removed from wishlist", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to remove" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpGet("Get")]
        public IActionResult GetWishlist(long userid)
        {
            try
            {
                var reg = this.wishBL.GetWishList(userid);
                if (reg != null)
                {
                    return this.Ok(new { Success = true, message = "All Wishlist details", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "unable to fetch" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
