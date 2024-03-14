namespace ProjectHive.Services.ProjectsAPI.Data.Entities
{
    public class ProjectStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
    }
}
