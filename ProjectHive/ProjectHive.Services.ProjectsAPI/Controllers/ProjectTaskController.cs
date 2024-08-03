using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.ProjectsAPI.Business.Services;
using ProjectHive.Services.ProjectsAPI.Data.Entities;
using ProjectHive.Services.ProjectsAPI.Dto;
using ProjectHive.Services.ProjectsAPI.Models;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;
using System.Security.Claims;

namespace ProjectHive.Services.ProjectsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTaskController : BaseController
    {
        private readonly IProjectTaskService _taskService;
        private readonly IProjectTaskStatusService _taskStatusService;
        private readonly IMapper _mapper;
        public ProjectTaskController(IMapper mapper, IProjectTaskService projectService, IProjectTaskStatusService taskStatusService)
        {
            _taskService = projectService;
            _mapper = mapper;
            _taskStatusService= taskStatusService;
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

        [HttpGet("[action]/{projectId}")]
        public async Task<IActionResult> GetProjectTasks(Guid projectId)
        {
            var tasks = (await _taskService.GetMany())
            .Where(dto=>dto.ProjectId==projectId)
            .Select(dto => _mapper.Map<ProjectTaskDto>(dto))
            .ToArray();

            return Ok(tasks);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProjectTasksForUser()
        {
            var userId = User.FindFirst("userId")?.Value;
            var tasks = (await _taskService.GetMany())
            .Where(dto => dto.UserId == Guid.Parse(userId))
            .Select(dto => _mapper.Map<ProjectTaskDto>(dto))
            .ToArray();
            return Ok(tasks);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetStatusProjectTasks()
        {
            var projectTaskStatuses= (await _taskStatusService.GetMany())
            .Select(dto => _mapper.Map<ProjectTaskStatusDto>(dto))
            .ToArray();

            return Ok(projectTaskStatuses);
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
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(GetUserId());
                var dto = _mapper.Map<ProjectTaskDto>(request);
                var task = await _taskService.CreateTask(dto, userId, cancellationToken);
                return Ok(_mapper.Map<ProjectTaskViewModel>(task));
            }
            else
            {
                return BadRequest();
            }
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
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<ProjectTaskDto>(request);

                return Ok(_mapper.Map<ProjectTaskViewModel>(await _taskService.Update(dto, cancellationToken)));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
