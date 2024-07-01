using FluentValidation;
using ProjectHive.Services.AuthAPI.Data.Entities;
using ProjectHive.Services.AuthAPI.Models.RequestModel;

namespace ProjectHive.Services.AuthAPI.FluentValidation
{
    public class UserUpdateValidator : AbstractValidator<UpdateUserRequestViewModel>
    {
        public UserUpdateValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().EmailAddress();
            RuleFor(user => user.Password)
                .NotEmpty().MinimumLength(8).MaximumLength(64);
        }
    }
}
