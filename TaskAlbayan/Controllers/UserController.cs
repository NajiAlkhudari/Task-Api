using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskAlbayan.Common;
using TaskAlbayan.Common.Dtos;
using TaskAlbayan.Common.Exceptions;
using TaskAlbayan.DB;
using TaskAlbayan.Service;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly BaseService _baseService;

    public UserController(UserService userService, BaseService baseService)
    {
        _userService = userService;
        _baseService = baseService;
    }

    [HttpGet]
    [Authorize]
    public async Task<HttpResponse<List<UserDto>>> GetAll()
    {
        if (_baseService.CheckUserRole(UserRoles.Admin))
            return new HttpResponse<List<UserDto>>(await _userService.GetAllAsync());
        else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));

    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<HttpResponse<UserDto>> GetById(Guid id)
    {
        if (_baseService.CheckUserRole(UserRoles.Admin))
            return new HttpResponse<UserDto>(await _userService.GetByIdAsync(id));
        else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));
    }

    [HttpPost]
    [Authorize]
    public async Task<HttpResponse<UserDto>> AddAsync(CreateUpdateUserDto createUserDto)
    {
        if (_baseService.CheckUserRole(UserRoles.Admin))
            return new HttpResponse<UserDto>(await _userService.RegisterAsync(createUserDto));
        else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));

    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<HttpResponse<UserDto>> UpdateAsync(Guid id, CreateUpdateUserDto updateUserDto)
    {
        if (_baseService.CheckUserRole(UserRoles.Admin))
            return new HttpResponse<UserDto>(await _userService.UpdateAsync(id, updateUserDto));
        else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));

    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<HttpResponse<Guid>> DeleteAsync(Guid id)
    {
        if (_baseService.CheckUserRole(UserRoles.Admin))
            return new HttpResponse<Guid>(await _userService.DeleteAsync(id));
        else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));

    }
}