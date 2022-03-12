using CommonLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class AddressBL : IAddressBL
    {
        IAddressRL addressrl;

        public AddressBL(IAddressRL addressrl)
        {
            this.addressrl = addressrl;
        }
        public AddressModel AddAddress(AddressModel addAddress)
        {
            try
            {
                return this.addressrl.AddAddress(addAddress);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteAddress(long addressid)
        {
            try
            {
                return this.addressrl.DeleteAddress(addressid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public AddressModel UpdateAddress(AddressModel addAddress)
        {
            try
            {
                return this.addressrl.UpdateAddress(addAddress);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<AddressModel> GetUserAddress()
        {
            try
            {
                return this.addressrl.GetUserAddress();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<AddressModel> GetUserAddressById(long userid)
        {
            try
            {
                return this.addressrl.GetUserAddress();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
