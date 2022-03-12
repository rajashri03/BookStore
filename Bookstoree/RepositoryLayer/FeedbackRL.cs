using CommonLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer
{
    public class FeedbackRL:IFeedbackRL
    {
        public static string ConnFile = @"Data Source = DESKTOP-0D45HNI; Initial Catalog = Bookstore; Integrated Security = True";

        SqlConnection conn = new SqlConnection(ConnFile);
        public FeedbackModel AddFeedback(FeedbackModel addfeedback)
        {
            try
            {
                using (conn)
                {
                    SqlCommand com = new SqlCommand("Sp_AddFeedback", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@UserId", addfeedback.UserId);
                    com.Parameters.AddWithValue("@BookId", addfeedback.BookId);
                    com.Parameters.AddWithValue("@Comment", addfeedback.Comments);
                    com.Parameters.AddWithValue("@Ratings", addfeedback.Ratings);

                    conn.Open();
                    com.ExecuteNonQuery();
                    conn.Close();
                    return addfeedback;

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<FeedbackModel> RetrieveOrderDetails(int bookId)
        {
            try
            {
                using (conn)
                {
                    string storeprocedure = "spGetFeedbacks";
                    SqlCommand sqlCommand = new SqlCommand(storeprocedure, conn);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@BookId", bookId);
                    conn.Open();
                    SqlDataReader sqlData = sqlCommand.ExecuteReader();
                    List<FeedbackModel> feedback = new List<FeedbackModel>();
                    if (sqlData.HasRows)
                    {
                        while (sqlData.Read())
                        {
                            FeedbackModel feedbackModel = new FeedbackModel();
                            UserModel user = new UserModel();
                            user.Fullname = sqlData["Fullname"].ToString();
                            feedbackModel.Comments = sqlData["Comment"].ToString();
                            feedbackModel.Ratings = Convert.ToInt32(sqlData["Ratings"]);
                            feedbackModel.UserId = Convert.ToInt32(sqlData["UserId"]);
                            feedbackModel.BookId = Convert.ToInt32(sqlData["BookId"]);
                            feedbackModel.FeedbackId = Convert.ToInt32(sqlData["FeedbackId"]);
                            feedbackModel.User = user;
                            feedback.Add(feedbackModel);
                        }
                        return feedback;
                    }
                    return null;
                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
