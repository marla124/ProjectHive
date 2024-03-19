using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.ProjectsAPI.Business.Services;
using ProjectHive.Services.ProjectsAPI.Data.Repository.Interfase;
using ProjectHive.Services.ProjectsAPI.Dto;
using ProjectHive.Services.ProjectsAPI.Models;

namespace ProjectHive.Services.ProjectsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public ProjectController(IMapper mapper, IProjectService projectService)
        {
            _projectService = projectService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = _mapper.Map<ProjectModel>(await _projectService.GetProjectById(id));
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            await _projectService.DeleteProjectById(id);
            return Ok();
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteManyProject(IEnumerable<ProjectModel> request)
        {
            var dtos = new List<ProjectDto>();
            foreach (var item in request)
            {
                dtos.Add(_mapper.Map<ProjectDto>(item));
            }
            await _projectService.DeleteManyProject(dtos);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectModel request)
        {
            var dto=_mapper.Map<ProjectDto>(request);
            await _projectService.CreateProject(dto);
            return Ok();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateManyProject(IEnumerable<ProjectModel> request)
        {
            var dtos = new List<ProjectDto>();
            foreach (var item in request)
            {
                dtos.Add(_mapper.Map<ProjectDto>(item));
            }
            await _projectService.CreateManyProject(dtos);
            return Ok();
        }
        
        [HttpPatch]
        public async Task<IActionResult> UpdateProject(ProjectModel request)
        {
            var dto = _mapper.Map<ProjectDto>(request);
            await _projectService.UpdateProject(dto);
            return Ok();
        }
    }
}
