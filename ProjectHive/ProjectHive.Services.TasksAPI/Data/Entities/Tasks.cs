namespace ProjectHive.Services.TasksAPI.Data.Entities
{
    public class Tasks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartExicution { get; set; }
        public DateTime Deadline { get; set; }
        public StatusTasks StatusTask { get; set; }
    }
}
