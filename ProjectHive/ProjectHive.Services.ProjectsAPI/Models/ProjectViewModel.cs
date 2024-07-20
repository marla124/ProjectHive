namespace ProjectHive.Services.ProjectsAPI.Models
{
    public class ProjectViewModel : BaseProjectViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
