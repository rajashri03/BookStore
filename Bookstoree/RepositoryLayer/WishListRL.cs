using CommonLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
    public class WishListRL:IwishListRL
    {
        public static string ConnFile = @"Data Source = DESKTOP-0D45HNI; Initial Catalog = Bookstore; Integrated Security = True";

        SqlConnection conn = new SqlConnection(ConnFile);

        public string AddWishlist(WishList addwishlist)
        {
            try
            {
                using (conn)
                {
                    SqlCommand sqlCommand = new SqlCommand("Sp_AddWishlist", conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    sqlCommand.Parameters.AddWithValue("@BookId", addwishlist.BookId);
                    sqlCommand.Parameters.AddWithValue("@UserId", addwishlist.UserId);
                    int result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result == 2)
                    {
                        return "BookId not exists";
                    }
                    else if (result == 1)
                    {
                        return "Book already added to wishlist";
                    }
                    else
                    {
                        return "Book Wishlisted successfully";
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public bool DeleteWishlist(long wishlistid)
        {
            try
            {
                using (conn)
                {
                    SqlCommand sqlCommand = new SqlCommand("Sp_deleteWishlist", conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    sqlCommand.Parameters.AddWithValue("@wishlistId", wishlistid);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
        public List<WishList> GetWishList(long userid)
        {
            try
            {
                SqlCommand sqlCommand = new SqlCommand("sp_ShowWishlistbyUserId", conn);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                sqlCommand.Parameters.AddWithValue("@UserId", userid);
                SqlDataReader rd = sqlCommand.ExecuteReader();
                if (rd.HasRows)
                {
                    List<WishList> wishList = new List<WishList>();
                    while (rd.Read())
                    {
                        BookModel booksModel = new BookModel();
                        WishList wishListModel = new WishList();

                        wishListModel.WishlistId = Convert.ToInt32(rd["WishlistId"]);
                        wishListModel.UserId = Convert.ToInt32(rd["UserId"]);
                        wishListModel.BookId = Convert.ToInt32(rd["BookId"]);
                        booksModel.bookImage = rd["bookName"].ToString();
                        booksModel.authorName = rd["authorName"].ToString();
                        booksModel.rating = Convert.ToInt32(rd["rating"]);
                        booksModel.bookId = Convert.ToInt32(rd["bookid"]);
                        booksModel.Reviewer = Convert.ToInt32(rd["Reviewer"]);
                        booksModel.originalPrice = Convert.ToInt32(rd["originalPrice"]);
                        booksModel.discountPrice = Convert.ToInt32(rd["discountPrice"]);
                        booksModel.bookImage = rd["bookImage"].ToString();
                        booksModel.description = rd["description"].ToString();
                        wishListModel.Book = booksModel;
                        wishList.Add(wishListModel);
                    }
                    return wishList;
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
            finally
            {
                conn.Close();
            }
        }
    }
}
