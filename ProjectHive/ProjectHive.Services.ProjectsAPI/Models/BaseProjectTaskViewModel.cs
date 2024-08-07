namespace ProjectHive.Services.ProjectsAPI.Models;

public class BaseProjectTaskViewModel
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime? Deadline { get; set; }
    public string ProjectId { get; set; }
    public string StatusTaskId { get; set; }
}