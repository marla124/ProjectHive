
using ProjectHive.Service.Core.Data;

namespace ProjectHive.Services.ProjectsAPI.Data.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
