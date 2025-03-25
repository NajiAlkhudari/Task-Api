using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskAlbayan.Common.Exceptions;
using TaskAlbayan.DB;
using TaskAlbayan.Service;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TaskController : ControllerBase
{
    private readonly TaskService _service;
    private readonly BaseService _baseService;


    public TaskController(TaskService service, BaseService baseService)
    {
        _service = service;
        _baseService = baseService;
    }

    [HttpGet]
    [Authorize]
    public async Task<HttpResponse<List<TaskDto>>> GetAll()
    {
        if (_baseService.CheckUserRole(UserRoles.Admin))
            return new HttpResponse<List<TaskDto>>(await _service.GetAllAsync());
        else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));


    }
    [HttpGet("{id}")]
    [Authorize]
    public async Task<HttpResponse<TaskDto>> GetById(Guid id)
    {
        if (_baseService.CheckUserRole(UserRoles.Admin))
            return new HttpResponse<TaskDto>(await _service.GetById(id));
        else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));
    }

    [HttpPost]
    [Authorize]
    public async Task<HttpResponse<TaskDto>> AddAsync(CreateUpdateTaskDto createUpdateTaskDto)
    {
        if (_baseService.CheckUserRole(UserRoles.User))
            return new HttpResponse<TaskDto>(await _service.AddAsync(createUpdateTaskDto));
        else throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));

    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<HttpResponse<TaskDto>> UpdateAsync(Guid id, CreateUpdateTaskDto createUpdateTaskDto)
    {
        if (_baseService.CheckUserRole(UserRoles.Admin))
            return new HttpResponse<TaskDto>(await _service.UpdateAsync(id, createUpdateTaskDto));
        else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));

    }


    [HttpDelete("{id}")]
    [Authorize]
    public async Task<HttpResponse<Guid>> DeleteAsync(Guid id)
    {
        if (_baseService.CheckUserRole(UserRoles.Admin))
            return new HttpResponse<Guid>(await _service.DeleteAsync(id));
        else
            throw new BaseException("Unauthorized", 403, new ErrorResponse("Unauthorized", 403, "You don't have Role"));

    }

}
