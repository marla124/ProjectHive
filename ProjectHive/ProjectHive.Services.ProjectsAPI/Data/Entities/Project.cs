namespace ProjectHive.Services.ProjectsAPI.Data.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid StatusProjectId { get; set; }
    public ProjectStatus StatusProject { get; set; }
    public Guid CreatorUserId { get; set; }
    public User CreatorUser { get; set; }
    public List<UserProject> UserProjects { get; set; }
    public List<ProjectTask> Tasks { get; set; }

}
