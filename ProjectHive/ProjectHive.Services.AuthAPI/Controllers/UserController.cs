using AutoMapper;
using Azure.Core;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.AuthAPI.Dto;
using ProjectHive.Services.AuthAPI.FluentValidation;
using ProjectHive.Services.AuthAPI.Model;
using ProjectHive.Services.AuthAPI.Models;
using ProjectHive.Services.AuthAPI.Models.RequestModel;
using ProjectHive.Services.AuthAPI.Services;

namespace ProjectHive.Services.AuthAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService userService, IMapper mapper) : BaseController
{
    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var project = mapper.Map<UserViewModel>(await userService.GetById(id, cancellationToken));
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var projects = mapper.Map<List<UserViewModel>>(await userService.GetMany(cancellationToken));
        return Ok(projects);
    }
    [Authorize]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetFriendlyUsers(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(GetUserId());
        var projects = mapper.Map<List<UserViewModel>>(await userService.GetFriendlyUsers(userId, cancellationToken));
        return Ok(projects);
    }

    [Authorize]
    [HttpPost("[action]/{friendlyUserId}")]
    public async Task<IActionResult> AddFriendlyUser(Guid friendlyUserId, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(GetUserId());
        await userService.AddFriendlyUser(friendlyUserId, userId, cancellationToken);
        return Ok();
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        await userService.DeleteById(id, cancellationToken);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateUser(RegisterModel request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var dto = mapper.Map<UserDto>(request);
            var user=await userService.RegisterUser(dto, cancellationToken);
            return Created($"users/{user.Id}", user);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPatch]
    [Route("[action]")]
    public async Task<IActionResult> UpdateUser(UpdateUserRequestViewModel request, CancellationToken cancellationToken)
    {
        if (ModelState.IsValid)
        {
            var dto = mapper.Map<UserDto>(request);

            return Ok(mapper.Map<UserViewModel>(await userService.Update(dto, cancellationToken)));
        }
        else
        {
            return BadRequest();
        }
    }
}
