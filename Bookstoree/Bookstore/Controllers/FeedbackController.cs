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
    public class FeedbackController : Controller
    {
        IFeedbackBL feedbackBL;
        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;

        }
        [HttpPost("Add")]
        public IActionResult Add_Address(FeedbackModel address)
        {
            try
            {
                var reg = this.feedbackBL.AddFeedback(address);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Feedback added Sucessfull", Response = reg });
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
        [HttpGet("Get")]
        public IActionResult RetrieveOrderDetails(int bookid)
        {
            try
            {
                var reg = this.feedbackBL.RetrieveOrderDetails(bookid);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Feedback Details", Response = reg });
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
