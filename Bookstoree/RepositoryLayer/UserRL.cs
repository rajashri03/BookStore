using CommonLayer;
using CommonLayer.Model;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer
{
    public class UserRL : IUserRL
    {
        private IConfiguration Iconfiguration { get; }
        public string ConnFile = @"Data Source = DESKTOP-0D45HNI; Initial Catalog = Bookstore; Integrated Security = True";
        public UserRL(IConfiguration Iconfiguration)
        {
            this.Iconfiguration = Iconfiguration;
        }
        public UserModel Registration(UserModel user)
        {
            SqlConnection conn = new SqlConnection(ConnFile);
            SqlCommand com = new SqlCommand("SpRegister", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Fullname", user.Fullname);
            com.Parameters.AddWithValue("@Email", user.Email);
            com.Parameters.AddWithValue("@Mobile", user.Mobile);
            com.Parameters.AddWithValue("@Password", user.Password);

            conn.Open();
            int i = com.ExecuteNonQuery();
            conn.Close();
            if (i >= 1)
            {

                return user;

            }
            return null;

        }
        public string Login(string email,string password)
        {
            SqlConnection conn = new SqlConnection(ConnFile);
            SqlCommand com = new SqlCommand("Splogin", conn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Email", email);
            com.Parameters.AddWithValue("@Password", password);
            conn.Open();
            SqlDataReader rd = com.ExecuteReader();
            if (rd.HasRows)
            {
                while(rd.Read())
                {
                    email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                    password = Convert.ToString(rd["Password"] == DBNull.Value ? default : rd["Password"]);
                }
                var token = this.GenerateJWTToken(email);
                return token;
            }
            return null;
        }
        public string GenerateJWTToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Iconfiguration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Email", email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string ForgetPassword(string Emailid)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnFile);
                SqlCommand com = new SqlCommand("SpForgetPass", conn);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Email", Emailid);
                conn.Open();
                SqlDataReader rd = com.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Emailid = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                    }
                    var token = this.GenerateJWTToken(Emailid);
                    new MSMQ().sendmsg(token);
                    return token;
                }
                conn.Close();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string email, string newpassword,string confirmpassword)
        {
            try
            {
                if(newpassword==confirmpassword)
                {
                    SqlConnection conn = new SqlConnection(ConnFile);
                    SqlCommand com = new SqlCommand("SpReset", conn);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Email", email);
                    com.Parameters.AddWithValue("@Password", newpassword);
                    conn.Open();
                    SqlDataReader rd = com.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            email = Convert.ToString(rd["Email"] == DBNull.Value ? default : rd["Email"]);
                            newpassword = Convert.ToString(rd["Password"] == DBNull.Value ? default : rd["Password"]);
                        }
                        return true;
                    }
                    return true;

                }
                
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    }
