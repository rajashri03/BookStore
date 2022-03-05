using CommonLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class BookBL : IBookBL
    {
        IBookRL bookrl;

        public BookBL(IBookRL bookrl)
        {
            this.bookrl = bookrl;
        }

        public BookModel AddBook(BookModel book)
        {
            try
            {
                return this.bookrl.AddBook(book);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public BookModel UpdateBook(BookModel book)
        {
            try
            {
                return this.bookrl.UpdateBook(book);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
