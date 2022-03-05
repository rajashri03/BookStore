using CommonLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class UserBL: IUserBL
    {
        IUserRL iuserrl;
        /// <summary>
        /// Inititalizing the instanse of UserBL class
        /// </summary>
        /// <param name="iuserrl"></param>
        public UserBL(IUserRL iuserrl)
        {
            this.iuserrl = iuserrl;
        }
        /// <summary>
        ///Registration model to register user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public UserModel Registration(UserModel user)
        {
            try
            {
                return this.iuserrl.Registration(user);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(string email, string password)
        {
            try
            {
                return this.iuserrl.Login(email,password);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public string GenerateJWTToken(string Emailid)
        {
            try
            {
                return iuserrl.GenerateJWTToken(Emailid);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public string ForgetPassword(string Emailid)
        {
            try
            {
                return iuserrl.ForgetPassword(Emailid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string email, string newpassword, string confirmpassword)
        {
            try
            {
                return iuserrl.ResetPassword(email, newpassword, confirmpassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
