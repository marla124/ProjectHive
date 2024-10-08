﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.ProjectsAPI.Business.Services;
using ProjectHive.Services.ProjectsAPI.Dto;
using ProjectHive.Services.ProjectsAPI.Models;
using ProjectHive.Services.ProjectsAPI.Models.RequestModel;

namespace ProjectHive.Services.ProjectsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController(IMapper mapper, IProjectService projectService, IProjectStatusService projectStatusService) : BaseController
    {
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var project = mapper.Map<ProjectViewModel>(await projectService.GetById(id, cancellationToken));
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetStatusProject(CancellationToken cancellationToken)
        {
            var projectStatuses = await projectStatusService.GetProjectStatuses(cancellationToken);
            return Ok(projectStatuses);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProjects(CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var userGuid = Guid.Parse(userId);
            var projects = await projectService.GetProjectsForUser(userGuid, cancellationToken);

            return Ok(mapper.Map<IEnumerable<ProjectViewModel>>(projects));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            await projectService.DeleteById(id, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteManyProject(IEnumerable<ProjectViewModel> request, CancellationToken cancellationToken)
        {
            await projectService.DeleteMany(mapper.Map<IEnumerable<ProjectDto>>(request), cancellationToken);
            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateProject(CreateProjectRequestViewModel request, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var userId = GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }

                var userGuid = Guid.Parse(userId);

                var dto = mapper.Map<ProjectDto>(request);
                dto.CreatorUserId = userGuid;

                return Ok(mapper.Map<ProjectViewModel>(await projectService.CreateProject(dto, cancellationToken)));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateManyProject(IEnumerable<CreateProjectRequestViewModel> request, CancellationToken cancellationToken)
        {
            await projectService.CreateMany(mapper.Map<IEnumerable<ProjectDto>>(request), cancellationToken);
            return Ok();
        }

        [HttpPatch]
        [Route("[action]")]
        public async Task<IActionResult> UpdateProject(UpdateProjectRequestViewModel request, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var dto = mapper.Map<ProjectDto>(request);

                return Ok(mapper.Map<ProjectViewModel>(await projectService.Update(dto, cancellationToken)));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
