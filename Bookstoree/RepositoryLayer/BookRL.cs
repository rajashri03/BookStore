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
        private IConfiguration Iconfiguration { get; }
        public string ConnFile = @"Data Source = DESKTOP-0D45HNI; Initial Catalog = Bookstore; Integrated Security = True";
        public BookRL(IConfiguration Iconfiguration)
        {
            this.Iconfiguration = Iconfiguration;
        }
        public BookModel AddBook(BookModel book)
        {
            SqlConnection conn = new SqlConnection(ConnFile);
            SqlCommand com = new SqlCommand("Sp_AddBook", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@bookName", book.bookName);
            com.Parameters.AddWithValue("@authorName", book.authorName);
            com.Parameters.AddWithValue("@rating", book.rating);
            com.Parameters.AddWithValue("@totalRating", book.totalRating);
            com.Parameters.AddWithValue("@discountPrice", book.discountPrice);
            com.Parameters.AddWithValue("@originalPrice", book.originalPrice);
            com.Parameters.AddWithValue("@description", book.description);
            com.Parameters.AddWithValue("@bookImage", book.bookImage);

            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return book;
            }
            return null;
        }
        public BookModel UpdateBook(BookModel book)
        {
            SqlConnection conn = new SqlConnection(ConnFile);
            SqlCommand com = new SqlCommand("Sp_Updatebook", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@bookId", book.bookId);
            com.Parameters.AddWithValue("@bookName", book.bookName);
            com.Parameters.AddWithValue("@authorName", book.authorName);
            com.Parameters.AddWithValue("@rating", book.rating);
            com.Parameters.AddWithValue("@totalRating", book.totalRating);
            com.Parameters.AddWithValue("@discountPrice", book.discountPrice);
            com.Parameters.AddWithValue("@originalPrice", book.originalPrice);
            com.Parameters.AddWithValue("@description", book.description);
            com.Parameters.AddWithValue("@bookImage", book.bookImage);

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
}
