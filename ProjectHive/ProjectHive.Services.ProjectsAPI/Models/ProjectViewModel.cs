namespace ProjectHive.Services.ProjectsAPI.Models
{
    public class ProjectViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid StatusProjectId { get; set; }
        public Guid? CreatorId { get; set; }
    }
}
