using Microsoft.AspNetCore.Mvc;
using TaskAlbayan.Common;
using TaskAlbayan.Common.Dtos;
using TaskAlbayan.Service;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<HttpResponse<List<UserDto>>> GetAll()
    {
        return new HttpResponse<List<UserDto>>(await _userService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<HttpResponse<UserDto>> GetById(Guid id)
    {
        return new HttpResponse<UserDto>(await _userService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<HttpResponse<UserDto>> AddAsync(CreateUpdateUserDto createUserDto)
    {
        return new HttpResponse<UserDto>(await _userService.RegisterAsync(createUserDto));
    }

    [HttpPut("{id}")]
    public async Task<HttpResponse<UserDto>> UpdateAsync(Guid id, CreateUpdateUserDto updateUserDto)
    {
        return new HttpResponse<UserDto>(await _userService.UpdateAsync(id, updateUserDto));
    }

    [HttpDelete("{id}")]
    public async Task<HttpResponse<Guid>> DeleteAsync(Guid id)
    {
        return new HttpResponse<Guid>(await _userService.DeleteAsync(id));
    }
}