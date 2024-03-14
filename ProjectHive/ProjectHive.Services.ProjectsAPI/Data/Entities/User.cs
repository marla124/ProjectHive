
namespace ProjectHive.Services.ProjectsAPI.Data.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
