using CommonLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class CartBL:ICartBL
    {
        ICartRL cartrl;

        public CartBL(ICartRL cartrl)
        {
            this.cartrl = cartrl;
        }

        public CartModel Cartc(CartModel cart)
        {
            try
            {
                return this.cartrl.Cartc(cart);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteCart(long cartid)
        {
            try
            {
                return this.cartrl.DeleteCart(cartid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public CartModel UpdateCart(long cartid,CartModel cart)
        {
            try
            {
                return this.cartrl.UpdateCart(cartid,cart);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CartModel> RetriveCartDetails(long userid)
        {
            try
            {
                return this.cartrl.RetriveCartDetails(userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
