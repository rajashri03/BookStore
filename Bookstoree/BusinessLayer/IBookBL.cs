using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel book);
        public BookModel UpdateBook(BookModel book);
    }
}
