namespace ProjectHive.Services.ProjectsAPI.Dto
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid StatusProjectId { get; set; }
        public Guid? CreatorUserId { get; set; }
    }
}
