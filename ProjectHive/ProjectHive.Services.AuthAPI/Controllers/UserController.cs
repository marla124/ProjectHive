using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectHive.Services.AuthAPI.Dto;
using ProjectHive.Services.AuthAPI.Model;
using ProjectHive.Services.AuthAPI.Models.RequestModel;
using ProjectHive.Services.AuthAPI.Services;

namespace ProjectHive.Services.AuthAPI.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public UserController(IMapper mapper, IUserService userService)
    {
        _userService = userService;
        _mapper = mapper;
    }
    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var project = _mapper.Map<UserViewModel>(await _userService.GetById(id, cancellationToken));
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    [HttpDelete("[action]/{id}")]
    public async Task<IActionResult> DeleteById(Guid id, CancellationToken cancellationToken)
    {
        await _userService.DeleteById(id, cancellationToken);
        return Ok();
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<IActionResult> CreateUser(CreateUserRequestViewModel request, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<UserDto>(request);

        return Ok(_mapper.Map<UserViewModel>(await _userService.Create(dto, cancellationToken)));
    }

    [HttpPatch]
    [Route("[action]")]
    public async Task<IActionResult> UpdateUser(UpdateUserRequestViewModel request, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<UserDto>(request);

        return Ok(_mapper.Map<UserViewModel>(await _userService.Update(dto, cancellationToken)));
    }
}
