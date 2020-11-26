namespace UserCase.Contract.User
{
    public class UserViewModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserAddressViewModel Address { get; set; }
    }

    public class UserAddressViewModel
    {
        public string Street { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public UserAddressLocationViewModel Geo { get; set; }
    }

    public class UserAddressLocationViewModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}