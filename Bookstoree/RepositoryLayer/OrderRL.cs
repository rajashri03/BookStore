using CommonLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer
{
    public class OrderRL:IOrderRL
    {
        public static string ConnFile = @"Data Source = DESKTOP-0D45HNI; Initial Catalog = Bookstore; Integrated Security = True";

        SqlConnection conn = new SqlConnection(ConnFile);
        /// <summary>
        /// Add orders
        /// </summary>
        /// <param name="addorder">add order details</param>
        /// <returns></returns>
        public OrderModel AddOrder(OrderModel addorder)
        {
            try
            {
                using (conn)
                {
                    SqlCommand com = new SqlCommand("Sp_AddOrder", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@UserId", addorder.UserId);
                    com.Parameters.AddWithValue("@BookId", addorder.BookId);
                    com.Parameters.AddWithValue("@BookQuantity", addorder.Quantity);
                    com.Parameters.AddWithValue("@AddressId", addorder.AddressId);
                    conn.Open();
                    com.ExecuteNonQuery();
                    conn.Close();
                    return addorder;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<OrderModel> GetOrderById(long userid)
        {
           
            List<OrderModel> orders = new List<OrderModel>();
            SqlConnection conn = new SqlConnection(ConnFile);
            SqlCommand com = new SqlCommand("Sp_GetOrderById", conn);

            com.CommandType = CommandType.StoredProcedure;
            conn.Open();
            com.Parameters.AddWithValue("@UserId", userid);
            com.ExecuteNonQuery();
            SqlDataReader dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                    {
                    OrderModel ordermodel = new OrderModel();
                    BookModel booksModel = new BookModel();
                    ordermodel.OrderId = Convert.ToInt32(dr["OrdersId"]);
                    booksModel.bookName = dr["bookName"].ToString();
                    booksModel.authorName = dr["authorName"].ToString();
                    booksModel.bookId = Convert.ToInt32(dr["bookId"]);
                    booksModel.originalPrice = Convert.ToInt32(dr["originalPrice"]);
                    booksModel.discountPrice = Convert.ToInt32(dr["discountPrice"]);
                    booksModel.bookImage = dr["bookImage"].ToString();
                    ordermodel.bookModel = booksModel;
                    orders.Add(ordermodel);
                    }

                    dr.Close();
                return orders;
            }

               
            
            return null;
        }
    }
}
