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
    public class AddBookController : Controller
    {
        IBookBL bookBL;
        public AddBookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;

        }
        [HttpPost("Add")]
        public IActionResult AddBooks(BookModel books)
        {
            try
            {
                var reg = this.bookBL.AddBook(books);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Book Added Sucessfull", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Book not added" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpPut("Update")]
        public IActionResult UpdateBooks(BookModel books)
        {
            try
            {
                var reg = this.bookBL.UpdateBook(books);
                if (reg != null)

                {
                    return this.Ok(new { Success = true, message = "Book Updated Sucessfull", Response = reg });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Book details not updated" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

    }
}
