namespace ProjectHive.Services.ProjectsAPI.Dto
{
    public class ProjectTaskDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartExecution { get; set; }
        public DateTime Deadline { get; set; }
        public Guid StatusTaskId { get; set; }
        public string? ProjectName { get; set; }
    }
}
