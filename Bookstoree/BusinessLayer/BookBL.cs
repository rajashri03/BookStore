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
        public BookModel UpdateBook(BookModel book,long bookid)
        {
            try
            {
                return this.bookrl.UpdateBook(book,bookid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteBook(long bookid)
        {
            try
            {
                return this.bookrl.DeleteBook(bookid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public object RetriveBookDetails(long bookid)
        {
            try
            {
                return this.bookrl.RetriveBookDetails(bookid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<BookModel> GetAllNotes()
        {
            try
            {
                return this.bookrl.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }
       
    }
}
