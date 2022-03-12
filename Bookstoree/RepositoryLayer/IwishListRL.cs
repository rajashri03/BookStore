using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
    public interface IwishListRL
    {
        public string AddWishlist(WishList addwishlist);
        public bool DeleteWishlist(long wishlistid);
        public List<WishList> GetWishList(long userid);
    }
}
