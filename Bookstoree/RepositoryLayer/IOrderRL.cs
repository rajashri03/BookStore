using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
    public interface IOrderRL
    {
        public OrderModel AddOrder(OrderModel addorder);
        public List<OrderModel> GetOrderById(long userid);
    }
}
