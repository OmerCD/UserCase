using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCase.Contract.User;

namespace UserCase.API.Validations.User
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.FirstName).NotNull().MinimumLength(2).MaximumLength(20);
            RuleFor(x => x.LastName).NotNull().MinimumLength(2).MaximumLength(30);
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}
