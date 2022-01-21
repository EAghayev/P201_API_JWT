using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Api.DTOs.AuthorDtos
{
    public class AuthorPostDto
    {
        public string FullName { get; set; }
        public int BornYear { get; set; }
    }

    public class AuthorPostDtoValidator : AbstractValidator<AuthorPostDto>
    {
        public AuthorPostDtoValidator()
        {
            RuleFor(x => x.FullName).NotNull().MaximumLength(50).MinimumLength(2);
            RuleFor(x => x.BornYear).NotNull().LessThanOrEqualTo(DateTime.Now.Year - 18);
        }
    }

}
