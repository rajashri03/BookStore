using CommonLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class WishListBL:IwishlistBL
    {
        IwishListRL wishrl;

        public WishListBL(IwishListRL wishrl)
        {
            this.wishrl = wishrl;
        }
        public string AddWishlist(WishList addwishlist)
        {
            try
            {
                return this.wishrl.AddWishlist(addwishlist);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteWishlist(long wishlistid)
        {
            try
            {
                return this.wishrl.DeleteWishlist(wishlistid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<WishList> GetWishList(long userid)
        {
            try
            {
                return this.wishrl.GetWishList(userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
