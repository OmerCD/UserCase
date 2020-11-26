using System;
using System.Collections.Generic;
using System.Text;

namespace UserCase.Contract.User
{
    public class UserRegisterResponseViewModel
    {
        public string FullName { get; set; }
        public string[] Errors { get; set; }
    }
}
