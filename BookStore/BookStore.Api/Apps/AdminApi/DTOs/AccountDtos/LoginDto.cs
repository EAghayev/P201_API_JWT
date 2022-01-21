using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.Apps.AdminApi.DTOs.AccountDtos
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(50).MinimumLength(4);
            RuleFor(x => x.Password).MaximumLength(25).MinimumLength(8);
        }
    }
}
