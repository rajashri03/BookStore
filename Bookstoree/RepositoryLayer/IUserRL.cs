using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
   public interface IUserRL
    {
        public UserModel Registration(UserModel user);
        public string Login(string email, string password);
        public string GenerateJWTToken(string Emailid);
        public string ForgetPassword(string Emailid);
        public bool ResetPassword(string email, string newpassword, string confirmpassword);
    }
}
