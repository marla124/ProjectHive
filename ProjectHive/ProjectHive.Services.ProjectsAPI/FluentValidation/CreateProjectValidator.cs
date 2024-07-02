using FluentValidation;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;

namespace ProjectHive.Services.ProjectsAPI.FluentValidation
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectRequestViewModel>
    {
        public CreateProjectValidator()
        {
            RuleFor(project => project.Name)
                .NotEmpty().MaximumLength(20);
            RuleFor(project => project.Description)
                .NotEmpty().MaximumLength(400);
        }
    }
}
