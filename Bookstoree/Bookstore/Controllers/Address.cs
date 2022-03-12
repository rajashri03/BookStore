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
    public class Address : Controller
    {
        IAddressBL addressBL;
        public Address(IAddressBL addressBL)
        {
            this.addressBL = addressBL;

        }
        [HttpPost("Add")]
        public IActionResult Add_Address(AddressModel address)
        {
            try
            {
                var reg = this.addressBL.AddAddress(address);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Address Details Sucessfull", Response = reg });
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
        [HttpPut("Update")]
        public IActionResult Update_Address(AddressModel address)
        {
            try
            {
                var reg = this.addressBL.UpdateAddress(address);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Address Details Updated Sucessfull", Response = reg });
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
        [HttpGet("GetAll")]
        public IActionResult GetUserAddress()
        {
            try
            {
                var reg = this.addressBL.GetUserAddress();
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Address Details Fetched Sucessfull", Response = reg });
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
        [HttpGet("GetByUserID")]
        public IActionResult GetUserAddressById(long userid)
        {
            try
            {
                var reg = this.addressBL.GetUserAddressById(userid);
                if (reg != null)
                {
                    return this.Ok(new { Success = true, message = "Address Details of user" ,Response=reg});
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
        [HttpDelete("Delete")]
        public IActionResult DeleteAddress(long addressid)
        {
            try
            {
                var reg = this.addressBL.DeleteAddress(addressid);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Address Deleted Sucessfull"});
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
        
    }
}
