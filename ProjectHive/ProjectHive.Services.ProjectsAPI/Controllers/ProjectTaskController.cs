using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.ProjectsAPI.Business.Services;
using ProjectHive.Services.ProjectsAPI.Dto;
using ProjectHive.Services.ProjectsAPI.Models;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;

namespace ProjectHive.Services.ProjectsAPI.Controllers
{
    public class ProjectTaskController : Controller
    {
        private readonly IProjectTaskService _taskService;
        private readonly IMapper _mapper;
        public ProjectTaskController(IMapper mapper, IProjectTaskService projectService)
        {
            _taskService = projectService;
            _mapper = mapper;
        }
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var task = _mapper.Map<ProjectTaskViewModel>(await _taskService.GetById(id, cancellationToken));
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            await _taskService.DeleteById(id, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteManyTasks(IEnumerable<ProjectTaskViewModel> request, CancellationToken cancellationToken)
        {
            await _taskService.DeleteMany(_mapper.Map<IEnumerable<ProjectTaskDto>>(request), cancellationToken);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateTask(CreateTaskRequestViewModel request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ProjectTaskDto>(request);

            return Ok(_mapper.Map<ProjectTaskViewModel>(await _taskService.Create(dto, cancellationToken)));
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateManyTask(IEnumerable<CreateTaskRequestViewModel> request, CancellationToken cancellationToken)
        {
            await _taskService.CreateMany(_mapper.Map<IEnumerable<ProjectTaskDto>>(request), cancellationToken);
            return Ok();
        }

        [HttpPatch]
        [Route("[action]")]
        public async Task<IActionResult> UpdateProject(UpdateTaskRequestViewModel request, CancellationToken cancellationToken)
        {
            var dto = _mapper.Map<ProjectTaskDto>(request);

            return Ok(_mapper.Map<ProjectTaskViewModel>(await _taskService.Update(dto, cancellationToken)));
        }
    }
}
