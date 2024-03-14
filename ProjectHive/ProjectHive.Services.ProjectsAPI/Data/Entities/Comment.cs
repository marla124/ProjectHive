
namespace ProjectHive.Services.ProjectsAPI.Data.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public DateTime DateAndTime { get; set; }
        public Guid TaskId { get; set; }
        public Tasks Tasks { get; set; }
        public Guid? ParentCommentId { get; set; }
        public List<Comment> ChildComments { get; set; }
        public Comment ParentComment { get; set; }
        public User User { get; set; }
    }
}
