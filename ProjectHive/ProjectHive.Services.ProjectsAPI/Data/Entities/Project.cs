namespace ProjectHive.Services.ProjectsAPI.Data.Entities;

public class Project : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public Guid? CreatorId { get; set; }
    public User? Creator { get; set; }

    public List<User> Users { get; set; }
    public List<ProjectTask> Tasks { get; set; }

}
