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

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<ProjectViewModel>(await _projectService.GetById(id, cancellationToken));
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            await _projectService.DeleteById(id, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteManyProject(IEnumerable<ProjectViewModel> request, CancellationToken cancellationToken)
        {
            await _projectService.DeleteMany(_mapper.Map<IEnumerable<ProjectDto>>(request), cancellationToken);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateProject(CreateProjectRequestViewModel request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ProjectDto>(request);

            return Ok(_mapper.Map<ProjectViewModel>(await _projectService.Create(dto, cancellationToken)));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateManyProject(IEnumerable<CreateProjectRequestViewModel> request, CancellationToken cancellationToken)
        {
            await _projectService.CreateMany(_mapper.Map<IEnumerable<ProjectDto>>(request), cancellationToken);
            return Ok();
        }

        [HttpPatch]
        [Route("[action]")]
        public async Task<IActionResult> UpdateProject(UpdateProjectRequestViewModel request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ProjectDto>(request);

            return Ok(_mapper.Map<ProjectViewModel>(await _projectService.Update(dto, cancellationToken)));
        }
    }
}
