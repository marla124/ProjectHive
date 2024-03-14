namespace ProjectHive.Services.ProjectsAPI.Data.Entities;

public class ProjectStatus : BaseEntity
{
    public string Name { get; set; }
    public List<Project> Projects { get; set; }
}
