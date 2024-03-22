using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.ProjectsAPI.Business.Services;
using ProjectHive.Services.ProjectsAPI.Dto;
using ProjectHive.Services.ProjectsAPI.Models;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;

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
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<ProjectViewModel>(await _projectService.GetProjectById(id, cancellationToken));
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            await _projectService.DeleteProjectById(id, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteManyProject(IEnumerable<ProjectViewModel> request, CancellationToken cancellationToken)
        {
            var dtos = new List<ProjectDto>();
            foreach (var item in request)
            {
                dtos.Add(_mapper.Map<ProjectDto>(item));
            }
            await _projectService.DeleteManyProject(dtos, cancellationToken);
            return Ok();
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> CreateProject(CreateProjectRequestViewModel request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ProjectDto>(request);
            await _projectService.CreateProject(dto, cancellationToken);
            return Ok();
        }

        [HttpPost]
        [Route("api/[controller]/[action]")]
        public async Task<IActionResult> CreateManyProject(IEnumerable<CreateProjectRequestViewModel> request, CancellationToken cancellationToken)
        {
            var dtos = new List<ProjectDto>();
            foreach (var item in request)
            {
                dtos.Add(_mapper.Map<ProjectDto>(item));
            }
            await _projectService.CreateManyProject(dtos, cancellationToken);
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProject(UpdateProjectRequestViewModel request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ProjectDto>(request);
            await _projectService.UpdateProject(dto, cancellationToken);
            return Ok();
        }
    }
}
