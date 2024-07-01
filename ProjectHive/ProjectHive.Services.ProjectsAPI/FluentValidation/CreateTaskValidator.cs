using FluentValidation;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;

namespace ProjectHive.Services.ProjectsAPI.FluentValidation
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskRequestViewModel>
    {
        public CreateTaskValidator()
        {
            RuleFor(project => project.Name)
                .NotEmpty().MaximumLength(20);
        }
    }
}
