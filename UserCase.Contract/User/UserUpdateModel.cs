namespace UserCase.Contract.User
{
    public class UserUpdateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserUpdateAddressModel Address { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }
        public int Company { get; set; }
    }

    public class UserUpdateAddressModel
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public UserUpdateAddressGeoModel Geo { get; set; }
    }

    public class UserUpdateAddressGeoModel
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}