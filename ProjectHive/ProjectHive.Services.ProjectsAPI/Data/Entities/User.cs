﻿using ProjectHive.Services.Core.Data;
namespace ProjectHive.Services.ProjectsAPI.Data.Entities
{
    public class User : BaseEntity
    {
        public List<Comment>? Comments { get; set; }
        public List<Project> CreatedProjects { get; set; } 
        public List<UserProject> UserProjects { get; set; } 
        public List<User> Users { get; set; }
    }
}
