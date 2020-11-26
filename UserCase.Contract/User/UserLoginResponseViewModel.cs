using System;

namespace UserCase.Contract.User
{
    public class UserLoginResponseViewModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool FailedLogin { get; set; }
    }
}