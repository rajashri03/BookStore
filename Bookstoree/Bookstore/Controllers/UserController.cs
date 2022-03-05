using BusinessLayer;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;

        }
        [HttpPost("Add")]
        public IActionResult AddUser(UserModel userRegistration)
        {
            try
            {
                var reg = this.userBL.Registration(userRegistration);
                if (reg!=null)

                {
                    return this.Ok(new { Success = true, message = "User Added Sucessfull", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User not added" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpPost("Login")]
        public IActionResult Login(string email,string password)
        {
            try
            {
                string tokenString = userBL.Login(email,password);
                if (tokenString != null)
                {
                    return Ok(new { Success = true, message = "login Sucessfull", Data = tokenString });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "login Unsucessfull" });
                }
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {
            try
            {
                string token = userBL.ForgetPassword(email);
                if (token != null)
                {
                    return Ok(new { success = true, Message = "Please check your Email.Token sent succesfully." });
                }
                else
                {
                    return this.BadRequest(new { Success = false, Message = "Email not registered" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPost("Reset")]
        public IActionResult ResetPassword(string newpassword, string confirmpassword)
        {
            try
            {
                var email = User.FindFirst("Email").Value.ToString();
                var data = userBL.ResetPassword(email, newpassword,confirmpassword);
                if (data!=null)
                {
                    if(newpassword==confirmpassword)
                    {
                        return this.Ok(new { Success = true, message = "Your password has been changed sucessfully" });
                    }
                    else
                    {
                        return this.Ok(new { Success = true, message = "Password dont matched" });
                    }
                    
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to reset password.Please try again" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
