using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IOrderBL
    {
        public OrderModel AddOrder(OrderModel addorder);
        public List<OrderModel> GetOrderById(long userid);
    }
}
