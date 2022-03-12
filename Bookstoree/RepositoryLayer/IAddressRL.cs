using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer
{
    public interface IAddressRL
    {
        public AddressModel AddAddress(AddressModel addAddress);
        public bool DeleteAddress(long addressid);
        public AddressModel UpdateAddress(AddressModel addAddress);
        public List<AddressModel> GetUserAddress();
        public List<AddressModel> GetUserAddressById(long userid);
    }
}
