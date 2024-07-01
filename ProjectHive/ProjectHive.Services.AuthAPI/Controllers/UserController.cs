using AutoMapper;
using FluentValidation;
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
public class UserController(IUserService userService, IMapper mapper, IValidator<RegisterModel> userRegisterValidator) : Controller
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

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        await userService.DeleteById(id, cancellationToken);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateUser(RegisterModel request, CancellationToken cancellationToken)
    {
        if ((await userRegisterValidator.ValidateAsync(request)).IsValid)
        {
            var dto = mapper.Map<UserDto>(request);
            await userService.RegisterUser(dto, cancellationToken);
            var user = await userService.GetByEmail(request.Email, cancellationToken);
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
        var dto = mapper.Map<UserDto>(request);

        return Ok(mapper.Map<UserViewModel>(await userService.Update(dto, cancellationToken)));
    }
}
