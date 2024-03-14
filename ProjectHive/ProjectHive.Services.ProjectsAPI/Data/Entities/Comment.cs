
namespace ProjectHive.Services.ProjectsAPI.Data.Entities;

public class Comment : BaseEntity
{
    public string Text { get; set; }

    public Guid TaskId { get; set; }
    public ProjectTask Task { get; set; }

    public Guid? UserId { get; set; }
    public User? User { get; set; }

    public Guid? ParentCommentId { get; set; }
    public Comment? ParentComment { get; set; }

    public List<Comment> ChildComments { get; set; }
}
