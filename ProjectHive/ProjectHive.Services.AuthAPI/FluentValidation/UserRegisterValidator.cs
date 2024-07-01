using FluentValidation;
using ProjectHive.Services.AuthAPI.Data.Repository.Interface;
using ProjectHive.Services.AuthAPI.Models;

namespace ProjectHive.Services.AuthAPI.FluentValidation
{
    public class UserRegisterValidator : AbstractValidator<RegisterModel>
    {                
        public UserRegisterValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty().EmailAddress();
            RuleFor(user => user.Password)
                .NotEmpty().MinimumLength(8).MaximumLength(64);
            RuleFor(user => user.PasswordConfirmation)
                .Equal(user => user.Password);
        }   
    }
}
