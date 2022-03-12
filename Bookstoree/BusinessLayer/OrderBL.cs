using CommonLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class OrderBL:IOrderBL
    {
        IOrderRL bookrl;

        public OrderBL(IOrderRL bookrl)
        {
            this.bookrl = bookrl;
        }
        public OrderModel AddOrder(OrderModel addorder)
        {
            try
            {
                return this.bookrl.AddOrder(addorder);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<OrderModel> GetOrderById(long userid)
        {
            try
            {
                return this.bookrl.GetOrderById(userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
