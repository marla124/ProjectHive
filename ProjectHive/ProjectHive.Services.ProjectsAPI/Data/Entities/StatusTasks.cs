using ProjectHive.Services.Core.Data;
namespace ProjectHive.Services.ProjectsAPI.Data.Entities;

public class StatusTasks : BaseEntity
{
    public string Name { get; set; }
    public List<ProjectTask>? Tasks { get; set; }
}
