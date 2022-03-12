using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public interface IAddressBL
    {
        public AddressModel AddAddress(AddressModel addAddress);
        public AddressModel UpdateAddress(AddressModel addAddress);
        public bool DeleteAddress(long addressid);
        public List<AddressModel> GetUserAddress();
        public List<AddressModel> GetUserAddressById(long userid);
    }
}
