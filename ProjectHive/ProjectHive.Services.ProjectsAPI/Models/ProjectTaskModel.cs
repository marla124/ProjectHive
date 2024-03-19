namespace ProjectHive.Services.ProjectsAPI.Models
{
    public class ProjectTaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartExecution { get; set; }
        public DateTime Deadline { get; set; }

        public Guid StatusTaskId { get; set; }
        public Guid? ProjectId { get; set; }
    }
}
