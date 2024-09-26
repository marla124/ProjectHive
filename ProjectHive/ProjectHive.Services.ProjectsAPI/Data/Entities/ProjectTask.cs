using ProjectHive.Services.Core.Data;
namespace ProjectHive.Services.ProjectsAPI.Data.Entities
{
    public class ProjectTask : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public Guid StatusTaskId { get; set; }
        public StatusTasks StatusTask { get; set; }
        public Guid? UserExecutorId { get; set; }
        public User UserExecutor {  get; set; }
        public Guid UserCreatorId { get; set; }
        public User UserCreator { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
