using FluentValidation;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;

namespace ProjectHive.Services.ProjectsAPI.FluentValidation
{
    public class UpdateTaskValidator : AbstractValidator<UpdateTaskRequestViewModel>
    {
        public UpdateTaskValidator()
        {
            RuleFor(project => project.Name)
                .NotEmpty().MaximumLength(20);
        }
    }
}
