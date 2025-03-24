using Microsoft.AspNetCore.Mvc;
using TaskAlbayan.Service;
using TaskAlbayan.Utils;

namespace TaskAlbayan.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TaskController : ControllerBase
{
    private readonly TaskService _service;

    public TaskController(TaskService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<HttpResponse<List<TaskDto>>> GetAll()
    {
        return new HttpResponse<List<TaskDto>>(await _service.GetAllAsync());

    }
    [HttpGet("{id}")]
    public async Task<HttpResponse<TaskDto>> GetById(Guid id)
    {
        return new HttpResponse<TaskDto>(await _service.GetById(id));
    }

    [HttpPost]
    public async Task<HttpResponse<TaskDto>> AddAsync(CreateUpdateTaskDto createUpdateTaskDto)
    {
        return new HttpResponse<TaskDto>(await _service.AddAsync(createUpdateTaskDto));
    }

    [HttpPut("{id}")]
    public async Task<HttpResponse<TaskDto>>UpdateAsync(Guid id, CreateUpdateTaskDto createUpdateTaskDto)
    {
        return new HttpResponse<TaskDto>(await _service.UpdateAsync(id, createUpdateTaskDto));

    }


    [HttpDelete("{id}")]
    public async Task<HttpResponse<Guid>> DeleteAsync(Guid id)
    {
        return new HttpResponse<Guid>(await _service.DeleteAsync(id));

    }

}
