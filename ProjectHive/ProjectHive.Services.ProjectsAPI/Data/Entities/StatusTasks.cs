namespace ProjectHive.Services.ProjectsAPI.Data.Entities
{
    public class StatusTasks
    {
        public Guid Id { get; set; }
        public string StatusName { get; set; }
        public List<Tasks> Tasks { get; set; }
    }
}
