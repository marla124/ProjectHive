namespace ProjectHive.Services.ProjectsAPI.Models.RequestModel
{
    public class UpdateProjectRequestViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid StatusProjectId { get; set; }
        public Guid? CreatorUserId { get; set; }
    }
}
