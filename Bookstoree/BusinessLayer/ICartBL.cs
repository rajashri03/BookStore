using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface ICartBL
    {
        public CartModel Cartc(CartModel cart);
        public CartModel UpdateCart(long cartid,CartModel cart);
        public bool DeleteCart(long cartid);
        public List<CartModel> RetriveCartDetails(long userid);
    }
}
