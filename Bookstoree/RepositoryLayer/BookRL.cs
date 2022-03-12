using CommonLayer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer
{
    public class BookRL : IBookRL
    {
        public static string ConnFile = @"Data Source = DESKTOP-0D45HNI; Initial Catalog = Bookstore; Integrated Security = True";

        SqlConnection conn = new SqlConnection(ConnFile);
        public BookModel AddBook(BookModel book)
        {
            try
            {
                using (conn)
                {
                    SqlCommand com = new SqlCommand("Sp_AddBook", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@bookName", book.bookName);
                    com.Parameters.AddWithValue("@authorName", book.authorName);
                    com.Parameters.AddWithValue("@rating", book.rating);
                    com.Parameters.AddWithValue("@Reviewer", book.Reviewer);
                    com.Parameters.AddWithValue("@discountPrice", book.discountPrice);
                    com.Parameters.AddWithValue("@originalPrice", book.originalPrice);
                    com.Parameters.AddWithValue("@description", book.description);
                    com.Parameters.AddWithValue("@bookImage", book.bookImage);
                    com.Parameters.AddWithValue("@BookCount", book.BookCount);

                    conn.Open();
                    com.ExecuteNonQuery();
                    conn.Close();
                    return book;

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public BookModel UpdateBook(BookModel book, long bookid)
        {
            try
            {
                using(conn)
                {
                    SqlConnection conn = new SqlConnection(ConnFile);
                    SqlCommand com = new SqlCommand("Sp_Updatebook", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@bookId", bookid);
                    com.Parameters.AddWithValue("@bookName", book.bookName);
                    com.Parameters.AddWithValue("@authorName", book.authorName);
                    com.Parameters.AddWithValue("@rating", book.rating);
                    com.Parameters.AddWithValue("@Reviewer", book.Reviewer);
                    com.Parameters.AddWithValue("@discountPrice", book.discountPrice);
                    com.Parameters.AddWithValue("@originalPrice", book.originalPrice);
                    com.Parameters.AddWithValue("@description", book.description);
                    com.Parameters.AddWithValue("@bookImage", book.bookImage);
                    com.Parameters.AddWithValue("@BookCount", book.BookCount);

                    conn.Open();
                    int i = com.ExecuteNonQuery();
                    conn.Close();
                    if (i >= 1)
                    {
                        return book;
                    }
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public bool DeleteBook(long bookid)
        {
            SqlConnection conn = new SqlConnection(ConnFile);
            SqlCommand com = new SqlCommand("Sp_Delete", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@bookId", bookid);

            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        public  object RetriveBookDetails(long bookid)
        {
            SqlConnection conn = new SqlConnection(ConnFile);
            SqlCommand com = new SqlCommand("Retrive_1_BookDetails", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@bookId", bookid);
            conn.Open();
            BookModel bookmodel = new BookModel();
            SqlDataReader rd = com.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    bookmodel.bookId = Convert.ToInt32(rd["bookId"]);
                    bookmodel.bookName = rd["BookName"].ToString();
                    bookmodel.authorName = rd["authorName"].ToString();
                    bookmodel.rating = Convert.ToInt32(rd["rating"]);
                    bookmodel.Reviewer = Convert.ToInt32(rd["Reviewer"]);
                    bookmodel.discountPrice = Convert.ToInt32(rd["discountPrice"]);
                    bookmodel.originalPrice = Convert.ToInt32(rd["originalPrice"]);
                    bookmodel.description =rd["description"].ToString();
                    bookmodel.bookImage =rd["bookImage"].ToString();
                }
                return bookmodel;
            }
            return null;
        }
        public List<BookModel> GetAllNotes()
        {
            try
            {
                List<BookModel> addBook = new List<BookModel>();
                SqlConnection conn = new SqlConnection(ConnFile);
                SqlCommand com = new SqlCommand("SpGetAll", conn);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                conn.Open();
                dataAdapter.Fill(dt);
                foreach (DataRow rd in dt.Rows)
                {

                    addBook.Add(
                            new BookModel
                            {
                                bookId = Convert.ToInt32(rd["bookId"]),
                                bookName = rd["BookName"].ToString(),
                                authorName = rd["authorName"].ToString(),
                                rating = Convert.ToInt32(rd["rating"]),
                                Reviewer = Convert.ToInt32(rd["Reviewer"]),
                                discountPrice = Convert.ToInt32(rd["discountPrice"]),
                                originalPrice = Convert.ToInt32(rd["originalPrice"]),
                                description = rd["description"].ToString(),
                                bookImage = rd["bookImage"].ToString()
                            }
                            );
                 }
                return addBook;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

    }
}
