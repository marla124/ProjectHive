
using ProjectHive.Services.AuthAPI.Data.Entities;

namespace ProjectHive.Services.ProjectsAPI.Data.Entities
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public User CreatorId { get; set; }
        public List<User> Users { get; set; }
        public List<Tasks> Tasks { get; set; }
        
    }
}
