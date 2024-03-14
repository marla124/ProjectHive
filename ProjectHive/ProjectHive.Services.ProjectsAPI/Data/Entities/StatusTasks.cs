namespace ProjectHive.Services.ProjectsAPI.Data.Entities;

public class StatusTasks : BaseEntity
{
    public string Name { get; set; }
    public List<Task> Tasks { get; set; }
}
