using FluentValidation;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;

namespace ProjectHive.Services.ProjectsAPI.FluentValidation
{
    public class UpdateProjectValidator : AbstractValidator<UpdateProjectRequestViewModel>
    {
        public UpdateProjectValidator()
        {
            RuleFor(project => project.Name)
                .NotEmpty().MaximumLength(20);
            RuleFor(project => project.Description)
                .MaximumLength(400).NotEmpty();
        }
    }
}
