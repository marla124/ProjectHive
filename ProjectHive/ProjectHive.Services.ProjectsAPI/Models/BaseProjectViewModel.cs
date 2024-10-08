﻿namespace ProjectHive.Services.ProjectsAPI.Models;

public class BaseProjectViewModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Guid> Users { get; set; }
}