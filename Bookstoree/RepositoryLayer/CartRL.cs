using CommonLayer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer
{
    public class CartRL : ICartRL
    {
        public static string ConnFile = @"Data Source = DESKTOP-0D45HNI; Initial Catalog = Bookstore; Integrated Security = True";

        SqlConnection conn = new SqlConnection(ConnFile);
        public CartModel Cartc(CartModel cart)
        {
            try
            {
                using (conn)
                {
                    SqlCommand com = new SqlCommand("Sp_AddCart", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@UserId", cart.UserId);
                    com.Parameters.AddWithValue("@BookId", cart.BookId);
                    com.Parameters.AddWithValue("@Quantity", cart.Quantity);
                    conn.Open();
                    com.ExecuteNonQuery();
                    conn.Close();
                    return cart;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool DeleteCart(long cartid)
        {
            try
            {
                using (conn)
                {
                    SqlCommand com = new SqlCommand("Sp_DeleteCart", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@CartId", cartid);
                    conn.Open();
                    conn.Open();
                    int i = com.ExecuteNonQuery();
                    conn.Close();
                    if (i >= 1)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CartModel UpdateCart(long cartid, CartModel cart)
        {
            try
            {
                using (conn)
                {
                    SqlCommand com = new SqlCommand("Sp_UpdateCart", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@CartId", cartid);
                    com.Parameters.AddWithValue("@UserId", cart.UserId);
                    com.Parameters.AddWithValue("@BookId", cart.BookId);
                    com.Parameters.AddWithValue("@Quantity", cart.Quantity);
                    conn.Open();
                    com.ExecuteNonQuery();
                    conn.Close();
                    return cart;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<CartModel> RetriveCartDetails(long userid)
        {
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("Sp_RetriveCart", conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    sqlCommand.Parameters.AddWithValue("@UserId", userid);
                    SqlDataReader rd = sqlCommand.ExecuteReader();
                    if (rd.HasRows)
                    {
                        List<CartModel> cartmodel = new List<CartModel>();
                        while (rd.Read())
                        {
                            BookModel booksModel = new BookModel();
                            CartModel cart=new CartModel();


                            booksModel.bookImage = rd["bookName"].ToString();
                            booksModel.authorName = rd["authorName"].ToString();
                            booksModel.bookId = Convert.ToInt32(rd["bookid"]);
                            booksModel.originalPrice = Convert.ToInt32(rd["originalPrice"]);
                            booksModel.discountPrice = Convert.ToInt32(rd["discountPrice"]);
                            cart.UserId = Convert.ToInt32(userid);
                            cart.BookId = Convert.ToInt32(rd["BookId"]);
                            cart.CartId = Convert.ToInt32(rd["CartId"]);
                            cart.bookmodel = booksModel;
                            cartmodel.Add(cart);
                        }
                        return cartmodel;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception)
                {
                    throw;
                }
            }
    }
}
