using System;
using System.Collections.Generic;
using System.Text;

namespace UserCase.Contract.User
{
    public class UserRegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRegisterAddressModel Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public int Company { get; set; }
    }

    public class UserRegisterAddressModel
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public UserRegisterAddressGeoModel Geo { get; set; }
    }

    public class UserRegisterAddressGeoModel
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
