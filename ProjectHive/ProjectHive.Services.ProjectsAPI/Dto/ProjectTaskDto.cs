﻿using ProjectHive.Services.ProjectsAPI.Data.Entities;

namespace ProjectHive.Services.ProjectsAPI.Dto
{
    public class ProjectTaskDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartExecution { get; set; }
        public DateTime Deadline { get; set; }

        public Guid StatusTaskId { get; set; }
        public Guid? ProjectId { get; set; }
    }
}
