using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IwishlistBL
    {
        public string AddWishlist(WishList addwishlist);
        public bool DeleteWishlist(long wishlistid);
        public List<WishList> GetWishList(long userid);
    }
}
