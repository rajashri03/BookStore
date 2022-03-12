using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
    public interface ICartRL
    {
        public CartModel Cartc(CartModel cart);
        public bool DeleteCart(long cartid);
        public CartModel UpdateCart(long cartid,CartModel cart);
        public List<CartModel> RetriveCartDetails(long userid);
    }
}
