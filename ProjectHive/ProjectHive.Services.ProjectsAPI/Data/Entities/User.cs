
namespace ProjectHive.Services.ProjectsAPI.Data.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Project> CreatedProjects { get; set; } 
        public List<UserProject> UserProjects { get; set; } 
    }
}
