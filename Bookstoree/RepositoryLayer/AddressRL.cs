using CommonLayer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RepositoryLayer
{
    public class AddressRL:IAddressRL
    {
        public static string ConnFile = @"Data Source = DESKTOP-0D45HNI; Initial Catalog = Bookstore; Integrated Security = True";
        SqlConnection conn = new SqlConnection(ConnFile);
        /// <summary>
        /// Add Address details
        /// </summary>
        /// <param name="addAddress">Address Details</param>
        /// <returns></returns>
     
        public AddressModel AddAddress(AddressModel addAddress)
        {
           
            try
            {
                using (conn)
                {
                    SqlCommand com = new SqlCommand("SpAddress", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@AddressId", addAddress.AddressId);
                    com.Parameters.AddWithValue("@Address", addAddress.Address);
                    com.Parameters.AddWithValue("@City", addAddress.City);
                    com.Parameters.AddWithValue("@State", addAddress.State);
                    com.Parameters.AddWithValue("@Type", addAddress.Type);
                    com.Parameters.AddWithValue("@Userid", addAddress.UserId);
                    conn.Open();
                    com.ExecuteNonQuery();
                    conn.Close();
                    return addAddress;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Delete Address
        /// </summary>
        /// <param name="addressid">Delete Address details</param>
        /// <returns></returns>
        public bool DeleteAddress(long addressid)
        {
            SqlCommand com = new SqlCommand("Sp_DeleteAddress", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@AddressId", addressid);

            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Update Address
        /// </summary>
        /// <param name="addAddress">Update address Details</param>
        /// <returns></returns>
        public AddressModel UpdateAddress(AddressModel addAddress)
        {
            SqlConnection conn = new SqlConnection(ConnFile);
            SqlCommand com = new SqlCommand("Sp_UpdateAddress", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@AddressId", addAddress.AddressId);
            com.Parameters.AddWithValue("@Address", addAddress.Address);
            com.Parameters.AddWithValue("@City", addAddress.City);
            com.Parameters.AddWithValue("@State", addAddress.State);
            com.Parameters.AddWithValue("@Type", addAddress.Type);

            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {
                return addAddress;
            }
            return null;
        }
        /// <summary>
        /// Get All Address Details
        /// </summary>
        /// <returns></returns>
        public List<AddressModel> GetUserAddress()
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Sp_GetUserAddress", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader readData = cmd.ExecuteReader();
                    List<AddressModel> userdetaillist = new List<AddressModel>();
                    if (readData.HasRows)
                    {
                        while (readData.Read())
                        {
                            AddressModel userDetail = new AddressModel();
                            userDetail.AddressId = readData.GetInt32("AddressId");
                            userDetail.Address = readData.GetString("Address");
                            userDetail.City = readData.GetString("City");
                            userDetail.State = readData.GetString("State");
                            userDetail.Type = readData.GetInt32("Type");
                            userdetaillist.Add(userDetail);
                        }
                    }
                    return userdetaillist;
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<AddressModel> GetUserAddressById(long userid)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Sp_GetUserAddressById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userid);
                    SqlDataReader readData = cmd.ExecuteReader();
                    List<AddressModel> userdetaillist = new List<AddressModel>();
                    if (readData.HasRows)
                    {
                        while (readData.Read())
                        {
                            AddressModel userDetail = new AddressModel();
                            userDetail.AddressId = readData.GetInt32("AddressId");
                            userDetail.UserId = readData.GetInt32("UserId");
                            userDetail.Address = readData.GetString("Address");
                            userDetail.City = readData.GetString("City");
                            userDetail.State = readData.GetString("State");
                            userDetail.Type = readData.GetInt32("Type");
                            userdetaillist.Add(userDetail);
                        }
                    }
                    return userdetaillist;
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
